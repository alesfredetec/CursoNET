using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CursoNET.ExerciseGenerator.Configuration
{
    /// <summary>
    /// Administrador de templates de prompts para generación de IA
    /// Permite cargar, personalizar y aplicar templates configurables
    /// </summary>
    public class PromptTemplateManager
    {
        private readonly string _configPath;
        private readonly JsonSerializerOptions _jsonOptions;
        private PromptTemplatesConfig? _config;
        private DateTime _lastLoaded;

        public PromptTemplateManager(string configPath = "jsondata/config")
        {
            _configPath = Path.GetFullPath(configPath);
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
        }

        /// <summary>
        /// Carga la configuración de templates desde archivo JSON
        /// </summary>
        public async Task LoadConfigurationAsync()
        {
            var configFile = Path.Combine(_configPath, "prompt-templates.json");
            
            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException($"Prompt templates configuration not found: {configFile}");
            }

            var json = await File.ReadAllTextAsync(configFile);
            _config = JsonSerializer.Deserialize<PromptTemplatesConfig>(json, _jsonOptions);
            _lastLoaded = DateTime.Now;

            if (_config == null)
            {
                throw new InvalidOperationException("Failed to load prompt templates configuration");
            }
        }

        /// <summary>
        /// Genera un prompt completo usando el template especificado
        /// </summary>
        public string GeneratePrompt(string templateId, PromptParameters parameters)
        {
            EnsureConfigurationLoaded();

            if (!_config!.PromptTemplates.ContainsKey(templateId))
            {
                // Fallback al template por defecto
                templateId = _config.PromptConfiguration.DefaultTemplate;
                
                if (!_config.PromptTemplates.ContainsKey(templateId))
                {
                    throw new ArgumentException($"Template not found: {templateId}");
                }
            }

            var template = _config.PromptTemplates[templateId];
            return ApplyTemplate(template, parameters);
        }

        /// <summary>
        /// Genera un prompt usando el template por defecto
        /// </summary>
        public string GeneratePrompt(PromptParameters parameters)
        {
            EnsureConfigurationLoaded();
            return GeneratePrompt(_config!.PromptConfiguration.DefaultTemplate, parameters);
        }

        /// <summary>
        /// Obtiene lista de templates disponibles
        /// </summary>
        public IEnumerable<string> GetAvailableTemplates()
        {
            EnsureConfigurationLoaded();
            return _config!.PromptTemplates.Keys;
        }

        /// <summary>
        /// Obtiene información detallada de un template
        /// </summary>
        public PromptTemplate? GetTemplate(string templateId)
        {
            EnsureConfigurationLoaded();
            _config!.PromptTemplates.TryGetValue(templateId, out var template);
            return template;
        }

        /// <summary>
        /// Valida que un template tenga todos los parámetros requeridos
        /// </summary>
        public PromptValidationResult ValidateTemplate(string templateId, PromptParameters parameters)
        {
            var template = GetTemplate(templateId);
            if (template == null)
            {
                return new PromptValidationResult(false, $"Template not found: {templateId}");
            }

            var missingParameters = new List<string>();
            var templateContent = template.Template.PromptStructure;

            // Buscar placeholders en el template (solo identificadores simples, no estructuras YAML/JSON)
            var placeholderPattern = @"\{([a-zA-Z][a-zA-Z0-9]*)\}";
            var matches = Regex.Matches(templateContent, placeholderPattern);

            foreach (Match match in matches)
            {
                var parameterName = match.Groups[1].Value;
                if (!HasParameter(parameters, parameterName))
                {
                    missingParameters.Add(parameterName);
                }
            }

            if (missingParameters.Count > 0)
            {
                return new PromptValidationResult(false, 
                    $"Missing parameters: {string.Join(", ", missingParameters)}");
            }

            return new PromptValidationResult(true, "All parameters provided");
        }

        private string ApplyTemplate(PromptTemplate template, PromptParameters parameters)
        {
            var promptStructure = template.Template.PromptStructure;
            
            // Reemplazar placeholders con valores reales
            var result = ReplacePlaceholders(promptStructure, parameters);
            
            // Validar que no queden placeholders sin reemplazar
            if (ContainsUnreplacedPlaceholders(result))
            {
                throw new InvalidOperationException(
                    "Template contains unreplaced placeholders. Check parameter mapping.");
            }

            return result;
        }

        private string ReplacePlaceholders(string template, PromptParameters parameters)
        {
            var result = template;
            
            // Mapeo de parámetros estándar
            var parameterMap = new Dictionary<string, string>
            {
                {"skillLevel", parameters.SkillLevel},
                {"topicArea", parameters.TopicArea},
                {"exerciseType", parameters.ExerciseType},
                {"context", parameters.Context},
                {"estimatedMinutes", parameters.EstimatedMinutes.ToString()},
                {"courseName", parameters.MentorConfiguration?.CourseName ?? ""},
                {"currentModule", parameters.TopicArea ?? ""}, // Use topicArea as module fallback
                {"teachingStyle", parameters.MentorConfiguration?.TeachingStyle ?? ""},
                {"preferredDomain", parameters.MentorConfiguration?.PreferredExampleDomain ?? ""},
                {"mentorConfiguration", FormatMentorConfiguration(parameters.MentorConfiguration)}
            };

            // Agregar parámetros personalizados
            if (parameters.CustomParameters != null)
            {
                foreach (var kvp in parameters.CustomParameters)
                {
                    parameterMap[kvp.Key] = kvp.Value;
                }
            }

            // Reemplazar todos los placeholders
            foreach (var kvp in parameterMap)
            {
                var placeholder = $"{{{kvp.Key}}}";
                result = result.Replace(placeholder, kvp.Value);
            }

            return result;
        }

        private string FormatMentorConfiguration(MentorConfiguration? config)
        {
            if (config == null) return "Standard configuration";

            return $"Mentor: {config.MentorName}, Style: {config.TeachingStyle}, " +
                   $"Domain: {config.PreferredExampleDomain}";
        }

        private bool ContainsUnreplacedPlaceholders(string text)
        {
            return Regex.IsMatch(text, @"\{[a-zA-Z][a-zA-Z0-9]*\}");
        }

        private bool HasParameter(PromptParameters parameters, string parameterName)
        {
            return parameterName.ToLower() switch
            {
                "skilllevel" => !string.IsNullOrEmpty(parameters.SkillLevel),
                "topicarea" => !string.IsNullOrEmpty(parameters.TopicArea),
                "exercisetype" => !string.IsNullOrEmpty(parameters.ExerciseType),
                "context" => !string.IsNullOrEmpty(parameters.Context),
                "estimatedminutes" => parameters.EstimatedMinutes > 0,
                "coursename" => !string.IsNullOrEmpty(parameters.MentorConfiguration?.CourseName),
                "currentmodule" => !string.IsNullOrEmpty(parameters.TopicArea), // Use topicArea as fallback
                "teachingstyle" => !string.IsNullOrEmpty(parameters.MentorConfiguration?.TeachingStyle),
                "preferreddomain" => !string.IsNullOrEmpty(parameters.MentorConfiguration?.PreferredExampleDomain),
                "mentorconfiguration" => parameters.MentorConfiguration != null,
                _ => parameters.CustomParameters?.ContainsKey(parameterName) ?? false
            };
        }

        private void EnsureConfigurationLoaded()
        {
            if (_config == null || (DateTime.Now - _lastLoaded).TotalMinutes > 30)
            {
                LoadConfigurationAsync().GetAwaiter().GetResult();
            }
        }
    }

    /// <summary>
    /// Parámetros para generar prompts
    /// </summary>
    public class PromptParameters
    {
        public string SkillLevel { get; set; } = "";
        public string TopicArea { get; set; } = "";
        public string ExerciseType { get; set; } = "";
        public string Context { get; set; } = "";
        public int EstimatedMinutes { get; set; }
        public MentorConfiguration? MentorConfiguration { get; set; }
        public Dictionary<string, string>? CustomParameters { get; set; }
    }

    /// <summary>
    /// Resultado de validación de template de prompt
    /// </summary>
    public class PromptValidationResult
    {
        public bool IsValid { get; }
        public string Message { get; }

        public PromptValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
    }

    #region Configuration Schema Classes

    public class PromptTemplatesConfig
    {
        public Dictionary<string, PromptTemplate> PromptTemplates { get; set; } = new();
        public PromptConfiguration PromptConfiguration { get; set; } = new();
    }

    public class PromptTemplate
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Version { get; set; } = "";
        public string Description { get; set; } = "";
        public TemplateStructure Template { get; set; } = new();
        public Dictionary<string, string> Parameters { get; set; } = new();
    }

    public class TemplateStructure
    {
        public string SystemPrompt { get; set; } = "";
        public string PromptStructure { get; set; } = "";
    }

    public class PromptConfiguration
    {
        public string DefaultTemplate { get; set; } = "";
        public int MaxTokens { get; set; }
        public double Temperature { get; set; }
        public string FallbackTemplate { get; set; } = "";
        public List<string> ValidationRules { get; set; } = new();
    }

    #endregion
}