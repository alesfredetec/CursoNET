using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Configuración para dependencias entre topics y exercise types
    /// </summary>
    public class DependencyConfiguration
    {
        private readonly DependenciesJsonSchema _data;

        public DependencyConfiguration(DependenciesJsonSchema data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Verifica si un topic tiene todos sus prerequisitos satisfechos
        /// </summary>
        public bool ArePrerequisitesSatisfied(string topicId, IEnumerable<string> completedTopics)
        {
            if (!_data.TopicDependencies.TryGetValue(topicId, out var dependency))
                return true; // Si no hay dependencias definidas, se considera satisfecho

            var completedSet = new HashSet<string>(completedTopics, StringComparer.OrdinalIgnoreCase);
            return dependency.Requires.All(req => completedSet.Contains(req));
        }

        /// <summary>
        /// Obtiene las dependencias de un topic
        /// </summary>
        public TopicDependency? GetTopicDependencies(string topicId)
        {
            return _data.TopicDependencies.TryGetValue(topicId, out var dependency) ? dependency : null;
        }

        /// <summary>
        /// Obtiene las dependencias de un exercise type
        /// </summary>
        public ExerciseTypeDependency? GetExerciseTypeDependencies(string exerciseTypeId)
        {
            return _data.ExerciseTypeDependencies.TryGetValue(exerciseTypeId, out var dependency) ? dependency : null;
        }

        /// <summary>
        /// Obtiene todos los topics que deben completarse antes de un topic específico
        /// </summary>
        public IEnumerable<string> GetAllPrerequisites(string topicId)
        {
            var prerequisites = new HashSet<string>();
            CollectAllPrerequisites(topicId, prerequisites, new HashSet<string>());
            return prerequisites;
        }

        /// <summary>
        /// Verifica si existe una dependencia circular
        /// </summary>
        public bool HasCircularDependency(string topicId)
        {
            var visited = new HashSet<string>();
            var recursionStack = new HashSet<string>();
            return HasCircularDependencyRecursive(topicId, visited, recursionStack);
        }

        /// <summary>
        /// Obtiene los topics recomendados para un exercise type
        /// </summary>
        public IEnumerable<string> GetRecommendedTopicsForExerciseType(string exerciseTypeId)
        {
            if (_data.ExerciseTypeDependencies.TryGetValue(exerciseTypeId, out var dependency))
            {
                return dependency.RecommendedTopics;
            }
            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Verifica si un exercise type es apropiado para un skill level
        /// </summary>
        public bool IsExerciseTypeAppropriateForLevel(string exerciseTypeId, string skillLevel)
        {
            if (!_data.ExerciseTypeDependencies.TryGetValue(exerciseTypeId, out var dependency))
                return true;

            var skillLevelOrder = GetSkillLevelOrder(skillLevel);
            var minimumLevelOrder = GetSkillLevelOrder(dependency.MinimumLevel);

            return skillLevelOrder >= minimumLevelOrder;
        }

        /// <summary>
        /// Obtiene la progresión de skill level
        /// </summary>
        public SkillLevelProgression? GetSkillLevelProgression(string skillLevel)
        {
            return _data.SkillLevelProgression.TryGetValue(skillLevel, out var progression) ? progression : null;
        }

        /// <summary>
        /// Valida todas las dependencias del sistema
        /// </summary>
        public IEnumerable<DependencyValidationResult> ValidateAllDependencies()
        {
            var results = new List<DependencyValidationResult>();

            // Validar dependencias circulares en topics
            foreach (var topicId in _data.TopicDependencies.Keys)
            {
                if (HasCircularDependency(topicId))
                {
                    results.Add(new DependencyValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"Circular dependency detected for topic: {topicId}",
                        ComponentType = "Topic",
                        ComponentId = topicId
                    });
                }
            }

            // Validar que todos los prerequisitos existen
            foreach (var (topicId, dependency) in _data.TopicDependencies)
            {
                foreach (var prerequisite in dependency.Requires)
                {
                    if (!_data.TopicDependencies.ContainsKey(prerequisite))
                    {
                        results.Add(new DependencyValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = $"Topic '{topicId}' requires '{prerequisite}' which doesn't exist",
                            ComponentType = "Topic",
                            ComponentId = topicId
                        });
                    }
                }
            }

            return results;
        }

        private void CollectAllPrerequisites(string topicId, HashSet<string> prerequisites, HashSet<string> visited)
        {
            if (visited.Contains(topicId) || !_data.TopicDependencies.TryGetValue(topicId, out var dependency))
                return;

            visited.Add(topicId);

            foreach (var prerequisite in dependency.Requires)
            {
                prerequisites.Add(prerequisite);
                CollectAllPrerequisites(prerequisite, prerequisites, visited);
            }
        }

        private bool HasCircularDependencyRecursive(string topicId, HashSet<string> visited, HashSet<string> recursionStack)
        {
            if (recursionStack.Contains(topicId))
                return true;

            if (visited.Contains(topicId))
                return false;

            visited.Add(topicId);
            recursionStack.Add(topicId);

            if (_data.TopicDependencies.TryGetValue(topicId, out var dependency))
            {
                foreach (var prerequisite in dependency.Requires)
                {
                    if (HasCircularDependencyRecursive(prerequisite, visited, recursionStack))
                        return true;
                }
            }

            recursionStack.Remove(topicId);
            return false;
        }

        private int GetSkillLevelOrder(string skillLevel)
        {
            return skillLevel.ToUpperInvariant() switch
            {
                "BEGINNER" => 1,
                "INTERMEDIATE" => 2,
                "ADVANCED" => 3,
                _ => 0
            };
        }
    }

    /// <summary>
    /// Schema JSON para deserialización de dependencies
    /// </summary>
    public class DependenciesJsonSchema
    {
        [JsonPropertyName("topicDependencies")]
        public Dictionary<string, TopicDependency> TopicDependencies { get; set; } = new();

        [JsonPropertyName("exerciseTypeDependencies")]
        public Dictionary<string, ExerciseTypeDependency> ExerciseTypeDependencies { get; set; } = new();

        [JsonPropertyName("skillLevelProgression")]
        public Dictionary<string, SkillLevelProgression> SkillLevelProgression { get; set; } = new();

        [JsonPropertyName("validationRules")]
        public ValidationRules ValidationRules { get; set; } = new();

        [JsonPropertyName("metadata")]
        public ConfigMetadata Metadata { get; set; } = new();
    }

    /// <summary>
    /// Dependencia de un topic
    /// </summary>
    public class TopicDependency
    {
        [JsonPropertyName("requires")]
        public List<string> Requires { get; set; } = new();

        [JsonPropertyName("recommends")]
        public List<string> Recommends { get; set; } = new();

        [JsonPropertyName("description")]
        public required string Description { get; set; }
    }

    /// <summary>
    /// Dependencia de un exercise type
    /// </summary>
    public class ExerciseTypeDependency
    {
        [JsonPropertyName("minimumLevel")]
        public required string MinimumLevel { get; set; }

        [JsonPropertyName("recommendedTopics")]
        public List<string> RecommendedTopics { get; set; } = new();

        [JsonPropertyName("description")]
        public required string Description { get; set; }
    }

    /// <summary>
    /// Progresión de skill level
    /// </summary>
    public class SkillLevelProgression
    {
        [JsonPropertyName("nextLevel")]
        public string? NextLevel { get; set; }

        [JsonPropertyName("requiredTopics")]
        public List<string> RequiredTopics { get; set; } = new();

        [JsonPropertyName("minimumExercisesCompleted")]
        public int MinimumExercisesCompleted { get; set; }

        [JsonPropertyName("recommendedExerciseTypes")]
        public List<string> RecommendedExerciseTypes { get; set; } = new();
    }

    /// <summary>
    /// Reglas de validación
    /// </summary>
    public class ValidationRules
    {
        [JsonPropertyName("topicValidation")]
        public TopicValidationRules TopicValidation { get; set; } = new();

        [JsonPropertyName("exerciseTypeValidation")]
        public ExerciseTypeValidationRules ExerciseTypeValidation { get; set; } = new();
    }

    public class TopicValidationRules
    {
        [JsonPropertyName("maxDependencyDepth")]
        public int MaxDependencyDepth { get; set; }

        [JsonPropertyName("circularDependencyCheck")]
        public bool CircularDependencyCheck { get; set; }

        [JsonPropertyName("requiredFields")]
        public List<string> RequiredFields { get; set; } = new();
    }

    public class ExerciseTypeValidationRules
    {
        [JsonPropertyName("validSkillLevels")]
        public List<string> ValidSkillLevels { get; set; } = new();

        [JsonPropertyName("requiredFields")]
        public List<string> RequiredFields { get; set; } = new();
    }

    /// <summary>
    /// Resultado de validación de dependencias
    /// </summary>
    public class DependencyValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; } = "";
        public string ComponentType { get; set; } = "";
        public string ComponentId { get; set; } = "";
    }
}