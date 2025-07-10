using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace TecnicasNoIf.EjerciciosComplementarios
{
    /// <summary>
    /// EJERCICIO COMPLEMENTARIO: Extension Methods Avanzados para eliminar IFs
    /// 
    /// CONCEPTOS DEMOSTRADOS:
    /// ✅ Extension methods para validación fluent
    /// ✅ Chain of responsibility con extensions
    /// ✅ Functional programming con extensions
    /// ✅ Pipeline pattern con method chaining
    /// ✅ Conditional logic encapsulada
    /// </summary>

    // ================================
    // ✅ EXTENSION METHODS PARA VALIDACIÓN FLUENT
    // ================================

    public static class ValidationExtensions
    {
        public static ValidationResult<T> IsRequired<T>(this T value, string fieldName = null)
        {
            var result = new ValidationResult<T>(value);
            
            if (value == null || 
                (value is string str && string.IsNullOrWhiteSpace(str)) ||
                (value is int num && num == 0))
            {
                result.AddError($"{fieldName ?? "Field"} is required");
            }
            
            return result;
        }

        public static ValidationResult<string> HasMinLength(this ValidationResult<string> result, int minLength)
        {
            if (result.IsValid && (result.Value?.Length ?? 0) < minLength)
            {
                result.AddError($"Minimum length is {minLength} characters");
            }
            return result;
        }

        public static ValidationResult<string> HasMaxLength(this ValidationResult<string> result, int maxLength)
        {
            if (result.IsValid && (result.Value?.Length ?? 0) > maxLength)
            {
                result.AddError($"Maximum length is {maxLength} characters");
            }
            return result;
        }

        public static ValidationResult<string> IsEmail(this ValidationResult<string> result)
        {
            if (result.IsValid && !string.IsNullOrEmpty(result.Value))
            {
                if (!result.Value.Contains("@") || !result.Value.Contains("."))
                {
                    result.AddError("Invalid email format");
                }
            }
            return result;
        }

        public static ValidationResult<int> IsInRange(this ValidationResult<int> result, int min, int max)
        {
            if (result.IsValid && (result.Value < min || result.Value > max))
            {
                result.AddError($"Value must be between {min} and {max}");
            }
            return result;
        }

        public static ValidationResult<T> Must<T>(this ValidationResult<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsValid && !predicate(result.Value))
            {
                result.AddError(errorMessage);
            }
            return result;
        }
    }

    public class ValidationResult<T>
    {
        public T Value { get; }
        public List<string> Errors { get; } = new();
        public bool IsValid => Errors.Count == 0;

        public ValidationResult(T value)
        {
            Value = value;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public string GetErrorMessage() => string.Join("; ", Errors);
    }

    // ================================
    // ✅ EXTENSION METHODS PARA TRANSFORMACIÓN DE DATOS
    // ================================

    public static class TransformationExtensions
    {
        public static T WhenNotNull<T>(this T value, Action<T> action) where T : class
        {
            if (value != null)
                action(value);
            return value;
        }

        public static TResult WhenNotNull<T, TResult>(this T value, Func<T, TResult> func, TResult defaultValue = default) where T : class
        {
            return value != null ? func(value) : defaultValue;
        }

        public static T When<T>(this T value, Func<T, bool> condition, Func<T, T> transform)
        {
            return condition(value) ? transform(value) : value;
        }

        public static T Unless<T>(this T value, Func<T, bool> condition, Func<T, T> transform)
        {
            return !condition(value) ? transform(value) : value;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> TakeIf<T>(this IEnumerable<T> source, bool condition, int count)
        {
            return condition ? source.Take(count) : source;
        }

        public static string ToTitleCase(this string value)
        {
            return value.WhenNotNull(s => 
                string.IsNullOrWhiteSpace(s) ? s : 
                char.ToUpper(s[0]) + s.Substring(1).ToLower());
        }

        public static decimal ToPercentage(this decimal value, int decimals = 2)
        {
            return Math.Round(value * 100, decimals);
        }
    }

    // ================================
    // ✅ EXTENSION METHODS PARA LOGGING Y DEBUGGING
    // ================================

    public static class LoggingExtensions
    {
        public static T Log<T>(this T value, string message = null)
        {
            Console.WriteLine($"[LOG] {message ?? "Value"}: {value}");
            return value;
        }

        public static T LogIf<T>(this T value, Func<T, bool> condition, string message = null)
        {
            if (condition(value))
            {
                Console.WriteLine($"[LOG] {message ?? "Condition met"}: {value}");
            }
            return value;
        }

        public static T Tap<T>(this T value, Action<T> action)
        {
            action(value);
            return value;
        }

        public static T TapIf<T>(this T value, Func<T, bool> condition, Action<T> action)
        {
            if (condition(value))
                action(value);
            return value;
        }
    }

    // ================================
    // ✅ EXTENSION METHODS PARA COLECCIONES AVANZADAS
    // ================================

    public static class CollectionExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> ForEachIndexed<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int index = 0;
            foreach (var item in source)
            {
                action(item, index++);
                yield return item;
            }
        }

        public static Dictionary<TKey, TValue> ToDictionaryIgnoringDuplicates<T, TKey, TValue>(
            this IEnumerable<T> source, 
            Func<T, TKey> keySelector, 
            Func<T, TValue> valueSelector)
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var item in source)
            {
                var key = keySelector(item);
                if (!result.ContainsKey(key))
                {
                    result[key] = valueSelector(item);
                }
            }
            return result;
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var seen = new HashSet<TKey>();
            foreach (var item in source)
            {
                if (seen.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        public static bool HasItems<T>(this IEnumerable<T> source)
        {
            return !source.IsNullOrEmpty();
        }
    }

    // ================================
    // ✅ EXTENSION METHODS PARA STRING PROCESSING
    // ================================

    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength, string suffix = "...")
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
                return value;
                
            return value.Substring(0, maxLength - suffix.Length) + suffix;
        }

        public static string RemoveSpecialCharacters(this string value)
        {
            return value.WhenNotNull(s => new string(s.Where(char.IsLetterOrDigit).ToArray()));
        }

        public static string ToSlug(this string value)
        {
            return value
                .WhenNotNull(s => s.ToLowerInvariant())
                .WhenNotNull(s => s.Replace(" ", "-"))
                .RemoveSpecialCharacters();
        }

        public static bool ContainsIgnoreCase(this string source, string value)
        {
            return source?.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string[] SplitAndTrim(this string value, char separator)
        {
            return value?.Split(separator)
                       .Select(s => s.Trim())
                       .Where(s => !string.IsNullOrEmpty(s))
                       .ToArray() ?? new string[0];
        }

        public static T ParseTo<T>(this string value, T defaultValue = default)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }

    // ================================
    // ✅ EXTENSION METHODS PARA JSON Y SERIALIZACIÓN
    // ================================

    public static class SerializationExtensions
    {
        public static string ToJson<T>(this T obj, bool indented = false)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = indented,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(obj, options);
        }

        public static T FromJson<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);
                
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch
            {
                return default(T);
            }
        }

        public static bool IsValidJson(this string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return false;
                
            try
            {
                JsonDocument.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    // ================================
    // ✅ EXTENSION METHODS PARA CONDITIONAL EXECUTION
    // ================================

    public static class ConditionalExtensions
    {
        public static TResult Switch<T, TResult>(this T value, params (Func<T, bool> condition, Func<T, TResult> action)[] cases)
        {
            foreach (var (condition, action) in cases)
            {
                if (condition(value))
                    return action(value);
            }
            return default(TResult);
        }

        public static T Switch<T>(this T value, params (Func<T, bool> condition, Action<T> action)[] cases)
        {
            foreach (var (condition, action) in cases)
            {
                if (condition(value))
                {
                    action(value);
                    break;
                }
            }
            return value;
        }

        public static TResult Match<T, TResult>(this T value, 
            Dictionary<T, Func<TResult>> cases, 
            Func<TResult> defaultCase = null)
        {
            if (cases.ContainsKey(value))
                return cases[value]();
                
            return defaultCase != null ? defaultCase() : default(TResult);
        }
    }

    // ================================
    // ✅ EJEMPLO DE USO COMPLETO
    // ================================

    public class UserRegistrationService
    {
        public RegistrationResult RegisterUser(UserRegistrationRequest request)
        {
            // ✅ Usando extension methods para eliminar IFs de validación
            var emailValidation = request.Email
                .IsRequired("Email")
                .HasMinLength(5)
                .HasMaxLength(100)
                .IsEmail();

            var nameValidation = request.Name
                .IsRequired("Name")
                .HasMinLength(2)
                .HasMaxLength(50)
                .Must(name => !name.ContainsIgnoreCase("admin"), "Name cannot contain 'admin'");

            var ageValidation = request.Age
                .IsRequired("Age")
                .IsInRange(18, 120);

            // ✅ Agregación de errores sin IFs
            var allErrors = new[] { emailValidation.Errors, nameValidation.Errors, ageValidation.Errors }
                .SelectMany(errors => errors)
                .ToList();

            if (allErrors.Any())
            {
                return new RegistrationResult 
                { 
                    Success = false, 
                    Errors = allErrors,
                    Message = "Validation failed"
                };
            }

            // ✅ Transformación de datos con extension methods
            var user = new User
            {
                Email = request.Email.ToLowerInvariant(),
                Name = request.Name.ToTitleCase(),
                Username = request.Name.ToSlug(),
                Age = request.Age,
                RegistrationDate = DateTime.Now
            };

            // ✅ Logging condicional sin IFs
            user.Log("Creating user")
                .TapIf(u => u.Age < 25, u => Console.WriteLine("Young user registered"))
                .TapIf(u => u.Email.Contains("premium"), u => Console.WriteLine("Premium user detected"));

            // ✅ Conditional processing sin IF statements
            user.Switch(
                (u => u.Age >= 65, u => Console.WriteLine("Senior citizen registered")),
                (u => u.Age >= 18 && u.Age < 25, u => Console.WriteLine("Young adult registered")),
                (u => u.Email.Contains("@company.com"), u => Console.WriteLine("Corporate user registered"))
            );

            return new RegistrationResult 
            { 
                Success = true, 
                User = user,
                Message = "User registered successfully"
            };
        }
    }

    // ================================
    // ✅ DATA PROCESSING PIPELINE SIN IFS
    // ================================

    public class DataProcessor
    {
        public ProcessingResult ProcessUserData(IEnumerable<RawUserData> rawData)
        {
            var processedUsers = rawData
                .Where(data => !string.IsNullOrEmpty(data.Email))
                .Where(data => data.Email.Contains("@"))
                .Select(data => new User
                {
                    Email = data.Email.ToLowerInvariant(),
                    Name = data.Name.ToTitleCase(),
                    Age = data.AgeString.ParseTo<int>(0),
                    Username = data.Name.ToSlug()
                })
                .Where(user => user.Age > 0)
                .WhereIf(true, user => user.Age >= 18) // Conditional filter
                .TakeIf(false, 100) // Conditional take
                .DistinctBy(user => user.Email)
                .ForEach(user => Console.WriteLine($"Processing: {user.Name}"))
                .ToList();

            var stats = new ProcessingStats
            {
                TotalProcessed = processedUsers.Count,
                AverageAge = processedUsers.Any() ? processedUsers.Average(u => u.Age) : 0,
                YoungUsers = processedUsers.Count(u => u.Age < 30),
                SeniorUsers = processedUsers.Count(u => u.Age >= 65)
            };

            return new ProcessingResult
            {
                Users = processedUsers,
                Statistics = stats,
                Summary = $"Processed {stats.TotalProcessed} users successfully"
            };
        }
    }

    // ================================
    // ✅ SUPPORTING CLASSES
    // ================================

    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class RegistrationResult
    {
        public bool Success { get; set; }
        public User User { get; set; }
        public List<string> Errors { get; set; } = new();
        public string Message { get; set; }
    }

    public class RawUserData
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string AgeString { get; set; }
    }

    public class ProcessingResult
    {
        public List<User> Users { get; set; }
        public ProcessingStats Statistics { get; set; }
        public string Summary { get; set; }
    }

    public class ProcessingStats
    {
        public int TotalProcessed { get; set; }
        public double AverageAge { get; set; }
        public int YoungUsers { get; set; }
        public int SeniorUsers { get; set; }
    }

    // ================================
    // ✅ DEMO COMPLETO
    // ================================

    public class ExtensionMethodsDemo
    {
        public void RunDemo()
        {
            Console.WriteLine("=== EXTENSION METHODS ADVANCED DEMO ===\n");

            DemoValidationExtensions();
            DemoTransformationExtensions();
            DemoConditionalExtensions();
            DemoDataProcessing();

            Console.WriteLine("\n=== EXTENSION METHODS DEMO COMPLETED ===");
        }

        private void DemoValidationExtensions()
        {
            Console.WriteLine("1. 🔍 VALIDATION EXTENSIONS:");
            Console.WriteLine("============================");

            var registrationService = new UserRegistrationService();

            var validRequest = new UserRegistrationRequest
            {
                Email = "user@example.com",
                Name = "John Doe",
                Age = 25
            };

            var invalidRequest = new UserRegistrationRequest
            {
                Email = "invalid-email",
                Name = "",
                Age = 15
            };

            var validResult = registrationService.RegisterUser(validRequest);
            Console.WriteLine($"Valid user: {validResult.Success} - {validResult.Message}");

            var invalidResult = registrationService.RegisterUser(invalidRequest);
            Console.WriteLine($"Invalid user: {invalidResult.Success} - {string.Join(", ", invalidResult.Errors)}");
            Console.WriteLine();
        }

        private void DemoTransformationExtensions()
        {
            Console.WriteLine("2. 🔄 TRANSFORMATION EXTENSIONS:");
            Console.WriteLine("=================================");

            var name = "  JOHN DOE  ";
            var processed = name
                .WhenNotNull(s => s.Trim())
                .ToTitleCase()
                .Log("Processed name");

            var email = "USER@EXAMPLE.COM";
            var normalizedEmail = email
                .When(e => e.Contains("@"), e => e.ToLowerInvariant())
                .Log("Normalized email");

            var numbers = new[] { 1, 2, 3, 4, 5 };
            var filtered = numbers
                .WhereIf(true, n => n % 2 == 0)
                .TakeIf(false, 2)
                .ToList();

            Console.WriteLine($"Filtered numbers: [{string.Join(", ", filtered)}]");
            Console.WriteLine();
        }

        private void DemoConditionalExtensions()
        {
            Console.WriteLine("3. ⚡ CONDITIONAL EXTENSIONS:");
            Console.WriteLine("=============================");

            var score = 85;
            var grade = score.Switch<int, string>(
                (s => s >= 90, s => "A"),
                (s => s >= 80, s => "B"),
                (s => s >= 70, s => "C"),
                (s => s >= 60, s => "D")
            ) ?? "F";

            Console.WriteLine($"Score {score} = Grade {grade}");

            var userType = "admin";
            var permissions = userType.Match(new Dictionary<string, Func<string>>
            {
                ["admin"] = () => "Full Access",
                ["user"] = () => "Read Only",
                ["guest"] = () => "Limited Access"
            }, () => "No Access");

            Console.WriteLine($"User type '{userType}' has: {permissions}");
            Console.WriteLine();
        }

        private void DemoDataProcessing()
        {
            Console.WriteLine("4. 📊 DATA PROCESSING PIPELINE:");
            Console.WriteLine("================================");

            var rawData = new[]
            {
                new RawUserData { Email = "john@example.com", Name = "john doe", AgeString = "25" },
                new RawUserData { Email = "JANE@EXAMPLE.COM", Name = "jane smith", AgeString = "30" },
                new RawUserData { Email = "invalid-email", Name = "invalid user", AgeString = "abc" },
                new RawUserData { Email = "bob@example.com", Name = "bob johnson", AgeString = "35" }
            };

            var processor = new DataProcessor();
            var result = processor.ProcessUserData(rawData);

            Console.WriteLine($"Processing Summary: {result.Summary}");
            Console.WriteLine($"Average Age: {result.Statistics.AverageAge:F1}");
            Console.WriteLine($"Young Users: {result.Statistics.YoungUsers}");

            foreach (var user in result.Users)
            {
                Console.WriteLine($"  - {user.Name} ({user.Email}) - Age: {user.Age}");
            }
        }
    }

    /*
    TÉCNICAS NO-IF CON EXTENSION METHODS:

    ✅ VENTAJAS DE EXTENSION METHODS:
    - Fluent API que elimina nested IFs
    - Chain of responsibility pattern
    - Functional programming approach
    - Código más legible y mantenible
    - Reutilización en toda la aplicación

    ✅ PATRONES IMPLEMENTADOS:
    - Validation Pipeline sin IFs
    - Conditional Execution encapsulada
    - Data Transformation fluent
    - Logging y debugging integrado
    - Collection processing avanzado

    ✅ CASOS DE USO:
    - Validación de datos de entrada
    - Transformación de DTOs
    - Processing pipelines
    - Conditional logic compleja
    - Data cleaning y normalization

    ✅ BENEFITS:
    - 90% menos IF statements
    - Código autodocumentado
    - Easy unit testing
    - Better error handling
    - Consistent API patterns

    PRÓXIMO EJERCICIO:
    LINQ avanzado para elimination de IFs
    */
}