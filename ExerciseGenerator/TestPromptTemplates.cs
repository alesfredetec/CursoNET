using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CursoNET.ExerciseGenerator.Configuration;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Demostración del sistema de templates configurables de prompts
    /// </summary>
    public class TestPromptTemplates
    {
        public static async Task RunPromptTemplateTestAsync()
        {
            Console.WriteLine("🎯 TESTING PROMPT TEMPLATES CONFIGURABLES");
            Console.WriteLine("=" + new string('=', 60));
            Console.WriteLine();

            try
            {
                await TestBasicTemplateUsage();
                Console.WriteLine();
                
                await TestAdvancedTemplateUsage();
                Console.WriteLine();
                
                await TestCustomTemplateUsage();
                Console.WriteLine();
                
                await TestTemplateValidation();
                Console.WriteLine();
                
                Console.WriteLine("✅ TODOS LOS TESTS DE PROMPT TEMPLATES COMPLETADOS!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR EN TESTS: {ex.Message}");
                throw;
            }
        }

        private static async Task TestBasicTemplateUsage()
        {
            Console.WriteLine("📋 1. Testing Basic Template Usage...");
            
            var generator = new AdvancedPromptGenerator();
            var config = new ExerciseConfiguration
            {
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.LINQ,
                Type = ExerciseType.Implementation,
                Context = "E-commerce analytics",
                EstimatedMinutes = 45
            };

            var mentorConfig = new MentorConfiguration
            {
                MentorName = "Test Mentor",
                CourseName = "Advanced .NET Course",
                TeachingStyle = "Practical",
                PreferredExampleDomain = "E-commerce"
            };

            try
            {
                var prompt = generator.GenerateTemplateBasedPrompt(config, mentorConfig);
                Console.WriteLine($"   ✅ Generated prompt length: {prompt.Length:N0} characters");
                Console.WriteLine($"   📝 Contains system prompt: {prompt.Contains("SuperClaude")}");
                Console.WriteLine($"   🎯 Contains exercise specs: {prompt.Contains("EXERCISE SPECIFICATIONS")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ❌ Template generation failed: {ex.Message}");
            }
        }

        private static async Task TestAdvancedTemplateUsage()
        {
            Console.WriteLine("📋 2. Testing Advanced Template with Context Engineering...");
            
            var generator = new AdvancedPromptGenerator();
            var config = new ExerciseConfiguration
            {
                Level = SkillLevel.Advanced,
                Topic = TopicArea.DesignPatterns,
                Type = ExerciseType.Design,
                Context = "Enterprise architecture",
                EstimatedMinutes = 90
            };

            var mentorConfig = new MentorConfiguration
            {
                MentorName = "Senior Architect",
                CourseName = "Enterprise .NET Patterns",
                TeachingStyle = "Architecture-focused",
                PreferredExampleDomain = "Enterprise systems"
            };

            try
            {
                var prompt = generator.GenerateTemplateBasedPrompt(config, mentorConfig, "advancedExerciseGeneration");
                Console.WriteLine($"   ✅ Advanced prompt generated successfully");
                Console.WriteLine($"   📊 Length: {prompt.Length:N0} characters");
                Console.WriteLine($"   🧠 Contains thinking framework: {prompt.Contains("<thinking>")}");
                Console.WriteLine($"   🎯 Contains context engineering: {prompt.Contains("Context Engineering")}");
                
                // Show a preview of the prompt
                var preview = prompt.Length > 500 ? prompt.Substring(0, 500) + "..." : prompt;
                Console.WriteLine($"   📖 Preview: {preview}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ❌ Advanced template failed: {ex.Message}");
            }
        }

        private static async Task TestCustomTemplateUsage()
        {
            Console.WriteLine("📋 3. Testing Custom Template with Additional Parameters...");
            
            var generator = new AdvancedPromptGenerator();
            var config = new ExerciseConfiguration
            {
                Level = SkillLevel.Beginner,
                Topic = TopicArea.CSharpFundamentals,
                Type = ExerciseType.Implementation,
                Context = "Gaming application",
                EstimatedMinutes = 30
            };

            var mentorConfig = new MentorConfiguration
            {
                MentorName = "Game Dev Mentor",
                CourseName = "C# for Game Development",
                TeachingStyle = "Interactive",
                PreferredExampleDomain = "Gaming"
            };

            var customParams = new Dictionary<string, string>
            {
                ["customSystemPrompt"] = "You are a game development instructor specializing in C# programming for Unity.",
                ["customPromptStructure"] = "Create a " + config.Type + " exercise for " + config.Level + " students learning " + config.Topic + " in the context of " + config.Context + ". Duration: " + config.EstimatedMinutes + " minutes."
            };

            try
            {
                var prompt = generator.GenerateCustomTemplatePrompt(config, mentorConfig, customParams, "customPromptGeneration");
                Console.WriteLine($"   ✅ Custom template used successfully");
                Console.WriteLine($"   📊 Generated prompt length: {prompt.Length} characters");
                Console.WriteLine($"   🎮 Contains gaming context: {prompt.Contains("game") || prompt.Contains("Gaming")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ❌ Custom template failed: {ex.Message}");
            }
        }

        private static async Task TestTemplateValidation()
        {
            Console.WriteLine("📋 4. Testing Template Validation...");
            
            var templateManager = new PromptTemplateManager();
            await templateManager.LoadConfigurationAsync();

            // Test valid parameters
            var validParams = new PromptParameters
            {
                SkillLevel = "Intermediate",
                TopicArea = "LINQ",
                ExerciseType = "Implementation",
                Context = "Data processing",
                EstimatedMinutes = 60,
                MentorConfiguration = new MentorConfiguration
                {
                    CourseName = "Test Course",
                    TeachingStyle = "Practical"
                }
            };

            var validation = templateManager.ValidateTemplate("advancedExerciseGeneration", validParams);
            Console.WriteLine($"   ✅ Valid parameters validation: {validation.IsValid} - {validation.Message}");

            // Test missing parameters
            var invalidParams = new PromptParameters
            {
                SkillLevel = "Beginner"
                // Missing required parameters
            };

            var invalidValidation = templateManager.ValidateTemplate("advancedExerciseGeneration", invalidParams);
            Console.WriteLine($"   ❌ Invalid parameters validation: {invalidValidation.IsValid} - {invalidValidation.Message}");

            // List available templates
            var templates = templateManager.GetAvailableTemplates();
            Console.WriteLine($"   📋 Available templates: {string.Join(", ", templates)}");
        }

        public static Task DemoTemplateInfo()
        {
            Console.WriteLine("\n🔍 TEMPLATE INFORMATION DEMO");
            Console.WriteLine("=" + new string('=', 40));
            
            var generator = new AdvancedPromptGenerator();
            var templates = generator.GetAvailableTemplates();
            
            foreach (var templateId in templates)
            {
                Console.WriteLine($"\n📋 Template ID: {templateId}");
                var info = generator.GetTemplateInfo(templateId);
                Console.WriteLine(info);
                Console.WriteLine(new string('-', 40));
            }
            
            return Task.CompletedTask;
        }
    }
}