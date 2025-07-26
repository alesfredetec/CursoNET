using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Comprehensive demonstration of the .NET Exercise Generator Agent
    /// 
    /// This demo shows how educators can use the system to:
    /// - Generate individual exercises for specific topics and skill levels
    /// - Create complete learning modules with progressive difficulty
    /// - Export exercises as ready-to-use project files
    /// - Build full curricula for .NET education programs
    /// </summary>
    public class ExerciseGeneratorDemo
    {
        private readonly DotNetExerciseGenerator _generator;
        private readonly string _outputPath;

        public ExerciseGeneratorDemo()
        {
            _generator = new DotNetExerciseGenerator();
            _outputPath = Path.Combine(Directory.GetCurrentDirectory(), "generated-exercises");
            
            // Ensure output directory exists
            Directory.CreateDirectory(_outputPath);
        }

        public static void RunOriginalDemo(string[] args)
        {
            var demo = new ExerciseGeneratorDemo();
            
            Console.WriteLine("🎓 .NET Exercise Generator Agent - Comprehensive Demo");
            Console.WriteLine("=" + new string('=', 60));
            
            // Show interactive menu
            demo.ShowInteractiveMenu();
        }

        private void ShowInteractiveMenu()
        {
            while (true)
            {
                Console.WriteLine("\n📚 What would you like to generate?");
                Console.WriteLine("1. 🔥 Quick Exercise Generation");
                Console.WriteLine("2. 📖 Complete Learning Module");
                Console.WriteLine("3. 🎯 Beginner's Journey (Full Course)");
                Console.WriteLine("4. 🚀 Advanced Developer Track");
                Console.WriteLine("5. 📊 Exercise Generator Statistics");
                Console.WriteLine("6. 🔧 Custom Exercise Builder");
                Console.WriteLine("7. 📁 Browse Generated Exercises");
                Console.WriteLine("0. Exit");
                
                Console.Write("\nEnter your choice (0-7): ");
                string? choice = Console.ReadLine();
                
                try
                {
                    switch (choice)
                    {
                        case "1":
                            DemoQuickGeneration();
                            break;
                        case "2":
                            DemoModuleGeneration();
                            break;
                        case "3":
                            DemoBeginnersJourney();
                            break;
                        case "4":
                            DemoAdvancedTrack();
                            break;
                        case "5":
                            ShowGeneratorStatistics();
                            break;
                        case "6":
                            CustomExerciseBuilder();
                            break;
                        case "7":
                            BrowseGeneratedExercises();
                            break;
                        case "0":
                            Console.WriteLine("\n👋 Thank you for using the .NET Exercise Generator!");
                            return;
                        default:
                            Console.WriteLine("❌ Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        #region Demo Scenarios

        private void DemoQuickGeneration()
        {
            Console.WriteLine("\n🔥 Quick Exercise Generation Demo");
            Console.WriteLine("Generating exercises for different skill levels and topics...\n");

            // Generate a variety of exercises to showcase capabilities
            var exercises = new[]
            {
                // Beginner exercises
                GenerateAndDisplay("C# Fundamentals - Beginner", new ExerciseConfiguration
                {
                    Level = SkillLevel.Beginner,
                    Topic = TopicArea.CSharpFundamentals,
                    Type = ExerciseType.Implementation,
                    Context = "Personal productivity app"
                }),

                // Intermediate exercises  
                GenerateAndDisplay("LINQ Queries - Intermediate", new ExerciseConfiguration
                {
                    Level = SkillLevel.Intermediate,
                    Topic = TopicArea.LINQ,
                    Type = ExerciseType.Implementation,
                    Context = "E-commerce analytics"
                }),

                // Advanced exercises
                GenerateAndDisplay("Design Patterns - Advanced", new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.DesignPatterns,
                    Type = ExerciseType.Implementation,
                    Context = "Enterprise payment system"
                })
            };

            // Export all exercises
            Console.WriteLine("\n📁 Exporting exercises to files...");
            foreach (var exercise in exercises)
            {
                _generator.ExportExercise(exercise, _outputPath);
            }

            Console.WriteLine($"\n✅ Generated {exercises.Length} exercises and exported to: {_outputPath}");
            PauseForUser();
        }

        private void DemoModuleGeneration()
        {
            Console.WriteLine("\n📖 Complete Learning Module Demo");
            Console.WriteLine("Creating a comprehensive 'Object-Oriented Programming' module...\n");

            var moduleConfigs = new List<ExerciseConfiguration>
            {
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Beginner,
                    Topic = TopicArea.BasicOOP,
                    Type = ExerciseType.Implementation,
                    EstimatedMinutes = 30,
                    Context = "Banking system"
                },
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Intermediate,
                    Topic = TopicArea.AdvancedOOP,
                    Type = ExerciseType.Implementation,
                    EstimatedMinutes = 45,
                    Context = "E-commerce platform"
                },
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.DesignPatterns,
                    Type = ExerciseType.Refactoring,
                    EstimatedMinutes = 60,
                    Context = "Legacy system modernization"
                }
            };

            // Generate each exercise individually with fallback logic
            var module = new List<Exercise>();
            Console.WriteLine("📚 Generating module exercises:");
            
            foreach (var config in moduleConfigs)
            {
                var exercise = GenerateAndDisplay($"OOP Module - {config.Topic}", config);
                module.Add(exercise);
            }

            // Export module if we have exercises
            if (module.Count > 0)
            {
                _generator.ExportModule("OOP-Mastery", module, _outputPath);
            }
            
            var totalTime = module.Sum(e => e.EstimatedMinutes);
            Console.WriteLine($"\n✅ Module exported successfully!");
            Console.WriteLine($"📊 Total estimated time: {totalTime} minutes ({totalTime / 60.0:F1} hours)");
            
            PauseForUser();
        }

        private void DemoBeginnersJourney()
        {
            Console.WriteLine("\n🎯 Beginner's Journey - Complete Course Demo");
            Console.WriteLine("Creating a full beginner-to-intermediate progression...\n");

            var curriculum = new Dictionary<string, List<ExerciseConfiguration>>
            {
                ["Module 1: C# Fundamentals"] = new List<ExerciseConfiguration>
                {
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.CSharpFundamentals, Type = ExerciseType.Implementation },
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.CSharpFundamentals, Type = ExerciseType.DebugFix },
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.CSharpFundamentals, Type = ExerciseType.Extension }
                },
                ["Module 2: Control Structures"] = new List<ExerciseConfiguration>
                {
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.ControlStructures, Type = ExerciseType.Implementation },
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.ControlStructures, Type = ExerciseType.Refactoring }
                },
                ["Module 3: Methods & Parameters"] = new List<ExerciseConfiguration>
                {
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.MethodsAndParameters, Type = ExerciseType.Implementation },
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.MethodsAndParameters, Type = ExerciseType.Extension }
                },
                ["Module 4: Object-Oriented Programming"] = new List<ExerciseConfiguration>
                {
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.BasicOOP, Type = ExerciseType.Implementation },
                    new() { Level = SkillLevel.Intermediate, Topic = TopicArea.AdvancedOOP, Type = ExerciseType.Implementation }
                },
                ["Module 5: Collections & LINQ"] = new List<ExerciseConfiguration>
                {
                    new() { Level = SkillLevel.Beginner, Topic = TopicArea.Collections, Type = ExerciseType.Implementation },
                    new() { Level = SkillLevel.Intermediate, Topic = TopicArea.LINQ, Type = ExerciseType.Implementation }
                }
            };

            // Generate curriculum using individual exercise generation with fallback
            var fullCurriculum = new Dictionary<string, List<Exercise>>();
            Console.WriteLine("🎓 Complete Beginner's Journey Curriculum:");
            
            int totalExercises = 0;
            int totalMinutes = 0;
            
            foreach (var moduleEntry in curriculum)
            {
                var moduleName = moduleEntry.Key;
                var moduleConfigs = moduleEntry.Value;
                var moduleExercises = new List<Exercise>();
                
                Console.WriteLine($"\n📚 {moduleName}:");
                
                foreach (var config in moduleConfigs)
                {
                    var exercise = GenerateAndDisplay($"{moduleName} - {config.Type}", config);
                    moduleExercises.Add(exercise);
                    Console.WriteLine($"  • {exercise.Title} ({exercise.Type}) - {exercise.EstimatedMinutes}min");
                    totalExercises++;
                    totalMinutes += exercise.EstimatedMinutes;
                }
                
                fullCurriculum[moduleName] = moduleExercises;
                
                // Export each module
                if (moduleExercises.Count > 0)
                {
                    _generator.ExportModule(moduleName, moduleExercises, _outputPath);
                }
            }
            
            Console.WriteLine($"\n📊 Curriculum Summary:");
            Console.WriteLine($"   Total Modules: {fullCurriculum.Count}");
            Console.WriteLine($"   Total Exercises: {totalExercises}");
            Console.WriteLine($"   Total Time: {totalMinutes} minutes ({totalMinutes / 60.0:F1} hours)");
            Console.WriteLine($"   Estimated Course Duration: {Math.Ceiling(totalMinutes / 60.0 / 2)} weeks (2 hours/week)");
            
            PauseForUser();
        }

        private void DemoAdvancedTrack()
        {
            Console.WriteLine("\n🚀 Advanced Developer Track Demo");
            Console.WriteLine("Creating advanced exercises for experienced developers...\n");

            var advancedConfigs = new List<ExerciseConfiguration>
            {
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.DesignPatterns,
                    Type = ExerciseType.Design,
                    EstimatedMinutes = 90,
                    Context = "Microservices architecture",
                    IncludeExtensionChallenges = true
                },
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.AsyncProgramming,
                    Type = ExerciseType.Performance,
                    EstimatedMinutes = 75,
                    Context = "High-throughput data processing"
                },
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.EntityFramework,
                    Type = ExerciseType.Implementation,
                    EstimatedMinutes = 80,
                    Context = "Enterprise application"
                },
                new ExerciseConfiguration
                {
                    Level = SkillLevel.Advanced,
                    Topic = TopicArea.AspNetCore,
                    Type = ExerciseType.Implementation,
                    EstimatedMinutes = 120,
                    Context = "RESTful API with authentication"
                }
            };

            var advancedExercises = new List<Exercise>();
            
            foreach (var config in advancedConfigs)
            {
                var exercise = GenerateAndDisplay($"Advanced - {config.Topic}", config);
                advancedExercises.Add(exercise);
                Console.WriteLine($"   Learning Objectives: {exercise.LearningObjectives.Count}");
                Console.WriteLine($"   Extension Challenges: {exercise.ExtensionChallenges.Count}");
                Console.WriteLine();
            }

            // Export advanced track
            _generator.ExportModule("Advanced-Developer-Track", advancedExercises, _outputPath);
            
            var totalTime = advancedExercises.Sum(e => e.EstimatedMinutes);
            Console.WriteLine($"✅ Advanced track generated with {advancedExercises.Count} exercises");
            Console.WriteLine($"📊 Total time: {totalTime} minutes ({totalTime / 60.0:F1} hours)");
            
            PauseForUser();
        }

        private void ShowGeneratorStatistics()
        {
            Console.WriteLine("\n📊 Exercise Generator Statistics");
            Console.WriteLine("=" + new string('=', 40));
            
            // Topic coverage
            var topicAreas = Enum.GetValues<TopicArea>();
            Console.WriteLine($"\n📚 Topic Coverage: {topicAreas.Length} areas");
            
            var beginnerTopics = topicAreas.Where(t => CanGenerateForLevel(t, SkillLevel.Beginner)).ToList();
            var intermediateTopics = topicAreas.Where(t => CanGenerateForLevel(t, SkillLevel.Intermediate)).ToList();
            var advancedTopics = topicAreas.Where(t => CanGenerateForLevel(t, SkillLevel.Advanced)).ToList();
            
            Console.WriteLine($"  • Beginner: {beginnerTopics.Count} topics");
            Console.WriteLine($"  • Intermediate: {intermediateTopics.Count} topics");
            Console.WriteLine($"  • Advanced: {advancedTopics.Count} topics");
            
            // Exercise types
            var exerciseTypes = Enum.GetValues<ExerciseType>();
            Console.WriteLine($"\n🎯 Exercise Types: {exerciseTypes.Length} types");
            foreach (var type in exerciseTypes)
            {
                Console.WriteLine($"  • {type}: {GetTypeDescription(type)}");
            }
            
            // Estimated generation capabilities
            Console.WriteLine("\n⚡ Generation Capabilities:");
            Console.WriteLine($"  • Single Exercise: ~2-5 seconds");
            Console.WriteLine($"  • Learning Module: ~10-30 seconds");
            Console.WriteLine($"  • Full Curriculum: ~30-120 seconds");
            Console.WriteLine($"  • Concurrent Generation: Supported");
            
            // Output formats
            Console.WriteLine("\n📁 Output Formats:");
            Console.WriteLine("  • Project files (.csproj)");
            Console.WriteLine("  • Source code (.cs)");
            Console.WriteLine("  • Unit tests (xUnit)");
            Console.WriteLine("  • Documentation (Markdown)");
            Console.WriteLine("  • Solution files (.sln)");
            
            PauseForUser();
        }

        private void CustomExerciseBuilder()
        {
            Console.WriteLine("\n🔧 Custom Exercise Builder");
            Console.WriteLine("Build your own exercise with custom parameters...\n");
            
            var config = new ExerciseConfiguration();
            
            // Skill level selection
            config.Level = SelectSkillLevel();
            
            // Topic selection
            config.Topic = SelectTopic(config.Level);
            
            // Exercise type selection
            config.Type = SelectExerciseType();
            
            // Context input
            Console.Write("Enter context/scenario (e.g., 'Banking system', 'E-commerce'): ");
            config.Context = Console.ReadLine() ?? "General";
            
            // Time estimation
            Console.Write("Estimated time in minutes (or press Enter for auto): ");
            string? timeInput = Console.ReadLine();
            if (int.TryParse(timeInput, out int minutes))
            {
                config.EstimatedMinutes = minutes;
            }
            
            // Additional options
            Console.Write("Include unit tests? (y/n): ");
            config.IncludeUnitTests = Console.ReadLine()?.ToLower().StartsWith("y") ?? true;
            
            Console.Write("Include extension challenges? (y/n): ");
            config.IncludeExtensionChallenges = Console.ReadLine()?.ToLower().StartsWith("y") ?? true;
            
            // Generate exercise
            Console.WriteLine("\n🔄 Generating custom exercise...");
            
            try
            {
                var exercise = _generator.GenerateExercise(config);
                
                // Display result
                DisplayExerciseDetails(exercise);
                
                // Export option
                Console.Write("\nExport this exercise? (y/n): ");
                if (Console.ReadLine()?.ToLower().StartsWith("y") ?? false)
                {
                    _generator.ExportExercise(exercise, _outputPath);
                    Console.WriteLine($"✅ Exercise exported to: {_outputPath}");
                }
            }
            catch (Exception ex) when (ex is NotSupportedException || ex.Message.Contains("not supported") || ex.Message.Contains("validation failed"))
            {
                Console.WriteLine("⚠️ No predefined example found for this configuration.");
                Console.WriteLine("💡 Generating AI prompt instead...\n");
                
                // Usar el generador expandido para crear un prompt
                var expandedGenerator = new ExpandedExerciseGenerator();
                var mentorConfig = new MentorConfiguration
                {
                    MentorName = "Assistant Mentor",
                    CourseName = "Custom .NET Course",
                    TeachingStyle = "Practical",
                    PreferredExampleDomain = config.Context
                };
                
                var promptResult = expandedGenerator.GenerateAIPrompt(config, mentorConfig);
                
                Console.WriteLine("📋 AI Prompt Generated Successfully!");
                Console.WriteLine($"📊 Prompt Length: {promptResult.CompletePrompt.Length:N0} characters");
                Console.WriteLine($"🔍 Parameters: {promptResult.PromptParameters.Count}");
                Console.WriteLine($"✅ Validation Criteria: {promptResult.ValidationCriteria.Count}");
                Console.WriteLine();
                
                Console.WriteLine("🎯 First 500 characters of the prompt:");
                Console.WriteLine("─" + new string('─', 50));
                var preview = promptResult.CompletePrompt.Length > 500 
                    ? promptResult.CompletePrompt.Substring(0, 500) + "..."
                    : promptResult.CompletePrompt;
                Console.WriteLine(preview);
                
                Console.WriteLine("\n💾 Export prompt to file? (y/n): ");
                if (Console.ReadLine()?.ToLower().StartsWith("y") ?? false)
                {
                    var fileName = $"ai-prompt-{config.Level}-{config.Topic}-{config.Type}-{DateTime.Now:yyyyMMdd-HHmmss}.txt";
                    var filePath = Path.Combine(_outputPath, fileName);
                    Directory.CreateDirectory(_outputPath);
                    File.WriteAllText(filePath, promptResult.CompletePrompt);
                    Console.WriteLine($"✅ Prompt exported to: {filePath}");
                    Console.WriteLine("💡 Copy this prompt and use it with Claude or another AI to generate the complete exercise.");
                }
            }
            
            PauseForUser();
        }

        private void BrowseGeneratedExercises()
        {
            Console.WriteLine("\n📁 Browse Generated Exercises");
            
            if (!Directory.Exists(_outputPath))
            {
                Console.WriteLine("❌ No exercises have been generated yet.");
                PauseForUser();
                return;
            }
            
            var directories = Directory.GetDirectories(_outputPath);
            if (directories.Length == 0)
            {
                Console.WriteLine("❌ No exercises found in output directory.");
                PauseForUser();
                return;
            }
            
            Console.WriteLine($"\n📂 Found {directories.Length} generated exercises/modules:");
            
            for (int i = 0; i < directories.Length; i++)
            {
                var dirName = Path.GetFileName(directories[i]);
                var files = Directory.GetFiles(directories[i]);
                Console.WriteLine($"{i + 1}. {dirName} ({files.Length} files)");
            }
            
            Console.Write($"\nSelect exercise to view (1-{directories.Length}, or 0 to return): ");
            if (int.TryParse(Console.ReadLine(), out int selection) && 
                selection > 0 && selection <= directories.Length)
            {
                ShowExerciseDetails(directories[selection - 1]);
            }
        }

        #endregion

        #region Helper Methods

        private Exercise GenerateAndDisplay(string title, ExerciseConfiguration config)
        {
            Console.WriteLine($"🔄 Generating: {title}");
            
            try
            {
                var exercise = _generator.GenerateExercise(config);
                
                Console.WriteLine($"   ✅ {exercise.Title}");
                Console.WriteLine($"      Level: {exercise.Level} | Topic: {exercise.Topic} | Type: {exercise.Type}");
                Console.WriteLine($"      Duration: {exercise.EstimatedMinutes} minutes");
                Console.WriteLine($"      Learning Objectives: {exercise.LearningObjectives.Count}");
                Console.WriteLine();
                
                return exercise;
            }
            catch (Exception)
            {
                Console.WriteLine($"   ⚠️  Exercise validation failed. Generating AI prompt instead...");
                
                // Usar sistema expandido para generar prompt de IA
                var expandedGenerator = new ExpandedExerciseGenerator();
                
                try
                {
                    var exercise = expandedGenerator.GenerateExercise(config);
                    
                    Console.WriteLine($"   ✅ {exercise.Title}");
                    Console.WriteLine($"      Level: {exercise.Level} | Topic: {exercise.Topic} | Type: {exercise.Type}");
                    Console.WriteLine($"      Duration: {exercise.EstimatedMinutes} minutes");
                    Console.WriteLine($"      Source: AI Prompt Generated");
                    Console.WriteLine();
                    
                    return exercise;
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine($"   🤖 Generating AI prompt for: {title}");
                    
                    // Crear ejercicio básico con prompt de IA
                    var aiExercise = CreateAIPromptExercise(title, config);
                    Console.WriteLine($"      ✅ AI Prompt ready for: {aiExercise.Title}");
                    Console.WriteLine($"      Level: {aiExercise.Level} | Duration: {aiExercise.EstimatedMinutes} min");
                    Console.WriteLine();
                    
                    return aiExercise;
                }
            }
        }

        private void DisplayExerciseDetails(Exercise exercise)
        {
            Console.WriteLine($"\n📋 Generated Exercise Details:");
            Console.WriteLine($"   Title: {exercise.Title}");
            Console.WriteLine($"   Level: {exercise.Level}");
            Console.WriteLine($"   Topic: {exercise.Topic}");
            Console.WriteLine($"   Type: {exercise.Type}");
            Console.WriteLine($"   Duration: {exercise.EstimatedMinutes} minutes");
            Console.WriteLine($"   Learning Objectives: {exercise.LearningObjectives.Count}");
            Console.WriteLine($"   Prerequisites: {exercise.Prerequisites.Count}");
            Console.WriteLine($"   Success Criteria: {exercise.SuccessCriteria.Count}");
            Console.WriteLine($"   Extension Challenges: {exercise.ExtensionChallenges.Count}");
            Console.WriteLine($"   Common Pitfalls: {exercise.CommonPitfalls.Count}");
            Console.WriteLine($"   Has Unit Tests: {!string.IsNullOrWhiteSpace(exercise.UnitTestCode)}");
        }

        private SkillLevel SelectSkillLevel()
        {
            var levels = Enum.GetValues<SkillLevel>();
            
            Console.WriteLine("Select skill level:");
            for (int i = 0; i < levels.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {levels[i]}");
            }
            
            Console.Write("Enter choice (1-3): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && 
                choice > 0 && choice <= levels.Length)
            {
                return levels[choice - 1];
            }
            
            return SkillLevel.Beginner; // Default
        }

        private TopicArea SelectTopic(SkillLevel level)
        {
            var allTopics = Enum.GetValues<TopicArea>();
            var availableTopics = allTopics.Where(t => CanGenerateForLevel(t, level)).ToArray();
            
            Console.WriteLine($"\nSelect topic for {level} level:");
            for (int i = 0; i < availableTopics.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {availableTopics[i]}");
            }
            
            Console.Write($"Enter choice (1-{availableTopics.Length}): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && 
                choice > 0 && choice <= availableTopics.Length)
            {
                return availableTopics[choice - 1];
            }
            
            return availableTopics[0]; // Default to first available
        }

        private ExerciseType SelectExerciseType()
        {
            var types = Enum.GetValues<ExerciseType>();
            
            Console.WriteLine("\nSelect exercise type:");
            for (int i = 0; i < types.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {types[i]} - {GetTypeDescription(types[i])}");
            }
            
            Console.Write($"Enter choice (1-{types.Length}): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && 
                choice > 0 && choice <= types.Length)
            {
                return types[choice - 1];
            }
            
            return ExerciseType.Implementation; // Default
        }

        private void ShowExerciseDetails(string exercisePath)
        {
            Console.WriteLine($"\n📁 Exercise: {Path.GetFileName(exercisePath)}");
            Console.WriteLine("=" + new string('=', 50));
            
            var files = Directory.GetFiles(exercisePath);
            Console.WriteLine($"Files ({files.Length}):");
            
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileSize = new FileInfo(file).Length;
                Console.WriteLine($"  • {fileName} ({fileSize:N0} bytes)");
            }
            
            // Show README if available
            var readmePath = Path.Combine(exercisePath, "README.md");
            if (File.Exists(readmePath))
            {
                Console.WriteLine("\n📄 Exercise Description:");
                var readmeContent = File.ReadAllText(readmePath);
                var lines = readmeContent.Split('\n').Take(10); // First 10 lines
                foreach (var line in lines)
                {
                    Console.WriteLine($"   {line}");
                }
                if (readmeContent.Split('\n').Length > 10)
                {
                    Console.WriteLine("   ... (see README.md for full details)");
                }
            }
            
            PauseForUser();
        }

        private bool CanGenerateForLevel(TopicArea topic, SkillLevel level)
        {
            // Beginner topics
            var beginnerTopics = new[]
            {
                TopicArea.CSharpFundamentals,
                TopicArea.ControlStructures,
                TopicArea.MethodsAndParameters,
                TopicArea.BasicOOP,
                TopicArea.Collections,
                TopicArea.ExceptionHandling
            };

            // Intermediate and advanced topics
            var intermediateTopics = new[]
            {
                TopicArea.AdvancedOOP,
                TopicArea.LINQ,
                TopicArea.Generics,
                TopicArea.DelegatesAndEvents,
                TopicArea.FileIO,
                TopicArea.UnitTesting
            };

            var advancedTopics = new[]
            {
                TopicArea.DesignPatterns,
                TopicArea.EntityFramework,
                TopicArea.AspNetCore,
                TopicArea.AsyncProgramming,
                TopicArea.PerformanceOptimization,
                TopicArea.Microservices
            };

            return level switch
            {
                SkillLevel.Beginner => beginnerTopics.Contains(topic),
                SkillLevel.Intermediate => beginnerTopics.Contains(topic) || intermediateTopics.Contains(topic),
                SkillLevel.Advanced => advancedTopics.Contains(topic) || intermediateTopics.Contains(topic),
                _ => false
            };
        }

        private string GetTypeDescription(ExerciseType type)
        {
            return type switch
            {
                ExerciseType.Implementation => "Build from scratch",
                ExerciseType.Refactoring => "Improve existing code",
                ExerciseType.DebugFix => "Find and fix issues",
                ExerciseType.Extension => "Add new features",
                ExerciseType.Design => "Architecture planning",
                ExerciseType.Performance => "Optimize performance",
                ExerciseType.Testing => "Write comprehensive tests",
                _ => "General exercise"
            };
        }

        private Exercise CreateAIPromptExercise(string title, ExerciseConfiguration config)
        {
            var expandedGenerator = new ExpandedExerciseGenerator();
            var mentorConfig = new MentorConfiguration
            {
                MentorName = "Assistant Mentor",
                CourseName = ".NET Course",
                TeachingStyle = "Practical",
                PreferredExampleDomain = config.Context ?? "General"
            };
            
            try
            {
                var promptResult = expandedGenerator.GenerateAIPrompt(config, mentorConfig);
                
                // Create a basic exercise structure with the AI prompt
                return new Exercise
                {
                    Title = title,
                    Level = config.Level,
                    Topic = config.Topic,
                    Type = config.Type,
                    EstimatedMinutes = config.EstimatedMinutes,
                    ProblemStatement = "AI Generated Exercise - Use the provided prompt with Claude AI",
                    Description = $"This exercise configuration requires AI generation. Use the following prompt:\n\n{promptResult.CompletePrompt.Substring(0, Math.Min(500, promptResult.CompletePrompt.Length))}...",
                    StarterCode = "// Generated by AI - See full prompt in description",
                    SolutionCode = "// Generated by AI - See full prompt in description", 
                    LearningObjectives = new List<string> { "Apply AI-generated learning objectives", "Practice problem-solving skills" },
                    Prerequisites = new List<string> { "Basic understanding of the topic area" },
                    SuccessCriteria = new List<string> { "Successfully complete the AI-generated exercise" },
                    ExtensionChallenges = new List<string>(),
                    CommonPitfalls = new List<string>(),
                    UnitTestCode = "// Tests will be generated by AI"
                };
            }
            catch (Exception)
            {
                // Fallback to a very basic exercise structure
                return new Exercise
                {
                    Title = title,
                    Level = config.Level,
                    Topic = config.Topic,
                    Type = config.Type,
                    EstimatedMinutes = config.EstimatedMinutes,
                    ProblemStatement = "Exercise configuration not found - Create manually",
                    Description = $"No predefined exercise or AI prompt could be generated for: {config.Level} {config.Topic} {config.Type}",
                    StarterCode = "// Please create your own starting code",
                    SolutionCode = "// Please create your own solution",
                    LearningObjectives = new List<string> { $"Learn {config.Topic} concepts", $"Practice {config.Type} skills" },
                    Prerequisites = new List<string> { "Basic programming knowledge" },
                    SuccessCriteria = new List<string> { "Complete the exercise successfully" },
                    ExtensionChallenges = new List<string>(),
                    CommonPitfalls = new List<string>(),
                    UnitTestCode = ""
                };
            }
        }

        private void PauseForUser()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        #endregion
    }
}