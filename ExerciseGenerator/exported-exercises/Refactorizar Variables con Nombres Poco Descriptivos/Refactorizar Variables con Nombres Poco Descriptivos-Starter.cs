using System;

class Program 
{
    static void Main() 
    {
        int a = 25;
        string b = "Juan";
        double c = 1.75;
        int d = DateTime.Now.Year - 1998;
        bool e = a >= 18;
        
        Console.WriteLine(b + " tiene " + d + " años y " + (e ? "es" : "no es") + " mayor de edad");
        Console.WriteLine("Su altura es: " + c + "m");
    }
}