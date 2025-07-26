using System;

namespace CSharpFundamentals.DebugMe
{
    /// <summary>
    /// BUGGY CODE - Contains Several Data Type and Conversion Errors
    /// 
    /// This program is supposed to calculate statistics for a dataset,
    /// but it contains several bugs. Find and fix them all!
    /// 
    /// Expected output (with sample data 10, 20, 30, 40, 50):
    /// - Average should be 30.00
    /// - Maximum should be 50
    /// - Percentage above average should be 40.00%
    /// </summary>
    class StatisticsCalculator
    {
        static void Main(string[] args)
        {
            // Sample dataset
            int[] numbers = { 10, 20, 30, 40, 50 };
            
            // BUG 1: Wrong data type for average calculation
            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }
            int average = sum / numbers.Length; // This should be a floating-point division!
            
            // BUG 2: Wrong data type for maximum value
            short maxValue = 0; // short might not be big enough!
            foreach (int number in numbers)
            {
                if (number > maxValue)
                {
                    maxValue = number;
                }
            }
            
            // BUG 3: Integer division instead of floating-point
            int countAboveAverage = 0;
            foreach (int number in numbers)
            {
                if (number > average)
                {
                    countAboveAverage++;
                }
            }
            int percentageAboveAverage = (countAboveAverage / numbers.Length) * 100; // Wrong!
            
            // BUG 4: Incorrect string concatenation instead of interpolation
            Console.WriteLine("Average: " + average); // Should show decimal places
            Console.WriteLine("Maximum: " + maxValue);
            Console.WriteLine("Percentage above average: " + percentageAboveAverage + "%");
            
            // BUG 5: Potential overflow in large calculations
            int largeNumber1 = 2000000000;
            int largeNumber2 = 2000000000;
            int product = largeNumber1 * largeNumber2; // Overflow!
            Console.WriteLine("Large calculation result: " + product);
        }
    }
}