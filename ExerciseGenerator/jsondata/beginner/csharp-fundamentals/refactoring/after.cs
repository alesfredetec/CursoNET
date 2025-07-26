using System;

namespace CSharpFundamentals.RefactorMe
{
    /// <summary>
    /// REFACTORED CODE - Following C# Best Practices
    /// 
    /// This refactored version demonstrates:
    /// - Descriptive variable names
    /// - Appropriate data types
    /// - Proper formatting and indentation
    /// - Meaningful comments
    /// - Following C# naming conventions
    /// </summary>
    class MathCalculatorProgram
    {
        static void Main(string[] args)
        {
            // Get user's name for personalized output
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine() ?? "User";
            
            // Get two numbers for mathematical operations
            Console.Write("Enter the first number: ");
            int firstNumber = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Enter the second number: ");
            int secondNumber = int.Parse(Console.ReadLine() ?? "0");
            
            // Perform comparisons and calculations
            string comparisonResult = (firstNumber > secondNumber) ? "yes" : "no";
            int multiplicationResult = firstNumber * secondNumber;
            int additionResult = firstNumber + secondNumber;
            
            // Use double for division to get accurate results
            double divisionResult = (secondNumber != 0) ? (double)firstNumber / secondNumber : 0;
            
            // Display results with proper formatting
            Console.WriteLine($"{userName}, the result of {firstNumber} compared to {secondNumber} is: {comparisonResult}");
            Console.WriteLine($"Multiplication: {multiplicationResult}");
            Console.WriteLine($"Sum: {additionResult}");
            Console.WriteLine($"Division: {divisionResult:F2}"); // Format to 2 decimal places
            
            // Classify the multiplication result
            string resultMagnitude = (multiplicationResult > 100) ? "large" : "small";
            Console.WriteLine($"The multiplication result is: {resultMagnitude}");
        }
    }
}