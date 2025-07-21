using System;

namespace TecnicasNoIf.EjerciciosJunior
{
    /// <summary>
    /// PROBLEMA: Una calculadora simple que usa un switch para seleccionar operaciones
    /// 
    /// Este ejemplo muestra los problemas de usar switch/if para seleccionar operaciones:
    /// - Si queremos agregar una nueva operación, debemos modificar el método Calculate
    /// - El código es más largo y menos legible con muchas operaciones
    /// - Es difícil probar cada operación por separado
    /// </summary>
    public class MiniCalculadora
    {
        /// <summary>
        /// Realiza operaciones básicas usando switch
        /// </summary>
        public double Calcular(double a, double b, string operacion)
        {
            // Utilizamos switch para decidir qué operación realizar
            switch (operacion.ToLower())
            {
                case "suma":
                    return a + b;
                
                case "resta":
                    return a - b;
                
                case "multiplicacion":
                    return a * b;
                
                case "division":
                    if (b == 0)
                        throw new DivideByZeroException("No se puede dividir por cero");
                    return a / b;
                
                default:
                    throw new ArgumentException($"Operación '{operacion}' no reconocida");
            }
        }

        /// <summary>
        /// Método principal que ejecuta la calculadora
        /// </summary>
        public void Ejecutar()
        {
            Console.WriteLine("=== Mini Calculadora ===");
            Console.WriteLine("Operaciones: suma, resta, multiplicacion, division");
            
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
}
