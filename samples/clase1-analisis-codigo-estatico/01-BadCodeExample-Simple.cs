using System;

namespace AnalisisEstatico.Ejercicios.Simple
{
    /// <summary>
    /// EJERCICIO SIMPLE: Código con problemas básicos para desarrolladores Jr
    /// 
    /// INSTRUCCIONES:
    /// 1. Copia este código en Visual Studio
    /// 2. Instala SonarLint (Extensions -> SonarLint for Visual Studio)
    /// 3. Observa las líneas subrayadas de colores
    /// 4. Ve a View -> Error List para ver los problemas
    /// 5. Arregla cada problema uno por uno
    /// 6. Compara con la solución en 01-BadCodeExample-Simple-After.cs
    /// 
    /// PROBLEMAS QUE VAS A ENCONTRAR:
    /// 🔴 Variables que no usas
    /// 🔴 Nombres de métodos incorrectos
    /// 🔴 Comparaciones raras con true/false
    /// 🔴 Números "mágicos" (sin explicación)
    /// 🔴 Texto concatenado de forma lenta
    /// 
    /// TIEMPO: 20-30 minutos
    /// </summary>
    public class CustomerService
    {
        // ❌ PROBLEMA 1: Esta variable no se usa para nada
        private string region = "North";

        // ❌ PROBLEMA 2: Este texto nunca cambia, debería ser constante
        private string companyName = "MiEmpresa";

        /// <summary>
        /// Saluda al cliente
        /// </summary>
        /// <param name="customerName">Nombre del cliente</param>
        public void SaludarCliente(string customerName)
        {
            // ❌ PROBLEMA 3: No validamos si customerName es null
            // ¿Qué pasa si alguien pasa null?

            // ❌ PROBLEMA 4: Esta variable nunca cambia, debería ser constante
            string saludo = "Hola";

            // ❌ PROBLEMA 5: Esta forma de unir texto es lenta
            Console.WriteLine(saludo + ", " + customerName + "!");

            // ❌ PROBLEMA 6: No necesitas escribir 'this.'
            Console.WriteLine(this.companyName);
        }

        /// <summary>
        /// Revisa si el cliente está activo
        /// </summary>
        /// <param name="estaActivo">¿El cliente está activo?</param>
        /// <returns>true si está activo, false si no</returns>
        public bool RevisarCliente(bool estaActivo)
        {
            // ❌ PROBLEMA 7: ¿Para qué comparar con true?
            if (estaActivo == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calcula descuento del cliente
        /// </summary>
        /// <param name="precio">Precio original</param>
        /// <param name="tipoCliente">Tipo de cliente (1, 2, o 3)</param>
        /// <returns>Descuento calculado</returns>
        public decimal calcularDescuento(decimal precio, int tipoCliente)
        {
            // ❌ PROBLEMA 8: Nombre del método debería empezar con mayúscula
            // ❌ PROBLEMA 9: ¿Qué significan estos números?
            switch (tipoCliente)
            {
                case 1:
                    return precio * 0.05m; // ¿5% de qué?
                case 2:
                    return precio * 0.10m; // ¿10% de qué?
                case 3:
                    return precio * 0.15m; // ¿15% de qué?
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Procesa datos del cliente
        /// </summary>
        /// <param name="datos">Datos del cliente</param>
        public void ProcesarDatos(string datos)
        {
            try
            {
                // ❌ PROBLEMA 10: ¿Qué pasa si datos es null o vacío?
                var resultado = 100 / datos.Length;
                Console.WriteLine(resultado);
            }
            catch (Exception ex)
            {
                // ❌ PROBLEMA 11: Atrapamos todos los errores, pero no hacemos nada
                // Esto es malo porque "esconde" los problemas
            }
        }

        /// <summary>
        /// Decide si aprobar un crédito
        /// </summary>
        /// <param name="edad">Edad del cliente</param>
        /// <param name="ingresos">Ingresos mensuales</param>
        /// <param name="tieneCredito">¿Ya tiene crédito?</param>
        /// <returns>true si se aprueba, false si no</returns>
        public bool AprobarCredito(int edad, decimal ingresos, bool tieneCredito)
        {
            // ❌ PROBLEMA 12: Demasiadas condiciones anidadas (una dentro de otra)
            // Es difícil de leer y entender
            if (edad >= 18)
            {
                if (edad <= 65)
                {
                    if (ingresos > 30000)
                    {
                        if (tieneCredito == false)
                        {
                            if (ingresos > 50000)
                            {
                                return true;
                            }
                            else
                            {
                                if (edad > 25)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (ingresos > 60000)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }

    /*
    EJERCICIO PASO A PASO:
    
    1. INSTALAR SONARLINT:
       - Ve a Extensions -> Manage Extensions
       - Busca "SonarLint" 
       - Instala y reinicia Visual Studio
    
    2. VER LOS PROBLEMAS:
       - Ve a View -> Error List
       - Verás una lista de problemas
       - Cuenta cuántos problemas encuentras
    
    3. ARREGLAR UNO POR UNO:
       - Haz clic en una línea con problema
       - Presiona Alt+Enter
       - Ve las sugerencias de arreglo
       - Aplica la corrección
    
    4. PREGUNTAS PARA REFLEXIONAR:
       - ¿Por qué estos problemas son importantes?
       - ¿Cómo afectan al rendimiento del programa?
       - ¿Qué pasaría si otro programador lee este código?
    
    5. VALIDAR MEJORAS:
       - Cuando termines, deberías tener 0 problemas
       - El código debería ser más fácil de leer
       - Los nombres deberían explicar mejor qué hace cada cosa
    
    SIGUIENTE PASO:
    Compara tu resultado con 01-BadCodeExample-Simple-After.cs
    */
}