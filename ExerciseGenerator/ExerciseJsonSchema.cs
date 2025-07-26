using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Esquema JSON para ejercicios - Define la estructura de datos para serialización/deserialización
    /// </summary>
    public class ExerciseJsonSchema
    {
        [JsonPropertyName("metadata")]
        public ExerciseMetadata Metadata { get; set; } = new();

        [JsonPropertyName("content")]
        public ExerciseContent Content { get; set; } = new();

        [JsonPropertyName("code")]
        public ExerciseCode Code { get; set; } = new();

        [JsonPropertyName("extensions")]
        public List<string> Extensions { get; set; } = new();

        [JsonPropertyName("pedagogical")]
        public PedagogicalInfo Pedagogical { get; set; } = new();
    }

    public class ExerciseMetadata
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("level")]
        public string Level { get; set; } = "";

        [JsonPropertyName("topic")]
        public string Topic { get; set; } = "";

        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

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
    }

    public class ExerciseContent
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
    }

    public class ExerciseCode
    {
        [JsonPropertyName("beforeCode")]
        public string BeforeCode { get; set; } = "";

        [JsonPropertyName("afterCode")]
        public string AfterCode { get; set; } = "";

        [JsonPropertyName("starterTemplate")]
        public string StarterTemplate { get; set; } = "";

        [JsonPropertyName("unitTests")]
        public string UnitTests { get; set; } = "";

        [JsonPropertyName("projectFile")]
        public string ProjectFile { get; set; } = "";

        [JsonPropertyName("additionalFiles")]
        public Dictionary<string, string> AdditionalFiles { get; set; } = new();
    }


    /// <summary>
    /// Extensiones para convertir entre ExerciseJsonSchema y Exercise
    /// </summary>
    public static class ExerciseJsonExtensions
    {
        /// <summary>
        /// Convierte ExerciseJsonSchema a Exercise para uso en el sistema actual
        /// </summary>
        public static Exercise ToExercise(this ExerciseJsonSchema jsonSchema)
        {
            return new Exercise
            {
                Title = jsonSchema.Metadata.Title,
                Description = jsonSchema.Content.Description,
                LearningObjectives = jsonSchema.Content.LearningObjectives,
                Prerequisites = jsonSchema.Content.Prerequisites,
                ProblemStatement = jsonSchema.Content.ProblemStatement,
                TechnicalRequirements = jsonSchema.Content.TechnicalRequirements,
                SuccessCriteria = jsonSchema.Content.SuccessCriteria,
                StarterCode = jsonSchema.Code.StarterTemplate,
                SolutionCode = jsonSchema.Code.AfterCode,
                UnitTestCode = jsonSchema.Code.UnitTests,
                ExtensionChallenges = jsonSchema.Extensions,
                CommonPitfalls = jsonSchema.Pedagogical.CommonPitfalls,
                Level = Enum.Parse<SkillLevel>(jsonSchema.Metadata.Level),
                Topic = Enum.Parse<TopicArea>(jsonSchema.Metadata.Topic),
                Type = Enum.Parse<ExerciseType>(jsonSchema.Metadata.Type),
                EstimatedMinutes = jsonSchema.Metadata.EstimatedMinutes
            };
        }

        /// <summary>
        /// Convierte Exercise a ExerciseJsonSchema para serialización
        /// </summary>
        public static ExerciseJsonSchema ToJsonSchema(this Exercise exercise)
        {
            return new ExerciseJsonSchema
            {
                Metadata = new ExerciseMetadata
                {
                    Id = GenerateId(exercise),
                    Title = exercise.Title,
                    Level = exercise.Level.ToString(),
                    Topic = exercise.Topic.ToString(),
                    Type = exercise.Type.ToString(),
                    EstimatedMinutes = exercise.EstimatedMinutes,
                    ComplexityScore = CalculateComplexityScore(exercise)
                },
                Content = new ExerciseContent
                {
                    Description = exercise.Description,
                    LearningObjectives = exercise.LearningObjectives,
                    Prerequisites = exercise.Prerequisites,
                    ProblemStatement = exercise.ProblemStatement,
                    TechnicalRequirements = exercise.TechnicalRequirements,
                    SuccessCriteria = exercise.SuccessCriteria
                },
                Code = new ExerciseCode
                {
                    StarterTemplate = exercise.StarterCode,
                    AfterCode = exercise.SolutionCode,
                    UnitTests = exercise.UnitTestCode
                },
                Extensions = exercise.ExtensionChallenges,
                Pedagogical = new PedagogicalInfo
                {
                    CommonPitfalls = exercise.CommonPitfalls
                }
            };
        }

        private static string GenerateId(Exercise exercise)
        {
            return $"{exercise.Topic.ToString().ToLowerInvariant()}-{exercise.Type.ToString().ToLowerInvariant()}-{exercise.Level.ToString().ToLowerInvariant()}";
        }

        private static int CalculateComplexityScore(Exercise exercise)
        {
            // Lógica simple para calcular complejidad basada en diversos factores
            int score = 1;
            
            // Factor por nivel
            score += exercise.Level switch
            {
                SkillLevel.Beginner => 1,
                SkillLevel.Intermediate => 3,
                SkillLevel.Advanced => 5,
                _ => 2
            };

            // Factor por tipo de ejercicio
            score += exercise.Type switch
            {
                ExerciseType.Implementation => 2,
                ExerciseType.Design => 3,
                ExerciseType.Performance => 3,
                ExerciseType.Refactoring => 1,
                ExerciseType.DebugFix => 1,
                ExerciseType.Extension => 2,
                ExerciseType.Testing => 2,
                _ => 1
            };

            // Factor por cantidad de objetivos de aprendizaje
            score += Math.Min(exercise.LearningObjectives.Count / 2, 2);

            return Math.Min(score, 10); // Máximo 10
        }
    }
}