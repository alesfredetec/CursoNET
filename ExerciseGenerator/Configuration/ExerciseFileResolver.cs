using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Resuelve y carga archivos de código externos referenciados en ejercicios
    /// </summary>
    public class ExerciseFileResolver
    {
        private readonly string _basePath;
        private readonly Dictionary<string, string> _fileCache;
        private readonly ConfigurationManager _configManager;

        public ExerciseFileResolver(string basePath, ConfigurationManager configManager)
        {
            _basePath = Path.GetFullPath(basePath);
            _configManager = configManager ?? throw new ArgumentNullException(nameof(configManager));
            _fileCache = new Dictionary<string, string>();
        }

        /// <summary>
        /// Carga un ejercicio completo incluyendo todos sus archivos de código
        /// </summary>
        public async Task<ResolvedExercise> LoadExerciseAsync(string exerciseJsonPath)
        {
            if (!File.Exists(exerciseJsonPath))
                throw new FileNotFoundException($"Exercise JSON not found: {exerciseJsonPath}");

            var exerciseDirectory = Path.GetDirectoryName(exerciseJsonPath);
            if (string.IsNullOrEmpty(exerciseDirectory))
                throw new InvalidOperationException("Cannot determine exercise directory");

            // Cargar el JSON del ejercicio
            var jsonContent = await File.ReadAllTextAsync(exerciseJsonPath);
            var exerciseSchema = JsonSerializer.Deserialize<NewExerciseSchema>(jsonContent, new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            });

            if (exerciseSchema == null)
                throw new InvalidOperationException($"Failed to deserialize exercise from: {exerciseJsonPath}");

            // Validar que las configuraciones referenciadas existen
            await ValidateExerciseReferences(exerciseSchema);

            // Cargar todos los archivos de código
            var resolvedFiles = await LoadExerciseFiles(exerciseSchema.Files, exerciseDirectory);

            return new ResolvedExercise
            {
                Schema = exerciseSchema,
                Files = resolvedFiles,
                DirectoryPath = exerciseDirectory,
                JsonPath = exerciseJsonPath
            };
        }

        /// <summary>
        /// Carga todos los archivos de código especificados en ExerciseFiles
        /// </summary>
        public async Task<ResolvedExerciseFiles> LoadExerciseFiles(ExerciseFiles fileReferences, string exerciseDirectory)
        {
            var resolvedFiles = new ResolvedExerciseFiles();

            // Cargar archivos principales
            resolvedFiles.BeforeCode = await LoadFileIfSpecified(fileReferences.BeforeCodeFile, exerciseDirectory);
            resolvedFiles.AfterCode = await LoadFileIfSpecified(fileReferences.AfterCodeFile, exerciseDirectory);
            resolvedFiles.StarterTemplate = await LoadFileIfSpecified(fileReferences.StarterTemplateFile, exerciseDirectory);
            resolvedFiles.UnitTests = await LoadFileIfSpecified(fileReferences.UnitTestsFile, exerciseDirectory);
            resolvedFiles.ProjectFile = await LoadFileIfSpecified(fileReferences.ProjectFile, exerciseDirectory);

            // Cargar archivos adicionales
            foreach (var (key, fileName) in fileReferences.AdditionalFiles)
            {
                var content = await LoadFileIfSpecified(fileName, exerciseDirectory);
                if (!string.IsNullOrEmpty(content))
                {
                    resolvedFiles.AdditionalFiles[key] = content;
                }
            }

            // Cargar archivos de configuración
            foreach (var (key, fileName) in fileReferences.ConfigFiles)
            {
                var content = await LoadFileIfSpecified(fileName, exerciseDirectory);
                if (!string.IsNullOrEmpty(content))
                {
                    resolvedFiles.ConfigFiles[key] = content;
                }
            }

            // Cargar archivos de recursos
            foreach (var (key, fileName) in fileReferences.ResourceFiles)
            {
                var content = await LoadFileIfSpecified(fileName, exerciseDirectory);
                if (!string.IsNullOrEmpty(content))
                {
                    resolvedFiles.ResourceFiles[key] = content;
                }
            }

            return resolvedFiles;
        }

        /// <summary>
        /// Valida que todas las referencias del ejercicio (topic, exerciseType, skillLevel) existen en las configuraciones
        /// </summary>
        public async Task ValidateExerciseReferences(NewExerciseSchema exercise)
        {
            if (!_configManager.IsLoaded)
                await _configManager.LoadAllAsync();

            var errors = new List<string>();

            // Validar topic
            if (_configManager.GetTopic(exercise.Metadata.TopicId) == null)
                errors.Add($"Topic '{exercise.Metadata.TopicId}' not found in configuration");

            // Validar exercise type
            if (_configManager.GetExerciseType(exercise.Metadata.ExerciseTypeId) == null)
                errors.Add($"Exercise type '{exercise.Metadata.ExerciseTypeId}' not found in configuration");

            // Validar skill level
            if (_configManager.GetSkillLevel(exercise.Metadata.SkillLevelId) == null)
                errors.Add($"Skill level '{exercise.Metadata.SkillLevelId}' not found in configuration");

            // Validar compatibilidad
            var topic = _configManager.GetTopic(exercise.Metadata.TopicId);
            var exerciseType = _configManager.GetExerciseType(exercise.Metadata.ExerciseTypeId);
            var skillLevel = _configManager.GetSkillLevel(exercise.Metadata.SkillLevelId);

            if (topic != null && !topic.IsAvailableForLevel(exercise.Metadata.SkillLevelId))
                errors.Add($"Topic '{exercise.Metadata.TopicId}' is not available for skill level '{exercise.Metadata.SkillLevelId}'");

            if (exerciseType != null && !exerciseType.IsAvailableForLevel(exercise.Metadata.SkillLevelId))
                errors.Add($"Exercise type '{exercise.Metadata.ExerciseTypeId}' is not available for skill level '{exercise.Metadata.SkillLevelId}'");

            if (errors.Count > 0)
                throw new ExerciseValidationException($"Exercise validation failed:\n{string.Join("\n", errors)}");
        }

        /// <summary>
        /// Genera la ruta del directorio del ejercicio basada en sus metadatos
        /// </summary>
        public string GenerateExerciseDirectoryPath(NewExerciseSchema exercise)
        {
            var topic = _configManager.GetTopic(exercise.Metadata.TopicId);
            var exerciseType = _configManager.GetExerciseType(exercise.Metadata.ExerciseTypeId);
            var skillLevel = _configManager.GetSkillLevel(exercise.Metadata.SkillLevelId);

            if (topic == null || exerciseType == null || skillLevel == null)
                throw new InvalidOperationException("Cannot generate path with invalid references");

            var parts = new[]
            {
                skillLevel.PathName,
                topic.PathName,
                exerciseType.PathName
            };

            return Path.Combine(_basePath, Path.Combine(parts));
        }

        /// <summary>
        /// Genera nombres de archivos apropiados para un ejercicio
        /// </summary>
        public ExerciseFiles GenerateFileNames(NewExerciseSchema exercise)
        {
            var context = !string.IsNullOrEmpty(exercise.Metadata.Context) 
                ? exercise.Metadata.Context.ToLowerInvariant() 
                : "exercise";

            return new ExerciseFiles
            {
                BeforeCodeFile = "before.cs",
                AfterCodeFile = "after.cs",
                StarterTemplateFile = "starter.cs",
                UnitTestsFile = "tests.cs",
                ProjectFile = "project.csproj"
            };
        }

        private async Task<string> LoadFileIfSpecified(string fileName, string directory)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "";

            var fullPath = Path.Combine(directory, fileName);
            var cacheKey = fullPath.ToLowerInvariant();

            // Usar caché si está disponible
            if (_fileCache.TryGetValue(cacheKey, out var cachedContent))
                return cachedContent;

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Warning: File not found: {fullPath}");
                return "";
            }

            try
            {
                var content = await File.ReadAllTextAsync(fullPath);
                _fileCache[cacheKey] = content; // Cachear para uso futuro
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to load file {fullPath}: {ex.Message}");
                return "";
            }
        }

        /// <summary>
        /// Limpia la caché de archivos
        /// </summary>
        public void ClearCache()
        {
            _fileCache.Clear();
        }
    }

    /// <summary>
    /// Ejercicio resuelto con todos sus archivos cargados
    /// </summary>
    public class ResolvedExercise
    {
        public NewExerciseSchema Schema { get; set; } = new();
        public ResolvedExerciseFiles Files { get; set; } = new();
        public string DirectoryPath { get; set; } = "";
        public string JsonPath { get; set; } = "";

        /// <summary>
        /// Convierte a Exercise para compatibilidad con sistema existente
        /// </summary>
        public Exercise ToLegacyExercise()
        {
            return new Exercise
            {
                Title = Schema.Metadata.Title,
                Description = Schema.Content.Description,
                LearningObjectives = Schema.Content.LearningObjectives,
                Prerequisites = Schema.Content.Prerequisites,
                ProblemStatement = Schema.Content.ProblemStatement,
                TechnicalRequirements = Schema.Content.TechnicalRequirements,
                SuccessCriteria = Schema.Content.SuccessCriteria,
                StarterCode = Files.StarterTemplate,
                SolutionCode = Files.AfterCode,
                UnitTestCode = Files.UnitTests,
                ExtensionChallenges = Schema.Extensions,
                CommonPitfalls = Schema.Pedagogical.CommonPitfalls,
                EstimatedMinutes = Schema.Metadata.EstimatedMinutes
            };
        }
    }

    /// <summary>
    /// Archivos de ejercicio resueltos (contenido cargado)
    /// </summary>
    public class ResolvedExerciseFiles
    {
        public string BeforeCode { get; set; } = "";
        public string AfterCode { get; set; } = "";
        public string StarterTemplate { get; set; } = "";
        public string UnitTests { get; set; } = "";
        public string ProjectFile { get; set; } = "";
        public Dictionary<string, string> AdditionalFiles { get; set; } = new();
        public Dictionary<string, string> ConfigFiles { get; set; } = new();
        public Dictionary<string, string> ResourceFiles { get; set; } = new();
    }

    /// <summary>
    /// Excepción para errores de validación de ejercicios
    /// </summary>
    public class ExerciseValidationException : Exception
    {
        public ExerciseValidationException(string message) : base(message) { }
        public ExerciseValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}