using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Administrador central de configuraciones del sistema de ejercicios
    /// </summary>
    public class ConfigurationManager
    {
        private readonly string _configPath;
        private readonly JsonSerializerOptions _jsonOptions;
        
        public TopicConfiguration Topics { get; private set; }
        public ExerciseTypeConfiguration ExerciseTypes { get; private set; }
        public SkillLevelConfiguration SkillLevels { get; private set; } 
        public DependencyConfiguration Dependencies { get; private set; }
        
        public DateTime LastLoaded { get; private set; }
        public bool IsLoaded { get; private set; }

        public ConfigurationManager(string configPath = "jsondata/config")
        {
            _configPath = Path.GetFullPath(configPath);
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
            
            // Inicializar con configuraciones vacías
            Topics = new TopicConfiguration(new List<TopicConfig>());
            ExerciseTypes = new ExerciseTypeConfiguration(new List<ExerciseTypeConfig>());
            SkillLevels = new SkillLevelConfiguration(new List<SkillLevelConfig>());
            Dependencies = new DependencyConfiguration(new DependenciesJsonSchema());
        }

        /// <summary>
        /// Carga todas las configuraciones desde archivos JSON
        /// </summary>
        public async Task LoadAllAsync()
        {
            try
            {
                await Task.WhenAll(
                    LoadTopicsAsync(),
                    LoadExerciseTypesAsync(),
                    LoadSkillLevelsAsync(),
                    LoadDependenciesAsync()
                );

                ValidateConfigurations();
                LastLoaded = DateTime.Now;
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"Error loading configurations: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Valida que las configuraciones sean consistentes entre sí
        /// </summary>
        public void ValidateConfigurations()
        {
            var validator = new ConfigurationValidator(this);
            var results = validator.ValidateAll();
            
            var errors = results.Where(r => !r.IsValid).ToList();
            if (errors.Any())
            {
                var errorMessages = string.Join("\n", errors.Select(e => e.ErrorMessage));
                throw new ConfigurationValidationException($"Configuration validation failed:\n{errorMessages}");
            }
        }

        /// <summary>
        /// Obtiene un topic por su ID
        /// </summary>
        public TopicConfig? GetTopic(string topicId)
        {
            return Topics.GetById(topicId);
        }

        /// <summary>
        /// Obtiene un exercise type por su ID
        /// </summary>
        public ExerciseTypeConfig? GetExerciseType(string exerciseTypeId)
        {
            return ExerciseTypes.GetById(exerciseTypeId);
        }

        /// <summary>
        /// Obtiene un skill level por su ID
        /// </summary>
        public SkillLevelConfig? GetSkillLevel(string skillLevelId)
        {
            return SkillLevels.GetById(skillLevelId);
        }

        /// <summary>
        /// Obtiene todos los topics disponibles para un nivel específico
        /// </summary>
        public IEnumerable<TopicConfig> GetTopicsForLevel(string skillLevel)
        {
            return Topics.GetForSkillLevel(skillLevel);
        }

        /// <summary>
        /// Obtiene todos los exercise types disponibles para un nivel específico
        /// </summary>
        public IEnumerable<ExerciseTypeConfig> GetExerciseTypesForLevel(string skillLevel)
        {
            return ExerciseTypes.GetForSkillLevel(skillLevel);
        }

        /// <summary>
        /// Verifica si un topic tiene todos sus prerequisitos satisfechos
        /// </summary>
        public bool ArePrerequisitesSatisfied(string topicId, IEnumerable<string> completedTopics)
        {
            return Dependencies.ArePrerequisitesSatisfied(topicId, completedTopics);
        }

        /// <summary>
        /// Obtiene la ruta de configuración resuelta
        /// </summary>
        public string GetConfigPath() => _configPath;

        private async Task LoadTopicsAsync()
        {
            var filePath = Path.Combine(_configPath, "topics.json");
            await EnsureFileExists(filePath, "topics configuration");
            
            var json = await File.ReadAllTextAsync(filePath);
            var data = JsonSerializer.Deserialize<TopicsJsonSchema>(json, _jsonOptions);
            
            if (data?.Topics == null)
                throw new ConfigurationException("Invalid topics configuration: missing topics array");
                
            Topics = new TopicConfiguration(data.Topics);
        }

        private async Task LoadExerciseTypesAsync()
        {
            var filePath = Path.Combine(_configPath, "exercise-types.json");
            await EnsureFileExists(filePath, "exercise types configuration");
            
            var json = await File.ReadAllTextAsync(filePath);
            var data = JsonSerializer.Deserialize<ExerciseTypesJsonSchema>(json, _jsonOptions);
            
            if (data?.ExerciseTypes == null)
                throw new ConfigurationException("Invalid exercise types configuration: missing exerciseTypes array");
                
            ExerciseTypes = new ExerciseTypeConfiguration(data.ExerciseTypes);
        }

        private async Task LoadSkillLevelsAsync()
        {
            var filePath = Path.Combine(_configPath, "skill-levels.json");
            await EnsureFileExists(filePath, "skill levels configuration");
            
            var json = await File.ReadAllTextAsync(filePath);
            var data = JsonSerializer.Deserialize<SkillLevelsJsonSchema>(json, _jsonOptions);
            
            if (data?.SkillLevels == null)
                throw new ConfigurationException("Invalid skill levels configuration: missing skillLevels array");
                
            SkillLevels = new SkillLevelConfiguration(data.SkillLevels);
        }

        private async Task LoadDependenciesAsync()
        {
            var filePath = Path.Combine(_configPath, "dependencies.json");
            await EnsureFileExists(filePath, "dependencies configuration");
            
            var json = await File.ReadAllTextAsync(filePath);
            var data = JsonSerializer.Deserialize<DependenciesJsonSchema>(json, _jsonOptions);
            
            if (data == null)
                throw new ConfigurationException("Invalid dependencies configuration");
                
            Dependencies = new DependencyConfiguration(data);
        }

        private Task EnsureFileExists(string filePath, string configType)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Configuration file not found: {filePath} ({configType})");
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Excepción específica para errores de configuración
    /// </summary>
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
        public ConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Excepción específica para errores de validación de configuración
    /// </summary>
    public class ConfigurationValidationException : ConfigurationException
    {
        public ConfigurationValidationException(string message) : base(message) { }
    }
}