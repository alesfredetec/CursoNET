# Guía de Mentoring: Reducir Complejidad Ciclomática - Paso a Paso

## 🎯 Objetivo del Ejercicio
Aprender a **reducir complejidad ciclomática** transformando métodos complejos con múltiples caminos de ejecución en código simple, mantenible y testeable.

## 🧠 ¿Qué es la Complejidad Ciclomática?

La **complejidad ciclomática** mide el número de caminos de ejecución independientes a través de un método. Se calcula contando:
- 1 (base) + número de decisiones (if, else, while, for, case, catch, &&, ||)

### Analogía del Mundo Real:
Imagina un **laberinto** (tu método):
- **Complejidad baja (1-5)**: Camino directo con pocas bifurcaciones - fácil de navegar
- **Complejidad media (6-10)**: Algunos desvíos - requiere mapa básico
- **Complejidad alta (11+)**: Laberinto complejo - te pierdes fácilmente, necesitas GPS

### ¿Por qué importa?
- **Testing**: Complejidad 15 = 15+ test cases mínimos
- **Bugs**: A mayor complejidad, más probabilidad de errores
- **Mantenimiento**: Métodos complejos son difíciles de modificar
- **Performance**: Anidación profunda afecta rendimiento

## 🔍 Identificando Complejidad Alta

### Señales de Alerta (Code Smells):
1. **Anidación profunda** (>3 niveles de if/for/while)
2. **Múltiples condiciones complejas** (&&, ||)
3. **Switch statements largos** con lógica anidada
4. **Early returns mezclados** con lógica compleja
5. **Difícil escribir tests** unitarios completos
6. **Difícil de leer** de una sola pasada

### Cálculo Rápido de Complejidad:
```csharp
public bool ComplexMethod(Order order)  // +1 (base)
{
    if (order != null)                  // +1 (if)
    {
        if (order.Total > 100)          // +1 (if anidado)
        {
            foreach (var item in order.Items) // +1 (foreach)
            {
                if (item.IsValid &&    // +1 (if)
                    item.Price > 0)    // +1 (&&)
                {
                    // lógica
                }
                else                   // +0 (else no suma)
                {
                    return false;      // Pero crea otro camino
                }
            }
        }
    }
    return true;
}
// Complejidad total: 6 (moderada pero mejorable)
```

## 📋 Proceso Paso a Paso

### Paso 1: Medir Complejidad Actual
**Tiempo estimado: 5 minutos**

1. **Contar manualmente:**
   - Método base: +1
   - Cada if/else if: +1
   - Cada &&/||: +1
   - Cada case en switch: +1
   - Cada catch: +1
   - Cada while/for/foreach: +1

2. **Usar herramientas automáticas:**
   - Visual Studio Code Metrics
   - SonarLint extension
   - Complexity calculators online

**Ejercicio práctico:**
```csharp
// Calcular complejidad de ValidateOrderComplex:
// Contar cada decisión y estructura de control
// Objetivo: Identificar qué contribuye más a la complejidad
```

### Paso 2: Identificar Patrones Problemáticos
**Tiempo estimado: 10 minutos**

**Patrones comunes de alta complejidad:**

#### Patrón 1: Validación Anidada Profunda
```csharp
// ❌ PROBLEMÁTICO: Anidación profunda
if (order != null)
{
    if (customer != null)
    {
        if (customer.IsActive)
        {
            if (customer.CreditLimit > 0)
            {
                // lógica en el nivel más profundo
            }
        }
    }
}
```

#### Patrón 2: Switch con Lógica Anidada
```csharp
// ❌ PROBLEMÁTICO: Switch + validaciones anidadas
switch (paymentMethod)
{
    case "CREDIT_CARD":
        if (creditCard != null)
        {
            if (creditCard.IsValid)
            {
                // validaciones adicionales anidadas
            }
        }
        break;
}
```

#### Patrón 3: Múltiples Condiciones Complejas
```csharp
// ❌ PROBLEMÁTICO: Condiciones complejas
if ((customer.Type == "PREMIUM" && order.Total > 1000 && customer.Years > 5) ||
    (customer.Type == "GOLD" && order.Total > 500 && customer.Years > 2) ||
    (customer.Type == "SILVER" && order.Total > 200))
{
    // lógica de descuentos compleja
}
```

### Paso 3: Aplicar Guard Clauses
**Tiempo estimado: 15 minutos**

**Guard Clauses** eliminan anidación convirtiendo condiciones positivas en early returns negativos.

#### Técnica: Invertir Condiciones

```csharp
// PASO 3.1: Identificar anidación problemática
if (order != null)
{
    if (customer != null)
    {
        if (customer.IsActive)
        {
            // lógica principal
        }
        else
        {
            return false; // Error: customer inactive
        }
    }
    else
    {
        return false; // Error: customer null
    }
}
else
{
    return false; // Error: order null
}

// PASO 3.2: Convertir a Guard Clauses
if (order == null)
{
    return false; // Guard clause
}

if (customer == null)
{
    return false; // Guard clause
}

if (!customer.IsActive)
{
    return false; // Guard clause
}

// Lógica principal sin anidación
// Complejidad reducida significativamente
```

#### Proceso Sistemático:
1. **Identificar la condición más externa**
2. **Invertir la lógica** (if positivo → if negativo)
3. **Agregar early return**
4. **Eliminar else y reducir indentación**
5. **Repetir para cada nivel**

### Paso 4: Extract Method para Responsabilidades
**Tiempo estimado: 20 minutos**

#### 4.1: Identificar Responsabilidades Separadas

```csharp
// PASO 4.1: Método con múltiples responsabilidades
public bool ProcessOrder(Order order, Customer customer)
{
    // Responsabilidad 1: Validación básica (10 líneas)
    if (order == null) return false;
    // ... más validaciones

    // Responsabilidad 2: Validación de productos (15 líneas)
    foreach (var product in order.Products)
    {
        // ... validaciones de productos
    }

    // Responsabilidad 3: Cálculo de descuentos (20 líneas)
    if (customer.Type == "PREMIUM")
    {
        // ... lógica de descuentos
    }

    // Responsabilidad 4: Procesamiento final (10 líneas)
    // ... finalización
}
```

#### 4.2: Extraer Cada Responsabilidad

```csharp
// PASO 4.2: Método principal simplificado
public bool ProcessOrder(Order order, Customer customer)
{
    if (!IsValidOrder(order, customer))        // Responsabilidad 1
        return false;
        
    if (!AreValidProducts(order.Products))     // Responsabilidad 2
        return false;
        
    ApplyDiscounts(order, customer);           // Responsabilidad 3
    
    return FinalizeOrder(order);               // Responsabilidad 4
}

// PASO 4.3: Métodos extraídos con baja complejidad
private bool IsValidOrder(Order order, Customer customer)
{
    // Guard clauses simples
    if (order == null) return false;
    if (customer == null) return false;
    if (!customer.IsActive) return false;
    
    return true; // Complejidad: 4
}

private bool AreValidProducts(List<Product> products)
{
    if (products == null || products.Count == 0)
        return false;
        
    foreach (var product in products)
    {
        if (!IsValidProduct(product))
            return false;
    }
    
    return true; // Complejidad: 3
}
```

### Paso 5: Replace Conditional with Polymorphism
**Tiempo estimado: 25 minutos**

Para **switch statements** complejos o múltiples condicionales del mismo tipo.

#### 5.1: Identificar Switch Problemático

```csharp
// PASO 5.1: Switch statement complejo
public PaymentResult ProcessPayment(PaymentInfo payment)
{
    switch (payment.Method)
    {
        case "CREDIT_CARD":
            // 20 líneas de validación y procesamiento
            if (payment.CreditCard != null)
            {
                // ... validaciones anidadas
            }
            break;
            
        case "PAYPAL":
            // 15 líneas de validación y procesamiento
            // ... más lógica anidada
            break;
            
        case "BANK_TRANSFER":
            // 18 líneas de validación y procesamiento
            // ... más lógica anidada
            break;
    }
}
// Complejidad total: 15+ (CRÍTICO)
```

#### 5.2: Crear Strategy Interface

```csharp
// PASO 5.2: Definir interface común
public interface IPaymentStrategy
{
    PaymentResult ProcessPayment(Order order, PaymentInfo payment, Customer customer);
}
```

#### 5.3: Implementar Strategies Específicas

```csharp
// PASO 5.3: Cada strategy con baja complejidad
public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public PaymentResult ProcessPayment(Order order, PaymentInfo payment, Customer customer)
    {
        // Guard clauses para validación
        var validationResult = ValidateCreditCard(payment.CreditCard);
        if (!validationResult.Success)
            return validationResult;
            
        // Lógica específica simple
        return ProcessCreditCardPayment(order, payment.CreditCard);
    }
    
    private PaymentResult ValidateCreditCard(CreditCardInfo card)
    {
        if (card == null) return Failure("Card required");
        if (string.IsNullOrEmpty(card.Number)) return Failure("Number required");
        if (card.ExpiryDate <= DateTime.Now) return Failure("Card expired");
        
        return Success();
    }
    // Complejidad por método: <5
}
```

#### 5.4: Usar Strategy Pattern

```csharp
// PASO 5.4: Procesador principal simplificado
public class PaymentProcessor
{
    private readonly Dictionary<string, IPaymentStrategy> _strategies;
    
    public PaymentProcessor()
    {
        _strategies = new Dictionary<string, IPaymentStrategy>
        {
            ["CREDIT_CARD"] = new CreditCardPaymentStrategy(),
            ["PAYPAL"] = new PayPalPaymentStrategy(),
            ["BANK_TRANSFER"] = new BankTransferPaymentStrategy()
        };
    }
    
    public PaymentResult ProcessPayment(Order order, PaymentInfo payment, Customer customer)
    {
        if (!_strategies.ContainsKey(payment.Method))
            return Failure($"Unsupported payment method: {payment.Method}");
            
        var strategy = _strategies[payment.Method];
        return strategy.ProcessPayment(order, payment, customer);
    }
    // Complejidad: 2 (EXCELENTE)
}
```

### Paso 6: Replace Complex Conditional with Table-Driven Logic
**Tiempo estimado: 15 minutos**

Para **múltiples condicionales** que siguen patrones similares.

#### 6.1: Identificar Patrón Repetitivo

```csharp
// PASO 6.1: Condicionales repetitivas
public decimal CalculateDiscount(Customer customer, decimal orderTotal)
{
    if (customer.Type == "PREMIUM")
    {
        if (orderTotal > 1000 && customer.Years > 5) return 15;
        if (orderTotal > 500 && customer.Years > 2) return 10;
        if (orderTotal > 100) return 5;
    }
    else if (customer.Type == "GOLD")
    {
        if (orderTotal > 750 && customer.Years > 3) return 12;
        if (orderTotal > 300) return 7;
    }
    // ... más condicionales similares
    
    return 0;
}
// Complejidad: 12 (ALTO)
```

#### 6.2: Crear Tabla de Datos

```csharp
// PASO 6.2: Table-driven logic
public class DiscountCalculator
{
    private readonly Dictionary<string, List<DiscountRule>> _discountRules;
    
    public DiscountCalculator()
    {
        _discountRules = new Dictionary<string, List<DiscountRule>>
        {
            ["PREMIUM"] = new List<DiscountRule>
            {
                new DiscountRule { MinAmount = 1000, MinYears = 5, Discount = 15 },
                new DiscountRule { MinAmount = 500, MinYears = 2, Discount = 10 },
                new DiscountRule { MinAmount = 100, MinYears = 0, Discount = 5 }
            },
            ["GOLD"] = new List<DiscountRule>
            {
                new DiscountRule { MinAmount = 750, MinYears = 3, Discount = 12 },
                new DiscountRule { MinAmount = 300, MinYears = 0, Discount = 7 }
            }
            // Fácil agregar nuevos tipos sin código
        };
    }
    
    public decimal CalculateDiscount(Customer customer, decimal orderTotal)
    {
        if (!_discountRules.ContainsKey(customer.Type))
            return 0;
            
        var rules = _discountRules[customer.Type];
        
        foreach (var rule in rules)
        {
            if (orderTotal >= rule.MinAmount && customer.Years >= rule.MinYears)
                return rule.Discount;
        }
        
        return 0;
    }
    // Complejidad: 3 (EXCELENTE)
}

public class DiscountRule
{
    public decimal MinAmount { get; set; }
    public int MinYears { get; set; }
    public decimal Discount { get; set; }
}
```

## 🧪 Estrategias de Testing

### Testing de Baja Complejidad:
```csharp
// ✅ FÁCIL DE TESTEAR: Complejidad baja
[Test]
public void IsValidOrder_WithNullOrder_ReturnsFalse()
{
    var result = validator.IsValidOrder(null, validCustomer);
    Assert.IsFalse(result); // Un solo path, fácil de verificar
}

[Test]
public void IsValidOrder_WithInactiveCustomer_ReturnsFalse()
{
    customer.IsActive = false;
    var result = validator.IsValidOrder(validOrder, customer);
    Assert.IsFalse(result); // Otro path independiente
}

[Test]
public void IsValidOrder_WithValidData_ReturnsTrue()
{
    var result = validator.IsValidOrder(validOrder, validCustomer);
    Assert.IsTrue(result); // Happy path simple
}
// Total: 3 tests para complejidad 3 ✅
```

### Testing de Alta Complejidad:
```csharp
// ❌ DIFÍCIL DE TESTEAR: Complejidad alta
[Test]
public void ValidateOrderComplex_RequiresExhaustiveTesting()
{
    // Necesitas probar TODAS las combinaciones:
    // - order null/not null
    // - customer null/not null/active/inactive
    // - products null/empty/valid/invalid
    // - shipping null/valid/invalid
    // - payment methods combinations
    // - discount calculations combinations
    
    // Resultado: 50+ test cases necesarios para coverage completo ❌
}
```

## 🚀 Casos Avanzados

### Chain of Responsibility para Validaciones
```csharp
public abstract class ValidationHandler
{
    protected ValidationHandler _nextHandler;
    
    public ValidationHandler SetNext(ValidationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }
    
    public virtual ValidationResult Handle(ValidationRequest request)
    {
        if (_nextHandler != null)
            return _nextHandler.Handle(request);
            
        return ValidationResult.Success();
    }
}

public class OrderBasicsValidator : ValidationHandler
{
    public override ValidationResult Handle(ValidationRequest request)
    {
        if (request.Order == null)
            return ValidationResult.Failure("Order is required");
            
        return base.Handle(request); // Complejidad: 1
    }
}

// Uso: Complejidad total distribuida
var validator = new OrderBasicsValidator()
    .SetNext(new CustomerValidator())
    .SetNext(new ProductsValidator())
    .SetNext(new ShippingValidator());

var result = validator.Handle(request); // Complejidad: 1
```

## ❌ Errores Comunes y Cómo Evitarlos

### Error 1: Extraer Métodos Muy Pequeños
```csharp
// ❌ MAL: Over-extraction
private bool IsNull(object obj)
{
    return obj == null; // Método innecesario
}

// ✅ BIEN: Agrupa validaciones relacionadas
private bool IsValidOrderBasics(Order order, Customer customer)
{
    return order != null && 
           customer != null && 
           customer.IsActive;
}
```

### Error 2: No Usar Early Returns
```csharp
// ❌ MAL: Mantiene anidación
if (order != null)
{
    if (customer != null)
    {
        // lógica principal
    }
}

// ✅ BIEN: Guard clauses
if (order == null) return false;
if (customer == null) return false;

// lógica principal sin anidación
```

### Error 3: Strategy Pattern Innecesario
```csharp
// ❌ MAL: Strategy para lógica simple
public interface IAdditionStrategy
{
    int Add(int a, int b);
}

// ✅ BIEN: Strategy solo para lógica compleja variable
public interface IPaymentStrategy
{
    PaymentResult ProcessPayment(/* parámetros complejos */);
}
```

## 🎯 Métricas de Éxito

### Objetivos por Método:
- **Complejidad ciclomática:** <5
- **Anidación máxima:** <3 niveles
- **Líneas de código:** <20
- **Test cases necesarios:** <10

### Antes vs Después:

| Método | Antes | Después | Técnica Aplicada |
|--------|-------|---------|------------------|
| ValidateOrderComplex | 18 | 3 | Guard Clauses + Extract Method |
| ProcessPayment | 14 | 2 | Strategy Pattern |
| CalculateFinalPrice | 16 | 1 | Table-Driven Logic |
| UpdateInventory | 12 | 2 | Strategy Pattern + Guard Clauses |

### Beneficios Cuantificables:
- ✅ **Testing effort:** 80% reducción en test cases
- ✅ **Bug probability:** 70% reducción estimada
- ✅ **Maintenance time:** 60% reducción
- ✅ **Code readability:** 90% improvement (subjective)

## 🏆 Ejercicio Práctico

### Desafío: Refactorizar ValidateOrderComplex

**Objetivo:** Reducir complejidad de 18 a <5 siguiendo esta guía paso a paso.

**Pasos sugeridos:**
1. **Medir complejidad inicial** (5 min)
2. **Aplicar Guard Clauses** a validaciones anidadas (15 min)
3. **Extract Methods** para cada responsabilidad (20 min)
4. **Replace conditionals** con table-driven logic (15 min)
5. **Verificar métricas finales** (5 min)

### Checklist de Validación:
- [ ] ¿Cada método tiene complejidad <5?
- [ ] ¿Eliminé anidación profunda (>3 niveles)?
- [ ] ¿Cada método hace una sola cosa?
- [ ] ¿Es fácil escribir tests unitarios?
- [ ] ¿El código es autodocumentado?

**Tiempo Estimado:** 60 minutos
**Nivel:** Intermedio-Avanzado

---

## 🔄 Próximo Paso
Una vez dominada la reducción de complejidad ciclomática, estarás listo para **técnicas avanzadas** como design patterns más complejos y arquitecturas limpias.

**Recuerda:** La complejidad baja es el fundamento de código mantenible. ¡Cada decisión cuenta!