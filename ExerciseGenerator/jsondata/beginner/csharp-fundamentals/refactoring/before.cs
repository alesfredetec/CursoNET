using System;

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
        string d=(b>c)?"yes":"no";
        int e=b*c;
        int f=b+c;
        int g=b/c;
        Console.WriteLine(a+" the result of "+b+" compared to "+c+" is: "+d);
        Console.WriteLine("multiplication: "+e);
        Console.WriteLine("sum: "+f);
        Console.WriteLine("division: "+g);
        string h=(e>100)?"large":"small";
        Console.WriteLine("The multiplication result is: "+h);
        }
    }
}