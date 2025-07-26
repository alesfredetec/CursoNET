using System;
using System.Collections.Generic;
using System.Text;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Generates exercises for Design Patterns (Strategy, Factory, Observer, etc.)
    /// </summary>
    public class DesignPatternsGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.DesignPatterns;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            if (!ValidateConfiguration(config))
                throw new ArgumentException("Invalid configuration for Design Patterns");

            return config.Type switch
            {
                ExerciseType.Implementation => GenerateStrategyPatternExercise(config),
                ExerciseType.Refactoring => GenerateRefactoringToPatternExercise(config),
                ExerciseType.Extension => GenerateExtensionExercise(config),
                ExerciseType.Design => GenerateDesignExercise(config),
                _ => GenerateStrategyPatternExercise(config)
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.DesignPatterns &&
                   config.Level >= SkillLevel.Intermediate;
        }

        private Exercise GenerateStrategyPatternExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "E-commerce Discount Strategy Pattern",
                Level = config.Level,
                Topic = TopicArea.DesignPatterns,
                Type = ExerciseType.Implementation,
                EstimatedMinutes = 45
            };

            exercise.Description = @"Implement the Strategy Pattern to handle different discount calculation strategies in an e-commerce system.

This exercise demonstrates how the Strategy Pattern eliminates complex conditional logic and makes the system easily extensible for new discount types.

Real-world application: E-commerce platforms need flexible discount systems that can handle seasonal sales, member discounts, bulk purchase discounts, and promotional codes without modifying existing code.";

            exercise.LearningObjectives.AddRange(new[]
            {
                "Implement the Strategy Pattern to eliminate conditional logic",
                "Create a flexible, extensible discount calculation system",
                "Understand how Strategy Pattern follows Open/Closed Principle",
                "Practice dependency injection and inversion of control",
                "Design maintainable object-oriented solutions"
            });

            exercise.Prerequisites.AddRange(new[]
            {
                "Understanding of interfaces and abstract classes",
                "Knowledge of polymorphism and inheritance",
                "Familiarity with SOLID principles (especially Open/Closed)",
                "Basic understanding of dependency injection concepts"
            });

            exercise.ProblemStatement = @"You're building an e-commerce system that needs to calculate discounts for different customer types and scenarios:

1. **Regular Customer**: No discount (0%)
2. **Premium Member**: 10% discount on all purchases
3. **VIP Member**: 15% discount + free shipping
4. **Seasonal Sales**: 20% discount during special periods
5. **Bulk Purchase**: 5% discount for orders over $100, 10% for orders over $500

Currently, the system uses a large switch statement that becomes harder to maintain as new discount types are added. Your task is to refactor this using the Strategy Pattern.

**Requirements:**
- Each discount strategy should be a separate class
- The system should be easily extensible for new discount types
- Discount calculation should be consistent and testable
- The client code should be clean and simple";

            exercise.TechnicalRequirements.AddRange(new[]
            {
                "Implement IDiscountStrategy interface",
                "Create concrete strategy classes for each discount type",
                "Implement DiscountContext class to manage strategies",
                "Use dependency injection to set strategies",
                "Include proper error handling and validation",
                "Follow SOLID principles, especially Open/Closed"
            });

            exercise.SuccessCriteria.AddRange(new[]
            {
                "All discount strategies implement the same interface",
                "Context class properly delegates to strategies",
                "New discount types can be added without modifying existing code",
                "All calculations are accurate and tested",
                "Client code is clean and doesn't contain business logic",
                "Code demonstrates proper object-oriented design"
            });

            exercise.StarterCode = GenerateStrategyStarterCode();
            exercise.SolutionCode = GenerateStrategySolutionCode();
            exercise.UnitTestCode = GenerateStrategyTestCode();

            exercise.ExtensionChallenges.AddRange(new[]
            {
                "Add combination discounts (e.g., member + seasonal)",
                "Implement discount validation rules and business constraints",
                "Add logging and audit trail for discount applications",
                "Create a discount factory to automatically select appropriate strategies",
                "Implement time-based discount activation/deactivation",
                "Add discount priority system for overlapping discounts"
            });

            exercise.CommonPitfalls.AddRange(new[]
            {
                "Don't create strategies with state - keep them stateless for reusability",
                "Avoid tight coupling between context and specific strategy implementations",
                "Remember to validate input parameters in both context and strategies",
                "Consider edge cases like negative amounts or null values",
                "Don't forget to test boundary conditions (exactly $100, $500, etc.)"
            });

            return exercise;
        }

        private Exercise GenerateRefactoringToPatternExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "Refactor Payment Processing to Strategy Pattern",
                Level = config.Level,
                Topic = TopicArea.DesignPatterns,
                Type = ExerciseType.Refactoring,
                EstimatedMinutes = 40
            };

            exercise.Description = @"Refactor a payment processing system that uses complex if/switch statements into a clean Strategy Pattern implementation.

This exercise shows how to identify and eliminate code smells related to conditional complexity while improving maintainability and extensibility.";

            exercise.LearningObjectives.AddRange(new[]
            {
                "Identify conditional complexity code smells",
                "Refactor procedural code to object-oriented patterns",
                "Apply the Strategy Pattern to eliminate switch statements",
                "Improve code maintainability and testability"
            });

            exercise.StarterCode = GenerateRefactoringStarterCode();
            exercise.SolutionCode = GenerateRefactoringSolutionCode();

            return exercise;
        }

        private Exercise GenerateDesignExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "Design a Notification System with Observer Pattern",
                Level = SkillLevel.Advanced,
                Topic = TopicArea.DesignPatterns,
                Type = ExerciseType.Design,
                EstimatedMinutes = 60
            };

            exercise.Description = @"Design and implement a comprehensive notification system using the Observer Pattern.

This exercise challenges you to create a scalable, loosely-coupled notification system that can handle multiple notification types and delivery methods.";

            exercise.LearningObjectives.AddRange(new[]
            {
                "Design systems using the Observer Pattern",
                "Create loosely-coupled, event-driven architectures",
                "Implement publish-subscribe mechanisms",
                "Handle complex notification routing and filtering"
            });

            exercise.StarterCode = GenerateObserverStarterCode();
            exercise.SolutionCode = GenerateObserverSolutionCode();

            return exercise;
        }

        private Exercise GenerateExtensionExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "Extend Logger with Decorator Pattern",
                Level = config.Level,
                Topic = TopicArea.DesignPatterns,
                Type = ExerciseType.Extension,
                EstimatedMinutes = 35
            };

            exercise.Description = @"Extend a basic logging system using the Decorator Pattern to add features like encryption, compression, and formatting without modifying the core logger.";

            exercise.StarterCode = GenerateDecoratorStarterCode();
            exercise.SolutionCode = GenerateDecoratorSolutionCode();

            return exercise;
        }

        #region Code Generation Methods

        private string GenerateStrategyStarterCode()
        {
            return @"using System;

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
            
            Console.WriteLine($""Original Amount: ${cart.TotalAmount:F2}"");
            Console.WriteLine($""Member Discount: ${cart.CalculateDiscount(DiscountType.Member):F2}"");
            Console.WriteLine($""Seasonal Discount: ${cart.CalculateDiscount(DiscountType.Seasonal):F2}"");
            Console.WriteLine($""Bulk Discount: ${cart.CalculateDiscount(DiscountType.Bulk):F2}"");
            
            // TODO: Replace this with Strategy Pattern implementation
        }
    }
}";
        }

        private string GenerateStrategySolutionCode()
        {
            return @"using System;
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
            return ""No discount applied"";
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
            return ""Member discount: Premium 10%, VIP 15%"";
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
            return ""Seasonal sale: 20% off during promotional periods"";
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
            return $""Bulk discount: 5% over ${BulkTier1Threshold}, 10% over ${BulkTier2Threshold}"";
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
            return ""VIP Special: 15% discount + free shipping ($10 value)"";
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
            Console.WriteLine(""=== E-commerce Discount System with Strategy Pattern ==="");
            
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
            Console.WriteLine(""\\n--- Regular Customer ---"");
            TestDiscountStrategies(regularCart);
            
            Console.WriteLine(""\\n--- Premium Customer ---"");
            TestDiscountStrategies(premiumCart);
            
            Console.WriteLine(""\\n--- VIP Customer ---"");
            TestDiscountStrategies(vipCart);
            
            // Demonstrate automatic best strategy selection
            Console.WriteLine(""\\n--- Automatic Best Strategy Selection ---"");
            DemonstrateAutomaticSelection(premiumCart);
        }
        
        private static void TestDiscountStrategies(ShoppingCart cart)
        {
            Console.WriteLine($""Cart Total: ${cart.TotalAmount:F2} | Customer: {cart.CustomerType} | Seasonal: {cart.IsSeasonalSale}"");
            
            var strategies = new List<(string name, IDiscountStrategy strategy)>
            {
                (""No Discount"", new NoDiscountStrategy()),
                (""Member Discount"", new MemberDiscountStrategy()),
                (""Seasonal Discount"", new SeasonalDiscountStrategy()),
                (""Bulk Discount"", new BulkDiscountStrategy()),
                (""VIP Special"", new VIPSpecialStrategy())
            };
            
            foreach (var (name, strategy) in strategies)
            {
                cart.SetDiscountStrategy(strategy);
                decimal discount = cart.CalculateDiscount();
                decimal final = cart.GetFinalAmount();
                
                Console.WriteLine($""  {name}: -${discount:F2} = ${final:F2}"");
            }
        }
        
        private static void DemonstrateAutomaticSelection(ShoppingCart cart)
        {
            var bestStrategy = DiscountStrategyFactory.GetBestStrategy(cart);
            cart.SetDiscountStrategy(bestStrategy);
            
            Console.WriteLine($""Best strategy for ${cart.TotalAmount:F2} cart: {cart.GetDiscountDescription()}"");
            Console.WriteLine($""Discount: ${cart.CalculateDiscount():F2}"");
            Console.WriteLine($""Final Amount: ${cart.GetFinalAmount():F2}"");
        }
    }
    
    #endregion
}";
        }

        private string GenerateStrategyTestCode()
        {
            return @"using System;
using Xunit;

namespace DesignPatterns.StrategyPattern.Tests
{
    /// <summary>
    /// Unit tests for the Strategy Pattern implementation
    /// Demonstrates how strategies can be tested independently
    /// </summary>
    public class DiscountStrategyTests
    {
        #region Strategy Tests
        
        [Fact]
        public void NoDiscountStrategy_Should_Return_Zero()
        {
            // Arrange
            var strategy = new NoDiscountStrategy();
            var cart = new ShoppingCart { TotalAmount = 100m };
            
            // Act
            var discount = strategy.CalculateDiscount(cart);
            
            // Assert
            Assert.Equal(0m, discount);
        }
        
        [Theory]
        [InlineData(CustomerType.Regular, 100, 0)]
        [InlineData(CustomerType.Premium, 100, 10)]
        [InlineData(CustomerType.VIP, 100, 15)]
        public void MemberDiscountStrategy_Should_Calculate_Correct_Discount(CustomerType customerType, decimal amount, decimal expectedDiscount)
        {
            // Arrange
            var strategy = new MemberDiscountStrategy();
            var cart = new ShoppingCart 
            { 
                TotalAmount = amount, 
                CustomerType = customerType 
            };
            
            // Act
            var discount = strategy.CalculateDiscount(cart);
            
            // Assert
            Assert.Equal(expectedDiscount, discount);
        }
        
        [Theory]
        [InlineData(true, 100, 20)]
        [InlineData(false, 100, 0)]
        public void SeasonalDiscountStrategy_Should_Apply_Only_During_Sale(bool isSeasonalSale, decimal amount, decimal expectedDiscount)
        {
            // Arrange
            var strategy = new SeasonalDiscountStrategy();
            var cart = new ShoppingCart 
            { 
                TotalAmount = amount, 
                IsSeasonalSale = isSeasonalSale 
            };
            
            // Act
            var discount = strategy.CalculateDiscount(cart);
            
            // Assert
            Assert.Equal(expectedDiscount, discount);
        }
        
        [Theory]
        [InlineData(50, 0)]    // Below tier 1
        [InlineData(100, 5)]   // Tier 1: 5%
        [InlineData(200, 10)]  // Tier 1: 5%
        [InlineData(500, 50)]  // Tier 2: 10%
        [InlineData(1000, 100)] // Tier 2: 10%
        public void BulkDiscountStrategy_Should_Apply_Correct_Tiers(decimal amount, decimal expectedDiscount)
        {
            // Arrange
            var strategy = new BulkDiscountStrategy();
            var cart = new ShoppingCart { TotalAmount = amount };
            
            // Act
            var discount = strategy.CalculateDiscount(cart);
            
            // Assert
            Assert.Equal(expectedDiscount, discount);
        }
        
        [Theory]
        [InlineData(CustomerType.VIP, 100, 25)] // 15% + $10 shipping
        [InlineData(CustomerType.Premium, 100, 0)] // Not VIP
        [InlineData(CustomerType.Regular, 100, 0)] // Not VIP
        public void VIPSpecialStrategy_Should_Apply_Only_To_VIP(CustomerType customerType, decimal amount, decimal expectedDiscount)
        {
            // Arrange
            var strategy = new VIPSpecialStrategy();
            var cart = new ShoppingCart 
            { 
                TotalAmount = amount, 
                CustomerType = customerType 
            };
            
            // Act
            var discount = strategy.CalculateDiscount(cart);
            
            // Assert
            Assert.Equal(expectedDiscount, discount);
        }
        
        #endregion
        
        #region Context Tests
        
        [Fact]
        public void DiscountContext_Should_Use_Injected_Strategy()
        {
            // Arrange
            var strategy = new MemberDiscountStrategy();
            var context = new DiscountContext(strategy);
            var cart = new ShoppingCart 
            { 
                TotalAmount = 100m, 
                CustomerType = CustomerType.Premium 
            };
            
            // Act
            var discount = context.ApplyDiscount(cart);
            
            // Assert
            Assert.Equal(10m, discount);
        }
        
        [Fact]
        public void DiscountContext_Should_Allow_Strategy_Change()
        {
            // Arrange
            var context = new DiscountContext(new NoDiscountStrategy());
            var cart = new ShoppingCart { TotalAmount = 100m };
            
            // Act & Assert - Initial strategy
            Assert.Equal(0m, context.ApplyDiscount(cart));
            
            // Change strategy
            context.SetStrategy(new SeasonalDiscountStrategy());
            cart.IsSeasonalSale = true;
            
            // Act & Assert - New strategy
            Assert.Equal(20m, context.ApplyDiscount(cart));
        }
        
        [Fact]
        public void DiscountContext_Should_Throw_When_Strategy_Is_Null()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DiscountContext(null));
        }
        
        #endregion
        
        #region Shopping Cart Tests
        
        [Fact]
        public void ShoppingCart_Should_Use_Default_No_Discount_Strategy()
        {
            // Arrange
            var cart = new ShoppingCart { TotalAmount = 100m };
            
            // Act
            var discount = cart.CalculateDiscount();
            
            // Assert
            Assert.Equal(0m, discount);
            Assert.Equal(100m, cart.GetFinalAmount());
        }
        
        [Fact]
        public void ShoppingCart_Should_Allow_Strategy_Change()
        {
            // Arrange
            var cart = new ShoppingCart 
            { 
                TotalAmount = 100m, 
                CustomerType = CustomerType.Premium 
            };
            
            // Act & Assert - Default strategy
            Assert.Equal(0m, cart.CalculateDiscount());
            
            // Change to member discount
            cart.SetDiscountStrategy(new MemberDiscountStrategy());
            Assert.Equal(10m, cart.CalculateDiscount());
            Assert.Equal(90m, cart.GetFinalAmount());
        }
        
        [Fact]
        public void ShoppingCart_Should_Not_Return_Negative_Final_Amount()
        {
            // Arrange
            var cart = new ShoppingCart { TotalAmount = 10m };
            cart.SetDiscountStrategy(new SeasonalDiscountStrategy());
            cart.IsSeasonalSale = true; // 20% discount = $2, but what if discount > total?
            
            // Create a custom strategy that gives huge discount
            cart.SetDiscountStrategy(new TestStrategy(discount: 50m)); // $50 discount on $10 purchase
            
            // Act
            var finalAmount = cart.GetFinalAmount();
            
            // Assert
            Assert.True(finalAmount >= 0);
        }
        
        #endregion
        
        #region Factory Tests
        
        [Fact]
        public void DiscountStrategyFactory_Should_Select_Best_Strategy()
        {
            // Arrange - VIP customer during seasonal sale with bulk purchase
            var cart = new ShoppingCart
            {
                TotalAmount = 600m,
                CustomerType = CustomerType.VIP,
                IsSeasonalSale = true
            };
            
            // Act
            var bestStrategy = DiscountStrategyFactory.GetBestStrategy(cart);
            var discount = bestStrategy.CalculateDiscount(cart);
            
            // Assert
            // VIP Special: 15% + $10 = $90 + $10 = $100
            // Seasonal: 20% = $120
            // Bulk: 10% = $60
            // Best should be Seasonal with $120 discount
            Assert.Equal(120m, discount);
            Assert.IsType<SeasonalDiscountStrategy>(bestStrategy);
        }
        
        [Fact]
        public void DiscountStrategyFactory_Should_Handle_Null_Cart()
        {
            // Act
            var strategy = DiscountStrategyFactory.GetBestStrategy(null);
            
            // Assert
            Assert.IsType<NoDiscountStrategy>(strategy);
        }
        
        #endregion
        
        #region Helper Classes for Testing
        
        private class TestStrategy : IDiscountStrategy
        {
            private readonly decimal _discount;
            
            public TestStrategy(decimal discount)
            {
                _discount = discount;
            }
            
            public decimal CalculateDiscount(ShoppingCart cart)
            {
                return _discount;
            }
            
            public string GetDiscountDescription()
            {
                return $""Test strategy with ${_discount} discount"";
            }
        }
        
        #endregion
    }
}";
        }

        private string GenerateRefactoringStarterCode()
        {
            return @"using System;

namespace DesignPatterns.RefactorMe
{
    /// <summary>
    /// LEGACY PAYMENT PROCESSING SYSTEM - Needs Refactoring
    /// 
    /// This payment processor has grown organically and now contains
    /// complex conditional logic that's hard to maintain and extend.
    /// 
    /// YOUR TASK: Refactor this to use the Strategy Pattern
    /// </summary>
    public class PaymentProcessor
    {
        public bool ProcessPayment(decimal amount, string paymentMethod, string cardNumber = null, 
            string bankAccount = null, string cryptoWallet = null, bool isExpressPayment = false)
        {
            // PROBLEM: This method has too many responsibilities and parameters
            // Adding new payment methods requires modifying this method
            
            switch (paymentMethod.ToLower())
            {
                case ""creditcard"":
                    Console.WriteLine(""Processing credit card payment..."");
                    if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 16)
                    {
                        Console.WriteLine(""Invalid card number"");
                        return false;
                    }
                    
                    // Simulate credit card processing
                    decimal fee = amount * 0.029m; // 2.9% fee
                    if (isExpressPayment) fee += 1.50m; // Express fee
                    
                    Console.WriteLine($""Card ending in {cardNumber.Substring(cardNumber.Length - 4)}"");
                    Console.WriteLine($""Processing fee: ${fee:F2}"");
                    Console.WriteLine($""Total charged: ${amount + fee:F2}"");
                    return SimulateProcessing();
                    
                case ""banktransfer"":
                    Console.WriteLine(""Processing bank transfer..."");
                    if (string.IsNullOrEmpty(bankAccount))
                    {
                        Console.WriteLine(""Bank account required"");
                        return false;
                    }
                    
                    // Bank transfers have different fee structure
                    decimal bankFee = amount > 1000 ? 0 : 5.00m; // Free over $1000
                    Console.WriteLine($""Bank account: {bankAccount}"");
                    Console.WriteLine($""Transfer fee: ${bankFee:F2}"");
                    Console.WriteLine($""Net amount: ${amount - bankFee:F2}"");
                    return SimulateProcessing();
                    
                case ""paypal"":
                    Console.WriteLine(""Processing PayPal payment..."");
                    decimal paypalFee = amount * 0.034m + 0.30m; // 3.4% + $0.30
                    Console.WriteLine($""PayPal fee: ${paypalFee:F2}"");
                    Console.WriteLine($""Total: ${amount + paypalFee:F2}"");
                    return SimulateProcessing();
                    
                case ""cryptocurrency"":
                    Console.WriteLine(""Processing cryptocurrency payment..."");
                    if (string.IsNullOrEmpty(cryptoWallet))
                    {
                        Console.WriteLine(""Crypto wallet address required"");
                        return false;
                    }
                    
                    // Crypto has network fees
                    decimal networkFee = 0.0005m * amount; // 0.05%
                    Console.WriteLine($""Wallet: {cryptoWallet}"");
                    Console.WriteLine($""Network fee: ${networkFee:F2}"");
                    Console.WriteLine($""Amount after fees: ${amount - networkFee:F2}"");
                    return SimulateProcessing();
                    
                case ""applepay"":
                    Console.WriteLine(""Processing Apple Pay..."");
                    // Apple Pay uses existing card, but different fee structure
                    decimal appleFee = amount * 0.025m; // 2.5%
                    Console.WriteLine($""Apple Pay fee: ${appleFee:F2}"");
                    Console.WriteLine($""Total: ${amount + appleFee:F2}"");
                    return SimulateProcessing();
                    
                default:
                    Console.WriteLine($""Unsupported payment method: {paymentMethod}"");
                    return false;
            }
        }
        
        private bool SimulateProcessing()
        {
            // Simulate network delay and random success/failure
            System.Threading.Thread.Sleep(1000);
            return new Random().Next(1, 11) > 2; // 80% success rate
        }
    }
    
    // Usage example showing the problems
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new PaymentProcessor();
            
            // Look at all these parameters and conditional logic!
            processor.ProcessPayment(100m, ""creditcard"", cardNumber: ""1234567890123456"");
            processor.ProcessPayment(500m, ""banktransfer"", bankAccount: ""ACC123456"");
            processor.ProcessPayment(25m, ""paypal"");
            processor.ProcessPayment(200m, ""cryptocurrency"", cryptoWallet: ""1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa"");
            
            // TODO: Refactor to use Strategy Pattern for cleaner, more maintainable code
        }
    }
}";
        }

        private string GenerateRefactoringSolutionCode()
        {
            return @"using System;
using System.Collections.Generic;

namespace DesignPatterns.RefactoredPayments
{
    /// <summary>
    /// REFACTORED PAYMENT PROCESSING SYSTEM - Using Strategy Pattern
    /// 
    /// BENEFITS:
    /// ✅ Each payment method is encapsulated in its own class
    /// ✅ Easy to add new payment methods without modifying existing code
    /// ✅ Clean separation of concerns
    /// ✅ Each payment strategy can be tested independently
    /// ✅ Follows Open/Closed Principle
    /// </summary>
    
    #region Payment Strategy Pattern
    
    /// <summary>
    /// Payment result containing processing outcome and details
    /// </summary>
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public decimal ProcessingFee { get; set; }
        public decimal FinalAmount { get; set; }
        public Dictionary<string, object> Details { get; set; } = new();
    }
    
    /// <summary>
    /// Payment request containing all necessary information
    /// </summary>
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string BankAccount { get; set; }
        public string CryptoWallet { get; set; }
        public bool IsExpressPayment { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; } = new();
    }
    
    /// <summary>
    /// Strategy interface for payment processing
    /// </summary>
    public interface IPaymentStrategy
    {
        PaymentResult ProcessPayment(PaymentRequest request);
        string GetPaymentMethodName();
        bool ValidateRequest(PaymentRequest request);
    }
    
    /// <summary>
    /// Context class for payment processing
    /// </summary>
    public class PaymentProcessor
    {
        private readonly Dictionary<string, IPaymentStrategy> _strategies;
        
        public PaymentProcessor()
        {
            _strategies = new Dictionary<string, IPaymentStrategy>(StringComparer.OrdinalIgnoreCase);
            RegisterDefaultStrategies();
        }
        
        public void RegisterStrategy(string methodName, IPaymentStrategy strategy)
        {
            _strategies[methodName] = strategy;
        }
        
        public PaymentResult ProcessPayment(string paymentMethod, PaymentRequest request)
        {
            if (!_strategies.TryGetValue(paymentMethod, out var strategy))
            {
                return new PaymentResult
                {
                    IsSuccessful = false,
                    Message = $""Unsupported payment method: {paymentMethod}""
                };
            }
            
            if (!strategy.ValidateRequest(request))
            {
                return new PaymentResult
                {
                    IsSuccessful = false,
                    Message = $""Invalid request for {strategy.GetPaymentMethodName()}""
                };
            }
            
            return strategy.ProcessPayment(request);
        }
        
        public List<string> GetSupportedMethods()
        {
            return new List<string>(_strategies.Keys);
        }
        
        private void RegisterDefaultStrategies()
        {
            RegisterStrategy(""creditcard"", new CreditCardStrategy());
            RegisterStrategy(""banktransfer"", new BankTransferStrategy());
            RegisterStrategy(""paypal"", new PayPalStrategy());
            RegisterStrategy(""cryptocurrency"", new CryptocurrencyStrategy());
            RegisterStrategy(""applepay"", new ApplePayStrategy());
        }
    }
    
    #endregion
    
    #region Concrete Payment Strategies
    
    /// <summary>
    /// Credit card payment strategy
    /// </summary>
    public class CreditCardStrategy : IPaymentStrategy
    {
        private const decimal StandardRate = 0.029m; // 2.9%
        private const decimal ExpressFee = 1.50m;
        
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            decimal fee = request.Amount * StandardRate;
            if (request.IsExpressPayment) fee += ExpressFee;
            
            bool success = SimulateProcessing();
            
            return new PaymentResult
            {
                IsSuccessful = success,
                Message = success ? ""Credit card payment processed successfully"" : ""Credit card payment failed"",
                ProcessingFee = fee,
                FinalAmount = request.Amount + fee,
                Details = new Dictionary<string, object>
                {
                    [""CardLast4""] = request.CardNumber.Substring(request.CardNumber.Length - 4),
                    [""ExpressPayment""] = request.IsExpressPayment
                }
            };
        }
        
        public string GetPaymentMethodName() => ""Credit Card"";
        
        public bool ValidateRequest(PaymentRequest request)
        {
            return !string.IsNullOrEmpty(request.CardNumber) && 
                   request.CardNumber.Length >= 16 && 
                   request.Amount > 0;
        }
        
        private bool SimulateProcessing() => new Random().Next(1, 11) > 2;
    }
    
    /// <summary>
    /// Bank transfer payment strategy
    /// </summary>
    public class BankTransferStrategy : IPaymentStrategy
    {
        private const decimal FeeThreshold = 1000m;
        private const decimal StandardFee = 5.00m;
        
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            decimal fee = request.Amount > FeeThreshold ? 0 : StandardFee;
            bool success = SimulateProcessing();
            
            return new PaymentResult
            {
                IsSuccessful = success,
                Message = success ? ""Bank transfer processed successfully"" : ""Bank transfer failed"",
                ProcessingFee = fee,
                FinalAmount = request.Amount - fee, // Fee deducted from amount
                Details = new Dictionary<string, object>
                {
                    [""BankAccount""] = request.BankAccount,
                    [""FreeTransfer""] = request.Amount > FeeThreshold
                }
            };
        }
        
        public string GetPaymentMethodName() => ""Bank Transfer"";
        
        public bool ValidateRequest(PaymentRequest request)
        {
            return !string.IsNullOrEmpty(request.BankAccount) && request.Amount > 0;
        }
        
        private bool SimulateProcessing() => new Random().Next(1, 11) > 1;
    }
    
    /// <summary>
    /// PayPal payment strategy
    /// </summary>
    public class PayPalStrategy : IPaymentStrategy
    {
        private const decimal PercentageFee = 0.034m; // 3.4%
        private const decimal FixedFee = 0.30m;
        
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            decimal fee = request.Amount * PercentageFee + FixedFee;
            bool success = SimulateProcessing();
            
            return new PaymentResult
            {
                IsSuccessful = success,
                Message = success ? ""PayPal payment processed successfully"" : ""PayPal payment failed"",
                ProcessingFee = fee,
                FinalAmount = request.Amount + fee,
                Details = new Dictionary<string, object>
                {
                    [""PaymentMethod""] = ""PayPal"",
                    [""PercentageFee""] = PercentageFee,
                    [""FixedFee""] = FixedFee
                }
            };
        }
        
        public string GetPaymentMethodName() => ""PayPal"";
        
        public bool ValidateRequest(PaymentRequest request)
        {
            return request.Amount > 0;
        }
        
        private bool SimulateProcessing() => new Random().Next(1, 11) > 2;
    }
    
    /// <summary>
    /// Cryptocurrency payment strategy
    /// </summary>
    public class CryptocurrencyStrategy : IPaymentStrategy
    {
        private const decimal NetworkFeeRate = 0.0005m; // 0.05%
        
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            decimal networkFee = request.Amount * NetworkFeeRate;
            bool success = SimulateProcessing();
            
            return new PaymentResult
            {
                IsSuccessful = success,
                Message = success ? ""Cryptocurrency payment processed successfully"" : ""Cryptocurrency payment failed"",
                ProcessingFee = networkFee,
                FinalAmount = request.Amount - networkFee, // Network fee deducted
                Details = new Dictionary<string, object>
                {
                    [""WalletAddress""] = request.CryptoWallet,
                    [""NetworkFee""] = networkFee
                }
            };
        }
        
        public string GetPaymentMethodName() => ""Cryptocurrency"";
        
        public bool ValidateRequest(PaymentRequest request)
        {
            return !string.IsNullOrEmpty(request.CryptoWallet) && 
                   request.CryptoWallet.Length >= 26 && 
                   request.Amount > 0;
        }
        
        private bool SimulateProcessing() => new Random().Next(1, 11) > 3; // Slightly lower success rate
    }
    
    /// <summary>
    /// Apple Pay payment strategy
    /// </summary>
    public class ApplePayStrategy : IPaymentStrategy
    {
        private const decimal ApplePayRate = 0.025m; // 2.5%
        
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            decimal fee = request.Amount * ApplePayRate;
            bool success = SimulateProcessing();
            
            return new PaymentResult
            {
                IsSuccessful = success,
                Message = success ? ""Apple Pay payment processed successfully"" : ""Apple Pay payment failed"",
                ProcessingFee = fee,
                FinalAmount = request.Amount + fee,
                Details = new Dictionary<string, object>
                {
                    [""PaymentMethod""] = ""Apple Pay"",
                    [""ProcessingRate""] = ApplePayRate
                }
            };
        }
        
        public string GetPaymentMethodName() => ""Apple Pay"";
        
        public bool ValidateRequest(PaymentRequest request)
        {
            return request.Amount > 0;
        }
        
        private bool SimulateProcessing() => new Random().Next(1, 11) > 1; // High success rate
    }
    
    #endregion
    
    #region Demo Program
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""=== Refactored Payment Processing System ==="");
            
            var processor = new PaymentProcessor();
            
            // Test credit card payment
            var creditCardRequest = new PaymentRequest
            {
                Amount = 100m,
                CardNumber = ""1234567890123456"",
                IsExpressPayment = true
            };
            
            var result = processor.ProcessPayment(""creditcard"", creditCardRequest);
            DisplayResult(""Credit Card"", result);
            
            // Test bank transfer
            var bankRequest = new PaymentRequest
            {
                Amount = 500m,
                BankAccount = ""ACC123456""
            };
            
            result = processor.ProcessPayment(""banktransfer"", bankRequest);
            DisplayResult(""Bank Transfer"", result);
            
            // Test PayPal
            var paypalRequest = new PaymentRequest { Amount = 25m };
            result = processor.ProcessPayment(""paypal"", paypalRequest);
            DisplayResult(""PayPal"", result);
            
            // Test cryptocurrency
            var cryptoRequest = new PaymentRequest
            {
                Amount = 200m,
                CryptoWallet = ""1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa""
            };
            
            result = processor.ProcessPayment(""cryptocurrency"", cryptoRequest);
            DisplayResult(""Cryptocurrency"", result);
            
            // Test Apple Pay
            var applePayRequest = new PaymentRequest { Amount = 75m };
            result = processor.ProcessPayment(""applepay"", applePayRequest);
            DisplayResult(""Apple Pay"", result);
            
            // Show supported methods
            Console.WriteLine(""\\nSupported payment methods:"");
            foreach (var method in processor.GetSupportedMethods())
            {
                Console.WriteLine($""- {method}"");
            }
        }
        
        private static void DisplayResult(string method, PaymentResult result)
        {
            Console.WriteLine($""\\n--- {method} ---"");
            Console.WriteLine($""Success: {result.IsSuccessful}"");
            Console.WriteLine($""Message: {result.Message}"");
            if (result.IsSuccessful)
            {
                Console.WriteLine($""Processing Fee: ${result.ProcessingFee:F2}"");
                Console.WriteLine($""Final Amount: ${result.FinalAmount:F2}"");
                
                if (result.Details.Any())
                {
                    Console.WriteLine(""Details:"");
                    foreach (var detail in result.Details)
                    {
                        Console.WriteLine($""  {detail.Key}: {detail.Value}"");
                    }
                }
            }
        }
    }
    
    #endregion
}";
        }

        private string GenerateObserverStarterCode()
        {
            return @"using System;
using System.Collections.Generic;

namespace DesignPatterns.ObserverPattern
{
    /// <summary>
    /// Notification System Design Challenge
    /// 
    /// REQUIREMENTS:
    /// Design a notification system that can:
    /// 1. Send notifications through multiple channels (email, SMS, push, in-app)
    /// 2. Allow users to subscribe/unsubscribe from different notification types
    /// 3. Support notification filtering and routing based on user preferences
    /// 4. Handle notification delivery failures gracefully
    /// 5. Allow for easy addition of new notification channels and types
    /// 
    /// YOUR TASK: Design and implement this system using the Observer Pattern
    /// </summary>
    
    public enum NotificationType
    {
        OrderConfirmation,
        PaymentReceived,
        ShippingUpdate,
        SecurityAlert,
        PromotionalOffer,
        SystemMaintenance
    }
    
    public enum NotificationChannel
    {
        Email,
        SMS,
        PushNotification,
        InApp
    }
    
    public class NotificationMessage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
    
    public class UserPreferences
    {
        public string UserId { get; set; }
        public Dictionary<NotificationType, List<NotificationChannel>> EnabledChannels { get; set; } = new();
        public bool IsOptedOut { get; set; }
        public TimeSpan QuietHoursStart { get; set; }
        public TimeSpan QuietHoursEnd { get; set; }
    }
    
    // TODO: Implement the Observer Pattern here
    // 1. Create INotificationObserver interface for notification handlers
    // 2. Create INotificationSubject interface for the notification publisher
    // 3. Implement concrete observers for each notification channel
    // 4. Implement NotificationCenter as the subject
    // 5. Add subscription management and filtering logic
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""=== Notification System Design Challenge ==="");
            Console.WriteLine(""TODO: Implement Observer Pattern for notification system"");
            
            // Example usage that should work after implementation:
            /*
            var notificationCenter = new NotificationCenter();
            
            // Register notification channels
            notificationCenter.Subscribe(new EmailNotificationHandler());
            notificationCenter.Subscribe(new SMSNotificationHandler());
            notificationCenter.Subscribe(new PushNotificationHandler());
            
            // Set user preferences
            var preferences = new UserPreferences
            {
                UserId = ""user123"",
                EnabledChannels = new Dictionary<NotificationType, List<NotificationChannel>>
                {
                    [NotificationType.OrderConfirmation] = new List<NotificationChannel> { NotificationChannel.Email, NotificationChannel.SMS },
                    [NotificationType.SecurityAlert] = new List<NotificationChannel> { NotificationChannel.Email, NotificationChannel.PushNotification }
                }
            };
            
            notificationCenter.SetUserPreferences(preferences);
            
            // Send notifications
            var orderNotification = new NotificationMessage
            {
                Type = NotificationType.OrderConfirmation,
                Title = ""Order Confirmed"",
                Content = ""Your order #12345 has been confirmed."",
                UserId = ""user123""
            };
            
            notificationCenter.SendNotification(orderNotification);
            */
        }
    }
}";
        }

        private string GenerateObserverSolutionCode()
        {
            return @"using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.ObserverPattern
{
    /// <summary>
    /// COMPLETE NOTIFICATION SYSTEM - Using Observer Pattern
    /// 
    /// This implementation demonstrates:
    /// ✅ Observer Pattern for loose coupling between publishers and subscribers
    /// ✅ Flexible notification routing based on user preferences
    /// ✅ Easy extension for new notification channels
    /// ✅ Graceful handling of delivery failures
    /// ✅ Sophisticated filtering and preference management
    /// </summary>
    
    #region Core Data Structures
    
    public enum NotificationType
    {
        OrderConfirmation,
        PaymentReceived,
        ShippingUpdate,
        SecurityAlert,
        PromotionalOffer,
        SystemMaintenance
    }
    
    public enum NotificationChannel
    {
        Email,
        SMS,
        PushNotification,
        InApp
    }
    
    public enum DeliveryStatus
    {
        Pending,
        Delivered,
        Failed,
        Filtered
    }
    
    public class NotificationMessage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Dictionary<string, object> Metadata { get; set; } = new();
        public int Priority { get; set; } = 1; // 1=Low, 5=High
    }
    
    public class DeliveryResult
    {
        public string MessageId { get; set; }
        public NotificationChannel Channel { get; set; }
        public DeliveryStatus Status { get; set; }
        public string StatusMessage { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
    
    public class UserPreferences
    {
        public string UserId { get; set; }
        public Dictionary<NotificationType, List<NotificationChannel>> EnabledChannels { get; set; } = new();
        public bool IsOptedOut { get; set; }
        public TimeSpan QuietHoursStart { get; set; } = TimeSpan.FromHours(22); // 10 PM
        public TimeSpan QuietHoursEnd { get; set; } = TimeSpan.FromHours(8);    // 8 AM
        public int MinimumPriority { get; set; } = 1;
    }
    
    #endregion
    
    #region Observer Pattern Interfaces
    
    /// <summary>
    /// Observer interface for notification handlers
    /// </summary>
    public interface INotificationObserver
    {
        NotificationChannel Channel { get; }
        DeliveryResult HandleNotification(NotificationMessage message);
        bool CanHandle(NotificationMessage message);
    }
    
    /// <summary>
    /// Subject interface for notification publishers
    /// </summary>
    public interface INotificationSubject
    {
        void Subscribe(INotificationObserver observer);
        void Unsubscribe(INotificationObserver observer);
        void SendNotification(NotificationMessage message);
    }
    
    #endregion
    
    #region Notification Center (Subject)
    
    /// <summary>
    /// Central notification system implementing the Subject role in Observer Pattern
    /// </summary>
    public class NotificationCenter : INotificationSubject
    {
        private readonly List<INotificationObserver> _observers = new();
        private readonly Dictionary<string, UserPreferences> _userPreferences = new();
        private readonly List<DeliveryResult> _deliveryHistory = new();
        
        public void Subscribe(INotificationObserver observer)
        {
            if (observer != null && !_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($""Subscribed {observer.Channel} notification handler"");
            }
        }
        
        public void Unsubscribe(INotificationObserver observer)
        {
            if (_observers.Remove(observer))
            {
                Console.WriteLine($""Unsubscribed {observer.Channel} notification handler"");
            }
        }
        
        public void SetUserPreferences(UserPreferences preferences)
        {
            if (preferences != null && !string.IsNullOrEmpty(preferences.UserId))
            {
                _userPreferences[preferences.UserId] = preferences;
                Console.WriteLine($""Updated preferences for user {preferences.UserId}"");
            }
        }
        
        public void SendNotification(NotificationMessage message)
        {
            if (message == null || string.IsNullOrEmpty(message.UserId))
            {
                Console.WriteLine(""Invalid notification message"");
                return;
            }
            
            Console.WriteLine($""\\n--- Processing Notification ---"");
            Console.WriteLine($""ID: {message.Id}"");
            Console.WriteLine($""Type: {message.Type}"");
            Console.WriteLine($""User: {message.UserId}"");
            Console.WriteLine($""Title: {message.Title}"");
            
            // Get user preferences
            var preferences = GetUserPreferences(message.UserId);
            
            // Check if user is opted out
            if (preferences.IsOptedOut)
            {
                LogDelivery(message.Id, NotificationChannel.Email, DeliveryStatus.Filtered, ""User opted out"");
                Console.WriteLine(""User has opted out of notifications"");
                return;
            }
            
            // Check quiet hours for non-critical notifications
            if (message.Priority < 4 && IsInQuietHours(preferences))
            {
                LogDelivery(message.Id, NotificationChannel.Email, DeliveryStatus.Filtered, ""Quiet hours active"");
                Console.WriteLine(""Notification filtered due to quiet hours"");
                return;
            }
            
            // Check minimum priority
            if (message.Priority < preferences.MinimumPriority)
            {
                LogDelivery(message.Id, NotificationChannel.Email, DeliveryStatus.Filtered, ""Below minimum priority"");
                Console.WriteLine($""Notification filtered: priority {message.Priority} < {preferences.MinimumPriority}"");
                return;
            }
            
            // Get enabled channels for this notification type
            var enabledChannels = GetEnabledChannels(preferences, message.Type);
            
            if (!enabledChannels.Any())
            {
                Console.WriteLine($""No enabled channels for {message.Type}"");
                return;
            }
            
            // Notify relevant observers
            var results = new List<DeliveryResult>();
            foreach (var observer in _observers)
            {
                if (enabledChannels.Contains(observer.Channel) && observer.CanHandle(message))
                {
                    try
                    {
                        var result = observer.HandleNotification(message);
                        results.Add(result);
                        LogDelivery(result);
                    }
                    catch (Exception ex)
                    {
                        var errorResult = new DeliveryResult
                        {
                            MessageId = message.Id,
                            Channel = observer.Channel,
                            Status = DeliveryStatus.Failed,
                            StatusMessage = ex.Message,
                            DeliveredAt = DateTime.Now
                        };
                        results.Add(errorResult);
                        LogDelivery(errorResult);
                    }
                }
            }
            
            // Summary
            Console.WriteLine($""\\nDelivery Summary: {results.Count(r => r.Status == DeliveryStatus.Delivered)} successful, {results.Count(r => r.Status == DeliveryStatus.Failed)} failed"");
        }
        
        private UserPreferences GetUserPreferences(string userId)
        {
            return _userPreferences.GetValueOrDefault(userId, new UserPreferences
            {
                UserId = userId,
                EnabledChannels = new Dictionary<NotificationType, List<NotificationChannel>>
                {
                    [NotificationType.OrderConfirmation] = new List<NotificationChannel> { NotificationChannel.Email },
                    [NotificationType.SecurityAlert] = new List<NotificationChannel> { NotificationChannel.Email, NotificationChannel.SMS }
                }
            });
        }
        
        private bool IsInQuietHours(UserPreferences preferences)
        {
            var now = DateTime.Now.TimeOfDay;
            
            // Handle overnight quiet hours (e.g., 10 PM to 8 AM)
            if (preferences.QuietHoursStart > preferences.QuietHoursEnd)
            {
                return now >= preferences.QuietHoursStart || now <= preferences.QuietHoursEnd;
            }
            
            // Handle same-day quiet hours (e.g., 1 PM to 3 PM)
            return now >= preferences.QuietHoursStart && now <= preferences.QuietHoursEnd;
        }
        
        private List<NotificationChannel> GetEnabledChannels(UserPreferences preferences, NotificationType type)
        {
            return preferences.EnabledChannels.GetValueOrDefault(type, new List<NotificationChannel>());
        }
        
        private void LogDelivery(string messageId, NotificationChannel channel, DeliveryStatus status, string message)
        {
            var result = new DeliveryResult
            {
                MessageId = messageId,
                Channel = channel,
                Status = status,
                StatusMessage = message,
                DeliveredAt = DateTime.Now
            };
            LogDelivery(result);
        }
        
        private void LogDelivery(DeliveryResult result)
        {
            _deliveryHistory.Add(result);
            Console.WriteLine($""  {result.Channel}: {result.Status} - {result.StatusMessage}"");
        }
        
        public List<DeliveryResult> GetDeliveryHistory(string messageId = null)
        {
            return string.IsNullOrEmpty(messageId) 
                ? new List<DeliveryResult>(_deliveryHistory)
                : _deliveryHistory.Where(h => h.MessageId == messageId).ToList();
        }
    }
    
    #endregion
    
    #region Concrete Observers (Notification Handlers)
    
    /// <summary>
    /// Email notification handler
    /// </summary>
    public class EmailNotificationHandler : INotificationObserver
    {
        public NotificationChannel Channel => NotificationChannel.Email;
        
        public DeliveryResult HandleNotification(NotificationMessage message)
        {
            // Simulate email sending
            Console.WriteLine($""  📧 Sending email to user {message.UserId}"");
            Console.WriteLine($""     Subject: {message.Title}"");
            
            bool success = SimulateDelivery(0.9); // 90% success rate
            
            return new DeliveryResult
            {
                MessageId = message.Id,
                Channel = Channel,
                Status = success ? DeliveryStatus.Delivered : DeliveryStatus.Failed,
                StatusMessage = success ? ""Email sent successfully"" : ""SMTP server error"",
                DeliveredAt = DateTime.Now
            };
        }
        
        public bool CanHandle(NotificationMessage message)
        {
            // Email can handle all message types
            return true;
        }
        
        private bool SimulateDelivery(double successRate)
        {
            System.Threading.Thread.Sleep(500); // Simulate processing time
            return new Random().NextDouble() < successRate;
        }
    }
    
    /// <summary>
    /// SMS notification handler
    /// </summary>
    public class SMSNotificationHandler : INotificationObserver
    {
        public NotificationChannel Channel => NotificationChannel.SMS;
        
        public DeliveryResult HandleNotification(NotificationMessage message)
        {
            // SMS has character limits
            string truncatedContent = message.Content.Length > 160 
                ? message.Content.Substring(0, 157) + ""..."" 
                : message.Content;
            
            Console.WriteLine($""  📱 Sending SMS to user {message.UserId}"");
            Console.WriteLine($""     Message: {truncatedContent}"");
            
            bool success = SimulateDelivery(0.85); // 85% success rate
            
            return new DeliveryResult
            {
                MessageId = message.Id,
                Channel = Channel,
                Status = success ? DeliveryStatus.Delivered : DeliveryStatus.Failed,
                StatusMessage = success ? ""SMS sent successfully"" : ""Network error"",
                DeliveredAt = DateTime.Now
            };
        }
        
        public bool CanHandle(NotificationMessage message)
        {
            // SMS works best for short, urgent messages
            return message.Priority >= 2 && message.Content.Length <= 500;
        }
        
        private bool SimulateDelivery(double successRate)
        {
            System.Threading.Thread.Sleep(300);
            return new Random().NextDouble() < successRate;
        }
    }
    
    /// <summary>
    /// Push notification handler
    /// </summary>
    public class PushNotificationHandler : INotificationObserver
    {
        public NotificationChannel Channel => NotificationChannel.PushNotification;
        
        public DeliveryResult HandleNotification(NotificationMessage message)
        {
            Console.WriteLine($""  🔔 Sending push notification to user {message.UserId}"");
            Console.WriteLine($""     Title: {message.Title}"");
            
            bool success = SimulateDelivery(0.75); // 75% success rate (device might be offline)
            
            return new DeliveryResult
            {
                MessageId = message.Id,
                Channel = Channel,
                Status = success ? DeliveryStatus.Delivered : DeliveryStatus.Failed,
                StatusMessage = success ? ""Push notification delivered"" : ""Device offline"",
                DeliveredAt = DateTime.Now
            };
        }
        
        public bool CanHandle(NotificationMessage message)
        {
            // Push notifications work for all types but better for time-sensitive ones
            return true;
        }
        
        private bool SimulateDelivery(double successRate)
        {
            System.Threading.Thread.Sleep(200);
            return new Random().NextDouble() < successRate;
        }
    }
    
    /// <summary>
    /// In-app notification handler
    /// </summary>
    public class InAppNotificationHandler : INotificationObserver
    {
        public NotificationChannel Channel => NotificationChannel.InApp;
        
        public DeliveryResult HandleNotification(NotificationMessage message)
        {
            Console.WriteLine($""  📋 Creating in-app notification for user {message.UserId}"");
            Console.WriteLine($""     Type: {message.Type}"");
            
            // In-app notifications are always ""delivered"" (stored)
            bool success = SimulateDelivery(0.99); // 99% success rate
            
            return new DeliveryResult
            {
                MessageId = message.Id,
                Channel = Channel,
                Status = success ? DeliveryStatus.Delivered : DeliveryStatus.Failed,
                StatusMessage = success ? ""In-app notification created"" : ""Database error"",
                DeliveredAt = DateTime.Now
            };
        }
        
        public bool CanHandle(NotificationMessage message)
        {
            // In-app can handle all message types
            return true;
        }
        
        private bool SimulateDelivery(double successRate)
        {
            System.Threading.Thread.Sleep(100);
            return new Random().NextDouble() < successRate;
        }
    }
    
    #endregion
    
    #region Demo Program
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""=== Advanced Notification System with Observer Pattern ==="");
            
            // Create notification center
            var notificationCenter = new NotificationCenter();
            
            // Register notification handlers (observers)
            notificationCenter.Subscribe(new EmailNotificationHandler());
            notificationCenter.Subscribe(new SMSNotificationHandler());
            notificationCenter.Subscribe(new PushNotificationHandler());
            notificationCenter.Subscribe(new InAppNotificationHandler());
            
            // Set up user preferences
            var preferences = new UserPreferences
            {
                UserId = ""user123"",
                EnabledChannels = new Dictionary<NotificationType, List<NotificationChannel>>
                {
                    [NotificationType.OrderConfirmation] = new List<NotificationChannel> 
                    { 
                        NotificationChannel.Email, 
                        NotificationChannel.InApp 
                    },
                    [NotificationType.SecurityAlert] = new List<NotificationChannel> 
                    { 
                        NotificationChannel.Email, 
                        NotificationChannel.SMS, 
                        NotificationChannel.PushNotification 
                    },
                    [NotificationType.PaymentReceived] = new List<NotificationChannel> 
                    { 
                        NotificationChannel.Email, 
                        NotificationChannel.PushNotification 
                    }
                },
                QuietHoursStart = TimeSpan.FromHours(22), // 10 PM
                QuietHoursEnd = TimeSpan.FromHours(8),    // 8 AM
                MinimumPriority = 2
            };
            
            notificationCenter.SetUserPreferences(preferences);
            
            // Test different types of notifications
            TestOrderConfirmation(notificationCenter);
            TestSecurityAlert(notificationCenter);
            TestPromotionalOffer(notificationCenter);
            TestLowPriorityDuringQuietHours(notificationCenter);
            
            Console.WriteLine(""\\n=== Demo Complete ==="");
        }
        
        private static void TestOrderConfirmation(NotificationCenter center)
        {
            var orderNotification = new NotificationMessage
            {
                Type = NotificationType.OrderConfirmation,
                Title = ""Order Confirmed"",
                Content = ""Your order #12345 has been confirmed and will be shipped within 2 business days."",
                UserId = ""user123"",
                Priority = 3
            };
            
            center.SendNotification(orderNotification);
        }
        
        private static void TestSecurityAlert(NotificationCenter center)
        {
            var securityAlert = new NotificationMessage
            {
                Type = NotificationType.SecurityAlert,
                Title = ""Security Alert"",
                Content = ""New login detected from unknown device. If this wasn't you, please secure your account immediately."",
                UserId = ""user123"",
                Priority = 5 // High priority
            };
            
            center.SendNotification(securityAlert);
        }
        
        private static void TestPromotionalOffer(NotificationCenter center)
        {
            var promoNotification = new NotificationMessage
            {
                Type = NotificationType.PromotionalOffer,
                Title = ""Special Offer"",
                Content = ""Get 20% off your next purchase with code SAVE20. Valid until end of month."",
                UserId = ""user123"",
                Priority = 1 // Low priority
            };
            
            center.SendNotification(promoNotification);
        }
        
        private static void TestLowPriorityDuringQuietHours(NotificationCenter center)
        {
            // This would typically be filtered during actual quiet hours
            var quietHourTest = new NotificationMessage
            {
                Type = NotificationType.PromotionalOffer,
                Title = ""Late Night Offer"",
                Content = ""This low-priority notification should be filtered during quiet hours."",
                UserId = ""user123"",
                Priority = 1 // Low priority - would be filtered during quiet hours
            };
            
            center.SendNotification(quietHourTest);
        }
    }
    
    #endregion
}";
        }

        private string GenerateDecoratorStarterCode()
        {
            return @"using System;

namespace DesignPatterns.DecoratorPattern
{
    /// <summary>
    /// Basic Logger System - Ready for Decorator Pattern Extension
    /// 
    /// Current system has a simple file logger, but we need to add features like:
    /// - Encryption for sensitive logs
    /// - Compression for large log files
    /// - Timestamp formatting
    /// - Level filtering
    /// - Multiple output formats
    /// 
    /// YOUR TASK: Extend this using the Decorator Pattern without modifying existing code
    /// </summary>
    
    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
        Critical = 5
    }
    
    public class LogEntry
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public Exception Exception { get; set; }
    }
    
    /// <summary>
    /// Basic file logger implementation
    /// </summary>
    public class FileLogger
    {
        private readonly string _filePath;
        
        public FileLogger(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }
        
        public virtual void Log(LogEntry entry)
        {
            string logMessage = $""{entry.Timestamp:yyyy-MM-dd HH:mm:ss} [{entry.Level}] {entry.Message}"";
            
            // Simulate writing to file
            Console.WriteLine($""Writing to {_filePath}: {logMessage}"");
            // File.AppendAllText(_filePath, logMessage + Environment.NewLine);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new FileLogger(""app.log"");
            
            logger.Log(new LogEntry
            {
                Level = LogLevel.Info,
                Message = ""Application started"",
                Category = ""Startup""
            });
            
            logger.Log(new LogEntry
            {
                Level = LogLevel.Error,
                Message = ""Database connection failed"",
                Category = ""Database"",
                Exception = new Exception(""Connection timeout"")
            });
            
            // TODO: Implement decorators to add:
            // - Encryption: EncryptedLogger
            // - Compression: CompressedLogger
            // - Formatting: TimestampFormatterLogger
            // - Filtering: LevelFilterLogger
            // - Multiple outputs: ConsoleLogger, EmailLogger
        }
    }
}";
        }

        private string GenerateDecoratorSolutionCode()
        {
            return @"using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace DesignPatterns.DecoratorPattern
{
    /// <summary>
    /// EXTENDED LOGGER SYSTEM - Using Decorator Pattern
    /// 
    /// BENEFITS:
    /// ✅ Can combine multiple features (encryption + compression + formatting)
    /// ✅ Features can be added/removed at runtime
    /// ✅ Original FileLogger remains unchanged
    /// ✅ Each decorator has a single responsibility
    /// ✅ Flexible composition of logging behaviors
    /// </summary>
    
    #region Core Logging Components
    
    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
        Critical = 5
    }
    
    public class LogEntry
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public string Category { get; set; } = ""General"";
        public Exception Exception { get; set; }
        public string UserContext { get; set; }
        public string CorrelationId { get; set; } = Guid.NewGuid().ToString(""N"")[..8];
    }
    
    /// <summary>
    /// Base logger interface that all loggers and decorators implement
    /// </summary>
    public interface ILogger
    {
        void Log(LogEntry entry);
    }
    
    /// <summary>
    /// Basic file logger implementation (unchanged from original)
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        
        public FileLogger(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }
        
        public virtual void Log(LogEntry entry)
        {
            string logMessage = $""{entry.Timestamp:yyyy-MM-dd HH:mm:ss} [{entry.Level}] {entry.Message}"";
            Console.WriteLine($""📁 FileLogger -> {Path.GetFileName(_filePath)}: {logMessage}"");
        }
    }
    
    /// <summary>
    /// Console logger for immediate output
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void Log(LogEntry entry)
        {
            var color = GetColorForLevel(entry.Level);
            var originalColor = Console.ForegroundColor;
            
            Console.ForegroundColor = color;
            Console.WriteLine($""💻 Console: [{entry.Level}] {entry.Message}"");
            Console.ForegroundColor = originalColor;
        }
        
        private ConsoleColor GetColorForLevel(LogLevel level)
        {
            return level switch
            {
                LogLevel.Debug => ConsoleColor.Gray,
                LogLevel.Info => ConsoleColor.White,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Critical => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };
        }
    }
    
    #endregion
    
    #region Base Decorator
    
    /// <summary>
    /// Base decorator class that implements ILogger and wraps another ILogger
    /// </summary>
    public abstract class LoggerDecorator : ILogger
    {
        protected readonly ILogger _innerLogger;
        
        protected LoggerDecorator(ILogger innerLogger)
        {
            _innerLogger = innerLogger ?? throw new ArgumentNullException(nameof(innerLogger));
        }
        
        public virtual void Log(LogEntry entry)
        {
            _innerLogger.Log(entry);
        }
    }
    
    #endregion
    
    #region Concrete Decorators
    
    /// <summary>
    /// Decorator that encrypts log messages before passing to inner logger
    /// </summary>
    public class EncryptedLoggerDecorator : LoggerDecorator
    {
        private readonly string _encryptionKey;
        
        public EncryptedLoggerDecorator(ILogger innerLogger, string encryptionKey = ""DefaultKey123!"") 
            : base(innerLogger)
        {
            _encryptionKey = encryptionKey;
        }
        
        public override void Log(LogEntry entry)
        {
            Console.WriteLine(""🔐 EncryptedLogger: Encrypting sensitive log data..."");
            
            // Create encrypted copy of log entry
            var encryptedEntry = new LogEntry
            {
                Timestamp = entry.Timestamp,
                Level = entry.Level,
                Message = EncryptString(entry.Message),
                Category = entry.Category,
                Exception = entry.Exception,
                UserContext = entry.UserContext != null ? EncryptString(entry.UserContext) : null,
                CorrelationId = entry.CorrelationId
            };
            
            base.Log(encryptedEntry);
        }
        
        private string EncryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;
            
            // Simple base64 encoding for demonstration (use proper encryption in production)
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            string encrypted = Convert.ToBase64String(bytes);
            return $""ENCRYPTED[{encrypted}]"";
        }
    }
    
    /// <summary>
    /// Decorator that compresses log messages to save space
    /// </summary>
    public class CompressedLoggerDecorator : LoggerDecorator
    {
        public CompressedLoggerDecorator(ILogger innerLogger) : base(innerLogger) { }
        
        public override void Log(LogEntry entry)
        {
            Console.WriteLine(""🗜️ CompressedLogger: Compressing log data..."");
            
            // Create compressed copy of log entry
            var compressedEntry = new LogEntry
            {
                Timestamp = entry.Timestamp,
                Level = entry.Level,
                Message = CompressString(entry.Message),
                Category = entry.Category,
                Exception = entry.Exception,
                UserContext = entry.UserContext,
                CorrelationId = entry.CorrelationId
            };
            
            base.Log(compressedEntry);
        }
        
        private string CompressString(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            
            // Simple compression simulation (use proper compression in production)
            if (text.Length > 50)
            {
                return $""COMPRESSED[{text.Length} chars -> {text.Length / 2} chars]"";
            }
            
            return text;
        }
    }
    
    /// <summary>
    /// Decorator that adds enhanced timestamp and correlation information
    /// </summary>
    public class TimestampFormatterDecorator : LoggerDecorator
    {
        private readonly string _timestampFormat;
        private readonly bool _includeMilliseconds;
        private readonly bool _includeCorrelationId;
        
        public TimestampFormatterDecorator(ILogger innerLogger, 
            string timestampFormat = ""yyyy-MM-dd HH:mm:ss.fff"",
            bool includeMilliseconds = true,
            bool includeCorrelationId = true) 
            : base(innerLogger)
        {
            _timestampFormat = timestampFormat;
            _includeMilliseconds = includeMilliseconds;
            _includeCorrelationId = includeCorrelationId;
        }
        
        public override void Log(LogEntry entry)
        {
            Console.WriteLine(""🕐 TimestampFormatter: Enhancing timestamp formatting..."");
            
            var formattedEntry = new LogEntry
            {
                Timestamp = entry.Timestamp,
                Level = entry.Level,
                Category = entry.Category,
                Exception = entry.Exception,
                UserContext = entry.UserContext,
                CorrelationId = entry.CorrelationId
            };
            
            // Enhanced message with better formatting
            var messageBuilder = new StringBuilder();
            
            if (_includeCorrelationId)
            {
                messageBuilder.Append($""[{entry.CorrelationId}] "");
            }
            
            if (!string.IsNullOrEmpty(entry.Category))
            {
                messageBuilder.Append($""[{entry.Category}] "");
            }
            
            messageBuilder.Append(entry.Message);
            
            if (entry.Exception != null)
            {
                messageBuilder.Append($"" | Exception: {entry.Exception.Message}"");
            }
            
            formattedEntry.Message = messageBuilder.ToString();
            
            base.Log(formattedEntry);
        }
    }
    
    /// <summary>
    /// Decorator that filters logs based on minimum level
    /// </summary>
    public class LevelFilterDecorator : LoggerDecorator
    {
        private readonly LogLevel _minimumLevel;
        
        public LevelFilterDecorator(ILogger innerLogger, LogLevel minimumLevel) 
            : base(innerLogger)
        {
            _minimumLevel = minimumLevel;
        }
        
        public override void Log(LogEntry entry)
        {
            if (entry.Level >= _minimumLevel)
            {
                Console.WriteLine($""🔍 LevelFilter: Log level {entry.Level} meets minimum {_minimumLevel}"");
                base.Log(entry);
            }
            else
            {
                Console.WriteLine($""🔍 LevelFilter: Filtered out {entry.Level} log (below {_minimumLevel})"");
            }
        }
    }
    
    /// <summary>
    /// Decorator that adds buffering and batch logging
    /// </summary>
    public class BufferedLoggerDecorator : LoggerDecorator
    {
        private readonly System.Collections.Generic.List<LogEntry> _buffer = new();
        private readonly int _bufferSize;
        private readonly object _lock = new object();
        
        public BufferedLoggerDecorator(ILogger innerLogger, int bufferSize = 5) 
            : base(innerLogger)
        {
            _bufferSize = bufferSize;
        }
        
        public override void Log(LogEntry entry)
        {
            lock (_lock)
            {
                _buffer.Add(entry);
                Console.WriteLine($""📦 BufferedLogger: Added to buffer ({_buffer.Count}/{_bufferSize})"");
                
                if (_buffer.Count >= _bufferSize)
                {
                    FlushBuffer();
                }
            }
        }
        
        public void FlushBuffer()
        {
            lock (_lock)
            {
                if (_buffer.Count > 0)
                {
                    Console.WriteLine($""📦 BufferedLogger: Flushing {_buffer.Count} log entries"");
                    foreach (var entry in _buffer)
                    {
                        base.Log(entry);
                    }
                    _buffer.Clear();
                }
            }
        }
    }
    
    /// <summary>
    /// Decorator that sends critical logs via email (simulated)
    /// </summary>
    public class EmailAlertDecorator : LoggerDecorator
    {
        private readonly LogLevel _alertLevel;
        private readonly string _emailAddress;
        
        public EmailAlertDecorator(ILogger innerLogger, LogLevel alertLevel = LogLevel.Error, 
            string emailAddress = ""admin@company.com"") 
            : base(innerLogger)
        {
            _alertLevel = alertLevel;
            _emailAddress = emailAddress;
        }
        
        public override void Log(LogEntry entry)
        {
            base.Log(entry);
            
            if (entry.Level >= _alertLevel)
            {
                SendEmailAlert(entry);
            }
        }
        
        private void SendEmailAlert(LogEntry entry)
        {
            Console.WriteLine($""📧 EmailAlert: Sending critical alert to {_emailAddress}"");
            Console.WriteLine($""   Subject: [{entry.Level}] System Alert - {entry.Category}"");
            Console.WriteLine($""   Message: {entry.Message}"");
        }
    }
    
    #endregion
    
    #region Logger Builder (Bonus)
    
    /// <summary>
    /// Fluent builder for creating decorated loggers
    /// </summary>
    public class LoggerBuilder
    {
        private ILogger _logger;
        
        public LoggerBuilder(ILogger baseLogger)
        {
            _logger = baseLogger ?? throw new ArgumentNullException(nameof(baseLogger));
        }
        
        public LoggerBuilder WithEncryption(string key = ""DefaultKey123!"")
        {
            _logger = new EncryptedLoggerDecorator(_logger, key);
            return this;
        }
        
        public LoggerBuilder WithCompression()
        {
            _logger = new CompressedLoggerDecorator(_logger);
            return this;
        }
        
        public LoggerBuilder WithTimestampFormatting(string format = ""yyyy-MM-dd HH:mm:ss.fff"", 
            bool includeCorrelationId = true)
        {
            _logger = new TimestampFormatterDecorator(_logger, format, true, includeCorrelationId);
            return this;
        }
        
        public LoggerBuilder WithLevelFilter(LogLevel minimumLevel)
        {
            _logger = new LevelFilterDecorator(_logger, minimumLevel);
            return this;
        }
        
        public LoggerBuilder WithBuffering(int bufferSize = 5)
        {
            _logger = new BufferedLoggerDecorator(_logger, bufferSize);
            return this;
        }
        
        public LoggerBuilder WithEmailAlerts(LogLevel alertLevel = LogLevel.Error, 
            string emailAddress = ""admin@company.com"")
        {
            _logger = new EmailAlertDecorator(_logger, alertLevel, emailAddress);
            return this;
        }
        
        public ILogger Build()
        {
            return _logger;
        }
    }
    
    #endregion
    
    #region Demo Program
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""=== Logger System with Decorator Pattern ==="");
            
            // Test 1: Basic file logger
            Console.WriteLine(""\\n--- Test 1: Basic File Logger ---"");
            var basicLogger = new FileLogger(""basic.log"");
            TestLogger(basicLogger, ""Basic"");
            
            // Test 2: Enhanced logger with multiple decorators
            Console.WriteLine(""\\n--- Test 2: Enhanced Logger with Multiple Decorators ---"");
            var enhancedLogger = new LoggerBuilder(new FileLogger(""enhanced.log""))
                .WithLevelFilter(LogLevel.Info)
                .WithTimestampFormatting()
                .WithEncryption()
                .WithEmailAlerts(LogLevel.Error)
                .Build();
            
            TestLogger(enhancedLogger, ""Enhanced"");
            
            // Test 3: Development logger (console + file)
            Console.WriteLine(""\\n--- Test 3: Development Logger ---"");
            var devLogger = new LoggerBuilder(new ConsoleLogger())
                .WithTimestampFormatting()
                .WithLevelFilter(LogLevel.Debug)
                .Build();
            
            TestLogger(devLogger, ""Development"");
            
            // Test 4: Production logger (encrypted, compressed, buffered)
            Console.WriteLine(""\\n--- Test 4: Production Logger ---"");
            var prodLogger = new LoggerBuilder(new FileLogger(""production.log""))
                .WithLevelFilter(LogLevel.Warning)
                .WithEncryption(""ProductionKey2024!"")
                .WithCompression()
                .WithTimestampFormatting()
                .WithBuffering(3)
                .WithEmailAlerts(LogLevel.Critical)
                .Build();
            
            TestLogger(prodLogger, ""Production"");
            
            // Flush any remaining buffered logs
            if (prodLogger is BufferedLoggerDecorator buffered)
            {
                buffered.FlushBuffer();
            }
            
            Console.WriteLine(""\\n=== Demo Complete ==="");
        }
        
        private static void TestLogger(ILogger logger, string loggerName)
        {
            Console.WriteLine($""Testing {loggerName} Logger:"");
            
            logger.Log(new LogEntry
            {
                Level = LogLevel.Debug,
                Message = ""Debug message for troubleshooting"",
                Category = ""Debug""
            });
            
            logger.Log(new LogEntry
            {
                Level = LogLevel.Info,
                Message = ""Application started successfully"",
                Category = ""Startup"",
                UserContext = ""System""
            });
            
            logger.Log(new LogEntry
            {
                Level = LogLevel.Warning,
                Message = ""Database connection is slow"",
                Category = ""Performance""
            });
            
            logger.Log(new LogEntry
            {
                Level = LogLevel.Error,
                Message = ""Failed to process user request"",
                Category = ""API"",
                Exception = new Exception(""Validation failed""),
                UserContext = ""user123""
            });
            
            logger.Log(new LogEntry
            {
                Level = LogLevel.Critical,
                Message = ""System is running out of memory"",
                Category = ""System""
            });
        }
    }
    
    #endregion
}";
        }

        #endregion
    }
}