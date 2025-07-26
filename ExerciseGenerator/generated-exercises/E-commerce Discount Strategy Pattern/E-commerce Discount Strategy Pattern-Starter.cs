using System;

namespace DesignPatterns.StrategyPattern
{
    /// <summary>
    /// BEFORE: E-commerce system with complex conditional discount logic
    /// 
    /// PROBLEMS:
    /// - Large switch statement that's hard to maintain
    /// - Adding new discount types requires modifying existing code
    /// - Difficult to test individual discount strategies
    /// - Violates Open/Closed Principle
    /// 
    /// YOUR TASK: Refactor this using the Strategy Pattern
    /// </summary>
    
    public enum CustomerType
    {
        Regular,
        Premium,
        VIP
    }
    
    public enum DiscountType
    {
        None,
        Member,
        Seasonal,
        Bulk
    }
    
    public class ShoppingCart
    {
        public decimal TotalAmount { get; set; }
        public CustomerType CustomerType { get; set; }
        public bool IsSeasonalSale { get; set; }
        public int ItemCount { get; set; }
        
        // TODO: Remove this method and replace with Strategy Pattern
        public decimal CalculateDiscount(DiscountType discountType)
        {
            // PROBLEM: This switch statement will grow larger as we add more discount types
            switch (discountType)
            {
                case DiscountType.None:
                    return 0;
                    
                case DiscountType.Member:
                    if (CustomerType == CustomerType.Premium)
                        return TotalAmount * 0.10m;
                    else if (CustomerType == CustomerType.VIP)
                        return TotalAmount * 0.15m;
                    return 0;
                    
                case DiscountType.Seasonal:
                    return IsSeasonalSale ? TotalAmount * 0.20m : 0;
                    
                case DiscountType.Bulk:
                    if (TotalAmount >= 500)
                        return TotalAmount * 0.10m;
                    else if (TotalAmount >= 100)
                        return TotalAmount * 0.05m;
                    return 0;
                    
                default:
                    return 0;
            }
        }
        
        public decimal GetFinalAmount(DiscountType discountType)
        {
            decimal discount = CalculateDiscount(discountType);
            return TotalAmount - discount;
        }
    }
    
    // TODO: Implement the Strategy Pattern below
    // 1. Create IDiscountStrategy interface
    // 2. Create concrete strategy classes
    // 3. Create DiscountContext class
    // 4. Refactor ShoppingCart to use strategies
    
    class Program
    {
        static void Main(string[] args)
        {
            // Test the current implementation
            var cart = new ShoppingCart
            {
                TotalAmount = 200m,
                CustomerType = CustomerType.Premium,
                IsSeasonalSale = true,
                ItemCount = 5
            };
            
            Console.WriteLine($"Original Amount: ${cart.TotalAmount:F2}");
            Console.WriteLine($"Member Discount: ${cart.CalculateDiscount(DiscountType.Member):F2}");
            Console.WriteLine($"Seasonal Discount: ${cart.CalculateDiscount(DiscountType.Seasonal):F2}");
            Console.WriteLine($"Bulk Discount: ${cart.CalculateDiscount(DiscountType.Bulk):F2}");
            
            // TODO: Replace this with Strategy Pattern implementation
        }
    }
}