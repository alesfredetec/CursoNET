using System;

namespace CSharpFundamentals.DebugMe
{
    /// <summary>
    /// FIXED CODE - All Bugs Corrected
    /// 
    /// This corrected version demonstrates:
    /// - Proper data types for calculations
    /// - Correct floating-point division
    /// - Handling potential overflow conditions
    /// - Proper string formatting
    /// </summary>
    class StatisticsCalculator
    {
        static void Main(string[] args)
        {
            // Sample dataset
            int[] numbers = { 10, 20, 30, 40, 50 };
            
            // FIX 1: Use double for accurate average calculation
            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }
            double average = (double)sum / numbers.Length; // Cast to double for floating-point division
            
            // FIX 2: Use int (or long) for maximum value - sufficient for most cases
            int maxValue = int.MinValue; // Start with minimum possible value
            foreach (int number in numbers)
            {
                if (number > maxValue)
                {
                    maxValue = number;
                }
            }
            
            // FIX 3: Use double for percentage calculation
            int countAboveAverage = 0;
            foreach (int number in numbers)
            {
                if (number > average)
                {
                    countAboveAverage++;
                }
            }
            double percentageAboveAverage = ((double)countAboveAverage / numbers.Length) * 100;
            
            // FIX 4: Use string interpolation and proper formatting
            Console.WriteLine($"Average: {average:F2}"); // Show 2 decimal places
            Console.WriteLine($"Maximum: {maxValue}");
            Console.WriteLine($"Percentage above average: {percentageAboveAverage:F2}%");
            
            // FIX 5: Use long to prevent overflow, or check for overflow
            long largeNumber1 = 2000000000L;
            long largeNumber2 = 2000000000L;
            long product = largeNumber1 * largeNumber2; // Use long to handle larger values
            Console.WriteLine($"Large calculation result: {product:N0}"); // Format with thousands separators
            
            // Alternative: Check for overflow before calculation
            try
            {
                checked
                {
                    int intResult = (int)largeNumber1 * (int)largeNumber2;
                    Console.WriteLine($"Checked calculation: {intResult}");
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("Calculation would overflow - using safe method instead");
            }
        }
    }
}