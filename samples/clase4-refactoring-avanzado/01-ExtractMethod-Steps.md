# Guía de Mentoring: Extract Method - Paso a Paso

## 🎯 Objetivo del Ejercicio
Aprender a **extraer métodos** para transformar código complejo y difícil de mantener en código limpio, legible y testeable.

## 🧠 ¿Qué es Extract Method?

**Extract Method** es una técnica de refactoring que consiste en tomar un fragmento de código que realiza una tarea específica y convertirlo en un método separado con un nombre descriptivo.

### ¿Por qué es importante?
- **Legibilidad**: El código se vuelve autodocumentado
- **Mantenibilidad**: Cambios aislados en métodos específicos  
- **Testabilidad**: Cada método se puede probar por separado
- **Reutilización**: Los métodos extraídos se pueden usar en otros lugares
- **Debugging**: Más fácil encontrar y arreglar problemas

## 🔍 Identificando Cuándo Aplicar Extract Method

### Señales de Alerta (Code Smells):
1. **Método muy largo** (>20 líneas)
2. **Complejidad ciclomática alta** (>10)
3. **Múltiples responsabilidades** en un método
4. **Comentarios que dicen "// Hacer X"** (el código debería ser autodocumentado)
5. **Código duplicado** en el mismo método o entre métodos
6. **Dificultad para escribir tests** unitarios

### Ejemplo de Identificación:
```csharp
// ❌ ANTES: Método con múltiples responsabilidades
public bool ProcessOrder(Order order)
{
    // Validación (10 líneas)
    if (order == null) return false;
    // ... más validaciones
    
    // Cálculos (15 líneas)  
    decimal total = 0;
    // ... cálculos complejos
    
    // Notificaciones (8 líneas)
    Console.WriteLine("Sending email...");
    // ... lógica de email
    
    return true;
}
```

## 📋 Proceso Paso a Paso

### Paso 1: Analizar el Método Original
**Tiempo estimado: 5 minutos**

1. **Leer el método completo** y entender qué hace
2. **Identificar las diferentes responsabilidades**
3. **Marcar las secciones** que realizan tareas específicas
4. **Anotar la complejidad actual** (líneas, ciclos, condiciones)

**Ejercicio práctico:**
```csharp
// En ProcessCustomerOrder, identifica:
// - ¿Cuántas líneas tiene?
// - ¿Cuántas responsabilidades diferentes realiza?
// - ¿Qué partes son más complejas?
```

### Paso 2: Identificar Candidatos para Extracción
**Tiempo estimado: 10 minutos**

**Criterios para identificar candidatos:**
- Bloques de código con **comentarios explicativos**
- Código que realiza **una tarea específica**
- **Lógica repetida** o similar
- **Condicionales complejas**
- **Bucles con lógica interna**

**Técnica del "Si esto fuera un método, ¿cómo se llamaría?"**

Ejemplo de análisis:
```csharp
// ❌ ANTES: En el método original
// Validación (15 líneas)
if (order == null)
{
    Console.WriteLine("ERROR: Order is null");
    return false;
}
// ... más validaciones

// 🤔 PREGUNTA: ¿Cómo llamarías a esta sección?
// ✅ RESPUESTA: IsValidOrder() o ValidateOrder()
```

### Paso 3: Extraer el Primer Método
**Tiempo estimado: 10 minutos**

**Reglas de oro:**
1. **Comenzar con el candidato más simple**
2. **Elegir un nombre descriptivo** que explique QUÉ hace (no cómo)
3. **Identificar qué parámetros necesita**
4. **Determinar qué debe retornar**

**Proceso técnico:**
```csharp
// PASO 3.1: Seleccionar el código a extraer
if (order == null)
{
    Console.WriteLine("ERROR: Order is null");
    return false;
}
if (string.IsNullOrEmpty(order.Id))
{
    Console.WriteLine("ERROR: Order ID is required");
    return false;
}

// PASO 3.2: Crear el método extraído
private bool IsValidOrder(CustomerOrder order)
{
    if (order == null)
    {
        Console.WriteLine("ERROR: Order is null");
        return false;
    }
    
    if (string.IsNullOrEmpty(order.Id))
    {
        Console.WriteLine("ERROR: Order ID is required");
        return false;
    }
    
    return true;
}

// PASO 3.3: Reemplazar en el método original
public bool ProcessCustomerOrder(CustomerOrder order)
{
    if (!IsValidOrder(order))
        return false;
        
    // ... resto del código
}
```

### Paso 4: Refinar y Mejorar
**Tiempo estimado: 5 minutos**

**Checklist de calidad:**
- [ ] ¿El nombre del método es claro y descriptivo?
- [ ] ¿Tiene una sola responsabilidad?
- [ ] ¿Los parámetros están bien definidos?
- [ ] ¿El valor de retorno es apropiado?
- [ ] ¿Se puede entender sin leer la implementación?

**Mejoras comunes:**
```csharp
// ❌ NOMBRE POCO CLARO
private bool CheckOrder(Order order)

// ✅ NOMBRE DESCRIPTIVO  
private bool IsValidOrder(Order order)

// ❌ MÚLTIPLES RESPONSABILIDADES
private bool ValidateAndCalculate(Order order)

// ✅ RESPONSABILIDAD ÚNICA
private bool IsValidOrder(Order order)
private void CalculateOrderTotals(Order order)
```

### Paso 5: Repetir el Proceso
**Tiempo estimado: 15 minutos por método**

**Estrategia recomendada:**
1. **Extraer de lo simple a lo complejo**
2. **Una extracción a la vez**
3. **Probar después de cada cambio**
4. **Refinar nombres si es necesario**

**Orden sugerido de extracción:**
1. **Validaciones** (suelen ser simples)
2. **Cálculos** (lógica bien definida)
3. **Operaciones de entrada/salida** (I/O)
4. **Lógica de negocio compleja**

## 🎨 Patrones de Nomenclatura

### Para Métodos de Validación:
```csharp
// ✅ BUENOS NOMBRES
private bool IsValidOrder(Order order)
private bool HasRequiredFields(Customer customer)  
private bool AreValidOrderItems(List<OrderItem> items)

// ❌ NOMBRES CONFUSOS
private bool CheckOrder(Order order)
private bool ValidateStuff(object data)
private bool Process(List<object> items)
```

### Para Métodos de Cálculo:
```csharp
// ✅ BUENOS NOMBRES
private decimal CalculateSubtotal(List<OrderItem> items)
private decimal GetTaxRateByState(string state)
private decimal ApplyVolumeDiscount(decimal amount, int quantity)

// ❌ NOMBRES CONFUSOS  
private decimal Calculate(List<object> data)
private decimal GetRate(string input)
private decimal Apply(decimal x, int y)
```

### Para Métodos de Acción:
```csharp
// ✅ BUENOS NOMBRES
private void SendConfirmationEmail(CustomerOrder order)
private void ReserveInventory(string productId, int quantity)
private void LogOrderCompletion(CustomerOrder order)

// ❌ NOMBRES CONFUSOS
private void Send(object data)
private void Reserve(string id, int num)
private void Log(object info)
```

## 🧪 Estrategias de Testing

### Antes del Refactoring:
```csharp
// ❌ DIFÍCIL DE TESTEAR
[Test]
public void TestProcessOrder()
{
    // ¿Qué parte específica estamos probando?
    // ¿Validación? ¿Cálculos? ¿Notificaciones?
    var result = processor.ProcessCustomerOrder(order);
    Assert.IsTrue(result);
}
```

### Después del Refactoring:
```csharp
// ✅ FÁCIL DE TESTEAR CADA PARTE
[Test]
public void IsValidOrder_WithNullOrder_ReturnsFalse()
{
    var result = processor.IsValidOrder(null);
    Assert.IsFalse(result);
}

[Test]
public void CalculateSubtotal_WithVolumeDiscount_AppliesCorrectDiscount()
{
    var items = CreateItemsWithQuantity(10);
    var result = processor.CalculateSubtotal(items);
    Assert.AreEqual(expectedDiscountedAmount, result);
}
```

## 🚀 Casos de Uso Comunes

### Caso 1: Métodos con Validaciones Complejas
**Cuándo:** Múltiples validaciones en secuencia
**Patrón:** Extraer cada grupo de validaciones
```csharp
// ANTES
public bool ProcessData(Data data)
{
    // 20 líneas de validaciones mezcladas
}

// DESPUÉS  
public bool ProcessData(Data data)
{
    if (!IsValidBasicInfo(data)) return false;
    if (!IsValidBusinessRules(data)) return false;
    if (!IsValidSecurityConstraints(data)) return false;
    
    return ProcessValidData(data);
}
```

### Caso 2: Métodos con Cálculos Complejos
**Cuándo:** Múltiples cálculos matemáticos o de negocio
**Patrón:** Extraer cada tipo de cálculo
```csharp
// ANTES
public decimal CalculatePrice(Order order)
{
    // 30 líneas mezclando subtotal, descuentos, impuestos, envío
}

// DESPUÉS
public decimal CalculatePrice(Order order)  
{
    var subtotal = CalculateSubtotal(order.Items);
    var discount = CalculateDiscount(subtotal, order.Customer);
    var taxes = CalculateTaxes(subtotal - discount, order.ShippingAddress);
    var shipping = CalculateShipping(order.Weight, order.Distance);
    
    return subtotal - discount + taxes + shipping;
}
```

### Caso 3: Métodos con Múltiples Responsabilidades de I/O
**Cuándo:** Mezclando lecturas, escrituras, notificaciones
**Patrón:** Separar por tipo de operación
```csharp
// ANTES
public void ProcessReport(ReportData data)
{
    // Lectura de datos, procesamiento, escritura de archivos, 
    // envío de emails, logging - todo mezclado
}

// DESPUÉS
public void ProcessReport(ReportData data)
{
    var processedData = ProcessReportData(data);
    var reportFile = GenerateReportFile(processedData);
    
    SaveReportToFile(reportFile);
    SendReportByEmail(reportFile, data.Recipients);
    LogReportGeneration(data.ReportId);
}
```

## ❌ Errores Comunes y Cómo Evitarlos

### Error 1: Métodos con Nombres Genéricos
```csharp
// ❌ MAL
private void Process(object data)
private bool Check(string input)
private void Handle(List<object> items)

// ✅ BIEN
private void ProcessPayment(PaymentRequest request)
private bool IsValidEmail(string email)
private void HandleOrderItems(List<OrderItem> items)
```

### Error 2: Extraer Demasiado (Over-extraction)
```csharp
// ❌ EXCESO - Métodos de 1-2 líneas sin valor
private bool IsNull(object obj)
{
    return obj == null;
}

// ✅ EQUILIBRIO - Agrupa validaciones relacionadas
private bool IsValidOrder(Order order)
{
    return order != null && 
           !string.IsNullOrEmpty(order.Id) && 
           order.Items?.Count > 0;
}
```

### Error 3: Parámetros Excesivos
```csharp
// ❌ MUCHOS PARÁMETROS
private decimal Calculate(decimal a, decimal b, decimal c, 
                         decimal d, string e, bool f, int g)

// ✅ USAR OBJETOS DE CONFIGURACIÓN
private decimal CalculatePrice(PriceCalculationConfig config)
```

## 🎯 Métricas de Éxito

### Antes del Refactoring:
- **Líneas por método:** 65
- **Complejidad ciclomática:** 12
- **Responsabilidades:** 6+
- **Testabilidad:** Baja

### Después del Refactoring:
- **Líneas por método:** 5-15
- **Complejidad ciclomática:** 1-4
- **Responsabilidades:** 1 por método
- **Testabilidad:** Alta

### Objetivos Jr (metas realistas):
- ✅ Métodos < 20 líneas
- ✅ Complejidad < 5
- ✅ Una responsabilidad por método
- ✅ Nombres autodocumentados

## 🏆 Ejercicio Práctico

### Desafío:
Toma el método `ProcessCustomerOrder` del archivo `-Before.cs` y aplica Extract Method siguiendo esta guía paso a paso.

### Checklist de Validación:
- [ ] ¿Extraje al menos 5 métodos diferentes?
- [ ] ¿Cada método tiene un nombre descriptivo?
- [ ] ¿Cada método tiene una sola responsabilidad?
- [ ] ¿El método principal es fácil de leer como una historia?
- [ ] ¿Se pueden escribir tests para cada método extraído?

### Tiempo Estimado: 45 minutos
### Nivel: Principiante-Intermedio

---

## 🔄 Próximo Paso
Una vez dominado Extract Method, estás listo para aprender **Delegates y Action/Func** en el siguiente ejercicio, donde veremos cómo eliminar código repetitivo usando funciones como parámetros.

**Recuerda:** La práctica hace al maestro. ¡Refactoriza todos los días!