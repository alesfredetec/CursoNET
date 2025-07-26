using System;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Muestra un mapa completo de navegación del sistema
    /// con descripciones detalladas de cada opción disponible
    /// </summary>
    public static class NavigationMap
    {
        public static void ShowNavigationMap()
        {
            Console.WriteLine("🗺️ MAPA DE NAVEGACIÓN DEL SISTEMA");
            Console.WriteLine("=" + new string('=', 80));
            Console.WriteLine();

            ShowSystemOverview();
            Console.WriteLine();

            ShowMenuOptions();
            Console.WriteLine();

            ShowTemplateGuide();
            Console.WriteLine();

            ShowSubMenuFlows();
            Console.WriteLine();

            ShowTechnicalDetails();
            Console.WriteLine();

            ShowUsageRecommendations();

            Console.WriteLine();
            Console.WriteLine("📖 Para más información, consulta los archivos README.md en el proyecto.");
            Console.WriteLine();
            Console.Write("Presiona Enter para volver al menú principal...");
            Console.ReadLine();
        }

        private static void ShowSystemOverview()
        {
            Console.WriteLine("📊 ARQUITECTURA DEL SISTEMA");
            Console.WriteLine("=" + new string('-', 50));
            Console.WriteLine();
            
            Console.WriteLine("🏗️ **TRES SISTEMAS INTEGRADOS:**");
            Console.WriteLine("   1. Sistema Original - Generación directa de ejercicios");
            Console.WriteLine("   2. Sistema Expandido - IA con Context Engineering avanzado");
            Console.WriteLine("   3. Sistema Configurable - JSON flexible sin código");
            Console.WriteLine();
            
            Console.WriteLine("🎯 **CAPACIDADES PRINCIPALES:**");
            Console.WriteLine("   • 18 áreas temáticas (.NET ecosystem completo)");
            Console.WriteLine("   • 8 tipos de ejercicios especializados");
            Console.WriteLine("   • 6 templates de IA para diferentes audiencias");
            Console.WriteLine("   • Configuración JSON sin modificar código");
            Console.WriteLine("   • Context Engineering para Claude AI");
        }

        private static void ShowMenuOptions()
        {
            Console.WriteLine("🎮 OPCIONES DEL MENÚ PRINCIPAL");
            Console.WriteLine("=" + new string('-', 50));
            Console.WriteLine();

            var menuOptions = new[]
            {
                new MenuOption("1", "📚 Demo Original", 
                    "Sistema básico de generación de ejercicios", 
                    "✅ Submenús interactivos\n   ✅ Selección de Topic/Level/Type\n   ✅ Generación inmediata\n   ✅ Export a archivos .cs/.csproj"),

                new MenuOption("2", "🚀 Demo Expandido", 
                    "Sistema avanzado con IA y Context Engineering", 
                    "✅ Módulos de aprendizaje estructurados\n   ✅ Prompts sofisticados para Claude\n   ✅ Configuración de mentor personalizada\n   ✅ Fallback automático a IA"),

                new MenuOption("3", "⚙️ Test Sistema Configurable", 
                    "Prueba del sistema JSON configurable", 
                    "✅ Validación de configuraciones\n   ✅ Carga de ejercicios desde JSON\n   ✅ Resolución de archivos de código\n   ✅ Testing de dependencias"),

                new MenuOption("4", "🧪 Test Menu Flows", 
                    "Validación de todos los flujos de menú", 
                    "✅ Prueba cascada de fallbacks\n   ✅ Generación + IA cuando falla\n   ✅ Validación de consistencia\n   ✅ Reporte de errores"),

                new MenuOption("5", "🎯 Test Prompt Templates", 
                    "Sistema de templates configurables", 
                    "✅ Validación de templates\n   ✅ Testing de parámetros\n   ✅ Generación de prompts 12K+ chars\n   ✅ Context Engineering validation"),

                new MenuOption("6", "🤖 Generador IA Interactivo", 
                    "Selección y generación interactiva de templates", 
                    "✅ 6 templates especializados\n   ✅ Configuración paso a paso\n   ✅ Preview y guardado de prompts\n   ✅ Audiencias diferenciadas"),

                new MenuOption("H", "🗺️ Mapa de Navegación", 
                    "Esta pantalla - Guía completa del sistema", 
                    "✅ Arquitectura del sistema\n   ✅ Descripción de opciones\n   ✅ Guía de templates\n   ✅ Recomendaciones de uso")
            };

            foreach (var option in menuOptions)
            {
                Console.WriteLine($"🔸 **Opción {option.Key}: {option.Title}**");
                Console.WriteLine($"   📝 {option.Description}");
                Console.WriteLine($"   {option.Features}");
                Console.WriteLine();
            }
        }

        private static void ShowTemplateGuide()
        {
            Console.WriteLine("🎨 GUÍA DE TEMPLATES DE IA");
            Console.WriteLine("=" + new string('-', 50));
            Console.WriteLine();

            var templates = new[]
            {
                new TemplateInfo("🎓 basicExerciseGeneration", "Principiantes", 
                    "Template simple y directo para ejercicios básicos", 
                    "• 5 parámetros básicos\n   • Generación rápida\n   • Ideal para: Estudiantes nuevos"),

                new TemplateInfo("🚀 advancedExerciseGeneration", "Intermedio/Avanzado", 
                    "Template avanzado con Context Engineering", 
                    "• 10 parámetros completos\n   • Context Engineering básico\n   • Ideal para: Cursos intermedios"),

                new TemplateInfo("🧠 contextEngineeringExpert", "Expertos", 
                    "Máxima sofisticación con técnicas avanzadas", 
                    "• Multi-phase thinking protocols\n   • 12K+ caracteres de prompt\n   • Ideal para: Contenido profesional"),

                new TemplateInfo("📚 studentFocused", "Estudiantes", 
                    "Enfoque pedagógico y explicativo paso a paso", 
                    "• Scaffolding educativo\n   • Checkpoints de validación\n   • Ideal para: Aprendizaje guiado"),

                new TemplateInfo("👨‍💻 seniorDeveloper", "Desarrolladores Senior", 
                    "Enfoque técnico, arquitectónico y enterprise", 
                    "• Criterios enterprise-grade\n   • Análisis arquitectónico\n   • Ideal para: Tech leads, seniors"),

                new TemplateInfo("🎨 customPromptGeneration", "Personalizable", 
                    "Template completamente customizable", 
                    "• 2 parámetros personalizables\n   • Máxima flexibilidad\n   • Ideal para: Casos específicos")
            };

            foreach (var template in templates)
            {
                Console.WriteLine($"🔹 **{template.Name}** - *{template.Audience}*");
                Console.WriteLine($"   📄 {template.Description}");
                Console.WriteLine($"   {template.Features}");
                Console.WriteLine();
            }
        }

        private static void ShowSubMenuFlows()
        {
            Console.WriteLine("🌊 FLUJOS DE SUBMENÚS");
            Console.WriteLine("=" + new string('-', 50));
            Console.WriteLine();

            Console.WriteLine("📚 **OPCIÓN 1 - Demo Original:**");
            Console.WriteLine("   1️⃣ Seleccionar Topic → 2️⃣ Seleccionar Level → 3️⃣ Seleccionar Type → 4️⃣ Generar");
            Console.WriteLine("   📊 18 topics × 3 levels × 8 types = 432 combinaciones posibles");
            Console.WriteLine();

            Console.WriteLine("🚀 **OPCIÓN 2 - Demo Expandido:**");
            Console.WriteLine("   1️⃣ Seleccionar Módulo → 2️⃣ Configurar Mentor → 3️⃣ Generar con IA");
            Console.WriteLine("   🎯 Módulos: Beginner Journey, Advanced Track, Custom Module");
            Console.WriteLine();

            Console.WriteLine("🤖 **OPCIÓN 6 - Generador IA Interactivo:**");
            Console.WriteLine("   1️⃣ Listar Templates → 2️⃣ Seleccionar Template → 3️⃣ Configurar Parámetros → 4️⃣ Generar y Guardar");
            Console.WriteLine("   💾 Auto-guardado en generated-exercises/ con timestamp");
            Console.WriteLine();

            Console.WriteLine("🔄 **SISTEMA DE FALLBACKS:**");
            Console.WriteLine("   ✅ Ejercicio Existente → ❌ Ejercicio IA → ❌ Prompt Para Usuario");
            Console.WriteLine("   🎯 Garantiza que siempre se genere contenido útil");
        }

        private static void ShowTechnicalDetails()
        {
            Console.WriteLine("🔧 DETALLES TÉCNICOS");
            Console.WriteLine("=" + new string('-', 50));
            Console.WriteLine();

            Console.WriteLine("📁 **ESTRUCTURA DE ARCHIVOS:**");
            Console.WriteLine("   📂 jsondata/config/ - Configuraciones del sistema");
            Console.WriteLine("   📂 jsondata/{level}/{topic}/{type}/ - Ejercicios por categoría");
            Console.WriteLine("   📂 generated-exercises/ - Output de ejercicios generados");
            Console.WriteLine("   📂 Configuration/ - Clases de configuración C#");
            Console.WriteLine();

            Console.WriteLine("⚙️ **ARCHIVOS DE CONFIGURACIÓN:**");
            Console.WriteLine("   🔸 topics.json - 18 áreas temáticas configurables");
            Console.WriteLine("   🔸 exercise-types.json - 8 tipos de ejercicios");
            Console.WriteLine("   🔸 skill-levels.json - 3 niveles de habilidad");
            Console.WriteLine("   🔸 prompt-templates.json - 6 templates de IA");
            Console.WriteLine("   🔸 dependencies.json - Sistema de dependencias");
            Console.WriteLine();

            Console.WriteLine("🎯 **MÉTRICAS DE RENDIMIENTO:**");
            Console.WriteLine("   ⚡ Generación individual: 2-5 segundos");
            Console.WriteLine("   ⚡ Carga de configuración: <100ms");
            Console.WriteLine("   ⚡ Validación de templates: <50ms");
            Console.WriteLine("   ⚡ Prompts generados: 1K-12K+ caracteres");
        }

        private static void ShowUsageRecommendations()
        {
            Console.WriteLine("💡 RECOMENDACIONES DE USO");
            Console.WriteLine("=" + new string('-', 50));
            Console.WriteLine();

            Console.WriteLine("🎓 **PARA EDUCADORES:**");
            Console.WriteLine("   • Usar Opción 6 con template 'studentFocused' para contenido pedagógico");
            Console.WriteLine("   • Probar Opción 2 para módulos de aprendizaje estructurados");
            Console.WriteLine("   • Personalizar mentor en configuración para adaptar estilo");
            Console.WriteLine();

            Console.WriteLine("👨‍💻 **PARA DESARROLLADORES:**");
            Console.WriteLine("   • Usar Opción 6 con template 'seniorDeveloper' para desafíos técnicos");
            Console.WriteLine("   • Explorar Opción 1 para generación rápida de ejercicios básicos");
            Console.WriteLine("   • Modificar JSONs en jsondata/config/ para personalizar");
            Console.WriteLine();

            Console.WriteLine("🏢 **PARA ORGANIZACIONES:**");
            Console.WriteLine("   • Template 'contextEngineeringExpert' para contenido profesional");
            Console.WriteLine("   • Sistema configurable (Opción 3) para validar configuraciones custom");
            Console.WriteLine("   • Integrar con sistemas LMS existentes via archivos generados");
            Console.WriteLine();

            Console.WriteLine("🔬 **PARA TESTING Y DESARROLLO:**");
            Console.WriteLine("   • Opción 4 para validar que todos los flujos funcionen");
            Console.WriteLine("   • Opción 5 para probar templates de IA");
            Console.WriteLine("   • Opción 3 para validar configuraciones JSON");
        }

        private class MenuOption
        {
            public string Key { get; }
            public string Title { get; }
            public string Description { get; }
            public string Features { get; }

            public MenuOption(string key, string title, string description, string features)
            {
                Key = key;
                Title = title;
                Description = description;
                Features = features;
            }
        }

        private class TemplateInfo
        {
            public string Name { get; }
            public string Audience { get; }
            public string Description { get; }
            public string Features { get; }

            public TemplateInfo(string name, string audience, string description, string features)
            {
                Name = name;
                Audience = audience;
                Description = description;
                Features = features;
            }
        }
    }
}