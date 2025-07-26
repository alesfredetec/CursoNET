using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoNET.ExerciseGenerator.Configuration;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Generador IA interactivo que permite seleccionar templates especializados
    /// y generar prompts personalizados para diferentes audiencias
    /// </summary>
    public class InteractiveAIGenerator
    {
        public static async Task RunInteractiveGeneratorAsync()
        {
            Console.WriteLine("🤖 GENERADOR IA INTERACTIVO");
            Console.WriteLine("=" + new string('=', 50));
            Console.WriteLine();

            try
            {
                var templateManager = new PromptTemplateManager();
                await templateManager.LoadConfigurationAsync();

                // Mostrar templates especializados disponibles
                await ShowAvailableTemplates(templateManager);
                
                // Permitir selección de template
                var selectedTemplate = await SelectTemplate(templateManager);
                if (selectedTemplate == null) return;

                // Recopilar parámetros según audiencia
                var parameters = await CollectParameters(selectedTemplate);
                if (parameters == null) return;

                // Generar y mostrar el prompt
                await GenerateAndDisplayPrompt(templateManager, selectedTemplate, parameters);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }

        private static async Task ShowAvailableTemplates(PromptTemplateManager templateManager)
        {
            Console.WriteLine("📋 TEMPLATES DE IA DISPONIBLES:");
            Console.WriteLine();

            var templates = templateManager.GetAvailableTemplates().ToList();
            
            for (int i = 0; i < templates.Count; i++)
            {
                var template = templateManager.GetTemplate(templates[i]);
                if (template != null)
                {
                    var audienceType = GetAudienceType(templates[i]);
                    Console.WriteLine($"{i + 1}. {GetTemplateIcon(templates[i])} {template.Name}");
                    Console.WriteLine($"   🎯 Audiencia: {audienceType}");
                    Console.WriteLine($"   📝 {template.Description}");
                    Console.WriteLine($"   🔧 Parámetros: {template.Parameters.Count}");
                    Console.WriteLine();
                }
            }
        }

        private static string GetTemplateIcon(string templateId)
        {
            return templateId switch
            {
                "basicExerciseGeneration" => "🎓",
                "advancedExerciseGeneration" => "🚀", 
                "contextEngineeringExpert" => "🧠",
                "customPromptGeneration" => "🎨",
                "studentFocused" => "📚",
                "seniorDeveloper" => "👨‍💻",
                _ => "🤖"
            };
        }

        private static string GetAudienceType(string templateId)
        {
            return templateId switch
            {
                "basicExerciseGeneration" => "Principiantes - Ejercicios básicos",
                "advancedExerciseGeneration" => "Intermedio/Avanzado - Context Engineering",
                "contextEngineeringExpert" => "Expertos - Máxima sofisticación IA",
                "customPromptGeneration" => "Personalizable - Cualquier audiencia",
                "studentFocused" => "Estudiantes - Pedagógico y explicativo",
                "seniorDeveloper" => "Desarrolladores Senior - Técnico y directo",
                _ => "General"
            };
        }

        private static async Task<string?> SelectTemplate(PromptTemplateManager templateManager)
        {
            var templates = templateManager.GetAvailableTemplates().ToList();
            
            Console.WriteLine("🎯 Seleccione un template (ingrese el número):");
            Console.Write("Opción: ");
            
            var input = Console.ReadLine();
            if (int.TryParse(input, out int selection) && selection >= 1 && selection <= templates.Count)
            {
                var selectedTemplate = templates[selection - 1];
                Console.WriteLine($"✅ Template seleccionado: {selectedTemplate}");
                Console.WriteLine();
                return selectedTemplate;
            }

            Console.WriteLine("❌ Selección inválida.");
            return null;
        }

        private static async Task<PromptParameters?> CollectParameters(string templateId)
        {
            Console.WriteLine($"📊 Configurando parámetros para: {templateId}");
            Console.WriteLine("=" + new string('-', 40));
            Console.WriteLine();

            var parameters = new PromptParameters();

            // Parámetros básicos requeridos por todos los templates
            Console.Write("🎯 Skill Level (Beginner/Intermediate/Advanced): ");
            parameters.SkillLevel = Console.ReadLine() ?? "Intermediate";

            Console.Write("📚 Topic Area (CSharpFundamentals/LINQ/DesignPatterns/etc): ");
            parameters.TopicArea = Console.ReadLine() ?? "CSharpFundamentals";

            Console.Write("🔧 Exercise Type (Implementation/Refactoring/Debug/etc): ");
            parameters.ExerciseType = Console.ReadLine() ?? "Implementation";

            Console.Write("🏢 Business Context (E-commerce/Banking/Gaming/etc): ");
            parameters.Context = Console.ReadLine() ?? "E-commerce";

            Console.Write("⏰ Estimated Minutes (15-120): ");
            if (int.TryParse(Console.ReadLine(), out int minutes))
                parameters.EstimatedMinutes = minutes;
            else
                parameters.EstimatedMinutes = 45;

            // Configuración de mentor si es necesario para el template
            if (RequiresMentorConfiguration(templateId))
            {
                Console.WriteLine();
                Console.WriteLine("👨‍🏫 CONFIGURACIÓN DE MENTOR:");
                
                var mentorConfig = new MentorConfiguration();
                
                Console.Write("📚 Course Name: ");
                mentorConfig.CourseName = Console.ReadLine() ?? "Curso .NET Avanzado";

                Console.Write("🎨 Teaching Style (Practical/Theoretical/Interactive): ");
                mentorConfig.TeachingStyle = Console.ReadLine() ?? "Practical";

                Console.Write("👤 Mentor Name: ");
                mentorConfig.MentorName = Console.ReadLine() ?? "Instructor Senior";

                Console.Write("🎯 Preferred Domain (Enterprise/Web/Mobile/etc): ");
                mentorConfig.PreferredExampleDomain = Console.ReadLine() ?? "Enterprise";

                parameters.MentorConfiguration = mentorConfig;
            }

            // Parámetros personalizados para template custom
            if (templateId == "customPromptGeneration")
            {
                Console.WriteLine();
                Console.WriteLine("🎨 PARÁMETROS PERSONALIZADOS:");
                
                parameters.CustomParameters = new Dictionary<string, string>();
                
                Console.Write("🤖 Custom System Prompt: ");
                var systemPrompt = Console.ReadLine() ?? "You are an expert .NET developer and instructor.";
                parameters.CustomParameters["customSystemPrompt"] = systemPrompt;

                Console.Write("📝 Custom Prompt Structure: ");
                var promptStructure = Console.ReadLine() ?? $"Create a {parameters.ExerciseType} exercise for {parameters.SkillLevel} developers.";
                parameters.CustomParameters["customPromptStructure"] = promptStructure;
            }

            Console.WriteLine();
            Console.WriteLine("✅ Parámetros configurados correctamente!");
            Console.WriteLine();

            return parameters;
        }

        private static bool RequiresMentorConfiguration(string templateId)
        {
            return templateId switch
            {
                "advancedExerciseGeneration" => true,
                "contextEngineeringExpert" => true,
                "studentFocused" => true,
                "seniorDeveloper" => true,
                _ => false
            };
        }

        private static async Task GenerateAndDisplayPrompt(
            PromptTemplateManager templateManager, 
            string templateId, 
            PromptParameters parameters)
        {
            Console.WriteLine("🔄 Generando prompt con IA...");
            Console.WriteLine();

            try
            {
                // Validar template
                var validation = templateManager.ValidateTemplate(templateId, parameters);
                if (!validation.IsValid)
                {
                    Console.WriteLine($"❌ Error de validación: {validation.Message}");
                    return;
                }

                // Generar prompt
                var prompt = templateManager.GeneratePrompt(templateId, parameters);
                
                Console.WriteLine("✅ PROMPT GENERADO EXITOSAMENTE");
                Console.WriteLine("=" + new string('=', 50));
                Console.WriteLine();
                
                Console.WriteLine($"📊 Estadísticas:");
                Console.WriteLine($"   • Longitud: {prompt.Length:N0} caracteres");
                Console.WriteLine($"   • Template: {templateId}");
                Console.WriteLine($"   • Audiencia: {GetAudienceType(templateId)}");
                Console.WriteLine();

                // Mostrar preview del prompt
                Console.WriteLine("📖 PREVIEW DEL PROMPT:");
                Console.WriteLine("=" + new string('-', 30));
                var preview = prompt.Length > 1000 ? prompt.Substring(0, 1000) + "\n\n[... continúa por " + (prompt.Length - 1000) + " caracteres más ...]" : prompt;
                Console.WriteLine(preview);
                Console.WriteLine();

                // Ofrecer guardar el prompt
                Console.Write("💾 ¿Desea guardar este prompt en un archivo? (s/n): ");
                var saveChoice = Console.ReadLine()?.ToLower();
                
                if (saveChoice == "s" || saveChoice == "si" || saveChoice == "yes" || saveChoice == "y")
                {
                    await SavePromptToFile(prompt, templateId, parameters);
                }

                Console.WriteLine();
                Console.WriteLine("🎉 ¡Proceso completado exitosamente!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generando prompt: {ex.Message}");
            }
        }

        private static async Task SavePromptToFile(string prompt, string templateId, PromptParameters parameters)
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                var filename = $"ai-prompt-{templateId}-{parameters.SkillLevel}-{parameters.TopicArea}-{timestamp}.txt";
                var filepath = System.IO.Path.Combine("generated-exercises", filename);

                // Crear directorio si no existe
                var directory = System.IO.Path.GetDirectoryName(filepath);
                if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                // Crear header con metadatos
                var header = $"# PROMPT GENERADO AUTOMÁTICAMENTE\n" +
                           $"# Template: {templateId}\n" +
                           $"# Skill Level: {parameters.SkillLevel}\n" +
                           $"# Topic: {parameters.TopicArea}\n" +
                           $"# Exercise Type: {parameters.ExerciseType}\n" +
                           $"# Context: {parameters.Context}\n" +
                           $"# Duration: {parameters.EstimatedMinutes} minutes\n" +
                           $"# Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                           $"# Length: {prompt.Length:N0} characters\n\n" +
                           $"---\n\n{prompt}";

                await System.IO.File.WriteAllTextAsync(filepath, header);
                
                Console.WriteLine($"💾 Prompt guardado en: {filepath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error guardando archivo: {ex.Message}");
            }
        }
    }
}