using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CursoNET.ExerciseGenerator.Configuration;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Generador avanzado de prompts usando Context Engineering para Claude
    /// Implementa técnicas avanzadas de prompting incluyendo:
    /// - Chain of Thought (CoT)
    /// - Role-based prompting
    /// - Few-shot learning
    /// - Tool usage patterns
    /// - Context injection
    /// </summary>
    public class AdvancedPromptGenerator
    {
        #region Configuración del Sistema de Cursos

        public class CourseSystemContext
        {
            public string CourseName { get; set; } = "Curso Completo de .NET";
            public string Institution { get; set; } = "Academia .NET";
            public string CurrentModule { get; set; } = "";
            public int ModuleNumber { get; set; }
            public int TotalModules { get; set; } = 12;
            public List<string> PreviousModules { get; set; } = new();
            public List<string> UpcomingModules { get; set; } = new();
            public Dictionary<string, int> StudentProgressMap { get; set; } = new();
            public string AssessmentStrategy { get; set; } = "Formativo y Sumativo";
            public List<string> LearningOutcomes { get; set; } = new();
            public string Methodology { get; set; } = "Aprendizaje Basado en Proyectos";
        }

        public class PersonaConfiguration
        {
            public string Name { get; set; } = "";
            public string Role { get; set; } = "";
            public string Expertise { get; set; } = "";
            public string TeachingPhilosophy { get; set; } = "";
            public List<string> Specializations { get; set; } = new();
            public string CommunicationStyle { get; set; } = "";
            public int YearsOfExperience { get; set; }
            public List<string> PreferredTools { get; set; } = new();
            public string ProblemSolvingApproach { get; set; } = "";
        }

        public class ThinkingFramework
        {
            public string PrimaryFramework { get; set; } = "";
            public List<string> Steps { get; set; } = new();
            public Dictionary<string, string> Techniques { get; set; } = new();
            public List<string> ValidationMethods { get; set; } = new();
            public string ReflectionPattern { get; set; } = "";
        }

        #endregion

        #region Templates de Prompt Avanzados

        /// <summary>
        /// Genera un prompt usando Chain of Thought con context engineering avanzado
        /// </summary>
        public string GenerateAdvancedCoTPrompt(
            ExerciseConfiguration exerciseConfig, 
            MentorConfiguration mentorConfig,
            CourseSystemContext courseContext,
            PersonaConfiguration persona,
            ThinkingFramework thinkingFramework)
        {
            var prompt = new StringBuilder();

            // === SYSTEM CONTEXT INJECTION ===
            prompt.AppendLine(GenerateSystemContextSection(courseContext, persona));
            
            // === ROLE DEFINITION WITH ADVANCED PERSONA ===
            prompt.AppendLine(GenerateAdvancedPersonaSection(persona, mentorConfig));
            
            // === THINKING FRAMEWORK SETUP ===
            prompt.AppendLine(GenerateThinkingFrameworkSection(thinkingFramework));
            
            // === CONTEXT ENGINEERING FOR COURSE SYSTEM ===
            prompt.AppendLine(GenerateCourseContextSection(courseContext, exerciseConfig));
            
            // === TOOL USAGE PATTERNS ===
            prompt.AppendLine(GenerateToolUsageSection());
            
            // === MAIN TASK WITH CoT STRUCTURE ===
            prompt.AppendLine(GenerateMainTaskWithCoT(exerciseConfig, mentorConfig, courseContext));
            
            // === OUTPUT STRUCTURE WITH VALIDATION ===
            prompt.AppendLine(GenerateOutputStructureSection(exerciseConfig));
            
            // === META-COGNITIVE REFLECTION ===
            prompt.AppendLine(GenerateReflectionSection());

            return prompt.ToString();
        }

        private string GenerateSystemContextSection(CourseSystemContext courseContext, PersonaConfiguration persona)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 🎯 CONTEXTO DEL SISTEMA EDUCATIVO");
            sb.AppendLine();
            sb.AppendLine("## Sistema de Cursos .NET");
            sb.AppendLine($"- **Institución**: {courseContext.Institution}");
            sb.AppendLine($"- **Curso**: {courseContext.CourseName}");
            sb.AppendLine($"- **Módulo Actual**: {courseContext.CurrentModule} ({courseContext.ModuleNumber}/{courseContext.TotalModules})");
            sb.AppendLine($"- **Metodología**: {courseContext.Methodology}");
            sb.AppendLine($"- **Estrategia de Evaluación**: {courseContext.AssessmentStrategy}");
            sb.AppendLine();
            
            if (courseContext.PreviousModules.Any())
            {
                sb.AppendLine("### Módulos Completados:");
                foreach (var module in courseContext.PreviousModules)
                {
                    sb.AppendLine($"✅ {module}");
                }
                sb.AppendLine();
            }
            
            if (courseContext.UpcomingModules.Any())
            {
                sb.AppendLine("### Próximos Módulos:");
                foreach (var module in courseContext.UpcomingModules.Take(3))
                {
                    sb.AppendLine($"⏭️ {module}");
                }
                sb.AppendLine();
            }
            
            if (courseContext.LearningOutcomes.Any())
            {
                sb.AppendLine("### Resultados de Aprendizaje del Curso:");
                foreach (var outcome in courseContext.LearningOutcomes)
                {
                    sb.AppendLine($"🎓 {outcome}");
                }
                sb.AppendLine();
            }
            
            sb.AppendLine("---");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string GenerateAdvancedPersonaSection(PersonaConfiguration persona, MentorConfiguration mentorConfig)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 👨‍🏫 DEFINICIÓN DE PERSONA AVANZADA");
            sb.AppendLine();
            sb.AppendLine($"**Nombre**: {persona.Name}");
            sb.AppendLine($"**Rol**: {persona.Role}");
            sb.AppendLine($"**Experiencia**: {persona.YearsOfExperience} años en la industria");
            sb.AppendLine();
            
            sb.AppendLine("## Perfil Profesional");
            sb.AppendLine($"- **Área de Expertise**: {persona.Expertise}");
            sb.AppendLine($"- **Filosofía de Enseñanza**: {persona.TeachingPhilosophy}");
            sb.AppendLine($"- **Estilo de Comunicación**: {persona.CommunicationStyle}");
            sb.AppendLine($"- **Enfoque de Resolución de Problemas**: {persona.ProblemSolvingApproach}");
            sb.AppendLine();
            
            if (persona.Specializations.Any())
            {
                sb.AppendLine("## Especializaciones:");
                foreach (var spec in persona.Specializations)
                {
                    sb.AppendLine($"🔧 {spec}");
                }
                sb.AppendLine();
            }
            
            if (persona.PreferredTools.Any())
            {
                sb.AppendLine("## Herramientas Preferidas:");
                foreach (var tool in persona.PreferredTools)
                {
                    sb.AppendLine($"⚡ {tool}");
                }
                sb.AppendLine();
            }
            
            sb.AppendLine("## Características de Tu Enseñanza:");
            sb.AppendLine("- Usas ejemplos del mundo real y casos prácticos");
            sb.AppendLine("- Explicas el 'por qué' detrás de cada concepto");
            sb.AppendLine("- Conectas temas actuales con conocimientos previos");
            sb.AppendLine("- Anticipas errores comunes y los conviertes en oportunidades de aprendizaje");
            sb.AppendLine("- Adaptas la complejidad según el nivel del estudiante");
            sb.AppendLine();
            
            sb.AppendLine("---");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string GenerateThinkingFrameworkSection(ThinkingFramework framework)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 🧠 MARCO DE PENSAMIENTO ESTRUCTURADO");
            sb.AppendLine();
            sb.AppendLine($"**Framework Principal**: {framework.PrimaryFramework}");
            sb.AppendLine();
            
            sb.AppendLine("## Proceso de Pensamiento (Chain of Thought):");
            sb.AppendLine();
            sb.AppendLine("Para cada ejercicio que crees, DEBES seguir este proceso de pensamiento:");
            sb.AppendLine();
            
            for (int i = 0; i < framework.Steps.Count; i++)
            {
                sb.AppendLine($"### Paso {i + 1}: {framework.Steps[i]}");
                sb.AppendLine("<thinking>");
                sb.AppendLine($"Aquí reflexionaré sobre: {framework.Steps[i]}");
                sb.AppendLine("- ¿Qué necesita saber el estudiante en este punto?");
                sb.AppendLine("- ¿Cómo se conecta con conocimientos previos?");
                sb.AppendLine("- ¿Qué errores comunes debo anticipar?");
                sb.AppendLine("- ¿Cómo puedo hacer esto más práctico y relevante?");
                sb.AppendLine("</thinking>");
                sb.AppendLine();
            }
            
            if (framework.Techniques.Any())
            {
                sb.AppendLine("## Técnicas de Enseñanza Disponibles:");
                foreach (var technique in framework.Techniques)
                {
                    sb.AppendLine($"- **{technique.Key}**: {technique.Value}");
                }
                sb.AppendLine();
            }
            
            if (framework.ValidationMethods.Any())
            {
                sb.AppendLine("## Métodos de Validación:");
                foreach (var method in framework.ValidationMethods)
                {
                    sb.AppendLine($"✓ {method}");
                }
                sb.AppendLine();
            }
            
            sb.AppendLine($"## Patrón de Reflexión:");
            sb.AppendLine(framework.ReflectionPattern);
            sb.AppendLine();
            
            sb.AppendLine("---");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string GenerateCourseContextSection(CourseSystemContext courseContext, ExerciseConfiguration exerciseConfig)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 📚 CONTEXTO ESPECÍFICO DEL EJERCICIO");
            sb.AppendLine();
            sb.AppendLine("## Posicionamiento Curricular");
            sb.AppendLine($"- **Ubicación**: Módulo {courseContext.ModuleNumber} de {courseContext.TotalModules}");
            sb.AppendLine($"- **Tema Actual**: {exerciseConfig.Topic}");
            sb.AppendLine($"- **Nivel de Estudiante**: {exerciseConfig.Level}");
            sb.AppendLine($"- **Tipo de Actividad**: {exerciseConfig.Type}");
            sb.AppendLine();
            
            sb.AppendLine("## Conocimientos Prerequisitos (Ya dominados):");
            if (courseContext.PreviousModules.Any())
            {
                foreach (var module in courseContext.PreviousModules)
                {
                    sb.AppendLine($"✅ {module}");
                }
            }
            else
            {
                sb.AppendLine("- Conceptos básicos de programación");
                sb.AppendLine("- Sintaxis fundamental de C#");
            }
            sb.AppendLine();
            
            sb.AppendLine("## Conexión con Módulos Futuros:");
            if (courseContext.UpcomingModules.Any())
            {
                foreach (var module in courseContext.UpcomingModules.Take(2))
                {
                    sb.AppendLine($"🔮 Este ejercicio prepara para: {module}");
                }
            }
            else
            {
                sb.AppendLine("🔮 Este ejercicio construye las bases para conceptos avanzados");
            }
            sb.AppendLine();
            
            if (courseContext.StudentProgressMap.Any())
            {
                sb.AppendLine("## Progreso Típico del Estudiante:");
                foreach (var progress in courseContext.StudentProgressMap)
                {
                    sb.AppendLine($"- {progress.Key}: {progress.Value}%");
                }
                sb.AppendLine();
            }
            
            sb.AppendLine("---");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string GenerateToolUsageSection()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 🛠️ HERRAMIENTAS CONCEPTUALES DISPONIBLES");
            sb.AppendLine();
            sb.AppendLine("Como mentor experto, tienes acceso a estas herramientas conceptuales:");
            sb.AppendLine();
            
            sb.AppendLine("## 🎯 Herramienta de Análisis de Complejidad");
            sb.AppendLine("```");
            sb.AppendLine("USAR CUANDO: Necesites evaluar si un ejercicio es apropiado para el nivel");
            sb.AppendLine("ENTRADA: Descripción del ejercicio");
            sb.AppendLine("SALIDA: Puntuación de complejidad (1-10) y justificación");
            sb.AppendLine("```");
            sb.AppendLine();
            
            sb.AppendLine("## 🔍 Herramienta de Detección de Prerequisitos");
            sb.AppendLine("```");
            sb.AppendLine("USAR CUANDO: Necesites identificar qué debe saber el estudiante");
            sb.AppendLine("ENTRADA: Conceptos del ejercicio");
            sb.AppendLine("SALIDA: Lista de prerequisitos ordenada por importancia");
            sb.AppendLine("```");
            sb.AppendLine();
            
            sb.AppendLine("## 🎨 Herramienta de Contextualización");
            sb.AppendLine("```");
            sb.AppendLine("USAR CUANDO: Necesites adaptar ejemplos a un dominio específico");
            sb.AppendLine("ENTRADA: Dominio objetivo (ej: banca, salud, e-commerce)");
            sb.AppendLine("SALIDA: Ejemplos y nomenclatura contextualizada");
            sb.AppendLine("```");
            sb.AppendLine();
            
            sb.AppendLine("## 🧪 Herramienta de Generación de Casos de Prueba");
            sb.AppendLine("```");
            sb.AppendLine("USAR CUANDO: Necesites crear pruebas comprehensivas");
            sb.AppendLine("ENTRADA: Funcionalidad a probar");
            sb.AppendLine("SALIDA: Casos felices, edge cases, y casos de error");
            sb.AppendLine("```");
            sb.AppendLine();
            
            sb.AppendLine("## 🔗 Herramienta de Conexión Curricular");
            sb.AppendLine("```");
            sb.AppendLine("USAR CUANDO: Necesites conectar con otros módulos");
            sb.AppendLine("ENTRADA: Concepto actual");
            sb.AppendLine("SALIDA: Conexiones con temas previos y futuros");
            sb.AppendLine("```");
            sb.AppendLine();
            
            sb.AppendLine("**IMPORTANTE**: Siempre indica qué herramienta estás usando cuando la apliques.");
            sb.AppendLine();
            
            sb.AppendLine("---");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string GenerateMainTaskWithCoT(
            ExerciseConfiguration exerciseConfig, 
            MentorConfiguration mentorConfig, 
            CourseSystemContext courseContext)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 🎯 TAREA PRINCIPAL CON CHAIN OF THOUGHT");
            sb.AppendLine();
            sb.AppendLine("## Especificaciones del Ejercicio:");
            sb.AppendLine($"- **Nivel**: {exerciseConfig.Level}");
            sb.AppendLine($"- **Tema**: {exerciseConfig.Topic}");
            sb.AppendLine($"- **Tipo**: {exerciseConfig.Type}");
            sb.AppendLine($"- **Contexto/Dominio**: {exerciseConfig.Context}");
            sb.AppendLine($"- **Duración Estimada**: {exerciseConfig.EstimatedMinutes} minutos");
            sb.AppendLine($"- **Incluir Pruebas**: {(exerciseConfig.IncludeUnitTests ? "Sí" : "No")}");
            sb.AppendLine();
            
            sb.AppendLine("## Proceso de Creación (DEBES seguir este orden):");
            sb.AppendLine();
            
            sb.AppendLine("### 🔍 FASE 1: ANÁLISIS Y CONTEXTUALIZACIÓN");
            sb.AppendLine("<thinking>");
            sb.AppendLine("En esta fase analizaré:");
            sb.AppendLine("1. ¿Qué posición ocupa este ejercicio en el currículo general?");
            sb.AppendLine("2. ¿Qué conocimientos previos puedo asumir?");
            sb.AppendLine("3. ¿Cómo se conecta con módulos anteriores y posteriores?");
            sb.AppendLine("4. ¿Cuál es el nivel de complejidad apropiado?");
            sb.AppendLine("5. ¿Qué contexto del mundo real sería más motivador?");
            sb.AppendLine("</thinking>");
            sb.AppendLine();
            sb.AppendLine("**Acción requerida**: Usar las herramientas de Análisis de Complejidad y Detección de Prerequisitos");
            sb.AppendLine();
            
            sb.AppendLine("### 🎨 FASE 2: DISEÑO PEDAGÓGICO");
            sb.AppendLine("<thinking>");
            sb.AppendLine("Ahora diseñaré la estructura pedagógica:");
            sb.AppendLine("1. ¿Cuáles son los objetivos de aprendizaje específicos?");
            sb.AppendLine("2. ¿Qué secuencia de descubrimiento será más efectiva?");
            sb.AppendLine("3. ¿Qué errores comunes debo anticipar y cómo convertirlos en aprendizaje?");
            sb.AppendLine("4. ¿Cómo puedo hacer el ejercicio progresivo en dificultad?");
            sb.AppendLine("5. ¿Qué elementos de gamificación o motivación puedo incluir?");
            sb.AppendLine("</thinking>");
            sb.AppendLine();
            sb.AppendLine("**Acción requerida**: Usar la herramienta de Contextualización si es necesario");
            sb.AppendLine();
            
            sb.AppendLine("### ⚡ FASE 3: IMPLEMENTACIÓN TÉCNICA");
            sb.AppendLine("<thinking>");
            sb.AppendLine("En la implementación consideraré:");
            sb.AppendLine("1. ¿Qué estructura de código inicial será más útil sin dar demasiado?");
            sb.AppendLine("2. ¿Cómo puedo hacer que los comentarios TODO sean guías claras?");
            sb.AppendLine("3. ¿La solución demuestra claramente las mejores prácticas?");
            sb.AppendLine("4. ¿Estoy usando patrones y técnicas apropiadas para el nivel?");
            sb.AppendLine("5. ¿El código es lo suficientemente realista sin ser abrumador?");
            sb.AppendLine("</thinking>");
            sb.AppendLine();
            sb.AppendLine("**Acción requerida**: Usar la herramienta de Generación de Casos de Prueba si incluye tests");
            sb.AppendLine();
            
            sb.AppendLine("### 🔗 FASE 4: CONEXIÓN CURRICULAR");
            sb.AppendLine("<thinking>");
            sb.AppendLine("Para la conexión curricular evalúo:");
            sb.AppendLine("1. ¿Cómo se conecta este ejercicio con lo que ya saben?");
            sb.AppendLine("2. ¿Qué conceptos futuros estoy preparando sutilmente?");
            sb.AppendLine("3. ¿Hay oportunidades para referencias cruzadas con otros módulos?");
            sb.AppendLine("4. ¿Estoy construyendo vocabulario técnico progresivamente?");
            sb.AppendLine("5. ¿El ejercicio fortalece conceptos fundamentales mientras introduce nuevos?");
            sb.AppendLine("</thinking>");
            sb.AppendLine();
            sb.AppendLine("**Acción requerida**: Usar la herramienta de Conexión Curricular");
            sb.AppendLine();
            
            sb.AppendLine("### ✅ FASE 5: VALIDACIÓN Y REFINAMIENTO");
            sb.AppendLine("<thinking>");
            sb.AppendLine("Finalmente validaré:");
            sb.AppendLine("1. ¿El ejercicio cumple todos los objetivos de aprendizaje declarados?");
            sb.AppendLine("2. ¿La dificultad es apropiada y progresiva?");
            sb.AppendLine("3. ¿Las instrucciones son claras y sin ambigüedades?");
            sb.AppendLine("4. ¿Los criterios de éxito son medibles y específicos?");
            sb.AppendLine("5. ¿Hay suficientes desafíos de extensión para estudiantes avanzados?");
            sb.AppendLine("</thinking>");
            sb.AppendLine();
            
            sb.AppendLine("---");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string GenerateOutputStructureSection(ExerciseConfiguration exerciseConfig)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 📋 ESTRUCTURA DE OUTPUT REQUERIDA");
            sb.AppendLine();
            sb.AppendLine("Tu respuesta DEBE seguir exactamente esta estructura:");
            sb.AppendLine();
            
            sb.AppendLine("## 🏷️ METADATOS DEL EJERCICIO");
            sb.AppendLine("```yaml");
            sb.AppendLine("titulo: \"[Título atractivo y descriptivo]\"");
            sb.AppendLine($"nivel: \"{exerciseConfig.Level}\"");
            sb.AppendLine($"tema: \"{exerciseConfig.Topic}\"");
            sb.AppendLine($"tipo: \"{exerciseConfig.Type}\"");
            sb.AppendLine("duracion_minutos: [número]");
            sb.AppendLine("complejidad_score: [1-10]");
            sb.AppendLine("keywords: [\"keyword1\", \"keyword2\", \"keyword3\"]");
            sb.AppendLine("```");
            sb.AppendLine();
            
            sb.AppendLine("## 🎯 ANÁLISIS PEDAGÓGICO");
            sb.AppendLine("### Resultados del Análisis de Complejidad:");
            sb.AppendLine("- **Puntuación**: X/10");
            sb.AppendLine("- **Justificación**: [Explicación de por qué esta puntuación]");
            sb.AppendLine();
            
            sb.AppendLine("### Prerequisitos Identificados:");
            sb.AppendLine("1. [Prerequisito más importante]");
            sb.AppendLine("2. [Segundo prerequisito]");
            sb.AppendLine("3. [Tercer prerequisito]");
            sb.AppendLine();
            
            sb.AppendLine("### Conexiones Curriculares:");
            sb.AppendLine("- **Conecta con temas previos**: [Lista]");
            sb.AppendLine("- **Prepara para temas futuros**: [Lista]");
            sb.AppendLine();
            
            sb.AppendLine("## 📚 CONTENIDO EDUCATIVO");
            sb.AppendLine();
            
            sb.AppendLine("### Descripción:");
            sb.AppendLine("[2-3 párrafos explicando qué hará el estudiante y por qué es importante]");
            sb.AppendLine();
            
            sb.AppendLine("### Objetivos de Aprendizaje:");
            sb.AppendLine("Al completar este ejercicio, el estudiante será capaz de:");
            sb.AppendLine("1. [Objetivo específico y medible]");
            sb.AppendLine("2. [Objetivo específico y medible]");
            sb.AppendLine("3. [Objetivo específico y medible]");
            sb.AppendLine("4. [Objetivo específico y medible]");
            sb.AppendLine("5. [Objetivo específico y medible]");
            sb.AppendLine();
            
            sb.AppendLine("### Enunciado del Problema:");
            sb.AppendLine("[Descripción detallada y contextualizada del problema a resolver]");
            sb.AppendLine();
            
            sb.AppendLine("### Requerimientos Técnicos:");
            sb.AppendLine("- [ ] [Requerimiento específico 1]");
            sb.AppendLine("- [ ] [Requerimiento específico 2]");
            sb.AppendLine("- [ ] [Requerimiento específico 3]");
            sb.AppendLine("- [ ] [Requerimiento específico 4]");
            sb.AppendLine("- [ ] [Requerimiento específico 5]");
            sb.AppendLine();
            
            sb.AppendLine("### Criterios de Éxito:");
            sb.AppendLine("✅ [Criterio medible 1]");
            sb.AppendLine("✅ [Criterio medible 2]");
            sb.AppendLine("✅ [Criterio medible 3]");
            sb.AppendLine("✅ [Criterio medible 4]");
            sb.AppendLine("✅ [Criterio medible 5]");
            sb.AppendLine();
            
            sb.AppendLine("## 💻 IMPLEMENTACIÓN");
            sb.AppendLine();
            
            sb.AppendLine("### Código de Inicio:");
            sb.AppendLine("```csharp");
            sb.AppendLine("// [Código inicial con estructura clara y TODOs específicos]");
            sb.AppendLine("```");
            sb.AppendLine();
            
            sb.AppendLine("### Solución Completa:");
            sb.AppendLine("```csharp");
            sb.AppendLine("// [Solución completa con comentarios explicativos]");
            sb.AppendLine("```");
            sb.AppendLine();
            
            if (exerciseConfig.IncludeUnitTests)
            {
                sb.AppendLine("### Pruebas Unitarias:");
                sb.AppendLine("```csharp");
                sb.AppendLine("// [Pruebas comprehensivas con casos felices, edge cases y manejo de errores]");
                sb.AppendLine("```");
                sb.AppendLine();
            }
            
            sb.AppendLine("## 🚀 EXTENSIONES Y RETOS");
            sb.AppendLine();
            
            sb.AppendLine("### Nivel Básico (para estudiantes que terminan rápido):");
            sb.AppendLine("1. [Extensión simple]");
            sb.AppendLine("2. [Extensión simple]");
            sb.AppendLine();
            
            sb.AppendLine("### Nivel Intermedio:");
            sb.AppendLine("1. [Extensión moderada]");
            sb.AppendLine("2. [Extensión moderada]");
            sb.AppendLine();
            
            sb.AppendLine("### Nivel Avanzado (para estudiantes excepcionales):");
            sb.AppendLine("1. [Desafío avanzado]");
            sb.AppendLine("2. [Desafío avanzado]");
            sb.AppendLine();
            
            sb.AppendLine("## 🧠 GUÍA PEDAGÓGICA");
            sb.AppendLine();
            
            sb.AppendLine("### Conceptos Clave Explicados:");
            sb.AppendLine("1. **[Concepto 1]**: [Explicación clara y práctica]");
            sb.AppendLine("2. **[Concepto 2]**: [Explicación clara y práctica]");
            sb.AppendLine("3. **[Concepto 3]**: [Explicación clara y práctica]");
            sb.AppendLine();
            
            sb.AppendLine("### Errores Comunes y Soluciones:");
            sb.AppendLine("| Error Común | Por qué ocurre | Cómo solucionarlo |");
            sb.AppendLine("|-------------|----------------|-------------------|");
            sb.AppendLine("| [Error 1] | [Causa] | [Solución] |");
            sb.AppendLine("| [Error 2] | [Causa] | [Solución] |");
            sb.AppendLine("| [Error 3] | [Causa] | [Solución] |");
            sb.AppendLine();
            
            sb.AppendLine("### Estrategias de Debugging:");
            sb.AppendLine("1. [Estrategia específica para este tipo de ejercicio]");
            sb.AppendLine("2. [Estrategia específica para este tipo de ejercicio]");
            sb.AppendLine("3. [Estrategia específica para este tipo de ejercicio]");
            sb.AppendLine();
            
            sb.AppendLine("### Preguntas Guía para el Mentor:");
            sb.AppendLine("- ¿[Pregunta que ayude al estudiante a reflexionar]?");
            sb.AppendLine("- ¿[Pregunta que conecte con conocimientos previos]?");
            sb.AppendLine("- ¿[Pregunta que anticipe conceptos futuros]?");
            sb.AppendLine();
            
            sb.AppendLine("## 📈 EVALUACIÓN Y ASSESSMENT");
            sb.AppendLine();
            
            sb.AppendLine("### Rúbrica de Evaluación:");
            sb.AppendLine("| Criterio | Novato (1-2) | Competente (3-4) | Experto (5) |");
            sb.AppendLine("|----------|--------------|------------------|-------------|");
            sb.AppendLine("| [Criterio 1] | [Descriptor] | [Descriptor] | [Descriptor] |");
            sb.AppendLine("| [Criterio 2] | [Descriptor] | [Descriptor] | [Descriptor] |");
            sb.AppendLine("| [Criterio 3] | [Descriptor] | [Descriptor] | [Descriptor] |");
            sb.AppendLine();
            
            sb.AppendLine("### Indicadores de Comprensión:");
            sb.AppendLine("- ✅ **Comprende completamente**: [Indicadores]");
            sb.AppendLine("- ⚠️ **Comprende parcialmente**: [Indicadores]");
            sb.AppendLine("- ❌ **No comprende**: [Indicadores]");
            sb.AppendLine();
            
            sb.AppendLine("---");
            sb.AppendLine();
            
            return sb.ToString();
        }

        private string GenerateReflectionSection()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# 🤔 REFLEXIÓN META-COGNITIVA FINAL");
            sb.AppendLine();
            sb.AppendLine("Antes de entregar tu respuesta, DEBES reflexionar sobre:");
            sb.AppendLine();
            
            sb.AppendLine("<thinking>");
            sb.AppendLine("## Auto-Evaluación del Ejercicio Creado:");
            sb.AppendLine();
            sb.AppendLine("### 1. Relevancia Curricular:");
            sb.AppendLine("- ¿Este ejercicio se alinea perfectamente con el módulo actual?");
            sb.AppendLine("- ¿Las conexiones con módulos previos y futuros son claras?");
            sb.AppendLine("- ¿El nivel de dificultad es apropiado para donde están los estudiantes?");
            sb.AppendLine();
            
            sb.AppendLine("### 2. Calidad Pedagógica:");
            sb.AppendLine("- ¿Los objetivos de aprendizaje son específicos y medibles?");
            sb.AppendLine("- ¿La secuencia de aprendizaje es lógica y progresiva?");
            sb.AppendLine("- ¿He anticipado y abordado errores comunes?");
            sb.AppendLine("- ¿El ejercicio promueve el pensamiento crítico?");
            sb.AppendLine();
            
            sb.AppendLine("### 3. Implementación Técnica:");
            sb.AppendLine("- ¿El código inicial proporciona suficiente estructura sin dar la respuesta?");
            sb.AppendLine("- ¿La solución demuestra mejores prácticas claramente?");
            sb.AppendLine("- ¿Los comentarios y TODOs son guías útiles?");
            sb.AppendLine("- ¿Las pruebas unitarias cubren casos importantes?");
            sb.AppendLine();
            
            sb.AppendLine("### 4. Engagement y Motivación:");
            sb.AppendLine("- ¿El contexto es relevante y motivador para los estudiantes?");
            sb.AppendLine("- ¿Hay suficientes desafíos para diferentes niveles?");
            sb.AppendLine("- ¿El ejercicio conecta con aplicaciones del mundo real?");
            sb.AppendLine();
            
            sb.AppendLine("### 5. Completitud:");
            sb.AppendLine("- ¿He seguido todos los pasos del framework de pensamiento?");
            sb.AppendLine("- ¿He usado las herramientas conceptuales apropiadamente?");
            sb.AppendLine("- ¿La estructura de output está completa?");
            sb.AppendLine("- ¿Faltan elementos importantes?");
            sb.AppendLine();
            
            sb.AppendLine("## Mejoras Identificadas:");
            sb.AppendLine("Si encuentro áreas de mejora, las abordaré antes de entregar la respuesta final.");
            sb.AppendLine("</thinking>");
            sb.AppendLine();
            
            sb.AppendLine("**INSTRUCCIÓN FINAL**: Después de completar tu reflexión, proporciona el ejercicio completo siguiendo exactamente la estructura de output especificada.");
            sb.AppendLine();
            
            return sb.ToString();
        }

        #endregion

        #region Factory Methods para Configuraciones Predefinidas

        public static CourseSystemContext CreateDotNetCourseContext()
        {
            return new CourseSystemContext
            {
                CourseName = "Desarrollo Full-Stack con .NET",
                Institution = "Academia Técnica Superior",
                CurrentModule = "Fundamentos de C# y POO",
                ModuleNumber = 3,
                TotalModules = 12,
                PreviousModules = new List<string>
                {
                    "Introducción a la Programación",
                    "Sintaxis Básica de C#"
                },
                UpcomingModules = new List<string>
                {
                    "Colecciones y LINQ",
                    "Manejo de Errores y Excepciones",
                    "Programación Asíncrona",
                    "Entity Framework Core",
                    "ASP.NET Core Web API",
                    "Blazor y Frontend",
                    "Pruebas Unitarias e Integración",
                    "Deployment y DevOps",
                    "Patrones de Diseño Avanzados",
                    "Proyecto Final"
                },
                StudentProgressMap = new Dictionary<string, int>
                {
                    ["Conceptos Básicos"] = 85,
                    ["Sintaxis C#"] = 80,
                    ["Debugging"] = 70,
                    ["POO Básico"] = 60
                },
                LearningOutcomes = new List<string>
                {
                    "Desarrollar aplicaciones web full-stack usando .NET",
                    "Aplicar principios de arquitectura de software limpia",
                    "Implementar patrones de diseño apropiados",
                    "Crear APIs RESTful escalables y mantenibles",
                    "Gestionar datos usando Entity Framework Core",
                    "Implementar pruebas automatizadas comprehensivas"
                },
                Methodology = "Aprendizaje Basado en Proyectos con Mentoring Personalizado"
            };
        }

        public static PersonaConfiguration CreateSeniorMentorPersona()
        {
            return new PersonaConfiguration
            {
                Name = "Carlos Mendoza",
                Role = "Senior Software Architect & .NET Mentor",
                Expertise = "Arquitectura de Software, .NET Ecosystem, DevOps",
                YearsOfExperience = 15,
                TeachingPhilosophy = "Aprender haciendo con comprensión profunda de los fundamentos",
                Specializations = new List<string>
                {
                    "Clean Architecture",
                    "Domain Driven Design",
                    "Microservices con .NET",
                    "Performance Optimization",
                    "Cloud-Native Development",
                    "Mentoring y Team Leadership"
                },
                CommunicationStyle = "Directo pero empático, usa analogías del mundo real",
                PreferredTools = new List<string>
                {
                    "Visual Studio / VS Code",
                    "Git & GitHub",
                    "Docker & Kubernetes",
                    "Azure DevOps",
                    "SonarQube",
                    "Postman/Swagger"
                },
                ProblemSolvingApproach = "Divide y vencerás, siempre pregunta '¿por qué?' tres veces"
            };
        }

        public static ThinkingFramework CreateCoTFramework()
        {
            return new ThinkingFramework
            {
                PrimaryFramework = "Bloom's Taxonomy + Constructivist Learning",
                Steps = new List<string>
                {
                    "Analizar el contexto curricular y prerequisitos",
                    "Identificar objetivos de aprendizaje específicos",
                    "Diseñar la secuencia de descubrimiento",
                    "Crear el código inicial con la complejidad apropiada",
                    "Desarrollar la solución exemplar",
                    "Anticipar errores comunes y misconceptions",
                    "Diseñar extensiones progresivas",
                    "Validar la alineación con objetivos"
                },
                Techniques = new Dictionary<string, string>
                {
                    ["Scaffolding"] = "Proporcionar estructura inicial que se puede remover gradualmente",
                    ["Analogías"] = "Conectar conceptos nuevos con experiencias familiares",
                    ["Contraste"] = "Mostrar código malo vs bueno para highlighting mejores prácticas",
                    ["Progresión"] = "Construir complejidad gradualmente",
                    ["Contextualización"] = "Usar escenarios del mundo real relevantes"
                },
                ValidationMethods = new List<string>
                {
                    "Verificar alineación con objetivos de aprendizaje",
                    "Confirmar nivel de dificultad apropiado",
                    "Validar que prerequisitos son suficientes",
                    "Asegurar que extensiones son desafiantes pero alcanzables",
                    "Confirmar que errores comunes están anticipados"
                },
                ReflectionPattern = "Después de cada fase, preguntarse: ¿Esto ayuda al estudiante a construir comprensión duradera? ¿Está conectado con su experiencia previa? ¿Los prepara para conceptos futuros?"
            };
        }

        #endregion

        #region Template-Based Prompt Generation

        /// <summary>
        /// Genera un prompt usando templates configurables desde archivos JSON
        /// </summary>
        public string GenerateTemplateBasedPrompt(
            ExerciseConfiguration exerciseConfig, 
            MentorConfiguration mentorConfig, 
            string templateId = "advancedExerciseGeneration")
        {
            var templateManager = new PromptTemplateManager();
            
            // Cargar configuración de templates
            templateManager.LoadConfigurationAsync().GetAwaiter().GetResult();
            
            // Preparar parámetros
            var parameters = new PromptParameters
            {
                SkillLevel = exerciseConfig.Level.ToString(),
                TopicArea = exerciseConfig.Topic.ToString(),
                ExerciseType = exerciseConfig.Type.ToString(),
                Context = exerciseConfig.Context,
                EstimatedMinutes = exerciseConfig.EstimatedMinutes,
                MentorConfiguration = mentorConfig
            };
            
            // Validar template
            var validation = templateManager.ValidateTemplate(templateId, parameters);
            if (!validation.IsValid)
            {
                throw new InvalidOperationException($"Template validation failed: {validation.Message}");
            }
            
            // Generar prompt usando template
            return templateManager.GeneratePrompt(templateId, parameters);
        }

        /// <summary>
        /// Genera un prompt usando template personalizado con parámetros adicionales
        /// </summary>
        public string GenerateCustomTemplatePrompt(
            ExerciseConfiguration exerciseConfig,
            MentorConfiguration mentorConfig,
            Dictionary<string, string> customParameters,
            string templateId = "customPromptGeneration")
        {
            var templateManager = new PromptTemplateManager();
            templateManager.LoadConfigurationAsync().GetAwaiter().GetResult();
            
            var parameters = new PromptParameters
            {
                SkillLevel = exerciseConfig.Level.ToString(),
                TopicArea = exerciseConfig.Topic.ToString(),
                ExerciseType = exerciseConfig.Type.ToString(),
                Context = exerciseConfig.Context,
                EstimatedMinutes = exerciseConfig.EstimatedMinutes,
                MentorConfiguration = mentorConfig,
                CustomParameters = customParameters
            };
            
            return templateManager.GeneratePrompt(templateId, parameters);
        }

        /// <summary>
        /// Lista los templates disponibles para generación de prompts
        /// </summary>
        public IEnumerable<string> GetAvailableTemplates()
        {
            var templateManager = new PromptTemplateManager();
            templateManager.LoadConfigurationAsync().GetAwaiter().GetResult();
            return templateManager.GetAvailableTemplates();
        }

        /// <summary>
        /// Obtiene información detallada de un template específico
        /// </summary>
        public string GetTemplateInfo(string templateId)
        {
            var templateManager = new PromptTemplateManager();
            templateManager.LoadConfigurationAsync().GetAwaiter().GetResult();
            
            var template = templateManager.GetTemplate(templateId);
            if (template == null)
            {
                return $"Template '{templateId}' not found";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"📋 Template: {template.Name}");
            sb.AppendLine($"🔖 ID: {template.Id}");
            sb.AppendLine($"📝 Version: {template.Version}");
            sb.AppendLine($"📄 Description: {template.Description}");
            sb.AppendLine($"🔧 Parameters: {template.Parameters.Count}");
            
            if (template.Parameters.Any())
            {
                sb.AppendLine("\n📊 Required Parameters:");
                foreach (var param in template.Parameters)
                {
                    sb.AppendLine($"  • {param.Key}: {param.Value}");
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}