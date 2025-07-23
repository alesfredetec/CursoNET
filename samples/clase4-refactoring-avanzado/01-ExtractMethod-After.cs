using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringJr.Ejercicios
{
    /// <summary>
    /// EJERCICIO 1: Extract Method - DESPUÉS del refactoring
    /// 
    /// MEJORAS APLICADAS:
    /// ✅ MEJORA 1: Métodos pequeños con una sola responsabilidad
    /// ✅ MEJORA 2: Complejidad ciclomática reducida (<5 por método)
    /// ✅ MEJORA 3: Nombres descriptivos que explican qué hace cada método
    /// ✅ MEJORA 4: Fácil de leer, entender y mantener
    /// ✅ MEJORA 5: Testeable - cada método se puede probar por separado
    /// 
    /// MÉTRICAS ALCANZADAS:
    /// - Líneas por método: <15
    /// - Complejidad ciclomática: <5
    /// - Responsabilidad única por método
    /// </summary>
    
    public class OrderProcessorRefactored
    {
        /// <summary>
        /// ✅ MÉTODO PRINCIPAL REFACTORIZADO: Solo orquesta el flujo
        /// Complejidad ciclomática: 2
        /// Líneas de código: 12
        /// Responsabilidades: 1 (orchestration)
        /// </summary>
        public bool ProcessCustomerOrder(CustomerOrder order)
        {
            Console.WriteLine($"Starting to process order {order?.Id}");
            
            if (!IsValidOrder(order))
                return false;
                
            CalculateOrderTotals(order);
            
            if (!ProcessInventory(order))
                return false;
                
            SendCustomerNotifications(order);
            LogOrderCompletion(order);
            
            return true;
        }
        
        /// <summary>
        /// ✅ VALIDACIÓN EXTRAÍDA: Solo se encarga de validar
        /// Complejidad ciclomática: 4
        /// Líneas: 8
        /// </summary>
        private bool IsValidOrder(CustomerOrder order)
        {
            if (order == null)
            {
                Console.WriteLine("ERROR: Order is null");
                return false;
            }
            
            if (!IsValidOrderBasicInfo(order))
                return false;
                
            if (!IsValidCustomerInfo(order.Customer))
                return false;
                
            return AreValidOrderItems(order.Items);
        }
        
        /// <summary>
        /// ✅ VALIDACIÓN BÁSICA: Información esencial del pedido
        /// </summary>
        private bool IsValidOrderBasicInfo(CustomerOrder order)
        {
            if (string.IsNullOrEmpty(order.Id))
            {
                Console.WriteLine("ERROR: Order ID is required");
                return false;
            }
            
            if (order.Items == null || order.Items.Count == 0)
            {
                Console.WriteLine("ERROR: Order must have at least one item");
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// ✅ VALIDACIÓN DE CLIENTE: Solo información del cliente
        /// </summary>
        private bool IsValidCustomerInfo(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("ERROR: Customer information is required");
                return false;
            }
            
            if (string.IsNullOrEmpty(customer.Email))
            {
                Console.WriteLine("ERROR: Customer email is required");
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// ✅ VALIDACIÓN DE ITEMS: Solo items del pedido
        /// </summary>
        private bool AreValidOrderItems(List<OrderItem> items)
        {
            foreach (var item in items)
            {
                if (item.Quantity <= 0)
                {
                    Console.WriteLine($"ERROR: Invalid quantity for item {item.ProductId}");
                    return false;
                }
                
                if (item.UnitPrice <= 0)
                {
                    Console.WriteLine($"ERROR: Invalid price for item {item.ProductId}");
                    return false;
                }
            }
            
            return true;
        }
        
        /// <summary>
        /// ✅ CÁLCULOS EXTRAÍDOS: Solo se encarga de calcular totales
        /// Complejidad ciclomática: 1
        /// </summary>
        private void CalculateOrderTotals(CustomerOrder order)
        {
            order.Subtotal = CalculateSubtotal(order.Items);
            order.Taxes = CalculateTaxes(order.Subtotal, order.Customer.State);
            order.Shipping = CalculateShipping(order.Subtotal, order.Customer.IsPremium);
            order.Total = order.Subtotal + order.Taxes + order.Shipping;
        }
        
        /// <summary>
        /// ✅ CÁLCULO DE SUBTOTAL: Incluye descuentos por volumen
        /// </summary>
        private decimal CalculateSubtotal(List<OrderItem> items)
        {
            decimal subtotal = 0;
            
            foreach (var item in items)
            {
                decimal itemTotal = item.Quantity * item.UnitPrice;
                decimal discountedTotal = ApplyVolumeDiscount(itemTotal, item.Quantity);
                subtotal += discountedTotal;
            }
            
            return subtotal;
        }
        
        /// <summary>
        /// ✅ DESCUENTOS POR VOLUMEN: Lógica específica extraída
        /// </summary>
        private decimal ApplyVolumeDiscount(decimal itemTotal, int quantity)
        {
            if (quantity >= 10)
                return itemTotal * 0.9m; // 10% descuento
            
            if (quantity >= 5)
                return itemTotal * 0.95m; // 5% descuento
                
            return itemTotal; // Sin descuento
        }
        
        /// <summary>
        /// ✅ CÁLCULO DE IMPUESTOS: Lógica por estado extraída
        /// </summary>
        private decimal CalculateTaxes(decimal subtotal, string state)
        {
            decimal taxRate = GetTaxRateByState(state);
            return subtotal * taxRate;
        }
        
        /// <summary>
        /// ✅ TASAS DE IMPUESTOS: Configuración centralizada
        /// </summary>
        private decimal GetTaxRateByState(string state)
        {
            return state switch
            {
                "CA" or "NY" => 0.08m, // 8% tax
                "TX" or "FL" => 0.06m, // 6% tax
                _ => 0.04m              // 4% tax default
            };
        }
        
        /// <summary>
        /// ✅ CÁLCULO DE ENVÍO: Lógica específica extraída
        /// </summary>
        private decimal CalculateShipping(decimal subtotal, bool isPremium)
        {
            if (subtotal >= 100)
                return 0; // Free shipping
                
            return isPremium ? 5.99m : 12.99m;
        }
        
        /// <summary>
        /// ✅ PROCESAMIENTO DE INVENTARIO: Responsabilidad extraída
        /// </summary>
        private bool ProcessInventory(CustomerOrder order)
        {
            foreach (var item in order.Items)
            {
                if (!IsProductAvailable(item.ProductId))
                    return false;
                    
                ReserveInventory(item.ProductId, item.Quantity);
            }
            
            return true;
        }
        
        /// <summary>
        /// ✅ VERIFICACIÓN DE DISPONIBILIDAD: Lógica específica
        /// </summary>
        private bool IsProductAvailable(string productId)
        {
            Console.WriteLine($"Checking inventory for product {productId}");
            
            if (productId.StartsWith("DISC"))
            {
                Console.WriteLine($"ERROR: Product {productId} is discontinued");
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// ✅ RESERVA DE INVENTARIO: Operación específica
        /// </summary>
        private void ReserveInventory(string productId, int quantity)
        {
            Console.WriteLine($"Reserved {quantity} units of {productId}");
        }
        
        /// <summary>
        /// ✅ NOTIFICACIONES EXTRAÍDAS: Solo se encarga de notificar
        /// </summary>
        private void SendCustomerNotifications(CustomerOrder order)
        {
            SendConfirmationEmail(order);
            
            if (order.Customer.IsPremium)
                SendPremiumThankYouMessage(order);
        }
        
        /// <summary>
        /// ✅ EMAIL DE CONFIRMACIÓN: Responsabilidad específica
        /// </summary>
        private void SendConfirmationEmail(CustomerOrder order)
        {
            Console.WriteLine($"Sending confirmation email to {order.Customer.Email}");
            Console.WriteLine($"Email subject: Order Confirmation - {order.Id}");
            Console.WriteLine($"Email body: Thank you for your order of ${order.Total:F2}");
        }
        
        /// <summary>
        /// ✅ MENSAJE PREMIUM: Lógica específica para clientes premium
        /// </summary>
        private void SendPremiumThankYouMessage(CustomerOrder order)
        {
            Console.WriteLine("Sending premium customer thank you message");
        }
        
        /// <summary>
        /// ✅ LOGGING EXTRAÍDO: Solo se encarga de registrar
        /// </summary>
        private void LogOrderCompletion(CustomerOrder order)
        {
            Console.WriteLine($"Order {order.Id} processed successfully");
            Console.WriteLine($"Total amount: ${order.Total:F2}");
            Console.WriteLine($"Customer: {order.Customer.Email}");
            Console.WriteLine($"Processing completed at: {DateTime.Now}");
        }
        
        /// <summary>
        /// ✅ GENERACIÓN DE REPORTES REFACTORIZADA
        /// </summary>
        public void GenerateOrderReport(List<CustomerOrder> orders, string reportType)
        {
            Console.WriteLine($"Generating {reportType} report for {orders.Count} orders");
            
            switch (reportType)
            {
                case "SUMMARY":
                    GenerateSummaryReport(orders);
                    break;
                case "DETAILED":
                    GenerateDetailedReport(orders);
                    break;
                default:
                    Console.WriteLine($"Unknown report type: {reportType}");
                    return;
            }
            
            LogReportCompletion();
        }
        
        /// <summary>
        /// ✅ REPORTE RESUMEN: Responsabilidad específica
        /// </summary>
        private void GenerateSummaryReport(List<CustomerOrder> orders)
        {
            var summary = CalculateOrderSummary(orders);
            var premiumAnalysis = AnalyzePremiumCustomers(orders);
            
            PrintSummaryReport(summary, premiumAnalysis);
        }
        
        /// <summary>
        /// ✅ CÁLCULOS DE RESUMEN: Extraído para ser testeable
        /// </summary>
        private (int totalOrders, decimal totalRevenue, decimal averageOrderValue, int totalItems) 
            CalculateOrderSummary(List<CustomerOrder> orders)
        {
            int totalOrders = orders.Count;
            decimal totalRevenue = orders.Sum(o => o.Total);
            int totalItems = orders.Sum(o => o.Items.Count);
            decimal averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;
            
            return (totalOrders, totalRevenue, averageOrderValue, totalItems);
        }
        
        /// <summary>
        /// ✅ ANÁLISIS PREMIUM: Lógica específica extraída
        /// </summary>
        private (int premiumCount, decimal premiumRevenue, double premiumPercentage) 
            AnalyzePremiumCustomers(List<CustomerOrder> orders)
        {
            var premiumOrders = orders.Where(o => o.Customer.IsPremium).ToList();
            int premiumCount = premiumOrders.Count;
            decimal premiumRevenue = premiumOrders.Sum(o => o.Total);
            double premiumPercentage = orders.Count > 0 ? premiumCount * 100.0 / orders.Count : 0;
            
            return (premiumCount, premiumRevenue, premiumPercentage);
        }
        
        /// <summary>
        /// ✅ IMPRESIÓN DE RESUMEN: Solo presentación
        /// </summary>
        private void PrintSummaryReport(
            (int totalOrders, decimal totalRevenue, decimal averageOrderValue, int totalItems) summary,
            (int premiumCount, decimal premiumRevenue, double premiumPercentage) premiumAnalysis)
        {
            Console.WriteLine("=== ORDER SUMMARY REPORT ===");
            Console.WriteLine($"Total Orders: {summary.totalOrders}");
            Console.WriteLine($"Total Revenue: ${summary.totalRevenue:F2}");
            Console.WriteLine($"Average Order Value: ${summary.averageOrderValue:F2}");
            Console.WriteLine($"Total Items Sold: {summary.totalItems}");
            Console.WriteLine($"Premium Customers: {premiumAnalysis.premiumCount}");
            Console.WriteLine($"Premium Revenue: ${premiumAnalysis.premiumRevenue:F2}");
            Console.WriteLine($"Premium Percentage: {premiumAnalysis.premiumPercentage:F1}%");
        }
        
        /// <summary>
        /// ✅ REPORTE DETALLADO: Responsabilidad específica
        /// </summary>
        private void GenerateDetailedReport(List<CustomerOrder> orders)
        {
            Console.WriteLine("=== DETAILED ORDER REPORT ===");
            
            foreach (var order in orders)
            {
                PrintOrderDetails(order);
            }
        }
        
        /// <summary>
        /// ✅ DETALLES DE ORDEN: Extraído para reutilización
        /// </summary>
        private void PrintOrderDetails(CustomerOrder order)
        {
            Console.WriteLine($"\nOrder ID: {order.Id}");
            Console.WriteLine($"Customer: {order.Customer.Email}");
            Console.WriteLine($"Premium: {(order.Customer.IsPremium ? "Yes" : "No")}");
            Console.WriteLine($"State: {order.Customer.State}");
            
            PrintOrderItems(order.Items);
            PrintOrderTotals(order);
            
            Console.WriteLine(new string('-', 50));
        }
        
        /// <summary>
        /// ✅ ITEMS DE ORDEN: Lógica de impresión extraída
        /// </summary>
        private void PrintOrderItems(List<OrderItem> items)
        {
            Console.WriteLine("Items:");
            foreach (var item in items)
            {
                decimal itemTotal = item.Quantity * item.UnitPrice;
                Console.WriteLine($"  - {item.ProductId}: {item.Quantity} x ${item.UnitPrice:F2} = ${itemTotal:F2}");
            }
        }
        
        /// <summary>
        /// ✅ TOTALES DE ORDEN: Presentación específica
        /// </summary>
        private void PrintOrderTotals(CustomerOrder order)
        {
            Console.WriteLine($"Subtotal: ${order.Subtotal:F2}");
            Console.WriteLine($"Taxes: ${order.Taxes:F2}");
            Console.WriteLine($"Shipping: ${order.Shipping:F2}");
            Console.WriteLine($"TOTAL: ${order.Total:F2}");
        }
        
        /// <summary>
        /// ✅ LOG DE FINALIZACIÓN: Responsabilidad específica
        /// </summary>
        private void LogReportCompletion()
        {
            Console.WriteLine($"Report generation completed at: {DateTime.Now}");
        }
    }
}

/*
RESULTADOS DEL REFACTORING:

MÉTRICAS MEJORADAS:
✅ Líneas por método: 5-15 (antes: 65)
✅ Complejidad ciclomática: 1-4 (antes: 12)
✅ Responsabilidades: 1 por método (antes: 6+)
✅ Legibilidad: ALTA (antes: BAJA)
✅ Testabilidad: ALTA (antes: IMPOSIBLE)

BENEFICIOS OBTENIDOS:
1. LEGIBILIDAD: Cada método tiene un nombre descriptivo
2. MANTENIBILIDAD: Cambios aislados en métodos específicos
3. TESTABILIDAD: Cada método se puede probar individualmente
4. REUTILIZACIÓN: Métodos pequeños se pueden reutilizar
5. DEBUGGING: Fácil encontrar problemas en métodos específicos

TÉCNICAS APLICADAS:
✅ Extract Method: 20+ métodos extraídos
✅ Single Responsibility: Cada método hace una cosa
✅ Descriptive Names: Nombres que explican la funcionalidad
✅ Early Return: Para reducir anidación
✅ Switch Expression: Para lógica de mapeo

PRINCIPIOS SEGUIDOS:
- Métodos cortos y enfocados
- Una sola razón para cambiar
- Nombres que explican intención
- Evitar código duplicado
- Separar cálculos de presentación

PRÓXIMOS PASOS:
- Agregar validación de parámetros
- Implementar manejo de errores
- Considerar inyección de dependencias
- Agregar logging estructurado
*/