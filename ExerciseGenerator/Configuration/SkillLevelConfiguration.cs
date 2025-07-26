using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Configuración para skill levels (niveles de habilidad)
    /// </summary>
    public class SkillLevelConfiguration
    {
        private readonly Dictionary<string, SkillLevelConfig> _skillLevels;
        private readonly List<SkillLevelConfig> _orderedLevels;

        public SkillLevelConfiguration(IEnumerable<SkillLevelConfig> skillLevels)
        {
            _skillLevels = skillLevels.ToDictionary(sl => sl.Id, sl => sl);
            _orderedLevels = skillLevels.OrderBy(sl => sl.Order).ToList();
        }

        public SkillLevelConfig? GetById(string id) => _skillLevels.TryGetValue(id, out var level) ? level : null;

        public IEnumerable<SkillLevelConfig> GetAll() => _skillLevels.Values;

        public IEnumerable<SkillLevelConfig> GetOrderedByProgression() => _orderedLevels;

        public SkillLevelConfig? GetNextLevel(string currentLevelId)
        {
            var currentIndex = _orderedLevels.FindIndex(sl => sl.Id.Equals(currentLevelId, StringComparison.OrdinalIgnoreCase));
            if (currentIndex >= 0 && currentIndex < _orderedLevels.Count - 1)
            {
                return _orderedLevels[currentIndex + 1];
            }
            return null;
        }

        public SkillLevelConfig? GetPreviousLevel(string currentLevelId)
        {
            var currentIndex = _orderedLevels.FindIndex(sl => sl.Id.Equals(currentLevelId, StringComparison.OrdinalIgnoreCase));
            if (currentIndex > 0)
            {
                return _orderedLevels[currentIndex - 1];
            }
            return null;
        }

        public bool IsValidProgression(string fromLevel, string toLevel)
        {
            var fromIndex = _orderedLevels.FindIndex(sl => sl.Id.Equals(fromLevel, StringComparison.OrdinalIgnoreCase));
            var toIndex = _orderedLevels.FindIndex(sl => sl.Id.Equals(toLevel, StringComparison.OrdinalIgnoreCase));
            
            return fromIndex >= 0 && toIndex >= 0 && toIndex == fromIndex + 1;
        }

        public bool Exists(string id) => _skillLevels.ContainsKey(id);

        public int Count => _skillLevels.Count;
    }

    /// <summary>
    /// Configuración individual de un skill level
    /// </summary>
    public class SkillLevelConfig
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("pathName")]
        public required string PathName { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("characteristics")]
        public List<string> Characteristics { get; set; } = new();

        [JsonPropertyName("estimatedExperienceHours")]
        public string EstimatedExperienceHours { get; set; } = "";

        [JsonPropertyName("prerequisites")]
        public List<string> Prerequisites { get; set; } = new();

        [JsonPropertyName("averageExerciseMinutes")]
        public int AverageExerciseMinutes { get; set; }

        [JsonPropertyName("supportLevel")]
        public required string SupportLevel { get; set; }

        [JsonPropertyName("complexityRange")]
        public required string ComplexityRange { get; set; }

        /// <summary>
        /// Obtiene el rango de complejidad como tupla (min, max)
        /// </summary>
        public (int Min, int Max) GetComplexityRange()
        {
            if (string.IsNullOrEmpty(ComplexityRange) || !ComplexityRange.Contains('-'))
                return (1, 5);

            var parts = ComplexityRange.Split('-');
            if (parts.Length == 2 && 
                int.TryParse(parts[0], out int min) && 
                int.TryParse(parts[1], out int max))
            {
                return (min, max);
            }

            return (1, 5);
        }

        /// <summary>
        /// Verifica si el nivel de complejidad está dentro del rango apropiado
        /// </summary>
        public bool IsComplexityAppropriate(int complexity)
        {
            var (min, max) = GetComplexityRange();
            return complexity >= min && complexity <= max;
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
    /// Schema JSON para deserialización de skill levels
    /// </summary>
    public class SkillLevelsJsonSchema
    {
        [JsonPropertyName("skillLevels")]
        public List<SkillLevelConfig> SkillLevels { get; set; } = new();

        [JsonPropertyName("progressionRules")]
        public ProgressionRules ProgressionRules { get; set; } = new();

        [JsonPropertyName("metadata")]
        public ConfigMetadata Metadata { get; set; } = new();
    }

    /// <summary>
    /// Reglas de progresión entre skill levels
    /// </summary>
    public class ProgressionRules
    {
        [JsonPropertyName("minimumCompletionRate")]
        public double MinimumCompletionRate { get; set; }

        [JsonPropertyName("recommendedPracticeExercises")]
        public int RecommendedPracticeExercises { get; set; }

        [JsonPropertyName("skillAssessmentRequired")]
        public bool SkillAssessmentRequired { get; set; }

        [JsonPropertyName("mentorReviewRequired")]
        public Dictionary<string, bool> MentorReviewRequired { get; set; } = new();
    }
}