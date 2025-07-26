using System;

namespace CSharpFundamentals.PersonalCalculator
{
    /// <summary>
    /// Personal Information Calculator - Starter Code
    /// 
    /// TODO: Complete the implementation to collect personal information
    /// and perform calculations with different data types.
    /// </summary>
    public class PersonalCalculator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Personal Information Calculator ===");
            
            // TODO: Declare variables for person's information
            // Hint: You'll need variables for name (string), birth year (int), 
            // height in cm (double), is student (bool), and weight (double)
            
            // TODO: Collect input from user
            Console.Write("Enter your name: ");
            
            Console.Write("Enter your birth year: ");
            
            Console.Write("Enter your height in centimeters: ");
            
            Console.Write("Enter your weight in kilograms (optional, press Enter to skip): ");
            
            Console.Write("Are you currently a student? (y/n): ");
            
            // TODO: Perform calculations
            // 1. Calculate age
            // 2. Convert height to meters and feet
            // 3. Calculate BMI if weight is provided
            
            // TODO: Display formatted results
            Console.WriteLine("\\n=== Your Information ===");
            
        }
        
        // TODO: Add helper methods if needed
    }
}