using System;

namespace TecnicasNoIf.EjerciciosJunior
{
    /// <summary>
    /// PROBLEMA: Una calculadora de descuentos que usa muchos if-else para diferentes tipos de cliente
    /// 
    /// Este ejemplo muestra los problemas de usar if-else para seleccionar algoritmos:
    /// - El código crece mucho al agregar nuevos tipos de descuentos
    /// - Es difícil entender toda la lógica en un solo método
    /// - Cambiar un tipo de descuento puede afectar a los otros
    /// - Es difícil probar cada algoritmo de descuento por separado
    /// </summary>
    public class CalculadoraDescuentos
    {
        /// <summary>
        /// Calcula el descuento según el tipo de cliente usando if-else
        /// </summary>
        public decimal CalcularDescuento(string tipoCliente, decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad)
        {
            // Usamos if-else para decidir qué algoritmo de descuento aplicar
            decimal descuento = 0;
            
            // ❌ PROBLEMA: Cada vez que agregamos un nuevo tipo, este método crece
            if (tipoCliente == "Normal")
            {
                // Algoritmo para cliente normal
                descuento = 0.05m; // 5% descuento base
                
                if (tieneTarjetaFidelidad)
                {
                    descuento += 0.02m; // 2% extra por tarjeta
                }
                
                if (esFindelSemana)
                {
                    descuento += 0.01m; // 1% extra fin de semana
                }
            }
            else if (tipoCliente == "Premium")
            {
                // Algoritmo para cliente premium
                descuento = 0.10m; // 10% descuento base
                
                if (tieneTarjetaFidelidad)
                {
                    descuento += 0.05m; // 5% extra por tarjeta
                }
                
                if (esFindelSemana)
                {
                    descuento += 0.05m; // 5% extra fin de semana
                }
            }
            else if (tipoCliente == "Estudiante")
            {
                // Algoritmo para estudiante
                descuento = 0.15m; // 15% descuento base
                
                // Los estudiantes no reciben descuento extra por tarjeta
                
                if (esFindelSemana)
                {
                    descuento += 0.05m; // 5% extra fin de semana
                }
                
                // Los estudiantes tienen un límite de descuento
                if (descuento > 0.20m)
                {
                    descuento = 0.20m; // Máximo 20%
                }
            }
            else
            {
                // Tipo de cliente no reconocido
                throw new ArgumentException($"Tipo de cliente '{tipoCliente}' no válido");
            }
            
            // Calculamos el precio con descuento
            return precioBase * (1 - descuento);
        }
        
        /// <summary>
        /// Muestra el descuento en formato texto - más if-else
        /// </summary>
        public string ObtenerDescripcion(string tipoCliente)
        {
            // ❌ PROBLEMA: Otro método con la misma estructura if-else
            if (tipoCliente == "Normal")
            {
                return "Cliente Normal: 5% base + 2% con tarjeta + 1% fin de semana";
            }
            else if (tipoCliente == "Premium")
            {
                return "Cliente Premium: 10% base + 5% con tarjeta + 5% fin de semana";
            }
            else if (tipoCliente == "Estudiante")
            {
                return "Cliente Estudiante: 15% base + 5% fin de semana (máximo 20%)";
            }
            else
            {
                return "Tipo de cliente desconocido";
            }
        }
        
        /// <summary>
        /// Método para procesar una compra
        /// </summary>
        public void ProcesarCompra(string tipoCliente, decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad)
        {
            try
            {
                decimal precioFinal = CalcularDescuento(tipoCliente, precioBase, esFindelSemana, tieneTarjetaFidelidad);
                
                Console.WriteLine($"=== Detalles de la compra ===");
                Console.WriteLine($"Tipo de cliente: {tipoCliente}");
                Console.WriteLine($"Precio base: ${precioBase:F2}");
                Console.WriteLine($"Fin de semana: {(esFindelSemana ? "Sí" : "No")}");
                Console.WriteLine($"Tarjeta de fidelidad: {(tieneTarjetaFidelidad ? "Sí" : "No")}");
                Console.WriteLine($"Descuento aplicado: {ObtenerDescripcion(tipoCliente)}");
                Console.WriteLine($"Precio final: ${precioFinal:F2}");
                Console.WriteLine($"Ahorro: ${precioBase - precioFinal:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar la compra: {ex.Message}");
            }
        }
    }
    
    /*
    PROBLEMAS DE ESTE CÓDIGO:
    
    1. EXTENSIBILIDAD LIMITADA:
       - Para agregar un nuevo tipo de cliente hay que modificar todos los métodos
       - Cada nuevo tipo hace crecer el método CalcularDescuento
       
    2. VIOLACIÓN PRINCIPIO SOLID:
       - Single Responsibility: Un método hace demasiadas cosas
       - Open/Closed: No es "abierto a extensión, cerrado a modificación"
       
    3. TESTING DIFÍCIL:
       - Para probar un tipo hay que navegar por todas las condiciones
       - Difícil aislar cada algoritmo de descuento
       
    4. DUPLICACIÓN DE ESTRUCTURA:
       - Misma estructura if-else se repite en varios métodos
       
    5. MANTENIMIENTO COMPLICADO:
       - Un cambio en la lógica de un tipo puede afectar a otros
       - Fácil introducir errores al agregar nuevos tipos
    */
}
