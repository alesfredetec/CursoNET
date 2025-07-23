# Guía de Mentoring: Extract Base Class - Paso a Paso

## 🎯 Objetivo del Ejercicio
Aprender a **extraer clases base** para eliminar duplicación masiva entre clases similares y habilitar el polimorfismo.

## 🧠 ¿Qué es Extract Base Class?

**Extract Base Class** es una técnica de refactoring que consiste en identificar código común entre clases similares y crear una clase padre que contenga esa funcionalidad compartida.

### Analogía del Mundo Real:
Imagina que tienes varios **tipos de empleados** (tiempo completo, medio tiempo, contratistas):
- Todos tienen **características comunes**: nombre, email, departamento
- Todos realizan **acciones comunes**: actualizar contacto, desactivarse
- Cada uno tiene **especialidades únicas**: cálculo de pago diferente

La clase base sería como un **"molde común"** que define qué características y comportamientos comparten todos los empleados, mientras que cada tipo específico añade sus propias particularidades.

## 🔍 Identificando Cuándo Usar Extract Base Class

### Señales de Alerta (Code Smells):
1. **Propiedades idénticas** en múltiples clases
2. **Métodos duplicados** con implementación igual
3. **Constructores similares** con validaciones repetidas
4. **Imposibilidad de tratar objetos polimórficamente** (necesitas múltiples listas/métodos)
5. **Jerarquías conceptuales naturales** (Empleado → FullTime/PartTime, Vehículo → Auto/Moto)

### Ejemplo de Identificación:
```csharp
// ❌ ANTES: Clases con duplicación masiva
public class FullTimeEmployee
{
    // Propiedades comunes
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    // ... más propiedades idénticas
    
    // Constructor con validación duplicada
    public FullTimeEmployee(int id, string name, string email)
    {
        // Validación que se repite en todas las clases
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name is required");
        // ... más validaciones idénticas
    }
    
    // Métodos duplicados
    public void UpdateContactInfo(string email) { /* implementación idéntica */ }
    public void Deactivate() { /* implementación idéntica */ }
}

public class PartTimeEmployee
{
    // ❌ MISMAS propiedades que FullTimeEmployee
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    // ... propiedades idénticas
    
    // ❌ MISMO constructor con validación duplicada
    // ❌ MISMOS métodos con implementación idéntica
}

// 🤔 PREGUNTA: ¿Qué tienen en común estas clases?
// ✅ RESPUESTA: 80-90% del código es idéntico - candidato perfecto para clase base
```

## 📋 Proceso Paso a Paso

### Paso 1: Análisis de Similitudes
**Tiempo estimado: 15 minutos**

1. **Listar todas las clases similares**
2. **Identificar propiedades comunes** (nombre idéntico y tipo)
3. **Identificar métodos comunes** (misma signatura e implementación)
4. **Identificar constructores similares** (misma lógica de validación)
5. **Determinar la jerarquía conceptual**

**Ejercicio práctico:**
```csharp
// Crear tabla de análisis:
// | Característica       | FullTime | PartTime | Contractor |
// |---------------------|----------|----------|------------|
// | Id                  | ✅       | ✅       | ✅         |
// | Name                | ✅       | ✅       | ✅         |
// | Email               | ✅       | ✅       | ✅         |
// | UpdateContactInfo() | ✅       | ✅       | ✅         |
// | Deactivate()        | ✅       | ✅       | ✅         |

// ✅ Conclusión: Mucha funcionalidad común → Crear clase base Employee
```

### Paso 2: Diseñar la Jerarquía
**Tiempo estimado: 10 minutos**

**Principios de diseño:**
- **Clase base**: Contiene funcionalidad común
- **Clases derivadas**: Solo características específicas
- **Métodos abstractos**: Para comportamiento que debe variar
- **Métodos virtuales**: Para comportamiento que puede variar

**Proceso de diseño:**
```csharp
// PASO 2.1: Identificar qué va en la base
public abstract class Employee  // ← abstract porque no instanciaremos directamente
{
    // ✅ Propiedades comunes
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    // ... más propiedades comunes
    
    // ✅ Constructor común
    protected Employee(int id, string name, string email) // ← protected para subclases
    {
        // Validación común
    }
    
    // ✅ Métodos comunes
    public void UpdateContactInfo(string email) { /* implementación común */ }
    public void Deactivate() { /* implementación común */ }
    
    // ✅ Método que DEBE variar en cada subtipo
    public abstract decimal CalculatePay();
    
    // ✅ Método que PUEDE variar (tiene implementación por defecto)
    public virtual string GetEmployeeType() 
    { 
        return "Employee"; 
    }
}
```

### Paso 3: Crear la Clase Base
**Tiempo estimado: 15 minutos**

**Checklist para la clase base:**
- [ ] ¿Es `abstract` si no debe instanciarse directamente?
- [ ] ¿Constructor es `protected` para uso de subclases?
- [ ] ¿Contiene todas las propiedades comunes?
- [ ] ¿Contiene todos los métodos comunes?
- [ ] ¿Métodos abstractos para comportamiento que varía?
- [ ] ¿Métodos virtuales para comportamiento personalizable?

**Implementación paso a paso:**
```csharp
// PASO 3.1: Declarar la clase base
public abstract class Employee
{
    // PASO 3.2: Mover propiedades comunes
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime HireDate { get; set; }
    public string Department { get; set; }
    public bool IsActive { get; set; }
    
    // PASO 3.3: Crear constructor base
    protected Employee(int id, string name, string email, string department)
    {
        // Inicialización común
        Id = id;
        Name = name;
        Email = email;
        Department = department;
        HireDate = DateTime.Now;
        IsActive = true;
        
        // PASO 3.4: Mover validación común
        ValidateBasicInfo(name, email, department);
    }
    
    // PASO 3.5: Método de validación privado
    private void ValidateBasicInfo(string name, string email, string department)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name is required");
        if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            throw new ArgumentException("Valid email is required");
        if (string.IsNullOrEmpty(department))
            throw new ArgumentException("Department is required");
    }
    
    // PASO 3.6: Mover métodos comunes
    public void UpdateContactInfo(string newEmail)
    {
        // Implementación común movida aquí
        if (string.IsNullOrEmpty(newEmail) || !newEmail.Contains("@"))
            throw new ArgumentException("Valid email is required");
            
        Email = newEmail;
        Console.WriteLine($"Contact info updated for {Name}");
    }
    
    public void Deactivate()
    {
        IsActive = false;
        Console.WriteLine($"Employee {Name} has been deactivated");
    }
    
    // PASO 3.7: Definir métodos abstractos
    public abstract decimal CalculatePay(); // ← Cada tipo debe implementar
    
    // PASO 3.8: Definir métodos virtuales
    public virtual string GetEmployeeInfo() // ← Puede ser sobrescrito
    {
        var status = IsActive ? "Active" : "Inactive";
        return $"ID: {Id}, Name: {Name}, Department: {Department}, Status: {status}";
    }
}
```

### Paso 4: Refactorizar Clases Derivadas
**Tiempo estimado: 20 minutos**

**Proceso de refactoring por clase:**
```csharp
// PASO 4.1: Cambiar declaración de clase
public class FullTimeEmployee : Employee  // ← Agregar herencia
{
    // PASO 4.2: Eliminar propiedades duplicadas
    // ❌ ELIMINAR: public int Id { get; set; }
    // ❌ ELIMINAR: public string Name { get; set; }
    // ... eliminar todas las propiedades que ahora están en la base
    
    // ✅ MANTENER: Solo propiedades específicas
    public decimal AnnualSalary { get; set; }
    public int VacationDays { get; set; }
    public bool HasHealthInsurance { get; set; }
    
    // PASO 4.3: Modificar constructor para usar base
    public FullTimeEmployee(int id, string name, string email, string department, decimal annualSalary)
        : base(id, name, email, department)  // ← Llamar constructor base
    {
        // Solo inicialización específica
        AnnualSalary = annualSalary;
        VacationDays = 20;
        HasHealthInsurance = true;
    }
    
    // PASO 4.4: Eliminar métodos duplicados
    // ❌ ELIMINAR: public void UpdateContactInfo(string email)
    // ❌ ELIMINAR: public void Deactivate()
    // ... estos ahora están en la clase base
    
    // PASO 4.5: Implementar métodos abstractos
    public override decimal CalculatePay()
    {
        return AnnualSalary / 12; // Implementación específica
    }
    
    // PASO 4.6: Override métodos virtuales si es necesario
    public override string GetEmployeeInfo()
    {
        var baseInfo = base.GetEmployeeInfo(); // ← Reutilizar base
        return $"{baseInfo}, Type: Full-Time, Salary: ${AnnualSalary:F2}";
    }
    
    // ✅ MANTENER: Solo métodos específicos únicos
    public void RequestVacation(int days)
    {
        // Lógica específica de empleados tiempo completo
    }
}
```

### Paso 5: Habilitar Polimorfismo
**Tiempo estimado: 15 minutos**

**Refactorizar managers para usar polimorfismo:**
```csharp
// PASO 5.1: Cambiar de múltiples listas a una sola
public class EmployeeManager
{
    // ❌ ANTES: Múltiples listas
    // private List<FullTimeEmployee> fullTimeEmployees;
    // private List<PartTimeEmployee> partTimeEmployees;  
    // private List<Contractor> contractors;
    
    // ✅ DESPUÉS: Una sola lista polimórfica
    private List<Employee> employees = new List<Employee>();
    
    // PASO 5.2: Unificar métodos duplicados
    // ❌ ANTES: Múltiples métodos
    // public void AddFullTimeEmployee(FullTimeEmployee emp)
    // public void AddPartTimeEmployee(PartTimeEmployee emp)
    // public void AddContractor(Contractor contractor)
    
    // ✅ DESPUÉS: Un solo método polimórfico
    public void AddEmployee(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));
            
        employees.Add(employee);
        Console.WriteLine($"Added employee: {employee.Name}");
    }
    
    // PASO 5.3: Métodos polimórficos que funcionan con cualquier tipo
    public Employee FindEmployeeById(int id)
    {
        return employees.FirstOrDefault(emp => emp.Id == id && emp.IsActive);
    }
    
    public decimal CalculateTotalPayroll()
    {
        return employees.Where(emp => emp.IsActive).Sum(emp => emp.CalculatePay());
    }
    
    // ✅ El polimorfismo permite que estos métodos funcionen con cualquier subtipo
}
```

## 🎨 Patrones de Herencia

### Patrón 1: Método Abstracto (Implementación Obligatoria)
```csharp
public abstract class Shape
{
    // ✅ Método que DEBE ser implementado por cada forma
    public abstract double CalculateArea();
    
    // ✅ Uso polimórfico
    public void PrintArea()
    {
        Console.WriteLine($"Area: {CalculateArea()}"); // Polimorfismo en acción
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }
    
    // ✅ OBLIGATORIO: Debe implementar el método abstracto
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}
```

### Patrón 2: Método Virtual (Personalización Opcional)
```csharp
public class Animal
{
    // ✅ Método con implementación por defecto que puede ser sobrescrito
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes a sound");
    }
}

public class Dog : Animal
{
    // ✅ OPCIONAL: Puede sobrescribir para personalizar
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }
}

public class Cat : Animal
{
    // ✅ También puede mantener comportamiento por defecto (no override)
    // En este caso, usará "Animal makes a sound"
}
```

### Patrón 3: Constructor Base con Validación
```csharp
public abstract class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    // ✅ Constructor protegido para uso de subclases
    protected Person(string name, int age)
    {
        ValidateInput(name, age);
        Name = name;
        Age = age;
    }
    
    // ✅ Validación centralizada
    private void ValidateInput(string name, int age)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name is required");
        if (age < 0 || age > 150)
            throw new ArgumentException("Invalid age");
    }
}

public class Student : Person
{
    public string School { get; set; }
    
    // ✅ Constructor que reutiliza validación de la base
    public Student(string name, int age, string school) 
        : base(name, age)  // ← Reutilizar validación
    {
        School = school;
    }
}
```

## 🧪 Estrategias de Testing

### Testing de Clase Base:
```csharp
[Test]
public void Employee_Constructor_ValidatesInput()
{
    // Arrange & Act & Assert
    Assert.Throws<ArgumentException>(() => 
        new FullTimeEmployee(1, "", "test@test.com", "IT", 50000));
    Assert.Throws<ArgumentException>(() => 
        new FullTimeEmployee(1, "John", "invalid-email", "IT", 50000));
}

[Test]
public void Employee_UpdateContactInfo_UpdatesEmail()
{
    // Arrange
    var employee = new FullTimeEmployee(1, "John", "john@test.com", "IT", 50000);
    
    // Act
    employee.UpdateContactInfo("john.doe@test.com");
    
    // Assert
    Assert.AreEqual("john.doe@test.com", employee.Email);
}
```

### Testing Polimórfico:
```csharp
[Test]
public void CalculateTotalPayroll_WithMixedEmployeeTypes_ReturnsCorrectTotal()
{
    // Arrange
    var manager = new EmployeeManager();
    manager.AddEmployee(new FullTimeEmployee(1, "Alice", "alice@test.com", "IT", 60000));
    manager.AddEmployee(new PartTimeEmployee(2, "Bob", "bob@test.com", "IT", 25));
    
    // Act - polimorfismo en acción
    var total = manager.CalculateTotalPayroll();
    
    // Assert - verifica que cada tipo calcula correctamente
    var expected = (60000 / 12) + (25 * 20 * 4); // Full-time + Part-time
    Assert.AreEqual(expected, total);
}
```

## 🚀 Casos Avanzados

### Caso 1: Herencia con Interfaces
```csharp
public interface IPayable
{
    decimal CalculatePay();
}

public interface INotifiable
{
    void SendNotification(string message);
}

// ✅ Combinar herencia con interfaces
public abstract class Employee : IPayable, INotifiable
{
    // Funcionalidad común
    public abstract decimal CalculatePay(); // De IPayable
    public virtual void SendNotification(string message) // De INotifiable
    {
        Console.WriteLine($"Notification to {Name}: {message}");
    }
}
```

### Caso 2: Jerarquías de Múltiples Niveles
```csharp
public abstract class Employee { /* funcionalidad común */ }

public abstract class RegularEmployee : Employee 
{ 
    // Funcionalidad común de empleados regulares (no contratistas)
    public int VacationDays { get; set; }
}

public class FullTimeEmployee : RegularEmployee 
{ 
    // Específico de tiempo completo
}

public class PartTimeEmployee : RegularEmployee 
{ 
    // Específico de medio tiempo  
}

public class Contractor : Employee 
{ 
    // Los contratistas no heredan de RegularEmployee
}
```

## ❌ Errores Comunes y Cómo Evitarlos

### Error 1: Herencia Demasiado Profunda
```csharp
// ❌ MAL - Jerarquía demasiado profunda
public class Entity : BaseObject : SuperBase : MegaBase : UltraBase

// ✅ BIEN - Máximo 3-4 niveles
public class Employee : Person
public class FullTimeEmployee : Employee
```

### Error 2: Clase Base No Cohesiva
```csharp
// ❌ MAL - Clase base que mezcla conceptos no relacionados
public class BaseEntity
{
    public int Id { get; set; }           // OK - común
    public string Name { get; set; }      // OK - común
    public void SaveToDatabase() { }      // ❌ - no todos necesitan esto
    public void SendEmail() { }           // ❌ - no todos necesitan esto
}

// ✅ BIEN - Clase base cohesiva
public abstract class Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Solo funcionalidad que realmente comparten TODOS
}
```

### Error 3: No Usar Métodos Abstractos Cuando Se Necesitan
```csharp
// ❌ MAL - Método virtual sin implementación útil
public virtual decimal CalculateSalary()
{
    throw new NotImplementedException(); // Malo
}

// ✅ BIEN - Método abstracto que obliga implementación
public abstract decimal CalculateSalary();
```

## 🎯 Métricas de Éxito

### Antes del Refactoring:
- **3 clases de empleados** con 80% código duplicado
- **300 líneas** de código repetitivo
- **9 métodos duplicados** en managers
- **Imposible** tratar empleados polimórficamente

### Después del Refactoring:
- **1 clase base** + 3 especializaciones
- **80 líneas** de código común
- **3 métodos polimórficos** en managers
- **Polimorfismo completo** habilitado

### Objetivos Jr:
- ✅ Eliminar 70%+ de duplicación entre clases
- ✅ Crear jerarquía clara y lógica
- ✅ Habilitar polimorfismo básico
- ✅ Constructor base funcional

## 🏆 Ejercicio Práctico

### Desafío:
Toma las clases `Car` y `Motorcycle` del archivo `-Before.cs` y refactorízalas usando Extract Base Class.

### Pasos sugeridos:
1. Identificar propiedades y métodos comunes
2. Crear clase base abstracta `Vehicle`
3. Mover funcionalidad común a la base
4. Refactorizar clases derivadas
5. Crear `VehicleManager` polimórfico
6. Probar que todo funciona igual

### Checklist de Validación:
- [ ] ¿Eliminé al menos 80% de código duplicado?
- [ ] ¿La clase base tiene solo funcionalidad común?
- [ ] ¿Las clases derivadas solo tienen código específico?
- [ ] ¿El manager puede tratar todos los vehículos polimórficamente?
- [ ] ¿Los constructores usan validación base?

### Tiempo Estimado: 40 minutos
### Nivel: Intermedio-Avanzado

---

## 🔄 Próximo Paso
Una vez dominada la herencia básica, estarás listo para **Reducir Complejidad Ciclomática**, donde aprenderemos técnicas específicas para simplificar métodos complejos.

**Recuerda:** La herencia es poderosa pero úsala solo cuando haya una relación "es-un" clara. ¡No fuerces jerarquías!