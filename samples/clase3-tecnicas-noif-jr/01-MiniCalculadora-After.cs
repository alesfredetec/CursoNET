using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJunior
{
    /// <summary>
    /// SOLUCIÓN: Una calculadora que usa un Dictionary para seleccionar operaciones
    /// 
    /// MEJORAS:
    /// ✅ Operaciones centralizadas en un solo lugar (el diccionario)
    /// ✅ Fácil de extender con nuevas operaciones sin modificar el método Calcular
    /// ✅ Código más limpio y mantenible
    /// ✅ Cada operación puede probarse por separado
    /// </summary>
    public class MiniCalculadoraMejorada
    {
        // PASO 1: Definimos un tipo de delegado para nuestras operaciones
        // Un delegado es como un "tipo de método" que podemos almacenar en variables
        private delegate double OperacionMatematica(double a, double b);
        
        // PASO 2: Creamos un diccionario que mapea nombres de operaciones a sus implementaciones
        // La clave es el nombre de la operación y el valor es una función que implementa la operación
        private readonly Dictionary<string, OperacionMatematica> _operaciones;
        
        /// <summary>
        /// Constructor donde inicializamos el diccionario con todas las operaciones disponibles
        /// </summary>
        public MiniCalculadoraMejorada()
        {
            // Inicializamos el diccionario y agregamos las operaciones básicas
            _operaciones = new Dictionary<string, OperacionMatematica>
            {
                // Para cada operación, la clave es el nombre y el valor es una función
                ["suma"] = (a, b) => a + b,
                ["resta"] = (a, b) => a - b,
                ["multiplicacion"] = (a, b) => a * b,
                ["division"] = (a, b) => {
                    if (b == 0)
                        throw new DivideByZeroException("No se puede dividir por cero");
                    return a / b;
                }
            };
            
            // BONUS: Podemos agregar más operaciones fácilmente
            _operaciones["potencia"] = (a, b) => Math.Pow(a, b);
            _operaciones["promedio"] = (a, b) => (a + b) / 2;
        }

        /// <summary>
        /// Realiza la operación seleccionada usando el diccionario
        /// </summary>
        public double Calcular(double a, double b, string operacion)
        {
            // Convertimos a minúsculas para hacer la búsqueda sin importar mayúsculas/minúsculas
            string opClave = operacion.ToLower();
            
            // Verificamos si la operación existe en nuestro diccionario
            if (_operaciones.ContainsKey(opClave))
            {
                // Si existe, obtenemos la función correspondiente y la ejecutamos
                return _operaciones[opClave](a, b);
            }
            
            // Si la operación no existe, lanzamos una excepción
            throw new ArgumentException($"Operación '{operacion}' no reconocida");
        }
        
        /// <summary>
        /// Agrega una nueva operación a la calculadora
        /// </summary>
        public void AgregarOperacion(string nombre, Func<double, double, double> operacion)
        {
            // Validamos que el nombre no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la operación no puede estar vacío");
            
            // Agregamos la nueva operación al diccionario
            _operaciones[nombre.ToLower()] = operacion;
            Console.WriteLine($"Operación '{nombre}' agregada correctamente");
        }
        
        /// <summary>
        /// Retorna la lista de operaciones disponibles
        /// </summary>
        public List<string> ObtenerOperacionesDisponibles()
        {
            // Convertimos las claves del diccionario a una lista
            return new List<string>(_operaciones.Keys);
        }

        /// <summary>
        /// Método principal que ejecuta la calculadora
        /// </summary>
        public void Ejecutar()
        {
            Console.WriteLine("=== Mini Calculadora Mejorada ===");
            
            // Mostramos las operaciones disponibles
            Console.WriteLine("Operaciones disponibles:");
            foreach (var op in ObtenerOperacionesDisponibles())
            {
                Console.WriteLine($"- {op}");
            }
            
            Console.Write("Primer número: ");
            double a = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Segundo número: ");
            double b = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Operación: ");
            string operacion = Console.ReadLine();
            
            try
            {
                double resultado = Calcular(a, b, operacion);
                Console.WriteLine($"Resultado: {resultado}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    
    /*
    VENTAJAS DEL PATRÓN DICCIONARIO:
    
    1. CENTRALIZACIÓN:
       - Todas las operaciones están en un solo lugar (el diccionario)
       - Fácil de ver qué operaciones están disponibles
       
    2. EXTENSIBILIDAD:
       - Podemos agregar nuevas operaciones sin modificar el método Calcular
       - Se pueden agregar operaciones incluso en tiempo de ejecución
       
    3. MANTENIBILIDAD:
       - Código más limpio y organizado
       - Cada operación es independiente de las demás
       
    4. TESTING:
       - Podemos probar cada operación por separado
       - Es más fácil simular (mock) operaciones para pruebas
       
    CUÁNDO USAR ESTE PATRÓN:
    - Cuando tienes un switch/if que selecciona entre diferentes comportamientos
    - Cuando quieres poder agregar nuevos comportamientos sin modificar código existente
    - Para mapeos simples entre un identificador y una acción
    
    CONCEPTOS CLAVE:
    - Dictionary<TKey, TValue>: Colección de pares clave-valor
    - Delegates: Tipos que representan referencias a métodos
    - Func<T, TResult>: Delegado que representa un método que toma un parámetro y devuelve un resultado
    - Lambda expressions (=>): Forma corta de definir funciones anónimas
    */
}
