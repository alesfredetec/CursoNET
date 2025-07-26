using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Configuración para tipos de ejercicios
    /// </summary>
    public class ExerciseTypeConfiguration
    {
        private readonly Dictionary<string, ExerciseTypeConfig> _exerciseTypes;

        public ExerciseTypeConfiguration(IEnumerable<ExerciseTypeConfig> exerciseTypes)
        {
            _exerciseTypes = exerciseTypes.ToDictionary(et => et.Id, et => et);
        }

        public ExerciseTypeConfig? GetById(string id) => _exerciseTypes.TryGetValue(id, out var type) ? type : null;

        public IEnumerable<ExerciseTypeConfig> GetAll() => _exerciseTypes.Values;

        public IEnumerable<ExerciseTypeConfig> GetForSkillLevel(string skillLevel)
        {
            return _exerciseTypes.Values.Where(et => et.SkillLevels.Contains(skillLevel, StringComparer.OrdinalIgnoreCase));
        }

        public IEnumerable<ExerciseTypeConfig> GetByDifficulty(int minDifficulty, int maxDifficulty)
        {
            return _exerciseTypes.Values.Where(et => et.Difficulty >= minDifficulty && et.Difficulty <= maxDifficulty);
        }

        public IEnumerable<ExerciseTypeConfig> GetOrderedByDifficulty()
        {
            return _exerciseTypes.Values.OrderBy(et => et.Difficulty);
        }

        public bool Exists(string id) => _exerciseTypes.ContainsKey(id);

        public int Count => _exerciseTypes.Count;
    }

    /// <summary>
    /// Configuración individual de un tipo de ejercicio
    /// </summary>
    public class ExerciseTypeConfig
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("pathName")]
        public required string PathName { get; set; }

        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }

        [JsonPropertyName("timeMultiplier")]
        public double TimeMultiplier { get; set; } = 1.0;

        [JsonPropertyName("skillLevels")]
        public List<string> SkillLevels { get; set; } = new();

        [JsonPropertyName("characteristics")]
        public List<string> Characteristics { get; set; } = new();

        [JsonPropertyName("learningObjectives")]
        public List<string> LearningObjectives { get; set; } = new();

        /// <summary>
        /// Verifica si este tipo de ejercicio está disponible para el skill level especificado
        /// </summary>
        public bool IsAvailableForLevel(string skillLevel)
        {
            return SkillLevels.Contains(skillLevel, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Calcula el tiempo estimado basado en el tiempo base y el multiplicador
        /// </summary>
        public int CalculateEstimatedTime(int baseTimeMinutes)
        {
            return (int)Math.Round(baseTimeMinutes * TimeMultiplier);
        }

        /// <summary>
        /// Obtiene una descripción completa incluyendo características
        /// </summary>
        public string GetFullDescription()
        {
            var description = Description;
            if (Characteristics.Any())
            {
                description += "\n\nCharacteristics:\n" + string.Join("\n", Characteristics.Select(c => $"• {c}"));
            }
            return description;
        }
    }

    /// <summary>
    /// Schema JSON para deserialización de exercise types
    /// </summary>
    public class ExerciseTypesJsonSchema
    {
        [JsonPropertyName("exerciseTypes")]
        public List<ExerciseTypeConfig> ExerciseTypes { get; set; } = new();

        [JsonPropertyName("metadata")]
        public ConfigMetadata Metadata { get; set; } = new();
    }
}