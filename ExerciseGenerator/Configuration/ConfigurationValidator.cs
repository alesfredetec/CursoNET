using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Validador para verificar la consistencia de las configuraciones
    /// </summary>
    public class ConfigurationValidator
    {
        private readonly ConfigurationManager _configManager;

        public ConfigurationValidator(ConfigurationManager configManager)
        {
            _configManager = configManager ?? throw new ArgumentNullException(nameof(configManager));
        }

        /// <summary>
        /// Valida todas las configuraciones y retorna los resultados
        /// </summary>
        public IEnumerable<ValidationResult> ValidateAll()
        {
            var results = new List<ValidationResult>();

            results.AddRange(ValidateTopics());
            results.AddRange(ValidateExerciseTypes());
            results.AddRange(ValidateSkillLevels());
            results.AddRange(ValidateDependencies());
            results.AddRange(ValidateCrossReferences());

            return results;
        }

        /// <summary>
        /// Valida la configuración de topics
        /// </summary>
        public IEnumerable<ValidationResult> ValidateTopics()
        {
            var results = new List<ValidationResult>();
            var topics = _configManager.Topics.GetAll().ToList();

            // Validar que no hay IDs duplicados
            var duplicateIds = topics.GroupBy(t => t.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            foreach (var duplicateId in duplicateIds)
            {
                results.Add(new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = $"Duplicate topic ID found: {duplicateId}",
                    ComponentType = "Topic",
                    ComponentId = duplicateId
                });
            }

            // Validar que todos los topics tienen campos requeridos
            foreach (var topic in topics)
            {
                if (string.IsNullOrWhiteSpace(topic.Id))
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Topic has empty or null ID",
                        ComponentType = "Topic",
                        ComponentId = topic.Id
                    });
                }

                if (string.IsNullOrWhiteSpace(topic.DisplayName))
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"Topic '{topic.Id}' has empty or null DisplayName",
                        ComponentType = "Topic",
                        ComponentId = topic.Id
                    });
                }

                if (string.IsNullOrWhiteSpace(topic.PathName))
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"Topic '{topic.Id}' has empty or null PathName",
                        ComponentType = "Topic",
                        ComponentId = topic.Id
                    });
                }

                if (!topic.SkillLevels.Any())
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"Topic '{topic.Id}' has no skill levels defined",
                        ComponentType = "Topic",
                        ComponentId = topic.Id
                    });
                }

                // Validar que los skill levels referenciados existen
                foreach (var skillLevel in topic.SkillLevels)
                {
                    if (!_configManager.SkillLevels.Exists(skillLevel))
                    {
                        results.Add(new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = $"Topic '{topic.Id}' references non-existent skill level: {skillLevel}",
                            ComponentType = "Topic",
                            ComponentId = topic.Id
                        });
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Valida la configuración de exercise types
        /// </summary>
        public IEnumerable<ValidationResult> ValidateExerciseTypes()
        {
            var results = new List<ValidationResult>();
            var exerciseTypes = _configManager.ExerciseTypes.GetAll().ToList();

            // Validar que no hay IDs duplicados
            var duplicateIds = exerciseTypes.GroupBy(et => et.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            foreach (var duplicateId in duplicateIds)
            {
                results.Add(new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = $"Duplicate exercise type ID found: {duplicateId}",
                    ComponentType = "ExerciseType",
                    ComponentId = duplicateId
                });
            }

            // Validar campos requeridos
            foreach (var exerciseType in exerciseTypes)
            {
                if (string.IsNullOrWhiteSpace(exerciseType.Id))
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Exercise type has empty or null ID",
                        ComponentType = "ExerciseType",
                        ComponentId = exerciseType.Id
                    });
                }

                if (exerciseType.TimeMultiplier <= 0)
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"Exercise type '{exerciseType.Id}' has invalid time multiplier: {exerciseType.TimeMultiplier}",
                        ComponentType = "ExerciseType",
                        ComponentId = exerciseType.Id
                    });
                }

                // Validar que los skill levels referenciados existen
                foreach (var skillLevel in exerciseType.SkillLevels)
                {
                    if (!_configManager.SkillLevels.Exists(skillLevel))
                    {
                        results.Add(new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = $"Exercise type '{exerciseType.Id}' references non-existent skill level: {skillLevel}",
                            ComponentType = "ExerciseType",
                            ComponentId = exerciseType.Id
                        });
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Valida la configuración de skill levels
        /// </summary>
        public IEnumerable<ValidationResult> ValidateSkillLevels()
        {
            var results = new List<ValidationResult>();
            var skillLevels = _configManager.SkillLevels.GetAll().ToList();

            // Validar orden secuencial
            var orderedLevels = skillLevels.OrderBy(sl => sl.Order).ToList();
            for (int i = 0; i < orderedLevels.Count; i++)
            {
                if (orderedLevels[i].Order != i + 1)
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"Skill level order is not sequential. Expected {i + 1}, found {orderedLevels[i].Order} for '{orderedLevels[i].Id}'",
                        ComponentType = "SkillLevel",
                        ComponentId = orderedLevels[i].Id
                    });
                }
            }

            // Validar campos requeridos
            foreach (var skillLevel in skillLevels)
            {
                if (string.IsNullOrWhiteSpace(skillLevel.Id))
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Skill level has empty or null ID",
                        ComponentType = "SkillLevel",
                        ComponentId = skillLevel.Id
                    });
                }

                if (skillLevel.AverageExerciseMinutes <= 0)
                {
                    results.Add(new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = $"Skill level '{skillLevel.Id}' has invalid average exercise minutes: {skillLevel.AverageExerciseMinutes}",
                        ComponentType = "SkillLevel",
                        ComponentId = skillLevel.Id
                    });
                }
            }

            return results;
        }

        /// <summary>
        /// Valida la configuración de dependencias
        /// </summary>
        public IEnumerable<ValidationResult> ValidateDependencies()
        {
            var results = new List<ValidationResult>();

            // Usar el validador de dependencias interno
            var dependencyValidationResults = _configManager.Dependencies.ValidateAllDependencies();
            
            foreach (var depResult in dependencyValidationResults)
            {
                results.Add(new ValidationResult
                {
                    IsValid = depResult.IsValid,
                    ErrorMessage = depResult.ErrorMessage,
                    ComponentType = depResult.ComponentType,
                    ComponentId = depResult.ComponentId
                });
            }

            return results;
        }

        /// <summary>
        /// Valida referencias cruzadas entre configuraciones
        /// </summary>
        public IEnumerable<ValidationResult> ValidateCrossReferences()
        {
            var results = new List<ValidationResult>();

            // Validar que todos los topics en dependencies existen
            var allTopicIds = _configManager.Topics.GetAll().Select(t => t.Id).ToHashSet();
            
            foreach (var topic in _configManager.Topics.GetAll())
            {
                foreach (var prerequisite in topic.Prerequisites)
                {
                    if (!allTopicIds.Contains(prerequisite))
                    {
                        results.Add(new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = $"Topic '{topic.Id}' has prerequisite '{prerequisite}' that doesn't exist",
                            ComponentType = "Topic",
                            ComponentId = topic.Id
                        });
                    }
                }
            }

            return results;
        }
    }

    /// <summary>
    /// Resultado de validación
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; set; } = true;
        public string ErrorMessage { get; set; } = "";
        public string ComponentType { get; set; } = "";
        public string ComponentId { get; set; } = "";
        public string? AdditionalInfo { get; set; }
    }
}