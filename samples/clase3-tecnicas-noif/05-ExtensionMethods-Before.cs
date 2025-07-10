using System;
using System.Collections.Generic;
using System.Linq;

namespace TecnicasNoIf.Ejercicios
{
    /// <summary>
    /// EJERCICIO COMPLEMENTARIO: Extension Methods para eliminar condicionales
    /// 
    /// PROBLEMAS IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Validaciones repetitivas con múltiples if
    /// ❌ PROBLEMA 2: Lógica condicional esparcida por toda la aplicación
    /// ❌ PROBLEMA 3: Métodos utilitarios estáticos poco legibles
    /// ❌ PROBLEMA 4: Fluent interfaces imposibles sin extension methods
    /// ❌ PROBLEMA 5: Código no chainable y difícil de leer
    /// ❌ PROBLEMA 6: Validación y transformación mezcladas
    /// </summary>

    public class UserValidationProblematic
    {
        // ❌ PROBLEMA 1: Validaciones con múltiples if anidados
        public bool ValidateUser(User user)
        {
            if (user == null)
            {
                Console.WriteLine("User cannot be null");
                return false;
            }

            if (user.Email == null)
            {
                Console.WriteLine("Email is required");
                return false;
            }

            if (user.Email.Length == 0)
            {
                Console.WriteLine("Email cannot be empty");
                return false;
            }

            if (!user.Email.Contains("@"))
            {
                Console.WriteLine("Email must contain @");
                return false;
            }

            if (user.Age < 0)
            {
                Console.WriteLine("Age cannot be negative");
                return false;
            }

            if (user.Age > 150)
            {
                Console.WriteLine("Age cannot be over 150");
                return false;
            }

            if (user.Name == null)
            {
                Console.WriteLine("Name is required");
                return false;
            }

            if (user.Name.Trim().Length == 0)
            {
                Console.WriteLine("Name cannot be empty");
                return false;
            }

            if (user.Name.Length < 2)
            {
                Console.WriteLine("Name must be at least 2 characters");
                return false;
            }

            return true;
        }

        // ❌ PROBLEMA 2: Transformación de datos con switch complejos
        public string FormatUserForDisplay(User user)
        {
            if (user == null) return "Unknown User";

            string displayName;
            if (user.Name != null && user.Name.Trim().Length > 0)
            {
                displayName = user.Name.Trim();
            }
            else
            {
                displayName = "Anonymous";
            }

            string ageDisplay;
            if (user.Age > 0)
            {
                ageDisplay = $"{user.Age} years old";
            }
            else
            {
                ageDisplay = "Age not specified";
            }

            string emailDisplay;
            if (user.Email != null && user.Email.Contains("@"))
            {
                emailDisplay = user.Email.ToLower();
            }
            else
            {
                emailDisplay = "No email provided";
            }

            return $"{displayName} ({ageDisplay}) - {emailDisplay}";
        }

        // ❌ PROBLEMA 3: Métodos estáticos poco legibles
        public List<User> FilterUsers(List<User> users, string criteria)
        {
            var result = new List<User>();

            foreach (var user in users)
            {
                bool shouldInclude = false;

                if (criteria == "ADULTS")
                {
                    if (user.Age >= 18) shouldInclude = true;
                }
                else if (criteria == "SENIORS")
                {
                    if (user.Age >= 65) shouldInclude = true;
                }
                else if (criteria == "TEENS")
                {
                    if (user.Age >= 13 && user.Age < 18) shouldInclude = true;
                }
                else if (criteria == "KIDS")
                {
                    if (user.Age < 13) shouldInclude = true;
                }
                else if (criteria == "VALID_EMAIL")
                {
                    if (user.Email != null && user.Email.Contains("@")) shouldInclude = true;
                }
                else if (criteria == "NO_EMAIL")
                {
                    if (user.Email == null || !user.Email.Contains("@")) shouldInclude = true;
                }

                if (shouldInclude)
                {
                    result.Add(user);
                }
            }

            return result;
        }

        // ❌ PROBLEMA 4: Procesamiento de datos no chainable
        public List<User> ProcessUserData(List<User> users)
        {
            // Step 1: Filter out null users
            var step1 = new List<User>();
            foreach (var user in users)
            {
                if (user != null) step1.Add(user);
            }

            // Step 2: Filter valid emails
            var step2 = new List<User>();
            foreach (var user in step1)
            {
                if (user.Email != null && user.Email.Contains("@"))
                {
                    step2.Add(user);
                }
            }

            // Step 3: Filter adults
            var step3 = new List<User>();
            foreach (var user in step2)
            {
                if (user.Age >= 18) step3.Add(user);
            }

            // Step 4: Normalize names
            var step4 = new List<User>();
            foreach (var user in step3)
            {
                var normalizedUser = new User
                {
                    Id = user.Id,
                    Name = user.Name?.Trim()?.ToTitleCase() ?? "Unknown",
                    Email = user.Email?.ToLower(),
                    Age = user.Age
                };
                step4.Add(normalizedUser);
            }

            return step4;
        }
    }

    // ❌ PROBLEMA 5: Validador de strings con múltiples métodos separados
    public class StringValidatorProblematic
    {
        public bool IsValidEmail(string email)
        {
            if (email == null) return false;
            if (email.Length == 0) return false;
            if (!email.Contains("@")) return false;
            if (email.IndexOf("@") == 0) return false;
            if (email.LastIndexOf("@") == email.Length - 1) return false;
            return true;
        }

        public bool IsValidPhoneNumber(string phone)
        {
            if (phone == null) return false;
            if (phone.Length == 0) return false;
            
            var digitsOnly = "";
            foreach (char c in phone)
            {
                if (char.IsDigit(c)) digitsOnly += c;
            }
            
            if (digitsOnly.Length < 10) return false;
            if (digitsOnly.Length > 15) return false;
            return true;
        }

        public bool IsValidName(string name)
        {
            if (name == null) return false;
            if (name.Trim().Length == 0) return false;
            if (name.Trim().Length < 2) return false;
            if (name.Trim().Length > 50) return false;
            
            foreach (char c in name.Trim())
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '-' && c != '\'')
                {
                    return false;
                }
            }
            
            return true;
        }

        // ❌ PROBLEMA: Uso engorroso de validaciones
        public ValidationResult ValidateUserInputProblematic(string name, string email, string phone)
        {
            var validator = new StringValidatorProblematic();
            var result = new ValidationResult();

            if (!validator.IsValidName(name))
            {
                result.AddError("Name", "Invalid name format");
            }

            if (!validator.IsValidEmail(email))
            {
                result.AddError("Email", "Invalid email format");
            }

            if (!validator.IsValidPhoneNumber(phone))
            {
                result.AddError("Phone", "Invalid phone number format");
            }

            return result;
        }
    }

    // ❌ PROBLEMA 6: Procesamiento de colecciones sin fluent interface
    public class DataProcessorProblematic
    {
        public List<UserSummary> CreateUserSummaries(List<User> users)
        {
            var summaries = new List<UserSummary>();

            foreach (var user in users)
            {
                // Multiple validation checks
                if (user == null) continue;
                if (user.Name == null || user.Name.Trim().Length == 0) continue;
                if (user.Age < 0 || user.Age > 150) continue;

                // Multiple transformation steps
                string displayName;
                if (user.Name.Length > 20)
                {
                    displayName = user.Name.Substring(0, 17) + "...";
                }
                else
                {
                    displayName = user.Name;
                }

                string ageGroup;
                if (user.Age < 18)
                {
                    ageGroup = "Minor";
                }
                else if (user.Age < 65)
                {
                    ageGroup = "Adult";
                }
                else
                {
                    ageGroup = "Senior";
                }

                bool hasValidEmail = user.Email != null && user.Email.Contains("@");

                var summary = new UserSummary
                {
                    Id = user.Id,
                    DisplayName = displayName,
                    AgeGroup = ageGroup,
                    HasValidEmail = hasValidEmail,
                    ContactInfo = hasValidEmail ? user.Email : "No contact available"
                };

                summaries.Add(summary);
            }

            return summaries;
        }

        // ❌ PROBLEMA: Buscar usuarios con criterios complejos
        public List<User> FindUsersWithComplexCriteria(List<User> users, SearchCriteria criteria)
        {
            var results = new List<User>();

            foreach (var user in users)
            {
                bool matches = true;

                // Age criteria
                if (criteria.MinAge.HasValue)
                {
                    if (user.Age < criteria.MinAge.Value) matches = false;
                }

                if (criteria.MaxAge.HasValue)
                {
                    if (user.Age > criteria.MaxAge.Value) matches = false;
                }

                // Name criteria
                if (!string.IsNullOrEmpty(criteria.NameContains))
                {
                    if (user.Name == null || !user.Name.ToLower().Contains(criteria.NameContains.ToLower()))
                    {
                        matches = false;
                    }
                }

                // Email criteria
                if (criteria.MustHaveEmail)
                {
                    if (user.Email == null || !user.Email.Contains("@")) matches = false;
                }

                if (criteria.EmailDomain != null)
                {
                    if (user.Email == null || !user.Email.ToLower().EndsWith($"@{criteria.EmailDomain.ToLower()}"))
                    {
                        matches = false;
                    }
                }

                if (matches)
                {
                    results.Add(user);
                }
            }

            return results;
        }
    }

    // Supporting classes
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

    public class UserSummary
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string AgeGroup { get; set; }
        public bool HasValidEmail { get; set; }
        public string ContactInfo { get; set; }
    }

    public class SearchCriteria
    {
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string NameContains { get; set; }
        public bool MustHaveEmail { get; set; }
        public string EmailDomain { get; set; }
    }

    public class ValidationResult
    {
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public bool IsValid => Errors.Count == 0;

        public void AddError(string field, string message)
        {
            Errors[field] = message;
        }
    }

    // Extension method helper (problematic implementation)
    public static class StringExtensionsProblematic
    {
        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            
            var words = input.Split(' ');
            var result = "";
            
            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    result += char.ToUpper(word[0]) + word.Substring(1).ToLower() + " ";
                }
            }
            
            return result.Trim();
        }
    }

    /*
    PROBLEMAS PRINCIPALES:

    1. VALIDACIÓN REPETITIVA:
       - Múltiples if statements para cada validación
       - Lógica duplicada en diferentes métodos
       - No reutilización de validaciones comunes

    2. CÓDIGO NO CHAINABLE:
       - Procesamiento paso a paso con variables temporales
       - No fluent interface para transformaciones
       - Difícil de leer y mantener

    3. MÉTODOS ESTÁTICOS POCO EXPRESIVOS:
       - StringValidator con métodos separados
       - No integración natural con objetos
       - API no intuitiva

    4. TRANSFORMACIONES COMPLEJAS:
       - Múltiples pasos de transformación manual
       - No composición de funciones
       - Lógica procedural en lugar de funcional

    5. FILTRADO INEFICIENTE:
       - Loops manuales para cada criterio
       - No aprovechamiento de LINQ
       - Criterios hard-coded

    OBJETIVO DEL EJERCICIO:
    Refactorizar usando Extension Methods para:
    ✅ Crear fluent interfaces expresivas
    ✅ Eliminar validaciones repetitivas
    ✅ Hacer código chainable y legible
    ✅ Simplificar transformaciones de datos
    ✅ Crear DSL interno para validaciones

    TIEMPO ESTIMADO: 45 minutos
    DIFICULTAD: Intermedio
    */
}