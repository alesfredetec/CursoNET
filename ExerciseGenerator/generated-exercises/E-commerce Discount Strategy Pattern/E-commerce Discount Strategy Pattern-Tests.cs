using System;
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
                return $"Test strategy with ${_discount} discount";
            }
        }
        
        #endregion
    }
}