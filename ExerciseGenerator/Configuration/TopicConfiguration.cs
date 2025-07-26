using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Configuración para topics (temas) de ejercicios
    /// </summary>
    public class TopicConfiguration
    {
        private readonly Dictionary<string, TopicConfig> _topics;

        public TopicConfiguration(IEnumerable<TopicConfig> topics)
        {
            _topics = topics.ToDictionary(t => t.Id, t => t);
        }

        public TopicConfig? GetById(string id) => _topics.TryGetValue(id, out var topic) ? topic : null;

        public IEnumerable<TopicConfig> GetAll() => _topics.Values;

        public IEnumerable<TopicConfig> GetForSkillLevel(string skillLevel)
        {
            return _topics.Values.Where(t => t.SkillLevels.Contains(skillLevel, StringComparer.OrdinalIgnoreCase));
        }

        public IEnumerable<TopicConfig> GetByCategory(string category)
        {
            return _topics.Values.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<TopicConfig> GetByDifficulty(int minDifficulty, int maxDifficulty)
        {
            return _topics.Values.Where(t => t.Difficulty >= minDifficulty && t.Difficulty <= maxDifficulty);
        }

        public bool Exists(string id) => _topics.ContainsKey(id);

        public int Count => _topics.Count;
    }

    /// <summary>
    /// Configuración individual de un topic
    /// </summary>
    public class TopicConfig
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("pathName")]
        public required string PathName { get; set; }

        [JsonPropertyName("skillLevels")]
        public List<string> SkillLevels { get; set; } = new();

        [JsonPropertyName("prerequisites")]
        public List<string> Prerequisites { get; set; } = new();

        [JsonPropertyName("estimatedHours")]
        public int EstimatedHours { get; set; }

        [JsonPropertyName("category")]
        public required string Category { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();

        [JsonPropertyName("difficulty")]
        public int Difficulty { get; set; }

        /// <summary>
        /// Verifica si este topic está disponible para el skill level especificado
        /// </summary>
        public bool IsAvailableForLevel(string skillLevel)
        {
            return SkillLevels.Contains(skillLevel, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Verifica si este topic tiene un tag específico
        /// </summary>
        public bool HasTag(string tag)
        {
            return Tags.Contains(tag, StringComparer.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Schema JSON para deserialización de topics
    /// </summary>
    public class TopicsJsonSchema
    {
        [JsonPropertyName("topics")]
        public List<TopicConfig> Topics { get; set; } = new();

        [JsonPropertyName("metadata")]
        public ConfigMetadata Metadata { get; set; } = new();
    }

    /// <summary>
    /// Metadatos de configuración
    /// </summary>
    public class ConfigMetadata
    {
        [JsonPropertyName("version")]
        public string Version { get; set; } = "";

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";
    }
}