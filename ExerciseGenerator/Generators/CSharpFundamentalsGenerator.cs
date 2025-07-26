using System;
using System.Collections.Generic;
using System.Text;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Generates exercises for C# fundamentals including variables, data types, operators
    /// </summary>
    public class CSharpFundamentalsGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.CSharpFundamentals;

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            if (!ValidateConfiguration(config))
                throw new ArgumentException("Invalid configuration for C# Fundamentals");

            return config.Type switch
            {
                ExerciseType.Implementation => GenerateImplementationExercise(config),
                ExerciseType.Refactoring => GenerateRefactoringExercise(config),
                ExerciseType.DebugFix => GenerateDebugFixExercise(config),
                ExerciseType.Extension => GenerateExtensionExercise(config),
                _ => GenerateImplementationExercise(config)
            };
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return config.Topic == TopicArea.CSharpFundamentals;
        }

        private Exercise GenerateImplementationExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "Personal Information Calculator",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.CSharpFundamentals,
                Type = ExerciseType.Implementation,
                EstimatedMinutes = 25
            };

            exercise.Description = @"Create a simple program that collects personal information and performs basic calculations with different data types.

This exercise will help you practice:
- Variable declarations and initialization
- Different data types (int, double, string, bool, DateTime)
- Basic arithmetic operations
- String interpolation and formatting
- Type conversions";

            exercise.LearningObjectives.AddRange(new[]
            {
                "Declare and initialize variables of different data types",
                "Perform arithmetic operations with numeric types",
                "Use string interpolation for output formatting",
                "Convert between different numeric types safely",
                "Work with DateTime for age calculations"
            });

            exercise.Prerequisites.AddRange(new[]
            {
                "Basic understanding of what variables are",
                "Familiarity with C# syntax for variable declaration",
                "Understanding of basic arithmetic operations"
            });

            exercise.ProblemStatement = @"You need to create a Personal Information Calculator that:

1. Collects a person's name, birth year, height in centimeters, and whether they are a student
2. Calculates their age based on the current year
3. Converts their height to meters and feet
4. Calculates their BMI if they provide their weight
5. Displays all information in a formatted output

The program should handle different data types correctly and perform accurate calculations.";

            exercise.TechnicalRequirements.AddRange(new[]
            {
                "Use appropriate data types for each piece of information",
                "Perform type conversions where necessary",
                "Use string interpolation for formatted output",
                "Handle potential division by zero for BMI calculation",
                "Use DateTime.Now.Year for current year calculation"
            });

            exercise.SuccessCriteria.AddRange(new[]
            {
                "Program compiles without errors",
                "All variables are declared with appropriate data types",
                "Age calculation is correct",
                "Height conversions are accurate (1m = 100cm, 1ft = 30.48cm)",
                "BMI calculation is correct (weight_kg / (height_m)^2)",
                "Output is well-formatted and readable"
            });

            exercise.StarterCode = GenerateStarterCode();
            exercise.SolutionCode = GenerateSolutionCode();
            exercise.UnitTestCode = GenerateUnitTestCode();

            exercise.ExtensionChallenges.AddRange(new[]
            {
                "Add input validation to ensure positive values for height and weight",
                "Include BMI category classification (underweight, normal, overweight, obese)",
                "Add calculation for ideal weight range based on height",
                "Format the birth year input to accept full dates and calculate exact age",
                "Add support for different measurement systems (metric vs imperial)"
            });

            exercise.CommonPitfalls.AddRange(new[]
            {
                "Don't forget to handle division by zero when calculating BMI",
                "Be careful with integer division - use double for accurate results",
                "Remember that DateTime calculations can be tricky - consider using DateTime.Today for current date",
                "Use meaningful variable names that describe what the data represents",
                "Test your calculations with known values to verify accuracy"
            });

            return exercise;
        }

        private Exercise GenerateRefactoringExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "Refactor Poor Variable Usage",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.CSharpFundamentals,
                Type = ExerciseType.Refactoring,
                EstimatedMinutes = 20
            };

            exercise.Description = @"Refactor a poorly written program that has bad variable naming, inappropriate data types, and poor formatting.

This exercise will help you practice:
- Choosing appropriate variable names
- Selecting correct data types
- Proper code formatting and organization
- Following C# naming conventions";

            exercise.LearningObjectives.AddRange(new[]
            {
                "Identify inappropriate variable names and data types",
                "Apply C# naming conventions consistently",
                "Choose the most appropriate data type for each variable",
                "Improve code readability through better formatting"
            });

            exercise.Prerequisites.AddRange(new[]
            {
                "Understanding of C# data types",
                "Basic knowledge of C# naming conventions",
                "Familiarity with variable declaration syntax"
            });

            exercise.ProblemStatement = @"The following code works but is poorly written with bad variable names, wrong data types, and poor formatting. 
Your task is to refactor it to follow C# best practices while maintaining the same functionality.

Focus on:
- Using descriptive variable names
- Choosing appropriate data types
- Following C# naming conventions (camelCase for local variables, PascalCase for methods)
- Improving code structure and readability";

            exercise.TechnicalRequirements.AddRange(new[]
            {
                "Maintain the exact same functionality",
                "Use descriptive variable names that explain their purpose",
                "Choose the most appropriate data type for each variable",
                "Follow C# naming conventions",
                "Add meaningful comments where helpful"
            });

            exercise.SuccessCriteria.AddRange(new[]
            {
                "Code produces identical output to the original",
                "All variable names are descriptive and follow conventions",
                "Data types are appropriate for the data being stored",
                "Code is well-formatted and readable",
                "Comments explain any complex logic"
            });

            exercise.StarterCode = GenerateRefactoringStarterCode();
            exercise.SolutionCode = GenerateRefactoringSolutionCode();

            exercise.ExtensionChallenges.AddRange(new[]
            {
                "Add input validation for negative values",
                "Extract calculations into separate methods",
                "Add XML documentation comments for the main method",
                "Implement error handling for invalid input"
            });

            return exercise;
        }

        private Exercise GenerateDebugFixExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "Fix Data Type and Conversion Bugs",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.CSharpFundamentals,
                Type = ExerciseType.DebugFix,
                EstimatedMinutes = 15
            };

            exercise.Description = @"Debug and fix a program that has several common data type and conversion errors.

This exercise will help you practice:
- Identifying type conversion issues
- Understanding integer vs floating-point division
- Recognizing overflow and underflow problems
- Debugging compilation and runtime errors";

            exercise.LearningObjectives.AddRange(new[]
            {
                "Identify and fix type conversion errors",
                "Understand the difference between integer and floating-point division",
                "Recognize and handle numeric overflow issues",
                "Debug common compilation errors related to data types"
            });

            exercise.Prerequisites.AddRange(new[]
            {
                "Understanding of C# data types",
                "Basic knowledge of type conversions",
                "Familiarity with arithmetic operations"
            });

            exercise.ProblemStatement = @"The following program is supposed to calculate statistics for a small dataset, but it contains several bugs related to data types and conversions.

Your task is to:
1. Identify all the bugs in the code
2. Fix them while maintaining the intended functionality
3. Ensure the program produces accurate results

The program should calculate the average, find the maximum value, and determine the percentage of values above the average.";

            exercise.TechnicalRequirements.AddRange(new[]
            {
                "Fix all compilation errors",
                "Ensure accurate floating-point calculations",
                "Handle potential division by zero",
                "Use appropriate data types for all calculations"
            });

            exercise.SuccessCriteria.AddRange(new[]
            {
                "Program compiles without errors",
                "Average calculation is accurate to 2 decimal places",
                "Maximum value is correctly identified",
                "Percentage calculation is accurate",
                "No runtime exceptions occur with valid input"
            });

            exercise.StarterCode = GenerateDebugStarterCode();
            exercise.SolutionCode = GenerateDebugSolutionCode();

            return exercise;
        }

        private Exercise GenerateExtensionExercise(ExerciseConfiguration config)
        {
            var exercise = new Exercise
            {
                Title = "Extend Simple Calculator with New Features",
                Level = SkillLevel.Beginner,
                Topic = TopicArea.CSharpFundamentals,
                Type = ExerciseType.Extension,
                EstimatedMinutes = 30
            };

            exercise.Description = @"Extend a basic calculator program with additional mathematical operations and features.

This exercise will help you practice:
- Adding new functionality to existing code
- Working with more complex mathematical operations
- Handling edge cases and error conditions
- Using constants and readonly variables";

            exercise.LearningObjectives.AddRange(new[]
            {
                "Extend existing functionality without breaking current features",
                "Implement more complex mathematical operations",
                "Use constants for mathematical values like PI",
                "Handle edge cases in mathematical calculations"
            });

            exercise.StarterCode = GenerateExtensionStarterCode();
            exercise.SolutionCode = GenerateExtensionSolutionCode();

            return exercise;
        }

        #region Code Generation Methods

        private string GenerateStarterCode()
        {
            return @"using System;

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
            Console.WriteLine(""=== Personal Information Calculator ==="");
            
            // TODO: Declare variables for person's information
            // Hint: You'll need variables for name (string), birth year (int), 
            // height in cm (double), is student (bool), and weight (double)
            
            // TODO: Collect input from user
            Console.Write(""Enter your name: "");
            
            Console.Write(""Enter your birth year: "");
            
            Console.Write(""Enter your height in centimeters: "");
            
            Console.Write(""Enter your weight in kilograms (optional, press Enter to skip): "");
            
            Console.Write(""Are you currently a student? (y/n): "");
            
            // TODO: Perform calculations
            // 1. Calculate age
            // 2. Convert height to meters and feet
            // 3. Calculate BMI if weight is provided
            
            // TODO: Display formatted results
            Console.WriteLine(""\\n=== Your Information ==="");
            
        }
        
        // TODO: Add helper methods if needed
    }
}";
        }

        private string GenerateSolutionCode()
        {
            return @"using System;

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
            Console.WriteLine(""=== Personal Information Calculator ==="");
            
            // Declare variables with appropriate data types
            string name;
            int birthYear;
            double heightCm;
            double weightKg = 0;
            bool isStudent;
            bool hasWeight = false;
            
            // Collect input from user
            Console.Write(""Enter your name: "");
            name = Console.ReadLine() ?? ""Unknown"";
            
            Console.Write(""Enter your birth year: "");
            if (!int.TryParse(Console.ReadLine(), out birthYear) || birthYear < 1900 || birthYear > DateTime.Now.Year)
            {
                Console.WriteLine(""Invalid birth year. Using 2000 as default."");
                birthYear = 2000;
            }
            
            Console.Write(""Enter your height in centimeters: "");
            if (!double.TryParse(Console.ReadLine(), out heightCm) || heightCm <= 0)
            {
                Console.WriteLine(""Invalid height. Using 170 cm as default."");
                heightCm = 170;
            }
            
            Console.Write(""Enter your weight in kilograms (optional, press Enter to skip): "");
            string weightInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(weightInput) && double.TryParse(weightInput, out weightKg) && weightKg > 0)
            {
                hasWeight = true;
            }
            
            Console.Write(""Are you currently a student? (y/n): "");
            string studentInput = Console.ReadLine()?.ToLower() ?? ""n"";
            isStudent = studentInput == ""y"" || studentInput == ""yes"";
            
            // Perform calculations
            int currentYear = DateTime.Now.Year;
            int age = currentYear - birthYear;
            
            double heightM = heightCm / 100.0;  // Convert cm to meters
            double heightFt = heightCm / 30.48; // Convert cm to feet
            
            double bmi = 0;
            string bmiCategory = ""Not calculated"";
            if (hasWeight && heightM > 0)
            {
                bmi = weightKg / (heightM * heightM);
                bmiCategory = GetBmiCategory(bmi);
            }
            
            // Display formatted results
            Console.WriteLine(""\\n=== Your Information ==="");
            Console.WriteLine($""Name: {name}"");
            Console.WriteLine($""Age: {age} years old"");
            Console.WriteLine($""Height: {heightCm:F1} cm ({heightM:F2} m, {heightFt:F1} ft)"");
            Console.WriteLine($""Student Status: {(isStudent ? ""Yes"" : ""No"")}"");
            
            if (hasWeight)
            {
                Console.WriteLine($""Weight: {weightKg:F1} kg"");
                Console.WriteLine($""BMI: {bmi:F2} ({bmiCategory})"");
            }
            else
            {
                Console.WriteLine(""Weight: Not provided"");
                Console.WriteLine(""BMI: Not calculated"");
            }
            
            Console.WriteLine($""\\nCalculated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"");
        }
        
        /// <summary>
        /// Determines BMI category based on BMI value
        /// </summary>
        /// <param name=""bmi"">BMI value</param>
        /// <returns>BMI category as string</returns>
        private static string GetBmiCategory(double bmi)
        {
            if (bmi < 18.5) return ""Underweight"";
            if (bmi < 25.0) return ""Normal weight"";
            if (bmi < 30.0) return ""Overweight"";
            return ""Obese"";
        }
    }
}";
        }

        private string GenerateUnitTestCode()
        {
            return @"using System;
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
        [InlineData(16.0, ""Underweight"")]
        [InlineData(22.0, ""Normal weight"")]
        [InlineData(27.0, ""Overweight"")]
        [InlineData(32.0, ""Obese"")]
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
            if (bmi < 18.5) return ""Underweight"";
            if (bmi < 25.0) return ""Normal weight"";
            if (bmi < 30.0) return ""Overweight"";
            return ""Obese"";
        }
    }
}";
        }

        private string GenerateRefactoringStarterCode()
        {
            return @"using System;

namespace CSharpFundamentals.RefactorMe
{
    /// <summary>
    /// BAD CODE - Needs Refactoring
    /// 
    /// This code works but violates many C# best practices:
    /// - Poor variable naming
    /// - Wrong data types
    /// - Bad formatting
    /// - No meaningful comments
    /// 
    /// YOUR TASK: Refactor this code to follow C# best practices
    /// while maintaining the exact same functionality.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string a=Console.ReadLine();
        int b=int.Parse(Console.ReadLine());
        int c=int.Parse(Console.ReadLine());
        string d=(b>c)?""yes"":""no"";
        int e=b*c;
        int f=b+c;
        int g=b/c;
        Console.WriteLine(a+"" the result of ""+b+"" compared to ""+c+"" is: ""+d);
        Console.WriteLine(""multiplication: ""+e);
        Console.WriteLine(""sum: ""+f);
        Console.WriteLine(""division: ""+g);
        string h=(e>100)?""large"":""small"";
        Console.WriteLine(""The multiplication result is: ""+h);
        }
    }
}";
        }

        private string GenerateRefactoringSolutionCode()
        {
            return @"using System;

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
            Console.Write(""Enter your name: "");
            string userName = Console.ReadLine() ?? ""User"";
            
            // Get two numbers for mathematical operations
            Console.Write(""Enter the first number: "");
            int firstNumber = int.Parse(Console.ReadLine() ?? ""0"");
            
            Console.Write(""Enter the second number: "");
            int secondNumber = int.Parse(Console.ReadLine() ?? ""0"");
            
            // Perform comparisons and calculations
            string comparisonResult = (firstNumber > secondNumber) ? ""yes"" : ""no"";
            int multiplicationResult = firstNumber * secondNumber;
            int additionResult = firstNumber + secondNumber;
            
            // Use double for division to get accurate results
            double divisionResult = (secondNumber != 0) ? (double)firstNumber / secondNumber : 0;
            
            // Display results with proper formatting
            Console.WriteLine($""{userName}, the result of {firstNumber} compared to {secondNumber} is: {comparisonResult}"");
            Console.WriteLine($""Multiplication: {multiplicationResult}"");
            Console.WriteLine($""Sum: {additionResult}"");
            Console.WriteLine($""Division: {divisionResult:F2}""); // Format to 2 decimal places
            
            // Classify the multiplication result
            string resultMagnitude = (multiplicationResult > 100) ? ""large"" : ""small"";
            Console.WriteLine($""The multiplication result is: {resultMagnitude}"");
        }
    }
}";
        }

        private string GenerateDebugStarterCode()
        {
            return @"using System;

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
            Console.WriteLine(""Average: "" + average); // Should show decimal places
            Console.WriteLine(""Maximum: "" + maxValue);
            Console.WriteLine(""Percentage above average: "" + percentageAboveAverage + ""%"");
            
            // BUG 5: Potential overflow in large calculations
            int largeNumber1 = 2000000000;
            int largeNumber2 = 2000000000;
            int product = largeNumber1 * largeNumber2; // Overflow!
            Console.WriteLine(""Large calculation result: "" + product);
        }
    }
}";
        }

        private string GenerateDebugSolutionCode()
        {
            return @"using System;

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
            Console.WriteLine($""Average: {average:F2}""); // Show 2 decimal places
            Console.WriteLine($""Maximum: {maxValue}"");
            Console.WriteLine($""Percentage above average: {percentageAboveAverage:F2}%"");
            
            // FIX 5: Use long to prevent overflow, or check for overflow
            long largeNumber1 = 2000000000L;
            long largeNumber2 = 2000000000L;
            long product = largeNumber1 * largeNumber2; // Use long to handle larger values
            Console.WriteLine($""Large calculation result: {product:N0}""); // Format with thousands separators
            
            // Alternative: Check for overflow before calculation
            try
            {
                checked
                {
                    int intResult = (int)largeNumber1 * (int)largeNumber2;
                    Console.WriteLine($""Checked calculation: {intResult}"");
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine(""Calculation would overflow - using safe method instead"");
            }
        }
    }
}";
        }

        private string GenerateExtensionStarterCode()
        {
            return @"using System;

namespace CSharpFundamentals.ExtendMe
{
    /// <summary>
    /// Basic Calculator - Ready for Extension
    /// 
    /// This calculator currently supports basic operations.
    /// YOUR TASK: Extend it with additional mathematical operations and features.
    /// </summary>
    public class BasicCalculator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(""=== Basic Calculator ==="");
            
            double num1 = GetNumber(""Enter first number: "");
            double num2 = GetNumber(""Enter second number: "");
            
            Console.WriteLine(""\\nBasic Operations:"");
            Console.WriteLine($""Addition: {num1} + {num2} = {Add(num1, num2)}"");
            Console.WriteLine($""Subtraction: {num1} - {num2} = {Subtract(num1, num2)}"");
            Console.WriteLine($""Multiplication: {num1} * {num2} = {Multiply(num1, num2)}"");
            Console.WriteLine($""Division: {num1} / {num2} = {Divide(num1, num2)}"");
            
            // TODO: Add your extended operations here!
            // Ideas: Power, square root, percentage, area calculations, etc.
        }
        
        private static double GetNumber(string prompt)
        {
            Console.Write(prompt);
            return double.Parse(Console.ReadLine() ?? ""0"");
        }
        
        private static double Add(double a, double b) => a + b;
        private static double Subtract(double a, double b) => a - b;
        private static double Multiply(double a, double b) => a * b;
        private static double Divide(double a, double b) => b != 0 ? a / b : double.NaN;
        
        // TODO: Add your extension methods here!
    }
}";
        }

        private string GenerateExtensionSolutionCode()
        {
            return @"using System;

namespace CSharpFundamentals.ExtendMe
{
    /// <summary>
    /// Extended Calculator - With Additional Mathematical Operations
    /// 
    /// This extended version demonstrates:
    /// - Additional mathematical operations
    /// - Use of Math class methods
    /// - Constants for mathematical values
    /// - Better error handling and input validation
    /// </summary>
    public class ExtendedCalculator
    {
        // Mathematical constants
        private const double PI = Math.PI;
        private const double E = Math.E;
        
        public static void Main(string[] args)
        {
            Console.WriteLine(""=== Extended Calculator ==="");
            
            double num1 = GetNumber(""Enter first number: "");
            double num2 = GetNumber(""Enter second number: "");
            
            Console.WriteLine(""\\nBasic Operations:"");
            Console.WriteLine($""Addition: {num1} + {num2} = {Add(num1, num2):F2}"");
            Console.WriteLine($""Subtraction: {num1} - {num2} = {Subtract(num1, num2):F2}"");
            Console.WriteLine($""Multiplication: {num1} * {num2} = {Multiply(num1, num2):F2}"");
            Console.WriteLine($""Division: {num1} / {num2} = {Divide(num1, num2):F2}"");
            
            Console.WriteLine(""\\nAdvanced Operations:"");
            Console.WriteLine($""Power: {num1}^{num2} = {Power(num1, num2):F2}"");
            Console.WriteLine($""Square root of {num1} = {SquareRoot(num1):F2}"");
            Console.WriteLine($""Percentage: {num1}% of {num2} = {Percentage(num1, num2):F2}"");
            Console.WriteLine($""Average: ({num1} + {num2}) / 2 = {Average(num1, num2):F2}"");
            
            Console.WriteLine(""\\nTrigonometric Operations (using first number as angle in degrees):"");
            Console.WriteLine($""Sin({num1}°) = {SinDegrees(num1):F4}"");
            Console.WriteLine($""Cos({num1}°) = {CosDegrees(num1):F4}"");
            Console.WriteLine($""Tan({num1}°) = {TanDegrees(num1):F4}"");
            
            Console.WriteLine(""\\nGeometric Calculations (using both numbers):"");
            Console.WriteLine($""Circle area (radius = {num1}): {CircleArea(num1):F2}"");
            Console.WriteLine($""Rectangle area ({num1} x {num2}): {RectangleArea(num1, num2):F2}"");
            Console.WriteLine($""Hypotenuse ({num1}, {num2}): {Hypotenuse(num1, num2):F2}"");
            
            Console.WriteLine(""\\nConstants:"");
            Console.WriteLine($""PI = {PI:F6}"");
            Console.WriteLine($""E = {E:F6}"");
        }
        
        private static double GetNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? ""0"";
                
                if (double.TryParse(input, out double result))
                {
                    return result;
                }
                
                Console.WriteLine(""Invalid number. Please try again."");
            }
        }
        
        // Basic operations
        private static double Add(double a, double b) => a + b;
        private static double Subtract(double a, double b) => a - b;
        private static double Multiply(double a, double b) => a * b;
        private static double Divide(double a, double b) => b != 0 ? a / b : double.NaN;
        
        // Extended mathematical operations
        private static double Power(double baseNum, double exponent)
        {
            if (baseNum == 0 && exponent < 0)
                return double.PositiveInfinity;
            return Math.Pow(baseNum, exponent);
        }
        
        private static double SquareRoot(double number)
        {
            if (number < 0)
                return double.NaN; // Cannot take square root of negative number
            return Math.Sqrt(number);
        }
        
        private static double Percentage(double percent, double total)
        {
            return (percent / 100.0) * total;
        }
        
        private static double Average(double a, double b)
        {
            return (a + b) / 2.0;
        }
        
        // Trigonometric functions (convert degrees to radians)
        private static double SinDegrees(double degrees)
        {
            double radians = degrees * (PI / 180.0);
            return Math.Sin(radians);
        }
        
        private static double CosDegrees(double degrees)
        {
            double radians = degrees * (PI / 180.0);
            return Math.Cos(radians);
        }
        
        private static double TanDegrees(double degrees)
        {
            double radians = degrees * (PI / 180.0);
            return Math.Tan(radians);
        }
        
        // Geometric calculations
        private static double CircleArea(double radius)
        {
            if (radius < 0) return 0;
            return PI * radius * radius;
        }
        
        private static double RectangleArea(double length, double width)
        {
            return Math.Abs(length * width); // Use absolute values for area
        }
        
        private static double Hypotenuse(double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }
    }
}";
        }

        #endregion
    }
}