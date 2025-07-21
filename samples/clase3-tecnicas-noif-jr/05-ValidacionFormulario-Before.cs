using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJr
{
    /// <summary>
    /// PROBLEMA: Validaciones con muchos if-else anidados
    /// 
    /// Este ejercicio simula un sistema de validación de formularios
    /// donde se usan múltiples condicionales para validar cada campo.
    /// 
    /// ISSUES IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Código repetitivo para cada validación
    /// ❌ PROBLEMA 2: Difícil de leer con muchas validaciones anidadas
    /// ❌ PROBLEMA 3: Difícil de extender con nuevas reglas de validación
    /// ❌ PROBLEMA 4: Manejo de errores mezclado con la lógica de validación
    /// </summary>
    public class ValidadorFormularioTradicional
    {
        /// <summary>
        /// Clase que representa un formulario de usuario
        /// </summary>
        public class FormularioUsuario
        {
            public string Nombre { get; set; }
            public string Email { get; set; }
            public string Telefono { get; set; }
            public int Edad { get; set; }
        }

        /// <summary>
        /// Método que valida un formulario con múltiples if-else
        /// </summary>
        public List<string> ValidarFormulario(FormularioUsuario formulario)
        {
            List<string> errores = new List<string>();

            // ❌ PROBLEMA 1: Validación del nombre con varios if-else
            if (string.IsNullOrEmpty(formulario.Nombre))
            {
                errores.Add("El nombre es obligatorio");
            }
            else
            {
                if (formulario.Nombre.Length < 3)
                {
                    errores.Add("El nombre debe tener al menos 3 caracteres");
                }
                if (formulario.Nombre.Length > 50)
                {
                    errores.Add("El nombre no debe exceder los 50 caracteres");
                }
            }

            // ❌ PROBLEMA 1: Validación del email con varios if-else
            if (string.IsNullOrEmpty(formulario.Email))
            {
                errores.Add("El email es obligatorio");
            }
            else
            {
                if (!formulario.Email.Contains("@"))
                {
                    errores.Add("El email debe contener @");
                }
                if (!formulario.Email.Contains("."))
                {
                    errores.Add("El email debe contener .");
                }
            }

            // ❌ PROBLEMA 1: Validación del teléfono con varios if-else
            if (!string.IsNullOrEmpty(formulario.Telefono))
            {
                if (formulario.Telefono.Length < 9)
                {
                    errores.Add("El teléfono debe tener al menos 9 dígitos");
                }
                foreach (char c in formulario.Telefono)
                {
                    if (!char.IsDigit(c))
                    {
                        errores.Add("El teléfono debe contener solo dígitos");
                        break;
                    }
                }
            }

            // ❌ PROBLEMA 1: Validación de la edad con varios if-else
            if (formulario.Edad <= 0)
            {
                errores.Add("La edad es obligatoria");
            }
            else
            {
                if (formulario.Edad < 18)
                {
                    errores.Add("Debes ser mayor de edad (18+)");
                }
                if (formulario.Edad > 120)
                {
                    errores.Add("La edad no parece válida");
                }
            }

            return errores;
        }

        /// <summary>
        /// Método de demostración del validador tradicional
        /// </summary>
        public void DemoValidacionTradicional()
        {
            var formularioValido = new FormularioUsuario
            {
                Nombre = "Juan Pérez",
                Email = "juan@ejemplo.com",
                Telefono = "123456789",
                Edad = 25
            };

            var formularioInvalido = new FormularioUsuario
            {
                Nombre = "A",
                Email = "correo-sin-arroba",
                Telefono = "123abc",
                Edad = 15
            };

            Console.WriteLine("=== Validación de Formulario Tradicional ===");

            Console.WriteLine("\nValidando formulario válido:");
            var erroresFormularioValido = ValidarFormulario(formularioValido);
            if (erroresFormularioValido.Count == 0)
            {
                Console.WriteLine("El formulario es válido. No hay errores.");
            }
            else
            {
                Console.WriteLine($"Errores encontrados: {erroresFormularioValido.Count}");
                foreach (var error in erroresFormularioValido)
                {
                    Console.WriteLine($"- {error}");
                }
            }

            Console.WriteLine("\nValidando formulario inválido:");
            var erroresFormularioInvalido = ValidarFormulario(formularioInvalido);
            if (erroresFormularioInvalido.Count == 0)
            {
                Console.WriteLine("El formulario es válido. No hay errores.");
            }
            else
            {
                Console.WriteLine($"Errores encontrados: {erroresFormularioInvalido.Count}");
                foreach (var error in erroresFormularioInvalido)
                {
                    Console.WriteLine($"- {error}");
                }
            }
        }
    }

    /*
    PROBLEMAS PRINCIPALES DE ESTE CÓDIGO:
    
    1. CÓDIGO REPETITIVO:
       - La misma estructura if-else se repite para cada campo
       - Para cada validación necesitamos bloques de código similares

    2. DIFÍCIL DE LEER:
       - Con más validaciones, el código se vuelve muy largo
       - Es fácil perderse entre tantos if-else anidados

    3. DIFÍCIL DE EXTENDER:
       - Para agregar una nueva validación, hay que modificar el método principal
       - Cada nueva regla requiere más código y más if-else

    4. MEZCLA DE RESPONSABILIDADES:
       - La validación y el manejo de errores están entrelazados
       - No es fácil reutilizar las validaciones en otros lugares
    */
}
