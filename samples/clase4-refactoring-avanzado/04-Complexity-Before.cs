using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringJr.Ejercicios
{
    /// <summary>
    /// EJERCICIO 4: Reducir Complejidad Ciclomática - ANTES del refactoring
    /// 
    /// PROBLEMAS IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Complejidad ciclomática extrema (>15 en varios métodos)
    /// ❌ PROBLEMA 2: Anidación profunda de condicionales (>5 niveles)
    /// ❌ PROBLEMA 3: Múltiples puntos de salida (early returns mezclados)
    /// ❌ PROBLEMA 4: Lógica de negocio compleja mezclada con control de flujo
    /// ❌ PROBLEMA 5: Difícil testing debido a múltiples caminos de ejecución
    /// 
    /// OBJETIVO: Reducir complejidad ciclomática a <5 por método
    /// TÉCNICAS: Guard clauses, Extract Method, Table-driven logic, Polymorphism
    /// TIEMPO ESTIMADO: 45 minutos
    /// </summary>
    
    public class OrderValidationService
    {
        /// <summary>
        /// ❌ MÉTODO ULTRA COMPLEJO: Complejidad ciclomática = 18
        /// Caminos de ejecución: 256+ combinaciones posibles
        /// Anidación: 6 niveles profundos
        /// Testing: Requiere 20+ test cases para cobertura completa
        /// </summary>
        public bool ValidateOrderComplex(Order order, Customer customer, List<Product> products, ShippingInfo shipping)
        {
            // ❌ PROBLEMA 1: Validación anidada compleja (Complejidad +8)
            if (order != null)
            {
                if (customer != null)
                {
                    if (customer.IsActive)
                    {
                        if (customer.CreditLimit > 0)
                        {
                            if (order.Total <= customer.CreditLimit)
                            {
                                if (products != null && products.Count > 0)
                                {
                                    foreach (var product in products)
                                    {
                                        if (product.IsAvailable)
                                        {
                                            if (product.Quantity > 0)
                                            {
                                                if (product.Price > 0)
                                                {
                                                    // Validación exitosa en el nivel más profundo
                                                    Console.WriteLine($"Product {product.Name} is valid");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Invalid price for {product.Name}");
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine($"No quantity for {product.Name}");
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Product {product.Name} not available");
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No products in order");
                                    return false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Order exceeds credit limit");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Customer has no credit limit");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Customer is not active");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Customer is null");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Order is null");
                return false;
            }
            
            // ❌ PROBLEMA 2: Más validaciones anidadas (Complejidad +6)
            if (shipping != null)
            {
                if (!string.IsNullOrEmpty(shipping.Address))
                {
                    if (!string.IsNullOrEmpty(shipping.City))
                    {
                        if (!string.IsNullOrEmpty(shipping.ZipCode))
                        {
                            if (shipping.Country != null)
                            {
                                if (shipping.ShippingMethod != null)
                                {
                                    if (shipping.ShippingMethod == "EXPRESS" && shipping.Country != "US")
                                    {
                                        Console.WriteLine("Express shipping not available for international orders");
                                        return false;
                                    }
                                    else if (shipping.ShippingMethod == "OVERNIGHT" && (shipping.Country != "US" || order.Total < 100))
                                    {
                                        Console.WriteLine("Overnight shipping requires US delivery and $100+ order");
                                        return false;
                                    }
                                    else if (shipping.ShippingMethod == "STANDARD" && order.Total < 25)
                                    {
                                        Console.WriteLine("Standard shipping requires $25+ order");
                                        return false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Shipping method is required");
                                    return false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Country is required");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Zip code is required");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("City is required");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Address is required");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Shipping info is required");
                return false;
            }
            
            // ❌ PROBLEMA 3: Lógica de descuentos ultra compleja (Complejidad +4)
            if (customer.CustomerType == "PREMIUM")
            {
                if (order.Total > 1000 && customer.YearsAsMember > 5)
                {
                    order.DiscountPercentage = 15;
                }
                else if (order.Total > 500 && customer.YearsAsMember > 2)
                {
                    order.DiscountPercentage = 10;
                }
                else if (order.Total > 100)
                {
                    order.DiscountPercentage = 5;
                }
            }
            else if (customer.CustomerType == "GOLD")
            {
                if (order.Total > 750 && customer.YearsAsMember > 3)
                {
                    order.DiscountPercentage = 12;
                }
                else if (order.Total > 300)
                {
                    order.DiscountPercentage = 7;
                }
            }
            else if (customer.CustomerType == "SILVER")
            {
                if (order.Total > 500 && customer.YearsAsMember > 1)
                {
                    order.DiscountPercentage = 8;
                }
                else if (order.Total > 200)
                {
                    order.DiscountPercentage = 3;
                }
            }
            
            Console.WriteLine("Order validation completed successfully");
            return true;
        }
        
        /// <summary>
        /// ❌ OTRO MÉTODO COMPLEJO: Complejidad ciclomática = 14
        /// Procesa diferentes tipos de payment con lógica anidada
        /// </summary>
        public PaymentResult ProcessPayment(Order order, PaymentInfo payment, Customer customer)
        {
            // ❌ PROBLEMA 1: Switch anidado con validaciones (Complejidad +10)
            switch (payment.PaymentMethod)
            {
                case "CREDIT_CARD":
                    if (payment.CreditCard != null)
                    {
                        if (!string.IsNullOrEmpty(payment.CreditCard.Number))
                        {
                            if (payment.CreditCard.ExpiryDate > DateTime.Now)
                            {
                                if (!string.IsNullOrEmpty(payment.CreditCard.CVV))
                                {
                                    if (payment.CreditCard.Number.Length >= 15 && payment.CreditCard.Number.Length <= 16)
                                    {
                                        // Procesamiento específico de tarjeta de crédito
                                        if (order.Total > customer.CreditLimit)
                                        {
                                            return new PaymentResult { Success = false, Message = "Exceeds credit limit" };
                                        }
                                        else if (payment.CreditCard.Type == "AMEX" && order.Total > 5000)
                                        {
                                            return new PaymentResult { Success = false, Message = "AMEX has $5000 limit" };
                                        }
                                        else
                                        {
                                            return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
                                        }
                                    }
                                    else
                                    {
                                        return new PaymentResult { Success = false, Message = "Invalid card number length" };
                                    }
                                }
                                else
                                {
                                    return new PaymentResult { Success = false, Message = "CVV is required" };
                                }
                            }
                            else
                            {
                                return new PaymentResult { Success = false, Message = "Card expired" };
                            }
                        }
                        else
                        {
                            return new PaymentResult { Success = false, Message = "Card number is required" };
                        }
                    }
                    else
                    {
                        return new PaymentResult { Success = false, Message = "Credit card info is required" };
                    }
                    
                case "PAYPAL":
                    if (payment.PayPal != null)
                    {
                        if (!string.IsNullOrEmpty(payment.PayPal.Email))
                        {
                            if (payment.PayPal.Email.Contains("@"))
                            {
                                if (payment.PayPal.IsVerified)
                                {
                                    if (order.Total <= 2500) // PayPal limit
                                    {
                                        return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
                                    }
                                    else
                                    {
                                        return new PaymentResult { Success = false, Message = "PayPal has $2500 transaction limit" };
                                    }
                                }
                                else
                                {
                                    return new PaymentResult { Success = false, Message = "PayPal account not verified" };
                                }
                            }
                            else
                            {
                                return new PaymentResult { Success = false, Message = "Invalid PayPal email" };
                            }
                        }
                        else
                        {
                            return new PaymentResult { Success = false, Message = "PayPal email is required" };
                        }
                    }
                    else
                    {
                        return new PaymentResult { Success = false, Message = "PayPal info is required" };
                    }
                    
                case "BANK_TRANSFER":
                    if (payment.BankTransfer != null)
                    {
                        if (!string.IsNullOrEmpty(payment.BankTransfer.AccountNumber))
                        {
                            if (!string.IsNullOrEmpty(payment.BankTransfer.RoutingNumber))
                            {
                                if (payment.BankTransfer.AccountNumber.Length >= 8)
                                {
                                    if (order.Total >= 50) // Minimum for bank transfer
                                    {
                                        return new PaymentResult { Success = true, TransactionId = Guid.NewGuid().ToString() };
                                    }
                                    else
                                    {
                                        return new PaymentResult { Success = false, Message = "Bank transfer requires $50 minimum" };
                                    }
                                }
                                else
                                {
                                    return new PaymentResult { Success = false, Message = "Invalid account number" };
                                }
                            }
                            else
                            {
                                return new PaymentResult { Success = false, Message = "Routing number is required" };
                            }
                        }
                        else
                        {
                            return new PaymentResult { Success = false, Message = "Account number is required" };
                        }
                    }
                    else
                    {
                        return new PaymentResult { Success = false, Message = "Bank transfer info is required" };
                    }
                    
                default:
                    return new PaymentResult { Success = false, Message = "Unsupported payment method" };
            }
        }
        
        /// <summary>
        /// ❌ MÉTODO DE PRICING COMPLEJO: Complejidad ciclomática = 16
        /// Cálculo de precios con múltiples factores y condiciones anidadas
        /// </summary>
        public decimal CalculateFinalPrice(Order order, Customer customer, List<Product> products, DateTime orderDate)
        {
            decimal basePrice = products.Sum(p => p.Price * p.Quantity);
            decimal finalPrice = basePrice;
            
            // ❌ PROBLEMA 1: Descuentos por volumen anidados (Complejidad +6)
            if (products.Count > 0)
            {
                if (products.Count >= 10)
                {
                    if (basePrice > 1000)
                    {
                        finalPrice = basePrice * 0.85m; // 15% descuento
                    }
                    else if (basePrice > 500)
                    {
                        finalPrice = basePrice * 0.90m; // 10% descuento
                    }
                    else
                    {
                        finalPrice = basePrice * 0.95m; // 5% descuento
                    }
                }
                else if (products.Count >= 5)
                {
                    if (basePrice > 500)
                    {
                        finalPrice = basePrice * 0.92m; // 8% descuento
                    }
                    else
                    {
                        finalPrice = basePrice * 0.97m; // 3% descuento
                    }
                }
            }
            
            // ❌ PROBLEMA 2: Descuentos estacionales complejos (Complejidad +5)
            var month = orderDate.Month;
            if (month == 12) // Diciembre
            {
                if (orderDate.Day >= 15) // Segunda quincena
                {
                    if (customer.CustomerType == "PREMIUM")
                    {
                        finalPrice = finalPrice * 0.80m; // 20% descuento navideño premium
                    }
                    else if (customer.CustomerType == "GOLD")
                    {
                        finalPrice = finalPrice * 0.85m; // 15% descuento navideño gold
                    }
                    else
                    {
                        finalPrice = finalPrice * 0.90m; // 10% descuento navideño estándar
                    }
                }
                else
                {
                    finalPrice = finalPrice * 0.95m; // 5% descuento primera quincena diciembre
                }
            }
            else if (month >= 6 && month <= 8) // Verano
            {
                if (customer.YearsAsMember > 2)
                {
                    finalPrice = finalPrice * 0.93m; // 7% descuento verano miembros antiguos
                }
                else
                {
                    finalPrice = finalPrice * 0.97m; // 3% descuento verano miembros nuevos
                }
            }
            else if (month == 1) // Enero - liquidación
            {
                finalPrice = finalPrice * 0.88m; // 12% descuento liquidación enero
            }
            
            // ❌ PROBLEMA 3: Impuestos por región complicados (Complejidad +5)
            if (customer.State != null)
            {
                if (customer.State == "CA" || customer.State == "NY")
                {
                    if (finalPrice > 1000)
                    {
                        finalPrice = finalPrice * 1.10m; // 10% impuesto alto valor
                    }
                    else
                    {
                        finalPrice = finalPrice * 1.08m; // 8% impuesto estándar
                    }
                }
                else if (customer.State == "TX" || customer.State == "FL")
                {
                    if (finalPrice > 500)
                    {
                        finalPrice = finalPrice * 1.07m; // 7% impuesto medio
                    }
                    else
                    {
                        finalPrice = finalPrice * 1.06m; // 6% impuesto bajo
                    }
                }
                else
                {
                    finalPrice = finalPrice * 1.04m; // 4% impuesto otros estados
                }
            }
            
            return Math.Round(finalPrice, 2);
        }
        
        /// <summary>
        /// ❌ MÉTODO DE INVENTARIO COMPLEJO: Complejidad ciclomática = 12
        /// Gestión de inventario con múltiples validaciones
        /// </summary>
        public InventoryResult UpdateInventory(List<Product> products, string operation, int warehouseId)
        {
            var result = new InventoryResult { Success = true, UpdatedProducts = new List<Product>() };
            
            // ❌ PROBLEMA: Operaciones con validaciones anidadas (Complejidad +12)
            foreach (var product in products)
            {
                if (operation == "RESERVE")
                {
                    if (product.Quantity > 0)
                    {
                        if (product.IsAvailable)
                        {
                            if (product.WarehouseId == warehouseId)
                            {
                                if (product.ReservedQuantity + product.RequestedQuantity <= product.Quantity)
                                {
                                    product.ReservedQuantity += product.RequestedQuantity;
                                    result.UpdatedProducts.Add(product);
                                }
                                else
                                {
                                    result.Success = false;
                                    result.ErrorMessage = $"Not enough quantity for {product.Name}";
                                    return result;
                                }
                            }
                            else
                            {
                                result.Success = false;
                                result.ErrorMessage = $"Product {product.Name} not in warehouse {warehouseId}";
                                return result;
                            }
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = $"Product {product.Name} is not available";
                            return result;
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = $"Product {product.Name} is out of stock";
                        return result;
                    }
                }
                else if (operation == "RELEASE")
                {
                    if (product.ReservedQuantity > 0)
                    {
                        if (product.ReservedQuantity >= product.RequestedQuantity)
                        {
                            product.ReservedQuantity -= product.RequestedQuantity;
                            result.UpdatedProducts.Add(product);
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = $"Cannot release more than reserved for {product.Name}";
                            return result;
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = $"No reserved quantity for {product.Name}";
                        return result;
                    }
                }
                else if (operation == "FULFILL")
                {
                    if (product.ReservedQuantity >= product.RequestedQuantity)
                    {
                        product.ReservedQuantity -= product.RequestedQuantity;
                        product.Quantity -= product.RequestedQuantity;
                        result.UpdatedProducts.Add(product);
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = $"Cannot fulfill more than reserved for {product.Name}";
                        return result;
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = $"Unknown operation: {operation}";
                    return result;
                }
            }
            
            return result;
        }
    }
    
    // Supporting classes
    public class Order
    {
        public string Id { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime OrderDate { get; set; }
    }
    
    public class Customer
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal CreditLimit { get; set; }
        public string CustomerType { get; set; } // PREMIUM, GOLD, SILVER
        public int YearsAsMember { get; set; }
        public string State { get; set; }
    }
    
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int RequestedQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public int WarehouseId { get; set; }
    }
    
    public class ShippingInfo
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string ShippingMethod { get; set; } // EXPRESS, OVERNIGHT, STANDARD
    }
    
    public class PaymentInfo
    {
        public string PaymentMethod { get; set; } // CREDIT_CARD, PAYPAL, BANK_TRANSFER
        public CreditCardInfo CreditCard { get; set; }
        public PayPalInfo PayPal { get; set; }
        public BankTransferInfo BankTransfer { get; set; }
    }
    
    public class CreditCardInfo
    {
        public string Number { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string Type { get; set; } // VISA, MASTERCARD, AMEX
    }
    
    public class PayPalInfo
    {
        public string Email { get; set; }
        public bool IsVerified { get; set; }
    }
    
    public class BankTransferInfo
    {
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
    }
    
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
    }
    
    public class InventoryResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public List<Product> UpdatedProducts { get; set; }
    }
}

/*
ANÁLISIS DE COMPLEJIDAD EXTREMA:

1. ValidateOrderComplex: 
   - Complejidad Ciclomática: 18 (CRÍTICO - Objetivo: <5)
   - Anidación: 6 niveles (CRÍTICO - Objetivo: <3)
   - Paths de ejecución: 256+ combinaciones
   - Testing: Requiere 20+ test cases

2. ProcessPayment:
   - Complejidad Ciclomática: 14 (CRÍTICO)
   - Switch statement anidado con validaciones
   - Código duplicado en cada case

3. CalculateFinalPrice:
   - Complejidad Ciclomática: 16 (CRÍTICO)
   - Múltiples factores de pricing combinados
   - Lógica de negocio entrelazada

4. UpdateInventory:
   - Complejidad Ciclomática: 12 (ALTO)
   - Operaciones similares con validaciones repetidas
   - Early returns múltiples

PROBLEMAS PRINCIPALES:
❌ Impossible to test thoroughly (exponential test cases)
❌ High risk of bugs in edge cases
❌ Difficult to maintain and modify
❌ Performance issues due to nested loops
❌ Code duplication in validations
❌ Business logic mixed with control flow

TÉCNICAS DE REFACTORING NECESARIAS:
✅ Guard Clauses para early returns
✅ Extract Method para validaciones
✅ Table-driven logic para pricing
✅ Strategy Pattern para payment methods
✅ Chain of Responsibility para validations
✅ Replace Nested Conditional with Guard Clauses

MÉTRICAS OBJETIVO:
- Complejidad Ciclomática: <5 por método
- Anidación: <3 niveles
- Líneas por método: <20
- Test cases: <10 por método

TIEMPO ESTIMADO: 45 minutos
DIFICULTAD: Avanzado
*/