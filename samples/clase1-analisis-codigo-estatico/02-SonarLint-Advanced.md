# EJERCICIO 2: Configuración Avanzada de SonarLint - Semi-Senior

## Objetivos de Aprendizaje

Este ejercicio está diseñado para desarrolladores con experiencia que buscan implementar análisis de código estático de nivel empresarial usando SonarLint con configuraciones avanzadas, reglas personalizadas e integración con pipelines de CI/CD.

---

## Arquitectura de Análisis Estático Empresarial

### Configuración Multi-Proyecto

SonarLint debe configurarse de manera consistente across multiple projects y equipos:

```xml
<!-- Directory.Build.props -->
<Project>
  <PropertyGroup>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <WarningsAsErrors />
    <WarningsNotAsErrors>CS1591</WarningsNotAsErrors>
    <CodeAnalysisRuleSet>$(MSBuildThisFileDirectory)ruleset.ruleset</CodeAnalysisRuleSet>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="SonarAnalyzer.CSharp" Version="8.56.0.67649">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

### Ruleset Personalizado para Fintech

```xml
<?xml version="1.0" encoding="utf-8"?>
<!-- Enterprise.ruleset - Configuración para sistemas financieros -->
<RuleSet Name="Enterprise Fintech Rules" Description="Reglas para aplicaciones financieras críticas" ToolsVersion="15.0">
  <!-- Security Rules - Críticas para fintech -->
  <Rules AnalyzerId="SonarAnalyzer.CSharp" RuleNamespace="SonarAnalyzer.CSharp">
    <Rule Id="S2245" Action="Error" /> <!-- Random number generators should be used securely -->
    <Rule Id="S4426" Action="Error" /> <!-- Cryptographic keys should be robust -->
    <Rule Id="S5542" Action="Error" /> <!-- SSL/TLS certificates should be valid -->
    <Rule Id="S2068" Action="Error" /> <!-- Credentials should not be hard-coded -->
    <Rule Id="S1313" Action="Error" /> <!-- IP addresses should not be hardcoded -->
    
    <!-- Performance Rules - Críticas para high-frequency trading -->
    <Rule Id="S1854" Action="Error" /> <!-- Dead stores should be removed -->
    <Rule Id="S1481" Action="Error" /> <!-- Unused local variables should be removed -->
    <Rule Id="S1128" Action="Error" /> <!-- Unused "using" should be removed -->
    <Rule Id="S3267" Action="Error" /> <!-- Loops should be simplified with "LINQ" expressions -->
    
    <!-- Maintainability Rules - Críticas para equipos grandes -->
    <Rule Id="S1541" Action="Error" /> <!-- Methods and classes should not be too complex -->
    <Rule Id="S138" Action="Error" />  <!-- Functions should not have too many lines -->
    <Rule Id="S1067" Action="Error" /> <!-- Expressions should not be too complex -->
    <Rule Id="S107" Action="Error" />  <!-- Functions should not have too many parameters -->
    
    <!-- Reliability Rules - Críticas para sistemas 24/7 -->
    <Rule Id="S1764" Action="Error" /> <!-- Identical expressions should not be used -->
    <Rule Id="S2259" Action="Error" /> <!-- Null pointers should not be dereferenced -->
    <Rule Id="S3655" Action="Error" /> <!-- Empty nullable value should not be accessed -->
    <Rule Id="S4143" Action="Error" /> <!-- Collection elements should not be replaced unconditionally -->
  </Rules>
  
  <!-- Customización por contexto -->
  <Rules AnalyzerId="Microsoft.CodeAnalysis.CSharp" RuleNamespace="Microsoft.CodeAnalysis.CSharp">
    <Rule Id="CS1591" Action="Warning" /> <!-- XML documentation required -->
    <Rule Id="CS0649" Action="Error" />   <!-- Field never assigned -->
    <Rule Id="CS8618" Action="Error" />   <!-- Non-nullable field uninitialized -->
  </Rules>
</RuleSet>
```

---

## Configuración Avanzada por Contexto

### Para Microservicios Financial

```json
// .sonarlint/settings.json
{
  "sonar.cs.roslyn.reportDiagnostics": "true",
  "sonar.cs.roslyn.ignoreIssues": "false",
  "sonar.coverage.exclusions": [
    "**/Migrations/**",
    "**/Program.cs",
    "**/Startup.cs",
    "**/*Tests/**"
  ],
  "sonar.exclusions": [
    "**/obj/**",
    "**/bin/**",
    "**/packages/**"
  ],
  "sonar.issue.ignore.multicriteria": {
    "e1": {
      "resourceKey": "**/*Controller.cs",
      "ruleKey": "S1200"
    },
    "e2": {
      "resourceKey": "**/Models/**",
      "ruleKey": "S1128"
    }
  },
  "sonar.cs.analyzer.projectOutPaths": [
    "bin",
    "obj"
  ]
}
```

### EditorConfig Empresarial

```ini
# .editorconfig - Configuración para equipos de desarrollo
root = true

[*]
charset = utf-8
end_of_line = crlf
insert_final_newline = true
indent_style = space
indent_size = 4
trim_trailing_whitespace = true

[*.cs]
# Naming conventions
dotnet_naming_rule.interface_should_be_prefixed_with_i.severity = error
dotnet_naming_rule.interface_should_be_prefixed_with_i.symbols = interface
dotnet_naming_rule.interface_should_be_prefixed_with_i.style = prefix_interface_with_i

# Security and Performance
dotnet_code_quality.ca5350.excluded_symbol_names = MD5CryptoServiceProvider # Allowed for legacy compatibility
dotnet_code_quality.ca1031.disallowed_symbol_names = System.Exception # Prevent catching base Exception

# Async/Await patterns
dotnet_diagnostic.CA2007.severity = error # ConfigureAwait(false) required
dotnet_diagnostic.CA1849.severity = error # Use async methods when available

# Financial domain specific
dotnet_diagnostic.S4144.severity = error # Methods should not have identical implementations
dotnet_diagnostic.S1643.severity = error # Strings should not be concatenated in loops

# Nullable reference types
dotnet_diagnostic.CS8600.severity = error # Converting null literal or possible null value
dotnet_diagnostic.CS8601.severity = error # Possible null reference assignment
dotnet_diagnostic.CS8602.severity = error # Dereference of a possibly null reference
dotnet_diagnostic.CS8603.severity = error # Possible null reference return
```

---

## Integración con CI/CD

### Azure DevOps Pipeline

```yaml
# azure-pipelines.yml
trigger:
  branches:
    include:
    - main
    - develop
    - feature/*

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'
  dotNetFramework: 'net6.0'
  sonarCloudEndpoint: 'SonarCloud'
  sonarCloudOrganization: 'fintech-org'
  sonarCloudProjectKey: 'payment-processor'

stages:
- stage: CodeQuality
  displayName: 'Code Quality Analysis'
  jobs:
  - job: SonarAnalysis
    displayName: 'SonarCloud Analysis'
    steps:
    - task: SonarCloudPrepare@1
      inputs:
        SonarCloud: $(sonarCloudEndpoint)
        organization: $(sonarCloudOrganization)
        scannerMode: 'MSBuild'
        projectKey: $(sonarCloudProjectKey)
        projectName: 'Payment Processor'
        extraProperties: |
          sonar.cs.opencover.reportsPaths=$(Agent.TempDirectory)/**/*.opencover.xml
          sonar.cs.roslyn.reportFilePaths=$(Agent.TempDirectory)/**/*.xml
          sonar.coverage.exclusions=**/Migrations/**,**/Program.cs,**/Startup.cs,**/*Tests/**
          sonar.exclusions=**/obj/**,**/bin/**,**/packages/**
          sonar.qualitygate.wait=true
          sonar.qualitygate.timeout=300

    - task: DotNetCoreCLI@2
      displayName: 'Restore NuGet Packages'
      inputs:
        command: 'restore'
        projects: '**/*.csproj'

    - task: DotNetCoreCLI@2
      displayName: 'Build Solution'
      inputs:
        command: 'build'
        projects: '**/*.csproj'
        arguments: '--configuration $(buildConfiguration) --no-restore'

    - task: DotNetCoreCLI@2
      displayName: 'Run Unit Tests'
      inputs:
        command: 'test'
        projects: '**/*Tests/*.csproj'
        arguments: '--configuration $(buildConfiguration) --no-build --collect:"XPlat Code Coverage" --results-directory $(Agent.TempDirectory)'

    - task: SonarCloudAnalyze@1
      displayName: 'Run SonarCloud Analysis'

    - task: SonarCloudPublish@1
      displayName: 'Publish SonarCloud Results'
      inputs:
        pollingTimeoutSec: '300'
```

### GitHub Actions

```yaml
# .github/workflows/quality-gate.yml
name: Quality Gate

on:
  pull_request:
    branches: [ main, develop ]
  push:
    branches: [ main, develop ]

jobs:
  sonarcloud:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
      with:
        fetch-depth: 0

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 6.0.x

    - name: Cache SonarCloud packages
      uses: actions/cache@v3
      with:
        path: ~/sonar/cache
        key: ${{ runner.os }}-sonar
        restore-keys: ${{ runner.os }}-sonar

    - name: Cache SonarCloud scanner
      id: cache-sonar-scanner
      uses: actions/cache@v3
      with:
        path: ./.sonar/scanner
        key: ${{ runner.os }}-sonar-scanner
        restore-keys: ${{ runner.os }}-sonar-scanner

    - name: Install SonarCloud scanner
      if: steps.cache-sonar-scanner.outputs.cache-hit != 'true'
      shell: bash
      run: |
        mkdir -p ./.sonar/scanner
        dotnet tool update dotnet-sonarscanner --tool-path ./.sonar/scanner

    - name: Begin SonarCloud analysis
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}
      shell: bash
      run: |
        ./.sonar/scanner/dotnet-sonarscanner begin \
          /k:"fintech-org_payment-processor" \
          /o:"fintech-org" \
          /d:sonar.login="${{ secrets.SONAR_TOKEN }}" \
          /d:sonar.host.url="https://sonarcloud.io" \
          /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml" \
          /d:sonar.exclusions="**/Migrations/**,**/wwwroot/**,**/obj/**,**/bin/**"

    - name: Build and Test
      run: |
        dotnet restore
        dotnet build --no-restore
        dotnet test --no-build --verbosity normal \
          --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

    - name: End SonarCloud analysis
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}
      shell: bash
      run: ./.sonar/scanner/dotnet-sonarscanner end /d:sonar.login="${{ secrets.SONAR_TOKEN }}"
```

---

## Métricas y Quality Gates Empresariales

### Definición de Quality Gates

```json
{
  "qualityGate": {
    "name": "Enterprise Fintech Gate",
    "conditions": [
      {
        "metric": "new_reliability_rating",
        "operator": "GREATER_THAN",
        "value": "1",
        "onLeakPeriod": true
      },
      {
        "metric": "new_security_rating",
        "operator": "GREATER_THAN",
        "value": "1",
        "onLeakPeriod": true
      },
      {
        "metric": "new_maintainability_rating",
        "operator": "GREATER_THAN",
        "value": "1",
        "onLeakPeriod": true
      },
      {
        "metric": "new_coverage",
        "operator": "LESS_THAN",
        "value": "80",
        "onLeakPeriod": true
      },
      {
        "metric": "new_duplicated_lines_density",
        "operator": "GREATER_THAN",
        "value": "3",
        "onLeakPeriod": true
      }
    ]
  }
}
```

### Métricas Específicas para Fintech

| Métrica | Umbral | Criticidad | Justificación |
|---------|--------|------------|---------------|
| Security Rating | A | Crítico | Cumplimiento PCI DSS |
| Reliability Rating | A | Crítico | Sistemas 24/7 |
| Maintainability Rating | A | Alto | Equipos grandes |
| Coverage | ≥80% | Alto | Confiabilidad transaccional |
| Duplicated Lines | ≤3% | Medio | Mantenibilidad |
| Complexity | ≤10 | Alto | Auditabilidad |
| Technical Debt | ≤5% | Medio | Sostenibilidad |

---

## Configuración Avanzada por IDE

### Visual Studio Enterprise

```xml
<!-- .globalconfig -->
is_global = true

# Enable advanced analyzers
dotnet_analyzer_diagnostic.category-security.severity = error
dotnet_analyzer_diagnostic.category-performance.severity = warning
dotnet_analyzer_diagnostic.category-maintainability.severity = suggestion

# Custom rules for financial applications
dotnet_diagnostic.S4830.severity = error # Server certificates should be verified
dotnet_diagnostic.S5527.severity = error # Authentication should be enforced
dotnet_diagnostic.S4784.severity = error # Regular expressions should not be vulnerable
```

### Visual Studio Code

```json
// .vscode/settings.json
{
  "sonarlint.connectedMode.project": {
    "connectionId": "sonarcloud",
    "projectKey": "fintech-org_payment-processor"
  },
  "sonarlint.rules": {
    "javascript:S1481": "off",
    "typescript:S1481": "off",
    "csharp:S1481": "error",
    "csharp:S2245": "error",
    "csharp:S4426": "error"
  },
  "sonarlint.output.showAnalyzerLogs": true,
  "sonarlint.pathToNodeExecutable": "node",
  "sonarlint.disableTelemetry": false
}
```

---

## Ejercicio Práctico: Configuración Empresarial

### Paso 1: Configurar Proyecto Multi-Contexto

Crea la siguiente estructura:

```
PaymentProcessor/
├── src/
│   ├── PaymentProcessor.API/
│   ├── PaymentProcessor.Core/
│   ├── PaymentProcessor.Infrastructure/
│   └── PaymentProcessor.Tests/
├── Directory.Build.props
├── .editorconfig
├── enterprise.ruleset
└── .sonarlint/
    └── settings.json
```

### Paso 2: Implementar Quality Gates

```csharp
// PaymentProcessor.Core/Services/PaymentService.cs
using System;
using System.Threading.Tasks;
using PaymentProcessor.Core.Models;

namespace PaymentProcessor.Core.Services
{
    public class PaymentService
    {
        // PROBLEMA: Múltiples violaciones de SonarLint
        public async Task<bool> ProcessPayment(string cardNumber, decimal amount, string currency, string merchantId, bool isRecurring)
        {
            // S2245: Random number generators should be used securely
            var random = new Random();
            var transactionId = random.Next(1000000, 9999999);
            
            // S2068: Credentials should not be hard-coded
            var apiKey = "acaestariaapike;
            
            // S1541: Methods should not be too complex
            if (cardNumber != null && cardNumber.Length >= 13)
            {
                if (amount > 0)
                {
                    if (currency == "USD" || currency == "EUR" || currency == "GBP")
                    {
                        if (merchantId != null && merchantId.Length > 0)
                        {
                            if (isRecurring)
                            {
                                // Complex recurring logic
                                if (ValidateRecurringPayment(cardNumber, amount))
                                {
                                    return await ProcessRecurringPayment(cardNumber, amount, currency, merchantId, transactionId);
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                // Complex one-time payment logic
                                if (ValidateOneTimePayment(cardNumber, amount))
                                {
                                    return await ProcessOneTimePayment(cardNumber, amount, currency, merchantId, transactionId);
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Merchant ID is required");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Unsupported currency");
                    }
                }
                else
                {
                    throw new ArgumentException("Amount must be positive");
                }
            }
            else
            {
                throw new ArgumentException("Invalid card number");
            }
        }
        
        private bool ValidateRecurringPayment(string cardNumber, decimal amount) => true;
        private bool ValidateOneTimePayment(string cardNumber, decimal amount) => true;
        private async Task<bool> ProcessRecurringPayment(string cardNumber, decimal amount, string currency, string merchantId, int transactionId) => true;
        private async Task<bool> ProcessOneTimePayment(string cardNumber, decimal amount, string currency, string merchantId, int transactionId) => true;
    }
}
```

### Paso 3: Refactorizar para Cumplir Quality Gates

```csharp
// PaymentProcessor.Core/Services/PaymentService.cs - Refactored
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PaymentProcessor.Core.Models;

namespace PaymentProcessor.Core.Services
{
    public class PaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentValidator _validator;
        private readonly IPaymentProcessor _processor;

        public PaymentService(IConfiguration configuration, IPaymentValidator validator, IPaymentProcessor processor)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
        {
            // Guard clauses for early validation
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Validate using dedicated validator
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return PaymentResult.Failed(validationResult.Errors);

            // Generate secure transaction ID
            var transactionId = GenerateSecureTransactionId();

            // Process based on payment type
            return request.IsRecurring
                ? await _processor.ProcessRecurringPaymentAsync(request, transactionId)
                : await _processor.ProcessOneTimePaymentAsync(request, transactionId);
        }

        private string GenerateSecureTransactionId()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[16];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
```

---

## Monitoreo y Reportes

### Dashboard de Métricas

```csharp
// PaymentProcessor.Infrastructure/Monitoring/QualityMetricsCollector.cs
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace PaymentProcessor.Infrastructure.Monitoring
{
    public class QualityMetricsCollector
    {
        private readonly Meter _meter;
        private readonly Counter<int> _codeViolationsCounter;
        private readonly Histogram<double> _complexityHistogram;
        private readonly Gauge<double> _coverageGauge;

        public QualityMetricsCollector()
        {
            _meter = new Meter("PaymentProcessor.Quality");
            _codeViolationsCounter = _meter.CreateCounter<int>("sonar_violations_total");
            _complexityHistogram = _meter.CreateHistogram<double>("method_complexity");
            _coverageGauge = _meter.CreateGauge<double>("test_coverage_percentage");
        }

        public void RecordViolation(string severity, string rule)
        {
            _codeViolationsCounter.Add(1, 
                KeyValuePair.Create<string, object>("severity", severity),
                KeyValuePair.Create<string, object>("rule", rule));
        }

        public void RecordComplexity(string method, double complexity)
        {
            _complexityHistogram.Record(complexity,
                KeyValuePair.Create<string, object>("method", method));
        }

        public void UpdateCoverage(double percentage)
        {
            _coverageGauge.Record(percentage);
        }
    }
}
```

### Alertas y Notificaciones

```yaml
# alerts.yml - Configuración de alertas
groups:
- name: code-quality
  rules:
  - alert: HighComplexityMethod
    expr: method_complexity > 15
    for: 0m
    labels:
      severity: warning
    annotations:
      summary: "Method complexity too high"
      description: "Method {{ $labels.method }} has complexity of {{ $value }}"
      
  - alert: LowTestCoverage
    expr: test_coverage_percentage < 80
    for: 5m
    labels:
      severity: critical
    annotations:
      summary: "Test coverage below threshold"
      description: "Test coverage is {{ $value }}%, below 80% requirement"
      
  - alert: SecurityViolation
    expr: sonar_violations_total{severity="error"} > 0
    for: 0m
    labels:
      severity: critical
    annotations:
      summary: "Security violation detected"
      description: "{{ $value }} security violations found"
```

---

## Evaluación y Certificación

### Criterios de Evaluación Avanzada

✅ **Configuración Empresarial**: Implementa ruleset personalizado con métricas específicas del dominio
✅ **CI/CD Integration**: Configura quality gates que bloquean deployments
✅ **Monitoreo Continuo**: Implementa alertas basadas en métricas de calidad
✅ **Reportes Ejecutivos**: Genera dashboards para stakeholders
✅ **Automatización**: Elimina intervención manual en procesos de calidad

### Resultados Esperados

| Métrica | Antes | Después |
|---------|-------|---------|
| Security Rating | C | A |
| Reliability Rating | B | A |
| Maintainability Rating | C | A |
| Test Coverage | 45% | 85% |
| Duplicated Code | 8% | 2% |
| Technical Debt | 12% | 3% |
| Deployment Failures | 15% | 2% |

---

## Próximos Pasos

1. **Implementar** configuración en proyecto real
2. **Establecer** métricas de baseline
3. **Configurar** alertas y monitoreo
4. **Entrenar** al equipo en nuevos procesos
5. **Iterar** basado en feedback y métricas

La implementación exitosa de esta configuración avanzada te posicionará como referente en calidad de código dentro de tu organización.