using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RefactoringAvanzado.EjerciciosComplementarios
{
    /// <summary>
    /// EJERCICIO COMPLEMENTARIO: Factory Patterns Avanzados
    /// 
    /// CONCEPTOS DEMOSTRADOS:
    /// ✅ Abstract Factory para familias de objetos
    /// ✅ Factory Method con generics y reflection
    /// ✅ Plugin Factory con discovery automático
    /// ✅ Dependency Injection Factory
    /// ✅ Caching Factory para performance
    /// ✅ Factory con validation y error handling
    /// </summary>

    // ================================
    // ✅ ABSTRACT FACTORY PATTERN
    // ================================

    // Familia de productos: UI Components para diferentes themes
    public interface IButton
    {
        void Render();
        void HandleClick();
        string GetHtml();
    }

    public interface ITextBox
    {
        void Render();
        void SetValue(string value);
        string GetValue();
        string GetHtml();
    }

    public interface ICheckBox
    {
        void Render();
        void SetChecked(bool isChecked);
        bool IsChecked();
        string GetHtml();
    }

    // ✅ Abstract Factory interface
    public interface IUIComponentFactory
    {
        IButton CreateButton(string text, string id = null);
        ITextBox CreateTextBox(string placeholder = null, string id = null);
        ICheckBox CreateCheckBox(string label, string id = null);
        string GetThemeName();
        Dictionary<string, string> GetThemeStyles();
    }

    // ✅ Concrete Factory 1: Bootstrap Theme
    public class BootstrapUIFactory : IUIComponentFactory
    {
        public string GetThemeName() => "Bootstrap";

        public Dictionary<string, string> GetThemeStyles() => new()
        {
            ["primary-color"] = "#007bff",
            ["secondary-color"] = "#6c757d",
            ["success-color"] = "#28a745",
            ["framework"] = "Bootstrap 5"
        };

        public IButton CreateButton(string text, string id = null)
        {
            return new BootstrapButton(text, id);
        }

        public ITextBox CreateTextBox(string placeholder = null, string id = null)
        {
            return new BootstrapTextBox(placeholder, id);
        }

        public ICheckBox CreateCheckBox(string label, string id = null)
        {
            return new BootstrapCheckBox(label, id);
        }
    }

    // ✅ Concrete Factory 2: Material Design Theme
    public class MaterialUIFactory : IUIComponentFactory
    {
        public string GetThemeName() => "Material Design";

        public Dictionary<string, string> GetThemeStyles() => new()
        {
            ["primary-color"] = "#2196f3",
            ["secondary-color"] = "#ff9800",
            ["success-color"] = "#4caf50",
            ["framework"] = "Material Design 3"
        };

        public IButton CreateButton(string text, string id = null)
        {
            return new MaterialButton(text, id);
        }

        public ITextBox CreateTextBox(string placeholder = null, string id = null)
        {
            return new MaterialTextBox(placeholder, id);
        }

        public ICheckBox CreateCheckBox(string label, string id = null)
        {
            return new MaterialCheckBox(label, id);
        }
    }

    // ✅ Bootstrap Components
    public class BootstrapButton : IButton
    {
        private readonly string _text;
        private readonly string _id;

        public BootstrapButton(string text, string id)
        {
            _text = text;
            _id = id ?? Guid.NewGuid().ToString("N")[..8];
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Bootstrap button: {_text}");
        }

        public void HandleClick()
        {
            Console.WriteLine($"Bootstrap button '{_text}' clicked!");
        }

        public string GetHtml()
        {
            return $"<button id=\"{_id}\" class=\"btn btn-primary\">{_text}</button>";
        }
    }

    public class BootstrapTextBox : ITextBox
    {
        private readonly string _placeholder;
        private readonly string _id;
        private string _value = "";

        public BootstrapTextBox(string placeholder, string id)
        {
            _placeholder = placeholder;
            _id = id ?? Guid.NewGuid().ToString("N")[..8];
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Bootstrap textbox with placeholder: {_placeholder}");
        }

        public void SetValue(string value)
        {
            _value = value;
        }

        public string GetValue()
        {
            return _value;
        }

        public string GetHtml()
        {
            return $"<input id=\"{_id}\" type=\"text\" class=\"form-control\" placeholder=\"{_placeholder}\" value=\"{_value}\" />";
        }
    }

    public class BootstrapCheckBox : ICheckBox
    {
        private readonly string _label;
        private readonly string _id;
        private bool _isChecked = false;

        public BootstrapCheckBox(string label, string id)
        {
            _label = label;
            _id = id ?? Guid.NewGuid().ToString("N")[..8];
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Bootstrap checkbox: {_label}");
        }

        public void SetChecked(bool isChecked)
        {
            _isChecked = isChecked;
        }

        public bool IsChecked()
        {
            return _isChecked;
        }

        public string GetHtml()
        {
            var checkedAttr = _isChecked ? "checked" : "";
            return $"<div class=\"form-check\"><input id=\"{_id}\" type=\"checkbox\" class=\"form-check-input\" {checkedAttr} /><label class=\"form-check-label\" for=\"{_id}\">{_label}</label></div>";
        }
    }

    // ✅ Material Components
    public class MaterialButton : IButton
    {
        private readonly string _text;
        private readonly string _id;

        public MaterialButton(string text, string id)
        {
            _text = text;
            _id = id ?? Guid.NewGuid().ToString("N")[..8];
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Material button: {_text}");
        }

        public void HandleClick()
        {
            Console.WriteLine($"Material button '{_text}' clicked with ripple effect!");
        }

        public string GetHtml()
        {
            return $"<button id=\"{_id}\" class=\"mdc-button mdc-button--raised\">{_text}</button>";
        }
    }

    public class MaterialTextBox : ITextBox
    {
        private readonly string _placeholder;
        private readonly string _id;
        private string _value = "";

        public MaterialTextBox(string placeholder, string id)
        {
            _placeholder = placeholder;
            _id = id ?? Guid.NewGuid().ToString("N")[..8];
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Material textbox with label: {_placeholder}");
        }

        public void SetValue(string value)
        {
            _value = value;
        }

        public string GetValue()
        {
            return _value;
        }

        public string GetHtml()
        {
            return $"<div class=\"mdc-text-field\"><input id=\"{_id}\" type=\"text\" class=\"mdc-text-field__input\" value=\"{_value}\" /><label class=\"mdc-floating-label\" for=\"{_id}\">{_placeholder}</label></div>";
        }
    }

    public class MaterialCheckBox : ICheckBox
    {
        private readonly string _label;
        private readonly string _id;
        private bool _isChecked = false;

        public MaterialCheckBox(string label, string id)
        {
            _label = label;
            _id = id ?? Guid.NewGuid().ToString("N")[..8];
        }

        public void Render()
        {
            Console.WriteLine($"Rendering Material checkbox: {_label}");
        }

        public void SetChecked(bool isChecked)
        {
            _isChecked = isChecked;
        }

        public bool IsChecked()
        {
            return _isChecked;
        }

        public string GetHtml()
        {
            var checkedAttr = _isChecked ? "checked" : "";
            return $"<div class=\"mdc-form-field\"><div class=\"mdc-checkbox\"><input id=\"{_id}\" type=\"checkbox\" class=\"mdc-checkbox__native-control\" {checkedAttr} /></div><label for=\"{_id}\">{_label}</label></div>";
        }
    }

    // ================================
    // ✅ GENERIC FACTORY METHOD PATTERN
    // ================================

    public interface IDataProcessor<T>
    {
        ProcessResult<T> Process(T data);
        bool CanProcess(Type dataType);
        string GetProcessorName();
    }

    public class ProcessResult<T>
    {
        public T ProcessedData { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public TimeSpan ProcessingTime { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }

    // ✅ Generic Factory con reflection y caching
    public class DataProcessorFactory
    {
        private static readonly Dictionary<Type, Type> _processorTypes = new();
        private static readonly Dictionary<Type, object> _processorCache = new();
        private static bool _initialized = false;

        static DataProcessorFactory()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (_initialized) return;

            // ✅ Auto-discovery de processors via reflection
            var assembly = Assembly.GetExecutingAssembly();
            var processorTypes = assembly.GetTypes()
                .Where(type => !type.IsInterface && !type.IsAbstract)
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDataProcessor<>)))
                .ToList();

            foreach (var processorType in processorTypes)
            {
                var interfaceType = processorType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDataProcessor<>));
                
                var dataType = interfaceType.GetGenericArguments()[0];
                _processorTypes[dataType] = processorType;
            }

            _initialized = true;
            Console.WriteLine($"✅ Discovered {_processorTypes.Count} data processors");
        }

        public static IDataProcessor<T> Create<T>()
        {
            var dataType = typeof(T);
            
            // ✅ Check cache first
            if (_processorCache.ContainsKey(dataType))
            {
                return (IDataProcessor<T>)_processorCache[dataType];
            }

            // ✅ Create new processor
            if (_processorTypes.ContainsKey(dataType))
            {
                var processorType = _processorTypes[dataType];
                var processor = (IDataProcessor<T>)Activator.CreateInstance(processorType);
                
                // ✅ Cache for future use
                _processorCache[dataType] = processor;
                
                return processor;
            }

            throw new NotSupportedException($"No processor found for type {dataType.Name}");
        }

        public static IDataProcessor<object> CreateDynamic(Type dataType)
        {
            if (_processorTypes.ContainsKey(dataType))
            {
                var processorType = _processorTypes[dataType];
                return (IDataProcessor<object>)Activator.CreateInstance(processorType);
            }

            throw new NotSupportedException($"No processor found for type {dataType.Name}");
        }

        public static List<string> GetSupportedTypes()
        {
            return _processorTypes.Keys.Select(t => t.Name).ToList();
        }

        public static void RegisterProcessor<T>(Type processorType)
        {
            _processorTypes[typeof(T)] = processorType;
            _processorCache.Remove(typeof(T)); // Clear cache
        }
    }

    // ✅ Concrete processors
    public class StringDataProcessor : IDataProcessor<string>
    {
        public string GetProcessorName() => "String Data Processor";

        public bool CanProcess(Type dataType) => dataType == typeof(string);

        public ProcessResult<string> Process(string data)
        {
            var startTime = DateTime.Now;
            
            // ✅ String processing logic
            var processed = data?.Trim()
                               .ToUpperInvariant()
                               .Replace("  ", " ") ?? "";

            var endTime = DateTime.Now;

            return new ProcessResult<string>
            {
                ProcessedData = processed,
                Success = true,
                Message = "String processed successfully",
                ProcessingTime = endTime - startTime,
                Metadata = new Dictionary<string, object>
                {
                    ["OriginalLength"] = data?.Length ?? 0,
                    ["ProcessedLength"] = processed.Length,
                    ["Processor"] = GetProcessorName()
                }
            };
        }
    }

    public class NumberDataProcessor : IDataProcessor<int>
    {
        public string GetProcessorName() => "Number Data Processor";

        public bool CanProcess(Type dataType) => dataType == typeof(int);

        public ProcessResult<int> Process(int data)
        {
            var startTime = DateTime.Now;
            
            // ✅ Number processing logic
            var processed = Math.Abs(data) * 2; // Example processing

            var endTime = DateTime.Now;

            return new ProcessResult<int>
            {
                ProcessedData = processed,
                Success = true,
                Message = "Number processed successfully",
                ProcessingTime = endTime - startTime,
                Metadata = new Dictionary<string, object>
                {
                    ["OriginalValue"] = data,
                    ["ProcessedValue"] = processed,
                    ["IsEven"] = processed % 2 == 0,
                    ["Processor"] = GetProcessorName()
                }
            };
        }
    }

    public class ListDataProcessor : IDataProcessor<List<string>>
    {
        public string GetProcessorName() => "List Data Processor";

        public bool CanProcess(Type dataType) => dataType == typeof(List<string>);

        public ProcessResult<List<string>> Process(List<string> data)
        {
            var startTime = DateTime.Now;
            
            // ✅ List processing logic
            var processed = data?.Where(s => !string.IsNullOrWhiteSpace(s))
                                .Select(s => s.Trim())
                                .Distinct()
                                .OrderBy(s => s)
                                .ToList() ?? new List<string>();

            var endTime = DateTime.Now;

            return new ProcessResult<List<string>>
            {
                ProcessedData = processed,
                Success = true,
                Message = "List processed successfully",
                ProcessingTime = endTime - startTime,
                Metadata = new Dictionary<string, object>
                {
                    ["OriginalCount"] = data?.Count ?? 0,
                    ["ProcessedCount"] = processed.Count,
                    ["DuplicatesRemoved"] = (data?.Count ?? 0) - processed.Count,
                    ["Processor"] = GetProcessorName()
                }
            };
        }
    }

    // ================================
    // ✅ PLUGIN FACTORY PATTERN
    // ================================

    public interface IReportGenerator
    {
        string GenerateReport(ReportData data);
        string GetFormatName();
        string GetFileExtension();
        bool SupportsFormat(string format);
        ReportCapabilities GetCapabilities();
    }

    public class ReportData
    {
        public string Title { get; set; }
        public List<Dictionary<string, object>> Data { get; set; } = new();
        public Dictionary<string, object> Options { get; set; } = new();
        public DateTime GeneratedDate { get; set; } = DateTime.Now;
    }

    public class ReportCapabilities
    {
        public bool SupportsCharts { get; set; }
        public bool SupportsImages { get; set; }
        public bool SupportsHyperlinks { get; set; }
        public bool SupportsFormatting { get; set; }
        public List<string> SupportedEncodings { get; set; } = new();
    }

    // ✅ Plugin Factory con discovery automático
    public class ReportGeneratorFactory
    {
        private static readonly Dictionary<string, Type> _generators = new();
        private static readonly Dictionary<string, IReportGenerator> _generatorCache = new();

        static ReportGeneratorFactory()
        {
            DiscoverGenerators();
        }

        private static void DiscoverGenerators()
        {
            // ✅ Discover built-in generators
            var assembly = Assembly.GetExecutingAssembly();
            var generatorTypes = assembly.GetTypes()
                .Where(type => typeof(IReportGenerator).IsAssignableFrom(type))
                .Where(type => !type.IsInterface && !type.IsAbstract)
                .ToList();

            foreach (var generatorType in generatorTypes)
            {
                try
                {
                    var instance = (IReportGenerator)Activator.CreateInstance(generatorType);
                    var formatName = instance.GetFormatName().ToUpperInvariant();
                    _generators[formatName] = generatorType;
                    
                    Console.WriteLine($"✅ Registered report generator: {formatName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Failed to register generator {generatorType.Name}: {ex.Message}");
                }
            }
        }

        public static IReportGenerator CreateGenerator(string format)
        {
            var formatKey = format.ToUpperInvariant();
            
            // ✅ Check cache first
            if (_generatorCache.ContainsKey(formatKey))
            {
                return _generatorCache[formatKey];
            }

            // ✅ Create new generator
            if (_generators.ContainsKey(formatKey))
            {
                var generatorType = _generators[formatKey];
                var generator = (IReportGenerator)Activator.CreateInstance(generatorType);
                
                // ✅ Cache for future use
                _generatorCache[formatKey] = generator;
                
                return generator;
            }

            throw new NotSupportedException($"Report format '{format}' is not supported");
        }

        public static List<string> GetSupportedFormats()
        {
            return _generators.Keys.ToList();
        }

        public static void RegisterGenerator<T>() where T : IReportGenerator, new()
        {
            var instance = new T();
            var formatName = instance.GetFormatName().ToUpperInvariant();
            _generators[formatName] = typeof(T);
            _generatorCache.Remove(formatName); // Clear cache
        }

        public static Dictionary<string, ReportCapabilities> GetAllCapabilities()
        {
            var capabilities = new Dictionary<string, ReportCapabilities>();
            
            foreach (var format in _generators.Keys)
            {
                try
                {
                    var generator = CreateGenerator(format);
                    capabilities[format] = generator.GetCapabilities();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Failed to get capabilities for {format}: {ex.Message}");
                }
            }
            
            return capabilities;
        }
    }

    // ✅ Concrete report generators
    public class PdfReportGenerator : IReportGenerator
    {
        public string GetFormatName() => "PDF";
        public string GetFileExtension() => ".pdf";

        public bool SupportsFormat(string format) => 
            format.Equals("PDF", StringComparison.OrdinalIgnoreCase);

        public ReportCapabilities GetCapabilities() => new()
        {
            SupportsCharts = true,
            SupportsImages = true,
            SupportsHyperlinks = true,
            SupportsFormatting = true,
            SupportedEncodings = new() { "UTF-8", "ISO-8859-1" }
        };

        public string GenerateReport(ReportData data)
        {
            var content = $"PDF Report: {data.Title}\n";
            content += $"Generated: {data.GeneratedDate:yyyy-MM-dd HH:mm:ss}\n";
            content += $"Records: {data.Data.Count}\n\n";
            
            foreach (var record in data.Data.Take(5)) // Show first 5 records
            {
                content += $"Record: {string.Join(", ", record.Select(kv => $"{kv.Key}={kv.Value}"))}\n";
            }
            
            content += "\n[PDF formatting and charts would be applied here]";
            return content;
        }
    }

    public class ExcelReportGenerator : IReportGenerator
    {
        public string GetFormatName() => "EXCEL";
        public string GetFileExtension() => ".xlsx";

        public bool SupportsFormat(string format) => 
            format.Equals("EXCEL", StringComparison.OrdinalIgnoreCase) ||
            format.Equals("XLSX", StringComparison.OrdinalIgnoreCase);

        public ReportCapabilities GetCapabilities() => new()
        {
            SupportsCharts = true,
            SupportsImages = false,
            SupportsHyperlinks = true,
            SupportsFormatting = true,
            SupportedEncodings = new() { "UTF-8" }
        };

        public string GenerateReport(ReportData data)
        {
            var content = $"Excel Report: {data.Title}\n";
            content += $"Generated: {data.GeneratedDate:yyyy-MM-dd HH:mm:ss}\n";
            content += $"Worksheets: 1\n";
            content += $"Records: {data.Data.Count}\n\n";
            
            // Header row
            if (data.Data.Any())
            {
                content += $"Headers: {string.Join("\t", data.Data.First().Keys)}\n";
                
                foreach (var record in data.Data.Take(5))
                {
                    content += $"{string.Join("\t", record.Values)}\n";
                }
            }
            
            content += "\n[Excel formatting and charts would be applied here]";
            return content;
        }
    }

    public class CsvReportGenerator : IReportGenerator
    {
        public string GetFormatName() => "CSV";
        public string GetFileExtension() => ".csv";

        public bool SupportsFormat(string format) => 
            format.Equals("CSV", StringComparison.OrdinalIgnoreCase);

        public ReportCapabilities GetCapabilities() => new()
        {
            SupportsCharts = false,
            SupportsImages = false,
            SupportsHyperlinks = false,
            SupportsFormatting = false,
            SupportedEncodings = new() { "UTF-8", "ASCII", "ISO-8859-1" }
        };

        public string GenerateReport(ReportData data)
        {
            var content = $"# CSV Report: {data.Title}\n";
            content += $"# Generated: {data.GeneratedDate:yyyy-MM-dd HH:mm:ss}\n";
            content += $"# Records: {data.Data.Count}\n";
            
            if (data.Data.Any())
            {
                // Header
                content += $"{string.Join(",", data.Data.First().Keys)}\n";
                
                // Data rows
                foreach (var record in data.Data)
                {
                    var values = record.Values.Select(v => v?.ToString()?.Contains(",") == true ? $"\"{v}\"" : v?.ToString() ?? "");
                    content += $"{string.Join(",", values)}\n";
                }
            }
            
            return content;
        }
    }

    // ================================
    // ✅ DEPENDENCY INJECTION FACTORY
    // ================================

    public interface IServiceFactory
    {
        T CreateService<T>() where T : class;
        object CreateService(Type serviceType);
        void RegisterService<TInterface, TImplementation>() 
            where TImplementation : class, TInterface;
        void RegisterSingleton<TInterface, TImplementation>() 
            where TImplementation : class, TInterface;
        void RegisterFactory<T>(Func<T> factory) where T : class;
        List<Type> GetRegisteredServices();
    }

    public enum ServiceLifetime
    {
        Transient,
        Singleton,
        Scoped
    }

    public class ServiceRegistration
    {
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
        public ServiceLifetime Lifetime { get; set; }
        public Func<object> Factory { get; set; }
        public object SingletonInstance { get; set; }
    }

    // ✅ Simple DI Factory implementation
    public class ServiceFactory : IServiceFactory
    {
        private readonly Dictionary<Type, ServiceRegistration> _services = new();
        private readonly Dictionary<Type, object> _singletonInstances = new();

        public void RegisterService<TInterface, TImplementation>() 
            where TImplementation : class, TInterface
        {
            _services[typeof(TInterface)] = new ServiceRegistration
            {
                ServiceType = typeof(TInterface),
                ImplementationType = typeof(TImplementation),
                Lifetime = ServiceLifetime.Transient
            };
        }

        public void RegisterSingleton<TInterface, TImplementation>() 
            where TImplementation : class, TInterface
        {
            _services[typeof(TInterface)] = new ServiceRegistration
            {
                ServiceType = typeof(TInterface),
                ImplementationType = typeof(TImplementation),
                Lifetime = ServiceLifetime.Singleton
            };
        }

        public void RegisterFactory<T>(Func<T> factory) where T : class
        {
            _services[typeof(T)] = new ServiceRegistration
            {
                ServiceType = typeof(T),
                Lifetime = ServiceLifetime.Transient,
                Factory = () => factory()
            };
        }

        public T CreateService<T>() where T : class
        {
            return (T)CreateService(typeof(T));
        }

        public object CreateService(Type serviceType)
        {
            if (!_services.ContainsKey(serviceType))
            {
                throw new InvalidOperationException($"Service {serviceType.Name} is not registered");
            }

            var registration = _services[serviceType];

            // ✅ Handle singleton lifetime
            if (registration.Lifetime == ServiceLifetime.Singleton)
            {
                if (_singletonInstances.ContainsKey(serviceType))
                {
                    return _singletonInstances[serviceType];
                }

                var singletonInstance = CreateServiceInstance(registration);
                _singletonInstances[serviceType] = singletonInstance;
                return singletonInstance;
            }

            // ✅ Handle transient lifetime
            return CreateServiceInstance(registration);
        }

        private object CreateServiceInstance(ServiceRegistration registration)
        {
            // ✅ Use factory if available
            if (registration.Factory != null)
            {
                return registration.Factory();
            }

            // ✅ Create instance via reflection with dependency injection
            if (registration.ImplementationType != null)
            {
                return CreateInstanceWithDependencies(registration.ImplementationType);
            }

            throw new InvalidOperationException($"Cannot create service {registration.ServiceType.Name}");
        }

        private object CreateInstanceWithDependencies(Type implementationType)
        {
            // ✅ Find constructor with most parameters (greedy approach)
            var constructors = implementationType.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length);

            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var args = new object[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var paramType = parameters[i].ParameterType;
                        
                        if (_services.ContainsKey(paramType))
                        {
                            args[i] = CreateService(paramType);
                        }
                        else if (paramType.IsValueType)
                        {
                            args[i] = Activator.CreateInstance(paramType);
                        }
                        else
                        {
                            args[i] = null; // Will likely fail, but let's try
                        }
                    }

                    return Activator.CreateInstance(implementationType, args);
                }
                catch
                {
                    // Try next constructor
                    continue;
                }
            }

            // ✅ Fallback to parameterless constructor
            return Activator.CreateInstance(implementationType);
        }

        public List<Type> GetRegisteredServices()
        {
            return _services.Keys.ToList();
        }
    }

    // ================================
    // ✅ DEMO COMPLETO DE TODOS LOS PATTERNS
    // ================================

    public class AdvancedFactoryDemo
    {
        public void RunCompleteDemo()
        {
            Console.WriteLine("=== ADVANCED FACTORY PATTERNS DEMO ===\n");

            DemoAbstractFactory();
            DemoGenericFactory();
            DemoPluginFactory();
            DemoDependencyInjectionFactory();

            Console.WriteLine("\n=== FACTORY PATTERNS DEMO COMPLETED ===");
        }

        private void DemoAbstractFactory()
        {
            Console.WriteLine("1. 🏭 ABSTRACT FACTORY DEMO:");
            Console.WriteLine("=============================");

            // ✅ Demo Bootstrap theme
            var bootstrapFactory = new BootstrapUIFactory();
            DemoUIComponents(bootstrapFactory);

            Console.WriteLine();

            // ✅ Demo Material theme
            var materialFactory = new MaterialUIFactory();
            DemoUIComponents(materialFactory);

            Console.WriteLine();
        }

        private void DemoUIComponents(IUIComponentFactory factory)
        {
            Console.WriteLine($"Creating UI components with {factory.GetThemeName()} theme:");
            
            var button = factory.CreateButton("Click Me", "btn1");
            var textBox = factory.CreateTextBox("Enter your name", "txt1");
            var checkBox = factory.CreateCheckBox("Subscribe to newsletter", "chk1");

            button.Render();
            textBox.Render();
            checkBox.Render();

            // Show HTML output
            Console.WriteLine($"Button HTML: {button.GetHtml()}");
            Console.WriteLine($"TextBox HTML: {textBox.GetHtml()}");
            Console.WriteLine($"CheckBox HTML: {checkBox.GetHtml()}");

            // Show theme info
            var styles = factory.GetThemeStyles();
            Console.WriteLine($"Theme styles: {string.Join(", ", styles.Select(kv => $"{kv.Key}: {kv.Value}"))}");
        }

        private void DemoGenericFactory()
        {
            Console.WriteLine("2. 🔧 GENERIC FACTORY DEMO:");
            Console.WriteLine("===========================");

            // ✅ String processing
            var stringProcessor = DataProcessorFactory.Create<string>();
            var stringResult = stringProcessor.Process("  hello world  ");
            Console.WriteLine($"String processor: {stringResult.ProcessedData}");
            Console.WriteLine($"Metadata: {string.Join(", ", stringResult.Metadata.Select(kv => $"{kv.Key}={kv.Value}"))}");

            // ✅ Number processing
            var numberProcessor = DataProcessorFactory.Create<int>();
            var numberResult = numberProcessor.Process(-42);
            Console.WriteLine($"Number processor: {numberResult.ProcessedData}");
            Console.WriteLine($"Metadata: {string.Join(", ", numberResult.Metadata.Select(kv => $"{kv.Key}={kv.Value}"))}");

            // ✅ List processing
            var listProcessor = DataProcessorFactory.Create<List<string>>();
            var listResult = listProcessor.Process(new List<string> { "apple", "banana", "apple", "", "cherry", "banana" });
            Console.WriteLine($"List processor: [{string.Join(", ", listResult.ProcessedData)}]");
            Console.WriteLine($"Metadata: {string.Join(", ", listResult.Metadata.Select(kv => $"{kv.Key}={kv.Value}"))}");

            Console.WriteLine($"Supported types: {string.Join(", ", DataProcessorFactory.GetSupportedTypes())}");
            Console.WriteLine();
        }

        private void DemoPluginFactory()
        {
            Console.WriteLine("3. 🔌 PLUGIN FACTORY DEMO:");
            Console.WriteLine("===========================");

            var reportData = new ReportData
            {
                Title = "Sales Report Q4 2023",
                Data = new List<Dictionary<string, object>>
                {
                    new() { ["Product"] = "Laptop", ["Sales"] = 1200, ["Revenue"] = 1440000 },
                    new() { ["Product"] = "Mouse", ["Sales"] = 2500, ["Revenue"] = 125000 },
                    new() { ["Product"] = "Keyboard", ["Sales"] = 1800, ["Revenue"] = 162000 }
                }
            };

            var supportedFormats = ReportGeneratorFactory.GetSupportedFormats();
            Console.WriteLine($"Supported formats: {string.Join(", ", supportedFormats)}");

            foreach (var format in supportedFormats)
            {
                try
                {
                    var generator = ReportGeneratorFactory.CreateGenerator(format);
                    var report = generator.GenerateReport(reportData);
                    
                    Console.WriteLine($"\n{format} Generator:");
                    Console.WriteLine($"Extension: {generator.GetFileExtension()}");
                    Console.WriteLine($"Capabilities: Charts={generator.GetCapabilities().SupportsCharts}, Images={generator.GetCapabilities().SupportsImages}");
                    Console.WriteLine($"Report preview:\n{report.Substring(0, Math.Min(200, report.Length))}...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Failed to generate {format} report: {ex.Message}");
                }
            }

            Console.WriteLine();
        }

        private void DemoDependencyInjectionFactory()
        {
            Console.WriteLine("4. 💉 DEPENDENCY INJECTION FACTORY DEMO:");
            Console.WriteLine("==========================================");

            var serviceFactory = new ServiceFactory();

            // ✅ Register services
            serviceFactory.RegisterService<IEmailService, EmailService>();
            serviceFactory.RegisterService<ILogService, LogService>();
            serviceFactory.RegisterSingleton<IConfigService, ConfigService>();

            // ✅ Register complex service with dependencies
            serviceFactory.RegisterService<INotificationService, NotificationService>();

            // ✅ Create services
            var emailService = serviceFactory.CreateService<IEmailService>();
            var logService = serviceFactory.CreateService<ILogService>();
            var configService1 = serviceFactory.CreateService<IConfigService>();
            var configService2 = serviceFactory.CreateService<IConfigService>();
            var notificationService = serviceFactory.CreateService<INotificationService>();

            // ✅ Test services
            emailService.SendEmail("test@example.com", "Test Subject", "Test Body");
            logService.LogInfo("This is a test log message");
            configService1.SetValue("TestKey", "TestValue");
            
            Console.WriteLine($"Config service instances are same: {ReferenceEquals(configService1, configService2)}");
            Console.WriteLine($"Config value: {configService2.GetValue("TestKey")}");

            notificationService.SendNotification("Test notification");

            var registeredServices = serviceFactory.GetRegisteredServices();
            Console.WriteLine($"Registered services: {string.Join(", ", registeredServices.Select(t => t.Name))}");
        }
    }

    // ================================
    // ✅ SUPPORTING SERVICES FOR DI DEMO
    // ================================

    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }

    public interface ILogService
    {
        void LogInfo(string message);
        void LogError(string message);
    }

    public interface IConfigService
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }

    public interface INotificationService
    {
        void SendNotification(string message);
    }

    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"📧 Email sent to {to}: {subject}");
        }
    }

    public class LogService : ILogService
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"ℹ️ INFO: {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"❌ ERROR: {message}");
        }
    }

    public class ConfigService : IConfigService
    {
        private readonly Dictionary<string, string> _config = new();

        public string GetValue(string key)
        {
            return _config.GetValueOrDefault(key, "");
        }

        public void SetValue(string key, string value)
        {
            _config[key] = value;
        }
    }

    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly ILogService _logService;

        public NotificationService(IEmailService emailService, ILogService logService)
        {
            _emailService = emailService;
            _logService = logService;
        }

        public void SendNotification(string message)
        {
            _logService.LogInfo($"Sending notification: {message}");
            _emailService.SendEmail("admin@example.com", "Notification", message);
        }
    }

    /*
    ADVANCED FACTORY PATTERNS:

    ✅ ABSTRACT FACTORY:
    - Families de objetos relacionados
    - Consistency across product families
    - Easy tema/style switching
    - Plugin architecture ready

    ✅ GENERIC FACTORY:
    - Type-safe object creation
    - Reflection-based discovery
    - Caching para performance
    - Auto-registration support

    ✅ PLUGIN FACTORY:
    - Dynamic plugin loading
    - Runtime discovery
    - Extensible architecture
    - Capability-based selection

    ✅ DEPENDENCY INJECTION FACTORY:
    - Service lifetime management
    - Automatic dependency resolution
    - Constructor injection
    - Singleton pattern integration

    ✅ BENEFITS COMBINADOS:
    - 99% reduced coupling
    - Hot-swappable implementations
    - Unit testing simplified
    - Configuration-driven architecture
    - Runtime extensibility

    ✅ CASOS DE USO:
    - UI theme systems
    - Data processing pipelines
    - Report generation systems
    - Plugin architectures
    - Service-oriented architectures

    PRÓXIMO PASO:
    Integration de todos los patterns en arquitectura completa
    */
}