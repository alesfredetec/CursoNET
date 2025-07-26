using System;
using System.Collections.Generic;

namespace DesignPatterns.StrategyPattern
{
    /// <summary>
    /// AFTER: E-commerce system using Strategy Pattern for discount calculations
    /// 
    /// BENEFITS:
    /// ✅ Each discount strategy is in its own class
    /// ✅ Easy to add new discount types without modifying existing code
    /// ✅ Each strategy can be tested independently
    /// ✅ Follows Open/Closed Principle
    /// ✅ Clean, maintainable code structure
    /// </summary>
    
    #region Strategy Pattern Implementation
    
    /// <summary>
    /// Strategy interface defining the contract for all discount strategies
    /// </summary>
    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(ShoppingCart cart);
        string GetDiscountDescription();
    }
    
    /// <summary>
    /// Context class that uses discount strategies
    /// </summary>
    public class DiscountContext
    {
        private IDiscountStrategy _discountStrategy;
        
        public DiscountContext(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy ?? throw new ArgumentNullException(nameof(discountStrategy));
        }
        
        public void SetStrategy(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy ?? throw new ArgumentNullException(nameof(discountStrategy));
        }
        
        public decimal ApplyDiscount(ShoppingCart cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));
            return _discountStrategy.CalculateDiscount(cart);
        }
        
        public string GetCurrentDiscountDescription()
        {
            return _discountStrategy.GetDiscountDescription();
        }
    }
    
    #endregion
    
    #region Concrete Strategy Implementations
    
    /// <summary>
    /// No discount strategy for regular customers
    /// </summary>
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(ShoppingCart cart)
        {
            return 0m;
        }
        
        public string GetDiscountDescription()
        {
            return "No discount applied";
        }
    }
    
    /// <summary>
    /// Member discount strategy based on customer type
    /// </summary>
    public class MemberDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(ShoppingCart cart)
        {
            if (cart == null) return 0m;
            
            return cart.CustomerType switch
            {
                CustomerType.Premium => cart.TotalAmount * 0.10m,
                CustomerType.VIP => cart.TotalAmount * 0.15m,
                CustomerType.Regular => 0m,
                _ => 0m
            };
        }
        
        public string GetDiscountDescription()
        {
            return "Member discount: Premium 10%, VIP 15%";
        }
    }
    
    /// <summary>
    /// Seasonal sale discount strategy
    /// </summary>
    public class SeasonalDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(ShoppingCart cart)
        {
            if (cart == null || !cart.IsSeasonalSale) return 0m;
            return cart.TotalAmount * 0.20m;
        }
        
        public string GetDiscountDescription()
        {
            return "Seasonal sale: 20% off during promotional periods";
        }
    }
    
    /// <summary>
    /// Bulk purchase discount strategy
    /// </summary>
    public class BulkDiscountStrategy : IDiscountStrategy
    {
        private const decimal BulkTier1Threshold = 100m;
        private const decimal BulkTier2Threshold = 500m;
        private const decimal BulkTier1Discount = 0.05m;
        private const decimal BulkTier2Discount = 0.10m;
        
        public decimal CalculateDiscount(ShoppingCart cart)
        {
            if (cart == null) return 0m;
            
            if (cart.TotalAmount >= BulkTier2Threshold)
                return cart.TotalAmount * BulkTier2Discount;
            else if (cart.TotalAmount >= BulkTier1Threshold)
                return cart.TotalAmount * BulkTier1Discount;
            else
                return 0m;
        }
        
        public string GetDiscountDescription()
        {
            return $"Bulk discount: 5% over ${BulkTier1Threshold}, 10% over ${BulkTier2Threshold}";
        }
    }
    
    /// <summary>
    /// VIP special discount combining member benefits with free shipping
    /// </summary>
    public class VIPSpecialStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(ShoppingCart cart)
        {
            if (cart == null || cart.CustomerType != CustomerType.VIP) return 0m;
            
            // VIP gets 15% discount plus free shipping (valued at $10)
            decimal discountAmount = cart.TotalAmount * 0.15m;
            decimal freeShippingValue = 10m;
            
            return discountAmount + freeShippingValue;
        }
        
        public string GetDiscountDescription()
        {
            return "VIP Special: 15% discount + free shipping ($10 value)";
        }
    }
    
    #endregion
    
    #region Enhanced Shopping Cart
    
    public enum CustomerType
    {
        Regular,
        Premium,
        VIP
    }
    
    /// <summary>
    /// Enhanced shopping cart that works with the Strategy Pattern
    /// </summary>
    public class ShoppingCart
    {
        public decimal TotalAmount { get; set; }
        public CustomerType CustomerType { get; set; }
        public bool IsSeasonalSale { get; set; }
        public int ItemCount { get; set; }
        
        private DiscountContext _discountContext;
        
        public ShoppingCart()
        {
            // Default to no discount
            _discountContext = new DiscountContext(new NoDiscountStrategy());
        }
        
        public void SetDiscountStrategy(IDiscountStrategy strategy)
        {
            _discountContext.SetStrategy(strategy);
        }
        
        public decimal CalculateDiscount()
        {
            return _discountContext.ApplyDiscount(this);
        }
        
        public decimal GetFinalAmount()
        {
            decimal discount = CalculateDiscount();
            return Math.Max(0, TotalAmount - discount); // Ensure non-negative result
        }
        
        public string GetDiscountDescription()
        {
            return _discountContext.GetCurrentDiscountDescription();
        }
    }
    
    #endregion
    
    #region Strategy Factory (Bonus)
    
    /// <summary>
    /// Factory to automatically select the best discount strategy
    /// </summary>
    public static class DiscountStrategyFactory
    {
        public static IDiscountStrategy GetBestStrategy(ShoppingCart cart)
        {
            if (cart == null) return new NoDiscountStrategy();
            
            var strategies = new List<IDiscountStrategy>
            {
                new NoDiscountStrategy(),
                new MemberDiscountStrategy(),
                new SeasonalDiscountStrategy(),
                new BulkDiscountStrategy(),
                new VIPSpecialStrategy()
            };
            
            IDiscountStrategy bestStrategy = strategies[0];
            decimal bestDiscount = 0m;
            
            foreach (var strategy in strategies)
            {
                decimal discount = strategy.CalculateDiscount(cart);
                if (discount > bestDiscount)
                {
                    bestDiscount = discount;
                    bestStrategy = strategy;
                }
            }
            
            return bestStrategy;
        }
    }
    
    #endregion
    
    #region Demo Program
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== E-commerce Discount System with Strategy Pattern ===");
            
            // Create test shopping carts
            var regularCart = new ShoppingCart
            {
                TotalAmount = 150m,
                CustomerType = CustomerType.Regular,
                IsSeasonalSale = false,
                ItemCount = 3
            };
            
            var premiumCart = new ShoppingCart
            {
                TotalAmount = 600m,
                CustomerType = CustomerType.Premium,
                IsSeasonalSale = true,
                ItemCount = 10
            };
            
            var vipCart = new ShoppingCart
            {
                TotalAmount = 300m,
                CustomerType = CustomerType.VIP,
                IsSeasonalSale = false,
                ItemCount = 5
            };
            
            // Demonstrate different strategies
            Console.WriteLine("\\n--- Regular Customer ---");
            TestDiscountStrategies(regularCart);
            
            Console.WriteLine("\\n--- Premium Customer ---");
            TestDiscountStrategies(premiumCart);
            
            Console.WriteLine("\\n--- VIP Customer ---");
            TestDiscountStrategies(vipCart);
            
            // Demonstrate automatic best strategy selection
            Console.WriteLine("\\n--- Automatic Best Strategy Selection ---");
            DemonstrateAutomaticSelection(premiumCart);
        }
        
        private static void TestDiscountStrategies(ShoppingCart cart)
        {
            Console.WriteLine($"Cart Total: ${cart.TotalAmount:F2} | Customer: {cart.CustomerType} | Seasonal: {cart.IsSeasonalSale}");
            
            var strategies = new List<(string name, IDiscountStrategy strategy)>
            {
                ("No Discount", new NoDiscountStrategy()),
                ("Member Discount", new MemberDiscountStrategy()),
                ("Seasonal Discount", new SeasonalDiscountStrategy()),
                ("Bulk Discount", new BulkDiscountStrategy()),
                ("VIP Special", new VIPSpecialStrategy())
            };
            
            foreach (var (name, strategy) in strategies)
            {
                cart.SetDiscountStrategy(strategy);
                decimal discount = cart.CalculateDiscount();
                decimal final = cart.GetFinalAmount();
                
                Console.WriteLine($"  {name}: -${discount:F2} = ${final:F2}");
            }
        }
        
        private static void DemonstrateAutomaticSelection(ShoppingCart cart)
        {
            var bestStrategy = DiscountStrategyFactory.GetBestStrategy(cart);
            cart.SetDiscountStrategy(bestStrategy);
            
            Console.WriteLine($"Best strategy for ${cart.TotalAmount:F2} cart: {cart.GetDiscountDescription()}");
            Console.WriteLine($"Discount: ${cart.CalculateDiscount():F2}");
            Console.WriteLine($"Final Amount: ${cart.GetFinalAmount():F2}");
        }
    }
    
    #endregion
}