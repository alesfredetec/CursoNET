using System;
using System.Collections.Generic;

namespace RefactoringJr.Ejercicios
{
    /// <summary>
    /// EJERCICIO 3: Extract Base Class - ANTES del refactoring
    /// 
    /// PROBLEMAS IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Código duplicado entre clases similares
    /// ❌ PROBLEMA 2: Propiedades y métodos comunes repetidos
    /// ❌ PROBLEMA 3: Lógica de validación idéntica en múltiples clases
    /// ❌ PROBLEMA 4: Imposible agregar funcionalidad común sin tocar todas las clases
    /// ❌ PROBLEMA 5: Violación del principio DRY a nivel de clases
    /// 
    /// OBJETIVO: Extraer clase base para eliminar duplicación
    /// CONCEPTOS A APRENDER: Herencia, clases base, métodos virtuales
    /// TIEMPO ESTIMADO: 35 minutos
    /// </summary>
    
    /// <summary>
    /// ❌ CLASE 1: Sistema de empleados con lógica duplicada
    /// </summary>
    public class FullTimeEmployee
    {
        // ❌ Propiedades duplicadas (aparecen en todas las clases de empleados)
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        
        // Propiedades específicas
        public decimal AnnualSalary { get; set; }
        public int VacationDays { get; set; }
        public bool HasHealthInsurance { get; set; }
        
        // ❌ Constructor duplicado (lógica común)
        public FullTimeEmployee(int id, string name, string email, string department)
        {
            Id = id;
            Name = name;
            Email = email;
            Department = department;
            HireDate = DateTime.Now;
            IsActive = true;
            
            // Validación duplicada
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is required");
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new ArgumentException("Valid email is required");
            if (string.IsNullOrEmpty(department))
                throw new ArgumentException("Department is required");
        }
        
        // ❌ Método duplicado (aparece en todas las clases)
        public void UpdateContactInfo(string newEmail)
        {
            if (string.IsNullOrEmpty(newEmail) || !newEmail.Contains("@"))
                throw new ArgumentException("Valid email is required");
                
            Email = newEmail;
            Console.WriteLine($"Contact info updated for {Name}");
        }
        
        // ❌ Método duplicado (aparece en todas las clases)
        public void Deactivate()
        {
            IsActive = false;
            Console.WriteLine($"Employee {Name} has been deactivated");
        }
        
        // ❌ Método duplicado (aparece en todas las clases)
        public string GetEmployeeInfo()
        {
            var status = IsActive ? "Active" : "Inactive";
            return $"ID: {Id}, Name: {Name}, Department: {Department}, Status: {status}";
        }
        
        // Método específico
        public decimal CalculateMonthlyPay()
        {
            return AnnualSalary / 12;
        }
        
        public void RequestVacation(int days)
        {
            if (days > VacationDays)
                throw new InvalidOperationException("Not enough vacation days");
                
            VacationDays -= days;
            Console.WriteLine($"{Name} requested {days} vacation days. Remaining: {VacationDays}");
        }
    }
    
    /// <summary>
    /// ❌ CLASE 2: Empleado de medio tiempo con 80% código duplicado
    /// </summary>
    public class PartTimeEmployee
    {
        // ❌ PROPIEDADES DUPLICADAS (idénticas a FullTimeEmployee)
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        
        // Propiedades específicas
        public decimal HourlyRate { get; set; }
        public int WeeklyHours { get; set; }
        public bool IsEligibleForBenefits { get; set; }
        
        // ❌ CONSTRUCTOR DUPLICADO (lógica idéntica)
        public PartTimeEmployee(int id, string name, string email, string department)
        {
            Id = id;
            Name = name;
            Email = email;
            Department = department;
            HireDate = DateTime.Now;
            IsActive = true;
            
            // ❌ Validación duplicada (idéntica)
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is required");
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new ArgumentException("Valid email is required");
            if (string.IsNullOrEmpty(department))
                throw new ArgumentException("Department is required");
        }
        
        // ❌ MÉTODO DUPLICADO (idéntico a FullTimeEmployee)
        public void UpdateContactInfo(string newEmail)
        {
            if (string.IsNullOrEmpty(newEmail) || !newEmail.Contains("@"))
                throw new ArgumentException("Valid email is required");
                
            Email = newEmail;
            Console.WriteLine($"Contact info updated for {Name}");
        }
        
        // ❌ MÉTODO DUPLICADO (idéntico a FullTimeEmployee)
        public void Deactivate()
        {
            IsActive = false;
            Console.WriteLine($"Employee {Name} has been deactivated");
        }
        
        // ❌ MÉTODO DUPLICADO (idéntico a FullTimeEmployee)
        public string GetEmployeeInfo()
        {
            var status = IsActive ? "Active" : "Inactive";
            return $"ID: {Id}, Name: {Name}, Department: {Department}, Status: {status}";
        }
        
        // Método específico
        public decimal CalculateWeeklyPay()
        {
            return HourlyRate * WeeklyHours;
        }
        
        public void UpdateSchedule(int newWeeklyHours)
        {
            if (newWeeklyHours > 40)
                throw new InvalidOperationException("Part-time employees cannot work more than 40 hours");
                
            WeeklyHours = newWeeklyHours;
            Console.WriteLine($"{Name}'s schedule updated to {newWeeklyHours} hours per week");
        }
    }
    
    /// <summary>
    /// ❌ CLASE 3: Contratista con más código duplicado
    /// </summary>
    public class Contractor
    {
        // ❌ PROPIEDADES DUPLICADAS (mayoría idénticas)
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        
        // Propiedades específicas
        public DateTime ContractEndDate { get; set; }
        public decimal ProjectRate { get; set; }
        public string ProjectName { get; set; }
        
        // ❌ CONSTRUCTOR DUPLICADO (lógica casi idéntica)
        public Contractor(int id, string name, string email, string department, DateTime contractEndDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Department = department;
            HireDate = DateTime.Now;
            IsActive = true;
            ContractEndDate = contractEndDate;
            
            // ❌ Validación duplicada (idéntica)
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name is required");
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new ArgumentException("Valid email is required");
            if (string.IsNullOrEmpty(department))
                throw new ArgumentException("Department is required");
                
            // Validación específica
            if (contractEndDate <= DateTime.Now)
                throw new ArgumentException("Contract end date must be in the future");
        }
        
        // ❌ MÉTODO DUPLICADO (idéntico)
        public void UpdateContactInfo(string newEmail)
        {
            if (string.IsNullOrEmpty(newEmail) || !newEmail.Contains("@"))
                throw new ArgumentException("Valid email is required");
                
            Email = newEmail;
            Console.WriteLine($"Contact info updated for {Name}");
        }
        
        // ❌ MÉTODO DUPLICADO (idéntico)
        public void Deactivate()
        {
            IsActive = false;
            Console.WriteLine($"Employee {Name} has been deactivated");
        }
        
        // ❌ MÉTODO DUPLICADO (idéntico)
        public string GetEmployeeInfo()
        {
            var status = IsActive ? "Active" : "Inactive";
            return $"ID: {Id}, Name: {Name}, Department: {Department}, Status: {status}";
        }
        
        // Método específico
        public bool IsContractExpired()
        {
            return DateTime.Now > ContractEndDate;
        }
        
        public void ExtendContract(DateTime newEndDate)
        {
            if (newEndDate <= ContractEndDate)
                throw new InvalidOperationException("New end date must be later than current end date");
                
            ContractEndDate = newEndDate;
            Console.WriteLine($"{Name}'s contract extended until {newEndDate:yyyy-MM-dd}");
        }
    }
    
    /// <summary>
    /// ❌ PROBLEMA SIMILAR EN VEHÍCULOS: Más clases con duplicación
    /// </summary>
    
    /// <summary>
    /// ❌ CLASE 4: Auto con lógica duplicada
    /// </summary>
    public class Car
    {
        // ❌ Propiedades duplicadas (aparecen en todos los vehículos)
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public bool IsRunning { get; set; }
        public int Mileage { get; set; }
        
        // Propiedades específicas
        public int NumberOfDoors { get; set; }
        public bool HasAirConditioning { get; set; }
        public string TransmissionType { get; set; }
        
        // ❌ Constructor duplicado
        public Car(string make, string model, int year, string color, string licensePlate)
        {
            Make = make;
            Model = model;
            Year = year;
            Color = color;
            LicensePlate = licensePlate;
            IsRunning = false;
            Mileage = 0;
            
            // ❌ Validación duplicada
            if (string.IsNullOrEmpty(make))
                throw new ArgumentException("Make is required");
            if (string.IsNullOrEmpty(model))
                throw new ArgumentException("Model is required");
            if (year < 1900 || year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");
            if (string.IsNullOrEmpty(licensePlate))
                throw new ArgumentException("License plate is required");
        }
        
        // ❌ Métodos duplicados (aparecen en todos los vehículos)
        public void Start()
        {
            if (IsRunning)
            {
                Console.WriteLine($"{Make} {Model} is already running");
                return;
            }
            
            IsRunning = true;
            Console.WriteLine($"{Make} {Model} started");
        }
        
        public void Stop()
        {
            if (!IsRunning)
            {
                Console.WriteLine($"{Make} {Model} is already stopped");
                return;
            }
            
            IsRunning = false;
            Console.WriteLine($"{Make} {Model} stopped");
        }
        
        public void Drive(int miles)
        {
            if (!IsRunning)
            {
                Console.WriteLine($"Cannot drive {Make} {Model} - vehicle is not running");
                return;
            }
            
            Mileage += miles;
            Console.WriteLine($"Drove {miles} miles. Total mileage: {Mileage}");
        }
        
        public string GetVehicleInfo()
        {
            var status = IsRunning ? "Running" : "Stopped";
            return $"{Year} {Make} {Model} ({Color}) - {LicensePlate} - {status} - {Mileage} miles";
        }
        
        // Método específico
        public void OpenTrunk()
        {
            Console.WriteLine($"Trunk opened on {Make} {Model}");
        }
    }
    
    /// <summary>
    /// ❌ CLASE 5: Motocicleta con 90% código duplicado
    /// </summary>
    public class Motorcycle
    {
        // ❌ PROPIEDADES DUPLICADAS (idénticas a Car)
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public bool IsRunning { get; set; }
        public int Mileage { get; set; }
        
        // Propiedades específicas
        public int EngineSize { get; set; }
        public bool HasSidecar { get; set; }
        public string BikeType { get; set; }
        
        // ❌ CONSTRUCTOR DUPLICADO (idéntico a Car)
        public Motorcycle(string make, string model, int year, string color, string licensePlate)
        {
            Make = make;
            Model = model;
            Year = year;
            Color = color;
            LicensePlate = licensePlate;
            IsRunning = false;
            Mileage = 0;
            
            // ❌ Validación duplicada (idéntica)
            if (string.IsNullOrEmpty(make))
                throw new ArgumentException("Make is required");
            if (string.IsNullOrEmpty(model))
                throw new ArgumentException("Model is required");
            if (year < 1900 || year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");
            if (string.IsNullOrEmpty(licensePlate))
                throw new ArgumentException("License plate is required");
        }
        
        // ❌ MÉTODOS DUPLICADOS (idénticos a Car)
        public void Start()
        {
            if (IsRunning)
            {
                Console.WriteLine($"{Make} {Model} is already running");
                return;
            }
            
            IsRunning = true;
            Console.WriteLine($"{Make} {Model} started");
        }
        
        public void Stop()
        {
            if (!IsRunning)
            {
                Console.WriteLine($"{Make} {Model} is already stopped");
                return;
            }
            
            IsRunning = false;
            Console.WriteLine($"{Make} {Model} stopped");
        }
        
        public void Drive(int miles)
        {
            if (!IsRunning)
            {
                Console.WriteLine($"Cannot drive {Make} {Model} - vehicle is not running");
                return;
            }
            
            Mileage += miles;
            Console.WriteLine($"Drove {miles} miles. Total mileage: {Mileage}");
        }
        
        public string GetVehicleInfo()
        {
            var status = IsRunning ? "Running" : "Stopped";
            return $"{Year} {Make} {Model} ({Color}) - {LicensePlate} - {status} - {Mileage} miles";
        }
        
        // Método específico
        public void PopWheelie()
        {
            if (!IsRunning)
            {
                Console.WriteLine("Cannot pop wheelie - motorcycle is not running");
                return;
            }
            
            Console.WriteLine($"Popping wheelie on {Make} {Model}!");
        }
    }
    
    /// <summary>
    /// ❌ PROBLEMA ADICIONAL: Manager que usa estas clases también duplica lógica
    /// </summary>
    public class EmployeeManager
    {
        private List<FullTimeEmployee> fullTimeEmployees = new List<FullTimeEmployee>();
        private List<PartTimeEmployee> partTimeEmployees = new List<PartTimeEmployee>();
        private List<Contractor> contractors = new List<Contractor>();
        
        // ❌ Métodos duplicados para cada tipo de empleado
        public void AddFullTimeEmployee(FullTimeEmployee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
                
            fullTimeEmployees.Add(employee);
            Console.WriteLine($"Added full-time employee: {employee.Name}");
        }
        
        public void AddPartTimeEmployee(PartTimeEmployee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
                
            partTimeEmployees.Add(employee);
            Console.WriteLine($"Added part-time employee: {employee.Name}");
        }
        
        public void AddContractor(Contractor contractor)
        {
            if (contractor == null)
                throw new ArgumentNullException(nameof(contractor));
                
            contractors.Add(contractor);
            Console.WriteLine($"Added contractor: {contractor.Name}");
        }
        
        // ❌ Métodos similares para buscar por ID
        public FullTimeEmployee FindFullTimeEmployeeById(int id)
        {
            foreach (var emp in fullTimeEmployees)
            {
                if (emp.Id == id && emp.IsActive)
                    return emp;
            }
            return null;
        }
        
        public PartTimeEmployee FindPartTimeEmployeeById(int id)
        {
            foreach (var emp in partTimeEmployees)
            {
                if (emp.Id == id && emp.IsActive)
                    return emp;
            }
            return null;
        }
        
        public Contractor FindContractorById(int id)
        {
            foreach (var contractor in contractors)
            {
                if (contractor.Id == id && contractor.IsActive)
                    return contractor;
            }
            return null;
        }
    }
}

/*
ANÁLISIS DE DUPLICACIÓN MASIVA:

1. EMPLEADOS (3 clases):
   - Propiedades comunes duplicadas: Id, Name, Email, HireDate, Department, IsActive
   - Constructores duplicados: Validación idéntica
   - Métodos duplicados: UpdateContactInfo, Deactivate, GetEmployeeInfo
   - ~80% del código es duplicado entre clases

2. VEHÍCULOS (2 clases):
   - Propiedades comunes duplicadas: Make, Model, Year, Color, LicensePlate, IsRunning, Mileage
   - Constructores duplicados: Validación idéntica
   - Métodos duplicados: Start, Stop, Drive, GetVehicleInfo
   - ~90% del código es duplicado

3. MANAGER:
   - Métodos duplicados para manejar cada tipo
   - Lógica de búsqueda idéntica
   - Imposible tratar empleados polimórficamente

PROBLEMAS PRINCIPALES:
❌ Violación masiva del principio DRY
❌ Mantenimiento: cambiar funcionalidad común requiere tocar N clases
❌ Testing: misma lógica se debe probar N veces
❌ Bugs: error en lógica común se replica en todas las clases
❌ Extensibilidad: agregar nueva funcionalidad común es muy costoso

OBJETIVO DEL REFACTORING:
✅ Crear clases base: Employee, Vehicle
✅ Mover funcionalidad común a la base
✅ Usar herencia para especialización
✅ Eliminar 80-90% de duplicación
✅ Permitir polimorfismo en managers

CONCEPTOS A APRENDER:
- Identificar jerarquías naturales
- Extraer propiedades y métodos comunes
- Usar constructores base
- Métodos virtuales para personalización
- Polimorfismo para simplificar managers

TIEMPO ESTIMADO: 35 minutos
DIFICULTAD: Intermedio
*/