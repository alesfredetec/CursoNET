using System;

class Program 
{
    static void Main() 
    {
        // Variables con nombres descriptivos y tipos apropiados
        int edadActual = 25;
        string nombrePersona = "Juan";
        double alturaEnMetros = 1.75;
        int anioNacimiento = 1998;
        int edadCalculada = DateTime.Now.Year - anioNacimiento;
        bool esMayorDeEdad = edadActual >= 18;
        
        // Uso de interpolación de strings para mayor claridad
        Console.WriteLine($"{nombrePersona} tiene {edadCalculada} años y {(esMayorDeEdad ? "es" : "no es")} mayor de edad");
        Console.WriteLine($"Su altura es: {alturaEnMetros:F2}m");
    }
}