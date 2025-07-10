using System;
using System.Collections.Generic;

namespace TecnicasNoIf.Ejercicios
{
    /// <summary>
    /// PROBLEMA: Type checking excesivo en lugar de polimorfismo
    /// 
    /// ISSUES IDENTIFICADOS:
    /// ❌ PROBLEMA 1: instanceof/is checks por todos lados
    /// ❌ PROBLEMA 2: Switch en tipo de objeto en lugar de comportamiento
    /// ❌ PROBLEMA 3: Casting unsafe y repetitivo
    /// ❌ PROBLEMA 4: Violación Open/Closed Principle
    /// ❌ PROBLEMA 5: Duplicación de lógica similar en múltiples lugares
    /// ❌ PROBLEMA 6: No aprovecha el polimorfismo de C#
    /// </summary>

    /// <summary>
    /// Sistema de reportes con type checking problemático
    /// </summary>
    public class ReportGeneratorProblematic
    {
        // ❌ PROBLEMA 1: Clases planas sin jerarquía polimórfica
        public class CustomerData
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime RegistrationDate { get; set; }
            public decimal TotalPurchases { get; set; }
        }

        public class ProductData
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
        }

        public class SalesData
        {
            public DateTime SaleDate { get; set; }
            public string CustomerName { get; set; }
            public string ProductName { get; set; }
            public decimal Amount { get; set; }
            public int Quantity { get; set; }
        }

        /// <summary>
        /// Genera reporte con type checking masivo - ANTI-PATTERN
        /// </summary>
        public string GenerateReport(object data, string format)
        {
            // ❌ PROBLEMA 2: Type checking en lugar de polimorfismo
            if (data is CustomerData)
            {
                var customer = (CustomerData)data; // ❌ PROBLEMA 3: Casting unsafe
                
                if (format == "HTML")
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
                else if (format == "CSV")
                {
                    return $"Name,Email,Registration,TotalPurchases\n{customer.Name},{customer.Email},{customer.RegistrationDate:yyyy-MM-dd},{customer.TotalPurchases}";
                }
                else if (format == "JSON")
                {
                    return $@"{{
                        ""name"": ""{customer.Name}"",
                        ""email"": ""{customer.Email}"",
                        ""registrationDate"": ""{customer.RegistrationDate:yyyy-MM-dd}"",
                        ""totalPurchases"": {customer.TotalPurchases}
                    }}";
                }
            }
            else if (data is ProductData)
            {
                var product = (ProductData)data; // ❌ Más casting unsafe
                
                if (format == "HTML")
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
                else if (format == "CSV")
                {
                    return $"Name,Category,Price,Stock\n{product.Name},{product.Category},{product.Price},{product.StockQuantity}";
                }
                else if (format == "JSON")
                {
                    return $@"{{
                        ""name"": ""{product.Name}"",
                        ""category"": ""{product.Category}"",
                        ""price"": {product.Price},
                        ""stockQuantity"": {product.StockQuantity}
                    }}";
                }
            }
            else if (data is SalesData)
            {
                var sale = (SalesData)data; // ❌ Más casting unsafe
                
                if (format == "HTML")
                {
                    return $@"
                        <div class='sales-report'>
                            <h2>Sales Report</h2>
                            <p>Date: {sale.SaleDate:yyyy-MM-dd}</p>
                            <p>Customer: {sale.CustomerName}</p>
                            <p>Product: {sale.ProductName}</p>
                            <p>Amount: ${sale.Amount:F2}</p>
                            <p>Quantity: {sale.Quantity}</p>
                        </div>";
                }
                else if (format == "CSV")
                {
                    return $"Date,Customer,Product,Amount,Quantity\n{sale.SaleDate:yyyy-MM-dd},{sale.CustomerName},{sale.ProductName},{sale.Amount},{sale.Quantity}";
                }
                else if (format == "JSON")
                {
                    return $@"{{
                        ""saleDate"": ""{sale.SaleDate:yyyy-MM-dd}"",
                        ""customerName"": ""{sale.CustomerName}"",
                        ""productName"": ""{sale.ProductName}"",
                        ""amount"": {sale.Amount},
                        ""quantity"": {sale.Quantity}
                    }}";
                }
            }

            throw new NotSupportedException($"Data type {data.GetType()} not supported");
        }

        /// <summary>
        /// Validación con más type checking - DUPLICACIÓN
        /// </summary>
        public bool ValidateData(object data)
        {
            // ❌ PROBLEMA 4: Mismo pattern de type checking repetido
            if (data is CustomerData customer)
            {
                return !string.IsNullOrEmpty(customer.Name) && 
                       !string.IsNullOrEmpty(customer.Email) &&
                       customer.TotalPurchases >= 0;
            }
            else if (data is ProductData product)
            {
                return !string.IsNullOrEmpty(product.Name) && 
                       !string.IsNullOrEmpty(product.Category) &&
                       product.Price > 0 &&
                       product.StockQuantity >= 0;
            }
            else if (data is SalesData sale)
            {
                return !string.IsNullOrEmpty(sale.CustomerName) && 
                       !string.IsNullOrEmpty(sale.ProductName) &&
                       sale.Amount > 0 &&
                       sale.Quantity > 0;
            }

            return false;
        }

        /// <summary>
        /// Cálculos con más type checking - TRIPLETS
        /// </summary>
        public decimal CalculateValue(object data)
        {
            // ❌ PROBLEMA 5: Tercera repetición del mismo pattern
            if (data is CustomerData customer)
            {
                // Customer value = total purchases
                return customer.TotalPurchases;
            }
            else if (data is ProductData product)
            {
                // Product value = price * stock
                return product.Price * product.StockQuantity;
            }
            else if (data is SalesData sale)
            {
                // Sale value = amount
                return sale.Amount;
            }

            throw new NotSupportedException($"Cannot calculate value for {data.GetType()}");
        }

        /// <summary>
        /// Envío de emails con type checking - CUARTA REPETICIÓN
        /// </summary>
        public string GetEmailSubject(object data)
        {
            // ❌ PROBLEMA 6: Cuarta vez el mismo pattern - violación DRY
            if (data is CustomerData)
            {
                return "Customer Report Generated";
            }
            else if (data is ProductData)
            {
                return "Product Report Generated";
            }
            else if (data is SalesData)
            {
                return "Sales Report Generated";
            }

            return "Report Generated";
        }

        /// <summary>
        /// Procesamiento en lote - COMPLEJIDAD EXPLOSIVA
        /// </summary>
        public void ProcessDataBatch(IEnumerable<object> dataItems, string format)
        {
            foreach (var item in dataItems)
            {
                // ❌ PROBLEMA: Type checking anidado en loops
                if (item is CustomerData customer)
                {
                    Console.WriteLine($"Processing customer: {customer.Name}");
                    if (ValidateData(customer))
                    {
                        var report = GenerateReport(customer, format);
                        var subject = GetEmailSubject(customer);
                        var value = CalculateValue(customer);
                        
                        Console.WriteLine($"Generated report for {customer.Name} (Value: ${value:F2})");
                        // TODO: Send email with subject and report
                    }
                    else
                    {
                        Console.WriteLine($"Invalid customer data: {customer.Name}");
                    }
                }
                else if (item is ProductData product)
                {
                    Console.WriteLine($"Processing product: {product.Name}");
                    if (ValidateData(product))
                    {
                        var report = GenerateReport(product, format);
                        var subject = GetEmailSubject(product);
                        var value = CalculateValue(product);
                        
                        Console.WriteLine($"Generated report for {product.Name} (Value: ${value:F2})");
                        // TODO: Send email with subject and report
                    }
                    else
                    {
                        Console.WriteLine($"Invalid product data: {product.Name}");
                    }
                }
                else if (item is SalesData sale)
                {
                    Console.WriteLine($"Processing sale: {sale.ProductName} to {sale.CustomerName}");
                    if (ValidateData(sale))
                    {
                        var report = GenerateReport(sale, format);
                        var subject = GetEmailSubject(sale);
                        var value = CalculateValue(sale);
                        
                        Console.WriteLine($"Generated report for sale (Value: ${value:F2})");
                        // TODO: Send email with subject and report
                    }
                    else
                    {
                        Console.WriteLine($"Invalid sale data");
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown data type: {item.GetType()}");
                }
            }
        }

        /// <summary>
        /// Análisis estadístico - MÁS TYPE CHECKING
        /// </summary>
        public Dictionary<string, int> AnalyzeDataTypes(IEnumerable<object> dataItems)
        {
            var statistics = new Dictionary<string, int>
            {
                ["Customers"] = 0,
                ["Products"] = 0,
                ["Sales"] = 0,
                ["Unknown"] = 0
            };

            foreach (var item in dataItems)
            {
                // ❌ PROBLEMA: Misma lógica de type checking una vez más
                if (item is CustomerData)
                {
                    statistics["Customers"]++;
                }
                else if (item is ProductData)
                {
                    statistics["Products"]++;
                }
                else if (item is SalesData)
                {
                    statistics["Sales"]++;
                }
                else
                {
                    statistics["Unknown"]++;
                }
            }

            return statistics;
        }
    }

    /*
    PROBLEMAS PRINCIPALES:

    1. TYPE CHECKING EXCESIVO:
       - 6+ métodos con "if (x is Type)" repetidos
       - Misma estructura de decisión en cada método
       - Pattern matching manual en lugar de polimorfismo

    2. CASTING UNSAFE:
       - (CustomerData)data repetido sin null checks
       - No aprovecha pattern matching de C# moderno
       - Riesgo de InvalidCastException

    3. VIOLACIÓN OPEN/CLOSED:
       - Agregar nuevo tipo de data = modificar TODOS los métodos
       - No extensible sin tocar código existente
       - Frágil ante cambios

    4. DUPLICACIÓN MASIVA:
       - Misma lógica de decisión en 6+ lugares
       - Tres implementaciones del mismo formato (HTML/CSV/JSON)
       - Validaciones similares repetidas

    5. MANTENIMIENTO IMPOSIBLE:
       - Cambiar comportamiento = buscar en 6+ métodos
       - Inconsistencias fáciles de introducir
       - Testing requiere muchos scenarios

    6. NO APROVECHA OOP:
       - Objects como data containers sin comportamiento
       - Lógica centralizada en lugar de distribuida
       - Missing encapsulation

    OBJETIVO DEL EJERCICIO:
    Refactorizar usando Polimorfismo para:
    ✅ Eliminar type checking con virtual methods
    ✅ Encapsular comportamiento en las clases
    ✅ Aplicar Open/Closed Principle
    ✅ Centralizar lógica por tipo
    ✅ Simplificar testing con herencia

    TIEMPO ESTIMADO: 45 minutos
    DIFICULTAD: Intermedio-Avanzado
    */
}