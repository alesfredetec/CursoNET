# Guía de Mentoring: Delegates, Action y Func - Paso a Paso

## 🎯 Objetivo del Ejercicio
Aprender a usar **delegates, Action y Func** para eliminar código duplicado y crear código más flexible y reutilizable.

## 🧠 ¿Qué son los Delegates?

Un **delegate** es como una "variable que contiene una función". Permite pasar funciones como parámetros, almacenarlas en variables y ejecutarlas cuando sea necesario.

### Analogía del Mundo Real:
Imagina que tienes un **control remoto** (delegate) que puede ejecutar diferentes funciones dependiendo del botón que presiones:
- Botón 1 → Función: duplicar número
- Botón 2 → Función: elevar al cuadrado  
- Botón 3 → Función: sumar 10

El control remoto es el **delegate**, y las funciones específicas son los **comportamientos** que puede ejecutar.

## 📚 Tipos de Delegates en C#

### 1. **Action** - "Hacer algo" (No retorna valor)
```csharp
// Action - Sin parámetros, no retorna nada
Action saludar = () => Console.WriteLine("¡Hola!");

// Action<T> - Recibe parámetro, no retorna nada
Action<string> imprimir = mensaje => Console.WriteLine(mensaje);

// Action<T1, T2> - Múltiples parámetros
Action<string, int> imprimirConNumero = (texto, numero) => 
    Console.WriteLine($"{texto}: {numero}");
```

### 2. **Func** - "Calcular algo" (Retorna valor)
```csharp
// Func<TResult> - Sin parámetros, retorna TResult
Func<int> obtenerNumeroAleatorio = () => new Random().Next(1, 100);

// Func<T, TResult> - Recibe T, retorna TResult
Func<int, int> duplicar = x => x * 2;

// Func<T1, T2, TResult> - Múltiples parámetros
Func<int, int, int> sumar = (a, b) => a + b;
```

### 3. **Predicate** - "Verificar algo" (Retorna bool)
```csharp
// Predicate<T> - Recibe T, retorna bool
Predicate<int> esPositivo = numero => numero > 0;
Predicate<string> noEsVacio = texto => !string.IsNullOrEmpty(texto);
```

## 🔍 Identificando Cuándo Usar Delegates

### Señales de Alerta (Code Smells):
1. **Métodos casi idénticos** que solo difieren en una operación
2. **Código duplicado** con pequeñas variaciones
3. **Switch/if largos** para seleccionar comportamientos
4. **Imposibilidad de agregar nuevos comportamientos** sin modificar código
5. **Lógica hardcoded** que debería ser configurable

### Ejemplo de Identificación:
```csharp
// ❌ ANTES: Métodos duplicados
public List<int> ProcessNumbersDouble(List<int> numbers)
{
    var result = new List<int>();
    foreach (var number in numbers)
    {
        result.Add(number * 2); // ← Solo esta línea cambia
    }
    return result;
}

public List<int> ProcessNumbersSquare(List<int> numbers)
{
    var result = new List<int>();
    foreach (var number in numbers)
    {
        result.Add(number * number); // ← Solo esta línea cambia
    }
    return result;
}

// 🤔 PREGUNTA: ¿Qué es lo que realmente cambia?
// ✅ RESPUESTA: Solo la operación matemática (x * 2 vs x * x)
```

## 📋 Proceso Paso a Paso

### Paso 1: Identificar el Patrón Común
**Tiempo estimado: 10 minutos**

1. **Analizar métodos similares** y marcar las diferencias
2. **Identificar la estructura común** (bucles, validaciones, logging)
3. **Aislar la lógica que cambia** entre métodos
4. **Determinar qué tipo de delegate necesitas**

**Ejercicio práctico:**
```csharp
// En los métodos ProcessNumbers, identifica:
// - ¿Qué partes son idénticas en todos los métodos?
// - ¿Qué línea(s) específica(s) cambian?
// - ¿Qué tipo de operación realizan? (transformación, validación, etc.)
```

### Paso 2: Elegir el Tipo de Delegate Correcto
**Tiempo estimado: 5 minutos**

**Guía de Decisión:**

| Si tu lógica... | Usa... | Ejemplo |
|----------------|---------|---------|
| **Transforma** un valor en otro | `Func<T, TResult>` | `x => x * 2` |
| **Ejecuta** una acción sin retornar | `Action<T>` | `x => Console.WriteLine(x)` |
| **Valida** y retorna true/false | `Predicate<T>` | `x => x > 0` |
| **Combina** dos valores en uno | `Func<T, T, T>` | `(a, b) => a + b` |
| **Formatea** un objeto a string | `Func<T, string>` | `emp => $"{emp.Name}: {emp.Salary}"` |

### Paso 3: Crear el Método Genérico
**Tiempo estimado: 15 minutos**

**Proceso técnico:**
```csharp
// PASO 3.1: Identificar la estructura común
public List<int> ProcessNumbers???(List<int> numbers)
{
    var result = new List<int>();
    
    Console.WriteLine("Starting to process numbers...");
    
    foreach (var number in numbers)
    {
        var processedValue = ???; // ← Esto cambia
        result.Add(processedValue);
        Console.WriteLine($"Processed {number} -> {processedValue}");
    }
    
    Console.WriteLine($"Completed processing. Total items: {result.Count}");
    return result;
}

// PASO 3.2: Parametrizar la lógica variable
public List<int> ProcessNumbers(List<int> numbers, 
                               Func<int, int> transformation, // ← La operación como parámetro
                               string operationName)          // ← Para logging descriptivo
{
    var result = new List<int>();
    
    Console.WriteLine($"Starting to process numbers for {operationName}...");
    
    foreach (var number in numbers)
    {
        var processedValue = transformation(number); // ← Llamar al delegate
        result.Add(processedValue);
        Console.WriteLine($"Processed {number} -> {processedValue}");
    }
    
    Console.WriteLine($"Completed processing. Total items: {result.Count}");
    return result;
}
```

### Paso 4: Adaptar los Métodos Específicos
**Tiempo estimado: 10 minutos**

**Convertir métodos largos en llamadas concisas:**
```csharp
// PASO 4.1: Método original (30+ líneas) → Una línea
public List<int> ProcessNumbersDouble(List<int> numbers)
    => ProcessNumbers(numbers, x => x * 2, "doubling");

// PASO 4.2: Todos los demás métodos
public List<int> ProcessNumbersSquare(List<int> numbers)
    => ProcessNumbers(numbers, x => x * x, "squaring");
    
public List<int> ProcessNumbersAddTen(List<int> numbers)
    => ProcessNumbers(numbers, x => x + 10, "adding ten");
    
public List<int> ProcessNumbersNegate(List<int> numbers)
    => ProcessNumbers(numbers, x => -x, "negation");
```

### Paso 5: Probar y Validar
**Tiempo estimado: 10 minutos**

**Checklist de validación:**
- [ ] ¿El método genérico funciona con diferentes delegates?
- [ ] ¿Los métodos específicos producen los mismos resultados?
- [ ] ¿Es fácil agregar nuevos comportamientos?
- [ ] ¿El código es más legible?

**Test rápido:**
```csharp
var numbers = new List<int> { 1, 2, 3, 4, 5 };

// Probar que funcionan igual que antes
var doubled = processor.ProcessNumbersDouble(numbers);
var squared = processor.ProcessNumbersSquare(numbers);

// Probar nuevos comportamientos (sin modificar código existente)
var tripled = processor.ProcessNumbers(numbers, x => x * 3, "tripling");
var fibonacci = processor.ProcessNumbers(numbers, x => x + x - 1, "fibonacci-like");
```

## 🎨 Patrones de Uso Comunes

### Patrón 1: Transformación de Datos
**Cuándo:** Necesitas aplicar diferentes operaciones a elementos de una colección
```csharp
// ✅ PATRÓN GENÉRICO
public List<TResult> Transform<T, TResult>(List<T> items, Func<T, TResult> transformer)
{
    var result = new List<TResult>();
    foreach (var item in items)
    {
        result.Add(transformer(item));
    }
    return result;
}

// ✅ USOS ESPECÍFICOS
var doubled = Transform(numbers, x => x * 2);
var names = Transform(employees, emp => emp.Name);
var prices = Transform(products, prod => prod.Price);
```

### Patrón 2: Validación Configurable
**Cuándo:** Necesitas aplicar diferentes criterios de validación
```csharp
// ✅ PATRÓN GENÉRICO
public List<T> Filter<T>(List<T> items, Predicate<T> criteria)
{
    var result = new List<T>();
    foreach (var item in items)
    {
        if (criteria(item))
            result.Add(item);
    }
    return result;
}

// ✅ USOS ESPECÍFICOS
var positives = Filter(numbers, x => x > 0);
var adults = Filter(people, person => person.Age >= 18);
var activeUsers = Filter(users, user => user.IsActive);
```

### Patrón 3: Procesamiento con Callbacks
**Cuándo:** Necesitas notificar o ejecutar acciones durante el procesamiento
```csharp
// ✅ PATRÓN GENÉRICO
public void ProcessWithCallback<T>(List<T> items, 
                                  Action<T> processor, 
                                  Action<int> onCompleted)
{
    foreach (var item in items)
    {
        processor(item);
    }
    onCompleted(items.Count);
}

// ✅ USOS ESPECÍFICOS
ProcessWithCallback(
    orders,
    order => SendConfirmationEmail(order),
    count => Console.WriteLine($"Processed {count} orders")
);
```

### Patrón 4: Agregación Personalizada
**Cuándo:** Necesitas calcular valores agregados con diferentes operaciones
```csharp
// ✅ PATRÓN GENÉRICO
public T Aggregate<T>(List<T> items, T initialValue, Func<T, T, T> aggregator)
{
    T result = initialValue;
    foreach (var item in items)
    {
        result = aggregator(result, item);
    }
    return result;
}

// ✅ USOS ESPECÍFICOS
var sum = Aggregate(numbers, 0, (acc, val) => acc + val);
var product = Aggregate(numbers, 1, (acc, val) => acc * val);
var max = Aggregate(numbers, int.MinValue, (acc, val) => Math.Max(acc, val));
```

## 🧪 Estrategias de Testing

### Testear Métodos Genéricos:
```csharp
[Test]
public void ProcessNumbers_WithDoubling_ReturnsDoubledValues()
{
    // Arrange
    var processor = new DataProcessor();
    var input = new List<int> { 1, 2, 3 };
    var expected = new List<int> { 2, 4, 6 };
    
    // Act
    var result = processor.ProcessNumbers(input, x => x * 2, "doubling");
    
    // Assert
    CollectionAssert.AreEqual(expected, result);
}

[Test]
public void ProcessNumbers_WithCustomOperation_WorksCorrectly()
{
    // Arrange
    var processor = new DataProcessor();
    var input = new List<int> { 5, 10, 15 };
    
    // Act - operación personalizada: convertir a string length
    var result = processor.ProcessNumbers(input, x => x.ToString().Length, "string length");
    
    // Assert
    Assert.AreEqual(1, result[0]); // "5".Length = 1
    Assert.AreEqual(2, result[1]); // "10".Length = 2
    Assert.AreEqual(2, result[2]); // "15".Length = 2
}
```

### Testear Delegates Específicos:
```csharp
[Test]
public void ValidateEmails_WithValidEmails_ReturnsValidList()
{
    // Arrange
    var validator = new ValidationProcessor();
    var emails = new List<string> { "test@example.com", "invalid-email", "another@test.org" };
    
    // Act
    var result = validator.ValidateEmails(emails);
    
    // Assert
    Assert.AreEqual(2, result.Count);
    Assert.Contains("test@example.com", result);
    Assert.Contains("another@test.org", result);
}
```

## 🚀 Casos Avanzados

### Caso 1: Múltiples Delegates en Pipeline
```csharp
public List<TResult> ProcessPipeline<T, TResult>(
    List<T> items,
    Predicate<T> filter,        // Filtrar
    Func<T, TResult> transform, // Transformar
    Action<TResult> sideEffect) // Efecto lateral
{
    var result = new List<TResult>();
    
    foreach (var item in items)
    {
        if (filter(item))
        {
            var transformed = transform(item);
            sideEffect(transformed);
            result.Add(transformed);
        }
    }
    
    return result;
}

// Uso: Filtrar números pares, elevar al cuadrado, imprimir resultado
var result = ProcessPipeline(
    numbers,
    x => x % 2 == 0,                    // Solo pares
    x => x * x,                         // Elevar al cuadrado  
    x => Console.WriteLine($"Result: {x}") // Imprimir
);
```

### Caso 2: Delegate Factory Pattern
```csharp
public class TransformationFactory
{
    public static Func<int, int> CreateMathOperation(string operation)
    {
        return operation.ToUpper() switch
        {
            "DOUBLE" => x => x * 2,
            "SQUARE" => x => x * x,
            "NEGATE" => x => -x,
            "ABS" => x => Math.Abs(x),
            _ => x => x // Identity
        };
    }
    
    // Uso dinámico
    public void ProcessWithDynamicOperation(List<int> numbers, string operation)
    {
        var transformer = CreateMathOperation(operation);
        var result = ProcessNumbers(numbers, transformer, operation);
    }
}
```

## ❌ Errores Comunes y Cómo Evitarlos

### Error 1: Usar Delegate Incorrecto
```csharp
// ❌ MAL - Usar Action cuando necesitas retorno
public List<int> ProcessNumbers(List<int> numbers, Action<int> processor)
{
    // No puedes obtener resultado de Action
}

// ✅ BIEN - Usar Func para transformaciones
public List<int> ProcessNumbers(List<int> numbers, Func<int, int> transformer)
{
    // Func retorna el valor transformado
}
```

### Error 2: Delegates Demasiado Complejos
```csharp
// ❌ MAL - Delegate que hace demasiado
Func<Employee, Customer, Order, bool, string, decimal> complexDelegate = 
    (emp, cust, order, flag, note) => { /* lógica compleja */ };

// ✅ BIEN - Delegates simples y enfocados
Func<Employee, decimal> calculateBonus = emp => emp.Salary * 0.1m;
Predicate<Order> isValidOrder = order => order.Total > 0;
```

### Error 3: No Usar Nombres Descriptivos
```csharp
// ❌ MAL - Nombres genéricos
public void Process(List<object> data, Func<object, object> func)

// ✅ BIEN - Nombres descriptivos
public List<int> TransformNumbers(List<int> numbers, Func<int, int> transformation)
```

## 🎯 Métricas de Éxito

### Antes del Refactoring:
- **4 métodos duplicados** → ProcessNumbersDouble, Square, AddTen, Negate
- **120 líneas** de código repetitivo
- **Imposible** agregar nuevos comportamientos sin duplicar código

### Después del Refactoring:
- **1 método genérico** + 4 métodos de una línea
- **30 líneas** de código total
- **Infinitas** transformaciones posibles sin duplicar código

### Objetivos Jr:
- ✅ Reducir duplicación en 70%+
- ✅ Método genérico reutilizable
- ✅ Fácil agregar nuevos comportamientos
- ✅ Código más expresivo y legible

## 🏆 Ejercicio Práctico

### Desafío:
Toma la clase `ValidationProcessorProblematic` y refactorízala usando `Predicate<T>` siguiendo esta guía.

### Pasos sugeridos:
1. Identificar la estructura común en los 3 métodos de validación
2. Crear método genérico `ValidateItems<T>(List<T>, Predicate<T>, string)`
3. Convertir métodos específicos en llamadas de una línea
4. Probar que funcionan igual que antes
5. Agregar 2 nuevas validaciones sin duplicar código

### Checklist de Validación:
- [ ] ¿Eliminé al menos 70% de código duplicado?
- [ ] ¿El método genérico funciona con diferentes tipos?
- [ ] ¿Los nombres de delegates son descriptivos?
- [ ] ¿Es fácil agregar nuevas validaciones?
- [ ] ¿El código es más expresivo que antes?

### Tiempo Estimado: 30 minutos
### Nivel: Intermedio

---

## 🔄 Próximo Paso
Una vez dominados los delegates básicos, estarás listo para **Extract Base Class**, donde aprenderemos a eliminar duplicación entre clases usando herencia.

**Recuerda:** Los delegates son poderosos pero úsalos con moderación. ¡La simplicidad siempre gana!