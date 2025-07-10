using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace RefactoringAvanzado.Ejercicios
{
    /// <summary>
    /// CÓDIGO LEGACY PROBLEMÁTICO - Sistema de gestión de empleados
    /// 
    /// PROBLEMAS IDENTIFICADOS:
    /// ❌ PROBLEMA 1: God Class - hace demasiadas cosas
    /// ❌ PROBLEMA 2: Long Methods - métodos de 50+ líneas
    /// ❌ PROBLEMA 3: Tight Coupling - dependencias hard-coded
    /// ❌ PROBLEMA 4: No error handling - operaciones pueden fallar
    /// ❌ PROBLEMA 5: Magic numbers y strings por todos lados
    /// ❌ PROBLEMA 6: Violación de todos los principios SOLID
    /// ❌ PROBLEMA 7: No separation of concerns
    /// ❌ PROBLEMA 8: Difficult to test - dependencias externas
    /// </summary>
    public class EmployeeManager
    {
        // ❌ PROBLEMA 1: Fields sin encapsulación
        public string connectionString = "Server=localhost;Database=HR;Trusted_Connection=true;";
        public List<Employee> employees = new List<Employee>();
        public string logFile = "C:\\Logs\\employee.log";

        // ❌ PROBLEMA 2: Clase anémica - solo datos
        public class Employee
        {
            public int ID;
            public string Name;
            public string Email;
            public DateTime HireDate;
            public decimal Salary;
            public string Department;
            public string Position;
            public bool IsActive;
            public string Manager;
            public int VacationDays;
        }

        /// <summary>
        /// ❌ PROBLEMA 3: Método gigante que hace todo
        /// </summary>
        public bool ProcessEmployee(string action, Employee emp)
        {
            // ❌ Magic strings
            if (action == "CREATE")
            {
                try
                {
                    // ❌ Validación mezclada con lógica de negocio
                    if (emp.Name == null || emp.Name.Length < 2)
                    {
                        Console.WriteLine("Error: Name is required and must be at least 2 characters");
                        return false;
                    }

                    if (emp.Email == null || !emp.Email.Contains("@"))
                    {
                        Console.WriteLine("Error: Valid email is required");
                        return false;
                    }

                    if (emp.Salary < 30000 || emp.Salary > 200000)
                    {
                        Console.WriteLine("Error: Salary must be between 30,000 and 200,000");
                        return false;
                    }

                    // ❌ Database access mezclado con lógica
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        // ❌ SQL injection vulnerable
                        string sql = "INSERT INTO Employees (Name, Email, HireDate, Salary, Department, Position, IsActive, Manager, VacationDays) " +
                                   "VALUES ('" + emp.Name + "', '" + emp.Email + "', '" + emp.HireDate.ToString("yyyy-MM-dd") + "', " +
                                   emp.Salary + ", '" + emp.Department + "', '" + emp.Position + "', " + 
                                   (emp.IsActive ? 1 : 0) + ", '" + emp.Manager + "', " + emp.VacationDays + ")";

                        var command = new SqlCommand(sql, connection);
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            // ❌ Logging hard-coded
                            System.IO.File.AppendAllText(logFile, 
                                DateTime.Now.ToString() + " - Employee created: " + emp.Name + "\n");
                            
                            // ❌ Business logic mezclada
                            if (emp.Department == "IT" && emp.Salary > 80000)
                            {
                                // Special IT bonus calculation
                                emp.VacationDays += 5;
                                
                                string updateSql = "UPDATE Employees SET VacationDays = " + emp.VacationDays + 
                                                 " WHERE Name = '" + emp.Name + "'";
                                var updateCommand = new SqlCommand(updateSql, connection);
                                updateCommand.ExecuteNonQuery();
                            }

                            // ❌ Email sending mezclado
                            SendWelcomeEmail(emp.Email, emp.Name);
                            
                            employees.Add(emp);
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ❌ Poor error handling
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
            else if (action == "UPDATE")
            {
                // ❌ Duplicated validation logic
                if (emp.Name == null || emp.Name.Length < 2)
                {
                    Console.WriteLine("Error: Name is required");
                    return false;
                }

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        // ❌ More SQL injection
                        string sql = "UPDATE Employees SET Name = '" + emp.Name + "', Email = '" + emp.Email + 
                                   "', Salary = " + emp.Salary + ", Department = '" + emp.Department + 
                                   "', Position = '" + emp.Position + "', IsActive = " + (emp.IsActive ? 1 : 0) + 
                                   ", Manager = '" + emp.Manager + "', VacationDays = " + emp.VacationDays + 
                                   " WHERE ID = " + emp.ID;

                        var command = new SqlCommand(sql, connection);
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            System.IO.File.AppendAllText(logFile, 
                                DateTime.Now.ToString() + " - Employee updated: " + emp.Name + "\n");
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating: " + ex.Message);
                    return false;
                }
            }
            else if (action == "DELETE")
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        string sql = "DELETE FROM Employees WHERE ID = " + emp.ID;
                        var command = new SqlCommand(sql, connection);
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            System.IO.File.AppendAllText(logFile, 
                                DateTime.Now.ToString() + " - Employee deleted: " + emp.ID + "\n");
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting: " + ex.Message);
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// ❌ PROBLEMA 4: Método con muchas responsabilidades
        /// </summary>
        public void GenerateReport(string reportType, string department = null)
        {
            if (reportType == "SALARY")
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "SELECT * FROM Employees";
                        
                        if (department != null)
                        {
                            sql += " WHERE Department = '" + department + "'";
                        }

                        var command = new SqlCommand(sql, connection);
                        var reader = command.ExecuteReader();

                        decimal totalSalary = 0;
                        int count = 0;

                        while (reader.Read())
                        {
                            decimal salary = Convert.ToDecimal(reader["Salary"]);
                            totalSalary += salary;
                            count++;
                            
                            // ❌ Presentation logic mezclada
                            Console.WriteLine($"{reader["Name"]}: ${salary:F2}");
                        }

                        Console.WriteLine($"Total employees: {count}");
                        Console.WriteLine($"Total salary cost: ${totalSalary:F2}");
                        Console.WriteLine($"Average salary: ${(count > 0 ? totalSalary / count : 0):F2}");

                        // ❌ File operations mezcladas
                        string reportContent = $"Salary Report - {DateTime.Now}\n" +
                                             $"Department: {department ?? "All"}\n" +
                                             $"Total employees: {count}\n" +
                                             $"Total salary: ${totalSalary:F2}\n";
                        
                        System.IO.File.WriteAllText($"salary_report_{DateTime.Now:yyyyMMdd}.txt", reportContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Report error: " + ex.Message);
                }
            }
            else if (reportType == "VACATION")
            {
                // ❌ Duplicated database code
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "SELECT Name, VacationDays FROM Employees WHERE IsActive = 1";

                        var command = new SqlCommand(sql, connection);
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int vacationDays = Convert.ToInt32(reader["VacationDays"]);
                            string name = reader["Name"].ToString();
                            
                            Console.WriteLine($"{name}: {vacationDays} vacation days");
                            
                            // ❌ Business logic en presentation
                            if (vacationDays > 25)
                            {
                                Console.WriteLine($"  WARNING: {name} has excessive vacation days!");
                            }
                            else if (vacationDays < 5)
                            {
                                Console.WriteLine($"  ALERT: {name} needs to take vacation!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Vacation report error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// ❌ PROBLEMA 5: Email sending hard-coded
        /// </summary>
        private void SendWelcomeEmail(string email, string name)
        {
            try
            {
                // ❌ Hard-coded email logic
                Console.WriteLine($"Sending welcome email to {email}");
                Console.WriteLine($"Subject: Welcome {name}!");
                Console.WriteLine($"Body: Welcome to our company, {name}. Please review our employee handbook.");
                
                // TODO: Implement actual email sending
                System.IO.File.AppendAllText(logFile, 
                    DateTime.Now.ToString() + " - Welcome email sent to: " + email + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email error: " + ex.Message);
            }
        }

        /// <summary>
        /// ❌ PROBLEMA 6: Method con side effects no documentados
        /// </summary>
        public Employee FindEmployeeByEmail(string email)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // ❌ SQL injection again
                    string sql = "SELECT * FROM Employees WHERE Email = '" + email + "'";
                    var command = new SqlCommand(sql, connection);
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var emp = new Employee
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            HireDate = Convert.ToDateTime(reader["HireDate"]),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            Department = reader["Department"].ToString(),
                            Position = reader["Position"].ToString(),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            Manager = reader["Manager"].ToString(),
                            VacationDays = Convert.ToInt32(reader["VacationDays"])
                        };

                        // ❌ Side effect no documentado - logging en query method
                        System.IO.File.AppendAllText(logFile, 
                            DateTime.Now.ToString() + " - Employee searched: " + email + "\n");

                        return emp;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Search error: " + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// ❌ PROBLEMA 7: Business logic mezclada con cálculos
        /// </summary>
        public decimal CalculateBonus(Employee emp, int performanceRating)
        {
            decimal bonus = 0;

            // ❌ Magic numbers everywhere
            if (performanceRating >= 9)
            {
                bonus = emp.Salary * 0.15m; // 15% bonus
            }
            else if (performanceRating >= 7)
            {
                bonus = emp.Salary * 0.10m; // 10% bonus
            }
            else if (performanceRating >= 5)
            {
                bonus = emp.Salary * 0.05m; // 5% bonus
            }

            // ❌ Department-specific logic scattered
            if (emp.Department == "Sales" && performanceRating >= 8)
            {
                bonus += 5000; // Sales excellence bonus
            }

            if (emp.Department == "IT" && emp.Position.Contains("Senior"))
            {
                bonus += 2000; // Senior IT bonus
            }

            // ❌ Date calculations hard-coded
            var yearsOfService = DateTime.Now.Year - emp.HireDate.Year;
            if (yearsOfService >= 5)
            {
                bonus += 1000; // Loyalty bonus
            }

            // ❌ Logging mezclado con calculation
            System.IO.File.AppendAllText(logFile, 
                DateTime.Now.ToString() + " - Bonus calculated for " + emp.Name + ": $" + bonus + "\n");

            return bonus;
        }
    }

    /*
    PROBLEMAS PRINCIPALES IDENTIFICADOS:

    1. GOD CLASS:
       - EmployeeManager hace TODO: validación, DB, email, logging, reportes
       - Viola Single Responsibility brutalmente
       - 300+ líneas en una sola clase

    2. LONG METHODS:
       - ProcessEmployee: 80+ líneas con toda la lógica
       - GenerateReport: 60+ líneas mezclando responsabilidades
       - Difícil de entender y mantener

    3. TIGHT COUPLING:
       - Connection string hard-coded
       - File paths hard-coded
       - SQL queries inline
       - Email logic embedded

    4. NO ERROR HANDLING:
       - Exceptions solo con Console.WriteLine
       - SQL operations pueden fallar silently
       - No recovery mechanisms

    5. SECURITY ISSUES:
       - SQL Injection en TODOS los queries
       - No validation de inputs
       - Paths absolutos expuestos

    6. SOLID VIOLATIONS:
       - S: Una clase hace todo
       - O: No extensible sin modificar
       - L: No hay abstracciones
       - I: No hay interfaces
       - D: Depende de concreciones

    OBJETIVO DEL EJERCICIO:
    Refactorizar este código aplicando:
    ✅ SOLID principles
    ✅ Design patterns apropiados
    ✅ Separation of concerns
    ✅ Dependency injection
    ✅ Proper error handling
    ✅ Security best practices

    TIEMPO ESTIMADO: 90 minutos
    DIFICULTAD: Avanzado
    */
}