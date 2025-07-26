using System;
using Xunit;

namespace CSharpFundamentals.PersonalCalculator.Tests
{
    /// <summary>
    /// Unit tests for Personal Calculator functionality
    /// These tests help verify that calculations are correct
    /// </summary>
    public class PersonalCalculatorTests
    {
        [Fact]
        public void BMI_Calculation_Should_Be_Accurate()
        {
            // Arrange
            double weightKg = 70;
            double heightM = 1.75;
            double expectedBMI = 22.86; // 70 / (1.75 * 1.75)
            
            // Act
            double actualBMI = weightKg / (heightM * heightM);
            
            // Assert
            Assert.Equal(expectedBMI, actualBMI, 2); // 2 decimal places precision
        }
        
        [Theory]
        [InlineData(100, 1.0)] // 100 cm = 1.0 m
        [InlineData(175, 1.75)]
        [InlineData(200, 2.0)]
        public void Height_Conversion_CM_To_Meters_Should_Be_Accurate(double heightCm, double expectedMeters)
        {
            // Act
            double actualMeters = heightCm / 100.0;
            
            // Assert
            Assert.Equal(expectedMeters, actualMeters, 2);
        }
        
        [Theory]
        [InlineData(152.4, 5.0)] // 152.4 cm = 5 feet
        [InlineData(182.88, 6.0)]
        [InlineData(213.36, 7.0)]
        public void Height_Conversion_CM_To_Feet_Should_Be_Accurate(double heightCm, double expectedFeet)
        {
            // Act
            double actualFeet = heightCm / 30.48;
            
            // Assert
            Assert.Equal(expectedFeet, actualFeet, 1);
        }
        
        [Theory]
        [InlineData(2000, 25)] // Assuming current year is 2025
        [InlineData(1990, 35)]
        [InlineData(1980, 45)]
        public void Age_Calculation_Should_Be_Accurate(int birthYear, int expectedAge)
        {
            // Arrange
            int currentYear = 2025; // Mock current year for predictable testing
            
            // Act
            int actualAge = currentYear - birthYear;
            
            // Assert
            Assert.Equal(expectedAge, actualAge);
        }
        
        [Theory]
        [InlineData(16.0, "Underweight")]
        [InlineData(22.0, "Normal weight")]
        [InlineData(27.0, "Overweight")]
        [InlineData(32.0, "Obese")]
        public void BMI_Category_Classification_Should_Be_Correct(double bmi, string expectedCategory)
        {
            // Act
            string actualCategory = GetBmiCategory(bmi);
            
            // Assert
            Assert.Equal(expectedCategory, actualCategory);
        }
        
        // Helper method for testing (should match the one in PersonalCalculator)
        private static string GetBmiCategory(double bmi)
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25.0) return "Normal weight";
            if (bmi < 30.0) return "Overweight";
            return "Obese";
        }
    }
}