using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJunior
{
    /// <summary>
    /// SOLUCIÓN: Calculadora de descuentos usando el patrón Strategy
    /// 
    /// MEJORAS:
    /// ✅ Cada tipo de cliente tiene su propia estrategia de descuento
    /// ✅ Fácil agregar nuevos tipos de cliente sin modificar código existente
    /// ✅ Cada algoritmo está encapsulado y puede probarse por separado
    /// ✅ Código más limpio y organizado
    /// ✅ Cumple con el principio Open/Closed
    /// </summary>
    public class CalculadoraDescuentosMejorada
    {
        // PASO 1: Creamos un diccionario que mapea tipos de cliente a sus estrategias
        private readonly Dictionary<string, IEstrategiaDescuento> _estrategias;
        
        /// <summary>
        /// Constructor donde registramos todas las estrategias disponibles
        /// </summary>
        public CalculadoraDescuentosMejorada()
        {
            // Inicializamos el diccionario y registramos las estrategias
            _estrategias = new Dictionary<string, IEstrategiaDescuento>
            {
                ["Normal"] = new EstrategiaNormal(),
                ["Premium"] = new EstrategiaPremium(),
                ["Estudiante"] = new EstrategiaEstudiante()
            };
        }
        
        /// <summary>
        /// Calcula el descuento delegando a la estrategia correspondiente
        /// </summary>
        public decimal CalcularDescuento(string tipoCliente, decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad)
        {
            // Buscamos la estrategia para el tipo de cliente
            if (!_estrategias.TryGetValue(tipoCliente, out var estrategia))
            {
                throw new ArgumentException($"Tipo de cliente '{tipoCliente}' no válido");
            }
            
            // Delegamos el cálculo a la estrategia elegida
            decimal descuento = estrategia.CalcularDescuento(precioBase, esFindelSemana, tieneTarjetaFidelidad);
            return descuento;
        }
        
        /// <summary>
        /// Obtiene la descripción del descuento delegando a la estrategia
        /// </summary>
        public string ObtenerDescripcion(string tipoCliente)
        {
            if (!_estrategias.TryGetValue(tipoCliente, out var estrategia))
            {
                return "Tipo de cliente desconocido";
            }
            
            return estrategia.ObtenerDescripcion();
        }
        
        /// <summary>
        /// Registra una nueva estrategia de descuento
        /// </summary>
        public void RegistrarEstrategia(string tipoCliente, IEstrategiaDescuento estrategia)
        {
            if (string.IsNullOrWhiteSpace(tipoCliente))
                throw new ArgumentException("El tipo de cliente no puede estar vacío");
            
            if (estrategia == null)
                throw new ArgumentNullException(nameof(estrategia));
            
            _estrategias[tipoCliente] = estrategia;
            Console.WriteLine($"Estrategia para '{tipoCliente}' registrada correctamente");
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
    
    // PASO 2: Creamos una interfaz para todas las estrategias de descuento
    /// <summary>
    /// Interfaz que define el contrato para todas las estrategias de descuento
    /// </summary>
    public interface IEstrategiaDescuento
    {
        /// <summary>
        /// Calcula el precio con descuento según la estrategia
        /// </summary>
        decimal CalcularDescuento(decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad);
        
        /// <summary>
        /// Devuelve una descripción del descuento
        /// </summary>
        string ObtenerDescripcion();
    }
    
    // PASO 3: Implementamos estrategias concretas para cada tipo de cliente
    
    /// <summary>
    /// Estrategia de descuento para clientes normales
    /// </summary>
    public class EstrategiaNormal : IEstrategiaDescuento
    {
        public decimal CalcularDescuento(decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad)
        {
            // Algoritmo para cliente normal
            decimal porcentajeDescuento = 0.05m; // 5% descuento base
            
            if (tieneTarjetaFidelidad)
            {
                porcentajeDescuento += 0.02m; // 2% extra por tarjeta
            }
            
            if (esFindelSemana)
            {
                porcentajeDescuento += 0.01m; // 1% extra fin de semana
            }
            
            return precioBase * (1 - porcentajeDescuento);
        }
        
        public string ObtenerDescripcion()
        {
            return "Cliente Normal: 5% base + 2% con tarjeta + 1% fin de semana";
        }
    }
    
    /// <summary>
    /// Estrategia de descuento para clientes premium
    /// </summary>
    public class EstrategiaPremium : IEstrategiaDescuento
    {
        public decimal CalcularDescuento(decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad)
        {
            // Algoritmo para cliente premium
            decimal porcentajeDescuento = 0.10m; // 10% descuento base
            
            if (tieneTarjetaFidelidad)
            {
                porcentajeDescuento += 0.05m; // 5% extra por tarjeta
            }
            
            if (esFindelSemana)
            {
                porcentajeDescuento += 0.05m; // 5% extra fin de semana
            }
            
            return precioBase * (1 - porcentajeDescuento);
        }
        
        public string ObtenerDescripcion()
        {
            return "Cliente Premium: 10% base + 5% con tarjeta + 5% fin de semana";
        }
    }
    
    /// <summary>
    /// Estrategia de descuento para estudiantes
    /// </summary>
    public class EstrategiaEstudiante : IEstrategiaDescuento
    {
        public decimal CalcularDescuento(decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad)
        {
            // Algoritmo para estudiante
            decimal porcentajeDescuento = 0.15m; // 15% descuento base
            
            // Los estudiantes no reciben descuento extra por tarjeta
            
            if (esFindelSemana)
            {
                porcentajeDescuento += 0.05m; // 5% extra fin de semana
            }
            
            // Los estudiantes tienen un límite de descuento
            if (porcentajeDescuento > 0.20m)
            {
                porcentajeDescuento = 0.20m; // Máximo 20%
            }
            
            return precioBase * (1 - porcentajeDescuento);
        }
        
        public string ObtenerDescripcion()
        {
            return "Cliente Estudiante: 15% base + 5% fin de semana (máximo 20%)";
        }
    }
    
    // PASO 4: Ejemplo de uso con una estrategia adicional
    
    /// <summary>
    /// Ejemplo de una nueva estrategia que podemos agregar fácilmente
    /// </summary>
    public class EstrategiaJubilado : IEstrategiaDescuento
    {
        public decimal CalcularDescuento(decimal precioBase, bool esFindelSemana, bool tieneTarjetaFidelidad)
        {
            // Algoritmo especial para jubilados
            decimal porcentajeDescuento = 0.20m; // 20% descuento base
            
            if (tieneTarjetaFidelidad)
            {
                porcentajeDescuento += 0.05m; // 5% extra
            }
            
            // Descuento extra entre semana (no en fin de semana)
            if (!esFindelSemana)
            {
                porcentajeDescuento += 0.10m; // 10% extra entre semana
            }
            
            return precioBase * (1 - porcentajeDescuento);
        }
        
        public string ObtenerDescripcion()
        {
            return "Cliente Jubilado: 20% base + 5% con tarjeta + 10% entre semana";
        }
    }
    
    /// <summary>
    /// Clase para demostrar el uso del patrón Strategy
    /// </summary>
    public class DemoStrategy
    {
        public static void EjecutarDemo()
        {
            Console.WriteLine("=== DEMO: Patrón Strategy para Descuentos ===\n");
            
            // Creamos la calculadora con las estrategias básicas
            var calculadora = new CalculadoraDescuentosMejorada();
            
            // Procesamos compras normales
            calculadora.ProcesarCompra("Normal", 100, false, false);
            calculadora.ProcesarCompra("Premium", 100, true, true);
            calculadora.ProcesarCompra("Estudiante", 100, true, false);
            
            Console.WriteLine("\n=== Agregando una nueva estrategia ===\n");
            
            // Agregamos dinámicamente una nueva estrategia sin modificar el código existente
            calculadora.RegistrarEstrategia("Jubilado", new EstrategiaJubilado());
            
            // Usamos la nueva estrategia
            calculadora.ProcesarCompra("Jubilado", 100, false, true);
        }
    }
    
    /*
    DIAGRAMA DEL PATRÓN STRATEGY:
    
    ┌────────────────────────┐       usa       ┌───────────────────┐
    │CalculadoraDescuentos   │────────────────>│IEstrategiaDescuento│
    └────────────────────────┘                 └─────────┬─────────┘
                                                         │
                                                         │ implementa
                  ┌────────────────┬────────────────┬────▼───────────┐
                  │                │                │                │
    ┌─────────────▼───┐  ┌─────────▼───────┐ ┌──────▼──────┐ ┌───────▼───────┐
    │EstrategiaNormal │  │EstrategiaPremium│ │EstrategiaEst│ │EstrategiaJubil│
    └─────────────────┘  └─────────────────┘ └─────────────┘ └───────────────┘
    
    
    VENTAJAS DEL PATRÓN STRATEGY:
    
    1. EXTENSIBILIDAD:
       - Podemos agregar nuevas estrategias sin modificar código existente
       - Cada estrategia está encapsulada en su propia clase
       
    2. CUMPLIMIENTO PRINCIPIOS SOLID:
       - Single Responsibility: Cada clase tiene una única responsabilidad
       - Open/Closed: Abierto para extensión, cerrado para modificación
       - Dependency Inversion: Dependemos de abstracciones, no concretos
       
    3. TESTING SIMPLIFICADO:
       - Podemos probar cada estrategia por separado
       - Fácil crear mocks para pruebas
       
    4. FLEXIBILIDAD:
       - Las estrategias pueden intercambiarse en tiempo de ejecución
       - Podemos configurar estrategias dinámicamente
       
    CUÁNDO USAR ESTE PATRÓN:
    - Cuando tenemos diferentes variantes de un algoritmo
    - Cuando queremos evitar condicionales complejos
    - Cuando necesitamos cambiar el comportamiento en tiempo de ejecución
    - Cuando hay diferentes comportamientos que comparten una estructura común
    
    CONCEPTOS CLAVE:
    - Context: Clase que utiliza las estrategias (CalculadoraDescuentosMejorada)
    - Strategy: Interfaz común para todas las estrategias (IEstrategiaDescuento)
    - ConcreteStrategy: Implementaciones específicas (EstrategiaNormal, etc.)
    */
}
