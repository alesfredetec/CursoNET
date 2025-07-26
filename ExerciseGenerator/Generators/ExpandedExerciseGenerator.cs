using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace CursoNET.ExerciseGenerator
{
    /// <summary>
    /// Generador expandido de ejercicios que incluye:
    /// - Biblioteca amplia de ejemplos before/after para diferentes niveles
    /// - Sistema de generación de prompts para IA cuando no existen ejemplos predefinidos
    /// - Captura completa de configuración del mentor y requerimientos pedagógicos
    /// </summary>
    public class ExpandedExerciseGenerator : IExerciseTopicGenerator
    {
        public TopicArea SupportedTopic => TopicArea.CSharpFundamentals; // Expandible a todos los temas

        #region Biblioteca de Ejemplos Before/After

        /// <summary>
        /// Biblioteca expandida de ejemplos organizados por nivel y tipo
        /// </summary>
        private Dictionary<(SkillLevel Level, ExerciseType Type, string Context), ExampleSet> _exampleLibrary = new();

        public class ExampleSet
        {
            public string Title { get; set; } = "";
            public string BeforeCode { get; set; } = "";
            public string AfterCode { get; set; } = "";
            public string Description { get; set; } = "";
            public List<string> KeyConcepts { get; set; } = new();
            public List<string> LearningObjectives { get; set; } = new();
            public string Explanation { get; set; } = "";
            public int ComplexityScore { get; set; } // 1-10
        }

        #endregion

        #region Configuración del Mentor
        // MentorConfiguration ahora está en ExerciseTypes.cs
        #endregion

        #region Sistema de Generación de Prompts para IA
        // AIPromptRequest y AIPromptResult ahora están en ExerciseTypes.cs
        #endregion

        public ExpandedExerciseGenerator()
        {
            InitializeExampleLibrary();
        }

        private void InitializeExampleLibrary()
        {
            _exampleLibrary = new Dictionary<(SkillLevel, ExerciseType, string), ExampleSet>();

            // ====== NIVEL PRINCIPIANTE ======

            // Variables y Tipos de Datos - Refactoring
            _exampleLibrary[(SkillLevel.Beginner, ExerciseType.Refactoring, "Variables")] = new ExampleSet
            {
                Title = "Refactorizar Variables con Nombres Poco Descriptivos",
                Description = "Mejorar la legibilidad del código usando nombres de variables descriptivos y tipos apropiados.",
                BeforeCode = @"using System;

class Program 
{
    static void Main() 
    {
        int a = 25;
        string b = ""Juan"";
        double c = 1.75;
        int d = DateTime.Now.Year - 1998;
        bool e = a >= 18;
        
        Console.WriteLine(b + "" tiene "" + d + "" años y "" + (e ? ""es"" : ""no es"") + "" mayor de edad"");
        Console.WriteLine(""Su altura es: "" + c + ""m"");
    }
}",
                AfterCode = @"using System;

class Program 
{
    static void Main() 
    {
        // Variables con nombres descriptivos y tipos apropiados
        int edadActual = 25;
        string nombrePersona = ""Juan"";
        double alturaEnMetros = 1.75;
        int anioNacimiento = 1998;
        int edadCalculada = DateTime.Now.Year - anioNacimiento;
        bool esMayorDeEdad = edadActual >= 18;
        
        // Uso de interpolación de strings para mayor claridad
        Console.WriteLine($""{nombrePersona} tiene {edadCalculada} años y {(esMayorDeEdad ? ""es"" : ""no es"")} mayor de edad"");
        Console.WriteLine($""Su altura es: {alturaEnMetros:F2}m"");
    }
}",
                KeyConcepts = new() { "Nombres descriptivos", "Interpolación de strings", "Tipos de datos apropiados" },
                LearningObjectives = new() { 
                    "Usar nombres de variables descriptivos",
                    "Aplicar interpolación de strings en lugar de concatenación",
                    "Elegir tipos de datos apropiados para cada situación"
                },
                Explanation = "La mejora se centra en hacer el código más legible y mantenible mediante nombres descriptivos y mejores prácticas de formateo.",
                ComplexityScore = 2
            };

            // Estructuras de Control - Implementation
            _exampleLibrary[(SkillLevel.Beginner, ExerciseType.Implementation, "ControlFlow")] = new ExampleSet
            {
                Title = "Implementar Sistema de Calificaciones Escolares",
                Description = "Crear un sistema que evalúe calificaciones y determine el rendimiento académico usando estructuras de control.",
                BeforeCode = @"// TODO: Implementar un sistema de calificaciones que:
// 1. Solicite al usuario ingresar una calificación (0-100)
// 2. Determine la letra correspondiente (A, B, C, D, F)
// 3. Calcule el promedio de múltiples calificaciones
// 4. Determine si el estudiante aprobó o reprobó
// 5. Muestre estadísticas finales

using System;

class SistemaCalificaciones 
{
    static void Main() 
    {
        // Su implementación aquí...
    }
}",
                AfterCode = @"using System;
using System.Collections.Generic;
using System.Linq;

class SistemaCalificaciones 
{
    static void Main() 
    {
        Console.WriteLine(""=== Sistema de Calificaciones Escolares ==="");
        
        List<double> calificaciones = new List<double>();
        string continuar = ""s"";
        
        // Recopilar calificaciones
        while (continuar.ToLower() == ""s"")
        {
            Console.Write(""Ingrese una calificación (0-100): "");
            
            if (double.TryParse(Console.ReadLine(), out double calificacion))
            {
                if (calificacion >= 0 && calificacion <= 100)
                {
                    calificaciones.Add(calificacion);
                    string letra = ObtenerLetraCalificacion(calificacion);
                    Console.WriteLine($""Calificación: {calificacion:F1} - Letra: {letra}"");
                }
                else
                {
                    Console.WriteLine(""Error: La calificación debe estar entre 0 y 100."");
                }
            }
            else
            {
                Console.WriteLine(""Error: Ingrese un número válido."");
            }
            
            Console.Write(""¿Desea ingresar otra calificación? (s/n): "");
            continuar = Console.ReadLine() ?? ""n"";
        }
        
        // Mostrar estadísticas
        if (calificaciones.Count > 0)
        {
            MostrarEstadisticas(calificaciones);
        }
        else
        {
            Console.WriteLine(""No se ingresaron calificaciones."");
        }
    }
    
    static string ObtenerLetraCalificacion(double calificacion)
    {
        // Uso de switch expression (C# 8.0+)
        return calificacion switch
        {
            >= 90 => ""A"",
            >= 80 => ""B"",
            >= 70 => ""C"",
            >= 60 => ""D"",
            _ => ""F""
        };
    }
    
    static void MostrarEstadisticas(List<double> calificaciones)
    {
        double promedio = calificaciones.Average();
        double maxima = calificaciones.Max();
        double minima = calificaciones.Min();
        bool aprobo = promedio >= 60;
        
        Console.WriteLine(""\\n=== Estadísticas Finales ==="");
        Console.WriteLine($""Total de calificaciones: {calificaciones.Count}"");
        Console.WriteLine($""Promedio: {promedio:F2} ({ObtenerLetraCalificacion(promedio)})"");
        Console.WriteLine($""Calificación más alta: {maxima:F1}"");
        Console.WriteLine($""Calificación más baja: {minima:F1}"");
        Console.WriteLine($""Estado: {(aprobo ? ""APROBADO"" : ""REPROBADO"")}"");
        
        // Mostrar distribución de calificaciones
        var distribucion = calificaciones
            .GroupBy(c => ObtenerLetraCalificacion(c))
            .OrderBy(g => g.Key);
            
        Console.WriteLine(""\\nDistribución por letra:"");
        foreach (var grupo in distribucion)
        {
            Console.WriteLine($""  {grupo.Key}: {grupo.Count()} calificación(es)"");
        }
    }
}",
                KeyConcepts = new() { "Bucles", "Condicionales", "Switch expressions", "Validación de entrada", "LINQ básico" },
                LearningObjectives = new() { 
                    "Implementar bucles para entrada repetitiva de datos",
                    "Usar condicionales para validación y lógica de negocio",
                    "Aplicar switch expressions para simplificar código",
                    "Manejar entrada de usuario con validación"
                },
                Explanation = "Este ejercicio combina múltiples estructuras de control en un contexto práctico, enseñando buenas prácticas de validación y organización de código.",
                ComplexityScore = 4
            };

            // ====== NIVEL INTERMEDIO ======

            // LINQ - Refactoring
            _exampleLibrary[(SkillLevel.Intermediate, ExerciseType.Refactoring, "LINQ")] = new ExampleSet
            {
                Title = "Refactorizar Bucles Imperativo a LINQ Declarativo",
                Description = "Transformar código que usa bucles tradicionales por consultas LINQ más expresivas y funcionales.",
                BeforeCode = @"using System;
using System.Collections.Generic;

public class ProcesadorVentas
{
    public class Venta
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public string Vendedor { get; set; }
        public string Categoria { get; set; }
    }
    
    public static void ProcesarVentas(List<Venta> ventas)
    {
        // Código imperativo con bucles tradicionales
        
        // 1. Encontrar ventas mayores a $1000
        List<Venta> ventasAltas = new List<Venta>();
        foreach (var venta in ventas)
        {
            if (venta.Precio > 1000)
            {
                ventasAltas.Add(venta);
            }
        }
        
        // 2. Calcular total por vendedor
        Dictionary<string, decimal> totalesPorVendedor = new Dictionary<string, decimal>();
        foreach (var venta in ventas)
        {
            if (totalesPorVendedor.ContainsKey(venta.Vendedor))
            {
                totalesPorVendedor[venta.Vendedor] += venta.Precio;
            }
            else
            {
                totalesPorVendedor[venta.Vendedor] = venta.Precio;
            }
        }
        
        // 3. Obtener top 5 productos más caros
        List<Venta> ventasOrdenadas = new List<Venta>(ventas);
        ventasOrdenadas.Sort((v1, v2) => v2.Precio.CompareTo(v1.Precio));
        List<Venta> top5 = new List<Venta>();
        for (int i = 0; i < Math.Min(5, ventasOrdenadas.Count); i++)
        {
            top5.Add(ventasOrdenadas[i]);
        }
        
        // 4. Contar ventas por categoría del mes actual
        Dictionary<string, int> ventasPorCategoria = new Dictionary<string, int>();
        foreach (var venta in ventas)
        {
            if (venta.Fecha.Month == DateTime.Now.Month && venta.Fecha.Year == DateTime.Now.Year)
            {
                if (ventasPorCategoria.ContainsKey(venta.Categoria))
                {
                    ventasPorCategoria[venta.Categoria]++;
                }
                else
                {
                    ventasPorCategoria[venta.Categoria] = 1;
                }
            }
        }
        
        // Mostrar resultados
        Console.WriteLine(""Ventas altas: "" + ventasAltas.Count);
        Console.WriteLine(""Vendedores: "" + totalesPorVendedor.Count);
        Console.WriteLine(""Top productos: "" + top5.Count);
        Console.WriteLine(""Categorías del mes: "" + ventasPorCategoria.Count);
    }
}",
                AfterCode = @"using System;
using System.Collections.Generic;
using System.Linq;

public class ProcesadorVentasMejorado
{
    public class Venta
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public string Vendedor { get; set; }
        public string Categoria { get; set; }
    }
    
    public static void ProcesarVentas(List<Venta> ventas)
    {
        // Código declarativo con LINQ
        
        // 1. Encontrar ventas mayores a $1000 (más legible y expresivo)
        var ventasAltas = ventas
            .Where(v => v.Precio > 1000)
            .ToList();
        
        // 2. Calcular total por vendedor (una línea vs múltiples)
        var totalesPorVendedor = ventas
            .GroupBy(v => v.Vendedor)
            .ToDictionary(g => g.Key, g => g.Sum(v => v.Precio));
        
        // 3. Obtener top 5 productos más caros (más claro el intent)
        var top5Productos = ventas
            .OrderByDescending(v => v.Precio)
            .Take(5)
            .ToList();
        
        // 4. Contar ventas por categoría del mes actual (filtro y agrupación expresiva)
        var ventasMesActual = DateTime.Now;
        var ventasPorCategoriaDelMes = ventas
            .Where(v => v.Fecha.Month == ventasMesActual.Month && v.Fecha.Year == ventasMesActual.Year)
            .GroupBy(v => v.Categoria)
            .ToDictionary(g => g.Key, g => g.Count());
        
        // Análisis adicional facilitado por LINQ
        
        // 5. Promedio de ventas por vendedor
        var promediosPorVendedor = ventas
            .GroupBy(v => v.Vendedor)
            .Select(g => new { 
                Vendedor = g.Key, 
                Promedio = g.Average(v => v.Precio),
                TotalVentas = g.Count()
            })
            .OrderByDescending(x => x.Promedio);
        
        // 6. Análisis de tendencias por mes
        var ventasPorMes = ventas
            .GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
            .Select(g => new {
                Periodo = $""{g.Key.Month:D2}/{g.Key.Year}"",
                TotalVentas = g.Sum(v => v.Precio),
                CantidadVentas = g.Count(),
                PromedioVenta = g.Average(v => v.Precio)
            })
            .OrderBy(x => x.Periodo);
        
        // 7. Productos que nunca se vendieron este mes (si tuviéramos catálogo completo)
        var categoriasActivas = ventas
            .Where(v => v.Fecha.Month == DateTime.Now.Month)
            .Select(v => v.Categoria)
            .Distinct()
            .OrderBy(c => c);
        
        // Mostrar resultados con información más rica
        Console.WriteLine($""Ventas altas (>${1000:N0}+): {ventasAltas.Count}"");
        Console.WriteLine($""Total vendedores activos: {totalesPorVendedor.Count}"");
        Console.WriteLine($""Top 5 productos más caros encontrados: {top5Productos.Count}"");
        Console.WriteLine($""Categorías con ventas este mes: {ventasPorCategoriaDelMes.Count}"");
        
        // Información adicional disponible fácilmente
        Console.WriteLine($""\\nAnálisis adicional:"");
        Console.WriteLine($""Mejor vendedor por promedio: {promediosPorVendedor.FirstOrDefault()?.Vendedor}"");
        Console.WriteLine($""Meses con datos: {ventasPorMes.Count()}"");
        Console.WriteLine($""Categorías activas este mes: {string.Join("", "", categoriasActivas)}"");
    }
}",
                KeyConcepts = new() { "LINQ", "Programación funcional", "Método encadenado", "Expresiones lambda", "GroupBy", "Aggregate functions" },
                LearningObjectives = new() { 
                    "Reemplazar bucles imperativos con consultas LINQ declarativas",
                    "Usar métodos de extensión para transformar y filtrar datos",
                    "Aplicar operaciones de agrupación y agregación",
                    "Comprender la diferencia entre programación imperativa y declarativa"
                },
                Explanation = "La refactorización muestra cómo LINQ hace el código más expresivo, legible y mantenible, reduciendo la posibilidad de errores y facilitando análisis complejos.",
                ComplexityScore = 6
            };

            // ====== NIVEL AVANZADO ======

            // Design Patterns - Implementation
            _exampleLibrary[(SkillLevel.Advanced, ExerciseType.Implementation, "DesignPatterns")] = new ExampleSet
            {
                Title = "Implementar Patrón Strategy con Factory para Sistema de Descuentos",
                Description = "Diseñar un sistema de descuentos extensible usando el patrón Strategy combinado con Factory Method.",
                BeforeCode = @"// TODO: Implementar un sistema de descuentos que:
// 1. Permita múltiples tipos de descuentos (Porcentaje, Cantidad Fija, BOGO, etc.)
// 2. Sea extensible para agregar nuevos tipos sin modificar código existente
// 3. Use el patrón Strategy para encapsular algoritmos de descuento
// 4. Use Factory Method para crear instancias de descuentos
// 5. Incluya validación y cálculo de descuentos máximos
// 6. Soporte combinación de descuentos con reglas de precedencia

using System;
using System.Collections.Generic;

namespace SistemaDescuentos
{
    public class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
    }
    
    public class CarritoCompras
    {
        public List<Producto> Productos { get; set; } = new();
        public string CodigoDescuento { get; set; }
        
        // TODO: Implementar lógica de descuentos aquí
    }
}",
                AfterCode = @"using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDescuentos
{
    #region Modelos de Dominio
    
    public class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
    }
    
    public class CarritoCompras
    {
        public List<Producto> Productos { get; set; } = new();
        public string CodigoDescuento { get; set; }
        
        public decimal CalcularSubtotal() => Productos.Sum(p => p.Precio);
    }
    
    public class ResultadoDescuento
    {
        public decimal MontoDescuento { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioFinal { get; set; }
        public string Descripcion { get; set; }
        public bool EsValido { get; set; }
        public string MensajeError { get; set; }
    }
    
    #endregion
    
    #region Patrón Strategy - Estrategias de Descuento
    
    /// <summary>
    /// Interfaz Strategy que define el contrato para todos los algoritmos de descuento
    /// </summary>
    public interface IEstrategiaDescuento
    {
        ResultadoDescuento CalcularDescuento(CarritoCompras carrito, Dictionary<string, object> parametros);
        string TipoDescuento { get; }
        int Prioridad { get; } // Para resolver conflictos cuando se combinan descuentos
    }
    
    /// <summary>
    /// Estrategia de descuento por porcentaje
    /// </summary>
    public class DescuentoPorcentaje : IEstrategiaDescuento
    {
        public string TipoDescuento => ""Porcentaje"";
        public int Prioridad => 1;
        
        public ResultadoDescuento CalcularDescuento(CarritoCompras carrito, Dictionary<string, object> parametros)
        {
            var resultado = new ResultadoDescuento();
            
            if (!parametros.ContainsKey(""porcentaje"") || !parametros.ContainsKey(""montoMinimo""))
            {
                resultado.EsValido = false;
                resultado.MensajeError = ""Faltan parámetros: porcentaje y montoMinimo"";
                return resultado;
            }
            
            var porcentaje = Convert.ToDecimal(parametros[""porcentaje""]);
            var montoMinimo = Convert.ToDecimal(parametros[""montoMinimo""]);
            var subtotal = carrito.CalcularSubtotal();
            
            if (subtotal < montoMinimo)
            {
                resultado.EsValido = false;
                resultado.MensajeError = $""Monto mínimo requerido: ${montoMinimo:C}"";
                return resultado;
            }
            
            resultado.PrecioOriginal = subtotal;
            resultado.MontoDescuento = Math.Round(subtotal * (porcentaje / 100), 2);
            resultado.PrecioFinal = subtotal - resultado.MontoDescuento;
            resultado.Descripcion = $""Descuento del {porcentaje}% aplicado"";
            resultado.EsValido = true;
            
            return resultado;
        }
    }
    
    /// <summary>
    /// Estrategia de descuento por cantidad fija
    /// </summary>
    public class DescuentoCantidadFija : IEstrategiaDescuento
    {
        public string TipoDescuento => ""CantidadFija"";
        public int Prioridad => 2;
        
        public ResultadoDescuento CalcularDescuento(CarritoCompras carrito, Dictionary<string, object> parametros)
        {
            var resultado = new ResultadoDescuento();
            
            if (!parametros.ContainsKey(""monto"") || !parametros.ContainsKey(""montoMinimo""))
            {
                resultado.EsValido = false;
                resultado.MensajeError = ""Faltan parámetros: monto y montoMinimo"";
                return resultado;
            }
            
            var monto = Convert.ToDecimal(parametros[""monto""]);
            var montoMinimo = Convert.ToDecimal(parametros[""montoMinimo""]);
            var subtotal = carrito.CalcularSubtotal();
            
            if (subtotal < montoMinimo)
            {
                resultado.EsValido = false;
                resultado.MensajeError = $""Monto mínimo requerido: ${montoMinimo:C}"";
                return resultado;
            }
            
            resultado.PrecioOriginal = subtotal;
            resultado.MontoDescuento = Math.Min(monto, subtotal); // No puede ser mayor al subtotal
            resultado.PrecioFinal = subtotal - resultado.MontoDescuento;
            resultado.Descripcion = $""Descuento fijo de ${monto:C} aplicado"";
            resultado.EsValido = true;
            
            return resultado;
        }
    }
    
    /// <summary>
    /// Estrategia Buy One Get One (BOGO)
    /// </summary>
    public class DescuentoBOGO : IEstrategiaDescuento
    {
        public string TipoDescuento => ""BOGO"";
        public int Prioridad => 3;
        
        public ResultadoDescuento CalcularDescuento(CarritoCompras carrito, Dictionary<string, object> parametros)
        {
            var resultado = new ResultadoDescuento();
            
            if (!parametros.ContainsKey(""categoria""))
            {
                resultado.EsValido = false;
                resultado.MensajeError = ""Falta parámetro: categoria"";
                return resultado;
            }
            
            var categoria = parametros[""categoria""].ToString();
            var productosCategoria = carrito.Productos
                .Where(p => p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Precio) // Descuenta el más barato
                .ToList();
            
            if (productosCategoria.Count < 2)
            {
                resultado.EsValido = false;
                resultado.MensajeError = $""Se requieren al menos 2 productos de la categoría {categoria}"";
                return resultado;
            }
            
            var subtotal = carrito.CalcularSubtotal();
            var productosGratis = productosCategoria.Count / 2;
            var montoDescuento = productosCategoria.Take(productosGratis).Sum(p => p.Precio);
            
            resultado.PrecioOriginal = subtotal;
            resultado.MontoDescuento = montoDescuento;
            resultado.PrecioFinal = subtotal - montoDescuento;
            resultado.Descripcion = $""BOGO en {categoria}: {productosGratis} producto(s) gratis"";
            resultado.EsValido = true;
            
            return resultado;
        }
    }
    
    #endregion
    
    #region Factory Pattern - Creación de Descuentos
    
    /// <summary>
    /// Abstract Factory para crear estrategias de descuento
    /// </summary>
    public abstract class FactoryDescuento
    {
        public abstract IEstrategiaDescuento CrearEstrategia(string tipoDescuento);
        
        /// <summary>
        /// Factory Method estático para obtener la instancia correcta del factory
        /// </summary>
        public static FactoryDescuento ObtenerFactory(string contexto = ""default"")
        {
            return contexto.ToLower() switch
            {
                ""premium"" => new FactoryDescuentoPremium(),
                ""basico"" => new FactoryDescuentoBasico(),
                _ => new FactoryDescuentoEstandar()
            };
        }
    }
    
    /// <summary>
    /// Factory concreto para descuentos estándar
    /// </summary>
    public class FactoryDescuentoEstandar : FactoryDescuento
    {
        private readonly Dictionary<string, Func<IEstrategiaDescuento>> _estrategias;
        
        public FactoryDescuentoEstandar()
        {
            _estrategias = new Dictionary<string, Func<IEstrategiaDescuento>>
            {
                [""porcentaje""] = () => new DescuentoPorcentaje(),
                [""fijo""] = () => new DescuentoCantidadFija(),
                [""bogo""] = () => new DescuentoBOGO()
            };
        }
        
        public override IEstrategiaDescuento CrearEstrategia(string tipoDescuento)
        {
            var tipo = tipoDescuento.ToLower();
            
            if (_estrategias.ContainsKey(tipo))
                return _estrategias[tipo]();
            
            throw new ArgumentException($""Tipo de descuento no soportado: {tipoDescuento}"");
        }
    }
    
    /// <summary>
    /// Factory para descuentos básicos (solo porcentaje y fijo)
    /// </summary>
    public class FactoryDescuentoBasico : FactoryDescuento
    {
        public override IEstrategiaDescuento CrearEstrategia(string tipoDescuento)
        {
            return tipoDescuento.ToLower() switch
            {
                ""porcentaje"" => new DescuentoPorcentaje(),
                ""fijo"" => new DescuentoCantidadFija(),
                _ => throw new ArgumentException($""Descuento básico no soporta: {tipoDescuento}"")
            };
        }
    }
    
    /// <summary>
    /// Factory para descuentos premium (con estrategias adicionales)
    /// </summary>
    public class FactoryDescuentoPremium : FactoryDescuentoEstandar
    {
        // Podría incluir descuentos adicionales como lealtad, miembresía, etc.
    }
    
    #endregion
    
    #region Servicio Aplicador de Descuentos
    
    /// <summary>
    /// Servicio principal que orquesta la aplicación de descuentos
    /// Implementa el patrón Facade para simplificar la interfaz
    /// </summary>
    public class ServicioDescuentos
    {
        private readonly FactoryDescuento _factory;
        private readonly Dictionary<string, (string tipo, Dictionary<string, object> parametros)> _codigosDescuento;
        
        public ServicioDescuentos(string contexto = ""default"")
        {
            _factory = FactoryDescuento.ObtenerFactory(contexto);
            InicializarCodigos();
        }
        
        private void InicializarCodigos()
        {
            _codigosDescuento = new Dictionary<string, (string, Dictionary<string, object>)>
            {
                [""DESC10""] = (""porcentaje"", new Dictionary<string, object>
                {
                    [""porcentaje""] = 10m,
                    [""montoMinimo""] = 50m
                }),
                [""AHORRA20""] = (""fijo"", new Dictionary<string, object>
                {
                    [""monto""] = 20m,
                    [""montoMinimo""] = 100m
                }),
                [""BOGO_ROPA""] = (""bogo"", new Dictionary<string, object>
                {
                    [""categoria""] = ""Ropa""
                })
            };
        }
        
        public ResultadoDescuento AplicarDescuento(CarritoCompras carrito)
        {
            if (string.IsNullOrWhiteSpace(carrito.CodigoDescuento))
            {
                return new ResultadoDescuento
                {
                    EsValido = false,
                    MensajeError = ""No se proporcionó código de descuento""
                };
            }
            
            if (!_codigosDescuento.ContainsKey(carrito.CodigoDescuento.ToUpper()))
            {
                return new ResultadoDescuento
                {
                    EsValido = false,
                    MensajeError = ""Código de descuento inválido""
                };
            }
            
            var (tipo, parametros) = _codigosDescuento[carrito.CodigoDescuento.ToUpper()];
            
            try
            {
                var estrategia = _factory.CrearEstrategia(tipo);
                return estrategia.CalcularDescuento(carrito, parametros);
            }
            catch (Exception ex)
            {
                return new ResultadoDescuento
                {
                    EsValido = false,
                    MensajeError = $""Error al aplicar descuento: {ex.Message}""
                };
            }
        }
        
        /// <summary>
        /// Método para aplicar múltiples descuentos con reglas de prioridad
        /// </summary>
        public List<ResultadoDescuento> AplicarMultiplesDescuentos(CarritoCompras carrito, List<string> codigos)
        {
            var resultados = new List<ResultadoDescuento>();
            var carritoTemporal = new CarritoCompras { Productos = new List<Producto>(carrito.Productos) };
            
            // Ordenar por prioridad y aplicar descuentos en secuencia
            var descuentosOrdenados = codigos
                .Where(codigo => _codigosDescuento.ContainsKey(codigo.ToUpper()))
                .Select(codigo => new { Codigo = codigo, Config = _codigosDescuento[codigo.ToUpper()] })
                .OrderBy(x => _factory.CrearEstrategia(x.Config.tipo).Prioridad);
            
            foreach (var descuento in descuentosOrdenados)
            {
                carritoTemporal.CodigoDescuento = descuento.Codigo;
                var resultado = AplicarDescuento(carritoTemporal);
                
                if (resultado.EsValido)
                {
                    // Actualizar el carrito temporal con el precio después del descuento
                    var nuevoPrecio = resultado.PrecioFinal / carritoTemporal.Productos.Count;
                    carritoTemporal.Productos.ForEach(p => p.Precio = nuevoPrecio);
                }
                
                resultados.Add(resultado);
            }
            
            return resultados;
        }
    }
    
    #endregion
    
    #region Programa de Demo
    
    class Program
    {
        static void Main(string[] args)
        {
            // Crear productos de ejemplo
            var carrito = new CarritoCompras
            {
                Productos = new List<Producto>
                {
                    new Producto { Id = ""1"", Nombre = ""Camisa"", Precio = 29.99m, Categoria = ""Ropa"" },
                    new Producto { Id = ""2"", Nombre = ""Pantalón"", Precio = 49.99m, Categoria = ""Ropa"" },
                    new Producto { Id = ""3"", Nombre = ""Zapatos"", Precio = 79.99m, Categoria = ""Calzado"" }
                }
            };
            
            var servicio = new ServicioDescuentos();
            
            Console.WriteLine(""=== Sistema de Descuentos con Strategy + Factory ==="");
            Console.WriteLine($""Subtotal: ${carrito.CalcularSubtotal():C}"");
            Console.WriteLine();
            
            // Probar diferentes tipos de descuentos
            var codigosAPotrar = new[] { ""DESC10"", ""AHORRA20"", ""BOGO_ROPA"", ""INVALIDO"" };
            
            foreach (var codigo in codigosAPotrar)
            {
                carrito.CodigoDescuento = codigo;
                var resultado = servicio.AplicarDescuento(carrito);
                
                Console.WriteLine($""Código: {codigo}"");
                if (resultado.EsValido)
                {
                    Console.WriteLine($""  {resultado.Descripcion}"");
                    Console.WriteLine($""  Descuento: ${resultado.MontoDescuento:C}"");
                    Console.WriteLine($""  Total final: ${resultado.PrecioFinal:C}"");
                }
                else
                {
                    Console.WriteLine($""  Error: {resultado.MensajeError}"");
                }
                Console.WriteLine();
            }
        }
    }
    
    #endregion
}",
                KeyConcepts = new() { "Strategy Pattern", "Factory Method", "Abstract Factory", "Facade Pattern", "SOLID Principles", "Extensibilidad" },
                LearningObjectives = new() { 
                    "Implementar el patrón Strategy para algoritmos intercambiables",
                    "Usar Factory Method para creación de objetos flexible",
                    "Combinar múltiples patrones de diseño efectivamente",
                    "Diseñar sistemas extensibles y mantenibles",
                    "Aplicar principios SOLID en diseño de software"
                },
                Explanation = "Este ejercicio demuestra cómo combinar múltiples patrones de diseño para crear un sistema robusto, extensible y mantenible. El patrón Strategy encapsula algoritmos de descuento, Factory Method facilita la creación de estrategias, y Facade simplifica el uso del sistema.",
                ComplexityScore = 9
            };

            // Agregar más ejemplos aquí...
        }

        #region Implementación Principal

        public Exercise GenerateExercise(ExerciseConfiguration config)
        {
            // Verificar si tenemos ejemplos predefinidos
            var exampleKey = (config.Level, config.Type, config.Context);
            
            if (_exampleLibrary.ContainsKey(exampleKey))
            {
                return GenerateFromExistingExample(config, _exampleLibrary[exampleKey]);
            }
            else
            {
                // Generar prompt para IA si no tenemos ejemplos predefinidos
                return GenerateFromAIPrompt(config);
            }
        }

        public bool ValidateConfiguration(ExerciseConfiguration config)
        {
            return true; // Validación básica
        }

        private Exercise GenerateFromExistingExample(ExerciseConfiguration config, ExampleSet example)
        {
            var exercise = new Exercise
            {
                Title = example.Title,
                Level = config.Level,
                Topic = config.Topic,
                Type = config.Type,
                EstimatedMinutes = EstimateTimeFromComplexity(example.ComplexityScore, config.Level),
                Description = example.Description,
                LearningObjectives = new List<string>(example.LearningObjectives),
                StarterCode = example.BeforeCode,
                SolutionCode = example.AfterCode,
                ProblemStatement = GenerateProblemStatement(example)
            };

            // Generar requirements técnicos basados en conceptos clave
            exercise.TechnicalRequirements = example.KeyConcepts
                .Select(concept => $"Implementar correctamente: {concept}")
                .ToList();

            // Generar criterios de éxito
            exercise.SuccessCriteria = GenerateSuccessCriteria(example);

            // Generar pruebas unitarias si es apropiado
            if (config.IncludeUnitTests)
            {
                exercise.UnitTestCode = GenerateUnitTestsForExample(example);
            }

            return exercise;
        }

        private Exercise GenerateFromAIPrompt(ExerciseConfiguration config)
        {
            var mentorConfig = new MentorConfiguration(); // Configuración por defecto
            var aiPrompt = GenerateAIPrompt(config, mentorConfig);
            
            var exercise = new Exercise
            {
                Title = $"Ejercicio Personalizado - {config.Topic} ({config.Type})",
                Level = config.Level,
                Topic = config.Topic,
                Type = config.Type,
                EstimatedMinutes = config.EstimatedMinutes,
                Description = "Ejercicio generado mediante prompt de IA - Requiere implementación manual",
                ProblemStatement = aiPrompt.CompletePrompt,
                StarterCode = "// TODO: Generar código inicial usando IA\n// Prompt: " + aiPrompt.UserMessage,
                SolutionCode = "// TODO: Generar solución usando IA\n// Usar el prompt generado para obtener la implementación completa"
            };

            return exercise;
        }

        #endregion

        #region Generación de Prompts para IA

        /// <summary>
        /// Genera un prompt completo para IA cuando no existen ejemplos predefinidos
        /// Utiliza técnicas avanzadas de Context Engineering para Claude
        /// </summary>
        public AIPromptResult GenerateAIPrompt(ExerciseConfiguration exerciseConfig, MentorConfiguration mentorConfig)
        {
            // Usar el generador avanzado de prompts
            var advancedGenerator = new AdvancedPromptGenerator();
            
            // Crear configuraciones por defecto si no se proporcionan
            var courseContext = AdvancedPromptGenerator.CreateDotNetCourseContext();
            var persona = AdvancedPromptGenerator.CreateSeniorMentorPersona();
            var thinkingFramework = AdvancedPromptGenerator.CreateCoTFramework();
            
            // Personalizar según la configuración del mentor
            PersonalizeContexts(courseContext, persona, mentorConfig, exerciseConfig);
            
            // Generar el prompt avanzado
            var advancedPrompt = advancedGenerator.GenerateAdvancedCoTPrompt(
                exerciseConfig, 
                mentorConfig, 
                courseContext, 
                persona, 
                thinkingFramework
            );
            
            return new AIPromptResult
            {
                CompletePrompt = advancedPrompt,
                SystemMessage = ExtractSystemMessage(advancedPrompt),
                UserMessage = ExtractUserMessage(advancedPrompt),
                PromptParameters = GeneratePromptParameters(exerciseConfig, mentorConfig),
                ValidationCriteria = GenerateValidationCriteria(exerciseConfig)
            };
        }
        
        /// <summary>
        /// Personaliza los contextos basado en la configuración del mentor
        /// </summary>
        private void PersonalizeContexts(
            AdvancedPromptGenerator.CourseSystemContext courseContext,
            AdvancedPromptGenerator.PersonaConfiguration persona,
            MentorConfiguration mentorConfig,
            ExerciseConfiguration exerciseConfig)
        {
            // Personalizar contexto del curso
            if (!string.IsNullOrEmpty(mentorConfig.CourseName))
                courseContext.CourseName = mentorConfig.CourseName;
            
            // Mapear nivel de estudiante a progreso en módulos
            courseContext.CurrentModule = GetCurrentModuleFromTopic(exerciseConfig.Topic);
            
            // Personalizar persona del mentor
            if (!string.IsNullOrEmpty(mentorConfig.MentorName))
                persona.Name = mentorConfig.MentorName;
            
            persona.TeachingPhilosophy = $"{mentorConfig.TeachingStyle} - {persona.TeachingPhilosophy}";
            
            // Agregar contexto específico si está definido
            if (exerciseConfig.Context != "General")
            {
                courseContext.CurrentModule += $" (Contexto: {exerciseConfig.Context})";
            }
        }
        
        private string GetCurrentModuleFromTopic(TopicArea topic)
        {
            return topic switch
            {
                TopicArea.CSharpFundamentals => "Fundamentos de C# y Sintaxis",
                TopicArea.ControlStructures => "Estructuras de Control y Lógica",
                TopicArea.MethodsAndParameters => "Métodos y Organización de Código",
                TopicArea.BasicOOP => "Programación Orientada a Objetos Básica",
                TopicArea.Collections => "Colecciones y Estructuras de Datos",
                TopicArea.ExceptionHandling => "Manejo de Errores y Excepciones",
                TopicArea.AdvancedOOP => "POO Avanzada y Principios SOLID",
                TopicArea.LINQ => "LINQ y Programación Funcional",
                TopicArea.Generics => "Genéricos y Tipos Avanzados",
                TopicArea.DelegatesAndEvents => "Delegates, Events y Callbacks",
                TopicArea.FileIO => "Entrada/Salida y Manejo de Archivos",
                TopicArea.UnitTesting => "Pruebas Unitarias y TDD",
                TopicArea.DesignPatterns => "Patrones de Diseño y Arquitectura",
                TopicArea.EntityFramework => "Entity Framework Core y ORM",
                TopicArea.AspNetCore => "ASP.NET Core y Web APIs",
                TopicArea.AsyncProgramming => "Programación Asíncrona y Multithreading",
                TopicArea.PerformanceOptimization => "Optimización y Performance",
                TopicArea.Microservices => "Microservicios y Arquitectura Distribuida",
                _ => "Módulo Personalizado"
            };
        }
        
        private string ExtractSystemMessage(string fullPrompt)
        {
            // Extraer la sección del contexto del sistema y persona
            var lines = fullPrompt.Split('\n');
            var systemLines = new List<string>();
            bool inSystemSection = false;
            
            foreach (var line in lines)
            {
                if (line.Contains("# 🎯 CONTEXTO DEL SISTEMA EDUCATIVO"))
                {
                    inSystemSection = true;
                }
                else if (line.Contains("# 🎯 TAREA PRINCIPAL CON CHAIN OF THOUGHT"))
                {
                    inSystemSection = false;
                }
                
                if (inSystemSection)
                {
                    systemLines.Add(line);
                }
            }
            
            return string.Join("\n", systemLines);
        }
        
        private string ExtractUserMessage(string fullPrompt)
        {
            // Extraer la sección de la tarea principal
            var lines = fullPrompt.Split('\n');
            var userLines = new List<string>();
            bool inUserSection = false;
            
            foreach (var line in lines)
            {
                if (line.Contains("# 🎯 TAREA PRINCIPAL CON CHAIN OF THOUGHT"))
                {
                    inUserSection = true;
                }
                
                if (inUserSection)
                {
                    userLines.Add(line);
                }
            }
            
            return string.Join("\n", userLines);
        }

        private string GenerateSystemMessage(MentorConfiguration mentorConfig)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"Eres un mentor experto en .NET llamado {mentorConfig.MentorName} que crea ejercicios educativos de alta calidad.");
            sb.AppendLine($"Estás enseñando el curso '{mentorConfig.CourseName}' a estudiantes de nivel {mentorConfig.StudentLevel}.");
            sb.AppendLine();
            sb.AppendLine("Características de tu estilo de enseñanza:");
            sb.AppendLine($"- Estilo pedagógico: {mentorConfig.TeachingStyle}");
            sb.AppendLine($"- Duración típica de sesión: {mentorConfig.SessionDurationMinutes} minutos");
            sb.AppendLine($"- Incluir ejemplos del mundo real: {(mentorConfig.IncludeRealWorldExamples ? "Sí" : "No")}");
            sb.AppendLine($"- Incluir pruebas unitarias: {(mentorConfig.IncludeUnitTests ? "Sí" : "No")}");
            sb.AppendLine($"- Considerar rendimiento: {(mentorConfig.IncludePerformanceConsiderations ? "Sí" : "No")}");
            sb.AppendLine();
            
            if (mentorConfig.CourseObjectives.Any())
            {
                sb.AppendLine("Objetivos del curso:");
                foreach (var objective in mentorConfig.CourseObjectives)
                {
                    sb.AppendLine($"- {objective}");
                }
                sb.AppendLine();
            }
            
            if (mentorConfig.PrerequisiteKnowledge.Any())
            {
                sb.AppendLine("Conocimientos prerequisitos de los estudiantes:");
                foreach (var prereq in mentorConfig.PrerequisiteKnowledge)
                {
                    sb.AppendLine($"- {prereq}");
                }
                sb.AppendLine();
            }
            
            if (mentorConfig.ForbiddenTopics.Any())
            {
                sb.AppendLine("Temas que NO debes incluir:");
                foreach (var forbidden in mentorConfig.ForbiddenTopics)
                {
                    sb.AppendLine($"- {forbidden}");
                }
                sb.AppendLine();
            }
            
            sb.AppendLine("IMPORTANTE: Crea ejercicios que sean:");
            sb.AppendLine("1. Educativamente sólidos y progresivos");
            sb.AppendLine("2. Prácticos y aplicables al mundo real");
            sb.AppendLine("3. Apropiados para el nivel de habilidad especificado");
            sb.AppendLine("4. Completos con código de inicio y solución");
            sb.AppendLine("5. Incluyan explicaciones claras de conceptos clave");
            
            return sb.ToString();
        }

        private string GenerateUserMessage(ExerciseConfiguration exerciseConfig, MentorConfiguration mentorConfig)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("Genera un ejercicio completo de .NET con las siguientes especificaciones:");
            sb.AppendLine();
            
            sb.AppendLine("## Configuración del Ejercicio:");
            sb.AppendLine($"- **Nivel de habilidad**: {exerciseConfig.Level}");
            sb.AppendLine($"- **Área temática**: {exerciseConfig.Topic}");
            sb.AppendLine($"- **Tipo de ejercicio**: {exerciseConfig.Type}");
            sb.AppendLine($"- **Contexto/Dominio**: {exerciseConfig.Context}");
            sb.AppendLine($"- **Tiempo estimado**: {exerciseConfig.EstimatedMinutes} minutos");
            sb.AppendLine($"- **Incluir pruebas unitarias**: {(exerciseConfig.IncludeUnitTests ? "Sí" : "No")}");
            sb.AppendLine($"- **Incluir desafíos de extensión**: {(exerciseConfig.IncludeExtensionChallenges ? "Sí" : "No")}");
            sb.AppendLine();
            
            // Agregar contexto específico
            if (exerciseConfig.Context != "General")
            {
                sb.AppendLine($"## Contexto Específico:");
                sb.AppendLine($"El ejercicio debe estar ambientado en el dominio de: **{exerciseConfig.Context}**");
                sb.AppendLine($"Usa ejemplos, nombres de variables y escenarios relevantes a este dominio.");
                sb.AppendLine();
            }
            
            // Instrucciones específicas por tipo de ejercicio
            sb.AppendLine("## Instrucciones Específicas:");
            switch (exerciseConfig.Type)
            {
                case ExerciseType.Refactoring:
                    sb.AppendLine("- Proporciona código 'antes' que tenga problemas claros pero que funcione");
                    sb.AppendLine("- El código 'después' debe mostrar mejores prácticas y patrones");
                    sb.AppendLine("- Explica específicamente qué se mejoró y por qué");
                    sb.AppendLine("- Incluye comentarios que expliquen las mejoras");
                    break;
                    
                case ExerciseType.Implementation:
                    sb.AppendLine("- Proporciona requerimientos claros y específicos");
                    sb.AppendLine("- Incluye código de inicio con TODOs y estructura básica");
                    sb.AppendLine("- La solución debe demostrar mejores prácticas");
                    sb.AppendLine("- Incluye casos de prueba para validar la implementación");
                    break;
                    
                case ExerciseType.DebugFix:
                    sb.AppendLine("- El código inicial debe tener errores realistas y educativos");
                    sb.AppendLine("- Incluye pistas sobre dónde están los problemas");
                    sb.AppendLine("- La solución debe explicar cada error y su corrección");
                    sb.AppendLine("- Incluye estrategias de debugging");
                    break;
                    
                case ExerciseType.Extension:
                    sb.AppendLine("- Proporciona una base funcional sólida");
                    sb.AppendLine("- Define claramente qué funcionalidades agregar");
                    sb.AppendLine("- Las extensiones deben construir sobre conceptos existentes");
                    sb.AppendLine("- Muestra cómo mantener compatibilidad hacia atrás");
                    break;
                    
                case ExerciseType.Design:
                    sb.AppendLine("- Enfócate en decisiones de arquitectura y diseño");
                    sb.AppendLine("- Incluye diagramas o pseudocódigo cuando sea útil");
                    sb.AppendLine("- Explica trade-offs entre diferentes enfoques");
                    sb.AppendLine("- Considera escalabilidad y mantenibilidad");
                    break;
                    
                case ExerciseType.Performance:
                    sb.AppendLine("- Incluye código que tenga problemas de rendimiento evidentes");
                    sb.AppendLine("- Proporciona métricas o formas de medir mejoras");
                    sb.AppendLine("- Explica conceptos de optimización relevantes");
                    sb.AppendLine("- Incluye benchmarks cuando sea apropiado");
                    break;
                    
                case ExerciseType.Testing:
                    sb.AppendLine("- Enfócate en escribir pruebas comprehensivas");
                    sb.AppendLine("- Incluye diferentes tipos de pruebas (unitarias, integración)");
                    sb.AppendLine("- Demuestra TDD si es apropiado para el nivel");
                    sb.AppendLine("- Incluye casos edge y manejo de errores");
                    break;
            }
            sb.AppendLine();
            
            // Estructura requerida del output
            sb.AppendLine("## Estructura Requerida del Ejercicio:");
            sb.AppendLine("Proporciona la respuesta en el siguiente formato:");
            sb.AppendLine();
            sb.AppendLine("### 1. Título del Ejercicio");
            sb.AppendLine("(Un título descriptivo y atractivo)");
            sb.AppendLine();
            sb.AppendLine("### 2. Descripción");
            sb.AppendLine("(2-3 párrafos explicando qué hará el estudiante)");
            sb.AppendLine();
            sb.AppendLine("### 3. Objetivos de Aprendizaje");
            sb.AppendLine("(Lista de 3-5 objetivos específicos)");
            sb.AppendLine();
            sb.AppendLine("### 4. Prerequisitos");
            sb.AppendLine("(Conocimientos necesarios antes de este ejercicio)");
            sb.AppendLine();
            sb.AppendLine("### 5. Enunciado del Problema");
            sb.AppendLine("(Descripción detallada de lo que debe implementar/resolver)");
            sb.AppendLine();
            sb.AppendLine("### 6. Requerimientos Técnicos");
            sb.AppendLine("(Lista específica de lo que debe cumplir el código)");
            sb.AppendLine();
            sb.AppendLine("### 7. Criterios de Éxito");
            sb.AppendLine("(Cómo saber si la solución es correcta)");
            sb.AppendLine();
            sb.AppendLine("### 8. Código de Inicio");
            sb.AppendLine("```csharp");
            sb.AppendLine("// Código inicial aquí");
            sb.AppendLine("```");
            sb.AppendLine();
            sb.AppendLine("### 9. Solución Completa");
            sb.AppendLine("```csharp");
            sb.AppendLine("// Código solución aquí");
            sb.AppendLine("```");
            sb.AppendLine();
            
            if (exerciseConfig.IncludeUnitTests)
            {
                sb.AppendLine("### 10. Pruebas Unitarias");
                sb.AppendLine("```csharp");
                sb.AppendLine("// Código de pruebas aquí");
                sb.AppendLine("```");
                sb.AppendLine();
            }
            
            if (exerciseConfig.IncludeExtensionChallenges)
            {
                sb.AppendLine("### 11. Desafíos de Extensión");
                sb.AppendLine("(Lista de 3-5 formas de extender el ejercicio)");
                sb.AppendLine();
            }
            
            sb.AppendLine("### 12. Conceptos Clave Explicados");
            sb.AppendLine("(Explicación de 2-3 conceptos importantes del ejercicio)");
            sb.AppendLine();
            sb.AppendLine("### 13. Errores Comunes y Consejos");
            sb.AppendLine("(Lista de errores típicos y cómo evitarlos)");
            
            // Agregar requerimientos personalizados del mentor
            if (mentorConfig.CustomRequirements.Any())
            {
                sb.AppendLine();
                sb.AppendLine("## Requerimientos Personalizados del Mentor:");
                foreach (var req in mentorConfig.CustomRequirements)
                {
                    sb.AppendLine($"- **{req.Key}**: {req.Value}");
                }
            }
            
            return sb.ToString();
        }

        private Dictionary<string, object> GeneratePromptParameters(ExerciseConfiguration exerciseConfig, MentorConfiguration mentorConfig)
        {
            return new Dictionary<string, object>
            {
                ["nivel"] = exerciseConfig.Level.ToString(),
                ["tema"] = exerciseConfig.Topic.ToString(),
                ["tipo"] = exerciseConfig.Type.ToString(),
                ["contexto"] = exerciseConfig.Context,
                ["duracion"] = exerciseConfig.EstimatedMinutes,
                ["incluir_tests"] = exerciseConfig.IncludeUnitTests,
                ["incluir_extensiones"] = exerciseConfig.IncludeExtensionChallenges,
                ["estilo_mentor"] = mentorConfig.TeachingStyle,
                ["dominio_preferido"] = mentorConfig.PreferredExampleDomain,
                ["ejemplos_mundo_real"] = mentorConfig.IncludeRealWorldExamples,
                ["considerar_rendimiento"] = mentorConfig.IncludePerformanceConsiderations
            };
        }

        private List<string> GenerateValidationCriteria(ExerciseConfiguration config)
        {
            var criteria = new List<string>
            {
                "El ejercicio debe ser apropiado para el nivel de habilidad especificado",
                "Debe incluir código de inicio funcional con TODOs claros",
                "La solución debe demostrar mejores prácticas de C#",
                "Los objetivos de aprendizaje deben ser específicos y medibles",
                "El enunciado del problema debe ser claro y sin ambigüedades"
            };

            if (config.IncludeUnitTests)
            {
                criteria.Add("Debe incluir pruebas unitarias comprehensivas usando xUnit");
            }

            if (config.IncludeExtensionChallenges)
            {
                criteria.Add("Los desafíos de extensión deben ser progresivos en dificultad");
            }

            return criteria;
        }

        #endregion

        #region Métodos Auxiliares

        private int EstimateTimeFromComplexity(int complexityScore, SkillLevel level)
        {
            var baseTime = level switch
            {
                SkillLevel.Beginner => 15,
                SkillLevel.Intermediate => 25,
                SkillLevel.Advanced => 40,
                _ => 25
            };

            return baseTime + (complexityScore * 5);
        }

        private string GenerateProblemStatement(ExampleSet example)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine(example.Description);
            sb.AppendLine();
            sb.AppendLine("Tu tarea es:");
            
            if (!string.IsNullOrEmpty(example.BeforeCode))
            {
                sb.AppendLine("1. Analizar el código proporcionado");
                sb.AppendLine("2. Identificar los problemas o áreas de mejora");
                sb.AppendLine("3. Implementar una solución que demuestre mejores prácticas");
            }
            else
            {
                sb.AppendLine("1. Implementar la funcionalidad requerida");
                sb.AppendLine("2. Seguir las mejores prácticas de C#");
                sb.AppendLine("3. Asegurar que el código sea mantenible y extensible");
            }
            
            sb.AppendLine();
            sb.AppendLine("Conceptos clave a aplicar:");
            foreach (var concept in example.KeyConcepts)
            {
                sb.AppendLine($"- {concept}");
            }
            
            return sb.ToString();
        }

        private List<string> GenerateSuccessCriteria(ExampleSet example)
        {
            var criteria = new List<string>
            {
                "El código compila sin errores",
                "Se implementan todos los requerimientos funcionales",
                "El código sigue las convenciones de nomenclatura de C#",
                "Se incluye manejo apropiado de errores"
            };

            // Agregar criterios específicos basados en conceptos clave
            foreach (var concept in example.KeyConcepts)
            {
                criteria.Add($"Se demuestra comprensión de: {concept}");
            }

            return criteria;
        }

        private string GenerateUnitTestsForExample(ExampleSet example)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("using System;");
            sb.AppendLine("using Xunit;");
            sb.AppendLine();
            sb.AppendLine($"namespace {example.Title.Replace(" ", "")}.Tests");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {example.Title.Replace(" ", "")}Tests");
            sb.AppendLine("    {");
            sb.AppendLine("        [Fact]");
            sb.AppendLine("        public void Test_BasicFunctionality()");
            sb.AppendLine("        {");
            sb.AppendLine("            // Arrange");
            sb.AppendLine("            // TODO: Configurar datos de prueba");
            sb.AppendLine();
            sb.AppendLine("            // Act");
            sb.AppendLine("            // TODO: Ejecutar funcionalidad a probar");
            sb.AppendLine();
            sb.AppendLine("            // Assert");
            sb.AppendLine("            // TODO: Verificar resultados esperados");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        [Theory]");
            sb.AppendLine("        [InlineData(/* TODO: Agregar casos de prueba */)]");
            sb.AppendLine("        public void Test_MultipleScenarios(/* parámetros */)");
            sb.AppendLine("        {");
            sb.AppendLine("            // TODO: Implementar pruebas paramétricas");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            
            return sb.ToString();
        }

        #endregion
    }
}