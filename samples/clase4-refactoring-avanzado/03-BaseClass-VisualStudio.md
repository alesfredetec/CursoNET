# Guía Visual Studio Professional: Extract Base Class con IDE

## 🎯 Objetivo
Dominar **Extract Base Class** usando las herramientas automáticas de **Visual Studio Professional** para eliminar duplicación masiva entre clases.

## 🛠️ Configuración IDE para Herencia

### Paso 1: Habilitar Análisis de Jerarquías
**Tiempo: 3 minutos**

1. **Configurar Class View:**
   ```
   View → Class View (Ctrl+Shift+C)
   ```
   - Permite ver jerarquías automáticamente
   - Detecta clases con estructura similar

2. **Habilitar Object Browser:**
   ```
   View → Object Browser (Ctrl+Alt+J)
   ```
   - Navegar jerarquías de herencia
   - Ver miembros compartidos entre clases

3. **Configurar Solution Explorer para herencia:**
   ```
   Solution Explorer → Show All Files
   ```
   - Ver todas las clases del proyecto
   - Identificar patrones de nomenclatura similares

### Paso 2: Instalar Herramientas de Análisis
**Tiempo: 2 minutos**

1. **Habilitar Code Map (VS Professional):**
   ```
   Architecture → Generate Code Map → For Solution
   ```
   - Visualización automática de dependencias
   - Identificación de clases candidatas para jerarquía

2. **Configurar Dependency Validation:**
   ```
   Architecture → Validate Dependencies
   ```
   - Analiza relaciones entre clases
   - Sugiere oportunidades de refactoring

## 🔍 Detección Automática de Candidatos

### Paso 1: Análisis de Similitud con IDE
**Tiempo: 5 minutos**

1. **Abrir archivo:** `03-BaseClass-Before.cs`

2. **Usar Code Clone Analysis:**
   ```
   Analyze → Analyze Solution for Code Clones
   ```

3. **Resultados automáticos esperados:**
   - **Clone Group 1:** `FullTimeEmployee`, `PartTimeEmployee`, `Contractor`
   - **Similarity:** 80-85% (Propiedades y métodos comunes)
   - **Shared Members:** Id, Name, Email, UpdateContactInfo(), Deactivate()
   - **Recommendation:** Extract base class with common functionality

4. **Usar Class Diagram para visualización:**
   ```
   Project → Add → New Item → Class Diagram
   Drag classes: FullTimeEmployee, PartTimeEmployee, Contractor
   ```
   - VS muestra visualmente la similitud
   - Identifica automáticamente miembros duplicados

### Paso 2: Identificar Jerarquía Natural
**Tiempo: 5 minutos**

1. **Usar Object Browser:**
   ```
   Ctrl+Alt+J
   ```
   - Navegar a las clases problemáticas
   - Ver miembros lado a lado
   - Identificar patrones comunes

2. **Generate Class Diagram automáticamente:**
   ```
   Right-click en proyecto → View → View Class Diagram
   ```
   
   **VS muestra automáticamente:**
   - Clases sin relación de herencia
   - Miembros duplicados resaltados
   - Sugerencia de jerarquía potencial

## 🚀 Extract Base Class Semi-Automático

### Paso 1: Crear Base Class con Template
**Tiempo: 10 minutos**

1. **Usar Item Template:**
   ```
   Right-click en proyecto → Add → Class
   Template: "Abstract Class"
   Name: Employee.cs
   ```

2. **VS genera automáticamente:**
   ```csharp
   public abstract class Employee
   {
       // Empty template ready for customization
   }
   ```

3. **Usar Extract Interface para análisis:**
   ```
   Right-click en FullTimeEmployee → Refactor → Extract Interface
   ```
   - VS detecta automáticamente miembros públicos
   - Sugiere interface que será base para abstract class

### Paso 2: Move Members Automáticamente
**Tiempo: 15 minutos**

#### 2.1: Identificar Miembros Comunes con IDE

1. **Multi-document view:**
   ```
   Window → New Window
   ```
   - Abrir FullTimeEmployee en ventana 1
   - Abrir PartTimeEmployee en ventana 2
   - Comparar lado a lado automáticamente

2. **Usar Compare Files:**
   ```
   Tools → Compare Files
   Select: FullTimeEmployee.cs vs PartTimeEmployee.cs
   ```
   - VS resalta automáticamente similitudes
   - Identifica miembros duplicados exactos

#### 2.2: Move Members con Refactoring Tools

1. **Seleccionar propiedades comunes:**
   ```csharp
   // En FullTimeEmployee, seleccionar:
   public int Id { get; set; }
   public string Name { get; set; }
   public string Email { get; set; }
   public DateTime HireDate { get; set; }
   public string Department { get; set; }
   public bool IsActive { get; set; }
   ```

2. **Cut and Paste con IntelliSense:**
   - Ctrl+X para cortar miembros
   - Pegar en Employee base class
   - VS automáticamente ajusta accessibility

3. **Generate Constructor automático:**
   ```
   En Employee class → Ctrl+. → Generate constructor
   ```
   - VS detecta parámetros necesarios
   - Genera constructor protected automáticamente

#### 2.3: Refactor Inheritance Chain

1. **Add inheritance automáticamente:**
   ```csharp
   // En FullTimeEmployee
   public class FullTimeEmployee // ← cursor aquí
   ```

2. **Quick Action para herencia:**
   ```
   Ctrl+. → "Inherit from class" → Select Employee
   ```

3. **VS automáticamente:**
   - Agrega `: Employee`
   - Sugiere constructor base
   - Elimina miembros duplicados

4. **Fix Constructor automáticamente:**
   ```csharp
   // VS detecta que necesita constructor base
   public FullTimeEmployee(int id, string name, string email, string department, decimal annualSalary)
   // Ctrl+. → "Generate constructor call to base"
   ```

   **VS genera automáticamente:**
   ```csharp
   public FullTimeEmployee(int id, string name, string email, string department, decimal annualSalary)
       : base(id, name, email, department)
   {
       AnnualSalary = annualSalary;
   }
   ```

## 🎯 Técnicas Avanzadas del IDE

### Usar Hierarchy View

1. **Navigate to Base/Derived:**
   ```
   Right-click en Employee → Go To → Base Types
   Right-click en Employee → Go To → Derived Types
   ```

2. **Class Hierarchy Window:**
   ```
   View → Other Windows → Class Hierarchy
   ```
   - Visualización en tiempo real de jerarquía
   - Navegación rápida entre niveles

### Generate Abstract Members

1. **Detectar métodos que deben ser abstractos:**
   ```csharp
   // VS detecta que CalculatePay() difiere en cada clase
   public decimal CalculatePay() // ← cursor aquí
   ```

2. **Quick Action automática:**
   ```
   Ctrl+. → "Make method abstract"
   ```

3. **VS automáticamente:**
   - Convierte método a abstract
   - Marca clase como abstract si es necesario
   - Sugiere implementation en clases derivadas

### Implement Abstract Class

1. **En clases derivadas:**
   ```csharp
   public class FullTimeEmployee : Employee // ← squiggly line
   ```

2. **Quick Action:**
   ```
   Ctrl+. → "Implement abstract class"
   ```

3. **VS genera automáticamente:**
   ```csharp
   public override decimal CalculatePay()
   {
       throw new NotImplementedException();
   }
   ```

4. **Replace implementation:**
   - Reemplazar `throw new NotImplementedException()`
   - Con lógica específica de cada clase

## 📊 Validation con IDE Tools

### Usar Architecture Tools

1. **Generate Dependency Graph:**
   ```
   Architecture → Generate Code Map → For Solution
   ```
   - Visualizar nueva jerarquía automáticamente
   - Verificar que dependencies son correctas

2. **Validate Architecture:**
   ```
   Architecture → Validate Dependencies
   ```
   - Confirmar que herencia es consistente
   - No circular dependencies

### Code Metrics para Jerarquía

1. **Analyze inheritance hierarchy:**
   ```
   Analyze → Calculate Code Metrics → For Current Project
   ```

2. **Métricas específicas de herencia:**
   - **Depth of Inheritance:** Should be 2-3 levels max
   - **Class Coupling:** Reduced significantly
   - **Lines of Code:** Dramatic reduction in derived classes

## 🎯 Ejercicio Completo IDE-Driven

### Challenge: Vehicle Hierarchy

**Objetivo:** Crear jerarquía `Vehicle → Car/Motorcycle` usando solo herramientas de IDE.

**Pasos con herramientas automáticas:**

1. **Análisis automático:**
   ```
   Analyze → Code Clones → Identify Car vs Motorcycle similarities
   ```

2. **Generate Class Diagram:**
   ```
   Add → Class Diagram → Drag Car and Motorcycle classes
   ```

3. **Extract Interface preview:**
   ```
   Right-click Car → Extract Interface → Preview common members
   ```

4. **Create base class:**
   ```
   Add → Abstract Class → Vehicle
   ```

5. **Move members automáticamente:**
   - Multi-select common properties
   - Cut/Paste to base class
   - VS auto-adjusts accessibility

6. **Establish inheritance:**
   ```
   Ctrl+. on Car → Inherit from Vehicle
   Fix constructors with Quick Actions
   ```

7. **Validate with tools:**
   ```
   Architecture → Code Map → Verify hierarchy
   Calculate Code Metrics → Confirm improvement
   ```

## 🏆 Polymorphic Manager con IDE

### Refactor Manager Classes

1. **Identify manager duplication:**
   ```
   Code Clone Analysis → Detect manager method patterns
   ```

2. **Generate generic methods:**
   ```csharp
   // Original duplicated methods detected by IDE
   public void AddFullTimeEmployee(FullTimeEmployee emp) { }
   public void AddPartTimeEmployee(PartTimeEmployee emp) { }
   public void AddContractor(Contractor emp) { }
   
   // Quick Action suggestion:
   // "Combine into generic method"
   ```

3. **Use Find All References:**
   ```
   F12 on each AddXXXEmployee method
   Replace all with single AddEmployee(Employee emp)
   ```

4. **Polymorphic operations:**
   ```csharp
   // VS IntelliSense automatically suggests polymorphic usage:
   employees.ForEach(emp => emp.CalculatePay()); // Available after hierarchy
   ```

## 📋 Checklist IDE-Optimizado

### Pre-Refactoring:
- [ ] Code Clone Analysis ejecutado
- [ ] Class Diagram generado
- [ ] Object Browser configurado
- [ ] Compare Files tool disponible

### Durante Refactoring:
- [ ] Extract Interface para análisis
- [ ] Generate Abstract Class template
- [ ] Move Members con Cut/Paste inteligente
- [ ] Quick Actions para inheritance
- [ ] Generate Constructor calls automáticos
- [ ] Implement Abstract Class automático

### Post-Refactoring:
- [ ] Code Map para visualizar jerarquía
- [ ] Dependency Validation passed
- [ ] Code Metrics recalculadas
- [ ] No inheritance warnings en Error List

## 🎯 Métricas de Éxito IDE

### Dashboard Automático:

| Métrica | Antes | Después | Herramienta |
|---------|-------|---------|-------------|
| Classes | 3 independent | 1 base + 3 derived | Class View |
| Duplicated LOC | 300+ | <50 | Code Clone Analysis |
| Coupling | High | Low | Code Metrics |
| Polymorphic methods | 0 | 5+ | Class Hierarchy |

### Visual Validation:
✅ **Class Diagram** shows clean hierarchy
✅ **Code Map** shows proper dependencies  
✅ **Object Browser** shows inheritance chain
✅ **IntelliSense** works polymorphically

---

## 💡 Pro Tips para Herencia en VS

1. **Class Designer shortcuts:**
   - Drag between classes to create inheritance
   - Right-click relationships to modify
   - Auto-layout for clean diagrams

2. **Navigation shortcuts:**
   - **F12:** Go to base member definition
   - **Shift+F12:** Find all derived implementations
   - **Ctrl+F12:** Go to declaration vs implementation

3. **Refactoring safety:**
   - Always use "Preview Changes" for inheritance changes
   - Test polymorphic calls immediately
   - Use "Find All References" before moving members

4. **Team collaboration:**
   - Share Class Diagrams with team
   - Export Code Maps for documentation
   - Use Architecture validation in CI/CD

**Resultado:** Jerarquía limpia y funcional creada eficientemente usando Visual Studio Professional como herramienta principal, con validación automática de la arquitectura resultante.