# Guía Visual Studio Professional: Extract Method con IDE

## 🎯 Objetivo
Dominar el refactoring **Extract Method** usando las herramientas automáticas y semi-automáticas de **Visual Studio Professional**.

## 🛠️ Configuración Inicial del IDE

### Paso 1: Habilitar Herramientas de Análisis
**Tiempo: 3 minutos**

1. **Habilitar Code Metrics:**
   - `Tools` → `Options` → `Text Editor` → `C#` → `Advanced`
   - ✅ Marcar "Enable full solution analysis"
   - ✅ Marcar "Show live code analysis"

2. **Instalar SonarLint (Recomendado):**
   - `Extensions` → `Manage Extensions` → Buscar "SonarLint"
   - Instalar para análisis automático de complejidad

3. **Configurar Code Analysis:**
   - Click derecho en proyecto → `Properties` → `Code Analysis`
   - ✅ Habilitar "Enable code analysis on build"
   - Configurar ruleset: "Microsoft Managed Recommended Rules"

### Paso 2: Verificar Métricas Automáticas
**Tiempo: 2 minutos**

1. **Calcular Code Metrics:**
   ```
   Analyze → Calculate Code Metrics → For Solution
   ```
   
2. **Interpretar resultados:**
   - **Cyclomatic Complexity:** >10 = Refactoring necesario
   - **Lines of Code:** >50 por método = Candidato a Extract Method
   - **Maintainability Index:** <20 = Código problemático

## 🎯 Ejercicio Práctico con IDE

### Paso 1: Analizar Método Problemático con IDE
**Tiempo: 5 minutos**

1. **Abrir archivo:** `01-ExtractMethod-Before.cs`

2. **Usar Code Metrics automáticas:**
   ```
   Analyze → Calculate Code Metrics → For Current Document
   ```

3. **Verificar en Code Metrics Results:**
   - Buscar método `ProcessCustomerOrder`
   - ✅ Cyclomatic Complexity: **12** (ALTO - Objetivo: <5)
   - ✅ Lines of Code: **65** (ALTO - Objetivo: <20)
   - ✅ Maintainability Index: **~15** (BAJO - Objetivo: >50)

4. **Usar SonarLint (si instalado):**
   - Observar **squiggly lines** amarillas/rojas
   - Ver **Warning List** para issues de complejidad
   - Mensajes típicos: "Cognitive Complexity is too high", "Method too long"

### Paso 2: Extract Method Automático con VS
**Tiempo: 15 minutos**

#### 2.1: Extraer Validación usando IDE

1. **Seleccionar código de validación:**
   ```csharp
   // Seleccionar desde línea 35 hasta 65 (todo el bloque de validación)
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
   // ... resto de validaciones
   ```

2. **Usar Extract Method automático:**
   ```
   Método 1: Ctrl+R, Ctrl+M
   Método 2: Click derecho → Quick Actions → Extract Method
   Método 3: Ctrl+. → Extract Method
   ```

3. **Configurar en el diálogo:**
   - **Method name:** `IsValidOrder`
   - **Return type:** VS detecta automáticamente `bool`
   - **Parameters:** VS detecta automáticamente `CustomerOrder order`
   - ✅ Click "OK"

4. **Resultado automático del IDE:**
   ```csharp
   // VS genera automáticamente:
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
       // ... resto de validaciones
       
       return true;
   }
   
   // Y reemplaza en el método original:
   public bool ProcessCustomerOrder(CustomerOrder order)
   {
       Console.WriteLine($"Starting to process order {order?.Id}");
       
       if (!IsValidOrder(order))
           return false;
           
       // ... resto del código
   }
   ```

#### 2.2: Verificar Mejora con Code Metrics

1. **Recalcular métricas:**
   ```
   Analyze → Calculate Code Metrics → For Current Document
   ```

2. **Comparar resultados:**
   - `ProcessCustomerOrder`: Complexity **8** (antes: 12) ✅
   - `IsValidOrder`: Complexity **4** (nuevo método) ✅
   - Lines of Code reducidas significativamente ✅

#### 2.3: Extraer Cálculos usando Quick Actions

1. **Posicionar cursor** en el bloque de cálculos de totales

2. **Usar Quick Actions inteligentes:**
   ```
   Ctrl+. (punto) → Ver sugerencias automáticas
   ```

3. **VS sugiere automáticamente:**
   - "Extract method"
   - "Extract local function" 
   - "Introduce variable for expression"

4. **Seleccionar "Extract method"** y nombrar: `CalculateOrderTotals`

### Paso 3: Usar IntelliSense para Nombres Descriptivos
**Tiempo: 5 minutos**

1. **Renombrar métodos con F2:**
   - Seleccionar nombre del método
   - Presionar `F2` 
   - VS muestra todas las referencias automáticamente
   - Cambiar a nombre más descriptivo

2. **Usar sugerencias de IntelliSense:**
   - Al escribir nombres, VS sugiere patrones comunes
   - Acepta sugerencias con `Tab`

### Paso 4: Validar con Herramientas de Análisis
**Tiempo: 5 minutos**

1. **Error List automática:**
   ```
   View → Error List
   ```
   - Verificar que no hay nuevos warnings
   - Confirmar que warnings de complejidad desaparecieron

2. **Code Metrics finales:**
   ```
   Analyze → Calculate Code Metrics → For Current Document
   ```
   
   **Métricas objetivo alcanzadas:**
   - ✅ Todos los métodos: Cyclomatic Complexity < 5
   - ✅ Todos los métodos: Lines of Code < 20
   - ✅ Maintainability Index > 50

3. **SonarLint validation:**
   - Verificar que desaparecieron las alertas de complejidad
   - No debe haber squiggly lines relacionadas con complexity

## 🚀 Técnicas Avanzadas del IDE

### Usar Code Lens para Métricas en Tiempo Real

1. **Habilitar CodeLens:**
   ```
   Tools → Options → Text Editor → All Languages → CodeLens
   ✅ Enable CodeLens
   ✅ Show test status
   ✅ Show references
   ```

2. **Ver métricas sobre cada método:**
   - Aparecen automáticamente sobre declaraciones de métodos
   - Muestra: referencias, complejidad, coverage
   - Click para detalles expandidos

### Usar Live Code Analysis

1. **Análisis en tiempo real:**
   - VS analiza automáticamente mientras escribes
   - Underlines rojos/amarillos para issues inmediatos
   - Sugerencias en tiempo real para refactoring

2. **Light Bulb Actions (💡):**
   - Aparece automáticamente cerca de código problemático
   - Click para ver sugerencias de refactoring
   - Aplicar con un click

### Configurar Custom Rules

1. **Crear EditorConfig:**
   ```ini
   # .editorconfig
   root = true
   
   [*.cs]
   # Reglas de complejidad personalizada
   dotnet_analyzer_diagnostic.CA1502.severity = warning  # Avoid excessive complexity
   dotnet_code_quality.CA1502.threshold = 5             # Complexity threshold
   
   # Reglas de longitud de método
   dotnet_analyzer_diagnostic.CA1506.severity = warning  # Avoid excessive class coupling
   ```

2. **Configurar Ruleset personalizado:**
   - Crear archivo `.ruleset`
   - Configurar reglas específicas para complejidad
   - Aplicar a proyecto

## 📊 Dashboard de Métricas

### Crear Reporte Automático

1. **Power BI integration (VS Enterprise):**
   ```
   Analyze → Code Metrics → Export to Excel
   ```

2. **Tracking de progreso:**
   - Guardar métricas antes del refactoring
   - Comparar métricas después
   - Crear dashboard de evolución

### Métricas Clave a Monitorear

| Métrica | Antes | Después | Objetivo |
|---------|-------|---------|----------|
| Cyclomatic Complexity | 12 | <5 | ✅ |
| Lines of Code | 65 | <20 | ✅ |
| Maintainability Index | 15 | >50 | ✅ |
| Method Count | 1 | 5-7 | ✅ |

## 🎯 Checklist IDE-Driven

### Antes de Empezar:
- [ ] Code Metrics habilitadas
- [ ] SonarLint instalado
- [ ] Error List visible
- [ ] CodeLens habilitado

### Durante Refactoring:
- [ ] Usar Extract Method automático (Ctrl+R, Ctrl+M)
- [ ] Verificar sugerencias de Quick Actions (Ctrl+.)
- [ ] Renombrar con F2 para consistencia
- [ ] Monitorear Error List en tiempo real

### Al Finalizar:
- [ ] Recalcular Code Metrics
- [ ] Verificar desaparición de warnings
- [ ] Confirmar métricas objetivo
- [ ] Validar con SonarLint

## 🏆 Ejercicio Completo IDE-Driven

### Challenge: Refactorizar con Solo Herramientas de IDE

**Objetivo:** Refactorizar `ProcessCustomerOrder` usando ÚNICAMENTE herramientas automáticas de Visual Studio.

**Reglas:**
- ❌ No editar código manualmente
- ✅ Solo usar Extract Method automático
- ✅ Solo usar Quick Actions
- ✅ Solo usar rename automático (F2)

**Pasos:**
1. Calcular métricas iniciales
2. Usar Extract Method 5-6 veces
3. Renombrar métodos con F2
4. Verificar métricas finales
5. Validar con SonarLint

**Tiempo límite:** 20 minutos

### Métricas de Éxito:
- ✅ Complexity reduction: >60%
- ✅ Method count: 5-7 métodos
- ✅ Zero complexity warnings
- ✅ Maintainability Index: >50

---

## 💡 Pro Tips para VS Professional

1. **Batch Refactoring:** Seleccionar múltiples métodos y aplicar refactoring en lote
2. **Preview Changes:** Siempre usar "Preview Changes" antes de aplicar refactorings masivos
3. **Undo Safety:** Ctrl+Z funciona con refactorings automáticos
4. **Shortcuts personalizados:** Configurar atajos para refactorings frecuentes
5. **Team Settings:** Compartir configuraciones de análisis con el equipo

**Resultado:** Dominio completo de Extract Method usando Visual Studio Professional como herramienta principal.