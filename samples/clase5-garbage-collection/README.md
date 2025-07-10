# Clase 5: Garbage Collection y Manejo de Memoria

## 📋 Descripción
Ejercicios prácticos para dominar el Garbage Collector de .NET, manejo eficiente de memoria, patrón IDisposable y optimización de performance relacionada con memoria.

## 🎯 Objetivos de Aprendizaje
- Entender cómo funciona el GC de .NET y sus generaciones
- Identificar y resolver memory leaks comunes
- Implementar correctamente el patrón IDisposable
- Optimizar código para minimizar presión en GC
- Usar herramientas de profiling de memoria
- Manejar recursos no administrados de forma segura

## 📁 Estructura de Archivos

### Ejercicio 1: Memory Leaks Detection
- `01-MemoryLeaks-Before.cs` - Código con multiple memory leaks
- `01-MemoryLeaks-After.cs` - Soluciones y best practices
- `01-MemoryProfiling-Guide.md` - Guía para usar herramientas de profiling

### Ejercicio 2: IDisposable Pattern
- `02-IDisposable-Before.cs` - Manejo incorrecto de recursos
- `02-IDisposable-After.cs` - Implementación correcta del patrón
- `02-ResourceManagement-Best-Practices.md` - Best practices para recursos

### Ejercicio 3: GC Optimization
- `03-GC-Optimization-Before.cs` - Código que presiona el GC
- `03-GC-Optimization-After.cs` - Técnicas de optimización
- `03-Performance-Benchmarks.md` - Métricas antes/después

## 🧠 Conceptos Fundamentales

### Generaciones del Garbage Collector
- **Gen 0**: Objetos nuevos, recolección frecuente y rápida
- **Gen 1**: Objetos que sobrevivieron una recolección de Gen 0
- **Gen 2**: Objetos de larga duración, recolección menos frecuente
- **Large Object Heap (LOH)**: Objetos > 85KB

### Tipos de Recolección
- **Workstation GC**: Para aplicaciones cliente
- **Server GC**: Para aplicaciones servidor
- **Concurrent GC**: Recolección en background
- **Low Latency GC**: Para aplicaciones time-sensitive

## ⚠️ Memory Leaks Comunes en .NET

### 1. Event Handlers No Desregistrados
```csharp
// ❌ PROBLEMA: Memory leak
publisher.SomeEvent += handler;
// Nunca se desregistra

// ✅ SOLUCIÓN: Desregistrar eventos
publisher.SomeEvent -= handler;
```

### 2. Static Collections que Crecen
```csharp
// ❌ PROBLEMA: Cache que nunca se limpia
private static Dictionary<string, object> _cache = new();

// ✅ SOLUCIÓN: Cache con expiry/limit
private static MemoryCache _cache = new MemoryCache();
```

### 3. Timers No Disposed
```csharp
// ❌ PROBLEMA: Timer mantiene referencia
var timer = new Timer(callback, null, 0, 1000);

// ✅ SOLUCIÓN: Dispose del timer
timer.Dispose();
```

## 🛠️ Herramientas de Profiling

### Visual Studio Diagnostic Tools
- Memory Usage analyzer integrado
- CPU Usage profiler
- Events viewer para GC

### JetBrains dotMemory
- Memory profiler profesional
- Snapshot comparison
- Memory traffic analysis

### PerfView (Microsoft)
- Herramienta gratuita de Microsoft
- ETW events analysis
- GC analysis detallado

### Application Insights
- Monitoring en producción
- Memory metrics
- Performance counters

## 💡 Best Practices para Desarrolladores Jr

### IDisposable Implementation
```csharp
public class ResourceHolder : IDisposable
{
    private bool _disposed = false;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Cleanup managed resources
            }
            // Cleanup unmanaged resources
            _disposed = true;
        }
    }
}
```

### Using Statement Always
```csharp
// ✅ CORRECTO: Automatic disposal
using (var resource = new SomeResource())
{
    // Use resource
} // Automatically disposed

// ✅ MODERNO: Using declaration
using var resource = new SomeResource();
// Use resource
// Automatically disposed at end of scope
```

### Avoid Boxing/Unboxing
```csharp
// ❌ PROBLEMA: Boxing creates objects
object obj = 42; // Boxing
int value = (int)obj; // Unboxing

// ✅ SOLUCIÓN: Use generics
List<int> numbers = new List<int>(); // No boxing
```

## 🚀 Técnicas de Optimización

### Object Pooling
```csharp
// Reuse objects instead of creating new ones
var pool = new ObjectPool<StringBuilder>(() => new StringBuilder());
var sb = pool.Get();
try
{
    // Use StringBuilder
}
finally
{
    pool.Return(sb);
}
```

### Struct vs Class Decision
```csharp
// Use struct for small, immutable data
public struct Point
{
    public int X { get; }
    public int Y { get; }
}

// Use class for complex, mutable objects
public class Employee
{
    public string Name { get; set; }
    public List<Skill> Skills { get; set; }
}
```

### Lazy Initialization
```csharp
// Don't create until needed
private readonly Lazy<ExpensiveObject> _expensive = 
    new Lazy<ExpensiveObject>(() => new ExpensiveObject());

public ExpensiveObject Expensive => _expensive.Value;
```

## 📊 Métricas a Monitorear

### GC Performance Counters
- **Gen 0-2 Collections/sec**: Frecuencia de recolección
- **% Time in GC**: Porcentaje de tiempo en GC
- **Allocated Bytes/sec**: Tasa de allocación
- **Large Object Heap size**: Tamaño del LOH

### Memory Metrics
- **Working Set**: Memoria física usada
- **Private Bytes**: Memoria virtual privada
- **Virtual Bytes**: Memoria virtual total
- **Managed Heap Size**: Tamaño del heap administrado

## ⚡ Performance Tips

### Reduce Allocations
- Use StringBuilder for string concatenation
- Reuse collections instead of creating new ones
- Consider ValueTask instead of Task for hot paths
- Use ReadOnlySpan<T> for slicing arrays

### GC-Friendly Patterns
- Avoid creating temporary objects in loops
- Use object pooling for frequently created objects
- Keep object graphs shallow
- Prefer structs for small data

### Finalizer Guidelines
- Avoid finalizers if possible
- If needed, make them fast and simple
- Always call GC.SuppressFinalize() in Dispose()
- Don't depend on finalization order

## 🎯 Red Flags - Problemas Comunes

### Memory Pressure Signs
- OutOfMemoryException frecuentes
- GC Collections muy frecuentes
- Application slowdowns
- High memory usage in Task Manager

### Code Smells
- Missing using statements
- Static collections that grow indefinitely
- Event handlers never unregistered
- Large objects allocated frequently
- Unnecessary string concatenation

## 📚 Recursos Adicionales
- [.NET Garbage Collection Documentation](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/)
- [Memory Management Best Practices](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/memory-management-and-gc)
- [IDisposable Pattern Guide](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose)

## 🎓 Challenge Final
Después de completar estos ejercicios:
- Diagnosticar memory leaks en aplicación real
- Implementar object pooling pattern
- Optimizar GC performance en 20%
- Crear monitoring dashboard de memoria

**TIEMPO ESTIMADO:** 2-3 horas
**DIFICULTAD:** Intermedio-Avanzado
**HERRAMIENTAS:** Visual Studio, PerfView, dotMemory