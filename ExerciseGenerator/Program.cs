using System;
using System.Threading.Tasks;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Programa principal que permite seleccionar entre diferentes demos
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("🚀 GENERADOR DE EJERCICIOS .NET - SISTEMA EXPANDIDO");
            Console.WriteLine("=" + new string('=', 60));
            Console.WriteLine();
            
            Console.WriteLine("Seleccione el demo a ejecutar:");
            Console.WriteLine("1. 📚 Demo Original (DotNetExerciseGenerator)");
            Console.WriteLine("2. 🚀 Demo Expandido (Sistema completo con IA prompts)");
            Console.WriteLine("3. ⚙️  Test Sistema Configurable (Nuevo sistema JSON)");
            Console.WriteLine("4. 🧪 Test Menu Flows (Verificar que todos los flujos funcionen)");
            Console.WriteLine("5. 🎯 Test Prompt Templates (Sistema de templates configurables)");
            Console.WriteLine("6. 🤖 Generador IA Interactivo (Seleccionar template y generar)");
            Console.WriteLine("H. 🗺️  Mapa de Navegación (Guía completa del sistema)");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            Console.Write("Opción: ");
            
            var option = Console.ReadLine()?.ToUpperInvariant();
            
            switch (option)
            {
                case "1":
                    Console.WriteLine("\n🔄 Ejecutando demo original...\n");
                    ExerciseGeneratorDemo.RunOriginalDemo(args);
                    break;
                case "2":
                    Console.WriteLine("\n🔄 Ejecutando demo expandido...\n");
                    ExpandedExerciseDemo.RunExpandedDemo(args);
                    break;
                case "3":
                    Console.WriteLine("\n🔄 Probando sistema configurable...\n");
                    try
                    {
                        TestConfigurableSystem.RunTestAsync().GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error en el test: {ex.Message}");
                    }
                    break;
                case "4":
                    Console.WriteLine("\n🔄 Probando flujos de menús...\n");
                    try
                    {
                        TestMenuFlows.RunTestAsync().GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error en el test de menu flows: {ex.Message}");
                    }
                    break;
                case "5":
                    Console.WriteLine("\n🔄 Probando sistema de prompt templates...\n");
                    try
                    {
                        TestPromptTemplates.RunPromptTemplateTestAsync().GetAwaiter().GetResult();
                        TestPromptTemplates.DemoTemplateInfo().GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error en el test de prompt templates: {ex.Message}");
                    }
                    break;
                case "6":
                    Console.WriteLine("\n🤖 Iniciando Generador IA Interactivo...\n");
                    try
                    {
                        InteractiveAIGenerator.RunInteractiveGeneratorAsync().GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error en el generador IA interactivo: {ex.Message}");
                    }
                    break;
                case "H":
                    Console.WriteLine("\n🗺️ Mostrando Mapa de Navegación...\n");
                    try
                    {
                        NavigationMap.ShowNavigationMap();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error mostrando mapa de navegación: {ex.Message}");
                    }
                    break;
                case "0":
                    Console.WriteLine("👋 ¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("❌ Opción inválida.");
                    break;
            }
        }
    }
}