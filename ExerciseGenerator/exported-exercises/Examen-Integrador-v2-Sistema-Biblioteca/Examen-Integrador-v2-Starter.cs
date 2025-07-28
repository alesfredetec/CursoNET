using System;
using System.Collections.Generic;
using System.Linq;

namespace BiblioTechSystem
{
    /// <summary>
    /// EXAMEN INTEGRADOR FINAL - VERSIÓN 2
    /// Sistema de Gestión de Biblioteca Digital - BiblioTech
    /// Tiempo: 60 minutos
    /// 
    /// INSTRUCCIONES:
    /// 1. Implementa todos los métodos marcados con TODO
    /// 2. No modifiques las firmas de métodos existentes
    /// 3. Puedes agregar métodos auxiliares si es necesario
    /// 4. Asegúrate de que todo compile sin errores
    /// 5. Demuestra funcionalidad en el método Main
    /// </summary>

    #region Parte 1: Fundamentos y Estructuras de Control (15 puntos)

    public static class ValidadorBiblioteca
    {
        /// <summary>
        /// 1.1 Validación de Datos (5 puntos)
        /// Valida la información de un nuevo libro
        /// </summary>
        public static bool ValidarLibro(string isbn, string titulo, int añoPublicacion, double precio)
        {
            // TODO: Implementar validaciones
            // - ISBN: debe tener exactamente 13 dígitos
            // - Título: no puede estar vacío ni ser solo espacios
            // - Año: debe estar entre 1800 y año actual
            // - Precio: debe ser mayor a 0 y menor a 10000
            
            throw new NotImplementedException("Implementar validación de libro");
        }

        /// <summary>
        /// 1.2 Cálculo de Multas (5 puntos)
        /// Calcula la multa por días de retraso según tipo de usuario
        /// </summary>
        public static decimal CalcularMulta(DateTime fechaPrestamo, DateTime fechaDevolucion, string tipoUsuario)
        {
            // TODO: Implementar lógica de multas
            // Días permitidos por tipo:
            // - "estudiante": 7 días, multa $5 por día de retraso
            // - "docente": 14 días, multa $3 por día de retraso  
            // - "publico": 5 días, multa $10 por día de retraso
            
            // Si no hay retraso, retorna 0
            // Usar switch expression para tipos de usuario
            
            throw new NotImplementedException("Implementar cálculo de multa");
        }

        /// <summary>
        /// 1.3 Procesamiento de Lotes (5 puntos)
        /// Procesa múltiples devoluciones de libros
        /// </summary>
        public static void ProcesarDevoluciones(List<Prestamo> prestamos)
        {
            // TODO: Iterar sobre los préstamos y:
            // - Marcar como devuelto (IsDevuelto = true)
            // - Calcular multa si corresponde
            // - Imprimir resumen por cada devolución
            // Usar foreach y estructuras condicionales apropiadas
            
            throw new NotImplementedException("Implementar procesamiento de devoluciones");
        }
    }

    #endregion

    #region Parte 2: Métodos y Modularización (15 puntos)

    public static class BuscadorLibros
    {
        /// <summary>
        /// 2.1 Método con Parámetros Opcionales (5 puntos)
        /// Búsqueda flexible de libros con múltiples criterios
        /// </summary>
        public static List<Libro> BuscarLibros(List<Libro> biblioteca, 
                                              string termino = "", 
                                              string categoria = "todas", 
                                              int añoMinimo = 0)
        {
            // TODO: Implementar búsqueda flexible
            // - Si termino está vacío, buscar por categoría y año
            // - Si categoría es "todas", ignorar filtro de categoría
            // - Si añoMinimo es 0, ignorar filtro de año
            // Usar parámetros opcionales y lógica condicional
            
            throw new NotImplementedException("Implementar búsqueda de libros");
        }

        /// <summary>
        /// 2.2 Método con Parámetros de Salida (5 puntos)
        /// Intenta renovar un préstamo con información detallada de resultado
        /// </summary>
        public static bool IntentarRenovarPrestamo(Prestamo prestamo, 
                                                  out DateTime nuevaFechaVencimiento, 
                                                  out string mensaje)
        {
            // TODO: Implementar lógica de renovación
            // - Si el usuario tiene multas pendientes: fallar renovación
            // - Si el libro está reservado por otro usuario: fallar renovación
            // - Si ya se renovó 2 veces: fallar renovación
            // - Si todo OK: renovar por 7 días adicionales
            // Usar parámetros out para retornar información adicional
            
            nuevaFechaVencimiento = DateTime.MinValue;
            mensaje = "";
            throw new NotImplementedException("Implementar renovación de préstamo");
        }
    }

    /// <summary>
    /// 2.3 Método de Extensión (5 puntos)
    /// Extensiones para la clase Libro
    /// </summary>
    public static class LibroExtensions
    {
        public static bool EsReciente(this Libro libro, int añosRecientes = 5)
        {
            // TODO: Determinar si el libro es reciente
            // - Comparar año de publicación con año actual  
            // - Usar parámetro por defecto para definir "reciente"
            
            throw new NotImplementedException("Implementar verificación de libro reciente");
        }
        
        public static string GenerarCodigoReferencia(this Libro libro)
        {
            // TODO: Generar código único
            // Formato: [PRIMERA_LETRA_CATEGORIA][AÑO][PRIMERAS_3_LETRAS_TITULO]
            // Ejemplo: "N2023PRO" para "Novela" "2023" "Programación"
            
            throw new NotImplementedException("Implementar generación de código de referencia");
        }
    }

    #endregion

    #region Parte 3: Programación Orientada a Objetos (25 puntos)

    /// <summary>
    /// 3.1 Jerarquía de Clases - Clase Base Abstracta (10 puntos)
    /// </summary>
    public abstract class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AñoPublicacion { get; set; }
        public string Categoria { get; set; }
        public bool EstaDisponible { get; set; }
        
        protected Libro(string isbn, string titulo, string autor, int año, string categoria)
        {
            // TODO: Implementar constructor con validaciones básicas
            // Inicializar propiedades y validar datos
            
            throw new NotImplementedException("Implementar constructor de Libro");
        }
        
        // TODO: Método abstracto que debe implementar cada tipo
        public abstract decimal CalcularCostoPrestamo(int diasPrestamo);
        
        // TODO: Método virtual que puede ser sobrescrito
        public virtual string ObtenerDescripcion()
        {
            return $"{Titulo} por {Autor} ({AñoPublicacion})";
        }
    }

    /// <summary>
    /// Libro Físico - Heredero de Libro
    /// </summary>
    public class LibroFisico : Libro
    {
        public string Ubicacion { get; set; }
        public bool RequiereMantenimiento { get; set; }
        
        public LibroFisico(string isbn, string titulo, string autor, int año, string categoria, string ubicacion)
            : base(isbn, titulo, autor, año, categoria)
        {
            // TODO: Implementar constructor específico
            // Inicializar propiedades específicas de LibroFisico
            
            throw new NotImplementedException("Implementar constructor de LibroFisico");
        }
        
        public override decimal CalcularCostoPrestamo(int diasPrestamo)
        {
            // TODO: Implementar cálculo específico
            // Costo: $2 por día base
            // Si requiere mantenimiento: +50% adicional
            
            throw new NotImplementedException("Implementar cálculo de costo para LibroFisico");
        }
    }

    /// <summary>
    /// Libro Digital - Heredero de Libro
    /// </summary>
    public class LibroDigital : Libro  
    {
        public string FormatoArchivo { get; set; }
        public double TamañoMB { get; set; }
        
        public LibroDigital(string isbn, string titulo, string autor, int año, string categoria, 
                           string formato, double tamaño)
            : base(isbn, titulo, autor, año, categoria)
        {
            // TODO: Implementar constructor específico
            
            throw new NotImplementedException("Implementar constructor de LibroDigital");
        }
        
        public override decimal CalcularCostoPrestamo(int diasPrestamo)
        {
            // TODO: Implementar cálculo específico
            // Costo: $1 por día base
            // Si tamaño > 100MB: +$0.5 por día adicional
            
            throw new NotImplementedException("Implementar cálculo de costo para LibroDigital");
        }
    }

    /// <summary>
    /// AudioLibro - Heredero de Libro
    /// </summary>
    public class AudioLibro : Libro
    {
        public double DuracionHoras { get; set; }
        public string Narrador { get; set; }
        
        public AudioLibro(string isbn, string titulo, string autor, int año, string categoria,
                         double duracion, string narrador)
            : base(isbn, titulo, autor, año, categoria)
        {
            // TODO: Implementar constructor específico
            
            throw new NotImplementedException("Implementar constructor de AudioLibro");
        }
        
        public override decimal CalcularCostoPrestamo(int diasPrestamo)
        {
            // TODO: Implementar cálculo específico  
            // Costo: $3 por día base
            // Si duración > 10 horas: costo fijo de $25 sin importar días
            
            throw new NotImplementedException("Implementar cálculo de costo para AudioLibro");
        }
    }

    /// <summary>
    /// 3.2 Clase de Gestión - Usuario (10 puntos)
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string TipoMembresia { get; set; }
        public decimal MultasPendientes { get; set; }
        public List<Prestamo> HistorialPrestamos { get; set; }
        
        public Usuario(int id, string nombre, string email, string tipoMembresia)
        {
            // TODO: Implementar constructor con inicialización
            // Inicializar todas las propiedades apropiadamente
            
            throw new NotImplementedException("Implementar constructor de Usuario");
        }
        
        public bool PuedeTomarPrestamo()
        {
            // TODO: Verificar si puede tomar préstamos
            // - No debe tener más de 3 libros prestados actualmente
            // - No debe tener multas mayores a $50
            
            throw new NotImplementedException("Implementar verificación de préstamo");
        }
        
        public void PagarMulta(decimal monto)
        {
            // TODO: Reducir multas pendientes
            // Validar que el monto no sea mayor a las multas pendientes
            
            throw new NotImplementedException("Implementar pago de multa");
        }
    }

    /// <summary>
    /// Clase Prestamo - Maneja información de préstamos
    /// </summary>
    public class Prestamo
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Libro Libro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool IsDevuelto { get; set; }
        public decimal MultaCalculada { get; set; }
        public int VecesRenovado { get; set; }
        
        public Prestamo(int id, Usuario usuario, Libro libro)
        {
            // TODO: Implementar constructor
            // Calcular fecha de vencimiento según tipo de usuario
            // Inicializar valores por defecto
            
            throw new NotImplementedException("Implementar constructor de Prestamo");
        }
    }

    /// <summary>
    /// 3.3 Encapsulación y Propiedades - Gestor Principal (5 puntos)
    /// </summary>
    public class GestorBiblioteca
    {
        private List<Libro> _libros;
        private List<Usuario> _usuarios;
        private List<Prestamo> _prestamosActivos;
        
        // TODO: Propiedades de solo lectura para exponer colecciones
        public IReadOnlyList<Libro> Libros => _libros?.AsReadOnly();
        public IReadOnlyList<Usuario> Usuarios => _usuarios?.AsReadOnly();
        public IReadOnlyList<Prestamo> PrestamosActivos => _prestamosActivos?.AsReadOnly();
        
        // TODO: Propiedad calculada
        public int LibrosDisponibles 
        { 
            get 
            { 
                // TODO: Contar libros disponibles
                throw new NotImplementedException("Implementar conteo de libros disponibles");
            } 
        }
        
        // TODO: Propiedad con validación
        private int _maximaRenovaciones = 2;
        public int MaximaRenovaciones 
        { 
            get => _maximaRenovaciones;
            set 
            { 
                // TODO: Validar que sea mayor a 0 y menor a 5
                throw new NotImplementedException("Implementar validación de máxima renovaciones");
            }
        }
        
        public GestorBiblioteca()
        {
            // TODO: Inicializar colecciones
            throw new NotImplementedException("Implementar constructor de GestorBiblioteca");
        }
        
        public void AgregarLibro(Libro libro)
        {
            // TODO: Agregar libro con validaciones
            throw new NotImplementedException("Implementar agregar libro");
        }
        
        public void RegistrarUsuario(Usuario usuario)
        {
            // TODO: Registrar usuario con validaciones
            throw new NotImplementedException("Implementar registrar usuario");
        }
    }

    #endregion

    #region Parte 4: Colecciones y LINQ (20 puntos)

    /// <summary>
    /// 4.1 Consultas Básicas (8 puntos)
    /// </summary>
    public class ReportesBiblioteca
    {
        private readonly GestorBiblioteca _gestor;
        
        public ReportesBiblioteca(GestorBiblioteca gestor)
        {
            _gestor = gestor ?? throw new ArgumentNullException(nameof(gestor));
        }
        
        // TODO: Libros más populares (más prestados)
        public List<(Libro libro, int vecesPrestado)> LibrosMasPopulares(int top = 10)
        {
            // TODO: Usar LINQ para agrupar préstamos por libro
            // Ordenar por cantidad de préstamos descendente
            // Tomar los primeros 'top' elementos
            
            throw new NotImplementedException("Implementar libros más populares");
        }
        
        // TODO: Usuarios con más multas
        public List<Usuario> UsuariosConMasMultas(decimal multaMinima = 20)
        {
            // TODO: Filtrar usuarios con multas >= multaMinima
            // Ordenar por multas descendente
            
            throw new NotImplementedException("Implementar usuarios con más multas");
        }
        
        // TODO: Libros por categoría y año
        public Dictionary<string, List<Libro>> LibrosPorCategoria()
        {
            // TODO: Agrupar libros por categoría usando LINQ
            // Retornar diccionario con categoría como clave
            
            throw new NotImplementedException("Implementar agrupación por categoría");
        }
    }

    /// <summary>
    /// 4.2 Consultas Avanzadas (7 puntos)
    /// </summary>
    public class EstadisticasBiblioteca
    {
        public static ReporteEstadistico GenerarReporte(List<Prestamo> prestamos)
        {
            var reporte = new ReporteEstadistico();
            
            // TODO: Usar LINQ para calcular:
            // reporte.TotalPrestamos = contar total
            // reporte.PromedioMultaPorUsuario = promedio de multas por usuario
            // reporte.CategoriasMasPopulares = top 5 categorías más prestadas
            // reporte.MesConMasPrestamos = mes con mayor actividad
            // reporte.PorcentajeDevolucionesATiempo = % sin multas
            
            throw new NotImplementedException("Implementar generación de reporte estadístico");
        }
    }

    public class ReporteEstadistico
    {
        public int TotalPrestamos { get; set; }
        public decimal PromedioMultaPorUsuario { get; set; }
        public List<string> CategoriasMasPopulares { get; set; }
        public string MesConMasPrestamos { get; set; }
        public double PorcentajeDevolucionesATiempo { get; set; }
        
        public ReporteEstadistico()
        {
            CategoriasMasPopulares = new List<string>();
        }
    }

    /// <summary>
    /// 4.3 Operaciones con Colecciones (5 puntos)
    /// </summary>
    public class OperacionesBiblioteca
    {
        // TODO: Migrar libros entre ubicaciones
        public static void MigrarLibros(Dictionary<string, List<LibroFisico>> estanterias,
                                       string ubicacionOrigen,
                                       string ubicacionDestino,
                                       Func<LibroFisico, bool> criterioSeleccion)
        {
            // TODO: Usar LINQ para filtrar libros según criterio
            // Mover de una estantería a otra
            // Actualizar ubicación de cada libro movido
            
            throw new NotImplementedException("Implementar migración de libros");
        }
        
        // TODO: Buscar libros con múltiples criterios
        public static List<Libro> BusquedaAvanzada(List<Libro> biblioteca,
                                                  string[] autores = null,
                                                  string[] categorias = null,
                                                  int? añoInicio = null,
                                                  int? añoFin = null)
        {
            // TODO: Usar LINQ con múltiples filtros opcionales
            // Filtrar por autores si se especifica
            // Filtrar por categorías si se especifica
            // Filtrar por rango de años si se especifica
            
            throw new NotImplementedException("Implementar búsqueda avanzada");
        }
    }

    #endregion

    #region Parte 5: Integración y Casos Reales (15 puntos)

    /// <summary>
    /// 5.1 Flujo Completo de Préstamo (8 puntos)
    /// </summary>
    public class ServicioPrestamo
    {
        private readonly GestorBiblioteca _gestor;
        
        public ServicioPrestamo(GestorBiblioteca gestor)
        {
            _gestor = gestor ?? throw new ArgumentNullException(nameof(gestor));
        }
        
        public ResultadoPrestamo ProcesarPrestamoCompleto(int usuarioId, string isbn)
        {
            // TODO: Implementar flujo completo:
            // 1. Validar que el usuario existe y puede tomar préstamos
            // 2. Validar que el libro existe y está disponible
            // 3. Crear el préstamo con fechas apropiadas
            // 4. Marcar libro como no disponible
            // 5. Agregar préstamo al historial del usuario
            // 6. Retornar resultado con toda la información
            
            // Usar manejo de errores y validaciones múltiples
            
            throw new NotImplementedException("Implementar procesamiento completo de préstamo");
        }
    }

    public class ResultadoPrestamo
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public Prestamo PrestamoCreado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal CostoEstimado { get; set; }
    }

    /// <summary>
    /// 5.2 Sistema de Notificaciones (7 puntos)
    /// </summary>
    public class SistemaNotificaciones
    {
        public static List<Notificacion> GenerarNotificacionesVencimiento(List<Prestamo> prestamosActivos)
        {
            var notificaciones = new List<Notificacion>();
            
            // TODO: Para cada préstamo activo:
            // - Si vence en 2 días: notificación de recordatorio
            // - Si vence hoy: notificación urgente
            // - Si está vencido: notificación de multa
            
            // Usar LINQ para filtrar y transformar datos
            // Aplicar diferentes plantillas según el tipo
            
            throw new NotImplementedException("Implementar generación de notificaciones");
        }
        
        public static void EnviarNotificaciones(List<Notificacion> notificaciones)
        {
            // TODO: Agrupar por usuario
            // Enviar resumen consolidado por email (simulado)
            // Mostrar estadísticas de envío
            
            throw new NotImplementedException("Implementar envío de notificaciones");
        }
    }

    public class Notificacion
    {
        public Usuario Usuario { get; set; }
        public string Tipo { get; set; } // "recordatorio", "urgente", "multa"
        public string Mensaje { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        public Notificacion()
        {
            FechaCreacion = DateTime.Now;
        }
    }

    #endregion

    /// <summary>
    /// MÉTODO MAIN - DEMOSTRACIÓN DE FUNCIONALIDADES
    /// Implementa aquí código para demostrar que tu sistema funciona
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA BIBLIOTECH - DEMOSTRACIÓN ===");
            Console.WriteLine();
            
            try
            {
                // TODO: Crear instancia del gestor
                var gestor = new GestorBiblioteca();
                
                // TODO: Crear datos de prueba
                CrearDatosPrueba(gestor);
                
                // TODO: Demostrar funcionalidades principales
                DemostrarValidaciones();
                DemostrarBusquedas(gestor);
                DemostrarPrestamos(gestor);
                DemostrarReportes(gestor);
                
                Console.WriteLine("\n=== DEMOSTRACIÓN COMPLETADA EXITOSAMENTE ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la demostración: {ex.Message}");
            }
            
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        static void CrearDatosPrueba(GestorBiblioteca gestor)
        {
            // TODO: Crear libros de ejemplo
            Console.WriteLine("Creando datos de prueba...");
            
            // Ejemplo de cómo agregar libros (implementar según tu código)
            /*
            var libro1 = new LibroFisico("9780134685991", "Clean Code", "Robert Martin", 2008, "Programación", "A1-B2");
            var libro2 = new LibroDigital("9780135957059", "The Pragmatic Programmer", "Hunt & Thomas", 2019, "Programación", "PDF", 15.5);
            var audioLibro = new AudioLibro("9781617294136", "Soft Skills", "John Sonmez", 2014, "Carrera", 8.5, "John Sonmez");
            
            gestor.AgregarLibro(libro1);
            gestor.AgregarLibro(libro2);
            gestor.AgregarLibro(audioLibro);
            
            // Crear usuarios
            var usuario1 = new Usuario(1, "Ana García", "ana@email.com", "estudiante");
            var usuario2 = new Usuario(2, "Carlos López", "carlos@email.com", "docente");
            
            gestor.RegistrarUsuario(usuario1);
            gestor.RegistrarUsuario(usuario2);
            */
            
            Console.WriteLine("Datos de prueba creados.");
        }
        
        static void DemostrarValidaciones()
        {
            Console.WriteLine("\n--- DEMOSTRANDO VALIDACIONES ---");
            
            // TODO: Demostrar ValidarLibro
            // TODO: Demostrar CalcularMulta
            // TODO: Mostrar resultados de validaciones
        }
        
        static void DemostrarBusquedas(GestorBiblioteca gestor)
        {
            Console.WriteLine("\n--- DEMOSTRANDO BÚSQUEDAS ---");
            
            // TODO: Demostrar BuscarLibros con diferentes parámetros
            // TODO: Demostrar métodos de extensión
            // TODO: Mostrar resultados
        }
        
        static void DemostrarPrestamos(GestorBiblioteca gestor)
        {
            Console.WriteLine("\n--- DEMOSTRANDO PRÉSTAMOS ---");
            
            // TODO: Demostrar creación de préstamos
            // TODO: Demostrar renovaciones
            // TODO: Demostrar devoluciones
        }
        
        static void DemostrarReportes(GestorBiblioteca gestor)
        {
            Console.WriteLine("\n--- DEMOSTRANDO REPORTES ---");
            
            // TODO: Generar y mostrar reportes
            // TODO: Mostrar estadísticas
            // TODO: Demostrar notificaciones
        }
    }
}