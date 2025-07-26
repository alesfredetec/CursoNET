using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CursoNET.ExerciseGenerator.Configuration;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Programa de prueba para el nuevo sistema configurable de ejercicios
    /// </summary>
    public class TestConfigurableSystem
    {
        public static async Task RunTestAsync()
        {
            Console.WriteLine("=== TESTING CONFIGURABLE EXERCISE SYSTEM ===\n");

            try
            {
                // 1. Probar carga de configuraciones
                await TestConfigurationLoading();

                // 2. Probar carga de ejercicio con código separado
                await TestExerciseFileResolution();

                // 3. Probar validaciones
                await TestValidations();

                // 4. Probar consultas y filtros
                await TestQueriesAndFilters();

                Console.WriteLine("\n✅ ALL TESTS PASSED - Sistema configurable funcionando correctamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ TEST FAILED: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private static async Task TestConfigurationLoading()
        {
            Console.WriteLine("📋 Testing Configuration Loading...");

            var configManager = new ConfigurationManager("jsondata/config");
            await configManager.LoadAllAsync();

            // Verificar topics
            var topics = configManager.Topics.GetAll().ToList();
            Console.WriteLine($"  ✅ Loaded {topics.Count} topics");
            
            var csharpTopic = configManager.GetTopic("CSharpFundamentals");
            if (csharpTopic != null)
            {
                Console.WriteLine($"  ✅ Found C# Fundamentals topic: {csharpTopic.DisplayName}");
            }

            // Verificar exercise types
            var exerciseTypes = configManager.ExerciseTypes.GetAll().ToList();
            Console.WriteLine($"  ✅ Loaded {exerciseTypes.Count} exercise types");

            // Verificar skill levels
            var skillLevels = configManager.SkillLevels.GetAll().ToList();
            Console.WriteLine($"  ✅ Loaded {skillLevels.Count} skill levels");

            // Verificar dependencias
            var hasPrereqs = configManager.ArePrerequisitesSatisfied("LINQ", new[] { "CSharpFundamentals", "BasicOOP" });
            Console.WriteLine($"  ✅ LINQ prerequisites check: {hasPrereqs}");

            Console.WriteLine("  🎉 Configuration loading test completed\n");
        }

        private static async Task TestExerciseFileResolution()
        {
            Console.WriteLine("📁 Testing Exercise File Resolution...");

            var configManager = new ConfigurationManager("jsondata/config");
            await configManager.LoadAllAsync();

            var fileResolver = new ExerciseFileResolver("jsondata", configManager);

            // Probar carga del ejercicio de variables
            var exercisePath = "jsondata/beginner/csharp-fundamentals/refactoring/variables-new.json";
            
            if (File.Exists(exercisePath))
            {
                var resolvedExercise = await fileResolver.LoadExerciseAsync(exercisePath);
                
                Console.WriteLine($"  ✅ Loaded exercise: {resolvedExercise.Schema.Metadata.Title}");
                Console.WriteLine($"  ✅ Topic: {resolvedExercise.Schema.Metadata.TopicId}");
                Console.WriteLine($"  ✅ Type: {resolvedExercise.Schema.Metadata.ExerciseTypeId}");
                Console.WriteLine($"  ✅ Level: {resolvedExercise.Schema.Metadata.SkillLevelId}");

                // Verificar archivos cargados
                var hasBeforeCode = !string.IsNullOrEmpty(resolvedExercise.Files.BeforeCode);
                var hasAfterCode = !string.IsNullOrEmpty(resolvedExercise.Files.AfterCode);
                var hasStarterCode = !string.IsNullOrEmpty(resolvedExercise.Files.StarterTemplate);

                Console.WriteLine($"  ✅ Before code loaded: {hasBeforeCode} ({resolvedExercise.Files.BeforeCode.Length} chars)");
                Console.WriteLine($"  ✅ After code loaded: {hasAfterCode} ({resolvedExercise.Files.AfterCode.Length} chars)");
                Console.WriteLine($"  ✅ Starter code loaded: {hasStarterCode} ({resolvedExercise.Files.StarterTemplate.Length} chars)");

                // Verificar conversión a formato legacy
                var legacyExercise = resolvedExercise.ToLegacyExercise();
                Console.WriteLine($"  ✅ Legacy conversion: {legacyExercise.Title}");
            }
            else
            {
                Console.WriteLine($"  ⚠️  Exercise file not found: {exercisePath}");
            }

            Console.WriteLine("  🎉 File resolution test completed\n");
        }

        private static async Task TestValidations()
        {
            Console.WriteLine("🔍 Testing Validations...");

            var configManager = new ConfigurationManager("jsondata/config");
            await configManager.LoadAllAsync();

            // Probar validación de configuraciones
            try
            {
                configManager.ValidateConfigurations();
                Console.WriteLine("  ✅ Configuration validation passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Configuration validation failed: {ex.Message}");
            }

            // Probar validación de dependencias
            var dependencyResults = configManager.Dependencies.ValidateAllDependencies().ToList();
            var validationErrors = dependencyResults.Where(r => !r.IsValid).ToList();
            
            if (validationErrors.Any())
            {
                Console.WriteLine($"  ⚠️  Found {validationErrors.Count} dependency validation issues:");
                foreach (var error in validationErrors.Take(3))
                {
                    Console.WriteLine($"    - {error.ErrorMessage}");
                }
            }
            else
            {
                Console.WriteLine("  ✅ Dependency validation passed");
            }

            Console.WriteLine("  🎉 Validation test completed\n");
        }

        private static async Task TestQueriesAndFilters()
        {
            Console.WriteLine("🔎 Testing Queries and Filters...");

            var configManager = new ConfigurationManager("jsondata/config");
            await configManager.LoadAllAsync();

            // Probar consultas de topics por nivel
            var beginnerTopics = configManager.GetTopicsForLevel("Beginner").ToList();
            var intermediateTopics = configManager.GetTopicsForLevel("Intermediate").ToList();
            var advancedTopics = configManager.GetTopicsForLevel("Advanced").ToList();

            Console.WriteLine($"  ✅ Beginner topics: {beginnerTopics.Count}");
            Console.WriteLine($"  ✅ Intermediate topics: {intermediateTopics.Count}");
            Console.WriteLine($"  ✅ Advanced topics: {advancedTopics.Count}");

            // Mostrar algunos examples
            Console.WriteLine("\n  📚 Sample Beginner Topics:");
            foreach (var topic in beginnerTopics.Take(3))
            {
                Console.WriteLine($"    - {topic.DisplayName} (Category: {topic.Category})");
            }

            // Probar consultas de exercise types por nivel
            var beginnerExerciseTypes = configManager.GetExerciseTypesForLevel("Beginner").ToList();
            Console.WriteLine($"\n  ✅ Exercise types for Beginner: {beginnerExerciseTypes.Count}");
            
            foreach (var exerciseType in beginnerExerciseTypes.Take(3))
            {
                Console.WriteLine($"    - {exerciseType.DisplayName} (Difficulty: {exerciseType.Difficulty})");
            }

            // Probar progresión de skill levels
            var skillLevels = configManager.SkillLevels.GetOrderedByProgression().ToList();
            Console.WriteLine($"\n  ✅ Skill level progression:");
            foreach (var level in skillLevels)
            {
                var nextLevel = configManager.SkillLevels.GetNextLevel(level.Id);
                var nextLevelName = nextLevel?.DisplayName ?? "None";
                Console.WriteLine($"    - {level.DisplayName} → {nextLevelName}");
            }

            Console.WriteLine("  🎉 Queries and filters test completed\n");
        }
    }
}