using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactoringAvanzado.Ejercicios
{
    /// <summary>
    /// SOLUCIÓN: Implementación correcta de Design Patterns
    /// 
    /// PATTERNS APLICADOS:
    /// ✅ Factory Pattern: Creación flexible de objetos
    /// ✅ Observer Pattern: Notificaciones desacopladas
    /// ✅ Command Pattern: Operaciones encapsuladas con undo
    /// ✅ Builder Pattern: Configuración compleja simplificada
    /// ✅ Template Method: Workflows reutilizables
    /// ✅ Strategy Pattern: Algoritmos intercambiables
    /// </summary>

    // ================================
    // ✅ FACTORY PATTERN - Creación flexible
    // ================================
    
    public interface IReport
    {
        void Generate();
        string GetOutput();
        DateTime CreatedDate { get; }
    }

    public interface IReportFactory
    {
        IReport CreateReport(string reportType, Dictionary<string, object> data);
        IEnumerable<string> GetSupportedTypes();
    }

    public class PdfReport : IReport
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; private set; }
        public string Watermark { get; set; }
        public int FontSize { get; set; }
        public string Orientation { get; set; }
        private string _output;

        public void Generate()
        {
            CreatedDate = DateTime.Now;
            _output = $"PDF Report: {Title} by {Author} ({Orientation}, {FontSize}pt)\nContent: {Content}\nWatermark: {Watermark}";
        }

        public string GetOutput() => _output;
    }

    public class ExcelReport : IReport
    {
        public string Title { get; set; }
        public List<Dictionary<string, object>> Data { get; set; }
        public string SheetName { get; set; }
        public bool IncludeCharts { get; set; }
        public bool AutoFitColumns { get; set; }
        public DateTime CreatedDate { get; private set; }
        private string _output;

        public void Generate()
        {
            CreatedDate = DateTime.Now;
            _output = $"Excel Report: {Title}\nSheet: {SheetName}\nRows: {Data?.Count ?? 0}\nCharts: {IncludeCharts}\nAutoFit: {AutoFitColumns}";
        }

        public string GetOutput() => _output;
    }

    public class JsonReport : IReport
    {
        public Dictionary<string, object> Data { get; set; }
        public bool Formatted { get; set; }
        public bool IncludeMetadata { get; set; }
        public DateTime CreatedDate { get; private set; }
        private string _output;

        public void Generate()
        {
            CreatedDate = DateTime.Now;
            _output = $"JSON Report: {Data?.Count ?? 0} properties\nFormatted: {Formatted}\nMetadata: {IncludeMetadata}";
        }

        public string GetOutput() => _output;
    }

    // ✅ Factory implementations - extensible design
    public abstract class ReportFactory : IReportFactory
    {
        protected readonly Dictionary<string, Func<Dictionary<string, object>, IReport>> _reportCreators;

        protected ReportFactory()
        {
            _reportCreators = new Dictionary<string, Func<Dictionary<string, object>, IReport>>();
            RegisterReportTypes();
        }

        protected abstract void RegisterReportTypes();

        public virtual IReport CreateReport(string reportType, Dictionary<string, object> data)
        {
            if (!_reportCreators.ContainsKey(reportType.ToUpper()))
                throw new ArgumentException($"Report type {reportType} not supported");

            return _reportCreators[reportType.ToUpper()](data);
        }

        public IEnumerable<string> GetSupportedTypes() => _reportCreators.Keys;
    }

    public class StandardReportFactory : ReportFactory
    {
        protected override void RegisterReportTypes()
        {
            _reportCreators["PDF"] = data => new PdfReport
            {
                Title = data.GetValueOrDefault("title", "Untitled").ToString(),
                Content = data.GetValueOrDefault("content", "").ToString(),
                Author = data.GetValueOrDefault("author", "Unknown").ToString(),
                Watermark = "CONFIDENTIAL",
                FontSize = 12,
                Orientation = "Portrait"
            };

            _reportCreators["EXCEL"] = data => new ExcelReport
            {
                Title = data.GetValueOrDefault("title", "Untitled").ToString(),
                Data = data.GetValueOrDefault("data", new List<Dictionary<string, object>>()) as List<Dictionary<string, object>>,
                SheetName = data.GetValueOrDefault("sheetName", "Sheet1").ToString(),
                IncludeCharts = true,
                AutoFitColumns = true
            };

            _reportCreators["JSON"] = data => new JsonReport
            {
                Data = data,
                Formatted = true,
                IncludeMetadata = true
            };
        }
    }

    // ✅ Advanced factory with validation and caching
    public class AdvancedReportFactory : StandardReportFactory
    {
        private readonly Dictionary<string, IReport> _cache = new();
        private readonly IReportValidator _validator;

        public AdvancedReportFactory(IReportValidator validator = null)
        {
            _validator = validator ?? new DefaultReportValidator();
        }

        public override IReport CreateReport(string reportType, Dictionary<string, object> data)
        {
            // Validate input
            var validationResult = _validator.Validate(reportType, data);
            if (!validationResult.IsValid)
                throw new ArgumentException($"Invalid report data: {validationResult.ErrorMessage}");

            // Check cache
            var cacheKey = $"{reportType}_{GetDataHash(data)}";
            if (_cache.ContainsKey(cacheKey))
                return _cache[cacheKey];

            // Create and cache
            var report = base.CreateReport(reportType, data);
            _cache[cacheKey] = report;
            
            return report;
        }

        private string GetDataHash(Dictionary<string, object> data)
        {
            return data.GetHashCode().ToString();
        }
    }

    public interface IReportValidator
    {
        ValidationResult Validate(string reportType, Dictionary<string, object> data);
    }

    public class DefaultReportValidator : IReportValidator
    {
        public ValidationResult Validate(string reportType, Dictionary<string, object> data)
        {
            var result = new ValidationResult();
            
            if (string.IsNullOrEmpty(reportType))
                result.AddError("reportType", "Report type is required");
                
            if (data == null)
                result.AddError("data", "Data is required");
            else if (!data.ContainsKey("title"))
                result.AddError("title", "Title is required");
                
            return result;
        }
    }

    // ================================
    // ✅ OBSERVER PATTERN - Notificaciones desacopladas
    // ================================

    public interface IOrderObserver
    {
        Task HandleOrderProcessedAsync(Order order);
        string Name { get; }
        int Priority { get; } // For ordering notifications
    }

    public interface IOrderSubject
    {
        void Attach(IOrderObserver observer);
        void Detach(IOrderObserver observer);
        Task NotifyAsync(Order order);
    }

    public class OrderProcessor : IOrderSubject
    {
        private readonly List<IOrderObserver> _observers = new();
        private readonly object _lock = new object();

        public void Attach(IOrderObserver observer)
        {
            lock (_lock)
            {
                if (!_observers.Contains(observer))
                {
                    _observers.Add(observer);
                    _observers.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                }
            }
        }

        public void Detach(IOrderObserver observer)
        {
            lock (_lock)
            {
                _observers.Remove(observer);
            }
        }

        public async Task NotifyAsync(Order order)
        {
            var observers = _observers.ToList(); // Thread-safe copy
            
            var tasks = observers.Select(async observer =>
            {
                try
                {
                    await observer.HandleOrderProcessedAsync(order);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Observer {observer.Name} failed: {ex.Message}");
                    // Log but don't break other observers
                }
            });

            await Task.WhenAll(tasks);
        }

        public async Task ProcessOrderAsync(Order order)
        {
            // Process order
            order.Status = "Processing";
            order.ProcessedDate = DateTime.Now;
            
            Console.WriteLine($"Processing order {order.Id}...");
            await Task.Delay(100); // Simulate processing
            
            order.Status = "Completed";
            
            // Notify all observers
            await NotifyAsync(order);
        }
    }

    // ✅ Concrete observers - easy to add new ones
    public class CustomerEmailNotificationObserver : IOrderObserver
    {
        public string Name => "Customer Email Notification";
        public int Priority => 1; // High priority

        public async Task HandleOrderProcessedAsync(Order order)
        {
            await Task.Delay(50); // Simulate email sending
            Console.WriteLine($"✅ Email sent to customer for order {order.Id}");
        }
    }

    public class WarehouseNotificationObserver : IOrderObserver
    {
        public string Name => "Warehouse Notification";
        public int Priority => 2;

        public async Task HandleOrderProcessedAsync(Order order)
        {
            await Task.Delay(30);
            Console.WriteLine($"✅ Warehouse notified for order {order.Id}");
        }
    }

    public class InventoryUpdateObserver : IOrderObserver
    {
        public string Name => "Inventory Update";
        public int Priority => 3;

        public async Task HandleOrderProcessedAsync(Order order)
        {
            await Task.Delay(40);
            Console.WriteLine($"✅ Inventory updated for order {order.Id}");
        }
    }

    public class AnalyticsObserver : IOrderObserver
    {
        public string Name => "Analytics";
        public int Priority => 10; // Low priority

        public async Task HandleOrderProcessedAsync(Order order)
        {
            await Task.Delay(20);
            Console.WriteLine($"✅ Analytics updated for order {order.Id}");
        }
    }

    // ================================
    // ✅ COMMAND PATTERN - Operaciones encapsuladas
    // ================================

    public interface ICommand
    {
        Task<CommandResult> ExecuteAsync();
        Task<CommandResult> UndoAsync();
        string Description { get; }
        DateTime ExecutedAt { get; }
        bool CanUndo { get; }
    }

    public class CommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public static CommandResult Successful(string message = "Command executed successfully") 
            => new CommandResult { Success = true, Message = message };
        
        public static CommandResult Failed(string message, Exception ex = null) 
            => new CommandResult { Success = false, Message = message, Exception = ex };
    }

    public abstract class DatabaseCommand : ICommand
    {
        protected readonly string ConnectionString;
        protected string _undoSql;
        
        public string Description { get; protected set; }
        public DateTime ExecutedAt { get; private set; }
        public bool CanUndo { get; protected set; } = true;

        protected DatabaseCommand(string connectionString, string description)
        {
            ConnectionString = connectionString;
            Description = description;
        }

        public async Task<CommandResult> ExecuteAsync()
        {
            try
            {
                ExecutedAt = DateTime.Now;
                var result = await ExecuteInternalAsync();
                
                if (result.Success)
                    Console.WriteLine($"✅ Executed: {Description}");
                else
                    Console.WriteLine($"❌ Failed: {Description} - {result.Message}");
                    
                return result;
            }
            catch (Exception ex)
            {
                return CommandResult.Failed($"Command execution failed: {ex.Message}", ex);
            }
        }

        public async Task<CommandResult> UndoAsync()
        {
            if (!CanUndo)
                return CommandResult.Failed("Command cannot be undone");

            try
            {
                var result = await UndoInternalAsync();
                
                if (result.Success)
                    Console.WriteLine($"↩️ Undone: {Description}");
                else
                    Console.WriteLine($"❌ Undo failed: {Description} - {result.Message}");
                    
                return result;
            }
            catch (Exception ex)
            {
                return CommandResult.Failed($"Undo failed: {ex.Message}", ex);
            }
        }

        protected abstract Task<CommandResult> ExecuteInternalAsync();
        protected abstract Task<CommandResult> UndoInternalAsync();
    }

    public class CreateCommand : DatabaseCommand
    {
        private readonly string _sql;
        private readonly Dictionary<string, object> _parameters;
        private int _insertedId;

        public CreateCommand(string connectionString, string sql, Dictionary<string, object> parameters = null)
            : base(connectionString, $"CREATE: {sql.Substring(0, Math.Min(50, sql.Length))}...")
        {
            _sql = sql;
            _parameters = parameters ?? new Dictionary<string, object>();
        }

        protected override async Task<CommandResult> ExecuteInternalAsync()
        {
            await Task.Delay(10); // Simulate database operation
            
            // Simulate getting inserted ID
            _insertedId = new Random().Next(1000, 9999);
            
            // Generate undo SQL
            if (_sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
            {
                _undoSql = $"DELETE FROM table WHERE ID = {_insertedId}";
            }
            
            Console.WriteLine($"Executing CREATE: {_sql}");
            return CommandResult.Successful($"Record created with ID: {_insertedId}");
        }

        protected override async Task<CommandResult> UndoInternalAsync()
        {
            await Task.Delay(10);
            Console.WriteLine($"Executing UNDO: {_undoSql}");
            return CommandResult.Successful($"Record {_insertedId} deleted");
        }
    }

    public class UpdateCommand : DatabaseCommand
    {
        private readonly string _sql;
        private readonly Dictionary<string, object> _parameters;
        private readonly Dictionary<string, object> _oldValues;

        public UpdateCommand(string connectionString, string sql, Dictionary<string, object> parameters = null, Dictionary<string, object> oldValues = null)
            : base(connectionString, $"UPDATE: {sql.Substring(0, Math.Min(50, sql.Length))}...")
        {
            _sql = sql;
            _parameters = parameters ?? new Dictionary<string, object>();
            _oldValues = oldValues ?? new Dictionary<string, object>();
        }

        protected override async Task<CommandResult> ExecuteInternalAsync()
        {
            await Task.Delay(10);
            
            // Generate undo SQL from old values
            if (_oldValues.Any())
            {
                var setClause = string.Join(", ", _oldValues.Select(kv => $"{kv.Key} = '{kv.Value}'"));
                _undoSql = $"UPDATE table SET {setClause} WHERE ID = {_parameters.GetValueOrDefault("ID", 0)}";
            }
            
            Console.WriteLine($"Executing UPDATE: {_sql}");
            return CommandResult.Successful("Record updated successfully");
        }

        protected override async Task<CommandResult> UndoInternalAsync()
        {
            await Task.Delay(10);
            Console.WriteLine($"Executing UNDO: {_undoSql}");
            return CommandResult.Successful("Record restored to previous values");
        }
    }

    // ✅ Command Manager with transaction support
    public class CommandManager
    {
        private readonly Stack<ICommand> _executedCommands = new();
        private readonly List<ICommand> _currentTransaction = new();
        private bool _inTransaction = false;

        public void BeginTransaction()
        {
            _inTransaction = true;
            _currentTransaction.Clear();
        }

        public async Task<CommandResult> CommitTransactionAsync()
        {
            if (!_inTransaction)
                return CommandResult.Failed("No active transaction");

            var results = new List<CommandResult>();
            
            foreach (var command in _currentTransaction)
            {
                var result = await command.ExecuteAsync();
                results.Add(result);
                
                if (result.Success)
                    _executedCommands.Push(command);
                else
                {
                    // Rollback previous commands in transaction
                    await RollbackTransactionAsync();
                    return CommandResult.Failed($"Transaction failed at command: {command.Description}");
                }
            }

            _inTransaction = false;
            _currentTransaction.Clear();
            
            return CommandResult.Successful($"Transaction committed successfully - {results.Count} commands executed");
        }

        public async Task<CommandResult> RollbackTransactionAsync()
        {
            if (!_inTransaction)
                return CommandResult.Failed("No active transaction");

            // Undo commands in reverse order
            for (int i = _currentTransaction.Count - 1; i >= 0; i--)
            {
                var command = _currentTransaction[i];
                if (_executedCommands.Contains(command))
                {
                    await command.UndoAsync();
                    _executedCommands.Pop();
                }
            }

            _inTransaction = false;
            _currentTransaction.Clear();
            
            return CommandResult.Successful("Transaction rolled back");
        }

        public async Task<CommandResult> ExecuteCommandAsync(ICommand command)
        {
            if (_inTransaction)
            {
                _currentTransaction.Add(command);
                return CommandResult.Successful("Command added to transaction");
            }
            else
            {
                var result = await command.ExecuteAsync();
                if (result.Success)
                    _executedCommands.Push(command);
                return result;
            }
        }

        public async Task<CommandResult> UndoLastCommandAsync()
        {
            if (_executedCommands.Count == 0)
                return CommandResult.Failed("No commands to undo");

            var lastCommand = _executedCommands.Pop();
            return await lastCommand.UndoAsync();
        }

        public IEnumerable<string> GetCommandHistory()
        {
            return _executedCommands.Reverse().Select(c => $"{c.ExecutedAt:HH:mm:ss} - {c.Description}");
        }
    }

    // ================================
    // ✅ BUILDER PATTERN - Configuración compleja
    // ================================

    public class EmailConfiguration
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Attachments { get; set; } = new();
        public string From { get; set; }
        public string ReplyTo { get; set; }
        public bool IsHtml { get; set; }
        public EmailPriority Priority { get; set; } = EmailPriority.Normal;
        public bool RequestReadReceipt { get; set; }
        public SmtpConfiguration SmtpConfig { get; set; }
        public List<string> CC { get; set; } = new();
        public List<string> BCC { get; set; } = new();
        public Dictionary<string, string> Headers { get; set; } = new();
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);
        public int RetryAttempts { get; set; } = 3;
        public string TemplateId { get; set; }
        public Dictionary<string, object> TemplateData { get; set; } = new();
    }

    public class SmtpConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }

    public enum EmailPriority { Low, Normal, High }

    public interface IEmailBuilder
    {
        IEmailBuilder To(string email);
        IEmailBuilder Subject(string subject);
        IEmailBuilder Body(string body, bool isHtml = false);
        IEmailBuilder From(string email);
        IEmailBuilder WithAttachment(string filePath);
        IEmailBuilder WithCC(params string[] emails);
        IEmailBuilder WithBCC(params string[] emails);
        IEmailBuilder WithPriority(EmailPriority priority);
        IEmailBuilder WithSmtp(string server, int port, string username, string password, bool enableSsl = true);
        IEmailBuilder WithTemplate(string templateId, object templateData = null);
        IEmailBuilder WithTimeout(TimeSpan timeout);
        IEmailBuilder WithRetry(int attempts);
        EmailConfiguration Build();
    }

    public class EmailBuilder : IEmailBuilder
    {
        private readonly EmailConfiguration _config = new();

        public IEmailBuilder To(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email address is required");
            _config.To = email;
            return this;
        }

        public IEmailBuilder Subject(string subject)
        {
            _config.Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            return this;
        }

        public IEmailBuilder Body(string body, bool isHtml = false)
        {
            _config.Body = body ?? throw new ArgumentNullException(nameof(body));
            _config.IsHtml = isHtml;
            return this;
        }

        public IEmailBuilder From(string email)
        {
            _config.From = email ?? throw new ArgumentNullException(nameof(email));
            return this;
        }

        public IEmailBuilder WithAttachment(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                _config.Attachments.Add(filePath);
            return this;
        }

        public IEmailBuilder WithCC(params string[] emails)
        {
            _config.CC.AddRange(emails.Where(e => !string.IsNullOrEmpty(e)));
            return this;
        }

        public IEmailBuilder WithBCC(params string[] emails)
        {
            _config.BCC.AddRange(emails.Where(e => !string.IsNullOrEmpty(e)));
            return this;
        }

        public IEmailBuilder WithPriority(EmailPriority priority)
        {
            _config.Priority = priority;
            return this;
        }

        public IEmailBuilder WithSmtp(string server, int port, string username, string password, bool enableSsl = true)
        {
            _config.SmtpConfig = new SmtpConfiguration
            {
                Server = server,
                Port = port,
                Username = username,
                Password = password,
                EnableSsl = enableSsl
            };
            return this;
        }

        public IEmailBuilder WithTemplate(string templateId, object templateData = null)
        {
            _config.TemplateId = templateId;
            if (templateData != null)
            {
                // Convert object to dictionary
                var properties = templateData.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    _config.TemplateData[prop.Name] = prop.GetValue(templateData);
                }
            }
            return this;
        }

        public IEmailBuilder WithTimeout(TimeSpan timeout)
        {
            _config.Timeout = timeout;
            return this;
        }

        public IEmailBuilder WithRetry(int attempts)
        {
            _config.RetryAttempts = Math.Max(1, attempts);
            return this;
        }

        public EmailConfiguration Build()
        {
            // Validate required fields
            if (string.IsNullOrEmpty(_config.To))
                throw new InvalidOperationException("To email address is required");
            if (string.IsNullOrEmpty(_config.Subject))
                throw new InvalidOperationException("Subject is required");
            if (string.IsNullOrEmpty(_config.Body) && string.IsNullOrEmpty(_config.TemplateId))
                throw new InvalidOperationException("Body or template is required");

            return _config;
        }
    }

    // ✅ Fluent email service using builder
    public class EmailService
    {
        public async Task<bool> SendEmailAsync(EmailConfiguration config)
        {
            Console.WriteLine($"📧 Sending email to: {config.To}");
            Console.WriteLine($"📧 Subject: {config.Subject}");
            Console.WriteLine($"📧 Priority: {config.Priority}");
            Console.WriteLine($"📧 Attachments: {config.Attachments.Count}");
            Console.WriteLine($"📧 CC: {config.CC.Count}, BCC: {config.BCC.Count}");
            
            if (!string.IsNullOrEmpty(config.TemplateId))
            {
                Console.WriteLine($"📧 Template: {config.TemplateId} with {config.TemplateData.Count} variables");
            }

            await Task.Delay(100); // Simulate sending
            Console.WriteLine("✅ Email sent successfully!");
            
            return true;
        }

        public static IEmailBuilder Create() => new EmailBuilder();
    }

    // ================================
    // ✅ TEMPLATE METHOD - Workflows reutilizables
    // ================================

    public abstract class DataImportProcessor
    {
        // ✅ Template method defining the algorithm
        public async Task<ImportResult> ImportDataAsync(string filePath)
        {
            var result = new ImportResult { FilePath = filePath, StartTime = DateTime.Now };

            try
            {
                // Step 1: Validate file (common)
                if (!await ValidateFileAsync(filePath))
                {
                    result.Success = false;
                    result.ErrorMessage = "File validation failed";
                    return result;
                }

                // Step 2: Read data (specific implementation)
                var rawData = await ReadDataAsync(filePath);
                result.RawRecordCount = rawData?.Count ?? 0;

                // Step 3: Parse data (specific implementation)
                var parsedData = await ParseDataAsync(rawData);
                
                // Step 4: Validate data (specific implementation)
                var validatedData = await ValidateDataAsync(parsedData);
                result.ValidRecordCount = validatedData?.Count ?? 0;

                // Step 5: Transform data (specific implementation)
                var transformedData = await TransformDataAsync(validatedData);

                // Step 6: Save to database (common)
                result.SavedRecordCount = await SaveToDatabase(transformedData);

                // Step 7: Generate report (common with customization)
                await GenerateReport(result);

                result.Success = true;
                result.EndTime = DateTime.Now;

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.EndTime = DateTime.Now;
                return result;
            }
        }

        // ✅ Common steps with default implementations
        protected virtual async Task<bool> ValidateFileAsync(string filePath)
        {
            await Task.Delay(10);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ File not found: {filePath}");
                return false;
            }
            
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Length == 0)
            {
                Console.WriteLine($"❌ File is empty: {filePath}");
                return false;
            }

            Console.WriteLine($"✅ File validation passed: {filePath}");
            return true;
        }

        protected virtual async Task<int> SaveToDatabase(List<Dictionary<string, object>> data)
        {
            await Task.Delay(50); // Simulate database operation
            Console.WriteLine($"💾 Saving {data.Count} records to database...");
            
            // Simulate some failures
            var savedCount = data.Count - new Random().Next(0, 3);
            Console.WriteLine($"✅ Saved {savedCount} records successfully");
            
            return savedCount;
        }

        protected virtual async Task GenerateReport(ImportResult result)
        {
            await Task.Delay(20);
            Console.WriteLine($"📊 Import Report for {result.FilePath}:");
            Console.WriteLine($"   Duration: {result.Duration?.TotalSeconds:F1} seconds");
            Console.WriteLine($"   Raw records: {result.RawRecordCount}");
            Console.WriteLine($"   Valid records: {result.ValidRecordCount}");
            Console.WriteLine($"   Saved records: {result.SavedRecordCount}");
            Console.WriteLine($"   Success rate: {(double)result.SavedRecordCount / result.RawRecordCount:P1}");
        }

        // ✅ Abstract methods that subclasses must implement
        protected abstract Task<List<string>> ReadDataAsync(string filePath);
        protected abstract Task<List<Dictionary<string, object>>> ParseDataAsync(List<string> rawData);
        protected abstract Task<List<Dictionary<string, object>>> ValidateDataAsync(List<Dictionary<string, object>> data);
        protected abstract Task<List<Dictionary<string, object>>> TransformDataAsync(List<Dictionary<string, object>> data);
    }

    public class CsvImportProcessor : DataImportProcessor
    {
        protected override async Task<List<string>> ReadDataAsync(string filePath)
        {
            await Task.Delay(30);
            // Simulate reading CSV file
            var lines = new List<string>
            {
                "Name,Email,Age,Country",
                "John Doe,john@email.com,30,USA",
                "Jane Smith,jane@email.com,25,Canada",
                "Invalid Line",
                "Bob Johnson,bob@email.com,35,UK"
            };
            
            Console.WriteLine($"📄 Read {lines.Count} lines from CSV file");
            return lines;
        }

        protected override async Task<List<Dictionary<string, object>>> ParseDataAsync(List<string> rawData)
        {
            await Task.Delay(20);
            var result = new List<Dictionary<string, object>>();
            
            if (rawData.Count == 0) return result;
            
            var headers = rawData[0].Split(',');
            
            for (int i = 1; i < rawData.Count; i++)
            {
                var values = rawData[i].Split(',');
                if (values.Length == headers.Length)
                {
                    var row = new Dictionary<string, object>();
                    for (int j = 0; j < headers.Length; j++)
                    {
                        row[headers[j]] = values[j].Trim('"');
                    }
                    result.Add(row);
                }
            }
            
            Console.WriteLine($"📝 Parsed {result.Count} CSV records");
            return result;
        }

        protected override async Task<List<Dictionary<string, object>>> ValidateDataAsync(List<Dictionary<string, object>> data)
        {
            await Task.Delay(15);
            var validData = new List<Dictionary<string, object>>();
            
            foreach (var row in data)
            {
                if (row.ContainsKey("Email") && 
                    row["Email"].ToString().Contains("@") &&
                    row.ContainsKey("Name") &&
                    !string.IsNullOrEmpty(row["Name"].ToString()))
                {
                    validData.Add(row);
                }
            }
            
            Console.WriteLine($"✅ Validated {validData.Count} records");
            return validData;
        }

        protected override async Task<List<Dictionary<string, object>>> TransformDataAsync(List<Dictionary<string, object>> data)
        {
            await Task.Delay(10);
            
            foreach (var row in data)
            {
                // CSV specific transformations
                if (row.ContainsKey("Email"))
                {
                    row["Email"] = row["Email"].ToString().ToLowerInvariant();
                }
                
                if (row.ContainsKey("Country"))
                {
                    row["CountryCode"] = GetCountryCode(row["Country"].ToString());
                }
                
                row["ImportDate"] = DateTime.Now;
                row["Source"] = "CSV";
            }
            
            Console.WriteLine($"🔄 Transformed {data.Count} CSV records");
            return data;
        }

        private string GetCountryCode(string country)
        {
            return country.ToUpper() switch
            {
                "USA" => "US",
                "CANADA" => "CA",
                "UK" => "GB",
                _ => "XX"
            };
        }
    }

    public class JsonImportProcessor : DataImportProcessor
    {
        protected override async Task<List<string>> ReadDataAsync(string filePath)
        {
            await Task.Delay(25);
            // Simulate JSON file content
            var jsonContent = @"[
                {""name"": ""Alice Wilson"", ""email"": ""alice@email.com"", ""age"": 28, ""country"": ""Australia""},
                {""name"": ""Charlie Brown"", ""email"": ""charlie@email.com"", ""age"": 32, ""country"": ""Germany""},
                {""name"": ""Diana Prince"", ""email"": ""diana@email.com"", ""age"": 29, ""country"": ""France""}
            ]";
            
            Console.WriteLine($"📄 Read JSON file content");
            return new List<string> { jsonContent };
        }

        protected override async Task<List<Dictionary<string, object>>> ParseDataAsync(List<string> rawData)
        {
            await Task.Delay(15);
            
            if (rawData.Count == 0) return new List<Dictionary<string, object>>();
            
            // Simulate JSON parsing
            var result = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { {"name", "Alice Wilson"}, {"email", "alice@email.com"}, {"age", 28}, {"country", "Australia"} },
                new Dictionary<string, object> { {"name", "Charlie Brown"}, {"email", "charlie@email.com"}, {"age", 32}, {"country", "Germany"} },
                new Dictionary<string, object> { {"name", "Diana Prince"}, {"email", "diana@email.com"}, {"age", 29}, {"country", "France"} }
            };
            
            Console.WriteLine($"📝 Parsed {result.Count} JSON records");
            return result;
        }

        protected override async Task<List<Dictionary<string, object>>> ValidateDataAsync(List<Dictionary<string, object>> data)
        {
            await Task.Delay(10);
            var validData = new List<Dictionary<string, object>>();
            
            foreach (var row in data)
            {
                if (row.ContainsKey("email") && 
                    row["email"].ToString().Contains("@") &&
                    row.ContainsKey("name") &&
                    !string.IsNullOrEmpty(row["name"].ToString()))
                {
                    validData.Add(row);
                }
            }
            
            Console.WriteLine($"✅ Validated {validData.Count} records");
            return validData;
        }

        protected override async Task<List<Dictionary<string, object>>> TransformDataAsync(List<Dictionary<string, object>> data)
        {
            await Task.Delay(8);
            
            foreach (var row in data)
            {
                // JSON specific transformations
                if (row.ContainsKey("email"))
                {
                    row["email"] = row["email"].ToString().ToLowerInvariant();
                }
                
                // Convert age to age group
                if (row.ContainsKey("age") && int.TryParse(row["age"].ToString(), out int age))
                {
                    row["ageGroup"] = age switch
                    {
                        < 25 => "Young",
                        < 35 => "Adult",
                        _ => "Mature"
                    };
                }
                
                row["importDate"] = DateTime.Now;
                row["source"] = "JSON";
            }
            
            Console.WriteLine($"🔄 Transformed {data.Count} JSON records");
            return data;
        }
    }

    public class ImportResult
    {
        public string FilePath { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? Duration => EndTime - StartTime;
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int RawRecordCount { get; set; }
        public int ValidRecordCount { get; set; }
        public int SavedRecordCount { get; set; }
    }

    // ================================
    // ✅ STRATEGY PATTERN - Algoritmos intercambiables
    // ================================

    public interface IPricingStrategy
    {
        decimal CalculatePrice(PricingContext context);
        string StrategyName { get; }
        bool AppliesTo(string customerType, string season);
    }

    public class PricingContext
    {
        public decimal BasePrice { get; set; }
        public string CustomerType { get; set; }
        public int Quantity { get; set; }
        public string Season { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductCategory { get; set; }
        public bool IsLoyalCustomer { get; set; }
        public decimal CustomerLifetimeValue { get; set; }
    }

    public class PremiumHighSeasonStrategy : IPricingStrategy
    {
        public string StrategyName => "Premium High Season";

        public bool AppliesTo(string customerType, string season) 
            => customerType == "PREMIUM" && season == "HIGH";

        public decimal CalculatePrice(PricingContext context)
        {
            var price = context.BasePrice * 1.2m; // Premium markup
            
            // Volume discounts
            if (context.Quantity > 100)
                price *= 0.95m;
            else if (context.Quantity > 50)
                price *= 0.97m;
                
            // Loyalty bonus
            if (context.IsLoyalCustomer)
                price *= 0.98m;
                
            return Math.Round(price, 2);
        }
    }

    public class PremiumNormalSeasonStrategy : IPricingStrategy
    {
        public string StrategyName => "Premium Normal Season";

        public bool AppliesTo(string customerType, string season) 
            => customerType == "PREMIUM" && season == "NORMAL";

        public decimal CalculatePrice(PricingContext context)
        {
            var price = context.BasePrice * 1.1m; // Premium markup
            
            // Better volume discounts in normal season
            if (context.Quantity > 50)
                price *= 0.90m;
            else if (context.Quantity > 25)
                price *= 0.95m;
                
            // Loyalty bonus
            if (context.IsLoyalCustomer)
                price *= 0.96m;
                
            return Math.Round(price, 2);
        }
    }

    public class WholesaleStrategy : IPricingStrategy
    {
        public string StrategyName => "Wholesale";

        public bool AppliesTo(string customerType, string season) 
            => customerType == "WHOLESALE";

        public decimal CalculatePrice(PricingContext context)
        {
            // Base wholesale discount
            var discount = context.Season == "HIGH" ? 0.8m : 0.7m;
            var price = context.BasePrice * discount;
            
            // Volume discounts
            if (context.Quantity > 1000)
                price *= 0.90m;
            else if (context.Quantity > 500)
                price *= 0.95m;
                
            // High-value customer bonus
            if (context.CustomerLifetimeValue > 100000)
                price *= 0.95m;
                
            return Math.Round(price, 2);
        }
    }

    public class StandardStrategy : IPricingStrategy
    {
        public string StrategyName => "Standard";

        public bool AppliesTo(string customerType, string season) 
            => customerType == "STANDARD";

        public decimal CalculatePrice(PricingContext context)
        {
            var price = context.BasePrice;
            
            // Season adjustment
            if (context.Season == "HIGH")
                price *= 1.1m;
                
            // Volume discounts
            if (context.Quantity > 200)
                price *= 0.98m;
            else if (context.Quantity > 100)
                price *= 0.99m;
                
            return Math.Round(price, 2);
        }
    }

    // ✅ Advanced pricing calculator with strategy management
    public class PricingCalculator
    {
        private readonly List<IPricingStrategy> _strategies;
        private readonly IPricingStrategy _defaultStrategy;

        public PricingCalculator(IEnumerable<IPricingStrategy> strategies = null)
        {
            _strategies = strategies?.ToList() ?? new List<IPricingStrategy>
            {
                new PremiumHighSeasonStrategy(),
                new PremiumNormalSeasonStrategy(),
                new WholesaleStrategy(),
                new StandardStrategy()
            };
            
            _defaultStrategy = new StandardStrategy();
        }

        public PricingResult CalculatePrice(PricingContext context)
        {
            var strategy = _strategies.FirstOrDefault(s => s.AppliesTo(context.CustomerType, context.Season))
                          ?? _defaultStrategy;

            var finalPrice = strategy.CalculatePrice(context);
            var discount = context.BasePrice - finalPrice;
            var discountPercentage = (discount / context.BasePrice) * 100;

            return new PricingResult
            {
                BasePrice = context.BasePrice,
                FinalPrice = finalPrice,
                Discount = discount,
                DiscountPercentage = discountPercentage,
                StrategyUsed = strategy.StrategyName,
                Context = context
            };
        }

        public void AddStrategy(IPricingStrategy strategy)
        {
            _strategies.Add(strategy);
        }

        public void RemoveStrategy(string strategyName)
        {
            _strategies.RemoveAll(s => s.StrategyName == strategyName);
        }

        public IEnumerable<string> GetAvailableStrategies()
        {
            return _strategies.Select(s => s.StrategyName);
        }
    }

    public class PricingResult
    {
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string StrategyUsed { get; set; }
        public PricingContext Context { get; set; }

        public override string ToString()
        {
            return $"Price: ${FinalPrice:F2} (was ${BasePrice:F2}) - {DiscountPercentage:F1}% off using {StrategyUsed}";
        }
    }

    // ================================
    // ✅ DEMO DE TODOS LOS PATTERNS
    // ================================

    public class DesignPatternsDemo
    {
        public async Task RunCompleteDemo()
        {
            Console.WriteLine("=== DESIGN PATTERNS DEMONSTRATION ===\n");

            await DemoFactoryPattern();
            await DemoObserverPattern();
            await DemoCommandPattern();
            await DemoBuilderPattern();
            await DemoTemplateMethodPattern();
            await DemoStrategyPattern();

            Console.WriteLine("\n=== ALL PATTERNS DEMONSTRATED SUCCESSFULLY ===");
        }

        private async Task DemoFactoryPattern()
        {
            Console.WriteLine("1. 🏭 FACTORY PATTERN DEMO");
            Console.WriteLine("========================");

            var factory = new AdvancedReportFactory(new DefaultReportValidator());
            
            var reportData = new Dictionary<string, object>
            {
                ["title"] = "Sales Report Q1",
                ["content"] = "Q1 sales exceeded expectations...",
                ["author"] = "Sales Team"
            };

            var pdfReport = factory.CreateReport("PDF", reportData);
            pdfReport.Generate();
            Console.WriteLine($"✅ Created: {pdfReport.GetOutput()}");

            Console.WriteLine($"Supported types: {string.Join(", ", factory.GetSupportedTypes())}\n");
        }

        private async Task DemoObserverPattern()
        {
            Console.WriteLine("2. 👁️ OBSERVER PATTERN DEMO");
            Console.WriteLine("=========================");

            var orderProcessor = new OrderProcessor();

            // Attach observers
            orderProcessor.Attach(new CustomerEmailNotificationObserver());
            orderProcessor.Attach(new WarehouseNotificationObserver());
            orderProcessor.Attach(new InventoryUpdateObserver());
            orderProcessor.Attach(new AnalyticsObserver());

            var order = new Order { Id = 12345, Total = 299.99m, CustomerEmail = "customer@example.com" };
            
            await orderProcessor.ProcessOrderAsync(order);
            Console.WriteLine($"✅ Order {order.Id} processed with status: {order.Status}\n");
        }

        private async Task DemoCommandPattern()
        {
            Console.WriteLine("3. ⚡ COMMAND PATTERN DEMO");
            Console.WriteLine("========================");

            var commandManager = new CommandManager();

            // Execute individual commands
            var createCommand = new CreateCommand("connection", "INSERT INTO Users VALUES ('John', 'john@email.com')");
            await commandManager.ExecuteCommandAsync(createCommand);

            var updateCommand = new UpdateCommand("connection", "UPDATE Users SET Email = 'john.doe@email.com'", 
                parameters: new Dictionary<string, object> { ["ID"] = 1 },
                oldValues: new Dictionary<string, object> { ["Email"] = "john@email.com" });
            await commandManager.ExecuteCommandAsync(updateCommand);

            // Undo last command
            await commandManager.UndoLastCommandAsync();

            Console.WriteLine("Command History:");
            foreach (var cmd in commandManager.GetCommandHistory())
                Console.WriteLine($"  {cmd}");
            Console.WriteLine();
        }

        private async Task DemoBuilderPattern()
        {
            Console.WriteLine("4. 🔨 BUILDER PATTERN DEMO");
            Console.WriteLine("========================");

            var emailService = new EmailService();

            // Simple email
            var simpleEmail = EmailService.Create()
                .To("customer@example.com")
                .Subject("Welcome!")
                .Body("Welcome to our service!", isHtml: true)
                .From("noreply@company.com")
                .Build();

            await emailService.SendEmailAsync(simpleEmail);

            // Complex email with all features
            var complexEmail = EmailService.Create()
                .To("vip@example.com")
                .Subject("VIP Newsletter")
                .WithTemplate("newsletter-template", new { CustomerName = "John Doe", Month = "January" })
                .From("marketing@company.com")
                .WithCC("manager@company.com")
                .WithPriority(EmailPriority.High)
                .WithAttachment("/path/to/newsletter.pdf")
                .WithSmtp("smtp.company.com", 587, "user", "pass")
                .WithRetry(3)
                .Build();

            await emailService.SendEmailAsync(complexEmail);
            Console.WriteLine();
        }

        private async Task DemoTemplateMethodPattern()
        {
            Console.WriteLine("5. 📋 TEMPLATE METHOD PATTERN DEMO");
            Console.WriteLine("================================");

            // CSV Import
            var csvProcessor = new CsvImportProcessor();
            var csvResult = await csvProcessor.ImportDataAsync("sample.csv");
            Console.WriteLine($"CSV Import Result: {(csvResult.Success ? "SUCCESS" : "FAILED")}");

            Console.WriteLine();

            // JSON Import
            var jsonProcessor = new JsonImportProcessor();
            var jsonResult = await jsonProcessor.ImportDataAsync("sample.json");
            Console.WriteLine($"JSON Import Result: {(jsonResult.Success ? "SUCCESS" : "FAILED")}\n");
        }

        private async Task DemoStrategyPattern()
        {
            Console.WriteLine("6. 🎯 STRATEGY PATTERN DEMO");
            Console.WriteLine("=========================");

            var calculator = new PricingCalculator();

            var contexts = new[]
            {
                new PricingContext { BasePrice = 100m, CustomerType = "PREMIUM", Quantity = 150, Season = "HIGH", IsLoyalCustomer = true },
                new PricingContext { BasePrice = 100m, CustomerType = "WHOLESALE", Quantity = 1500, Season = "NORMAL", CustomerLifetimeValue = 150000 },
                new PricingContext { BasePrice = 100m, CustomerType = "STANDARD", Quantity = 250, Season = "HIGH" }
            };

            foreach (var context in contexts)
            {
                var result = calculator.CalculatePrice(context);
                Console.WriteLine($"💰 {context.CustomerType} customer: {result}");
            }

            Console.WriteLine($"Available strategies: {string.Join(", ", calculator.GetAvailableStrategies())}\n");
        }
    }

    // Supporting classes for demos
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime ProcessedDate { get; set; }
        public decimal Total { get; set; }
        public string CustomerEmail { get; set; }
    }

    /*
    DESIGN PATTERNS IMPLEMENTADOS:

    ✅ FACTORY PATTERN:
    - Creación flexible y extensible de objetos
    - Soporte para validación y caching
    - Fácil agregar nuevos tipos sin modificar código existente

    ✅ OBSERVER PATTERN:
    - Notificaciones desacopladas y asíncronas
    - Manejo de errores sin afectar otros observers
    - Priorización de notificaciones

    ✅ COMMAND PATTERN:
    - Encapsulación completa de operaciones
    - Undo/Redo capability
    - Soporte para transacciones y rollback

    ✅ BUILDER PATTERN:
    - API fluent para configuraciones complejas
    - Validación en build time
    - Inmutabilidad y thread safety

    ✅ TEMPLATE METHOD PATTERN:
    - Algoritmos reutilizables con customización
    - Pasos comunes centralizados
    - Fácil extensión para nuevos formatos

    ✅ STRATEGY PATTERN:
    - Algoritmos intercambiables en runtime
    - Fácil testing y mantenimiento
    - Separación de concerns clara

    BENEFICIOS OBTENIDOS:

    🚀 EXTENSIBILIDAD:
    - Agregar nuevos tipos sin modificar código existente
    - Open/Closed Principle respetado
    - Plugin architecture posible

    🔧 MANTENIBILIDAD:
    - Responsabilidades bien separadas
    - Código autoexplicativo
    - Fácil localización de bugs

    🧪 TESTABILIDAD:
    - Mocking sencillo de dependencias
    - Unit tests específicos por patrón
    - Integration tests simplificados

    💪 ROBUSTEZ:
    - Manejo de errores centralizado
    - Validación consistente
    - Recovery mechanisms

    COMPARACIÓN CON CÓDIGO ORIGINAL:

    ANTES:
    ❌ Switch statements para creación
    ❌ Notificaciones hardcoded
    ❌ Sin undo capability
    ❌ Constructores con 18+ parámetros
    ❌ Código duplicado en workflows
    ❌ Algoritmos mezclados con lógica

    DESPUÉS:
    ✅ Factory extensible
    ✅ Observer pattern desacoplado
    ✅ Command pattern con transacciones
    ✅ Builder pattern fluent
    ✅ Template method reutilizable
    ✅ Strategy pattern intercambiable

    PRÓXIMO EJERCICIO:
    Solid Principles aplicados en conjunto con estos patterns
    */
}