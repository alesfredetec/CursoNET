using System;

namespace CSharpFundamentals.RefactorMe
{
    /// <summary>
    /// TODO: Refactor this code to follow C# best practices
    /// Focus on: variable naming, data types, formatting, comments
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Replace single-letter variables with descriptive names
            string a=Console.ReadLine();
            int b=int.Parse(Console.ReadLine());
            int c=int.Parse(Console.ReadLine());
            
            // TODO: Improve variable naming and formatting
            string d=(b>c)?"yes":"no";
            int e=b*c;
            int f=b+c;
            int g=b/c; // TODO: Consider data type for division
            
            // TODO: Use string interpolation instead of concatenation
            Console.WriteLine(a+" the result of "+b+" compared to "+c+" is: "+d);
            Console.WriteLine("multiplication: "+e);
            Console.WriteLine("sum: "+f);
            Console.WriteLine("division: "+g);
            
            // TODO: Improve variable naming
            string h=(e>100)?"large":"small";
            Console.WriteLine("The multiplication result is: "+h);
        }
    }
}