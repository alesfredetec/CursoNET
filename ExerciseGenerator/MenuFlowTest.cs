using System;
using System.Threading.Tasks;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Test manual para verificar que todos los flujos de menú funcionen correctamente
    /// </summary>
    public class MenuFlowTest
    {
        public static async Task RunMenuFlowTestAsync()
        {
            Console.WriteLine("🧪 TESTING MENU FLOWS - Verificando que todos los flujos trabajen con ejercicios o prompts IA");
            Console.WriteLine("=" + new string('=', 80));
            Console.WriteLine();

            var demo = new ExerciseGeneratorDemo();
            
            // Test configurations that should trigger different fallback scenarios
            var testConfigs = new[]
            {
                // Test 1: Basic configuration that might have existing exercises
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Beginner,
                    Topic = TopicArea.CSharpFundamentals,
                    Type = ExerciseType.Implementation,
                    Context = "Personal productivity app"
                },
                
                // Test 2: Intermediate configuration  
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Intermediate,
                    Topic = TopicArea.LINQ,
                    Type = ExerciseType.Implementation,
                    Context = "E-commerce analytics"
                },
                
                // Test 3: Advanced configuration that likely needs AI prompt
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.Microservices,
                    Type = ExerciseType.Design,
                    Context = "Enterprise system architecture"
                },
                
                // Test 4: Configuration that should definitely trigger AI prompt
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.PerformanceOptimization,
                    Type = ExerciseType.Performance,
                    Context = "High-frequency trading system"
                }
            };

            int testNumber = 1;
            foreach (var config in testConfigs)
            {
                Console.WriteLine($"📋 Test {testNumber}: {config.Level} {config.Topic} {config.Type}");
                Console.WriteLine($"   Context: {config.Context}");
                
                try
                {
                    // Use reflection to call the private GenerateAndDisplay method
                    var method = typeof(ExerciseGeneratorDemo).GetMethod("GenerateAndDisplay", 
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    
                    if (method != null)
                    {
                        var result = method.Invoke(demo, new object[] { $"Test Exercise {testNumber}", config });
                        Console.WriteLine($"   ✅ Result: {result?.GetType()?.Name ?? "null"}");
                    }
                    else
                    {
                        Console.WriteLine($"   ❌ Could not access GenerateAndDisplay method");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"   ❌ Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"      Inner: {ex.InnerException.Message}");
                    }
                }
                
                Console.WriteLine();
                testNumber++;
            }

            Console.WriteLine("🎯 Test Summary:");
            Console.WriteLine("   - All configurations should either generate complete exercises or fall back to AI prompts");
            Console.WriteLine("   - No configuration should crash the application");
            Console.WriteLine("   - Menu flows 1-7 should all work without throwing unhandled exceptions");
            Console.WriteLine();
            Console.WriteLine("✅ Menu flow testing completed!");
        }
    }
}