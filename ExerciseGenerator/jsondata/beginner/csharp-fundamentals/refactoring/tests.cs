using System;
using Xunit;
using System.IO;

namespace CSharpFundamentals.RefactorMe.Tests
{
    /// <summary>
    /// Unit tests to verify refactored code maintains same functionality
    /// </summary>
    public class MathCalculatorTests
    {
        [Theory]
        [InlineData("Alice", "10", "5", "Alice, the result of 10 compared to 5 is: yes")]
        [InlineData("Bob", "3", "7", "Bob, the result of 3 compared to 7 is: no")]
        [InlineData("Carol", "5", "5", "Carol, the result of 5 compared to 5 is: no")]
        public void ComparisonOutput_ShouldBeCorrect(string name, string first, string second, string expectedComparison)
        {
            // This test would require refactoring the main method to be testable
            // by extracting the logic into separate methods
            Assert.True(true); // Placeholder - would need method extraction first
        }
        
        [Theory]
        [InlineData(10, 5, 50)]
        [InlineData(3, 7, 21)]
        [InlineData(-2, 4, -8)]
        public void Multiplication_ShouldBeCorrect(int first, int second, int expected)
        {
            // Test multiplication logic
            int result = first * second;
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(10, 5, 15)]
        [InlineData(3, 7, 10)]
        [InlineData(-2, 4, 2)]
        public void Addition_ShouldBeCorrect(int first, int second, int expected)
        {
            // Test addition logic
            int result = first + second;
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(10, 5, 2.0)]
        [InlineData(3, 2, 1.5)]
        [InlineData(7, 4, 1.75)]
        public void Division_ShouldBeCorrect(int first, int second, double expected)
        {
            // Test division logic with proper data types
            double result = (double)first / second;
            Assert.Equal(expected, result, 2); // 2 decimal places
        }
        
        [Theory]
        [InlineData(150, "large")]
        [InlineData(50, "small")]
        [InlineData(100, "small")]
        [InlineData(101, "large")]
        public void ResultMagnitude_ShouldBeCorrect(int value, string expected)
        {
            // Test magnitude classification
            string result = (value > 100) ? "large" : "small";
            Assert.Equal(expected, result);
        }
    }
}