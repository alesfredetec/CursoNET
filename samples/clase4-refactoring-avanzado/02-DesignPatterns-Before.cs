using System;
using System.Collections.Generic;

namespace RefactoringAvanzado.Ejercicios
{
    /// <summary>
    /// PROBLEMA: Código procedural sin design patterns
    /// 
    /// ISSUES IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Creación de objetos hardcoded sin Factory
    /// ❌ PROBLEMA 2: Notificaciones hardcoded sin Observer
    /// ❌ PROBLEMA 3: Operaciones sin encapsular (no Command pattern)
    /// ❌ PROBLEMA 4: Configuración estática sin Builder
    /// ❌ PROBLEMA 5: No separation of concerns
    /// ❌ PROBLEMA 6: Difícil testing y extensión
    /// </summary>

    // ❌ PROBLEMA 1: Creación de reportes sin Factory Pattern
    public class ReportGeneratorProblematic
    {
        public object GenerateReport(string reportType, Dictionary<string, object> data)
        {
            // ❌ PROBLEMA: Switch para creación - violación Open/Closed
            if (reportType == "PDF")
            {
                var pdfReport = new PdfReport();
                pdfReport.Title = data["title"].ToString();
                pdfReport.Content = data["content"].ToString();
                pdfReport.Author = data.ContainsKey("author") ? data["author"].ToString() : "Unknown";
                pdfReport.CreatedDate = DateTime.Now;
                pdfReport.Watermark = "CONFIDENTIAL";
                pdfReport.FontSize = 12;
                pdfReport.Orientation = "Portrait";
                
                // ❌ Lógica específica hardcoded
                pdfReport.GeneratePdf();
                return pdfReport;
            }
            else if (reportType == "EXCEL")
            {
                var excelReport = new ExcelReport();
                excelReport.Title = data["title"].ToString();
                excelReport.Data = (List<Dictionary<string, object>>)data["data"];
                excelReport.SheetName = data.ContainsKey("sheetName") ? data["sheetName"].ToString() : "Sheet1";
                excelReport.IncludeCharts = true;
                excelReport.AutoFitColumns = true;
                
                // ❌ Más lógica hardcoded
                excelReport.GenerateExcel();
                return excelReport;
            }
            else if (reportType == "JSON")
            {
                var jsonReport = new JsonReport();
                jsonReport.Data = data;
                jsonReport.Formatted = true;
                jsonReport.IncludeMetadata = true;
                
                jsonReport.GenerateJson();
                return jsonReport;
            }
            
            throw new ArgumentException($"Report type {reportType} not supported");
        }
    }

    // ❌ PROBLEMA 2: Sistema de notificaciones sin Observer Pattern
    public class OrderProcessorProblematic
    {
        public void ProcessOrder(Order order)
        {
            // Procesar orden
            order.Status = "Processing";
            order.ProcessedDate = DateTime.Now;
            
            // ❌ PROBLEMA: Notificaciones hardcoded - tight coupling
            SendEmailToCustomer(order);
            SendEmailToWarehouse(order);
            UpdateInventory(order);
            LogOrderProcessed(order);
            UpdateAnalytics(order);
            SendSMSNotification(order);
            
            // ❌ Si falla una notificación, pueden fallar todas
            // ❌ No hay forma de agregar nuevas notificaciones sin modificar código
            
            order.Status = "Completed";
        }
        
        private void SendEmailToCustomer(Order order)
        {
            Console.WriteLine($"Email sent to customer for order {order.Id}");
            // ❌ Email logic hardcoded
        }
        
        private void SendEmailToWarehouse(Order order)
        {
            Console.WriteLine($"Email sent to warehouse for order {order.Id}");
        }
        
        private void UpdateInventory(Order order)
        {
            Console.WriteLine($"Inventory updated for order {order.Id}");
        }
        
        private void LogOrderProcessed(Order order)
        {
            Console.WriteLine($"Order {order.Id} logged");
        }
        
        private void UpdateAnalytics(Order order)
        {
            Console.WriteLine($"Analytics updated for order {order.Id}");
        }
        
        private void SendSMSNotification(Order order)
        {
            Console.WriteLine($"SMS sent for order {order.Id}");
        }
    }

    // ❌ PROBLEMA 3: Operaciones sin Command Pattern
    public class DatabaseManagerProblematic
    {
        public void ExecuteOperations(List<string> operations, string connectionString)
        {
            foreach (var operation in operations)
            {
                // ❌ PROBLEMA: No encapsulación de comandos
                if (operation.StartsWith("CREATE"))
                {
                    Console.WriteLine($"Creating: {operation}");
                    // Execute CREATE
                }
                else if (operation.StartsWith("UPDATE"))
                {
                    Console.WriteLine($"Updating: {operation}");
                    // Execute UPDATE
                }
                else if (operation.StartsWith("DELETE"))
                {
                    Console.WriteLine($"Deleting: {operation}");
                    // Execute DELETE
                }
                
                // ❌ No hay undo capability
                // ❌ No hay logging de comandos
                // ❌ No hay retry logic
                // ❌ No hay transaction management
            }
        }
        
        public void UndoLastOperation()
        {
            // ❌ IMPOSIBLE: No hay historial de comandos
            throw new NotImplementedException("Undo not supported");
        }
    }

    // ❌ PROBLEMA 4: Configuración compleja sin Builder Pattern
    public class EmailServiceProblematic
    {
        public void SendComplexEmail(
            string to,
            string subject,
            string body,
            List<string> attachments = null,
            string from = null,
            string replyTo = null,
            bool isHtml = false,
            string priority = "Normal",
            bool requestReadReceipt = false,
            string smtpServer = null,
            int smtpPort = 587,
            string username = null,
            string password = null,
            bool enableSsl = true,
            List<string> cc = null,
            List<string> bcc = null,
            Dictionary<string, string> headers = null,
            int timeoutMinutes = 5)
        {
            // ❌ PROBLEMA: Constructor con 18 parámetros
            // ❌ Difícil de leer y mantener
            // ❌ Fácil confundir parámetros
            // ❌ No validación de configuración
            
            Console.WriteLine($"Sending email to: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"SMTP: {smtpServer}:{smtpPort}");
            
            // Simulate email sending
        }
        
        // ❌ Multiple overloads para diferentes scenarios
        public void SendSimpleEmail(string to, string subject, string body)
        {
            SendComplexEmail(to, subject, body);
        }
        
        public void SendEmailWithAttachment(string to, string subject, string body, string attachment)
        {
            SendComplexEmail(to, subject, body, new List<string> { attachment });
        }
        
        // ❌ Más overloads = más complejidad
    }

    // ❌ PROBLEMA 5: Workflow sin Template Method Pattern
    public class DataImportProcessorProblematic
    {
        public void ImportCsvData(string filePath)
        {
            // ❌ PROBLEMA: Código duplicado en múltiples métodos
            
            // Step 1: Validate file
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");
            
            // Step 2: Read data
            var lines = File.ReadAllLines(filePath);
            Console.WriteLine($"Read {lines.Length} lines from CSV");
            
            // Step 3: Parse CSV specific
            var data = new List<Dictionary<string, object>>();
            var headers = lines[0].Split(',');
            
            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                var row = new Dictionary<string, object>();
                for (int j = 0; j < headers.Length; j++)
                {
                    row[headers[j]] = values[j];
                }
                data.Add(row);
            }
            
            // Step 4: Validate data
            foreach (var row in data)
            {
                if (row.Count != headers.Length)
                    throw new InvalidDataException("Invalid row data");
            }
            
            // Step 5: Transform data
            foreach (var row in data)
            {
                // CSV specific transformations
                foreach (var key in row.Keys.ToList())
                {
                    if (row[key].ToString().Contains("\""))
                    {
                        row[key] = row[key].ToString().Replace("\"", "");
                    }
                }
            }
            
            // Step 6: Save to database
            Console.WriteLine($"Saving {data.Count} records to database");
            
            // Step 7: Generate report
            Console.WriteLine("CSV import completed successfully");
        }
        
        public void ImportJsonData(string filePath)
        {
            // ❌ PROBLEMA: Mismos pasos pero código duplicado
            
            // Step 1: Validate file (DUPLICATED)
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");
            
            // Step 2: Read data (DUPLICATED)
            var content = File.ReadAllText(filePath);
            Console.WriteLine($"Read JSON file: {filePath}");
            
            // Step 3: Parse JSON specific
            var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
            
            // Step 4: Validate data (SIMILAR)
            if (data == null || data.Count == 0)
                throw new InvalidDataException("No data found in JSON");
            
            // Step 5: Transform data (DIFFERENT)
            foreach (var row in data)
            {
                // JSON specific transformations
                foreach (var key in row.Keys.ToList())
                {
                    if (row[key] is DateTime dt)
                    {
                        row[key] = dt.ToString("yyyy-MM-dd");
                    }
                }
            }
            
            // Step 6: Save to database (DUPLICATED)
            Console.WriteLine($"Saving {data.Count} records to database");
            
            // Step 7: Generate report (DUPLICATED)
            Console.WriteLine("JSON import completed successfully");
        }
        
        public void ImportXmlData(string filePath)
        {
            // ❌ MÁS DUPLICACIÓN del mismo workflow
            // ... código similar con pequeñas variaciones
        }
    }

    // ❌ PROBLEMA 6: Sin Strategy para algoritmos
    public class PricingCalculatorProblematic
    {
        public decimal CalculatePrice(decimal basePrice, string customerType, int quantity, string season)
        {
            decimal finalPrice = basePrice;
            
            // ❌ PROBLEMA: Nested ifs para diferentes algoritmos
            if (customerType == "PREMIUM")
            {
                if (season == "HIGH")
                {
                    finalPrice = basePrice * 1.2m; // Premium high season
                    if (quantity > 100)
                        finalPrice = finalPrice * 0.95m; // Volume discount
                }
                else
                {
                    finalPrice = basePrice * 1.1m; // Premium normal season
                    if (quantity > 50)
                        finalPrice = finalPrice * 0.90m; // Volume discount
                }
            }
            else if (customerType == "STANDARD")
            {
                if (season == "HIGH")
                {
                    finalPrice = basePrice * 1.1m; // Standard high season
                    if (quantity > 200)
                        finalPrice = finalPrice * 0.98m; // Small volume discount
                }
                else
                {
                    finalPrice = basePrice; // Standard normal season
                    if (quantity > 100)
                        finalPrice = finalPrice * 0.95m; // Volume discount
                }
            }
            else if (customerType == "WHOLESALE")
            {
                if (season == "HIGH")
                {
                    finalPrice = basePrice * 0.8m; // Wholesale high season
                    if (quantity > 1000)
                        finalPrice = finalPrice * 0.90m; // Large volume discount
                }
                else
                {
                    finalPrice = basePrice * 0.7m; // Wholesale normal season
                    if (quantity > 500)
                        finalPrice = finalPrice * 0.85m; // Volume discount
                }
            }
            
            return finalPrice;
        }
    }

    // Supporting classes
    public class PdfReport
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Watermark { get; set; }
        public int FontSize { get; set; }
        public string Orientation { get; set; }
        
        public void GeneratePdf() { /* PDF generation logic */ }
    }

    public class ExcelReport
    {
        public string Title { get; set; }
        public List<Dictionary<string, object>> Data { get; set; }
        public string SheetName { get; set; }
        public bool IncludeCharts { get; set; }
        public bool AutoFitColumns { get; set; }
        
        public void GenerateExcel() { /* Excel generation logic */ }
    }

    public class JsonReport
    {
        public Dictionary<string, object> Data { get; set; }
        public bool Formatted { get; set; }
        public bool IncludeMetadata { get; set; }
        
        public void GenerateJson() { /* JSON generation logic */ }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime ProcessedDate { get; set; }
        public decimal Total { get; set; }
        public string CustomerEmail { get; set; }
    }

    /*
    PROBLEMAS PRINCIPALES:

    1. NO FACTORY PATTERN:
       - Creación de objetos hardcoded
       - Switch statements para tipos
       - Violación Open/Closed Principle

    2. NO OBSERVER PATTERN:
       - Notificaciones hardcoded
       - Tight coupling entre componentes
       - Difícil agregar nuevas notificaciones

    3. NO COMMAND PATTERN:
       - Operaciones no encapsuladas
       - Sin undo/redo capability
       - Sin logging ni retry logic

    4. NO BUILDER PATTERN:
       - Constructores con muchos parámetros
       - Configuración compleja y propensa a errores
       - Multiple overloads confusos

    5. NO TEMPLATE METHOD:
       - Código duplicado en workflows similares
       - Difícil mantener pasos comunes
       - No reutilización de algoritmos

    6. NO STRATEGY PATTERN:
       - Algoritmos hardcoded con if/else
       - Difícil agregar nuevos algoritmos
       - Lógica de negocio mezclada

    OBJETIVO DEL EJERCICIO:
    Refactorizar aplicando Design Patterns para:
    ✅ Factory: Creación flexible de objetos
    ✅ Observer: Notificaciones desacopladas
    ✅ Command: Operaciones encapsuladas
    ✅ Builder: Configuración compleja simplificada
    ✅ Template Method: Workflows reutilizables
    ✅ Strategy: Algoritmos intercambiables

    TIEMPO ESTIMADO: 75 minutos
    DIFICULTAD: Avanzado
    */
}