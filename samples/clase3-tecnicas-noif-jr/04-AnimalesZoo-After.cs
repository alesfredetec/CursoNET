using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJr
{
    /// <summary>
    /// SOLUCIÓN: Uso de polimorfismo para eliminar condicionales.
    /// 
    /// Este ejercicio muestra cómo refactorizar un sistema de gestión de animales
    /// para eliminar condicionales mediante el uso de polimorfismo, creando una
    /// jerarquía de clases donde cada animal implementa su propio comportamiento.
    /// 
    /// MEJORAS IMPLEMENTADAS:
    /// ✅ SOLUCIÓN 1: Crear una jerarquía de clases con una clase base abstracta
    /// ✅ SOLUCIÓN 2: Implementar métodos virtuales para comportamientos específicos
    /// ✅ SOLUCIÓN 3: Eliminar condicionales basados en tipo
    /// ✅ SOLUCIÓN 4: Facilitar la extensión con nuevos tipos de animales
    /// </summary>
    public class SistemaZoologicoConPolimorfismo
    {
        // ✅ SOLUCIÓN 1: Crear clase base abstracta con comportamientos comunes
        /// <summary>
        /// Clase base abstracta que define la estructura y comportamiento común de todos los animales
        /// </summary>
        public abstract class Animal
        {
            // Propiedades comunes a todos los animales
            public string Nombre { get; set; }
            public int Edad { get; set; }

            // Constructor que inicializa las propiedades comunes
            protected Animal(string nombre, int edad)
            {
                Nombre = nombre;
                Edad = edad;
            }

            // ✅ SOLUCIÓN 2: Definir métodos abstractos que cada animal debe implementar
            
            /// <summary>
            /// Método abstracto que define el sonido que hace un animal
            /// Cada clase derivada DEBE implementar este método
            /// </summary>
            public abstract string ObtenerSonido();

            /// <summary>
            /// Método abstracto que define qué come un animal
            /// Cada clase derivada DEBE implementar este método
            /// </summary>
            public abstract string ObtenerAlimentacion();

            /// <summary>
            /// Método abstracto que define cómo se mueve un animal
            /// Cada clase derivada DEBE implementar este método
            /// </summary>
            public abstract string ObtenerMovimiento();

            /// <summary>
            /// Método abstracto que define el hábitat recomendado para un animal
            /// Cada clase derivada DEBE implementar este método
            /// </summary>
            public abstract string ObtenerHabitat();

            /// <summary>
            /// Método para obtener un emoji representativo del animal
            /// Cada clase derivada DEBE implementar este método
            /// </summary>
            public abstract string ObtenerEmoji();

            // Método común a todos los animales que puede utilizar los métodos abstractos
            // gracias al polimorfismo. ¡No necesita saber el tipo específico del animal!
            public string GenerarDescripcion()
            {
                return $"Animal: {Nombre} ({this.GetType().Name})\n" +
                       $"Edad: {Edad} años\n" +
                       $"Sonido: {ObtenerSonido()}\n" +
                       $"Alimentación: {ObtenerAlimentacion()}\n" +
                       $"Movimiento: {ObtenerMovimiento()}\n" +
                       $"Hábitat: {ObtenerHabitat()}";
            }
        }

        // ✅ SOLUCIÓN 3: Implementar clases concretas para cada tipo de animal

        /// <summary>
        /// Clase concreta para representar un León
        /// </summary>
        public class Leon : Animal
        {
            public Leon(string nombre, int edad) : base(nombre, edad) { }

            public override string ObtenerSonido() => "¡Rugido!";
            
            public override string ObtenerAlimentacion() => "Carne";
            
            public override string ObtenerMovimiento() => "Camina y corre en cuatro patas";
            
            public override string ObtenerHabitat() => "Sabana con espacios abiertos";
            
            public override string ObtenerEmoji() => "🦁";
        }

        /// <summary>
        /// Clase concreta para representar un Elefante
        /// </summary>
        public class Elefante : Animal
        {
            public Elefante(string nombre, int edad) : base(nombre, edad) { }

            public override string ObtenerSonido() => "¡Barrito!";
            
            public override string ObtenerAlimentacion() => "Plantas y vegetación";
            
            public override string ObtenerMovimiento() => "Camina lentamente sobre cuatro patas";
            
            public override string ObtenerHabitat() => "Sabana amplia con acceso a agua";
            
            public override string ObtenerEmoji() => "🐘";
        }

        /// <summary>
        /// Clase concreta para representar un Mono
        /// </summary>
        public class Mono : Animal
        {
            public Mono(string nombre, int edad) : base(nombre, edad) { }

            public override string ObtenerSonido() => "¡Aullido!";
            
            public override string ObtenerAlimentacion() => "Frutas y plantas";
            
            public override string ObtenerMovimiento() => "Trepa y salta entre árboles";
            
            public override string ObtenerHabitat() => "Selva con muchos árboles para trepar";
            
            public override string ObtenerEmoji() => "🐒";
        }
        
        /// <summary>
        /// Clase concreta para representar una Jirafa
        /// </summary>
        public class Jirafa : Animal
        {
            public Jirafa(string nombre, int edad) : base(nombre, edad) { }

            public override string ObtenerSonido() => "¡Mugido suave!";
            
            public override string ObtenerAlimentacion() => "Hojas de árboles altos";
            
            public override string ObtenerMovimiento() => "Camina con elegancia en cuatro patas";
            
            public override string ObtenerHabitat() => "Sabana con árboles altos";
            
            public override string ObtenerEmoji() => "🦒";
        }

        /// <summary>
        /// Método que muestra información de todos los animales en el zoológico
        /// ¡Ya no necesita condicionales para determinar el comportamiento!
        /// </summary>
        public void MostrarTodosLosAnimales(List<Animal> animales)
        {
            Console.WriteLine("=== ANIMALES EN EL ZOOLÓGICO ===");
            
            foreach (var animal in animales)
            {
                // ✅ SOLUCIÓN 4: Usar polimorfismo para obtener el comportamiento adecuado
                // sin necesidad de condicionales o conocer el tipo concreto
                Console.WriteLine($"{animal.ObtenerEmoji()} {animal.Nombre} - {animal.ObtenerSonido()}");
            }
            
            Console.WriteLine("==============================");
        }
    }

    // Código para probar el sistema
    public class ProgramZoologico
    {
        public static void Main()
        {
            var sistema = new SistemaZoologicoConPolimorfismo();
            
            // ✅ SOLUCIÓN 5: Crear instancias de las clases específicas en lugar de usar enums
            var leon = new SistemaZoologicoConPolimorfismo.Leon("Simba", 5);
            var elefante = new SistemaZoologicoConPolimorfismo.Elefante("Dumbo", 10);
            var mono = new SistemaZoologicoConPolimorfismo.Mono("Chita", 3);

            // Crear lista de animales - ¡Usando el tipo base Animal para todas las instancias!
            var animales = new List<SistemaZoologicoConPolimorfismo.Animal>
            {
                leon,
                elefante,
                mono
            };

            // Mostrar todos los animales
            sistema.MostrarTodosLosAnimales(animales);

            // Mostrar descripción detallada de un animal
            Console.WriteLine("\nDescripción detallada:");
            // ✅ SOLUCIÓN 6: Usar el método GenerarDescripcion() del propio animal
            Console.WriteLine(leon.GenerarDescripcion());
            
            // ✅ SOLUCIÓN 7: ¡Fácil extensión! Agregar un nuevo tipo de animal sin modificar código existente
            Console.WriteLine("\nAgregando un nuevo tipo de animal (Jirafa):");
            
            // Crear una jirafa y agregarla a nuestra colección
            var jirafa = new SistemaZoologicoConPolimorfismo.Jirafa("Melman", 7);
            animales.Add(jirafa);
            
            // Mostrar todos los animales de nuevo - ¡incluida la jirafa sin modificar MostrarTodosLosAnimales!
            sistema.MostrarTodosLosAnimales(animales);
            
            /*
            VENTAJAS DEL POLIMORFISMO:
            
            1. ELIMINACIÓN DE CONDICIONALES:
               - No hay if-else o switch para determinar comportamientos
               - El sistema no necesita conocer los tipos concretos de animales
            
            2. FÁCIL DE MANTENER:
               - Comportamiento de cada animal encapsulado en su propia clase
               - Cambiar comportamiento sólo requiere modificar una clase
            
            3. CUMPLE CON EL PRINCIPIO OPEN/CLOSED:
               - Se pueden agregar nuevos tipos de animales sin modificar código existente
               - El sistema está abierto a extensión pero cerrado a modificación
            
            4. APROVECHA LA ORIENTACIÓN A OBJETOS:
               - Cada animal encapsula su propio comportamiento
               - Reutilización de código a través de herencia
               - Polimorfismo para tratar todos los animales de manera uniforme
            
            
            DIAGRAMA DE CLASES (ASCII):
            
            +-------------------+
            |      Animal       |
            |-------------------|
            | +Nombre: string   |
            | +Edad: int        |
            |-------------------|
            | +ObtenerSonido()  |
            | +ObtenerAliment.. |
            | +ObtenerMovim...  |
            | +ObtenerHabitat() |
            | +ObtenerEmoji()   |
            | +GenerarDesc...   |
            +--------^----------+
                     |
            +-----------------+-----------------+
            |                 |                 |
            |                 |                 |
    +-------+------+  +-------+------+  +-------+------+
    |     León     |  |   Elefante   |  |     Mono     |
    |--------------|  |--------------|  |--------------|
    |              |  |              |  |              |
    |--------------|  |--------------|  |--------------|
    | +ObtenerS... |  | +ObtenerS... |  | +ObtenerS... |
    | +ObtenerA... |  | +ObtenerA... |  | +ObtenerA... |
    | +ObtenerM... |  | +ObtenerM... |  | +ObtenerM... |
    | +ObtenerH... |  | +ObtenerH... |  | +ObtenerH... |
    | +ObtenerE... |  | +ObtenerE... |  | +ObtenerE... |
    +--------------+  +--------------+  +--------------+
            */
        }
    }
}
