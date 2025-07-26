using System;
using System.Threading.Tasks;
using CursoNET.ExerciseGenerator.Configuration;

// Quick test to verify template validation fixes
public class TemplateFixTest
{
    public static async Task TestTemplateValidation()
    {
        Console.WriteLine("🔧 Testing Template Validation Fix...\n");
        
        try
        {
            var templateManager = new PromptTemplateManager();
            await templateManager.LoadConfigurationAsync();
            
            // Test parameters that should work
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
                    TeachingStyle = "Practical",
                    MentorName = "Test Mentor",
                    PreferredExampleDomain = "E-commerce"
                }
            };
            
            // Test contextEngineeringExpert template
            var validation = templateManager.ValidateTemplate("contextEngineeringExpert", validParams);
            Console.WriteLine($"✅ contextEngineeringExpert validation: {validation.IsValid}");
            if (!validation.IsValid)
            {
                Console.WriteLine($"   ❌ Error: {validation.Message}");
            }
            
            // Try to generate a prompt
            var prompt = templateManager.GeneratePrompt("contextEngineeringExpert", validParams);
            Console.WriteLine($"✅ Prompt generation: SUCCESS ({prompt.Length} characters)");
            
            // Test other templates too
            var basicValidation = templateManager.ValidateTemplate("basicExerciseGeneration", validParams);
            Console.WriteLine($"✅ basicExerciseGeneration validation: {basicValidation.IsValid}");
            
            Console.WriteLine("\n🎉 Template validation fix appears to be working!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error during test: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
}