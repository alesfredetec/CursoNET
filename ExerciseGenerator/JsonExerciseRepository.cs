using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Implementación del repositorio de ejercicios que lee desde archivos JSON
    /// </summary>
    public class JsonExerciseRepository : IExerciseRepository
    {
        private readonly string _basePath;
        private readonly Dictionary<string, ExerciseJsonSchema> _exerciseCache;
        private readonly JsonSerializerOptions _jsonOptions;
        private DateTime _lastCacheUpdate;

        public JsonExerciseRepository(string basePath = "jsondata")
        {
            _basePath = Path.GetFullPath(basePath);
            _exerciseCache = new Dictionary<string, ExerciseJsonSchema>();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            
            // Cargar ejercicios al inicializar
            Task.Run(async () => await RefreshCacheAsync());
        }

        public async Task<ExerciseJsonSchema?> FindExerciseAsync(SkillLevel level, TopicArea topic, ExerciseType type, string context = "")
        {
            await EnsureCacheLoadedAsync();

            // Buscar ejercicio exacto
            var exactMatch = _exerciseCache.Values.FirstOrDefault(e =>
                e.Metadata.Level.Equals(level.ToString(), StringComparison.OrdinalIgnoreCase) &&
                e.Metadata.Topic.Equals(topic.ToString(), StringComparison.OrdinalIgnoreCase) &&
                e.Metadata.Type.Equals(type.ToString(), StringComparison.OrdinalIgnoreCase) &&
                (string.IsNullOrEmpty(context) || e.Metadata.Context.Equals(context, StringComparison.OrdinalIgnoreCase)));

            if (exactMatch != null)
                return exactMatch;

            // Buscar sin contexto específico si no se encontró exacto
            if (!string.IsNullOrEmpty(context))
            {
                return _exerciseCache.Values.FirstOrDefault(e =>
                    e.Metadata.Level.Equals(level.ToString(), StringComparison.OrdinalIgnoreCase) &&
                    e.Metadata.Topic.Equals(topic.ToString(), StringComparison.OrdinalIgnoreCase) &&
                    e.Metadata.Type.Equals(type.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        public async Task<IEnumerable<ExerciseJsonSchema>> FindExercisesAsync(SkillLevel? level = null, TopicArea? topic = null, ExerciseType? type = null)
        {
            await EnsureCacheLoadedAsync();

            var query = _exerciseCache.Values.AsEnumerable();

            if (level.HasValue)
                query = query.Where(e => e.Metadata.Level.Equals(level.ToString(), StringComparison.OrdinalIgnoreCase));

            if (topic.HasValue)
                query = query.Where(e => e.Metadata.Topic.Equals(topic.ToString(), StringComparison.OrdinalIgnoreCase));

            if (type.HasValue)
                query = query.Where(e => e.Metadata.Type.Equals(type.ToString(), StringComparison.OrdinalIgnoreCase));

            return query.ToList();
        }

        public async Task<IEnumerable<ExerciseJsonSchema>> ListAllExercisesAsync()
        {
            await EnsureCacheLoadedAsync();
            return _exerciseCache.Values.ToList();
        }

        public async Task<ExerciseJsonSchema?> GetExerciseByIdAsync(string id)
        {
            await EnsureCacheLoadedAsync();
            _exerciseCache.TryGetValue(id, out var exercise);
            return exercise;
        }

        public async Task<bool> ExistsAsync(SkillLevel level, TopicArea topic, ExerciseType type, string context = "")
        {
            var exercise = await FindExerciseAsync(level, topic, type, context);
            return exercise != null;
        }

        public async Task<ExerciseRepositoryStats> GetStatsAsync()
        {
            await EnsureCacheLoadedAsync();

            var exercises = _exerciseCache.Values.ToList();
            var stats = new ExerciseRepositoryStats
            {
                TotalExercises = exercises.Count,
                LastUpdated = _lastCacheUpdate,
                RepositoryType = "JSON Files"
            };

            // Estadísticas por nivel
            foreach (SkillLevel level in Enum.GetValues<SkillLevel>())
            {
                stats.ExercisesByLevel[level] = exercises.Count(e => 
                    e.Metadata.Level.Equals(level.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            // Estadísticas por tema
            foreach (TopicArea topic in Enum.GetValues<TopicArea>())
            {
                stats.ExercisesByTopic[topic] = exercises.Count(e => 
                    e.Metadata.Topic.Equals(topic.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            // Estadísticas por tipo
            foreach (ExerciseType type in Enum.GetValues<ExerciseType>())
            {
                stats.ExercisesByType[type] = exercises.Count(e => 
                    e.Metadata.Type.Equals(type.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return stats;
        }

        public async Task<bool> SaveExerciseAsync(ExerciseJsonSchema exercise)
        {
            try
            {
                var filePath = GetExerciseFilePath(exercise);
                var directory = Path.GetDirectoryName(filePath);
                
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonSerializer.Serialize(exercise, _jsonOptions);
                await File.WriteAllTextAsync(filePath, json);

                // Actualizar caché
                _exerciseCache[exercise.Metadata.Id] = exercise;

                return true;
            }
            catch (Exception ex)
            {
                throw new ExerciseRepositoryException($"Error saving exercise {exercise.Metadata.Id}: {ex.Message}", ex);
            }
        }

        public Task<bool> DeleteExerciseAsync(string id)
        {
            try
            {
                if (!_exerciseCache.TryGetValue(id, out var exercise))
                    return Task.FromResult(false);

                var filePath = GetExerciseFilePath(exercise);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _exerciseCache.Remove(id);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new ExerciseRepositoryException($"Error deleting exercise {id}: {ex.Message}", ex);
            }
        }

        public async Task RefreshCacheAsync()
        {
            try
            {
                _exerciseCache.Clear();

                if (!Directory.Exists(_basePath))
                {
                    Directory.CreateDirectory(_basePath);
                    _lastCacheUpdate = DateTime.Now;
                    return;
                }

                var jsonFiles = Directory.GetFiles(_basePath, "*.json", SearchOption.AllDirectories);

                foreach (var filePath in jsonFiles)
                {
                    try
                    {
                        var json = await File.ReadAllTextAsync(filePath);
                        var exercise = JsonSerializer.Deserialize<ExerciseJsonSchema>(json, _jsonOptions);
                        
                        if (exercise != null && !string.IsNullOrEmpty(exercise.Metadata.Id))
                        {
                            _exerciseCache[exercise.Metadata.Id] = exercise;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error but continue loading other exercises
                        Console.WriteLine($"Warning: Could not load exercise from {filePath}: {ex.Message}");
                    }
                }

                _lastCacheUpdate = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw new ExerciseRepositoryException($"Error refreshing exercise cache: {ex.Message}", ex);
            }
        }

        private async Task EnsureCacheLoadedAsync()
        {
            if (_exerciseCache.Count == 0 || (DateTime.Now - _lastCacheUpdate).TotalMinutes > 30)
            {
                await RefreshCacheAsync();
            }
        }

        private string GetExerciseFilePath(ExerciseJsonSchema exercise)
        {
            var level = exercise.Metadata.Level.ToLowerInvariant();
            var topic = ConvertTopicToPath(exercise.Metadata.Topic);
            var type = exercise.Metadata.Type.ToLowerInvariant();
            var fileName = $"{exercise.Metadata.Context.ToLowerInvariant()}.json";

            if (string.IsNullOrEmpty(exercise.Metadata.Context))
            {
                fileName = $"{topic}-{type}.json";
            }

            return Path.Combine(_basePath, level, topic, type, fileName);
        }

        private string ConvertTopicToPath(string topic)
        {
            return topic switch
            {
                "CSharpFundamentals" => "csharp-fundamentals",
                "ControlStructures" => "control-structures",
                "MethodsAndParameters" => "methods-parameters",
                "BasicOOP" => "basic-oop",
                "AdvancedOOP" => "advanced-oop",
                "DelegatesAndEvents" => "delegates-events",
                "FileIO" => "file-io",
                "UnitTesting" => "unit-testing",
                "DesignPatterns" => "design-patterns",
                "EntityFramework" => "entity-framework",
                "AspNetCore" => "aspnet-core",
                "AsyncProgramming" => "async-programming",
                "PerformanceOptimization" => "performance-optimization",
                _ => topic.ToLowerInvariant()
            };
        }
    }
}