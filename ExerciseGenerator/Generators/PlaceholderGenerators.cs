using System;
using System.Collections.Generic;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Placeholder generators for remaining topics - these can be expanded later
    /// Each generator follows the same pattern as CSharpFundamentalsGenerator and DesignPatternsGenerator
    /// </summary>

    #region Beginner Level Generators

    public class ControlStructuresGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.ControlStructures;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Grade Calculator with Control Structures",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.ControlStructures,
                Type = config.Type,
                EstimatedMinutes = 30,
                Description = "Practice using if-else statements, switch expressions, loops, and conditional logic to build a grade calculator.",
                LearningObjectives = new List<string>
                {
                    "Use if-else statements for conditional logic",
                    "Implement switch expressions for multiple conditions",
                    "Apply for and while loops for repetitive tasks",
                    "Combine different control structures effectively"
                },
                StarterCode = GenerateControlStructuresStarter(),
                SolutionCode = GenerateControlStructuresSolution()
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.ControlStructures && config.Level == SkillLevel.Beginner;
        }

        private string GenerateControlStructuresStarter()
        {
            return @"// TODO: Implement grade calculator using various control structures
using System;

namespace ControlStructures.GradeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Implement grade calculator
            // Requirements:
            // 1. Use if-else for grade boundaries (A: 90+, B: 80-89, etc.)
            // 2. Use switch expression for letter grade descriptions
            // 3. Use loops to process multiple students
            // 4. Handle invalid input gracefully
        }
    }
}";
        }

        private string GenerateControlStructuresSolution()
        {
            return @"using System;
using System.Collections.Generic;

namespace ControlStructures.GradeCalculator
{
    class GradeCalculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""=== Grade Calculator ==="" );
            
            bool continueProcessing = true;
            var studentGrades = new List<(string Name, double Score, char Grade)>();
            
            while (continueProcessing)
            {
                Console.Write(""Enter student name (or 'quit' to finish): "");
                string name = Console.ReadLine();
                
                if (name?.ToLower() == ""quit"")
                {
                    continueProcessing = false;
                    continue;
                }
                
                Console.Write($""Enter grade for {name}: "");
                if (double.TryParse(Console.ReadLine(), out double score))
                {
                    char letterGrade = CalculateLetterGrade(score);
                    studentGrades.Add((name, score, letterGrade));
                    
                    string description = GetGradeDescription(letterGrade);
                    Console.WriteLine($""{name}: {score:F1} - {letterGrade} ({description})"");
                }
                else
                {
                    Console.WriteLine(""Invalid score entered."");
                }
            }
            
            DisplaySummary(studentGrades);
        }
        
        static char CalculateLetterGrade(double score)
        {
            // Using if-else for grade boundaries
            if (score >= 97)
                return 'A';
            else if (score >= 93)
                return 'A';
            else if (score >= 90)
                return 'A';
            else if (score >= 87)
                return 'B';
            else if (score >= 83)
                return 'B';
            else if (score >= 80)
                return 'B';
            else if (score >= 77)
                return 'C';
            else if (score >= 73)
                return 'C';
            else if (score >= 70)
                return 'C';
            else if (score >= 67)
                return 'D';
            else if (score >= 60)
                return 'D';
            else
                return 'F';
        }
        
        static string GetGradeDescription(char grade)
        {
            // Using switch expression
            return grade switch
            {
                'A' => ""Excellent"",
                'B' => ""Good"",
                'C' => ""Satisfactory"",
                'D' => ""Needs Improvement"",
                'F' => ""Failing"",
                _ => ""Invalid Grade""
            };
        }
        
        static void DisplaySummary(List<(string Name, double Score, char Grade)> grades)
        {
            if (grades.Count == 0) return;
            
            Console.WriteLine(""\\n=== Class Summary ==="");
            
            // Calculate statistics using loops
            double total = 0;
            int aCount = 0, bCount = 0, cCount = 0, dCount = 0, fCount = 0;
            
            foreach (var (name, score, grade) in grades)
            {
                total += score;
                
                // Count grades using switch expression
                _ = grade switch
                {
                    'A' => aCount++,
                    'B' => bCount++,
                    'C' => cCount++,
                    'D' => dCount++,
                    'F' => fCount++,
                    _ => 0
                };
            }
            
            double average = total / grades.Count;
            Console.WriteLine($""Class Average: {average:F2}"");
            Console.WriteLine($""Grade Distribution: A:{aCount} B:{bCount} C:{cCount} D:{dCount} F:{fCount}"");
        }
    }
}";
        }
    }

    public class MethodsParametersGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.MethodsAndParameters;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Math Library with Method Overloading",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.MethodsAndParameters,
                Type = config.Type,
                EstimatedMinutes = 35,
                Description = "Create a comprehensive math library demonstrating method overloading, optional parameters, ref/out parameters, and params arrays.",
                LearningObjectives = new List<string>
                {
                    "Create methods with different parameter types",
                    "Implement method overloading effectively",
                    "Use ref, out, and params parameters",
                    "Design clean, reusable method signatures"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.MethodsAndParameters && config.Level == SkillLevel.Beginner;
        }
    }

    public class BasicOOPGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.BasicOOP;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Bank Account Management System",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.BasicOOP,
                Type = config.Type,
                EstimatedMinutes = 45,
                Description = "Build a bank account system using classes, encapsulation, constructors, properties, and basic inheritance.",
                LearningObjectives = new List<string>
                {
                    "Create classes with proper encapsulation",
                    "Implement constructors and properties",
                    "Use inheritance for specialized account types",
                    "Apply basic object-oriented principles"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.BasicOOP && config.Level == SkillLevel.Beginner;
        }
    }

    public class CollectionsGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.Collections;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Student Management System with Collections",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.Collections,
                Type = config.Type,
                EstimatedMinutes = 40,
                Description = "Use List<T>, Dictionary<T>, HashSet<T>, and other collections to manage student data efficiently.",
                LearningObjectives = new List<string>
                {
                    "Choose appropriate collection types for different scenarios",
                    "Perform CRUD operations on collections",
                    "Understand performance characteristics of different collections",
                    "Implement search and sorting functionality"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.Collections && config.Level == SkillLevel.Beginner;
        }
    }

    public class ExceptionHandlingGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.ExceptionHandling;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Robust File Processing with Exception Handling",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.ExceptionHandling,
                Type = config.Type,
                EstimatedMinutes = 30,
                Description = "Build a file processing application that gracefully handles various types of exceptions.",
                LearningObjectives = new List<string>
                {
                    "Use try-catch-finally blocks effectively",
                    "Handle specific exception types appropriately",
                    "Create custom exception classes",
                    "Implement proper error logging and user feedback"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.ExceptionHandling && config.Level == SkillLevel.Beginner;
        }
    }

    #endregion

    #region Intermediate Level Generators

    public class AdvancedOOPGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.AdvancedOOP;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "E-commerce System with Advanced OOP",
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.AdvancedOOP,
                Type = config.Type,
                EstimatedMinutes = 60,
                Description = "Design an e-commerce system using abstract classes, interfaces, polymorphism, and composition.",
                LearningObjectives = new List<string>
                {
                    "Design systems using abstract classes and interfaces",
                    "Implement polymorphism effectively",
                    "Use composition over inheritance where appropriate",
                    "Apply SOLID principles in system design"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.AdvancedOOP && config.Level >= SkillLevel.Intermediate;
        }
    }

    public class LINQGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.LINQ;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Data Analysis with LINQ",
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.LINQ,
                Type = config.Type,
                EstimatedMinutes = 50,
                Description = "Perform complex data queries and transformations using LINQ methods and query syntax.",
                LearningObjectives = new List<string>
                {
                    "Master LINQ query syntax and method syntax",
                    "Perform complex data filtering, grouping, and aggregation",
                    "Chain LINQ operations for complex transformations",
                    "Optimize LINQ queries for performance"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.LINQ && config.Level >= SkillLevel.Intermediate;
        }
    }

    public class GenericsGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.Generics;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Generic Data Structures and Algorithms",
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.Generics,
                Type = config.Type,
                EstimatedMinutes = 45,
                Description = "Create generic collections, algorithms, and utility classes with type constraints.",
                LearningObjectives = new List<string>
                {
                    "Create generic classes and methods",
                    "Apply generic constraints effectively",
                    "Understand variance (covariance and contravariance)",
                    "Build reusable, type-safe components"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.Generics && config.Level >= SkillLevel.Intermediate;
        }
    }

    public class DelegatesEventsGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.DelegatesAndEvents;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Event-Driven Order Processing System",
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.DelegatesAndEvents,
                Type = config.Type,
                EstimatedMinutes = 55,
                Description = "Build an order processing system using delegates, events, and lambda expressions.",
                LearningObjectives = new List<string>
                {
                    "Implement delegates and events for loose coupling",
                    "Use lambda expressions and anonymous methods",
                    "Create event-driven architectures",
                    "Handle event subscription and unsubscription properly"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.DelegatesAndEvents && config.Level >= SkillLevel.Intermediate;
        }
    }

    public class FileIOGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.FileIO;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Document Management System",
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.FileIO,
                Type = config.Type,
                EstimatedMinutes = 40,
                Description = "Create a document management system with file operations, serialization, and data persistence.",
                LearningObjectives = new List<string>
                {
                    "Perform file and directory operations safely",
                    "Implement JSON and XML serialization",
                    "Handle file streams and buffering",
                    "Create robust file processing pipelines"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.FileIO && config.Level >= SkillLevel.Intermediate;
        }
    }

    public class UnitTestingGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.UnitTesting;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "TDD Shopping Cart Implementation",
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.UnitTesting,
                Type = config.Type,
                EstimatedMinutes = 50,
                Description = "Implement a shopping cart using Test-Driven Development with xUnit, mocking, and comprehensive test coverage.",
                LearningObjectives = new List<string>
                {
                    "Practice Test-Driven Development methodology",
                    "Write comprehensive unit tests with xUnit",
                    "Use mocking frameworks for dependencies",
                    "Achieve high test coverage and quality"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.UnitTesting && config.Level >= SkillLevel.Intermediate;
        }
    }

    #endregion

    #region Advanced Level Generators

    public class EntityFrameworkGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.EntityFramework;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Blog Platform with Entity Framework Core",
                Level = SkillLevel.Advanced,
                Topic = TopicArea.EntityFramework,
                Type = config.Type,
                EstimatedMinutes = 75,
                Description = "Build a complete blog platform using EF Core with Code First, migrations, relationships, and performance optimization.",
                LearningObjectives = new List<string>
                {
                    "Design database schema using Code First approach",
                    "Implement complex entity relationships",
                    "Optimize queries and handle N+1 problems",
                    "Use migrations for schema evolution"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.EntityFramework && config.Level == SkillLevel.Advanced;
        }
    }

    public class AspNetCoreGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.AspNetCore;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "RESTful API with ASP.NET Core",
                Level = SkillLevel.Advanced,
                Topic = TopicArea.AspNetCore,
                Type = config.Type,
                EstimatedMinutes = 90,
                Description = "Create a complete RESTful API with authentication, authorization, validation, and comprehensive error handling.",
                LearningObjectives = new List<string>
                {
                    "Build RESTful APIs following best practices",
                    "Implement JWT authentication and authorization",
                    "Use dependency injection and middleware effectively",
                    "Handle validation, logging, and error responses"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.AspNetCore && config.Level == SkillLevel.Advanced;
        }
    }

    public class AsyncProgrammingGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.AsyncProgramming;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "Concurrent Data Processing Pipeline",
                Level = SkillLevel.Advanced,
                Topic = TopicArea.AsyncProgramming,
                Type = config.Type,
                EstimatedMinutes = 70,
                Description = "Build a high-performance data processing pipeline using async/await, Task Parallel Library, and concurrent collections.",
                LearningObjectives = new List<string>
                {
                    "Master async/await patterns and best practices",
                    "Use Task Parallel Library for CPU-bound work",
                    "Handle cancellation and timeout scenarios",
                    "Avoid common async pitfalls and deadlocks"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.AsyncProgramming && config.Level == SkillLevel.Advanced;
        }
    }

    public class PerformanceOptimizationGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.PerformanceOptimization;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "High-Performance Data Processing Optimization",
                Level = SkillLevel.Advanced,
                Topic = TopicArea.PerformanceOptimization,
                Type = config.Type,
                EstimatedMinutes = 80,
                Description = "Optimize a slow data processing application using profiling, memory management, and algorithmic improvements.",
                LearningObjectives = new List<string>
                {
                    "Profile applications to identify bottlenecks",
                    "Optimize memory usage and garbage collection",
                    "Improve algorithmic complexity",
                    "Use performance-oriented data structures"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.PerformanceOptimization && config.Level == SkillLevel.Advanced;
        }
    }

    public class MicroservicesGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.Microservices;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            return new Exercise
            {
                Title = "E-commerce Microservices Architecture",
                Level = SkillLevel.Advanced,
                Topic = TopicArea.Microservices,
                Type = config.Type,
                EstimatedMinutes = 120,
                Description = "Design and implement a microservices-based e-commerce system with service communication, resilience patterns, and monitoring.",
                LearningObjectives = new List<string>
                {
                    "Design microservices architecture",
                    "Implement service-to-service communication",
                    "Apply resilience patterns (Circuit Breaker, Retry)",
                    "Set up monitoring and distributed tracing"
                }
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.Microservices && config.Level == SkillLevel.Advanced;
        }
    }

    #endregion
}