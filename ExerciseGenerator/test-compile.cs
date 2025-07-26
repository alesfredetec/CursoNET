// Archivo de prueba para verificar que todas las clases se pueden instanciar correctamente

using System;

namespace CursoNET.ExerciseGenerator
{
    public class TestCompile
    {
        public static void TestInstantiation()
        {
            // Probar tipos básicos
            var config = new ExerciseConfiguration();
            var exercise = new Exercise();
            var mentorConfig = new MentorConfiguration();
            var promptRequest = new AIPromptRequest();
            var promptResult = new AIPromptResult();
            
            // Probar generadores
            var generator = new DotNetExerciseGenerator();
            var expandedGenerator = new ExpandedExerciseGenerator();
            var promptGenerator = new AdvancedPromptGenerator();
            
            Console.WriteLine("✅ Todas las clases se pueden instanciar correctamente");
        }
    }
}