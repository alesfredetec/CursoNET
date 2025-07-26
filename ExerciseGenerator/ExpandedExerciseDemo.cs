using System;
using System.Collections.Generic;
using System.IO;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Demostración del sistema expandido de generación de ejercicios
    /// Incluye biblioteca amplia de ejemplos y generación avanzada de prompts
    /// </summary>
    public class ExpandedExerciseDemo
    {
        private readonly ExpandedExerciseGenerator _expandedGenerator;
        private readonly AdvancedPromptGenerator _promptGenerator;

        public ExpandedExerciseDemo()
        {
            _expandedGenerator = new ExpandedExerciseGenerator();
            _promptGenerator = new AdvancedPromptGenerator();
        }

        public static void RunExpandedDemo(string[] args)
        {
            var demo = new ExpandedExerciseDemo();
            
            Console.WriteLine("🚀 SISTEMA EXPANDIDO DE GENERACIÓN DE EJERCICIOS .NET");
            Console.WriteLine("=" + new string('=', 60));
            Console.WriteLine();
            
            bool continuar = true;
            while (continuar)
            {
                demo.MostrarMenuPrincipal();
                var opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        demo.DemostrarEjemplosExistentes();
                        break;
                    case "2":
                        demo.DemostrarGeneracionPrompts();
                        break;
                    case "3":
                        demo.DemostrarConfiguracionPersonalizada();
                        break;
                    case "4":
                        demo.DemostrarGeneracionCompleta();
                        break;
                    case "5":
                        demo.MostrarEjemploPromptCompleto();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("❌ Opción inválida. Intente de nuevo.");
                        break;
                }
                
                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            
            Console.WriteLine("👋 ¡Hasta luego! Gracias por probar el sistema.");
        }

        private void MostrarMenuPrincipal()
        {
            Console.WriteLine("📋 MENÚ PRINCIPAL");
            Console.WriteLine("─" + new string('─', 30));
            Console.WriteLine("1. 📚 Demostrar Ejercicios con Ejemplos Existentes");
            Console.WriteLine("2. 🤖 Demostrar Generación de Prompts para IA");
            Console.WriteLine("3. ⚙️  Configuración Personalizada de Mentor");
            Console.WriteLine("4. 🎯 Generación Completa de Ejercicio");
            Console.WriteLine("5. 📄 Mostrar Ejemplo de Prompt Completo");
            Console.WriteLine("0. 🚪 Salir");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }

        private void DemostrarEjemplosExistentes()
        {
            Console.WriteLine("📚 EJERCICIOS CON EJEMPLOS BEFORE/AFTER EXISTENTES");
            Console.WriteLine("=" + new string('=', 55));
            Console.WriteLine();

            // Ejercicio de refactoring para principiantes
            var configRefactoring = new ExerciseConfiguration
            {
                Level = SkillLevel.Beginner,
                Topic = TopicArea.CSharpFundamentals,
                Type = ExerciseType.Refactoring,
                Context = "Variables",
                EstimatedMinutes = 20,
                IncludeUnitTests = true,
                IncludeExtensionChallenges = true
            };

            Console.WriteLine("🔧 EJEMPLO 1: Refactoring de Variables (Principiante)");
            Console.WriteLine("─" + new string('─', 50));
            
            try
            {
                var exerciseRefactoring = _expandedGenerator.GenerateExercise(configRefactoring);
                MostrarResumenEjercicio(exerciseRefactoring);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error generado ejercicio: {ex.Message}");
                Console.WriteLine("💡 Esto es normal - este ejercicio usa ejemplos predefinidos en la biblioteca interna.");
            }

            Console.WriteLine();
            
            // Ejercicio de implementación para intermedios
            var configImplementation = new ExerciseConfiguration
            {
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.LINQ,
                Type = ExerciseType.Refactoring,
                Context = "LINQ",
                EstimatedMinutes = 30,
                IncludeUnitTests = true
            };

            Console.WriteLine("⚡ EJEMPLO 2: Refactoring a LINQ (Intermedio)");
            Console.WriteLine("─" + new string('─', 45));
            
            try
            {
                var exerciseLinq = _expandedGenerator.GenerateExercise(configImplementation);
                MostrarResumenEjercicio(exerciseLinq);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error generado ejercicio: {ex.Message}");
                Console.WriteLine("💡 Esto es normal - este ejercicio usa ejemplos predefinidos en la biblioteca interna.");
            }
        }

        private void DemostrarGeneracionPrompts()
        {
            Console.WriteLine("🤖 GENERACIÓN AVANZADA DE PROMPTS PARA IA");
            Console.WriteLine("=" + new string('=', 45));
            Console.WriteLine();

            // Configuración del ejercicio
            var exerciseConfig = new ExerciseConfiguration
            {
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.Collections,
                Type = ExerciseType.Implementation,
                Context = "E-Commerce",
                EstimatedMinutes = 45,
                IncludeUnitTests = true,
                IncludeExtensionChallenges = true
            };

            // Configuración del mentor
            var mentorConfig = new MentorConfiguration
            {
                MentorName = "Ana García",
                CourseName = "Desarrollo .NET Avanzado",
                StudentLevel = "Intermedio",
                TeachingStyle = "Práctico con ejemplos reales",
                IncludeRealWorldExamples = true,
                PreferredExampleDomain = "E-Commerce"
            };

            Console.WriteLine("📋 Configuración del Ejercicio:");
            Console.WriteLine($"   Nivel: {exerciseConfig.Level}");
            Console.WriteLine($"   Tema: {exerciseConfig.Topic}");
            Console.WriteLine($"   Tipo: {exerciseConfig.Type}");
            Console.WriteLine($"   Contexto: {exerciseConfig.Context}");
            Console.WriteLine($"   Duración: {exerciseConfig.EstimatedMinutes} minutos");
            Console.WriteLine();

            Console.WriteLine("👩‍🏫 Configuración del Mentor:");
            Console.WriteLine($"   Nombre: {mentorConfig.MentorName}");
            Console.WriteLine($"   Curso: {mentorConfig.CourseName}");
            Console.WriteLine($"   Estilo: {mentorConfig.TeachingStyle}");
            Console.WriteLine($"   Dominio: {mentorConfig.PreferredExampleDomain}");
            Console.WriteLine();

            Console.WriteLine("🎯 Generando prompt avanzado con Context Engineering...");
            
            var promptResult = _expandedGenerator.GenerateAIPrompt(exerciseConfig, mentorConfig);
            
            Console.WriteLine("✅ Prompt generado exitosamente!");
            Console.WriteLine();
            Console.WriteLine($"📊 Estadísticas del Prompt:");
            Console.WriteLine($"   Longitud total: {promptResult.CompletePrompt.Length:N0} caracteres");
            Console.WriteLine($"   Número de parámetros: {promptResult.PromptParameters.Count}");
            Console.WriteLine($"   Criterios de validación: {promptResult.ValidationCriteria.Count}");
            Console.WriteLine();

            Console.WriteLine("🔍 Primeros 500 caracteres del prompt:");
            Console.WriteLine("─" + new string('─', 50));
            var preview = promptResult.CompletePrompt.Length > 500 
                ? promptResult.CompletePrompt.Substring(0, 500) + "..."
                : promptResult.CompletePrompt;
            Console.WriteLine(preview);
            Console.WriteLine();
            
            Console.WriteLine("💡 Este prompt incluye:");
            Console.WriteLine("   ✅ Context Engineering avanzado");
            Console.WriteLine("   ✅ Chain of Thought estructurado");
            Console.WriteLine("   ✅ Persona del mentor definida");
            Console.WriteLine("   ✅ Herramientas conceptuales");
            Console.WriteLine("   ✅ Contexto del sistema de cursos");
            Console.WriteLine("   ✅ Framework de pensamiento");
            Console.WriteLine("   ✅ Reflexión meta-cognitiva");
        }

        private void DemostrarConfiguracionPersonalizada()
        {
            Console.WriteLine("⚙️ CONFIGURACIÓN PERSONALIZADA DE MENTOR Y CURSO");
            Console.WriteLine("=" + new string('=', 52));
            Console.WriteLine();

            // Crear configuraciones personalizadas
            var courseContext = new AdvancedPromptGenerator.CourseSystemContext
            {
                CourseName = "Bootcamp Full-Stack .NET",
                Institution = "TechAcademy Pro",
                CurrentModule = "Arquitectura de Microservicios",
                ModuleNumber = 8,
                TotalModules = 10,
                PreviousModules = new List<string>
                {
                    "Fundamentos de C#",
                    "ASP.NET Core Básico",
                    "Entity Framework",
                    "APIs RESTful",
                    "Autenticación y Autorización",
                    "Pruebas Unitarias",
                    "Docker y Containerización"
                },
                LearningOutcomes = new List<string>
                {
                    "Diseñar arquitecturas de microservicios escalables",
                    "Implementar patrones de comunicación entre servicios",
                    "Gestionar datos distribuidos efectivamente"
                }
            };

            var persona = new AdvancedPromptGenerator.PersonaConfiguration
            {
                Name = "Roberto Silva",
                Role = "Lead Solutions Architect",
                Expertise = "Microservices, Cloud Architecture, DevOps",
                YearsOfExperience = 12,
                TeachingPhilosophy = "Aprender resolviendo problemas reales de la industria",
                Specializations = new List<string>
                {
                    "Kubernetes",
                    "Azure Service Fabric",
                    "Event-Driven Architecture",
                    "CQRS y Event Sourcing"
                }
            };

            Console.WriteLine("🎓 Contexto del Curso Personalizado:");
            Console.WriteLine($"   📚 Curso: {courseContext.CourseName}");
            Console.WriteLine($"   🏫 Institución: {courseContext.Institution}");
            Console.WriteLine($"   📖 Módulo Actual: {courseContext.CurrentModule} ({courseContext.ModuleNumber}/{courseContext.TotalModules})");
            Console.WriteLine();
            
            Console.WriteLine("   📋 Módulos Completados:");
            foreach (var module in courseContext.PreviousModules)
            {
                Console.WriteLine($"      ✅ {module}");
            }
            Console.WriteLine();

            Console.WriteLine("👨‍💼 Persona del Mentor:");
            Console.WriteLine($"   👤 Nombre: {persona.Name}");
            Console.WriteLine($"   💼 Rol: {persona.Role}");
            Console.WriteLine($"   🎯 Expertise: {persona.Expertise}");
            Console.WriteLine($"   📅 Experiencia: {persona.YearsOfExperience} años");
            Console.WriteLine($"   🎓 Filosofía: {persona.TeachingPhilosophy}");
            Console.WriteLine();
            Console.WriteLine("   🔧 Especializaciones:");
            foreach (var spec in persona.Specializations)
            {
                Console.WriteLine($"      ⚡ {spec}");
            }
            Console.WriteLine();

            Console.WriteLine("💡 Esta configuración personalizada permite:");
            Console.WriteLine("   ✅ Ejercicios adaptados al contexto específico del curso");
            Console.WriteLine("   ✅ Prompts que reflejan la experiencia del mentor");
            Console.WriteLine("   ✅ Conexiones claras con módulos previos y futuros");
            Console.WriteLine("   ✅ Nivel de complejidad apropiado para la progresión");
        }

        private void DemostrarGeneracionCompleta()
        {
            Console.WriteLine("🎯 GENERACIÓN COMPLETA DE EJERCICIO");
            Console.WriteLine("=" + new string('=', 40));
            Console.WriteLine();

            Console.WriteLine("Seleccione el tipo de ejercicio a generar:");
            Console.WriteLine("1. 📚 Ejercicio con ejemplos existentes (Before/After)");
            Console.WriteLine("2. 🤖 Ejercicio con prompt para IA (sin ejemplos predefinidos)");
            Console.WriteLine();
            Console.Write("Opción (1 o 2): ");
            
            var option = Console.ReadLine();
            Console.WriteLine();

            if (option == "1")
            {
                GenerarEjercicioConEjemplos();
            }
            else if (option == "2")
            {
                GenerarEjercicioConPrompt();
            }
            else
            {
                Console.WriteLine("❌ Opción inválida.");
            }
        }

        private void GenerarEjercicioConEjemplos()
        {
            Console.WriteLine("📚 GENERANDO EJERCICIO CON EJEMPLOS EXISTENTES");
            Console.WriteLine("─" + new string('─', 48));
            Console.WriteLine();

            var config = new ExerciseConfiguration
            {
                Level = SkillLevel.Beginner,
                Topic = TopicArea.CSharpFundamentals,
                Type = ExerciseType.Refactoring,
                Context = "Variables",
                EstimatedMinutes = 25,
                IncludeUnitTests = true,
                IncludeExtensionChallenges = true,
                OutputPath = "./output/"
            };

            Console.WriteLine("⚙️ Configuración utilizada:");
            MostrarConfiguracion(config);
            Console.WriteLine();

            Console.WriteLine("🔄 Generando ejercicio...");
            
            try
            {
                var exercise = _expandedGenerator.GenerateExercise(config);
                
                Console.WriteLine("✅ Ejercicio generado exitosamente!");
                Console.WriteLine();
                
                MostrarEjercicioCompleto(exercise);
                
                // Exportar archivos
                Console.WriteLine("💾 ¿Desea exportar los archivos del ejercicio? (s/n): ");
                var exportar = Console.ReadLine()?.ToLower();
                
                if (exportar == "s" || exportar == "si" || exportar == "sí")
                {
                    var exportPath = Path.Combine(Directory.GetCurrentDirectory(), "exported-exercises");
                    Directory.CreateDirectory(exportPath);
                    
                    // Usar el generador principal para exportar
                    var mainGenerator = new DotNetExerciseGenerator();
                    mainGenerator.ExportExercise(exercise, exportPath);
                    
                    Console.WriteLine($"📁 Archivos exportados en: {exportPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generando ejercicio: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine("💡 Esto puede ocurrir si el ejemplo específico no está en la biblioteca.");
                Console.WriteLine("   Intente con diferentes combinaciones de Nivel/Tema/Tipo/Contexto.");
            }
        }

        private void GenerarEjercicioConPrompt()
        {
            Console.WriteLine("🤖 GENERANDO PROMPT PARA IA (Sin ejemplos predefinidos)");
            Console.WriteLine("─" + new string('─', 58));
            Console.WriteLine();

            var exerciseConfig = new ExerciseConfiguration
            {
                Level = SkillLevel.Advanced,
                Topic = TopicArea.DesignPatterns,
                Type = ExerciseType.Implementation,
                Context = "Banking",
                EstimatedMinutes = 60,
                IncludeUnitTests = true,
                IncludeExtensionChallenges = true
            };

            var mentorConfig = new MentorConfiguration
            {
                MentorName = "Dr. Patricia López",
                CourseName = "Arquitectura de Software Empresarial",
                StudentLevel = "Avanzado",
                TeachingStyle = "Teórico-Práctico",
                IncludeRealWorldExamples = true,
                IncludePerformanceConsiderations = true,
                PreferredExampleDomain = "Banking"
            };

            Console.WriteLine("⚙️ Configuración del ejercicio:");
            MostrarConfiguracion(exerciseConfig);
            Console.WriteLine();

            Console.WriteLine("👩‍🏫 Configuración del mentor:");
            Console.WriteLine($"   Nombre: {mentorConfig.MentorName}");
            Console.WriteLine($"   Curso: {mentorConfig.CourseName}");
            Console.WriteLine($"   Estilo: {mentorConfig.TeachingStyle}");
            Console.WriteLine($"   Dominio: {mentorConfig.PreferredExampleDomain}");
            Console.WriteLine();

            Console.WriteLine("🔄 Generando prompt avanzado...");
            
            var promptResult = _expandedGenerator.GenerateAIPrompt(exerciseConfig, mentorConfig);
            
            Console.WriteLine("✅ Prompt generado exitosamente!");
            Console.WriteLine();
            Console.WriteLine($"📊 Estadísticas:");
            Console.WriteLine($"   Longitud: {promptResult.CompletePrompt.Length:N0} caracteres");
            Console.WriteLine($"   Parámetros: {promptResult.PromptParameters.Count}");
            Console.WriteLine($"   Validaciones: {promptResult.ValidationCriteria.Count}");
            Console.WriteLine();

            Console.WriteLine("💾 ¿Desea guardar el prompt en un archivo? (s/n): ");
            var guardar = Console.ReadLine()?.ToLower();
            
            if (guardar == "s" || guardar == "si" || guardar == "sí")
            {
                var fileName = $"prompt-{exerciseConfig.Level}-{exerciseConfig.Topic}-{DateTime.Now:yyyyMMdd-HHmmss}.txt";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                
                File.WriteAllText(filePath, promptResult.CompletePrompt);
                Console.WriteLine($"📁 Prompt guardado en: {filePath}");
                Console.WriteLine();
                Console.WriteLine("💡 Puedes copiar este prompt y usarlo con Claude u otra IA para generar el ejercicio completo.");
            }
        }

        private void MostrarEjemploPromptCompleto()
        {
            Console.WriteLine("📄 EJEMPLO DE PROMPT COMPLETO CON CONTEXT ENGINEERING");
            Console.WriteLine("=" + new string('=', 58));
            Console.WriteLine();

            var exerciseConfig = new ExerciseConfiguration
            {
                Level = SkillLevel.Intermediate,
                Topic = TopicArea.LINQ,
                Type = ExerciseType.Refactoring,
                Context = "E-Commerce",
                EstimatedMinutes = 40
            };

            var mentorConfig = new MentorConfiguration
            {
                MentorName = "Carlos Mendoza",
                CourseName = "Desarrollo .NET Profesional",
                TeachingStyle = "Práctico"
            };

            var promptResult = _expandedGenerator.GenerateAIPrompt(exerciseConfig, mentorConfig);

            Console.WriteLine("🎯 Mostrando las primeras secciones del prompt generado:");
            Console.WriteLine("─" + new string('─', 55));
            Console.WriteLine();

            // Mostrar solo las primeras 2000 caracteres para demostración
            var promptPreview = promptResult.CompletePrompt.Length > 2000 
                ? promptResult.CompletePrompt.Substring(0, 2000) + "\n\n[... continúa con más secciones ...]"
                : promptResult.CompletePrompt;

            Console.WriteLine(promptPreview);
            Console.WriteLine();
            
            Console.WriteLine("🔍 Estructura del prompt completo:");
            Console.WriteLine("   🎯 Contexto del Sistema Educativo");
            Console.WriteLine("   👨‍🏫 Definición de Persona Avanzada");
            Console.WriteLine("   🧠 Marco de Pensamiento Estructurado (Chain of Thought)");
            Console.WriteLine("   📚 Contexto Específico del Ejercicio");
            Console.WriteLine("   🛠️ Herramientas Conceptuales Disponibles");
            Console.WriteLine("   🎯 Tarea Principal con CoT");
            Console.WriteLine("   📋 Estructura de Output Requerida");
            Console.WriteLine("   🤔 Reflexión Meta-cognitiva Final");
            Console.WriteLine();
            
            Console.WriteLine($"📊 Longitud total: {promptResult.CompletePrompt.Length:N0} caracteres");
            Console.WriteLine("💡 Este prompt está optimizado para Claude y usa técnicas avanzadas de prompting.");
        }

        private void MostrarConfiguracion(ExerciseConfiguration config)
        {
            Console.WriteLine($"   Nivel: {config.Level}");
            Console.WriteLine($"   Tema: {config.Topic}");
            Console.WriteLine($"   Tipo: {config.Type}");
            Console.WriteLine($"   Contexto: {config.Context}");
            Console.WriteLine($"   Duración: {config.EstimatedMinutes} minutos");
            Console.WriteLine($"   Incluir tests: {(config.IncludeUnitTests ? "Sí" : "No")}");
            Console.WriteLine($"   Incluir extensiones: {(config.IncludeExtensionChallenges ? "Sí" : "No")}");
        }

        private void MostrarResumenEjercicio(Exercise exercise)
        {
            Console.WriteLine($"   📝 Título: {exercise.Title}");
            Console.WriteLine($"   ⏱️ Duración: {exercise.EstimatedMinutes} minutos");
            Console.WriteLine($"   🎯 Objetivos: {exercise.LearningObjectives.Count}");
            Console.WriteLine($"   ✅ Criterios: {exercise.SuccessCriteria.Count}");
            Console.WriteLine($"   🚀 Extensions: {exercise.ExtensionChallenges.Count}");
            Console.WriteLine($"   🧪 Tests: {(!string.IsNullOrEmpty(exercise.UnitTestCode) ? "Incluidos" : "No incluidos")}");
        }

        private void MostrarEjercicioCompleto(Exercise exercise)
        {
            Console.WriteLine("📋 EJERCICIO GENERADO:");
            Console.WriteLine("─" + new string('─', 25));
            Console.WriteLine($"📝 **Título**: {exercise.Title}");
            Console.WriteLine($"⏱️ **Duración**: {exercise.EstimatedMinutes} minutos");
            Console.WriteLine($"📊 **Nivel**: {exercise.Level} | **Tema**: {exercise.Topic} | **Tipo**: {exercise.Type}");
            Console.WriteLine();
            
            Console.WriteLine("📖 **Descripción**:");
            Console.WriteLine($"   {exercise.Description}");
            Console.WriteLine();
            
            Console.WriteLine($"🎯 **Objetivos de Aprendizaje** ({exercise.LearningObjectives.Count}):");
            for (int i = 0; i < exercise.LearningObjectives.Count; i++)
            {
                Console.WriteLine($"   {i + 1}. {exercise.LearningObjectives[i]}");
            }
            Console.WriteLine();
            
            Console.WriteLine($"✅ **Criterios de Éxito** ({exercise.SuccessCriteria.Count}):");
            for (int i = 0; i < exercise.SuccessCriteria.Count; i++)
            {
                Console.WriteLine($"   ✓ {exercise.SuccessCriteria[i]}");
            }
            Console.WriteLine();
            
            if (exercise.ExtensionChallenges.Count > 0)
            {
                Console.WriteLine($"🚀 **Desafíos de Extensión** ({exercise.ExtensionChallenges.Count}):");
                for (int i = 0; i < exercise.ExtensionChallenges.Count; i++)
                {
                    Console.WriteLine($"   + {exercise.ExtensionChallenges[i]}");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("💻 **Código**:");
            Console.WriteLine($"   📄 Código inicial: {exercise.StarterCode?.Length ?? 0} caracteres");
            Console.WriteLine($"   ✅ Solución: {exercise.SolutionCode?.Length ?? 0} caracteres");
            Console.WriteLine($"   🧪 Tests: {(!string.IsNullOrEmpty(exercise.UnitTestCode) ? exercise.UnitTestCode.Length + " caracteres" : "No incluidos")}");
        }
    }
}