# EJERCICIO 3: Análisis Profundo de Métricas - Nivel Semi-Senior

## Objetivos Estratégicos

Este ejercicio desarrolla competencias en análisis cuantitativo de código, implementación de métricas empresariales y establecimiento de procesos de mejora continua basados en datos objetivos para sistemas críticos.

---

## Fundamentos de Métricas de Software

### Taxonomía de Métricas en Sistemas Empresariales

#### 1. Métricas de Complejidad Estructural

**Complejidad Ciclomática (McCabe)**
- **Definición**: Número de caminos linealmente independientes a través del código
- **Cálculo**: E - N + 2P (Edges - Nodes + 2 × Connected Components)
- **Threshold Empresarial**: ≤10 para métodos críticos, ≤15 para métodos estándar

**Complejidad Cognitiva (SonarSource)**
- **Definición**: Medida de dificultad de comprensión del código
- **Factores**: Anidamiento, saltos no lineales, recursión
- **Threshold**: ≤15 para métodos en sistemas financieros

```csharp
// Ejemplo: Análisis de complejidad en sistema de pagos
public class PaymentProcessor
{
    // Complejidad Ciclomática: 12, Complejidad Cognitiva: 18
    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        // +1 CC, +1 CogC - if statement
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        
        // +1 CC, +1 CogC - if statement
        if (request.Amount <= 0)
            return PaymentResult.Failed("Invalid amount");
        
        // +1 CC, +1 CogC - if statement
        if (string.IsNullOrEmpty(request.MerchantId))
            return PaymentResult.Failed("Merchant ID required");
        
        // +1 CC, +2 CogC - if statement with &&
        if (request.CardNumber == null && request.Token == null)
            return PaymentResult.Failed("Payment method required");
        
        // +1 CC, +1 CogC - if statement, +1 CogC - nested
        if (request.IsRecurring)
        {
            // +1 CC, +2 CogC - if statement (nested +1)
            if (!ValidateRecurringSetup(request))
                return PaymentResult.Failed("Invalid recurring setup");
            
            // +1 CC, +2 CogC - if statement (nested +1)
            if (request.RecurringFrequency == null)
                return PaymentResult.Failed("Recurring frequency required");
        }
        
        // +3 CC, +3 CogC - switch statement
        switch (request.Currency)
        {
            case "USD":
                return ProcessUSDPayment(request);
            case "EUR":
                return ProcessEURPayment(request);
            case "GBP":
                return ProcessGBPPayment(request);
            default:
                return PaymentResult.Failed("Unsupported currency");
        }
    }
}
```

#### 2. Métricas de Acoplamiento y Cohesión

**Afferent Coupling (Ca)**
- **Definición**: Número de clases externas que dependen de esta clase
- **Impacto**: Alta Ca indica alta responsabilidad y riesgo de cambios

**Efferent Coupling (Ce)**
- **Definición**: Número de clases externas de las que depende esta clase
- **Impacto**: Alta Ce indica alta dependencia y fragilidad

**Instability (I = Ce / (Ca + Ce))**
- **Rango**: 0 (máxima estabilidad) a 1 (máxima inestabilidad)
- **Objetivo**: Componentes abstractos con I→0, componentes concretos con I→1

```csharp
// Ejemplo: Análisis de acoplamiento en microservicio
namespace PaymentProcessor.Core.Services
{
    // Ca = 5 (usado por Controllers, Tests, Adapters, etc.)
    // Ce = 8 (usa Repository, Logger, Configuration, etc.)
    // I = 8/(5+8) = 0.62 (moderadamente inestable)
    public class PaymentService
    {
        private readonly IPaymentRepository _repository;        // +1 Ce
        private readonly ILogger<PaymentService> _logger;       // +1 Ce
        private readonly IConfiguration _configuration;         // +1 Ce
        private readonly IPaymentValidator _validator;          // +1 Ce
        private readonly INotificationService _notifications;   // +1 Ce
        private readonly IFraudDetectionService _fraudService; // +1 Ce
        private readonly IAuditService _auditService;           // +1 Ce
        private readonly IMetricsCollector _metrics;            // +1 Ce
        
        // Múltiples dependencias indican alta complejidad de integración
    }
}
```

#### 3. Métricas de Mantenibilidad

**Maintainability Index (MI)**
- **Fórmula**: 171 - 5.2 × ln(HalsteadVolume) - 0.23 × CyclomaticComplexity - 16.2 × ln(LinesOfCode)
- **Interpretación**: 0-9 (Baja), 10-19 (Moderada), 20-100 (Alta)

**Technical Debt Ratio**
- **Cálculo**: (Remediation Cost / Development Cost) × 100
- **Benchmark**: <5% para sistemas críticos, <10% para sistemas estándar

---

## Herramientas de Análisis Empresarial

### 1. SonarQube Enterprise

```xml
<!-- sonar-project.properties -->
# Project identification
sonar.projectKey=fintech:payment-processor
sonar.organization=fintech-corp
sonar.projectName=Payment Processor Microservice

# Source code location
sonar.sources=src
sonar.tests=tests
sonar.exclusions=**/bin/**,**/obj/**,**/Migrations/**

# Quality Gate configuration
sonar.qualitygate.wait=true
sonar.qualitygate.timeout=300

# Coverage settings
sonar.cs.opencover.reportsPaths=coverage.opencover.xml
sonar.coverage.exclusions=**/Program.cs,**/Startup.cs,**/Migrations/**

# Duplication settings
sonar.cpd.exclusions=**/Models/**,**/DTOs/**

# Custom metrics thresholds
sonar.issue.ignore.multicriteria=c1,c2,c3
sonar.issue.ignore.multicriteria.c1.ruleKey=csharpsquid:S1541
sonar.issue.ignore.multicriteria.c1.resourceKey=**/Legacy/**
sonar.issue.ignore.multicriteria.c2.ruleKey=csharpsquid:S138
sonar.issue.ignore.multicriteria.c2.resourceKey=**/Generated/**
sonar.issue.ignore.multicriteria.c3.ruleKey=csharpsquid:S1067
sonar.issue.ignore.multicriteria.c3.resourceKey=**/Specifications/**
```

### 2. NDepend Analysis

```xml
<!-- NDepend.config -->
<NDepend>
  <Projects>
    <Project>
      <Name>PaymentProcessor</Name>
      <Assemblies>
        <Assembly>PaymentProcessor.Core.dll</Assembly>
        <Assembly>PaymentProcessor.Infrastructure.dll</Assembly>
        <Assembly>PaymentProcessor.API.dll</Assembly>
      </Assemblies>
      <Queries>
        <Query>
          // Methods with high cyclomatic complexity
          from m in Methods 
          where m.CyclomaticComplexity > 10
          orderby m.CyclomaticComplexity descending
          select new { m, m.CyclomaticComplexity }
        </Query>
        <Query>
          // Classes with high afferent coupling
          from t in Types
          where t.AfferentCoupling > 20
          orderby t.AfferentCoupling descending
          select new { t, t.AfferentCoupling }
        </Query>
      </Queries>
    </Project>
  </Projects>
</NDepend>
```

### 3. Métricas Personalizadas con Roslyn

```csharp
// PaymentProcessor.Analyzers/ComplexityAnalyzer.cs
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class FinancialComplexityAnalyzer : DiagnosticAnalyzer
{
    public static readonly DiagnosticDescriptor HighComplexityRule = new DiagnosticDescriptor(
        "FC001",
        "Financial method complexity too high",
        "Method '{0}' has complexity of {1}, which exceeds the financial domain threshold of {2}",
        "Maintainability",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => 
        ImmutableArray.Create(HighComplexityRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeMethod, SyntaxKind.MethodDeclaration);
    }

    private static void AnalyzeMethod(SyntaxNodeAnalysisContext context)
    {
        var method = (MethodDeclarationSyntax)context.Node;
        var complexity = CalculateComplexity(method);
        
        // Financial domain: stricter thresholds
        var threshold = IsFinancialMethod(method) ? 8 : 10;
        
        if (complexity > threshold)
        {
            var diagnostic = Diagnostic.Create(
                HighComplexityRule,
                method.Identifier.GetLocation(),
                method.Identifier.ValueText,
                complexity,
                threshold);
            
            context.ReportDiagnostic(diagnostic);
        }
    }

    private static int CalculateComplexity(MethodDeclarationSyntax method)
    {
        var walker = new ComplexityWalker();
        walker.Visit(method);
        return walker.Complexity;
    }

    private static bool IsFinancialMethod(MethodDeclarationSyntax method)
    {
        var methodName = method.Identifier.ValueText.ToLower();
        return methodName.Contains("payment") || 
               methodName.Contains("transaction") || 
               methodName.Contains("calculate") ||
               methodName.Contains("validate");
    }
}

internal class ComplexityWalker : CSharpSyntaxWalker
{
    public int Complexity { get; private set; } = 1;

    public override void VisitIfStatement(IfStatementSyntax node)
    {
        Complexity++;
        base.VisitIfStatement(node);
    }

    public override void VisitSwitchStatement(SwitchStatementSyntax node)
    {
        Complexity += node.Sections.Count;
        base.VisitSwitchStatement(node);
    }

    public override void VisitWhileStatement(WhileStatementSyntax node)
    {
        Complexity++;
        base.VisitWhileStatement(node);
    }

    public override void VisitForStatement(ForStatementSyntax node)
    {
        Complexity++;
        base.VisitForStatement(node);
    }

    public override void VisitForEachStatement(ForEachStatementSyntax node)
    {
        Complexity++;
        base.VisitForEachStatement(node);
    }

    public override void VisitCatchClause(CatchClauseSyntax node)
    {
        Complexity++;
        base.VisitCatchClause(node);
    }

    public override void VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        if (node.OperatorToken.IsKind(SyntaxKind.AmpersandAmpersandToken) ||
            node.OperatorToken.IsKind(SyntaxKind.BarBarToken))
        {
            Complexity++;
        }
        base.VisitBinaryExpression(node);
    }
}
```

---

## Análisis Avanzado de Código Legacy

### Caso de Estudio: Sistema de Pagos Legacy

```csharp
// LegacyPaymentProcessor.cs - Análisis completo
public class LegacyPaymentProcessor
{
    // Métricas actuales:
    // - Complejidad Ciclomática: 47
    // - Complejidad Cognitiva: 63
    // - Líneas de código: 247
    // - Maintainability Index: 12 (Baja)
    // - Afferent Coupling: 15
    // - Efferent Coupling: 23
    
    public PaymentResult ProcessPayment(string cardNumber, decimal amount, string currency, 
                                      string merchantId, bool isRecurring, string cvv, 
                                      string expiryDate, string billingAddress, 
                                      string shippingAddress, bool saveCard, 
                                      string customerId, Dictionary<string, string> metadata)
    {
        // Violación: Demasiados parámetros (12 > 7)
        // Violación: Método demasiado largo (247 líneas)
        // Violación: Complejidad ciclomática excesiva (47 > 10)
        
        var result = new PaymentResult();
        
        // Duplicated code smell - validación repetitiva
        if (cardNumber == null || cardNumber.Length < 13 || cardNumber.Length > 19)
        {
            result.Success = false;
            result.ErrorMessage = "Invalid card number";
            result.ErrorCode = "INVALID_CARD";
            return result;
        }
        
        if (amount <= 0)
        {
            result.Success = false;
            result.ErrorMessage = "Invalid amount";
            result.ErrorCode = "INVALID_AMOUNT";
            return result;
        }
        
        if (string.IsNullOrEmpty(currency))
        {
            result.Success = false;
            result.ErrorMessage = "Currency is required";
            result.ErrorCode = "MISSING_CURRENCY";
            return result;
        }
        
        // Hardcoded values - Security smell
        var secretKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";
        var apiUrl = "https://api.stripe.com/v1/charges";
        
        // Complex nested conditions - Complexity smell
        if (currency == "USD" || currency == "EUR" || currency == "GBP")
        {
            if (amount > 0 && amount <= 999999)
            {
                if (merchantId != null && merchantId.Length > 0)
                {
                    if (cvv != null && cvv.Length >= 3)
                    {
                        if (expiryDate != null && expiryDate.Length == 4)
                        {
                            var month = int.Parse(expiryDate.Substring(0, 2));
                            var year = int.Parse(expiryDate.Substring(2, 2)) + 2000;
                            
                            if (month >= 1 && month <= 12)
                            {
                                if (year >= DateTime.Now.Year)
                                {
                                    if (year == DateTime.Now.Year && month >= DateTime.Now.Month)
                                    {
                                        // Luhn algorithm validation - Complex logic
                                        var checksum = 0;
                                        var isEven = false;
                                        
                                        for (int i = cardNumber.Length - 1; i >= 0; i--)
                                        {
                                            var digit = cardNumber[i] - '0';
                                            
                                            if (isEven)
                                            {
                                                digit *= 2;
                                                if (digit > 9)
                                                {
                                                    digit -= 9;
                                                }
                                            }
                                            
                                            checksum += digit;
                                            isEven = !isEven;
                                        }
                                        
                                        if (checksum % 10 == 0)
                                        {
                                            // Fraud detection - Complex nested logic
                                            if (amount > 1000)
                                            {
                                                if (customerId != null)
                                                {
                                                    // Check customer history
                                                    var customerHistory = GetCustomerHistory(customerId);
                                                    if (customerHistory != null)
                                                    {
                                                        if (customerHistory.TotalTransactions > 10)
                                                        {
                                                            if (customerHistory.AverageAmount * 3 > amount)
                                                            {
                                                                // Proceed with payment
                                                                return ProcessHighValuePayment(cardNumber, amount, currency, merchantId, isRecurring, metadata);
                                                            }
                                                            else
                                                            {
                                                                // Requires manual review
                                                                result.Success = false;
                                                                result.ErrorMessage = "Transaction requires manual review";
                                                                result.ErrorCode = "MANUAL_REVIEW_REQUIRED";
                                                                return result;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            result.Success = false;
                                                            result.ErrorMessage = "Insufficient customer history";
                                                            result.ErrorCode = "INSUFFICIENT_HISTORY";
                                                            return result;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        result.Success = false;
                                                        result.ErrorMessage = "Customer not found";
                                                        result.ErrorCode = "CUSTOMER_NOT_FOUND";
                                                        return result;
                                                    }
                                                }
                                                else
                                                {
                                                    result.Success = false;
                                                    result.ErrorMessage = "Customer ID required for high-value transactions";
                                                    result.ErrorCode = "CUSTOMER_REQUIRED";
                                                    return result;
                                                }
                                            }
                                            else
                                            {
                                                // Regular payment processing
                                                return ProcessRegularPayment(cardNumber, amount, currency, merchantId, isRecurring, metadata);
                                            }
                                        }
                                        else
                                        {
                                            result.Success = false;
                                            result.ErrorMessage = "Invalid card number";
                                            result.ErrorCode = "INVALID_CARD_LUHN";
                                            return result;
                                        }
                                    }
                                    else
                                    {
                                        result.Success = false;
                                        result.ErrorMessage = "Card expired";
                                        result.ErrorCode = "CARD_EXPIRED";
                                        return result;
                                    }
                                }
                                else
                                {
                                    result.Success = false;
                                    result.ErrorMessage = "Card expired";
                                    result.ErrorCode = "CARD_EXPIRED";
                                    return result;
                                }
                            }
                            else
                            {
                                result.Success = false;
                                result.ErrorMessage = "Invalid expiry month";
                                result.ErrorCode = "INVALID_EXPIRY_MONTH";
                                return result;
                            }
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Invalid expiry date format";
                            result.ErrorCode = "INVALID_EXPIRY_FORMAT";
                            return result;
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Invalid CVV";
                        result.ErrorCode = "INVALID_CVV";
                        return result;
                    }
                }
                else
                {
                    result.Success = false;
                    result.ErrorMessage = "Merchant ID is required";
                    result.ErrorCode = "MISSING_MERCHANT_ID";
                    return result;
                }
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Amount out of range";
                result.ErrorCode = "AMOUNT_OUT_OF_RANGE";
                return result;
            }
        }
        else
        {
            result.Success = false;
            result.ErrorMessage = "Unsupported currency";
            result.ErrorCode = "UNSUPPORTED_CURRENCY";
            return result;
        }
    }
    
    // Métodos adicionales...
}
```

### Estrategia de Refactoring Basada en Métricas

```csharp
// Refactored PaymentProcessor - Arquitectura mejorada
public class PaymentProcessor
{
    private readonly IPaymentValidator _validator;
    private readonly IFraudDetectionService _fraudService;
    private readonly IPaymentGateway _gateway;
    private readonly ILogger<PaymentProcessor> _logger;

    public PaymentProcessor(
        IPaymentValidator validator,
        IFraudDetectionService fraudService,
        IPaymentGateway gateway,
        ILogger<PaymentProcessor> logger)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _fraudService = fraudService ?? throw new ArgumentNullException(nameof(fraudService));
        _gateway = gateway ?? throw new ArgumentNullException(nameof(gateway));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // Métricas mejoradas:
    // - Complejidad Ciclomática: 4 (vs 47)
    // - Complejidad Cognitiva: 5 (vs 63)
    // - Líneas de código: 25 (vs 247)
    // - Maintainability Index: 78 (vs 12)
    // - Parámetros: 1 (vs 12)
    
    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // Early validation - Guard clauses
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        // Validation delegation - Single Responsibility
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return PaymentResult.Failed(validationResult.Errors);

        // Fraud detection delegation
        var fraudResult = await _fraudService.AssessRiskAsync(request);
        if (fraudResult.RequiresManualReview)
            return PaymentResult.RequiresReview(fraudResult.Reason);

        // Payment processing delegation
        return await _gateway.ProcessPaymentAsync(request);
    }
}

// Validator con métricas optimizadas
public class PaymentValidator : IPaymentValidator
{
    // Complejidad Ciclomática: 8 (distribuida en múltiples métodos)
    // Maintainability Index: 82
    
    public async Task<ValidationResult> ValidateAsync(PaymentRequest request)
    {
        var errors = new List<string>();

        // Cada validación en método separado - Complejidad distribuida
        ValidateCardNumber(request.CardNumber, errors);
        ValidateAmount(request.Amount, errors);
        ValidateCurrency(request.Currency, errors);
        ValidateExpiry(request.ExpiryDate, errors);
        ValidateCvv(request.Cvv, errors);

        return new ValidationResult(errors.Count == 0, errors);
    }

    // Complejidad Ciclomática: 2 por método
    private void ValidateCardNumber(string cardNumber, List<string> errors)
    {
        if (string.IsNullOrEmpty(cardNumber) || !IsValidLuhn(cardNumber))
            errors.Add("Invalid card number");
    }

    private void ValidateAmount(decimal amount, List<string> errors)
    {
        if (amount <= 0 || amount > 999999)
            errors.Add("Invalid amount");
    }

    private void ValidateCurrency(string currency, List<string> errors)
    {
        if (!SupportedCurrencies.Contains(currency))
            errors.Add("Unsupported currency");
    }

    private void ValidateExpiry(string expiryDate, List<string> errors)
    {
        if (!IsValidExpiryDate(expiryDate))
            errors.Add("Invalid expiry date");
    }

    private void ValidateCvv(string cvv, List<string> errors)
    {
        if (string.IsNullOrEmpty(cvv) || cvv.Length < 3 || cvv.Length > 4)
            errors.Add("Invalid CVV");
    }

    // Utilities con complejidad mínima
    private bool IsValidLuhn(string cardNumber) => 
        LuhnAlgorithm.Validate(cardNumber);

    private bool IsValidExpiryDate(string expiryDate) => 
        ExpiryDateValidator.IsValid(expiryDate);

    private static readonly HashSet<string> SupportedCurrencies = 
        new HashSet<string> { "USD", "EUR", "GBP", "CAD", "AUD" };
}
```

---

## Métricas de Rendimiento y Escalabilidad

### Análisis de Performance

```csharp
// PaymentProcessor.Performance/PerformanceAnalyzer.cs
using System.Diagnostics;
using System.Threading.Tasks;

public class PerformanceAnalyzer
{
    public async Task<PerformanceMetrics> AnalyzePaymentProcessingAsync(int concurrentRequests = 100)
    {
        var tasks = new List<Task<PaymentResult>>();
        var stopwatch = Stopwatch.StartNew();
        
        // Generar carga de trabajo
        for (int i = 0; i < concurrentRequests; i++)
        {
            tasks.Add(ProcessSamplePaymentAsync());
        }
        
        var results = await Task.WhenAll(tasks);
        stopwatch.Stop();
        
        return new PerformanceMetrics
        {
            TotalRequests = concurrentRequests,
            TotalDuration = stopwatch.Elapsed,
            AverageResponseTime = stopwatch.Elapsed.TotalMilliseconds / concurrentRequests,
            ThroughputPerSecond = concurrentRequests / stopwatch.Elapsed.TotalSeconds,
            SuccessRate = results.Count(r => r.Success) / (double)concurrentRequests,
            P95ResponseTime = CalculatePercentile(results, 0.95),
            P99ResponseTime = CalculatePercentile(results, 0.99),
            MemoryUsage = GC.GetTotalMemory(false),
            GCCollections = GC.CollectionCount(0) + GC.CollectionCount(1) + GC.CollectionCount(2)
        };
    }
    
    private double CalculatePercentile(PaymentResult[] results, double percentile)
    {
        var responseTimes = results.Select(r => r.ProcessingTime).OrderBy(t => t).ToArray();
        int index = (int)Math.Ceiling(percentile * responseTimes.Length) - 1;
        return responseTimes[index];
    }
}

public class PerformanceMetrics
{
    public int TotalRequests { get; set; }
    public TimeSpan TotalDuration { get; set; }
    public double AverageResponseTime { get; set; }
    public double ThroughputPerSecond { get; set; }
    public double SuccessRate { get; set; }
    public double P95ResponseTime { get; set; }
    public double P99ResponseTime { get; set; }
    public long MemoryUsage { get; set; }
    public int GCCollections { get; set; }
    
    // SLA Validation
    public bool MeetsSLA => 
        AverageResponseTime < 200 && // < 200ms average
        P95ResponseTime < 500 &&     // < 500ms P95
        P99ResponseTime < 1000 &&    // < 1000ms P99
        SuccessRate > 0.999;         // > 99.9% success rate
}
```

### Métricas de Escalabilidad

```csharp
// PaymentProcessor.Scalability/ScalabilityMetrics.cs
public class ScalabilityMetrics
{
    public static async Task<ScalabilityReport> GenerateReportAsync()
    {
        var report = new ScalabilityReport();
        
        // CPU utilization under load
        report.CpuUtilization = await MeasureCpuUtilizationAsync();
        
        // Memory scalability
        report.MemoryScalability = await MeasureMemoryScalabilityAsync();
        
        // Database connection pooling efficiency
        report.DatabasePoolEfficiency = await MeasureDatabasePoolingAsync();
        
        // Thread pool utilization
        report.ThreadPoolUtilization = MeasureThreadPoolUtilization();
        
        return report;
    }
    
    private static async Task<double> MeasureCpuUtilizationAsync()
    {
        using var process = Process.GetCurrentProcess();
        var startTime = DateTime.UtcNow;
        var startCpuUsage = process.TotalProcessorTime;
        
        // Simulate workload
        await Task.Delay(1000);
        
        var endTime = DateTime.UtcNow;
        var endCpuUsage = process.TotalProcessorTime;
        
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds;
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        
        return cpuUsageTotal * 100;
    }
}
```

---

## Reportes Ejecutivos y Dashboards

### Generación de Reportes Automatizados

```csharp
// PaymentProcessor.Reporting/ExecutiveReportGenerator.cs
public class ExecutiveReportGenerator
{
    public async Task<ExecutiveReport> GenerateAsync(DateTime startDate, DateTime endDate)
    {
        var report = new ExecutiveReport
        {
            Period = $"{startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}",
            GeneratedAt = DateTime.UtcNow
        };
        
        // Quality metrics
        report.QualityMetrics = await GetQualityMetricsAsync(startDate, endDate);
        
        // Performance metrics
        report.PerformanceMetrics = await GetPerformanceMetricsAsync(startDate, endDate);
        
        // Reliability metrics
        report.ReliabilityMetrics = await GetReliabilityMetricsAsync(startDate, endDate);
        
        // Technical debt
        report.TechnicalDebt = await GetTechnicalDebtAsync();
        
        return report;
    }
    
    private async Task<QualityMetrics> GetQualityMetricsAsync(DateTime start, DateTime end)
    {
        return new QualityMetrics
        {
            CodeCoverage = await GetCodeCoverageAsync(),
            SonarQubeRating = await GetSonarQubeRatingAsync(),
            SecurityVulnerabilities = await GetSecurityVulnerabilitiesAsync(),
            CodeSmells = await GetCodeSmellsAsync(),
            TechnicalDebtRatio = await GetTechnicalDebtRatioAsync()
        };
    }
}

public class ExecutiveReport
{
    public string Period { get; set; }
    public DateTime GeneratedAt { get; set; }
    public QualityMetrics QualityMetrics { get; set; }
    public PerformanceMetrics PerformanceMetrics { get; set; }
    public ReliabilityMetrics ReliabilityMetrics { get; set; }
    public TechnicalDebtMetrics TechnicalDebt { get; set; }
    
    // Executive Summary
    public ExecutiveSummary Summary => new ExecutiveSummary
    {
        OverallHealth = CalculateOverallHealth(),
        KeyRisks = IdentifyKeyRisks(),
        Recommendations = GenerateRecommendations()
    };
}
```

---

## Ejercicio Práctico: Implementación Completa

### Configuración del Proyecto

```bash
# Crear estructura del proyecto
mkdir PaymentProcessor.MetricsAnalysis
cd PaymentProcessor.MetricsAnalysis

# Crear solución
dotnet new sln -n PaymentProcessor

# Crear proyectos
dotnet new classlib -n PaymentProcessor.Core
dotnet new classlib -n PaymentProcessor.Infrastructure
dotnet new webapi -n PaymentProcessor.API
dotnet new xunit -n PaymentProcessor.Tests
dotnet new console -n PaymentProcessor.MetricsCollector

# Agregar proyectos a la solución
dotnet sln add PaymentProcessor.Core
dotnet sln add PaymentProcessor.Infrastructure
dotnet sln add PaymentProcessor.API
dotnet sln add PaymentProcessor.Tests
dotnet sln add PaymentProcessor.MetricsCollector

# Instalar herramientas de análisis
dotnet add PaymentProcessor.Core package SonarAnalyzer.CSharp
dotnet add PaymentProcessor.Core package Microsoft.CodeAnalysis.Analyzers
dotnet add PaymentProcessor.MetricsCollector package Microsoft.CodeAnalysis.Metrics

# Configurar ruleset
cp enterprise.ruleset .
```

### Implementación del Analizador

```csharp
// PaymentProcessor.MetricsCollector/Program.cs
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var solutionPath = args.Length > 0 ? args[0] : "PaymentProcessor.sln";
        
        var analyzer = new ComprehensiveMetricsAnalyzer();
        var results = await analyzer.AnalyzeSolutionAsync(solutionPath);
        
        // Generar reportes
        await GenerateReportsAsync(results);
    }
    
    static async Task GenerateReportsAsync(AnalysisResults results)
    {
        // Reporte JSON para herramientas
        var jsonReport = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync("metrics-report.json", jsonReport);
        
        // Reporte HTML para humanos
        var htmlReport = GenerateHtmlReport(results);
        await File.WriteAllTextAsync("metrics-report.html", htmlReport);
        
        // Reporte CSV para análisis
        var csvReport = GenerateCsvReport(results);
        await File.WriteAllTextAsync("metrics-report.csv", csvReport);
        
        Console.WriteLine($"Analysis complete. Reports generated:");
        Console.WriteLine($"- JSON: metrics-report.json");
        Console.WriteLine($"- HTML: metrics-report.html");
        Console.WriteLine($"- CSV: metrics-report.csv");
    }
}

public class ComprehensiveMetricsAnalyzer
{
    public async Task<AnalysisResults> AnalyzeSolutionAsync(string solutionPath)
    {
        var results = new AnalysisResults();
        
        // Analizar cada proyecto
        var projectPaths = Directory.GetFiles(Path.GetDirectoryName(solutionPath), "*.csproj", SearchOption.AllDirectories);
        
        foreach (var projectPath in projectPaths)
        {
            var projectResults = await AnalyzeProjectAsync(projectPath);
            results.ProjectResults.Add(projectResults);
        }
        
        // Calcular métricas agregadas
        results.Summary = CalculateSummary(results.ProjectResults);
        
        return results;
    }
    
    private async Task<ProjectAnalysisResults> AnalyzeProjectAsync(string projectPath)
    {
        var project = new ProjectAnalysisResults
        {
            ProjectName = Path.GetFileNameWithoutExtension(projectPath),
            ProjectPath = projectPath
        };
        
        // Analizar archivos C#
        var sourceFiles = Directory.GetFiles(Path.GetDirectoryName(projectPath), "*.cs", SearchOption.AllDirectories);
        
        foreach (var sourceFile in sourceFiles)
        {
            var fileResults = await AnalyzeFileAsync(sourceFile);
            project.FileResults.Add(fileResults);
        }
        
        return project;
    }
    
    private async Task<FileAnalysisResults> AnalyzeFileAsync(string filePath)
    {
        var sourceCode = await File.ReadAllTextAsync(filePath);
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var root = syntaxTree.GetRoot();
        
        var fileResults = new FileAnalysisResults
        {
            FilePath = filePath,
            LinesOfCode = sourceCode.Split('\n').Length,
            ClassCount = root.DescendantNodes().OfType<ClassDeclarationSyntax>().Count(),
            MethodCount = root.DescendantNodes().OfType<MethodDeclarationSyntax>().Count()
        };
        
        // Analizar métodos
        var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
        foreach (var method in methods)
        {
            var methodMetrics = AnalyzeMethod(method);
            fileResults.MethodMetrics.Add(methodMetrics);
        }
        
        return fileResults;
    }
    
    private MethodMetrics AnalyzeMethod(MethodDeclarationSyntax method)
    {
        var walker = new MetricsWalker();
        walker.Visit(method);
        
        return new MethodMetrics
        {
            Name = method.Identifier.ValueText,
            CyclomaticComplexity = walker.CyclomaticComplexity,
            CognitiveComplexity = walker.CognitiveComplexity,
            LinesOfCode = method.GetText().Lines.Count,
            ParameterCount = method.ParameterList.Parameters.Count,
            NestingDepth = walker.MaxNestingDepth,
            ReturnStatements = walker.ReturnStatements
        };
    }
}

public class MetricsWalker : CSharpSyntaxWalker
{
    public int CyclomaticComplexity { get; private set; } = 1;
    public int CognitiveComplexity { get; private set; } = 0;
    public int MaxNestingDepth { get; private set; } = 0;
    public int ReturnStatements { get; private set; } = 0;
    
    private int _currentNestingDepth = 0;
    
    public override void VisitIfStatement(IfStatementSyntax node)
    {
        CyclomaticComplexity++;
        CognitiveComplexity += 1 + _currentNestingDepth;
        
        _currentNestingDepth++;
        MaxNestingDepth = Math.Max(MaxNestingDepth, _currentNestingDepth);
        
        base.VisitIfStatement(node);
        
        _currentNestingDepth--;
    }
    
    public override void VisitSwitchStatement(SwitchStatementSyntax node)
    {
        CyclomaticComplexity += node.Sections.Count;
        CognitiveComplexity += node.Sections.Count;
        
        base.VisitSwitchStatement(node);
    }
    
    public override void VisitWhileStatement(WhileStatementSyntax node)
    {
        CyclomaticComplexity++;
        CognitiveComplexity += 1 + _currentNestingDepth;
        
        _currentNestingDepth++;
        base.VisitWhileStatement(node);
        _currentNestingDepth--;
    }
    
    public override void VisitForStatement(ForStatementSyntax node)
    {
        CyclomaticComplexity++;
        CognitiveComplexity += 1 + _currentNestingDepth;
        
        _currentNestingDepth++;
        base.VisitForStatement(node);
        _currentNestingDepth--;
    }
    
    public override void VisitReturnStatement(ReturnStatementSyntax node)
    {
        ReturnStatements++;
        if (ReturnStatements > 1)
            CognitiveComplexity++;
        
        base.VisitReturnStatement(node);
    }
}
```

---

## Evaluación y Certificación

### Criterios de Evaluación

✅ **Análisis Profundo**: Implementa análisis multi-dimensional de métricas  
✅ **Herramientas Empresariales**: Configura SonarQube, NDepend, analyzers personalizados  
✅ **Automatización**: Integra análisis en CI/CD con quality gates  
✅ **Reportes Ejecutivos**: Genera dashboards para diferentes stakeholders  
✅ **Mejora Continua**: Establece procesos de monitoreo y mejora  

### Resultados Esperados

| Aspecto | Antes | Después |
|---------|-------|---------|
| Complejidad Promedio | 15+ | <8 |
| Maintainability Index | <50 | >75 |
| Technical Debt Ratio | >15% | <5% |
| Test Coverage | <60% | >85% |
| Security Rating | C | A |
| Performance P95 | >500ms | <200ms |

El dominio de estas técnicas avanzadas de análisis posiciona al desarrollador como líder técnico capaz de establecer y mantener estándares de calidad empresarial.