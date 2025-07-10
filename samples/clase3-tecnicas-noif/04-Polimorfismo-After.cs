using System;
using System.Collections.Generic;
using System.Linq;

namespace TecnicasNoIf.Ejercicios
{
    /// <summary>
    /// SOLUCIÓN: Polimorfismo para eliminar type checking
    /// 
    /// MEJORAS APLICADAS:
    /// ✅ MEJORA 1: Interface común para todos los tipos de data
    /// ✅ MEJORA 2: Cada clase encapsula su comportamiento
    /// ✅ MEJORA 3: Eliminado todo type checking/casting
    /// ✅ MEJORA 4: Virtual methods para comportamiento polimórfico
    /// ✅ MEJORA 5: Strategy Pattern integrado para formatos
    /// ✅ MEJORA 6: Open/Closed Principle aplicado
    /// ✅ MEJORA 7: Testing simplificado por clase
    /// </summary>

    // ✅ MEJORA 1: Interface común para comportamiento polimórfico
    public interface IReportableData
    {
        string GenerateReport(IReportFormatter formatter);
        bool Validate();
        decimal CalculateValue();
        string GetEmailSubject();
        string GetDataType();
        void ProcessData();
    }

    // ✅ MEJORA 2: Strategy Pattern para formateo
    public interface IReportFormatter
    {
        string FormatCustomer(CustomerDataImproved customer);
        string FormatProduct(ProductDataImproved product);
        string FormatSales(SalesDataImproved sales);
    }

    // ✅ MEJORA 3: Formatters específicos implementan Strategy
    public class HtmlFormatter : IReportFormatter
    {
        public string FormatCustomer(CustomerDataImproved customer)
        {
            return $@"
                <div class='customer-report'>
                    <h2>Customer Report</h2>
                    <p>Name: {customer.Name}</p>
                    <p>Email: {customer.Email}</p>
                    <p>Registration: {customer.RegistrationDate:yyyy-MM-dd}</p>
                    <p>Total Purchases: ${customer.TotalPurchases:F2}</p>
                </div>";
        }

        public string FormatProduct(ProductDataImproved product)
        {
            return $@"
                <div class='product-report'>
                    <h2>Product Report</h2>
                    <p>Name: {product.Name}</p>
                    <p>Category: {product.Category}</p>
                    <p>Price: ${product.Price:F2}</p>
                    <p>Stock: {product.StockQuantity}</p>
                </div>";
        }

        public string FormatSales(SalesDataImproved sales)
        {
            return $@"
                <div class='sales-report'>
                    <h2>Sales Report</h2>
                    <p>Date: {sales.SaleDate:yyyy-MM-dd}</p>
                    <p>Customer: {sales.CustomerName}</p>
                    <p>Product: {sales.ProductName}</p>
                    <p>Amount: ${sales.Amount:F2}</p>
                    <p>Quantity: {sales.Quantity}</p>
                </div>";
        }
    }

    public class CsvFormatter : IReportFormatter
    {
        public string FormatCustomer(CustomerDataImproved customer)
        {
            return $"Name,Email,Registration,TotalPurchases\n{customer.Name},{customer.Email},{customer.RegistrationDate:yyyy-MM-dd},{customer.TotalPurchases}";
        }

        public string FormatProduct(ProductDataImproved product)
        {
            return $"Name,Category,Price,Stock\n{product.Name},{product.Category},{product.Price},{product.StockQuantity}";
        }

        public string FormatSales(SalesDataImproved sales)
        {
            return $"Date,Customer,Product,Amount,Quantity\n{sales.SaleDate:yyyy-MM-dd},{sales.CustomerName},{sales.ProductName},{sales.Amount},{sales.Quantity}";
        }
    }

    public class JsonFormatter : IReportFormatter
    {
        public string FormatCustomer(CustomerDataImproved customer)
        {
            return $@"{{
                ""name"": ""{customer.Name}"",
                ""email"": ""{customer.Email}"",
                ""registrationDate"": ""{customer.RegistrationDate:yyyy-MM-dd}"",
                ""totalPurchases"": {customer.TotalPurchases}
            }}";
        }

        public string FormatProduct(ProductDataImproved product)
        {
            return $@"{{
                ""name"": ""{product.Name}"",
                ""category"": ""{product.Category}"",
                ""price"": {product.Price},
                ""stockQuantity"": {product.StockQuantity}
            }}";
        }

        public string FormatSales(SalesDataImproved sales)
        {
            return $@"{{
                ""saleDate"": ""{sales.SaleDate:yyyy-MM-dd}"",
                ""customerName"": ""{sales.CustomerName}"",
                ""productName"": ""{sales.ProductName}"",
                ""amount"": {sales.Amount},
                ""quantity"": {sales.Quantity}
            }}";
        }
    }

    // ✅ MEJORA 4: CustomerData con comportamiento encapsulado
    public class CustomerDataImproved : IReportableData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal TotalPurchases { get; set; }

        public string GenerateReport(IReportFormatter formatter)
        {
            return formatter.FormatCustomer(this);
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Name) && 
                   !string.IsNullOrEmpty(Email) &&
                   TotalPurchases >= 0;
        }

        public decimal CalculateValue()
        {
            // Customer value = total purchases
            return TotalPurchases;
        }

        public string GetEmailSubject()
        {
            return "Customer Report Generated";
        }

        public string GetDataType()
        {
            return "Customer";
        }

        public void ProcessData()
        {
            Console.WriteLine($"Processing customer: {Name}");
        }
    }

    // ✅ MEJORA 5: ProductData con comportamiento encapsulado
    public class ProductDataImproved : IReportableData
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public string GenerateReport(IReportFormatter formatter)
        {
            return formatter.FormatProduct(this);
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Name) && 
                   !string.IsNullOrEmpty(Category) &&
                   Price > 0 &&
                   StockQuantity >= 0;
        }

        public decimal CalculateValue()
        {
            // Product value = price * stock
            return Price * StockQuantity;
        }

        public string GetEmailSubject()
        {
            return "Product Report Generated";
        }

        public string GetDataType()
        {
            return "Product";
        }

        public void ProcessData()
        {
            Console.WriteLine($"Processing product: {Name}");
        }
    }

    // ✅ MEJORA 6: SalesData con comportamiento encapsulado
    public class SalesDataImproved : IReportableData
    {
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }

        public string GenerateReport(IReportFormatter formatter)
        {
            return formatter.FormatSales(this);
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(CustomerName) && 
                   !string.IsNullOrEmpty(ProductName) &&
                   Amount > 0 &&
                   Quantity > 0;
        }

        public decimal CalculateValue()
        {
            // Sale value = amount
            return Amount;
        }

        public string GetEmailSubject()
        {
            return "Sales Report Generated";
        }

        public string GetDataType()
        {
            return "Sales";
        }

        public void ProcessData()
        {
            Console.WriteLine($"Processing sale: {ProductName} to {CustomerName}");
        }
    }

    // ✅ MEJORA 7: Generator simplificado sin type checking
    public class ReportGeneratorImproved
    {
        private readonly Dictionary<string, IReportFormatter> _formatters;

        public ReportGeneratorImproved()
        {
            // ✅ MEJORA 8: Formatters registrados dinámicamente
            _formatters = new Dictionary<string, IReportFormatter>
            {
                ["HTML"] = new HtmlFormatter(),
                ["CSV"] = new CsvFormatter(),
                ["JSON"] = new JsonFormatter()
            };
        }

        /// <summary>
        /// ✅ MEJORA 9: Generación sin type checking - polimorfismo puro
        /// </summary>
        public string GenerateReport(IReportableData data, string format)
        {
            if (!_formatters.TryGetValue(format.ToUpper(), out var formatter))
            {
                throw new NotSupportedException($"Format '{format}' not supported");
            }

            return data.GenerateReport(formatter);
        }

        /// <summary>
        /// ✅ MEJORA 10: Validación polimórfica
        /// </summary>
        public bool ValidateData(IReportableData data)
        {
            return data.Validate();
        }

        /// <summary>
        /// ✅ MEJORA 11: Cálculo polimórfico
        /// </summary>
        public decimal CalculateValue(IReportableData data)
        {
            return data.CalculateValue();
        }

        /// <summary>
        /// ✅ MEJORA 12: Email subject polimórfico
        /// </summary>
        public string GetEmailSubject(IReportableData data)
        {
            return data.GetEmailSubject();
        }

        /// <summary>
        /// ✅ MEJORA 13: Procesamiento en lote simplificado
        /// </summary>
        public void ProcessDataBatch(IEnumerable<IReportableData> dataItems, string format)
        {
            foreach (var item in dataItems)
            {
                // ✅ Sin type checking - todo polimórfico
                item.ProcessData();
                
                if (item.Validate())
                {
                    var report = GenerateReport(item, format);
                    var subject = GetEmailSubject(item);
                    var value = CalculateValue(item);
                    
                    Console.WriteLine($"Generated report for {item.GetDataType()} (Value: ${value:F2})");
                    // TODO: Send email with subject and report
                }
                else
                {
                    Console.WriteLine($"Invalid {item.GetDataType()} data");
                }
            }
        }

        /// <summary>
        /// ✅ MEJORA 14: Análisis estadístico polimórfico
        /// </summary>
        public Dictionary<string, int> AnalyzeDataTypes(IEnumerable<IReportableData> dataItems)
        {
            return dataItems
                .GroupBy(item => item.GetDataType())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// ✅ MEJORA 15: Registro dinámico de nuevos formatters
        /// </summary>
        public void RegisterFormatter(string format, IReportFormatter formatter)
        {
            _formatters[format.ToUpper()] = formatter;
        }

        /// <summary>
        /// ✅ MEJORA 16: Obtener formatters disponibles
        /// </summary>
        public string[] GetAvailableFormats()
        {
            return _formatters.Keys.ToArray();
        }
    }

    // ✅ MEJORA 17: Ejemplo de extensión - nuevo tipo de data
    public class InventoryDataImproved : IReportableData
    {
        public string WarehouseName { get; set; }
        public Dictionary<string, int> ProductCounts { get; set; } = new();
        public DateTime LastUpdated { get; set; }

        public string GenerateReport(IReportFormatter formatter)
        {
            // Para un nuevo tipo, el formatter necesita extension
            // O podemos implementar un comportamiento por defecto
            var totalItems = ProductCounts.Values.Sum();
            return $@"
                <div class='inventory-report'>
                    <h2>Inventory Report</h2>
                    <p>Warehouse: {WarehouseName}</p>
                    <p>Total Items: {totalItems}</p>
                    <p>Last Updated: {LastUpdated:yyyy-MM-dd HH:mm}</p>
                </div>";
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(WarehouseName) && 
                   ProductCounts.Count > 0;
        }

        public decimal CalculateValue()
        {
            // Inventory value = total items count (simplified)
            return ProductCounts.Values.Sum();
        }

        public string GetEmailSubject()
        {
            return "Inventory Report Generated";
        }

        public string GetDataType()
        {
            return "Inventory";
        }

        public void ProcessData()
        {
            Console.WriteLine($"Processing inventory: {WarehouseName}");
        }
    }

    // ✅ MEJORA 18: Factory para crear datos de diferentes tipos
    public static class ReportDataFactory
    {
        private static readonly Dictionary<string, Func<IReportableData>> DataCreators = new()
        {
            ["Customer"] = () => new CustomerDataImproved(),
            ["Product"] = () => new ProductDataImproved(),
            ["Sales"] = () => new SalesDataImproved(),
            ["Inventory"] = () => new InventoryDataImproved()
        };

        public static IReportableData CreateData(string dataType)
        {
            if (DataCreators.TryGetValue(dataType, out var creator))
            {
                return creator();
            }
            
            throw new ArgumentException($"Unknown data type: {dataType}");
        }

        public static void RegisterDataType(string dataType, Func<IReportableData> creator)
        {
            DataCreators[dataType] = creator;
        }

        public static string[] GetAvailableDataTypes() => DataCreators.Keys.ToArray();
    }

    /*
    COMPARACIÓN DE MÉTRICAS:

    ANTES (ReportGeneratorProblematic):
    ❌ Type checking: 6+ métodos con "if (x is Type)"
    ❌ Casting unsafe: 20+ lugares
    ❌ Complejidad ciclomática: 8-12 por método
    ❌ Duplicación: 3 implementaciones × 3 formatos
    ❌ Mantenimiento: Muy difícil - cambios en 6+ lugares
    ❌ Extensibilidad: Requiere modificar todo el código

    DESPUÉS (ReportGeneratorImproved + Polimorfismo):
    ✅ Type checking: 0 - todo polimórfico
    ✅ Casting: 0 - type safety garantizado
    ✅ Complejidad ciclomática: 1-2 por método
    ✅ Duplicación: 0 - comportamiento encapsulado
    ✅ Mantenimiento: Fácil - cambios aislados por clase
    ✅ Extensibilidad: Factory pattern + interfaces

    BENEFICIOS DEL POLIMORFISMO:

    1. ELIMINACIÓN DE TYPE CHECKING:
       - No más "if (x is Type)" patterns
       - Virtual methods manejan comportamiento
       - Compile-time type safety

    2. ENCAPSULACIÓN POR TIPO:
       - Cada clase conoce su comportamiento
       - Lógica relacionada agrupada
       - Single Responsibility aplicado

    3. EXTENSIBILIDAD:
       - Nuevos tipos sin modificar código existente
       - Factory pattern para creación dinámica
       - Open/Closed Principle cumplido

    4. MANTENIBILIDAD:
       - Cambios aislados por clase
       - Testing independiente por tipo
       - Código auto-documentado

    5. PERFORMANCE:
       - No casting ni boxing/unboxing
       - Dispatch polimórfico eficiente
       - Memory layout optimizado

    CUÁNDO USAR POLIMORFISMO:

    ✅ USAR cuando:
    - Múltiples tipos con comportamiento similar
    - Lógica que varía por tipo de objeto
    - Necesitas extensibilidad sin modificar código
    - Hierarchy natural de clases

    ❌ NO USAR cuando:
    - Solo 2-3 tipos muy simples
    - Comportamiento idéntico entre tipos
    - Performance crítica (rare)
    - Tipos de datos primitivos simples

    LECCIONES CLAVE:
    - Interface común permite polimorfismo
    - Cada clase encapsula su comportamiento específico
    - Strategy Pattern complementa polimorfismo
    - Factory Pattern facilita extensión
    - Testing se simplifica dramáticamente

    PRÓXIMA CLASE:
    Clase 4: Refactoring Avanzado - Aplicando todos estos patterns
    */
}