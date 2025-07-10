using System;
using System.Collections.Generic;

namespace AnalisisEstatico.Ejercicios
{
    /// <summary>
    /// EJERCICIO 1: Código con múltiples problemas detectados por SonarLint
    /// 
    /// INSTRUCCIONES:
    /// 1. Copia este código en Visual Studio
    /// 2. Instala SonarLint si no lo tienes
    /// 3. Observa todos los subrayados de colores
    /// 4. Ve a View -> Error List para ver problemas
    /// 5. Resuelve cada issue usando Alt+Enter
    /// 6. Compara con 01-BadCodeExample-After.cs
    /// 
    /// PROBLEMAS ESPERADOS:
    /// - Campo privado no utilizado
    /// - Comparación redundante con boolean
    /// - Concatenación ineficiente de strings
    /// - Variable que puede ser constante
    /// - Uso innecesario de 'this'
    /// - Falta de validación de parámetros
    /// - Manejo problemático de excepciones
    /// 
    /// TIEMPO ESTIMADO: 30-45 minutos
    /// </summary>
    public class CustomerProcessor
    {
        // ❌ PROBLEMA 1: Campo privado no utilizado
        // SonarLint: "Remove this unused private field 'defaultRegion'."
        // Regla: S1144 - Unused private types or members should be removed
        private string defaultRegion;

        // ❌ PROBLEMA 2: Campo que debería ser readonly
        private string companyName = "TechCorp";

        /// <summary>
        /// Procesa información de cliente con múltiples problemas
        /// </summary>
        /// <param name="customer">Información del cliente</param>
        public void ProcessCustomer(string customer)
        {
            // ❌ PROBLEMA 3: Sin validación de parámetros
            // SonarLint: "Parameter 'customer' could be null. Add null check."

            // ❌ PROBLEMA 4: Variable que puede ser constante
            var greeting = "Welcome";

            // ❌ PROBLEMA 5: Concatenación ineficiente
            Console.WriteLine(greeting + ", " + customer + "!");

            // ❌ PROBLEMA 6: Uso innecesario de 'this'
            Console.WriteLine(this.companyName);
        }

        /// <summary>
        /// Valida cliente con lógica condicional problemática
        /// </summary>
        /// <param name="isActive">Estado del cliente</param>
        /// <returns>Resultado de validación</returns>
        public bool ValidateCustomer(bool isActive)
        {
            // ❌ PROBLEMA 7: Comparación redundante con boolean
            if (isActive == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calcula descuento con magic numbers
        /// </summary>
        /// <param name="amount">Monto</param>
        /// <param name="customerType">Tipo de cliente</param>
        /// <returns>Descuento calculado</returns>
        public decimal calculateDiscount(decimal amount, int customerType)
        {
            // ❌ PROBLEMA 8: Nombre método no PascalCase
            // ❌ PROBLEMA 9: Magic numbers
            switch (customerType)
            {
                case 1:
                    return amount * 0.05m; // 5%
                case 2:
                    return amount * 0.10m; // 10%
                case 3:
                    return amount * 0.15m; // 15%
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Procesa datos con manejo problemático de excepciones
        /// </summary>
        /// <param name="data">Datos a procesar</param>
        public void ProcessData(string data)
        {
            try
            {
                // ❌ PROBLEMA 10: División por cero potencial
                var result = 100 / data.Length;
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                // ❌ PROBLEMA 11: Catch muy genérico
                // ❌ PROBLEMA 12: Exception silenciada
                // No hacer nada con la excepción
            }
        }

        /// <summary>
        /// Método con alta complejidad ciclomática
        /// </summary>
        /// <param name="age">Edad</param>
        /// <param name="income">Ingresos</param>
        /// <param name="hasCredit">Tiene crédito</param>
        /// <param name="region">Región</param>
        /// <returns>Aprobación de crédito</returns>
        public bool ApproveCredit(int age, decimal income, bool hasCredit, string region)
        {
            // ❌ PROBLEMA 13: Complejidad ciclomática alta (>10)
            if (age >= 18)
            {
                if (age <= 65)
                {
                    if (income > 30000)
                    {
                        if (hasCredit == false)
                        {
                            if (region == "North" || region == "South")
                            {
                                if (income > 50000)
                                {
                                    return true;
                                }
                                else
                                {
                                    if (age > 25)
                                    {
                                        return true;
                                    }
                                }
                            }
                            else if (region == "East" || region == "West")
                            {
                                if (income > 40000)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            if (income > 60000)
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
    EJERCICIO PRÁCTICO:
    
    1. INSTALAR SONARLINT:
       - Extensions -> Manage Extensions
       - Buscar "SonarLint"
       - Instalar y reiniciar VS

    2. ANALIZAR PROBLEMAS:
       - View -> Error List
       - Filtrar por "SonarLint"
       - Contar total de issues

    3. RESOLVER UNO POR UNO:
       - Click en línea con problema
       - Alt+Enter para Quick Actions
       - Aplicar corrección sugerida

    4. MÉTRICAS ESPERADAS:
       Antes de corrección:
       - SonarLint Issues: 15+
       - Complejidad Ciclomática ApproveCredit: 12+
       - Mantenibilidad: Baja (< 60)

       Después de corrección:
       - SonarLint Issues: 0
       - Complejidad Ciclomática: < 5 por método
       - Mantenibilidad: Alta (> 80)

    5. VERIFICAR MEJORAS:
       - Analyze -> Calculate Code Metrics
       - Comparar antes/después
       - Documentar aprendizajes

    PRÓXIMO EJERCICIO:
    Ver 01-BadCodeExample-After.cs para la solución completa
    */
}