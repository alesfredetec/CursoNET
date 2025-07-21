using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJr
{
    /// <summary>
    /// SOLUCIÓN: Validaciones fluidas con extension methods
    /// 
    /// Este ejercicio muestra cómo refactorizar un sistema de validación
    /// usando métodos de extensión para crear una API fluida que elimina
    /// condicionales y mejora la legibilidad.
    /// 
    /// MEJORAS IMPLEMENTADAS:
    /// ✅ SOLUCIÓN 1: Crear métodos de extensión para validaciones comunes
    /// ✅ SOLUCIÓN 2: Implementar una API fluida con encadenamiento de métodos
    /// ✅ SOLUCIÓN 3: Separar la validación del manejo de errores
    /// ✅ SOLUCIÓN 4: Facilitar la adición de nuevas reglas de validación
    /// </summary>
    public class ValidadorFormularioConExtensions
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

        // ✅ SOLUCIÓN 1: Crear una clase para representar el resultado de una validación
        /// <summary>
        /// Clase que mantiene el valor validado y los errores encontrados
        /// </summary>
        public class ResultadoValidacion<T>
        {
            public T Valor { get; }
            public List<string> Errores { get; } = new List<string>();
            public bool EsValido => Errores.Count == 0;

            public ResultadoValidacion(T valor)
            {
                Valor = valor;
            }

            public void AgregarError(string error)
            {
                Errores.Add(error);
            }
        }

        // ✅ SOLUCIÓN 2: Crear métodos de extensión para validaciones comunes
        /// <summary>
        /// Métodos de extensión para validación de strings
        /// </summary>
        public static class ValidacionExtensions
        {
            // Validación: Campo requerido (no nulo y no vacío)
            public static ResultadoValidacion<string> Requerido(this string valor, string nombreCampo)
            {
                var resultado = new ResultadoValidacion<string>(valor);
                
                if (string.IsNullOrEmpty(valor))
                {
                    resultado.AgregarError($"{nombreCampo} es obligatorio");
                }
                
                return resultado;
            }

            // Validación: Longitud mínima de texto
            public static ResultadoValidacion<string> LongitudMinima(this ResultadoValidacion<string> resultado, int longitud)
            {
                // Solo validamos si no hay errores previos
                if (resultado.EsValido && resultado.Valor.Length < longitud)
                {
                    resultado.AgregarError($"Debe tener al menos {longitud} caracteres");
                }
                
                return resultado;
            }

            // Validación: Longitud máxima de texto
            public static ResultadoValidacion<string> LongitudMaxima(this ResultadoValidacion<string> resultado, int longitud)
            {
                // Solo validamos si no hay errores previos
                if (resultado.EsValido && resultado.Valor.Length > longitud)
                {
                    resultado.AgregarError($"No debe exceder los {longitud} caracteres");
                }
                
                return resultado;
            }

            // Validación: Debe contener un carácter específico
            public static ResultadoValidacion<string> DebeContener(this ResultadoValidacion<string> resultado, string caracter, string mensaje = null)
            {
                // Solo validamos si no hay errores previos
                if (resultado.EsValido && !resultado.Valor.Contains(caracter))
                {
                    resultado.AgregarError(mensaje ?? $"Debe contener '{caracter}'");
                }
                
                return resultado;
            }

            // Validación: Solo dígitos
            public static ResultadoValidacion<string> SoloDigitos(this ResultadoValidacion<string> resultado)
            {
                // Solo validamos si no hay errores previos y si hay valor
                if (resultado.EsValido && !string.IsNullOrEmpty(resultado.Valor))
                {
                    foreach (char c in resultado.Valor)
                    {
                        if (!char.IsDigit(c))
                        {
                            resultado.AgregarError("Debe contener solo dígitos");
                            break;
                        }
                    }
                }
                
                return resultado;
            }

            // Validación: Campo requerido para enteros
            public static ResultadoValidacion<int> Requerido(this int valor, string nombreCampo)
            {
                var resultado = new ResultadoValidacion<int>(valor);
                
                if (valor <= 0)
                {
                    resultado.AgregarError($"{nombreCampo} es obligatorio");
                }
                
                return resultado;
            }

            // Validación: Rango de valores para enteros
            public static ResultadoValidacion<int> EnRango(this ResultadoValidacion<int> resultado, int minimo, int maximo)
            {
                // Solo validamos si no hay errores previos
                if (resultado.EsValido)
                {
                    if (resultado.Valor < minimo)
                    {
                        resultado.AgregarError($"Debe ser al menos {minimo}");
                    }
                    else if (resultado.Valor > maximo)
                    {
                        resultado.AgregarError($"No debe ser mayor que {maximo}");
                    }
                }
                
                return resultado;
            }
        }

        /// <summary>
        /// Método que valida un formulario usando extension methods
        /// </summary>
        public List<string> ValidarFormulario(FormularioUsuario formulario)
        {
            var errores = new List<string>();

            // ✅ SOLUCIÓN 3: Validación del nombre con API fluida
            var resultadoNombre = formulario.Nombre
                .Requerido("El nombre")           // Verificar si no está vacío
                .LongitudMinima(3)                // Verificar longitud mínima
                .LongitudMaxima(50);              // Verificar longitud máxima
            
            // Agregar errores del nombre si existen
            if (!resultadoNombre.EsValido)
            {
                errores.AddRange(resultadoNombre.Errores);
            }

            // ✅ SOLUCIÓN 3: Validación del email con API fluida
            var resultadoEmail = formulario.Email
                .Requerido("El email")             // Verificar si no está vacío
                .DebeContener("@", "El email debe contener @")
                .DebeContener(".", "El email debe contener .");
            
            // Agregar errores del email si existen
            if (!resultadoEmail.EsValido)
            {
                errores.AddRange(resultadoEmail.Errores);
            }

            // ✅ SOLUCIÓN 3: Validación del teléfono con API fluida (opcional)
            if (!string.IsNullOrEmpty(formulario.Telefono))
            {
                var resultadoTelefono = new ResultadoValidacion<string>(formulario.Telefono)
                    .LongitudMinima(9)            // Verificar longitud mínima
                    .SoloDigitos();               // Verificar que solo contiene dígitos
                
                // Agregar errores del teléfono si existen
                if (!resultadoTelefono.EsValido)
                {
                    errores.AddRange(resultadoTelefono.Errores);
                }
            }

            // ✅ SOLUCIÓN 3: Validación de la edad con API fluida
            var resultadoEdad = formulario.Edad
                .Requerido("La edad")             // Verificar si es mayor que cero
                .EnRango(18, 120);                // Verificar rango válido
            
            // Agregar errores de la edad si existen
            if (!resultadoEdad.EsValido)
            {
                errores.AddRange(resultadoEdad.Errores);
            }

            return errores;
        }

        /// <summary>
        /// Método de demostración del validador con extension methods
        /// </summary>
        public void DemoValidacionConExtensions()
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

            Console.WriteLine("=== Validación de Formulario con Extension Methods ===");

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

            // ✅ SOLUCIÓN 4: Demostración de cómo extender con reglas de validación personalizadas
            Console.WriteLine("\n=== Demostración de extensibilidad ===");
            
            // Agregamos un método de extensión personalizado directamente aquí para demostración
            static ResultadoValidacion<string> EsEmailEmpresarial(this ResultadoValidacion<string> resultado)
            {
                if (resultado.EsValido && !resultado.Valor.EndsWith(".com"))
                {
                    resultado.AgregarError("Debe ser un email empresarial (.com)");
                }
                return resultado;
            }

            // Validamos un email con nuestra regla personalizada
            var emailPersonalizado = "usuario@dominio.org";
            var resultadoPersonalizado = emailPersonalizado
                .Requerido("El email")
                .DebeContener("@")
                .DebeContener(".")
                .EsEmailEmpresarial(); // Nuestra nueva regla de validación

            Console.WriteLine($"Validando email '{emailPersonalizado}' con regla personalizada:");
            if (resultadoPersonalizado.EsValido)
            {
                Console.WriteLine("El email es válido para reglas personalizadas.");
            }
            else
            {
                Console.WriteLine($"Errores encontrados: {resultadoPersonalizado.Errores.Count}");
                foreach (var error in resultadoPersonalizado.Errores)
                {
                    Console.WriteLine($"- {error}");
                }
            }
        }
    }

    /*
    VENTAJAS DE LOS EXTENSION METHODS:
    
    1. CÓDIGO MÁS LEGIBLE:
       - Las validaciones se encadenan de manera fluida y descriptiva
       - Fácil de leer y entender lo que hace cada validación

    2. CÓDIGO MÁS MANTENIBLE:
       - Cada regla de validación está encapsulada en su propio método
       - No hay if-else anidados que compliquen el seguimiento

    3. FÁCIL DE EXTENDER:
       - Se pueden crear nuevos métodos de extensión para nuevas reglas
       - No es necesario modificar el código existente (Open/Closed)

    4. SEPARACIÓN DE RESPONSABILIDADES:
       - La validación está separada del manejo de errores
       - Los métodos de extensión son reutilizables en toda la aplicación

    DIAGRAMA DE LA API FLUIDA (ASCII):
    
    +------------------------+
    | "texto".Requerido()    |
    +------------------------+
                |
                ▼
    +------------------------+
    | .LongitudMinima(3)     |
    +------------------------+
                |
                ▼
    +------------------------+
    | .LongitudMaxima(50)    |
    +------------------------+
                |
                ▼
    +------------------------+
    | ResultadoValidacion    |
    | - Valor: string        |
    | - Errores: List<string>|
    | - EsValido: bool       |
    +------------------------+
    */
}
