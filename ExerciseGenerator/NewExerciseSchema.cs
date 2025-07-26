using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Nuevo esquema de ejercicio que separa el código del JSON usando referencias a archivos
    /// </summary>
    public class NewExerciseSchema
    {
        [JsonPropertyName("metadata")]
        public NewExerciseMetadata Metadata { get; set; } = new()
        {
            Id = "",
            Title = "",
            TopicId = "",
            ExerciseTypeId = "",
            SkillLevelId = ""
        };

        [JsonPropertyName("content")]
        public NewExerciseContent Content { get; set; } = new();

        [JsonPropertyName("files")]
        public ExerciseFiles Files { get; set; } = new();

        [JsonPropertyName("extensions")]
        public List<string> Extensions { get; set; } = new();

        [JsonPropertyName("pedagogical")]
        public PedagogicalInfo Pedagogical { get; set; } = new();
    }

    /// <summary>
    /// Metadatos del ejercicio usando IDs configurables
    /// </summary>
    public class NewExerciseMetadata
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("topicId")]
        public required string TopicId { get; set; }

        [JsonPropertyName("exerciseTypeId")]
        public required string ExerciseTypeId { get; set; }

        [JsonPropertyName("skillLevelId")]
        public required string SkillLevelId { get; set; }

        [JsonPropertyName("context")]
        public string Context { get; set; } = "";

        [JsonPropertyName("estimatedMinutes")]
        public int EstimatedMinutes { get; set; }

        [JsonPropertyName("complexityScore")]
        public int ComplexityScore { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.0";

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [JsonPropertyName("author")]
        public string Author { get; set; } = "";

        [JsonPropertyName("reviewStatus")]
        public string ReviewStatus { get; set; } = "Draft";
    }

    /// <summary>
    /// Contenido descriptivo del ejercicio (sin código)
    /// </summary>
    public class NewExerciseContent
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = "";

        [JsonPropertyName("learningObjectives")]
        public List<string> LearningObjectives { get; set; } = new();

        [JsonPropertyName("prerequisites")]
        public List<string> Prerequisites { get; set; } = new();

        [JsonPropertyName("problemStatement")]
        public string ProblemStatement { get; set; } = "";

        [JsonPropertyName("technicalRequirements")]
        public List<string> TechnicalRequirements { get; set; } = new();

        [JsonPropertyName("successCriteria")]
        public List<string> SuccessCriteria { get; set; } = new();

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; } = "";

        [JsonPropertyName("hints")]
        public List<string> Hints { get; set; } = new();

        [JsonPropertyName("expectedOutput")]
        public string ExpectedOutput { get; set; } = "";
    }

    /// <summary>
    /// Referencias a archivos de código externos
    /// </summary>
    public class ExerciseFiles
    {
        [JsonPropertyName("beforeCodeFile")]
        public string BeforeCodeFile { get; set; } = "";

        [JsonPropertyName("afterCodeFile")]
        public string AfterCodeFile { get; set; } = "";

        [JsonPropertyName("starterTemplateFile")]
        public string StarterTemplateFile { get; set; } = "";

        [JsonPropertyName("unitTestsFile")]
        public string UnitTestsFile { get; set; } = "";

        [JsonPropertyName("projectFile")]
        public string ProjectFile { get; set; } = "";

        [JsonPropertyName("additionalFiles")]
        public Dictionary<string, string> AdditionalFiles { get; set; } = new();

        [JsonPropertyName("configFiles")]
        public Dictionary<string, string> ConfigFiles { get; set; } = new();

        [JsonPropertyName("resourceFiles")]
        public Dictionary<string, string> ResourceFiles { get; set; } = new();

        /// <summary>
        /// Verifica si todos los archivos requeridos están especificados
        /// </summary>
        public bool HasRequiredFiles()
        {
            return !string.IsNullOrEmpty(StarterTemplateFile);
        }

        /// <summary>
        /// Obtiene todos los archivos como una lista
        /// </summary>
        public IEnumerable<(string Type, string FileName)> GetAllFiles()
        {
            if (!string.IsNullOrEmpty(BeforeCodeFile))
                yield return ("BeforeCode", BeforeCodeFile);
            
            if (!string.IsNullOrEmpty(AfterCodeFile))
                yield return ("AfterCode", AfterCodeFile);
                
            if (!string.IsNullOrEmpty(StarterTemplateFile))
                yield return ("StarterTemplate", StarterTemplateFile);
                
            if (!string.IsNullOrEmpty(UnitTestsFile))
                yield return ("UnitTests", UnitTestsFile);
                
            if (!string.IsNullOrEmpty(ProjectFile))
                yield return ("Project", ProjectFile);

            foreach (var (key, value) in AdditionalFiles)
                yield return ("Additional", value);

            foreach (var (key, value) in ConfigFiles)
                yield return ("Config", value);

            foreach (var (key, value) in ResourceFiles)
                yield return ("Resource", value);
        }
    }

    /// <summary>
    /// Información pedagógica del ejercicio
    /// </summary>
    public class PedagogicalInfo
    {
        [JsonPropertyName("keyConcepts")]
        public List<string> KeyConcepts { get; set; } = new();

        [JsonPropertyName("commonPitfalls")]
        public List<string> CommonPitfalls { get; set; } = new();

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; } = "";

        [JsonPropertyName("teachingTips")]
        public List<string> TeachingTips { get; set; } = new();

        [JsonPropertyName("relatedConcepts")]
        public List<string> RelatedConcepts { get; set; } = new();

        [JsonPropertyName("furtherReading")]
        public List<string> FurtherReading { get; set; } = new();

        [JsonPropertyName("assessmentCriteria")]
        public List<string> AssessmentCriteria { get; set; } = new();

        [JsonPropertyName("difficultyFactors")]
        public List<string> DifficultyFactors { get; set; } = new();
    }

    /// <summary>
    /// Extensiones del nuevo esquema para compatibilidad con el sistema existente
    /// </summary>
    public static class NewExerciseSchemaExtensions
    {
        /// <summary>
        /// Genera un ID único para el ejercicio basado en sus metadatos
        /// </summary>
        public static string GenerateId(this NewExerciseSchema exercise)
        {
            var parts = new[]
            {
                exercise.Metadata.TopicId.ToLowerInvariant(),
                exercise.Metadata.ExerciseTypeId.ToLowerInvariant(),
                exercise.Metadata.SkillLevelId.ToLowerInvariant()
            };

            var baseId = string.Join("-", parts);
            
            if (!string.IsNullOrEmpty(exercise.Metadata.Context))
            {
                baseId += "-" + exercise.Metadata.Context.ToLowerInvariant();
            }

            return baseId;
        }

        /// <summary>
        /// Valida que el ejercicio tenga todos los campos requeridos
        /// </summary>
        public static IEnumerable<string> ValidateRequiredFields(this NewExerciseSchema exercise)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(exercise.Metadata.Title))
                errors.Add("Title is required");

            if (string.IsNullOrWhiteSpace(exercise.Metadata.TopicId))
                errors.Add("TopicId is required");

            if (string.IsNullOrWhiteSpace(exercise.Metadata.ExerciseTypeId))
                errors.Add("ExerciseTypeId is required");

            if (string.IsNullOrWhiteSpace(exercise.Metadata.SkillLevelId))
                errors.Add("SkillLevelId is required");

            if (string.IsNullOrWhiteSpace(exercise.Content.Description))
                errors.Add("Description is required");

            if (string.IsNullOrWhiteSpace(exercise.Content.ProblemStatement))
                errors.Add("ProblemStatement is required");

            if (!exercise.Files.HasRequiredFiles())
                errors.Add("StarterTemplateFile is required");

            return errors;
        }

        /// <summary>
        /// Calcula el tiempo estimado basado en la configuración
        /// </summary>
        public static int CalculateEstimatedTime(this NewExerciseSchema exercise, 
            Configuration.ExerciseTypeConfig exerciseTypeConfig,
            Configuration.SkillLevelConfig skillLevelConfig)
        {
            var baseTime = exercise.Metadata.EstimatedMinutes > 0 
                ? exercise.Metadata.EstimatedMinutes 
                : skillLevelConfig.AverageExerciseMinutes;

            return exerciseTypeConfig.CalculateEstimatedTime(baseTime);
        }
    }
}