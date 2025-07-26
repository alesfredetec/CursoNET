using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Comprehensive .NET Exercise Generator Agent
    /// 
    /// This agent creates educational exercises, practice problems, and learning activities
    /// for .NET students across all skill levels. It follows pedagogical best practices
    /// and generates exercises that actively build competency and confidence.
    /// 
    /// Key Features:
    /// - Multi-level difficulty scaling (Beginner, Intermediate, Advanced)
    /// - Comprehensive topic coverage (C# fundamentals to advanced patterns)
    /// - Real-world scenario integration
    /// - Automated validation and quality assurance
    /// - Extensible template system
    /// </summary>
    public class DotNetExerciseGenerator
    {
        #region Configuration and Data Structures
        // Los tipos base ahora están en ExerciseTypes.cs
        #endregion

        #region Topic-Specific Generators

        private readonly Dictionary<TopicArea, IExerciseTopicGenerator> _topicGenerators;

        public DotNetExerciseGenerator()
        {
            _topicGenerators = new Dictionary<TopicArea, IExerciseTopicGenerator>
            {
                { TopicArea.CSharpFundamentals, new CSharpFundamentalsGenerator() },
                { TopicArea.ControlStructures, new ControlStructuresGenerator() },
                { TopicArea.MethodsAndParameters, new MethodsParametersGenerator() },
                { TopicArea.BasicOOP, new BasicOOPGenerator() },
                { TopicArea.Collections, new CollectionsGenerator() },
                { TopicArea.ExceptionHandling, new ExceptionHandlingGenerator() },
                { TopicArea.AdvancedOOP, new AdvancedOOPGenerator() },
                { TopicArea.LINQ, new LINQGenerator() },
                { TopicArea.Generics, new GenericsGenerator() },
                { TopicArea.DelegatesAndEvents, new DelegatesEventsGenerator() },
                { TopicArea.FileIO, new FileIOGenerator() },
                { TopicArea.UnitTesting, new UnitTestingGenerator() },
                { TopicArea.DesignPatterns, new DesignPatternsGenerator() },
                { TopicArea.EntityFramework, new EntityFrameworkGenerator() },
                { TopicArea.AspNetCore, new AspNetCoreGenerator() },
                { TopicArea.AsyncProgramming, new AsyncProgrammingGenerator() },
                { TopicArea.PerformanceOptimization, new PerformanceOptimizationGenerator() },
                { TopicArea.Microservices, new MicroservicesGenerator() }
            };
        }

        #endregion

        #region Core Exercise Generation

        /// <summary>
        /// Generates a single exercise based on the provided configuration
        /// </summary>
        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            ValidateConfiguration(config);

            if (!_topicGenerators.ContainsKey(config.Topic))
            {
                throw new NotSupportedException($"Topic {config.Topic} is not supported yet. Use AI prompt generation instead.");
            }

            var generator = _topicGenerators[config.Topic];
            
            try
            {
                var exercise = generator.GenerateExercise(config);

                // Apply educational validation
                ValidateExercise(exercise);

                // Enhance with common pedagogical elements
                EnhanceExercise(exercise, config);

                return exercise;
            }
            catch (Exception ex) when (ex.Message.Contains("not recognized") || ex.Message.Contains("not supported"))
            {
                throw new NotSupportedException($"Configuration not supported: {config.Level} {config.Type} for {config.Topic}. Use AI prompt generation instead.", ex);
            }
        }

        /// <summary>
        /// Generates multiple exercises for a complete learning module
        /// </summary>
        public List<Exercise> GenerateModule(string moduleName, List<ExerciseConfiguration> configs)
        {
            var exercises = new List<Exercise>();

            foreach (var config in configs)
            {
                var exercise = GenerateExercise(config);
                exercise.Title = $"{moduleName} - {exercise.Title}";
                exercises.Add(exercise);
            }

            // Ensure progressive difficulty
            ValidateProgression(exercises);

            return exercises;
        }

        /// <summary>
        /// Generates a complete curriculum with multiple modules
        /// </summary>
        public Dictionary<string, List<Exercise>> GenerateCurriculum(Dictionary<string, List<ExerciseConfiguration>> modules)
        {
            var curriculum = new Dictionary<string, List<Exercise>>();

            foreach (var module in modules)
            {
                curriculum[module.Key] = GenerateModule(module.Key, module.Value);
            }

            return curriculum;
        }

        #endregion

        #region Exercise Export and File Management

        /// <summary>
        /// Exports an exercise to files (starter code, solution, tests, documentation)
        /// </summary>
        public void ExportExercise(Exercise exercise, string basePath)
        {
            var exerciseDir = Path.Combine(basePath, SanitizeFileName(exercise.Title));
            Directory.CreateDirectory(exerciseDir);

            // Generate README with exercise description
            var readme = GenerateReadMe(exercise);
            File.WriteAllText(Path.Combine(exerciseDir, "README.md"), readme);

            // Generate starter code file
            var starterFileName = $"{SanitizeFileName(exercise.Title)}-Starter.cs";
            File.WriteAllText(Path.Combine(exerciseDir, starterFileName), exercise.StarterCode);

            // Generate solution code file
            var solutionFileName = $"{SanitizeFileName(exercise.Title)}-Solution.cs";
            File.WriteAllText(Path.Combine(exerciseDir, solutionFileName), exercise.SolutionCode);

            // Generate unit test file if included
            if (!string.IsNullOrWhiteSpace(exercise.UnitTestCode))
            {
                var testFileName = $"{SanitizeFileName(exercise.Title)}-Tests.cs";
                File.WriteAllText(Path.Combine(exerciseDir, testFileName), exercise.UnitTestCode);
            }

            // Generate project file
            var projectFile = GenerateProjectFile(exercise);
            File.WriteAllText(Path.Combine(exerciseDir, $"{SanitizeFileName(exercise.Title)}.csproj"), projectFile);
        }

        /// <summary>
        /// Exports multiple exercises as a complete module
        /// </summary>
        public void ExportModule(string moduleName, List<Exercise> exercises, string basePath)
        {
            var moduleDir = Path.Combine(basePath, SanitizeFileName(moduleName));
            Directory.CreateDirectory(moduleDir);

            // Generate module README
            var moduleReadme = GenerateModuleReadMe(moduleName, exercises);
            File.WriteAllText(Path.Combine(moduleDir, "README.md"), moduleReadme);

            // Export each exercise
            foreach (var exercise in exercises)
            {
                ExportExercise(exercise, moduleDir);
            }

            // Generate solution file for the module
            var solutionFile = GenerateModuleSolution(moduleName, exercises);
            File.WriteAllText(Path.Combine(moduleDir, $"{SanitizeFileName(moduleName)}.sln"), solutionFile);
        }

        #endregion

        #region Validation and Quality Assurance

        private void ValidateConfiguration(ExerciseConfiguration config)
        {
            if (!_topicGenerators.ContainsKey(config.Topic))
                throw new ArgumentException($"Topic {config.Topic} is not supported");

            if (config.EstimatedMinutes < 5 || config.EstimatedMinutes > 480)
                throw new ArgumentException("Estimated minutes should be between 5 and 480");
        }

        private void ValidateExercise(Exercise exercise)
        {
            var issues = new List<string>();

            // Check for essential components
            if (string.IsNullOrWhiteSpace(exercise.Title))
                issues.Add("Exercise must have a title");

            if (exercise.LearningObjectives.Count == 0)
                issues.Add("Exercise must have at least one learning objective");

            if (string.IsNullOrWhiteSpace(exercise.ProblemStatement))
                issues.Add("Exercise must have a clear problem statement");

            if (string.IsNullOrWhiteSpace(exercise.StarterCode))
                issues.Add("Exercise must provide starter code");

            if (string.IsNullOrWhiteSpace(exercise.SolutionCode))
                issues.Add("Exercise must provide solution code");

            if (exercise.SuccessCriteria.Count == 0)
                issues.Add("Exercise must define success criteria");

            // Validate code compilation (simplified check)
            if (!IsValidCSharpSyntax(exercise.StarterCode))
                issues.Add("Starter code contains syntax errors");

            if (!IsValidCSharpSyntax(exercise.SolutionCode))
                issues.Add("Solution code contains syntax errors");

            if (issues.Any())
                throw new InvalidOperationException($"Exercise validation failed: {string.Join(", ", issues)}");
        }

        private void ValidateProgression(List<Exercise> exercises)
        {
            // Ensure exercises progress logically in difficulty
            for (int i = 1; i < exercises.Count; i++)
            {
                var prev = exercises[i - 1];
                var curr = exercises[i];

                // Current exercise shouldn't be significantly easier than previous
                if (curr.Level < prev.Level && curr.EstimatedMinutes < prev.EstimatedMinutes * 0.7)
                {
                    Console.WriteLine($"Warning: Exercise '{curr.Title}' may be too easy after '{prev.Title}'");
                }
            }
        }

        private bool IsValidCSharpSyntax(string code)
        {
            // Basic syntax validation - in a real implementation, 
            // this would use Roslyn to compile and validate
            return !string.IsNullOrWhiteSpace(code) && 
                   code.Contains("namespace") && 
                   code.Contains("class");
        }

        #endregion

        #region Helper Methods

        private void EnhanceExercise(Exercise exercise, ExerciseConfiguration config)
        {
            // Add context-specific enhancements
            if (config.Context != "General")
            {
                exercise.Description = $"Context: {config.Context}\n\n{exercise.Description}";
            }

            // Ensure realistic time estimates
            if (exercise.EstimatedMinutes == 0)
            {
                exercise.EstimatedMinutes = EstimateTime(exercise);
            }

            // Add skill-appropriate guidance
            AddSkillLevelGuidance(exercise);
        }

        private int EstimateTime(Exercise exercise)
        {
            int baseTime = exercise.Level switch
            {
                SkillLevel.Beginner => 20,
                SkillLevel.Intermediate => 35,
                SkillLevel.Advanced => 50,
                _ => 30
            };

            // Adjust based on exercise type
            var typeMultiplier = exercise.Type switch
            {
                ExerciseType.Implementation => 1.5,
                ExerciseType.Design => 2.0,
                ExerciseType.Refactoring => 1.0,
                ExerciseType.DebugFix => 0.8,
                ExerciseType.Extension => 1.2,
                ExerciseType.Performance => 1.8,
                ExerciseType.Testing => 1.3,
                _ => 1.0
            };

            return (int)(baseTime * typeMultiplier);
        }

        private void AddSkillLevelGuidance(Exercise exercise)
        {
            switch (exercise.Level)
            {
                case SkillLevel.Beginner:
                    exercise.CommonPitfalls.Add("Don't worry about perfect code - focus on getting it working first");
                    exercise.CommonPitfalls.Add("Use descriptive variable names to make your code readable");
                    exercise.CommonPitfalls.Add("Test your code frequently with small changes");
                    break;

                case SkillLevel.Intermediate:
                    exercise.CommonPitfalls.Add("Consider SOLID principles when designing your solution");
                    exercise.CommonPitfalls.Add("Think about error handling and edge cases");
                    exercise.CommonPitfalls.Add("Consider the maintainability of your code");
                    break;

                case SkillLevel.Advanced:
                    exercise.CommonPitfalls.Add("Consider performance implications of your design choices");
                    exercise.CommonPitfalls.Add("Think about scalability and extensibility");
                    exercise.CommonPitfalls.Add("Consider architectural patterns and their trade-offs");
                    break;
            }
        }

        private string SanitizeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        }

        private string GenerateReadMe(Exercise exercise)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"# {exercise.Title}");
            sb.AppendLine();
            sb.AppendLine($"**Skill Level:** {exercise.Level}");
            sb.AppendLine($"**Topic Area:** {exercise.Topic}");
            sb.AppendLine($"**Exercise Type:** {exercise.Type}");
            sb.AppendLine($"**Estimated Time:** {exercise.EstimatedMinutes} minutes");
            sb.AppendLine();
            
            sb.AppendLine("## Description");
            sb.AppendLine(exercise.Description);
            sb.AppendLine();
            
            sb.AppendLine("## Learning Objectives");
            foreach (var objective in exercise.LearningObjectives)
            {
                sb.AppendLine($"- {objective}");
            }
            sb.AppendLine();
            
            sb.AppendLine("## Prerequisites");
            foreach (var prereq in exercise.Prerequisites)
            {
                sb.AppendLine($"- {prereq}");
            }
            sb.AppendLine();
            
            sb.AppendLine("## Problem Statement");
            sb.AppendLine(exercise.ProblemStatement);
            sb.AppendLine();
            
            sb.AppendLine("## Technical Requirements");
            foreach (var req in exercise.TechnicalRequirements)
            {
                sb.AppendLine($"- {req}");
            }
            sb.AppendLine();
            
            sb.AppendLine("## Success Criteria");
            foreach (var criteria in exercise.SuccessCriteria)
            {
                sb.AppendLine($"- {criteria}");
            }
            sb.AppendLine();
            
            if (exercise.ExtensionChallenges.Any())
            {
                sb.AppendLine("## Extension Challenges");
                foreach (var challenge in exercise.ExtensionChallenges)
                {
                    sb.AppendLine($"- {challenge}");
                }
                sb.AppendLine();
            }
            
            if (exercise.CommonPitfalls.Any())
            {
                sb.AppendLine("## Common Pitfalls & Tips");
                foreach (var pitfall in exercise.CommonPitfalls)
                {
                    sb.AppendLine($"- {pitfall}");
                }
                sb.AppendLine();
            }
            
            sb.AppendLine("## Files");
            sb.AppendLine($"- `{SanitizeFileName(exercise.Title)}-Starter.cs` - Your starting point");
            sb.AppendLine($"- `{SanitizeFileName(exercise.Title)}-Solution.cs` - Complete solution (review after attempting)");
            if (!string.IsNullOrWhiteSpace(exercise.UnitTestCode))
            {
                sb.AppendLine($"- `{SanitizeFileName(exercise.Title)}-Tests.cs` - Unit tests to validate your solution");
            }
            
            return sb.ToString();
        }

        private string GenerateModuleReadMe(string moduleName, List<Exercise> exercises)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"# {moduleName} - Learning Module");
            sb.AppendLine();
            sb.AppendLine($"This module contains {exercises.Count} exercises designed to build your .NET development skills progressively.");
            sb.AppendLine();
            
            sb.AppendLine("## Exercises");
            for (int i = 0; i < exercises.Count; i++)
            {
                var ex = exercises[i];
                sb.AppendLine($"{i + 1}. **{ex.Title}** ({ex.Level}) - {ex.EstimatedMinutes} minutes");
                sb.AppendLine($"   - Topic: {ex.Topic}");
                sb.AppendLine($"   - Type: {ex.Type}");
                sb.AppendLine();
            }
            
            var totalTime = exercises.Sum(e => e.EstimatedMinutes);
            sb.AppendLine($"**Total Estimated Time:** {totalTime} minutes ({totalTime / 60.0:F1} hours)");
            
            return sb.ToString();
        }

        private string GenerateProjectFile(Exercise exercise)
        {
            var targetFramework = exercise.Level switch
            {
                SkillLevel.Beginner => "net8.0",
                SkillLevel.Intermediate => "net8.0",
                SkillLevel.Advanced => "net8.0",
                _ => "net8.0"
            };

            var sb = new StringBuilder();
            sb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sb.AppendLine();
            sb.AppendLine("  <PropertyGroup>");
            sb.AppendLine($"    <TargetFramework>{targetFramework}</TargetFramework>");
            sb.AppendLine("    <ImplicitUsings>enable</ImplicitUsings>");
            sb.AppendLine("    <Nullable>enable</Nullable>");
            sb.AppendLine("  </PropertyGroup>");
            sb.AppendLine();

            // Add packages based on topic
            if (NeedsTestingPackages(exercise.Topic) || !string.IsNullOrWhiteSpace(exercise.UnitTestCode))
            {
                sb.AppendLine("  <ItemGroup>");
                sb.AppendLine("    <PackageReference Include=\"Microsoft.NET.Test.Sdk\" Version=\"17.8.0\" />");
                sb.AppendLine("    <PackageReference Include=\"xunit\" Version=\"2.6.1\" />");
                sb.AppendLine("    <PackageReference Include=\"xunit.runner.visualstudio\" Version=\"2.5.3\" />");
                sb.AppendLine("  </ItemGroup>");
                sb.AppendLine();
            }

            if (NeedsEntityFramework(exercise.Topic))
            {
                sb.AppendLine("  <ItemGroup>");
                sb.AppendLine("    <PackageReference Include=\"Microsoft.EntityFrameworkCore\" Version=\"8.0.0\" />");
                sb.AppendLine("    <PackageReference Include=\"Microsoft.EntityFrameworkCore.InMemory\" Version=\"8.0.0\" />");
                sb.AppendLine("  </ItemGroup>");
                sb.AppendLine();
            }

            if (NeedsAspNetCore(exercise.Topic))
            {
                sb.AppendLine("  <ItemGroup>");
                sb.AppendLine("    <PackageReference Include=\"Microsoft.AspNetCore.App\" />");
                sb.AppendLine("  </ItemGroup>");
                sb.AppendLine();
            }

            sb.AppendLine("</Project>");
            return sb.ToString();
        }

        private string GenerateModuleSolution(string moduleName, List<Exercise> exercises)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Microsoft Visual Studio Solution File, Format Version 12.00");
            sb.AppendLine("# Visual Studio Version 17");
            sb.AppendLine("VisualStudioVersion = 17.0.31903.59");
            sb.AppendLine("MinimumVisualStudioVersion = 10.0.40219.1");

            foreach (var exercise in exercises)
            {
                var projectName = SanitizeFileName(exercise.Title);
                var guid = Guid.NewGuid().ToString().ToUpper();
                sb.AppendLine($"Project(\"{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}\") = \"{projectName}\", \"{projectName}\\{projectName}.csproj\", \"{{{guid}}}\"");
                sb.AppendLine("EndProject");
            }

            sb.AppendLine("Global");
            sb.AppendLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            sb.AppendLine("\t\tDebug|Any CPU = Debug|Any CPU");
            sb.AppendLine("\t\tRelease|Any CPU = Release|Any CPU");
            sb.AppendLine("\tEndGlobalSection");
            sb.AppendLine("EndGlobal");

            return sb.ToString();
        }

        private bool NeedsTestingPackages(TopicArea topic)
        {
            return topic == TopicArea.UnitTesting || 
                   topic == TopicArea.AdvancedOOP || 
                   topic == TopicArea.DesignPatterns;
        }

        private bool NeedsEntityFramework(TopicArea topic)
        {
            return topic == TopicArea.EntityFramework;
        }

        private bool NeedsAspNetCore(TopicArea topic)
        {
            return topic == TopicArea.AspNetCore || topic == TopicArea.Microservices;
        }

        #endregion
    }
}