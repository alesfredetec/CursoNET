# Guía Visual Studio Professional: Delegates y Refactoring con IDE

## 🎯 Objetivo
Dominar el uso de **Delegates, Action y Func** aprovechando las herramientas automáticas de **Visual Studio Professional** para eliminar código duplicado.

## 🛠️ Configuración IDE para Delegates

### Paso 1: Configurar Análisis de Duplicación
**Tiempo: 3 minutos**

1. **Habilitar análisis de código duplicado:**
   ```
   Tools → Options → Text Editor → C# → Advanced
   ✅ Enable full solution analysis
   ✅ Show live semantic errors
   ✅ Enable background analysis
   ```

2. **Configurar Code Clone Analysis:**
   ```
   Analyze → Analyze Solution for Code Clones
   ```
   - VS Professional detecta automáticamente métodos similares
   - Muestra porcentaje de similitud
   - Sugiere oportunidades de refactoring

3. **Instalar Productivity Power Tools (Opcional):**
   - Mejora detección de patrones repetitivos
   - Sugerencias automáticas para delegates

### Paso 2: Habilitar Quick Actions para Delegates
**Tiempo: 2 minutos**

1. **Configurar sugerencias inteligentes:**
   ```
   Tools → Options → Text Editor → C# → Code Style → Formatting
   ✅ Format document on type
   ✅ Format document on paste
   ```

2. **Habilitar refactoring suggestions:**
   ```
   Tools → Options → Text Editor → C# → Advanced
   ✅ Enable editor color scheme
   ✅ Show severity in editor
   ```

## 🔍 Detección Automática de Duplicación

### Paso 1: Análisis de Code Clones con IDE
**Tiempo: 5 minutos**

1. **Abrir archivo:** `02-Delegates-Before.cs`

2. **Ejecutar Code Clone Analysis:**
   ```
   Analyze → Analyze Solution for Code Clones
   ```

3. **Interpretar resultados automáticos:**
   - **Clone Group 1:** Métodos `ProcessNumbersDouble`, `ProcessNumbersSquare`, `ProcessNumbersAddTen`, `ProcessNumbersNegate`
   - **Similarity:** 85-90% (ALTO - Candidato perfecto para Func<>)
   - **Clone Length:** 15-20 líneas por clon
   - **Recommendation:** Extract common pattern with delegate parameter

4. **Ver en Code Clone Results Window:**
   ```
   View → Other Windows → Code Clone Analysis Results
   ```
   - Doble-click en cada clon para comparar visualmente
   - VS resalta automáticamente las diferencias

### Paso 2: Quick Actions para Delegates
**Tiempo: 10 minutos**

#### 2.1: Detectar Patrón Repetitivo Automáticamente

1. **Posicionar cursor** en método `ProcessNumbersDouble`

2. **Usar Quick Actions inteligentes:**
   ```
   Ctrl+. (punto) → Ver sugerencias
   ```

3. **VS sugiere automáticamente:**
   - "Extract method" (básico)
   - "Replace with parameterized method" (avanzado - si detecta el patrón)
   - "Convert to generic method"

#### 2.2: Refactoring Semi-Automático con IDE

1. **Seleccionar método completo** `ProcessNumbersDouble`:
   ```csharp
   public List<int> ProcessNumbersDouble(List<int> numbers)
   {
       var result = new List<int>();
       Console.WriteLine("Starting to process numbers for doubling...");
       foreach (var number in numbers)
       {
           var processedValue = number * 2; // ← Solo esta línea cambia
           result.Add(processedValue);
           Console.WriteLine($"Processed {number} -> {processedValue}");
       }
       Console.WriteLine($"Completed processing. Total items: {result.Count}");
       return result;
   }
   ```

2. **Extract Method con parámetro funcional:**
   ```
   Ctrl+R, Ctrl+M → Extract Method
   ```

3. **En el diálogo, VS sugiere:**
   - **Method name:** `ProcessNumbers`
   - **Parameters:** `List<int> numbers, string operationName`
   - **Return type:** `List<int>`

4. **Modificar manualmente para agregar Func:**
   ```csharp
   // El IDE genera esto:
   private List<int> ProcessNumbers(List<int> numbers, string operationName)
   {
       // ... código común
   }
   
   // Modificamos para agregar el delegate:
   private List<int> ProcessNumbers(List<int> numbers, Func<int, int> transformation, string operationName)
   {
       // ... código común
       var processedValue = transformation(number); // ← Usar el delegate
   }
   ```

#### 2.3: Usar Convert to Lambda Expression

1. **Refactorizar métodos específicos:**
   ```csharp
   // Método original
   public List<int> ProcessNumbersDouble(List<int> numbers)
   {
       return ProcessNumbers(numbers, DoubleNumber, "doubling");
   }
   
   private int DoubleNumber(int x)
   {
       return x * 2;
   }
   ```

2. **Quick Action automática:**
   - Posicionar cursor en `DoubleNumber`
   - Ctrl+. → "Inline method"
   - VS convierte automáticamente a lambda

3. **Resultado automático:**
   ```csharp
   public List<int> ProcessNumbersDouble(List<int> numbers)
   {
       return ProcessNumbers(numbers, x => x * 2, "doubling");
   }
   // Método DoubleNumber se elimina automáticamente
   ```

## 🚀 Técnicas Avanzadas del IDE

### Usar Symbol Search para Patrones

1. **Buscar patrones similares:**
   ```
   Ctrl+, (comma) → Buscar "ProcessNumbers"
   ```

2. **Go to All (Ctrl+T):**
   - Buscar métodos con patrones similares
   - Ver todas las implementaciones
   - Identificar candidatos para delegates

### Generate Delegate Automáticamente

1. **Crear signature de método:**
   ```csharp
   public void ProcessWithCallback(List<int> data, /* cursor aquí */)
   ```

2. **IntelliSense sugiere automáticamente:**
   - `Action<int>` para callbacks sin retorno
   - `Func<int, bool>` para predicados
   - `Func<int, int>` para transformaciones

3. **Usar code snippets:**
   - Escribir `action` + Tab + Tab
   - Escribir `func` + Tab + Tab
   - VS genera templates automáticamente

## 📊 Medición de Mejoras con IDE

### Antes vs Después con Code Metrics

1. **Métricas antes del refactoring:**
   ```
   Analyze → Calculate Code Metrics → For Current Document
   ```
   
   **Resultados típicos:**
   - **Methods count:** 4 métodos similares
   - **Lines of Code:** 120 líneas totales
   - **Duplication:** 85% código repetido
   - **Maintainability Index:** ~25

2. **Métricas después del refactoring:**
   - **Methods count:** 1 genérico + 4 wrappers de 1 línea
   - **Lines of Code:** 35 líneas totales
   - **Duplication:** <5% código repetido
   - **Maintainability Index:** >70

### Usar Code Clone Analysis para Validar

1. **Ejecutar nuevamente después del refactoring:**
   ```
   Analyze → Analyze Solution for Code Clones
   ```

2. **Resultado esperado:**
   - ❌ No more clone groups detected
   - ✅ Duplication eliminated message
   - ✅ Zero similarity warnings

## 🎯 Ejercicio Práctico IDE-Driven

### Challenge 1: Validation Processor

**Objetivo:** Refactorizar `ValidationProcessorProblematic` usando herramientas automáticas.

**Pasos con IDE:**

1. **Analizar duplicación:**
   ```
   Analyze → Analyze Solution for Code Clones
   ```

2. **Identificar patrón común:**
   - VS detecta 90% similitud entre métodos de validación
   - Solo cambia la condición de validación

3. **Extract Method automático:**
   ```
   Seleccionar ValidateEmails → Ctrl+R, Ctrl+M
   Method name: ValidateItems<T>
   ```

4. **Quick Action para generic:**
   ```
   Cursor en ValidateItems → Ctrl+.
   "Make method generic" → Seleccionar
   ```

5. **Convert to Predicate:**
   ```csharp
   // VS sugiere cambiar parámetro de:
   bool IsValidEmail(string email)
   // A:
   Predicate<string> emailValidator
   ```

### Challenge 2: Report Generator

**Pasos automáticos:**

1. **Multi-cursor editing:**
   ```
   Alt+Click en cada método GenerateXXXReport
   Edit all simultaneously
   ```

2. **Extract common structure:**
   ```
   Select common parts → Ctrl+R, Ctrl+M
   Add Func<T, string> formatter parameter
   ```

3. **Inline specific formatters:**
   ```
   Select formatting logic → Ctrl+. → "Convert to lambda"
   ```

## 🛠️ Pro Tips para Delegates en VS

### 1. IntelliSense para Delegates
```csharp
// Escribir esto:
list.Where(

// IntelliSense sugiere automáticamente:
list.Where(x => x.)
list.Where(item => item.)
```

### 2. Generate from Usage
```csharp
// Escribir código que usa delegate inexistente:
ProcessData(items, TransformItem, "operation");

// Ctrl+. sobre TransformItem → "Generate method"
// VS crea automáticamente la signature correcta
```

### 3. Convert Between Delegate Types
```csharp
// Método existente:
Func<int, bool> predicate = x => x > 0;

// Quick Action (Ctrl+.):
// "Convert to Predicate<int>"
// "Convert to Expression<Func<int, bool>>"
```

### 4. Batch Rename para Consistencia
```csharp
// Seleccionar todos los métodos ProcessXXX
// F2 → Rename all to ProcessNumbers
// VS actualiza todas las referencias automáticamente
```

## 📋 Checklist IDE-Optimizado

### Configuración:
- [ ] Code Clone Analysis configurado
- [ ] Quick Actions habilitadas
- [ ] IntelliSense para delegates activo
- [ ] Code snippets para Action/Func disponibles

### Durante Refactoring:
- [ ] Usar Code Clone Analysis para encontrar duplicación
- [ ] Extract Method automático (Ctrl+R, Ctrl+M)
- [ ] Convert to lambda con Quick Actions (Ctrl+.)
- [ ] Generate from Usage para nuevos delegates
- [ ] Multi-cursor editing para cambios simultáneos

### Validación:
- [ ] Re-run Code Clone Analysis (cero duplicación)
- [ ] Code Metrics mejoradas
- [ ] IntelliSense funciona con nuevos delegates
- [ ] No warnings de código duplicado

## 🏆 Resultados Esperados

### Métricas de Éxito:
| Aspecto | Antes | Después | Mejora |
|---------|-------|---------|--------|
| Líneas de código | 400+ | <100 | 75% ↓ |
| Métodos duplicados | 12 | 3 | 80% ↓ |
| Code clones | 6 grupos | 0 | 100% ↓ |
| Maintainability | 25 | 70+ | 180% ↑ |

### Beneficios del IDE:
✅ **Detección automática** de patrones duplicados
✅ **Refactoring seguro** con preview de cambios
✅ **IntelliSense inteligente** para delegates
✅ **Validation en tiempo real** de mejoras
✅ **Métricas cuantificables** del progreso

---

**Resultado:** Dominio completo de delegates usando Visual Studio Professional como herramienta principal, con reducción dramática de duplicación de código medida automáticamente.