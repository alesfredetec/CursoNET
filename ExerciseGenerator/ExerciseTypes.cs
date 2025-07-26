using System;
using System.Collections.Generic;

namespace CursoNET.ExerciseGenerator
{
    #region Enums y Tipos Base

    public enum SkillLevel
    {
        Beginner,      // Junior developers, new to .NET
        Intermediate,  // Some experience, learning patterns
        Advanced       // Experienced, architecture focus
    }

    public enum ExerciseType
    {
        Refactoring,     // Transform bad code to good code
        Implementation,  // Build functionality from requirements
        DebugFix,       // Find and correct issues
        Extension,      // Add features to existing code
        Design,         // Create architecture
        Performance,    // Optimize code
        Testing         // Write comprehensive tests
    }

    public enum TopicArea
    {
        // Beginner Topics
        CSharpFundamentals,
        ControlStructures,
        MethodsAndParameters,
        BasicOOP,
        Collections,
        ExceptionHandling,

        // Intermediate Topics
        AdvancedOOP,
        LINQ,
        Generics,
        DelegatesAndEvents,
        FileIO,
        UnitTesting,

        // Advanced Topics
        DesignPatterns,
        EntityFramework,
        AspNetCore,
        AsyncProgramming,
        PerformanceOptimization,
        Microservices
    }

    #endregion

    #region Configuraciones

    public class ExerciseConfiguration
    {
        public SkillLevel Level { get; set; }
        public TopicArea Topic { get; set; }
        public ExerciseType Type { get; set; }
        public string Context { get; set; } = "General";
        public int EstimatedMinutes { get; set; } = 30;
        public bool IncludeUnitTests { get; set; } = true;
        public bool IncludeExtensionChallenges { get; set; } = true;
        public string OutputPath { get; set; } = "./generated-exercises/";
    }

    public class Exercise
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> LearningObjectives { get; set; } = new();
        public List<string> Prerequisites { get; set; } = new();
        public string ProblemStatement { get; set; }
        public List<string> TechnicalRequirements { get; set; } = new();
        public List<string> SuccessCriteria { get; set; } = new();
        public string StarterCode { get; set; }
        public string SolutionCode { get; set; }
        public string UnitTestCode { get; set; }
        public List<string> ExtensionChallenges { get; set; } = new();
        public List<string> CommonPitfalls { get; set; } = new();
        public SkillLevel Level { get; set; }
        public TopicArea Topic { get; set; }
        public ExerciseType Type { get; set; }
        public int EstimatedMinutes { get; set; }
    }

    #endregion

    #region Configuración del Mentor (para el sistema expandido)

    public class MentorConfiguration
    {
        public string MentorName { get; set; } = "Mentor";
        public string CourseName { get; set; } = "Curso .NET";
        public string StudentLevel { get; set; } = "Principiante";
        public List<string> CourseObjectives { get; set; } = new();
        public List<string> PrerequisiteKnowledge { get; set; } = new();
        public string TeachingStyle { get; set; } = "Práctico"; // Práctico, Teórico, Mixto
        public int SessionDurationMinutes { get; set; } = 45;
        public bool IncludeRealWorldExamples { get; set; } = true;
        public bool IncludeUnitTests { get; set; } = true;
        public bool IncludePerformanceConsiderations { get; set; } = false;
        public string PreferredExampleDomain { get; set; } = "General"; // Banking, ECommerce, Healthcare, etc.
        public List<string> ForbiddenTopics { get; set; } = new();
        public Dictionary<string, string> CustomRequirements { get; set; } = new();
    }

    #endregion

    #region Resultados de IA

    public class AIPromptRequest
    {
        public ExerciseConfiguration ExerciseConfig { get; set; }
        public MentorConfiguration MentorConfig { get; set; }
        public bool HasExistingExamples { get; set; }
        public List<object> RelatedExamples { get; set; } = new(); // Usando object para evitar dependencias circulares
        public string SpecialInstructions { get; set; }
    }

    public class AIPromptResult
    {
        public string CompletePrompt { get; set; }
        public string SystemMessage { get; set; }
        public string UserMessage { get; set; }
        public Dictionary<string, object> PromptParameters { get; set; } = new();
        public List<string> ValidationCriteria { get; set; } = new();
    }

    #endregion
}