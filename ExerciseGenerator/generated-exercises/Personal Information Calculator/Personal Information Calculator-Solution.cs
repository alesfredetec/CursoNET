using System;

namespace CSharpFundamentals.PersonalCalculator
{
    /// <summary>
    /// Personal Information Calculator - Complete Solution
    /// 
    /// This program demonstrates proper use of C# data types, variables,
    /// arithmetic operations, and string formatting.
    /// </summary>
    public class PersonalCalculator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Personal Information Calculator ===");
            
            // Declare variables with appropriate data types
            string name;
            int birthYear;
            double heightCm;
            double weightKg = 0;
            bool isStudent;
            bool hasWeight = false;
            
            // Collect input from user
            Console.Write("Enter your name: ");
            name = Console.ReadLine() ?? "Unknown";
            
            Console.Write("Enter your birth year: ");
            if (!int.TryParse(Console.ReadLine(), out birthYear) || birthYear < 1900 || birthYear > DateTime.Now.Year)
            {
                Console.WriteLine("Invalid birth year. Using 2000 as default.");
                birthYear = 2000;
            }
            
            Console.Write("Enter your height in centimeters: ");
            if (!double.TryParse(Console.ReadLine(), out heightCm) || heightCm <= 0)
            {
                Console.WriteLine("Invalid height. Using 170 cm as default.");
                heightCm = 170;
            }
            
            Console.Write("Enter your weight in kilograms (optional, press Enter to skip): ");
            string weightInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(weightInput) && double.TryParse(weightInput, out weightKg) && weightKg > 0)
            {
                hasWeight = true;
            }
            
            Console.Write("Are you currently a student? (y/n): ");
            string studentInput = Console.ReadLine()?.ToLower() ?? "n";
            isStudent = studentInput == "y" || studentInput == "yes";
            
            // Perform calculations
            int currentYear = DateTime.Now.Year;
            int age = currentYear - birthYear;
            
            double heightM = heightCm / 100.0;  // Convert cm to meters
            double heightFt = heightCm / 30.48; // Convert cm to feet
            
            double bmi = 0;
            string bmiCategory = "Not calculated";
            if (hasWeight && heightM > 0)
            {
                bmi = weightKg / (heightM * heightM);
                bmiCategory = GetBmiCategory(bmi);
            }
            
            // Display formatted results
            Console.WriteLine("\\n=== Your Information ===");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Age: {age} years old");
            Console.WriteLine($"Height: {heightCm:F1} cm ({heightM:F2} m, {heightFt:F1} ft)");
            Console.WriteLine($"Student Status: {(isStudent ? "Yes" : "No")}");
            
            if (hasWeight)
            {
                Console.WriteLine($"Weight: {weightKg:F1} kg");
                Console.WriteLine($"BMI: {bmi:F2} ({bmiCategory})");
            }
            else
            {
                Console.WriteLine("Weight: Not provided");
                Console.WriteLine("BMI: Not calculated");
            }
            
            Console.WriteLine($"\\nCalculated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
        
        /// <summary>
        /// Determines BMI category based on BMI value
        /// </summary>
        /// <param name="bmi">BMI value</param>
        /// <returns>BMI category as string</returns>
        private static string GetBmiCategory(double bmi)
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25.0) return "Normal weight";
            if (bmi < 30.0) return "Overweight";
            return "Obese";
        }
    }
}