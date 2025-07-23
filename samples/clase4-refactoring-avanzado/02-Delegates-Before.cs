using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringJr.Ejercicios
{
    /// <summary>
    /// EJERCICIO 2: Delegates, Action y Func - ANTES del refactoring
    /// 
    /// PROBLEMAS IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Código repetitivo con pequeñas variaciones
    /// ❌ PROBLEMA 2: Métodos similares que difieren solo en la lógica interna
    /// ❌ PROBLEMA 3: Imposible parametrizar comportamientos
    /// ❌ PROBLEMA 4: Violación del principio DRY (Don't Repeat Yourself)
    /// ❌ PROBLEMA 5: Difícil agregar nuevas variaciones sin duplicar código
    /// 
    /// OBJETIVO: Usar delegates, Action y Func para eliminar duplicación
    /// CONCEPTOS A APRENDER: Action, Func, Predicate, Delegate básicos
    /// TIEMPO ESTIMADO: 40 minutos
    /// </summary>
    
    public class DataProcessorProblematic
    {
        /// <summary>
        /// ❌ PROBLEMA 1: Procesamiento de listas con lógica similar
        /// Estos 4 métodos tienen la MISMA estructura pero diferente lógica interna
        /// </summary>
        
        // Método 1: Procesar números para duplicarlos
        public List<int> ProcessNumbersDouble(List<int> numbers)
        {
            var result = new List<int>();
            
            Console.WriteLine("Starting to process numbers for doubling...");
            
            foreach (var number in numbers)
            {
                // ❌ Lógica específica: duplicar
                var processedValue = number * 2;
                result.Add(processedValue);
                
                Console.WriteLine($"Processed {number} -> {processedValue}");
            }
            
            Console.WriteLine($"Completed processing. Total items: {result.Count}");
            return result;
        }
        
        // Método 2: Procesar números para elevar al cuadrado  
        public List<int> ProcessNumbersSquare(List<int> numbers)
        {
            var result = new List<int>();
            
            Console.WriteLine("Starting to process numbers for squaring...");
            
            foreach (var number in numbers)
            {
                // ❌ Lógica específica: cuadrado (solo esta línea cambia!)
                var processedValue = number * number;
                result.Add(processedValue);
                
                Console.WriteLine($"Processed {number} -> {processedValue}");
            }
            
            Console.WriteLine($"Completed processing. Total items: {result.Count}");
            return result;
        }
        
        // Método 3: Procesar números para agregar 10
        public List<int> ProcessNumbersAddTen(List<int> numbers)
        {
            var result = new List<int>();
            
            Console.WriteLine("Starting to process numbers for adding ten...");
            
            foreach (var number in numbers)
            {
                // ❌ Lógica específica: sumar 10 (solo esta línea cambia!)
                var processedValue = number + 10;
                result.Add(processedValue);
                
                Console.WriteLine($"Processed {number} -> {processedValue}");
            }
            
            Console.WriteLine($"Completed processing. Total items: {result.Count}");
            return result;
        }
        
        // Método 4: Procesar números para convertir a negativo
        public List<int> ProcessNumbersNegate(List<int> numbers)
        {
            var result = new List<int>();
            
            Console.WriteLine("Starting to process numbers for negation...");
            
            foreach (var number in numbers)
            {
                // ❌ Lógica específica: negar (solo esta línea cambia!)
                var processedValue = -number;
                result.Add(processedValue);
                
                Console.WriteLine($"Processed {number} -> {processedValue}");
            }
            
            Console.WriteLine($"Completed processing. Total items: {result.Count}");
            return result;
        }
    }
    
    public class ValidationProcessorProblematic
    {
        /// <summary>
        /// ❌ PROBLEMA 2: Validaciones con estructura idéntica
        /// Solo cambia la condición de validación
        /// </summary>
        
        // Validar emails
        public List<string> ValidateEmails(List<string> emails)
        {
            var validItems = new List<string>();
            var invalidItems = new List<string>();
            
            Console.WriteLine("Starting email validation...");
            
            foreach (var email in emails)
            {
                // ❌ Lógica específica: validar email
                if (!string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains("."))
                {
                    validItems.Add(email);
                    Console.WriteLine($"✅ Valid email: {email}");
                }
                else
                {
                    invalidItems.Add(email);
                    Console.WriteLine($"❌ Invalid email: {email}");
                }
            }
            
            Console.WriteLine($"Validation completed. Valid: {validItems.Count}, Invalid: {invalidItems.Count}");
            return validItems;
        }
        
        // Validar números positivos
        public List<int> ValidatePositiveNumbers(List<int> numbers)
        {
            var validItems = new List<int>();
            var invalidItems = new List<int>();
            
            Console.WriteLine("Starting positive number validation...");
            
            foreach (var number in numbers)
            {
                // ❌ Lógica específica: validar positivo (solo esta línea cambia!)
                if (number > 0)
                {
                    validItems.Add(number);
                    Console.WriteLine($"✅ Valid positive number: {number}");
                }
                else
                {
                    invalidItems.Add(number);
                    Console.WriteLine($"❌ Invalid positive number: {number}");
                }
            }
            
            Console.WriteLine($"Validation completed. Valid: {validItems.Count}, Invalid: {invalidItems.Count}");
            return validItems;
        }
        
        // Validar strings no vacíos
        public List<string> ValidateNonEmptyStrings(List<string> strings)
        {
            var validItems = new List<string>();
            var invalidItems = new List<string>();
            
            Console.WriteLine("Starting non-empty string validation...");
            
            foreach (var str in strings)
            {
                // ❌ Lógica específica: validar no vacío (solo esta línea cambia!)
                if (!string.IsNullOrWhiteSpace(str))
                {
                    validItems.Add(str);
                    Console.WriteLine($"✅ Valid non-empty string: {str}");
                }
                else
                {
                    invalidItems.Add(str);
                    Console.WriteLine($"❌ Invalid empty string: {str ?? "null"}");
                }
            }
            
            Console.WriteLine($"Validation completed. Valid: {validItems.Count}, Invalid: {invalidItems.Count}");
            return validItems;
        }
    }
    
    public class ReportGeneratorProblematic
    {
        /// <summary>
        /// ❌ PROBLEMA 3: Generación de reportes con formateo similar
        /// Solo cambia cómo se formatea cada elemento
        /// </summary>
        
        // Reporte de empleados
        public void GenerateEmployeeReport(List<Employee> employees)
        {
            Console.WriteLine("=== EMPLOYEE REPORT ===");
            Console.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Total records: {employees.Count}");
            Console.WriteLine(new string('-', 50));
            
            foreach (var employee in employees)
            {
                // ❌ Lógica específica: formatear empleado
                var formatted = $"ID: {employee.Id}, Name: {employee.Name}, " +
                               $"Department: {employee.Department}, Salary: ${employee.Salary:F2}";
                Console.WriteLine(formatted);
            }
            
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("End of Employee Report");
        }
        
        // Reporte de productos
        public void GenerateProductReport(List<Product> products)
        {
            Console.WriteLine("=== PRODUCT REPORT ===");
            Console.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Total records: {products.Count}");
            Console.WriteLine(new string('-', 50));
            
            foreach (var product in products)
            {
                // ❌ Lógica específica: formatear producto (solo esta línea cambia!)
                var formatted = $"SKU: {product.Sku}, Name: {product.Name}, " +
                               $"Category: {product.Category}, Price: ${product.Price:F2}";
                Console.WriteLine(formatted);
            }
            
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("End of Product Report");
        }
        
        // Reporte de órdenes
        public void GenerateOrderReport(List<Order> orders)
        {
            Console.WriteLine("=== ORDER REPORT ===");
            Console.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Total records: {orders.Count}");
            Console.WriteLine(new string('-', 50));
            
            foreach (var order in orders)
            {
                // ❌ Lógica específica: formatear orden (solo esta línea cambia!)
                var formatted = $"Order ID: {order.Id}, Customer: {order.CustomerName}, " +
                               $"Date: {order.OrderDate:yyyy-MM-dd}, Total: ${order.Total:F2}";
                Console.WriteLine(formatted);
            }
            
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("End of Order Report");
        }
    }
    
    public class EventProcessorProblematic
    {
        /// <summary>
        /// ❌ PROBLEMA 4: Procesamiento de eventos con callbacks hardcoded
        /// No se pueden agregar nuevos tipos de procesamiento sin modificar código
        /// </summary>
        
        public void ProcessOrderEvents(List<OrderEvent> events)
        {
            Console.WriteLine("Processing order events...");
            
            foreach (var evt in events)
            {
                // ❌ Procesamiento hardcoded: no se puede cambiar sin modificar código
                Console.WriteLine($"Processing event: {evt.EventType} at {evt.Timestamp}");
                
                if (evt.EventType == "ORDER_CREATED")
                {
                    Console.WriteLine($"New order created: {evt.OrderId}");
                    // Lógica específica hardcoded
                }
                else if (evt.EventType == "ORDER_SHIPPED")
                {
                    Console.WriteLine($"Order shipped: {evt.OrderId}");
                    // Lógica específica hardcoded
                }
                else if (evt.EventType == "ORDER_DELIVERED")
                {
                    Console.WriteLine($"Order delivered: {evt.OrderId}");
                    // Lógica específica hardcoded
                }
                
                Console.WriteLine($"Event {evt.Id} processed successfully");
            }
            
            Console.WriteLine("All events processed");
        }
        
        public void ProcessUserEvents(List<UserEvent> events)
        {
            Console.WriteLine("Processing user events...");
            
            foreach (var evt in events)
            {
                // ❌ CÓDIGO DUPLICADO con pequeñas variaciones
                Console.WriteLine($"Processing event: {evt.EventType} at {evt.Timestamp}");
                
                if (evt.EventType == "USER_REGISTERED")
                {
                    Console.WriteLine($"New user registered: {evt.UserId}");
                    // Lógica específica hardcoded
                }
                else if (evt.EventType == "USER_LOGIN")
                {
                    Console.WriteLine($"User logged in: {evt.UserId}");
                    // Lógica específica hardcoded
                }
                else if (evt.EventType == "USER_LOGOUT")
                {
                    Console.WriteLine($"User logged out: {evt.UserId}");
                    // Lógica específica hardcoded
                }
                
                Console.WriteLine($"Event {evt.Id} processed successfully");
            }
            
            Console.WriteLine("All events processed");
        }
    }
    
    public class CalculationEngineProblematic
    {
        /// <summary>
        /// ❌ PROBLEMA 5: Cálculos con aggregation patterns repetidos
        /// Solo cambia la operación matemática
        /// </summary>
        
        public decimal CalculateSum(List<decimal> values)
        {
            Console.WriteLine("Starting sum calculation...");
            
            if (values == null || values.Count == 0)
            {
                Console.WriteLine("No values to calculate");
                return 0;
            }
            
            decimal result = 0;
            foreach (var value in values)
            {
                // ❌ Operación específica: suma
                result += value;
                Console.WriteLine($"Current sum: {result}");
            }
            
            Console.WriteLine($"Sum calculation completed: {result}");
            return result;
        }
        
        public decimal CalculateProduct(List<decimal> values)
        {
            Console.WriteLine("Starting product calculation...");
            
            if (values == null || values.Count == 0)
            {
                Console.WriteLine("No values to calculate");
                return 0;
            }
            
            decimal result = 1; // Diferente valor inicial
            foreach (var value in values)
            {
                // ❌ Operación específica: multiplicación (solo esta línea cambia!)
                result *= value;
                Console.WriteLine($"Current product: {result}");
            }
            
            Console.WriteLine($"Product calculation completed: {result}");
            return result;
        }
        
        public decimal CalculateMax(List<decimal> values)
        {
            Console.WriteLine("Starting max calculation...");
            
            if (values == null || values.Count == 0)
            {
                Console.WriteLine("No values to calculate");
                return 0;
            }
            
            decimal result = decimal.MinValue; // Diferente valor inicial
            foreach (var value in values)
            {
                // ❌ Operación específica: máximo (solo esta línea cambia!)
                if (value > result)
                    result = value;
                Console.WriteLine($"Current max: {result}");
            }
            
            Console.WriteLine($"Max calculation completed: {result}");
            return result;
        }
    }
    
    // Supporting classes
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
    
    public class Product
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
    
    public class Order
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }
    
    public class OrderEvent
    {
        public string Id { get; set; }
        public string EventType { get; set; }
        public string OrderId { get; set; }
        public DateTime Timestamp { get; set; }
    }
    
    public class UserEvent
    {
        public string Id { get; set; }
        public string EventType { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

/*
ANÁLISIS DE PROBLEMAS DE DUPLICACIÓN:

1. DATAPROCESSOR:
   - 4 métodos con EXACTAMENTE la misma estructura
   - Solo cambia UNA LÍNEA de lógica en cada método
   - 90% del código es duplicado

2. VALIDATIONPROCESSOR:
   - 3 métodos de validación idénticos en estructura
   - Solo cambia la condición de validación
   - Mismo patrón de logging y contadores

3. REPORTGENERATOR:
   - 3 métodos de reporte con estructura idéntica
   - Solo cambia cómo se formatea cada elemento
   - Headers, footers y contadores duplicados

4. EVENTPROCESSOR:
   - 2 métodos casi idénticos
   - Lógica de procesamiento hardcoded
   - Imposible agregar nuevos tipos sin modificar código

5. CALCULATIONENGINE:
   - 3 métodos con mismo patrón de aggregation
   - Solo cambia la operación matemática
   - Validaciones y logging duplicados

CONCEPTOS A APLICAR:
✅ Action<T> - Para operaciones que no retornan valor
✅ Func<T, TResult> - Para funciones que transforman T en TResult
✅ Predicate<T> - Para validaciones (retorna bool)
✅ Delegates básicos - Para callbacks personalizados

BENEFICIOS ESPERADOS:
- Eliminar 70-80% de código duplicado
- Flexibilidad para agregar nuevos comportamientos
- Código más expresivo y mantenible
- Reutilización de patrones comunes

TIEMPO ESTIMADO: 40 minutos
DIFICULTAD: Intermedio
*/