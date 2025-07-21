using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJr
{
    /// <summary>
    /// PROBLEMA: Uso excesivo de condicionales para determinar comportamiento basado en el tipo.
    /// 
    /// Este ejercicio simula un sistema simple de gestión de animales en un zoológico
    /// donde se usan condicionales para determinar cómo suena cada animal, qué come
    /// y cómo se mueve, en lugar de usar polimorfismo.
    /// 
    /// ISSUES IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Condicionales repetitivos para determinar comportamiento por tipo
    /// ❌ PROBLEMA 2: Duplicación de lógica en múltiples métodos
    /// ❌ PROBLEMA 3: Difícil de mantener y extender con nuevos tipos de animales
    /// ❌ PROBLEMA 4: No aprovecha las ventajas de la programación orientada a objetos
    /// </summary>
    public class SistemaZoologicoConCondicionales
    {
        // Tipos de animales disponibles en nuestro zoológico
        public enum TipoAnimal
        {
            Leon,
            Elefante,
            Mono
        }

        // Clase que representa un animal en nuestro zoológico
        public class Animal
        {
            public string Nombre { get; set; }
            public TipoAnimal Tipo { get; set; }
            public int Edad { get; set; }

            // Constructor simple para crear un animal
            public Animal(string nombre, TipoAnimal tipo, int edad)
            {
                Nombre = nombre;
                Tipo = tipo;
                Edad = edad;
            }
        }

        /// <summary>
        /// Método que determina el sonido que hace cada animal basado en su tipo
        /// </summary>
        public string ObtenerSonido(Animal animal)
        {
            // ❌ PROBLEMA 1: Condicional que se repite en cada método
            if (animal.Tipo == TipoAnimal.Leon)
            {
                return "¡Rugido!";
            }
            else if (animal.Tipo == TipoAnimal.Elefante)
            {
                return "¡Barrito!";
            }
            else if (animal.Tipo == TipoAnimal.Mono)
            {
                return "¡Aullido!";
            }
            else
            {
                return "Desconocido";
            }
        }

        /// <summary>
        /// Método que determina qué come cada animal basado en su tipo
        /// </summary>
        public string ObtenerAlimentacion(Animal animal)
        {
            // ❌ PROBLEMA 1: Misma estructura condicional repetida
            if (animal.Tipo == TipoAnimal.Leon)
            {
                return "Carne";
            }
            else if (animal.Tipo == TipoAnimal.Elefante)
            {
                return "Plantas y vegetación";
            }
            else if (animal.Tipo == TipoAnimal.Mono)
            {
                return "Frutas y plantas";
            }
            else
            {
                return "Alimento desconocido";
            }
        }

        /// <summary>
        /// Método que determina cómo se mueve cada animal basado en su tipo
        /// </summary>
        public string ObtenerMovimiento(Animal animal)
        {
            // ❌ PROBLEMA 1: Tercera repetición de la misma estructura
            if (animal.Tipo == TipoAnimal.Leon)
            {
                return "Camina y corre en cuatro patas";
            }
            else if (animal.Tipo == TipoAnimal.Elefante)
            {
                return "Camina lentamente sobre cuatro patas";
            }
            else if (animal.Tipo == TipoAnimal.Mono)
            {
                return "Trepa y salta entre árboles";
            }
            else
            {
                return "Movimiento desconocido";
            }
        }

        /// <summary>
        /// Método que determina el hábitat recomendado para cada animal basado en su tipo
        /// </summary>
        public string ObtenerHabitat(Animal animal)
        {
            // ❌ PROBLEMA 1: Cuarta repetición de la misma estructura
            if (animal.Tipo == TipoAnimal.Leon)
            {
                return "Sabana con espacios abiertos";
            }
            else if (animal.Tipo == TipoAnimal.Elefante)
            {
                return "Sabana amplia con acceso a agua";
            }
            else if (animal.Tipo == TipoAnimal.Mono)
            {
                return "Selva con muchos árboles para trepar";
            }
            else
            {
                return "Hábitat desconocido";
            }
        }

        /// <summary>
        /// Método que genera una descripción completa del animal
        /// </summary>
        public string GenerarDescripcion(Animal animal)
        {
            string sonido = ObtenerSonido(animal);
            string alimentacion = ObtenerAlimentacion(animal);
            string movimiento = ObtenerMovimiento(animal);
            string habitat = ObtenerHabitat(animal);

            return $"Animal: {animal.Nombre} ({animal.Tipo})\n" +
                   $"Edad: {animal.Edad} años\n" +
                   $"Sonido: {sonido}\n" +
                   $"Alimentación: {alimentacion}\n" +
                   $"Movimiento: {movimiento}\n" +
                   $"Hábitat: {habitat}";
        }

        /// <summary>
        /// Método que muestra información básica de todos los animales en el zoológico
        /// </summary>
        public void MostrarTodosLosAnimales(List<Animal> animales)
        {
            Console.WriteLine("=== ANIMALES EN EL ZOOLÓGICO ===");
            
            foreach (var animal in animales)
            {
                // ❌ PROBLEMA 1: Más condicionales basados en tipo
                string tipoEmoji;
                
                if (animal.Tipo == TipoAnimal.Leon)
                {
                    tipoEmoji = "🦁";
                }
                else if (animal.Tipo == TipoAnimal.Elefante)
                {
                    tipoEmoji = "🐘";
                }
                else if (animal.Tipo == TipoAnimal.Mono)
                {
                    tipoEmoji = "🐒";
                }
                else
                {
                    tipoEmoji = "❓";
                }
                
                Console.WriteLine($"{tipoEmoji} {animal.Nombre} - {ObtenerSonido(animal)}");
            }
            
            Console.WriteLine("==============================");
        }
    }

    // Código para probar el sistema
    public class ProgramZoologico
    {
        public static void Main()
        {
            var sistema = new SistemaZoologicoConCondicionales();
            
            // Crear algunos animales
            var leon = new SistemaZoologicoConCondicionales.Animal(
                "Simba", 
                SistemaZoologicoConCondicionales.TipoAnimal.Leon, 
                5);
                
            var elefante = new SistemaZoologicoConCondicionales.Animal(
                "Dumbo", 
                SistemaZoologicoConCondicionales.TipoAnimal.Elefante, 
                10);
                
            var mono = new SistemaZoologicoConCondicionales.Animal(
                "Chita", 
                SistemaZoologicoConCondicionales.TipoAnimal.Mono, 
                3);

            // Crear lista de animales
            var animales = new List<SistemaZoologicoConCondicionales.Animal>
            {
                leon,
                elefante,
                mono
            };

            // Mostrar todos los animales
            sistema.MostrarTodosLosAnimales(animales);

            // Mostrar descripción detallada de un animal
            Console.WriteLine("\nDescripción detallada:");
            Console.WriteLine(sistema.GenerarDescripcion(leon));
            
            /*
            PROBLEMAS PRINCIPALES DE ESTE CÓDIGO:
            
            1. CÓDIGO DUPLICADO:
               - La misma estructura de if-else se repite en 5 métodos diferentes
               - Cada vez que agregamos un nuevo tipo de animal, debemos modificar 5 métodos

            2. DIFÍCIL DE MANTENER:
               - Si queremos cambiar el comportamiento de un animal, debemos buscar en 
                 múltiples métodos
               - Es fácil cometer errores y olvidar actualizar algún método

            3. VIOLACIÓN DEL PRINCIPIO OPEN/CLOSED:
               - Para agregar un nuevo tipo de animal, debemos modificar código existente
               - No podemos extender el sistema sin cambiar lo que ya funciona

            4. NO APROVECHA LA ORIENTACIÓN A OBJETOS:
               - Los animales son simples contenedores de datos sin comportamiento propio
               - La lógica está centralizada en el sistema en lugar de en los animales
            */
        }
    }
}
