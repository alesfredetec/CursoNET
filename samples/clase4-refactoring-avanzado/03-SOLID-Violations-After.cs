using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringAvanzado.Ejercicios
{
    /// <summary>
    /// SOLUCIÓN: Aplicación correcta de principios SOLID
    /// 
    /// PRINCIPIOS APLICADOS:
    /// ✅ S - Single Responsibility: Cada clase una responsabilidad
    /// ✅ O - Open/Closed: Extensible sin modificar código existente
    /// ✅ L - Liskov Substitution: Interfaces bien definidas
    /// ✅ I - Interface Segregation: Interfaces específicas y pequeñas
    /// ✅ D - Dependency Inversion: Depende de abstracciones
    /// </summary>

    // ✅ S - Single Responsibility: Interfaces específicas
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }

    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }

    public interface IUserValidator
    {
        ValidationResult Validate(User user);
    }

    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public interface IUserNotificationService
    {
        Task SendWelcomeEmailAsync(User user);
        Task SendPasswordResetEmailAsync(User user, string resetToken);
    }

    // ✅ L - Liskov Substitution: Base abstraction for notifications
    public interface INotificationStrategy
    {
        Task SendAsync(string recipient, string subject, string content);
        bool CanHandle(NotificationType type);
    }

    public enum NotificationType
    {
        Email,
        SMS,
        Push,
        InApp
    }

    // ✅ O - Open/Closed: Extensible notification strategies
    public class EmailNotificationStrategy : INotificationStrategy
    {
        private readonly IEmailService _emailService;

        public EmailNotificationStrategy(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendAsync(string recipient, string subject, string content)
        {
            await _emailService.SendAsync(recipient, subject, content);
        }

        public bool CanHandle(NotificationType type) => type == NotificationType.Email;
    }

    public class SmsNotificationStrategy : INotificationStrategy
    {
        public async Task SendAsync(string recipient, string subject, string content)
        {
            // SMS implementation
            await Task.Delay(100); // Simulate SMS sending
            Console.WriteLine($"SMS sent to {recipient}: {content}");
        }

        public bool CanHandle(NotificationType type) => type == NotificationType.SMS;
    }

    public class PushNotificationStrategy : INotificationStrategy
    {
        public async Task SendAsync(string recipient, string subject, string content)
        {
            // Push notification implementation
            await Task.Delay(50);
            Console.WriteLine($"Push sent to {recipient}: {subject}");
        }

        public bool CanHandle(NotificationType type) => type == NotificationType.Push;
    }

    // ✅ D - Dependency Inversion: Depends on abstractions
    public class NotificationService
    {
        private readonly IEnumerable<INotificationStrategy> _strategies;

        public NotificationService(IEnumerable<INotificationStrategy> strategies)
        {
            _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
        }

        public async Task SendNotificationAsync(
            NotificationType type, 
            string recipient, 
            string subject, 
            string content)
        {
            var strategy = _strategies.FirstOrDefault(s => s.CanHandle(type));
            
            if (strategy == null)
                throw new NotSupportedException($"Notification type {type} not supported");

            await strategy.SendAsync(recipient, subject, content);
        }
    }

    // ✅ S - Single Responsibility: User validation only
    public class UserValidator : IUserValidator
    {
        public ValidationResult Validate(User user)
        {
            var result = new ValidationResult();

            if (string.IsNullOrWhiteSpace(user.Name))
                result.AddError("Name", "Name is required");

            if (string.IsNullOrWhiteSpace(user.Email))
                result.AddError("Email", "Email is required");
            else if (!IsValidEmail(user.Email))
                result.AddError("Email", "Email format is invalid");

            if (string.IsNullOrWhiteSpace(user.Password))
                result.AddError("Password", "Password is required");
            else if (!IsValidPassword(user.Password))
                result.AddError("Password", "Password must be at least 8 characters with numbers and letters");

            return result;
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 8 && 
                   password.Any(char.IsDigit) && 
                   password.Any(char.IsLetter);
        }
    }

    // ✅ S - Single Responsibility: Password operations only
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Simplified hashing - use BCrypt in production
            return Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(password + "salt"));
        }

        public bool VerifyPassword(string password, string hash)
        {
            var computedHash = HashPassword(password);
            return computedHash == hash;
        }
    }

    // ✅ S - Single Responsibility: User notifications only
    public class UserNotificationService : IUserNotificationService
    {
        private readonly NotificationService _notificationService;

        public UserNotificationService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task SendWelcomeEmailAsync(User user)
        {
            var subject = "Welcome to our platform!";
            var body = $"Hello {user.Name},\n\nWelcome to our platform. Your account has been created successfully.";
            
            await _notificationService.SendNotificationAsync(
                NotificationType.Email, 
                user.Email, 
                subject, 
                body);
        }

        public async Task SendPasswordResetEmailAsync(User user, string resetToken)
        {
            var subject = "Password Reset Request";
            var body = $"Hello {user.Name},\n\nClick here to reset your password: /reset?token={resetToken}";
            
            await _notificationService.SendNotificationAsync(
                NotificationType.Email, 
                user.Email, 
                subject, 
                body);
        }
    }

    // ✅ S - Single Responsibility: User business operations
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _validator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserNotificationService _notificationService;

        public UserService(
            IUserRepository userRepository,
            IUserValidator validator,
            IPasswordHasher passwordHasher,
            IUserNotificationService notificationService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public async Task<Result<User>> CreateUserAsync(CreateUserCommand command)
        {
            var user = new User
            {
                Name = command.Name,
                Email = command.Email,
                Password = command.Password
            };

            // Validate user
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid)
            {
                return Result<User>.Failure(validationResult.GetErrorMessage());
            }

            // Hash password
            user.Password = _passwordHasher.HashPassword(user.Password);

            try
            {
                // Create user
                var createdUser = await _userRepository.CreateAsync(user);
                
                // Send welcome email
                await _notificationService.SendWelcomeEmailAsync(createdUser);
                
                return Result<User>.Success(createdUser);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Failed to create user: {ex.Message}");
            }
        }

        public async Task<Result> ResetPasswordAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                {
                    return Result.Failure("User not found");
                }

                var resetToken = Guid.NewGuid().ToString();
                user.PasswordResetToken = resetToken;
                user.PasswordResetExpires = DateTime.UtcNow.AddHours(1);

                await _userRepository.UpdateAsync(user);
                await _notificationService.SendPasswordResetEmailAsync(user, resetToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to reset password: {ex.Message}");
            }
        }
    }

    // ✅ I - Interface Segregation: Specific repository interface
    public interface IUserRepositoryExtended : IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<bool> EmailExistsAsync(string email);
    }

    // ✅ L - Liskov Substitution: Concrete implementation
    public class UserRepository : IUserRepositoryExtended
    {
        private readonly IDbContext _context;

        public UserRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            user.CreatedDate = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            user.ModifiedDate = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _context.Users.Where(u => u.IsActive).ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }

    // ✅ O - Open/Closed: Extensible validation rules
    public interface IValidationRule<T>
    {
        ValidationResult Validate(T item);
    }

    public class EmailUniquenessValidationRule : IValidationRule<User>
    {
        private readonly IUserRepositoryExtended _userRepository;

        public EmailUniquenessValidationRule(IUserRepositoryExtended userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validate(User user)
        {
            var result = new ValidationResult();
            
            // Note: In real implementation, this should be async
            // This is simplified for demonstration
            var emailExists = _userRepository.EmailExistsAsync(user.Email).Result;
            
            if (emailExists)
            {
                result.AddError("Email", "Email already exists");
            }

            return result;
        }
    }

    public class CompositeUserValidator : IUserValidator
    {
        private readonly IEnumerable<IValidationRule<User>> _rules;

        public CompositeUserValidator(IEnumerable<IValidationRule<User>> rules)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public ValidationResult Validate(User user)
        {
            var result = new ValidationResult();

            foreach (var rule in _rules)
            {
                var ruleResult = rule.Validate(user);
                result.Merge(ruleResult);
            }

            return result;
        }
    }

    // ✅ Supporting classes
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetExpires { get; set; }
    }

    public class CreateUserCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ValidationResult
    {
        public Dictionary<string, List<string>> Errors { get; } = new();
        public bool IsValid => Errors.Count == 0;

        public void AddError(string field, string message)
        {
            if (!Errors.ContainsKey(field))
                Errors[field] = new List<string>();
            
            Errors[field].Add(message);
        }

        public void Merge(ValidationResult other)
        {
            foreach (var kvp in other.Errors)
            {
                if (!Errors.ContainsKey(kvp.Key))
                    Errors[kvp.Key] = new List<string>();
                
                Errors[kvp.Key].AddRange(kvp.Value);
            }
        }

        public string GetErrorMessage()
        {
            var messages = new List<string>();
            foreach (var kvp in Errors)
            {
                messages.AddRange(kvp.Value);
            }
            return string.Join("; ", messages);
        }
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        protected Result(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(string errorMessage) => new(false, errorMessage);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        private Result(bool isSuccess, T value, string errorMessage) : base(isSuccess, errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
    }

    // ✅ Dependency Injection setup example
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // ✅ Register interfaces with implementations
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepositoryExtended, UserRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            
            // ✅ Register notification strategies
            services.AddScoped<INotificationStrategy, EmailNotificationStrategy>();
            services.AddScoped<INotificationStrategy, SmsNotificationStrategy>();
            services.AddScoped<INotificationStrategy, PushNotificationStrategy>();
            services.AddScoped<NotificationService>();
            
            // ✅ Register validation rules
            services.AddScoped<IValidationRule<User>, EmailUniquenessValidationRule>();
            services.AddScoped<IUserValidator, CompositeUserValidator>();
            
            // ✅ Register main service
            services.AddScoped<UserService>();
        }
    }

    // ✅ Demo de uso
    public class SOLIDDemo
    {
        public async Task DemonstrateSOLIDPrinciples()
        {
            // Setup dependencies (normally done by DI container)
            var dbContext = new MockDbContext();
            var userRepository = new UserRepository(dbContext);
            var emailService = new MockEmailService();
            var passwordHasher = new PasswordHasher();
            
            var notificationStrategies = new List<INotificationStrategy>
            {
                new EmailNotificationStrategy(emailService),
                new SmsNotificationStrategy(),
                new PushNotificationStrategy()
            };
            
            var notificationService = new NotificationService(notificationStrategies);
            var userNotificationService = new UserNotificationService(notificationService);
            
            var validationRules = new List<IValidationRule<User>>
            {
                new EmailUniquenessValidationRule(userRepository)
            };
            
            var validator = new CompositeUserValidator(validationRules);
            
            var userService = new UserService(
                userRepository, 
                validator, 
                passwordHasher, 
                userNotificationService);

            // ✅ Create user using SOLID-compliant service
            var command = new CreateUserCommand
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "password123"
            };

            var result = await userService.CreateUserAsync(command);
            
            if (result.IsSuccess)
            {
                Console.WriteLine($"User created successfully: {result.Value.Name}");
                
                // ✅ Demonstrate extensibility - add new notification
                await notificationService.SendNotificationAsync(
                    NotificationType.SMS, 
                    "+1234567890", 
                    "Welcome", 
                    "Welcome via SMS!");
            }
            else
            {
                Console.WriteLine($"Failed to create user: {result.ErrorMessage}");
            }
        }
    }

    /*
    PRINCIPIOS SOLID APLICADOS:

    ✅ SINGLE RESPONSIBILITY PRINCIPLE (SRP):
    - UserValidator: Solo validación
    - PasswordHasher: Solo operaciones de password
    - UserNotificationService: Solo notificaciones de usuario
    - UserRepository: Solo operaciones de datos
    - UserService: Solo lógica de negocio de usuario

    ✅ OPEN/CLOSED PRINCIPLE (OCP):
    - INotificationStrategy: Extensible para nuevos tipos
    - IValidationRule<T>: Extensible para nuevas reglas
    - CompositeUserValidator: Acepta nuevas reglas sin modificación
    - NotificationService: Acepta nuevas strategies sin modificación

    ✅ LISKOV SUBSTITUTION PRINCIPLE (LSP):
    - Todas las implementaciones de INotificationStrategy son intercambiables
    - UserRepository implementa completamente IUserRepository
    - Cualquier IValidationRule<User> funciona en CompositeUserValidator

    ✅ INTERFACE SEGREGATION PRINCIPLE (ISP):
    - IUserRepository: Solo operaciones básicas de usuario
    - IEmailService: Solo envío de emails
    - IUserValidator: Solo validación
    - IPasswordHasher: Solo operaciones de password
    - Interfaces pequeñas y específicas

    ✅ DEPENDENCY INVERSION PRINCIPLE (DIP):
    - UserService depende de abstracciones (interfaces)
    - No depende de implementaciones concretas
    - Todas las dependencias inyectadas
    - Fácil testing con mocks

    BENEFICIOS OBTENIDOS:

    ✅ TESTABILIDAD:
    - Cada componente se puede testear independientemente
    - Fácil mocking de dependencias
    - Unit tests específicos por responsabilidad

    ✅ MANTENIBILIDAD:
    - Cambios aislados por responsabilidad
    - Código auto-documentado
    - Fácil localización de bugs

    ✅ EXTENSIBILIDAD:
    - Nuevas strategies sin modificar código
    - Nuevas reglas de validación sin cambios
    - Nuevos tipos de notificación fáciles

    ✅ FLEXIBILITY:
    - Dependency injection permite configuración
    - Intercambio de implementaciones en runtime
    - A/B testing de diferentes strategies

    COMPARACIÓN:

    ANTES (SOLID Violations):
    ❌ Una clase: 200+ líneas
    ❌ Múltiples responsabilidades mezcladas
    ❌ Hard dependencies
    ❌ No extensible
    ❌ Difícil testing

    DESPUÉS (SOLID Compliant):
    ✅ Múltiples clases: 20-50 líneas cada una
    ✅ Una responsabilidad por clase
    ✅ Dependency injection
    ✅ Fácilmente extensible
    ✅ 100% testeable

    PRÓXIMO EJERCICIO:
    Design Patterns completos - Factory, Builder, Observer patterns
    */

    // Mock implementations for demo
    public interface IDbContext
    {
        IQueryable<User> Users { get; }
        Task<int> SaveChangesAsync();
    }

    public class MockDbContext : IDbContext
    {
        private readonly List<User> _users = new();
        public IQueryable<User> Users => _users.AsQueryable();
        public Task<int> SaveChangesAsync() => Task.FromResult(1);
    }

    public class MockEmailService : IEmailService
    {
        public Task SendAsync(string to, string subject, string body)
        {
            Console.WriteLine($"Email sent to {to}: {subject}");
            return Task.CompletedTask;
        }
    }

    public interface IServiceCollection
    {
        void AddScoped<TInterface, TImplementation>() 
            where TImplementation : class, TInterface;
        void AddScoped<T>() where T : class;
    }
}