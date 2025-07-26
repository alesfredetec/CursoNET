using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Interface para repositorio de ejercicios - abstrae la fuente de datos
    /// </summary>
    public interface IExerciseRepository
    {
        /// <summary>
        /// Busca un ejercicio específico por criterios
        /// </summary>
        Task<ExerciseJsonSchema?> FindExerciseAsync(SkillLevel level, TopicArea topic, ExerciseType type, string context = "");

        /// <summary>
        /// Obtiene todos los ejercicios que coinciden con los criterios
        /// </summary>
        Task<IEnumerable<ExerciseJsonSchema>> FindExercisesAsync(SkillLevel? level = null, TopicArea? topic = null, ExerciseType? type = null);

        /// <summary>
        /// Lista todos los ejercicios disponibles
        /// </summary>
        Task<IEnumerable<ExerciseJsonSchema>> ListAllExercisesAsync();

        /// <summary>
        /// Obtiene un ejercicio por su ID único
        /// </summary>
        Task<ExerciseJsonSchema?> GetExerciseByIdAsync(string id);

        /// <summary>
        /// Verifica si existe un ejercicio con los criterios especificados
        /// </summary>
        Task<bool> ExistsAsync(SkillLevel level, TopicArea topic, ExerciseType type, string context = "");

        /// <summary>
        /// Obtiene estadísticas del repositorio
        /// </summary>
        Task<ExerciseRepositoryStats> GetStatsAsync();

        /// <summary>
        /// Guarda un nuevo ejercicio (si el repositorio soporta escritura)
        /// </summary>
        Task<bool> SaveExerciseAsync(ExerciseJsonSchema exercise);

        /// <summary>
        /// Elimina un ejercicio (si el repositorio soporta eliminación)
        /// </summary>
        Task<bool> DeleteExerciseAsync(string id);

        /// <summary>
        /// Actualiza la caché de ejercicios (si aplica)
        /// </summary>
        Task RefreshCacheAsync();
    }

    /// <summary>
    /// Estadísticas del repositorio de ejercicios
    /// </summary>
    public class ExerciseRepositoryStats
    {
        public int TotalExercises { get; set; }
        public Dictionary<SkillLevel, int> ExercisesByLevel { get; set; } = new();
        public Dictionary<TopicArea, int> ExercisesByTopic { get; set; } = new();
        public Dictionary<ExerciseType, int> ExercisesByType { get; set; } = new();
        public DateTime LastUpdated { get; set; }
        public string RepositoryType { get; set; } = "";
    }

    /// <summary>
    /// Excepción específica para errores del repositorio de ejercicios
    /// </summary>
    public class ExerciseRepositoryException : Exception
    {
        public ExerciseRepositoryException(string message) : base(message) { }
        public ExerciseRepositoryException(string message, Exception innerException) : base(message, innerException) { }
    }
}