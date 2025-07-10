using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RefactoringAvanzado.Ejercicios
{
    /// <summary>
    /// PROBLEMA: Violaciones severas de principios SOLID
    /// 
    /// VIOLACIONES IDENTIFICADAS:
    /// ❌ SRP: Single Responsibility - Una clase hace muchas cosas
    /// ❌ OCP: Open/Closed - Cambios requieren modificar código existente
    /// ❌ LSP: Liskov Substitution - Herencia incorrecta
    /// ❌ ISP: Interface Segregation - Interfaces demasiado grandes
    /// ❌ DIP: Dependency Inversion - Dependencias concretas hard-coded
    /// 
    /// COMPLEJIDAD: 15+ (Muy Alto)
    /// MANTENIBILIDAD: Muy Baja
    /// TESTABILIDAD: Imposible
    /// </summary>

    // ❌ VIOLACIÓN SRP: Esta clase hace DEMASIADAS cosas diferentes
    public class CustomerOrderProcessor
    {
        private readonly HttpClient _httpClient = new HttpClient();

        // ❌ VIOLACIÓN 1: Validación + Processing + Logging + Email + Database + API calls
        public async Task<bool> ProcessCustomerOrder(int customerId, List<OrderItem> items, 
            string paymentMethod, string shippingAddress, bool sendEmail = true)
        {
            // ❌ VIOLACIÓN SRP: Esta clase maneja validación
            if (customerId <= 0)
                throw new ArgumentException("Invalid customer ID");
            
            if (items == null || items.Count == 0)
                throw new ArgumentException("No items in order");

            if (string.IsNullOrEmpty(paymentMethod))
                throw new ArgumentException("Payment method required");

            // ❌ VIOLACIÓN DIP: Hard-coded dependencies
            var customer = GetCustomerFromDatabase(customerId);
            if (customer == null)
                return false;

            // ❌ VIOLACIÓN SRP: Esta clase maneja cálculos de pricing
            decimal totalAmount = 0;
            foreach (var item in items)
            {
                // ❌ VIOLACIÓN OCP: Lógica de pricing hard-coded
                if (item.Category == "ELECTRONICS")
                {
                    totalAmount += item.Price * item.Quantity * 0.9m; // 10% discount
                }
                else if (item.Category == "BOOKS")
                {
                    totalAmount += item.Price * item.Quantity * 0.85m; // 15% discount
                }
                else if (item.Category == "CLOTHING")
                {
                    totalAmount += item.Price * item.Quantity * 0.95m; // 5% discount
                }
                else
                {
                    totalAmount += item.Price * item.Quantity; // No discount
                }
            }

            // ❌ VIOLACIÓN SRP: Esta clase maneja payment processing
            bool paymentSuccessful = false;
            if (paymentMethod == "CREDIT_CARD")
            {
                paymentSuccessful = ProcessCreditCardPayment(customer.CreditCardNumber, totalAmount);
            }
            else if (paymentMethod == "PAYPAL")
            {
                paymentSuccessful = ProcessPayPalPayment(customer.PayPalEmail, totalAmount);
            }
            else if (paymentMethod == "BANK_TRANSFER")
            {
                paymentSuccessful = ProcessBankTransfer(customer.BankAccount, totalAmount);
            }

            if (!paymentSuccessful)
                return false;

            // ❌ VIOLACIÓN SRP: Esta clase maneja inventory management
            foreach (var item in items)
            {
                var inventory = GetInventoryFromDatabase(item.ProductId);
                if (inventory.Quantity < item.Quantity)
                {
                    // ❌ VIOLACIÓN SRP: Esta clase maneja rollback logic
                    RollbackPayment(paymentMethod, totalAmount);
                    return false;
                }
                inventory.Quantity -= item.Quantity;
                UpdateInventoryInDatabase(inventory);
            }

            // ❌ VIOLACIÓN SRP: Esta clase maneja order persistence
            var order = new Order
            {
                CustomerId = customerId,
                Items = items,
                TotalAmount = totalAmount,
                PaymentMethod = paymentMethod,
                ShippingAddress = shippingAddress,
                OrderDate = DateTime.Now,
                Status = "CONFIRMED"
            };
            
            SaveOrderToDatabase(order);

            // ❌ VIOLACIÓN SRP: Esta clase maneja shipping
            var shippingCost = CalculateShippingCost(shippingAddress, items);
            var trackingNumber = CreateShippingLabel(order, shippingCost);
            order.TrackingNumber = trackingNumber;
            UpdateOrderInDatabase(order);

            // ❌ VIOLACIÓN SRP: Esta clase maneja notifications
            if (sendEmail)
            {
                var emailTemplate = GenerateOrderConfirmationEmail(customer, order);
                SendEmail(customer.Email, "Order Confirmation", emailTemplate);
            }

            // ❌ VIOLACIÓN SRP: Esta clase maneja analytics
            await SendAnalyticsEvent(customerId, totalAmount, items.Count);

            // ❌ VIOLACIÓN SRP: Esta clase maneja logging
            LogOrderProcessing(order.OrderId, customerId, totalAmount);

            // ❌ VIOLACIÓN SRP: Esta clase maneja external API calls
            await NotifyWarehouise(order);
            await UpdateCustomerLoyaltyPoints(customerId, totalAmount);

            return true;
        }

        // ❌ VIOLACIÓN DIP: Database calls hard-coded
        private Customer GetCustomerFromDatabase(int customerId)
        {
            // Hard-coded database access
            return new Customer 
            { 
                Id = customerId, 
                Email = "customer@email.com",
                CreditCardNumber = "1234-5678-9012-3456",
                PayPalEmail = "customer@paypal.com",
                BankAccount = "123456789"
            };
        }

        // ❌ VIOLACIÓN OCP: Payment methods hard-coded
        private bool ProcessCreditCardPayment(string cardNumber, decimal amount)
        {
            // ❌ Hard-coded credit card processing
            Console.WriteLine($"Processing credit card payment: {amount}");
            return true; // Simulate success
        }

        private bool ProcessPayPalPayment(string email, decimal amount)
        {
            // ❌ Hard-coded PayPal processing
            Console.WriteLine($"Processing PayPal payment: {amount}");
            return true; // Simulate success
        }

        private bool ProcessBankTransfer(string bankAccount, decimal amount)
        {
            // ❌ Hard-coded bank transfer processing
            Console.WriteLine($"Processing bank transfer: {amount}");
            return true; // Simulate success
        }

        // ❌ VIOLACIÓN SRP: Rollback logic mixed with main logic
        private void RollbackPayment(string paymentMethod, decimal amount)
        {
            Console.WriteLine($"Rolling back {paymentMethod} payment of {amount}");
        }

        // ❌ VIOLACIÓN DIP: Database operations hard-coded
        private Inventory GetInventoryFromDatabase(int productId)
        {
            return new Inventory { ProductId = productId, Quantity = 100 };
        }

        private void UpdateInventoryInDatabase(Inventory inventory)
        {
            Console.WriteLine($"Updated inventory for product {inventory.ProductId}");
        }

        private void SaveOrderToDatabase(Order order)
        {
            order.OrderId = new Random().Next(1000, 9999);
            Console.WriteLine($"Saved order {order.OrderId} to database");
        }

        private void UpdateOrderInDatabase(Order order)
        {
            Console.WriteLine($"Updated order {order.OrderId} in database");
        }

        // ❌ VIOLACIÓN SRP: Shipping logic in order processor
        private decimal CalculateShippingCost(string address, List<OrderItem> items)
        {
            // ❌ Hard-coded shipping calculation
            return items.Count * 5.99m;
        }

        private string CreateShippingLabel(Order order, decimal shippingCost)
        {
            return $"TRACK_{order.OrderId}_{DateTime.Now:yyyyMMdd}";
        }

        // ❌ VIOLACIÓN SRP: Email logic in order processor
        private string GenerateOrderConfirmationEmail(Customer customer, Order order)
        {
            return $"Dear Customer,\nYour order {order.OrderId} has been confirmed.\nTotal: ${order.TotalAmount}";
        }

        private void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Email sent to {to}: {subject}");
        }

        // ❌ VIOLACIÓN SRP: Analytics logic in order processor
        private async Task SendAnalyticsEvent(int customerId, decimal amount, int itemCount)
        {
            var analyticsData = new 
            { 
                CustomerId = customerId, 
                Amount = amount, 
                ItemCount = itemCount,
                Timestamp = DateTime.Now
            };
            
            // ❌ Hard-coded analytics API
            var json = JsonSerializer.Serialize(analyticsData);
            await _httpClient.PostAsync("https://analytics.api.com/events", 
                new StringContent(json));
        }

        // ❌ VIOLACIÓN SRP: Logging logic in order processor
        private void LogOrderProcessing(int orderId, int customerId, decimal amount)
        {
            var logEntry = $"{DateTime.Now}: Order {orderId} processed for customer {customerId}, amount: ${amount}";
            File.AppendAllText("orders.log", logEntry + Environment.NewLine);
        }

        // ❌ VIOLACIÓN SRP: External API calls in order processor
        private async Task NotifyWarehouise(Order order)
        {
            var notificationData = new { OrderId = order.OrderId, Items = order.Items };
            var json = JsonSerializer.Serialize(notificationData);
            await _httpClient.PostAsync("https://warehouse.api.com/orders", 
                new StringContent(json));
        }

        private async Task UpdateCustomerLoyaltyPoints(int customerId, decimal amount)
        {
            var points = (int)(amount / 10); // 1 point per $10
            var updateData = new { CustomerId = customerId, Points = points };
            var json = JsonSerializer.Serialize(updateData);
            await _httpClient.PostAsync("https://loyalty.api.com/points", 
                new StringContent(json));
        }
    }

    // ❌ VIOLACIÓN ISP: Interface demasiado grande
    public interface IOrderService
    {
        Task<bool> ProcessOrder(int customerId, List<OrderItem> items, string paymentMethod, string shippingAddress);
        Task<decimal> CalculateOrderTotal(List<OrderItem> items);
        Task<bool> ValidatePayment(string paymentMethod, decimal amount);
        Task<bool> CheckInventory(List<OrderItem> items);
        Task<string> CreateShippingLabel(Order order);
        Task<bool> SendOrderConfirmation(Customer customer, Order order);
        Task LogOrder(Order order);
        Task SendAnalytics(Order order);
        Task NotifyWarehouse(Order order);
        Task UpdateLoyaltyPoints(int customerId, decimal amount);
        // ❌ Too many responsibilities in one interface
    }

    // ❌ VIOLACIÓN LSP: Herencia incorrecta
    public class DiscountedOrderProcessor : CustomerOrderProcessor
    {
        // ❌ LSP Violation: This subclass changes the expected behavior
        public new async Task<bool> ProcessCustomerOrder(int customerId, List<OrderItem> items, 
            string paymentMethod, string shippingAddress, bool sendEmail = true)
        {
            // ❌ Throws exception instead of returning false like parent
            if (items.Sum(i => i.Price * i.Quantity) < 100)
                throw new InvalidOperationException("Minimum order amount is $100");

            return await base.ProcessCustomerOrder(customerId, items, paymentMethod, shippingAddress, sendEmail);
        }
    }

    // ❌ Supporting classes with poor design
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CreditCardNumber { get; set; } // ❌ Sensitive data in domain model
        public string PayPalEmail { get; set; }
        public string BankAccount { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

    public class Inventory
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // ❌ VIOLACIÓN SRP: Demo class que muestra los problemas
    public class SOLIDViolationsDemo
    {
        public static async Task RunDemo()
        {
            Console.WriteLine("=== DEMO: SOLID Violations (PROBLEMATIC CODE) ===\n");

            var processor = new CustomerOrderProcessor();
            
            var items = new List<OrderItem>
            {
                new() { ProductId = 1, ProductName = "Laptop", Category = "ELECTRONICS", Price = 999.99m, Quantity = 1 },
                new() { ProductId = 2, ProductName = "Book", Category = "BOOKS", Price = 29.99m, Quantity = 2 }
            };

            try
            {
                // ❌ This single method call does TOO MANY things
                var success = await processor.ProcessCustomerOrder(
                    customerId: 12345,
                    items: items,
                    paymentMethod: "CREDIT_CARD",
                    shippingAddress: "123 Main St, City, State",
                    sendEmail: true
                );

                Console.WriteLine($"Order processing result: {success}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

/*
SOLID VIOLATIONS SUMMARY:

❌ SINGLE RESPONSIBILITY PRINCIPLE (SRP):
- CustomerOrderProcessor does 10+ different things
- Validation, pricing, payment, inventory, shipping, email, logging, analytics, etc.
- Should be split into separate classes

❌ OPEN/CLOSED PRINCIPLE (OCP):
- Payment methods hard-coded with if/else
- Pricing logic hard-coded with if/else
- Adding new payment method requires modifying existing code

❌ LISKOV SUBSTITUTION PRINCIPLE (LSP):
- DiscountedOrderProcessor changes expected behavior
- Throws exception instead of returning false
- Breaks polymorphism expectations

❌ INTERFACE SEGREGATION PRINCIPLE (ISP):
- IOrderService interface has too many methods
- Forces implementations to implement methods they don't need
- Should be split into focused interfaces

❌ DEPENDENCY INVERSION PRINCIPLE (DIP):
- Depends on concrete HttpClient, File system, Console
- Hard-coded database calls
- No dependency injection

PROBLEMS CAUSED:
- Impossible to unit test
- Changes require modifying multiple parts
- High coupling between unrelated concerns
- Code duplication
- Difficult to maintain and extend

EXERCISE GOAL:
Refactor this code to follow all SOLID principles properly.
*/