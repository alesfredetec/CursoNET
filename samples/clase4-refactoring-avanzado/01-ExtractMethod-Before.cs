using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringJr.Ejercicios
{
    /// <summary>
    /// EJERCICIO 1: Extract Method - Métodos largos y complejos
    /// 
    /// PROBLEMAS IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Método ProcessCustomerOrder demasiado largo (60+ líneas)
    /// ❌ PROBLEMA 2: Múltiples responsabilidades en un solo método
    /// ❌ PROBLEMA 3: Lógica de validación mezclada con procesamiento
    /// ❌ PROBLEMA 4: Difícil de leer y mantener
    /// ❌ PROBLEMA 5: Imposible hacer testing de partes individuales
    /// 
    /// OBJETIVO: Extraer métodos más pequeños y específicos
    /// COMPLEJIDAD ACTUAL: Ciclomática = 12 (ALTO)
    /// COMPLEJIDAD OBJETIVO: Ciclomática < 5 por método
    /// </summary>
    
    public class OrderProcessor
    {
        /// <summary>
        /// ❌ MÉTODO PROBLEMÁTICO: Hace demasiadas cosas
        /// Complejidad ciclomática: 12
        /// Líneas de código: 65
        /// Responsabilidades: 6 diferentes
        /// </summary>
        public bool ProcessCustomerOrder(CustomerOrder order)
        {
            Console.WriteLine($"Starting to process order {order?.Id}");
            
            // ❌ PROBLEMA 1: Validación mezclada con lógica (15 líneas)
            if (order == null)
            {
                Console.WriteLine("ERROR: Order is null");
                return false;
            }
            
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
            
            if (order.Customer == null)
            {
                Console.WriteLine("ERROR: Customer information is required");
                return false;
            }
            
            if (string.IsNullOrEmpty(order.Customer.Email))
            {
                Console.WriteLine("ERROR: Customer email is required");
                return false;
            }
            
            // ❌ PROBLEMA 2: Cálculo de totales mezclado (10 líneas)
            decimal subtotal = 0;
            decimal taxes = 0;
            decimal shipping = 0;
            
            foreach (var item in order.Items)
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
                
                decimal itemTotal = item.Quantity * item.UnitPrice;
                subtotal += itemTotal;
                
                // Calcular descuentos por volumen
                if (item.Quantity >= 10)
                {
                    subtotal -= itemTotal * 0.1m; // 10% descuento
                }
                else if (item.Quantity >= 5)
                {
                    subtotal -= itemTotal * 0.05m; // 5% descuento
                }
            }
            
            // ❌ PROBLEMA 3: Cálculo de impuestos complejo (8 líneas)
            if (order.Customer.State == "CA" || order.Customer.State == "NY")
            {
                taxes = subtotal * 0.08m; // 8% tax
            }
            else if (order.Customer.State == "TX" || order.Customer.State == "FL")
            {
                taxes = subtotal * 0.06m; // 6% tax
            }
            else
            {
                taxes = subtotal * 0.04m; // 4% tax default
            }
            
            // ❌ PROBLEMA 4: Cálculo de envío mezclado (6 líneas)
            if (subtotal >= 100)
            {
                shipping = 0; // Free shipping
            }
            else if (order.Customer.IsPremium)
            {
                shipping = 5.99m; // Premium discount
            }
            else
            {
                shipping = 12.99m; // Standard shipping
            }
            
            order.Subtotal = subtotal;
            order.Taxes = taxes;
            order.Shipping = shipping;
            order.Total = subtotal + taxes + shipping;
            
            // ❌ PROBLEMA 5: Procesamiento de inventario mezclado (8 líneas)
            foreach (var item in order.Items)
            {
                Console.WriteLine($"Checking inventory for product {item.ProductId}");
                
                // Simular verificación de inventario
                if (item.ProductId.StartsWith("DISC")) // Discontinued items
                {
                    Console.WriteLine($"ERROR: Product {item.ProductId} is discontinued");
                    return false;
                }
                
                Console.WriteLine($"Reserved {item.Quantity} units of {item.ProductId}");
            }
            
            // ❌ PROBLEMA 6: Notificaciones mezcladas (6 líneas)
            Console.WriteLine($"Sending confirmation email to {order.Customer.Email}");
            Console.WriteLine($"Email subject: Order Confirmation - {order.Id}");
            Console.WriteLine($"Email body: Thank you for your order of ${order.Total:F2}");
            
            if (order.Customer.IsPremium)
            {
                Console.WriteLine("Sending premium customer thank you message");
            }
            
            // ❌ PROBLEMA 7: Logging y auditoría mezclados (4 líneas)
            Console.WriteLine($"Order {order.Id} processed successfully");
            Console.WriteLine($"Total amount: ${order.Total:F2}");
            Console.WriteLine($"Customer: {order.Customer.Email}");
            Console.WriteLine($"Processing completed at: {DateTime.Now}");
            
            return true;
        }
        
        /// <summary>
        /// ❌ OTRO MÉTODO PROBLEMÁTICO: Generar reporte muy largo
        /// </summary>
        public void GenerateOrderReport(List<CustomerOrder> orders, string reportType)
        {
            Console.WriteLine($"Generating {reportType} report for {orders.Count} orders");
            
            // ❌ PROBLEMA: Método largo con múltiples responsabilidades (40+ líneas)
            if (reportType == "SUMMARY")
            {
                decimal totalRevenue = 0;
                int totalOrders = orders.Count;
                int totalItems = 0;
                
                foreach (var order in orders)
                {
                    totalRevenue += order.Total;
                    totalItems += order.Items.Count;
                }
                
                decimal averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;
                
                Console.WriteLine("=== ORDER SUMMARY REPORT ===");
                Console.WriteLine($"Total Orders: {totalOrders}");
                Console.WriteLine($"Total Revenue: ${totalRevenue:F2}");
                Console.WriteLine($"Average Order Value: ${averageOrderValue:F2}");
                Console.WriteLine($"Total Items Sold: {totalItems}");
                
                // Premium customer analysis
                var premiumOrders = orders.Where(o => o.Customer.IsPremium).ToList();
                decimal premiumRevenue = premiumOrders.Sum(o => o.Total);
                
                Console.WriteLine($"Premium Customers: {premiumOrders.Count}");
                Console.WriteLine($"Premium Revenue: ${premiumRevenue:F2}");
                Console.WriteLine($"Premium Percentage: {(premiumOrders.Count * 100.0 / totalOrders):F1}%");
            }
            else if (reportType == "DETAILED")
            {
                Console.WriteLine("=== DETAILED ORDER REPORT ===");
                
                foreach (var order in orders)
                {
                    Console.WriteLine($"\nOrder ID: {order.Id}");
                    Console.WriteLine($"Customer: {order.Customer.Email}");
                    Console.WriteLine($"Premium: {(order.Customer.IsPremium ? "Yes" : "No")}");
                    Console.WriteLine($"State: {order.Customer.State}");
                    Console.WriteLine($"Items:");
                    
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"  - {item.ProductId}: {item.Quantity} x ${item.UnitPrice:F2} = ${item.Quantity * item.UnitPrice:F2}");
                    }
                    
                    Console.WriteLine($"Subtotal: ${order.Subtotal:F2}");
                    Console.WriteLine($"Taxes: ${order.Taxes:F2}");
                    Console.WriteLine($"Shipping: ${order.Shipping:F2}");
                    Console.WriteLine($"TOTAL: ${order.Total:F2}");
                    Console.WriteLine(new string('-', 50));
                }
            }
            
            Console.WriteLine($"Report generation completed at: {DateTime.Now}");
        }
    }
    
    // Supporting classes
    public class CustomerOrder
    {
        public string Id { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal Subtotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
    }
    
    public class Customer
    {
        public string Email { get; set; }
        public string State { get; set; }
        public bool IsPremium { get; set; }
    }
    
    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

/*
ANÁLISIS DE PROBLEMAS:

1. MÉTODO ProcessCustomerOrder:
   - 65 líneas de código (OBJETIVO: <20)
   - Complejidad ciclomática: 12 (OBJETIVO: <5)
   - 7 responsabilidades diferentes
   - Imposible hacer unit testing granular

2. MÉTODO GenerateOrderReport:
   - 45 líneas de código
   - Lógica de presentación mezclada con cálculos
   - Código duplicado en los dos tipos de reporte

TÉCNICAS A APLICAR:
✅ Extract Method para cada responsabilidad
✅ Separar validación, cálculos, procesamiento
✅ Crear métodos con nombres descriptivos
✅ Reducir complejidad ciclomática
✅ Mejorar legibilidad y mantenibilidad

MÉTRICAS OBJETIVO:
- Líneas por método: <20
- Complejidad ciclomática: <5
- Una sola responsabilidad por método
- Nombres descriptivos y claros

TIEMPO ESTIMADO: 30 minutos
DIFICULTAD: Principiante
*/