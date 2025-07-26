using System;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Interface for topic-specific exercise generators
    /// Each topic area implements this interface to provide specialized exercise generation
    /// </summary>
    public interface IExerciseTopicGenerator
    {
        /// <summary>
        /// Generates an exercise for this specific topic area
        /// </summary>
        /// <param name="config">Configuration parameters for the exercise</param>
        /// <returns>A complete exercise ready for use</returns>
        Exercise GenerateExercise(ExerciseConfiguration config);

        /// <summary>
        /// Gets the topics that this generator can handle
        /// </summary>
        TopicArea SupportedTopic { get; }

        /// <summary>
        /// Validates that the configuration is appropriate for this topic
        /// </summary>
        /// <param name="config">Configuration to validate</param>
        /// <returns>True if configuration is valid for this topic</returns>
        bool ValidateConfiguration(ExerciseConfiguration config);
    }
}