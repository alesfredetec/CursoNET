# Personal Information Calculator

**Skill Level:** Beginner
**Topic Area:** CSharpFundamentals
**Exercise Type:** Implementation
**Estimated Time:** 25 minutes

## Description
Context: 

Create a simple program that collects personal information and performs basic calculations with different data types.

This exercise will help you practice:
- Variable declarations and initialization
- Different data types (int, double, string, bool, DateTime)
- Basic arithmetic operations
- String interpolation and formatting
- Type conversions

## Learning Objectives
- Declare and initialize variables of different data types
- Perform arithmetic operations with numeric types
- Use string interpolation for output formatting
- Convert between different numeric types safely
- Work with DateTime for age calculations

## Prerequisites
- Basic understanding of what variables are
- Familiarity with C# syntax for variable declaration
- Understanding of basic arithmetic operations

## Problem Statement
You need to create a Personal Information Calculator that:

1. Collects a person's name, birth year, height in centimeters, and whether they are a student
2. Calculates their age based on the current year
3. Converts their height to meters and feet
4. Calculates their BMI if they provide their weight
5. Displays all information in a formatted output

The program should handle different data types correctly and perform accurate calculations.

## Technical Requirements
- Use appropriate data types for each piece of information
- Perform type conversions where necessary
- Use string interpolation for formatted output
- Handle potential division by zero for BMI calculation
- Use DateTime.Now.Year for current year calculation

## Success Criteria
- Program compiles without errors
- All variables are declared with appropriate data types
- Age calculation is correct
- Height conversions are accurate (1m = 100cm, 1ft = 30.48cm)
- BMI calculation is correct (weight_kg / (height_m)^2)
- Output is well-formatted and readable

## Extension Challenges
- Add input validation to ensure positive values for height and weight
- Include BMI category classification (underweight, normal, overweight, obese)
- Add calculation for ideal weight range based on height
- Format the birth year input to accept full dates and calculate exact age
- Add support for different measurement systems (metric vs imperial)

## Common Pitfalls & Tips
- Don't forget to handle division by zero when calculating BMI
- Be careful with integer division - use double for accurate results
- Remember that DateTime calculations can be tricky - consider using DateTime.Today for current date
- Use meaningful variable names that describe what the data represents
- Test your calculations with known values to verify accuracy
- Don't worry about perfect code - focus on getting it working first
- Use descriptive variable names to make your code readable
- Test your code frequently with small changes

## Files
- `Personal Information Calculator-Starter.cs` - Your starting point
- `Personal Information Calculator-Solution.cs` - Complete solution (review after attempting)
- `Personal Information Calculator-Tests.cs` - Unit tests to validate your solution
