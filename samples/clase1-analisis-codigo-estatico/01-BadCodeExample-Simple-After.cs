using System;

namespace AnalisisEstatico.Ejercicios.Simple
{
    /// <summary>
    /// SOLUCIÓN SIMPLE: Código arreglado para desarrolladores Jr
    /// 
    /// MEJORAS APLICADAS:
    /// ✅ Eliminé variables que no se usaban
    /// ✅ Arreglé nombres de métodos (deben empezar con mayúscula)
    /// ✅ Eliminé comparaciones raras con true/false
    /// ✅ Cambié números "mágicos" por constantes con nombres claros
    /// ✅ Uso interpolación de strings (más rápido y claro)
    /// ✅ Agregué validaciones para evitar errores
    /// ✅ Simplifiqué condiciones complicadas
    /// 
    /// RESULTADO:
    /// ✅ 0 problemas de SonarLint
    /// ✅ Código más fácil de leer
    /// ✅ Menos posibilidades de errores
    /// </summary>
    public class CustomerServiceMejorado
    {
        // ✅ MEJORA 1: Eliminé 'region' porque no se usaba
        // ✅ MEJORA 2: Hice 'companyName' constante porque nunca cambia
        private const string CompanyName = "MiEmpresa";

        // ✅ MEJORA 3: Constantes para descuentos (en lugar de números mágicos)
        private const decimal DescuentoBasico = 0.05m;    // 5%
        private const decimal DescuentoPremium = 0.10m;   // 10%
        private const decimal DescuentoVip = 0.15m;       // 15%

        // ✅ MEJORA 4: Constantes para validación de crédito
        private const int EdadMinima = 18;
        private const int EdadMaxima = 65;
        private const decimal IngresoMinimo = 30000m;
        private const decimal IngresoAlto = 50000m;
        private const decimal IngresoMuyAlto = 60000m;
        private const int EdadPreferida = 25;

        /// <summary>
        /// Saluda al cliente de forma segura
        /// </summary>
        /// <param name="customerName">Nombre del cliente</param>
        public void SaludarCliente(string customerName)
        {
            // ✅ MEJORA 5: Validación para evitar errores
            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Error: El nombre del cliente no puede estar vacío");
                return;
            }

            // ✅ MEJORA 6: Constante local para valor que no cambia
            const string saludo = "Hola";

            // ✅ MEJORA 7: Interpolación de strings (más rápido y claro)
            Console.WriteLine($"{saludo}, {customerName}!");

            // ✅ MEJORA 8: Eliminé 'this.' innecesario
            Console.WriteLine(CompanyName);
        }

        /// <summary>
        /// Revisa si el cliente está activo (versión simple)
        /// </summary>
        /// <param name="estaActivo">¿El cliente está activo?</param>
        /// <returns>El mismo valor que recibe</returns>
        public bool RevisarCliente(bool estaActivo)
        {
            // ✅ MEJORA 9: Retorno directo, sin comparaciones raras
            return estaActivo;
        }

        /// <summary>
        /// Calcula descuento usando constantes claras
        /// </summary>
        /// <param name="precio">Precio original</param>
        /// <param name="tipoCliente">Tipo de cliente (1=Básico, 2=Premium, 3=VIP)</param>
        /// <returns>Descuento calculado</returns>
        public decimal CalcularDescuento(decimal precio, int tipoCliente)
        {
            // ✅ MEJORA 10: Nombre del método con mayúscula
            // ✅ MEJORA 11: Uso constantes en lugar de números mágicos
            switch (tipoCliente)
            {
                case 1: // Cliente Básico
                    return precio * DescuentoBasico;
                case 2: // Cliente Premium
                    return precio * DescuentoPremium;
                case 3: // Cliente VIP
                    return precio * DescuentoVip;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Procesa datos del cliente con validación
        /// </summary>
        /// <param name="datos">Datos del cliente</param>
        public void ProcesarDatos(string datos)
        {
            // ✅ MEJORA 12: Validación antes de usar los datos
            if (string.IsNullOrEmpty(datos))
            {
                Console.WriteLine("Error: Los datos no pueden estar vacíos");
                return;
            }

            try
            {
                var resultado = 100 / datos.Length;
                Console.WriteLine($"Resultado del procesamiento: {resultado}");
            }
            catch (Exception ex)
            {
                // ✅ MEJORA 13: Manejo de errores con información útil
                Console.WriteLine($"Error al procesar datos: {ex.Message}");
            }
        }

        /// <summary>
        /// Decide si aprobar un crédito usando validaciones simples
        /// </summary>
        /// <param name="edad">Edad del cliente</param>
        /// <param name="ingresos">Ingresos mensuales</param>
        /// <param name="tieneCredito">¿Ya tiene crédito?</param>
        /// <returns>true si se aprueba, false si no</returns>
        public bool AprobarCredito(int edad, decimal ingresos, bool tieneCredito)
        {
            // ✅ MEJORA 14: Validaciones simples al inicio (Guard Clauses)
            // Si no cumple algo básico, salimos inmediatamente
            if (edad < EdadMinima || edad > EdadMaxima)
                return false;

            if (ingresos <= IngresoMinimo)
                return false;

            // ✅ MEJORA 15: Lógica simplificada y clara
            if (tieneCredito)
            {
                // Si ya tiene crédito, necesita ingresos muy altos
                return ingresos > IngresoMuyAlto;
            }
            else
            {
                // Si no tiene crédito, puede calificar con ingresos altos
                // o con ingresos normales pero siendo mayor de 25
                return ingresos > IngresoAlto || edad > EdadPreferida;
            }
        }
    }

    /*
    COMPARACIÓN ANTES VS DESPUÉS:
    
    ANTES:
    ❌ 12+ problemas de SonarLint
    ❌ Difícil de leer y entender
    ❌ Números sin explicación
    ❌ Posibles errores por datos null
    ❌ Lógica complicada y anidada
    
    DESPUÉS:
    ✅ 0 problemas de SonarLint
    ✅ Código claro y fácil de leer
    ✅ Constantes con nombres que explican qué significan
    ✅ Validaciones que previenen errores
    ✅ Lógica simple y directa
    
    TÉCNICAS QUE APRENDISTE:
    
    1. GUARD CLAUSES (Validaciones al inicio):
       - En lugar de if dentro de if dentro de if
       - Validamos condiciones básicas primero
       - Si no se cumple, salimos inmediatamente
       - El código queda más plano y fácil de leer
    
    2. CONSTANTES NOMBRADAS:
       - En lugar de números mágicos como 0.05m
       - Usamos const decimal DescuentoBasico = 0.05m;
       - Más fácil de entender y cambiar después
    
    3. VALIDACIÓN DE DATOS:
       - Siempre verificar si los datos son válidos
       - Usar string.IsNullOrWhiteSpace() para textos
       - Dar mensajes de error claros
    
    4. INTERPOLACIÓN DE STRINGS:
       - En lugar de: "Hola" + ", " + nombre + "!"
       - Usar: $"Hola, {nombre}!"
       - Más rápido y fácil de leer
    
    5. NOMBRES DESCRIPTIVOS:
       - Los nombres deben explicar qué hace cada cosa
       - EdadMinima es mejor que 18
       - CalcularDescuento es mejor que calcularDescuento
    
    CONSEJOS PARA RECORDAR:
    
    - SonarLint te ayuda a encontrar problemas automáticamente
    - Siempre valida los datos antes de usarlos
    - Usa nombres que expliquen qué hace cada cosa
    - Mantén las condiciones simples y claras
    - Si tienes números en el código, probablemente necesitan un nombre
    
    PRÓXIMO PASO:
    Clase 2: Análisis de Requisitos - Aprende a entender qué necesita el cliente
    */
}