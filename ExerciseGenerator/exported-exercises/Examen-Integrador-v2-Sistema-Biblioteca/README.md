# Examen Integrador Final - Versión 2
## Sistema de Gestión de Biblioteca Digital

**Duración:** 60 minutos  
**Fecha:** _________________  
**Nombre:** _________________  

---

## Instrucciones Generales

- **Tiempo total:** 60 minutos
- **Modalidad:** Práctico en computadora
- **Herramientas permitidas:** Visual Studio, documentación oficial de Microsoft
- **Entrega:** Proyecto completo funcionando + comentarios explicativos

### Distribución del Tiempo Sugerida:
- **Análisis y diseño:** 10 minutos
- **Implementación core:** 35 minutos  
- **Testing y refinamiento:** 10 minutos
- **Documentación:** 5 minutos

---

## Caso de Estudio: BiblioTech - Sistema de Gestión Bibliotecaria

Eres el desarrollador principal de **BiblioTech**, un sistema moderno para gestionar operaciones de una biblioteca digital. El sistema debe manejar libros, usuarios, préstamos y generar reportes automatizados.

### Contexto del Negocio:
- La biblioteca maneja **múltiples tipos de libros** (físicos, digitales, audiolibros)
- Los **usuarios** tienen diferentes tipos de membresía (estudiante, docente, público general)
- El sistema debe **calcular multas** por retrasos de manera automática
- Se requieren **reportes estadísticos** para la administración

---

## Parte 1: Fundamentos y Estructuras de Control (15 puntos)

### 1.1 Validación de Datos (5 puntos)
Implementa un método que valide la información de un nuevo libro:

```csharp
public static bool ValidarLibro(string isbn, string titulo, int añoPublicacion, double precio)
{
    // TODO: Implementar validaciones
    // - ISBN: debe tener exactamente 13 dígitos
    // - Título: no puede estar vacío ni ser solo espacios
    // - Año: debe estar entre 1800 y año actual
    // - Precio: debe ser mayor a 0 y menor a 10000
    
    // Retorna true si todos los datos son válidos
}
```

### 1.2 Cálculo de Multas (5 puntos)
Crea un método que calcule la multa por días de retraso:

```csharp
public static decimal CalcularMulta(DateTime fechaPrestamo, DateTime fechaDevolucion, string tipoUsuario)
{
    // TODO: Implementar lógica de multas
    // Días permitidos por tipo:
    // - "estudiante": 7 días, multa $5 por día de retraso
    // - "docente": 14 días, multa $3 por día de retraso  
    // - "publico": 5 días, multa $10 por día de retraso
    
    // Si no hay retraso, retorna 0
    // Usar switch expression para tipos de usuario
}
```

### 1.3 Procesamiento de Lotes (5 puntos)
Implementa un método que procese múltiples devoluciones:

```csharp
public static void ProcesarDevoluciones(List<Prestamo> prestamos)
{
    // TODO: Iterar sobre los préstamos y:
    // - Marcar como devuelto (IsDevuelto = true)
    // - Calcular multa si corresponde
    // - Imprimir resumen por cada devolución
    // Usar foreach y estructuras condicionales apropiadas
}
```

---

## Parte 2: Métodos y Modularización (15 puntos)

### 2.1 Método con Parámetros Opcionales (5 puntos)
```csharp
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
}
```

### 2.2 Método con Parámetros de Salida (5 puntos)
```csharp
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
}
```

### 2.3 Método de Extensión (5 puntos)
```csharp
public static class LibroExtensions
{
    public static bool EsReciente(this Libro libro, int añosRecientes = 5)
    {
        // TODO: Determinar si el libro es reciente
        // - Comparar año de publicación con año actual
        // - Usar parámetro por defecto para definir "reciente"
    }
    
    public static string GenerarCodigoReferencia(this Libro libro)
    {
        // TODO: Generar código único
        // Formato: [PRIMERA_LETRA_CATEGORIA][AÑO][PRIMERAS_3_LETRAS_TITULO]
        // Ejemplo: "N2023PRO" para "Novela" "2023" "Programación"
    }
}
```

---

## Parte 3: Programación Orientada a Objetos (25 puntos)

### 3.1 Jerarquía de Clases (10 puntos)

```csharp
// Clase base abstracta
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
    }
    
    // TODO: Método abstracto que debe implementar cada tipo
    public abstract decimal CalcularCostoPrestamo(int diasPrestamo);
    
    // TODO: Método virtual que puede ser sobrescrito
    public virtual string ObtenerDescripcion()
    {
        return $"{Titulo} por {Autor} ({AñoPublicacion})";
    }
}

// TODO: Implementar clases derivadas
public class LibroFisico : Libro
{
    public string Ubicacion { get; set; }
    public bool RequiereMantenimiento { get; set; }
    
    // Constructor y métodos específicos
    // Costo: $2 por día base
}

public class LibroDigital : Libro  
{
    public string FormatoArchivo { get; set; }
    public double TamañoMB { get; set; }
    
    // Constructor y métodos específicos
    // Costo: $1 por día base
}

public class AudioLibro : Libro
{
    public double DuracionHoras { get; set; }
    public string Narrador { get; set; }
    
    // Constructor y métodos específicos  
    // Costo: $3 por día base
}
```

### 3.2 Clase de Gestión (10 puntos)

```csharp
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
    }
    
    public bool PuedeTomarPrestamo()
    {
        // TODO: Verificar si puede tomar préstamos
        // - No debe tener más de 3 libros prestados
        // - No debe tener multas mayores a $50
    }
    
    public void PagarMulta(decimal monto)
    {
        // TODO: Reducir multas pendientes
    }
}

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
    
    // TODO: Implementar constructor y métodos de negocio
}
```

### 3.3 Encapsulación y Propiedades (5 puntos)

```csharp
public class GestorBiblioteca
{
    private List<Libro> _libros;
    private List<Usuario> _usuarios;
    private List<Prestamo> _prestamosActivos;
    
    // TODO: Propiedades de solo lectura para exponer colecciones
    public IReadOnlyList<Libro> Libros => _libros.AsReadOnly();
    
    // TODO: Propiedad calculada
    public int LibrosDisponibles => // contar libros disponibles
    
    // TODO: Propiedad con validación
    public int MaximaRenovaciones { get; set; } = 2;
    
    public GestorBiblioteca()
    {
        // TODO: Inicializar colecciones
    }
}
```

---

## Parte 4: Colecciones y LINQ (20 puntos)

### 4.1 Consultas Básicas (8 puntos)

```csharp
public class ReportesBiblioteca
{
    private readonly GestorBiblioteca _gestor;
    
    public ReportesBiblioteca(GestorBiblioteca gestor)
    {
        _gestor = gestor;
    }
    
    // TODO: Libros más populares (más prestados)
    public List<(Libro libro, int vecesRestado)> LibrosMasPopulares(int top = 10)
    {
        // Usar LINQ para agrupar préstamos por libro
        // Ordenar por cantidad de préstamos descendente
        // Tomar los primeros 'top' elementos
    }
    
    // TODO: Usuarios con más multas
    public List<Usuario> UsuariosConMasMultas(decimal multaMinima = 20)
    {
        // Filtrar usuarios con multas >= multaMinima
        // Ordenar por multas descendente
    }
    
    // TODO: Libros por categoría y año
    public Dictionary<string, List<Libro>> LibrosPorCategoria()
    {
        // Agrupar libros por categoría usando LINQ
        // Retornar diccionario con categoría como clave
    }
}
```

### 4.2 Consultas Avanzadas (7 puntos)

```csharp
// TODO: Estadísticas complejas
public class EstadisticasBiblioteca
{
    public static ReporteEstadistico GenerarReporte(List<Prestamo> prestamos)
    {
        var reporte = new ReporteEstadistico();
        
        // TODO: Usar LINQ para calcular:
        reporte.TotalPrestamos = // contar total
        reporte.PromedioMultaPorUsuario = // promedio de multas
        reporte.CategoriasMasPopulares = // top 5 categorías más prestadas
        reporte.MesConMasPrestamos = // mes con mayor actividad
        reporte.PorcentajeDevolucionesATiempo = // % sin multas
        
        return reporte;
    }
}

public class ReporteEstadistico
{
    public int TotalPrestamos { get; set; }
    public decimal PromedioMultaPorUsuario { get; set; }
    public List<string> CategoriasMasPopulares { get; set; }
    public string MesConMasPrestamos { get; set; }
    public double PorcentajeDevolucionesATiempo { get; set; }
}
```

### 4.3 Operaciones con Colecciones (5 puntos)

```csharp
public class OperacionesBiblioteca
{
    // TODO: Migrar libros entre ubicaciones
    public static void MigrarLibros(Dictionary<string, List<LibroFisico>> estanterias,
                                   string ubicacionOrigen,
                                   string ubicacionDestino,
                                   Func<LibroFisico, bool> criterioSeleccion)
    {
        // Usar LINQ para filtrar libros según criterio
        // Mover de una estantería a otra
        // Actualizar ubicación de cada libro movido
    }
    
    // TODO: Buscar libros con múltiples criterios
    public static List<Libro> BusquedaAvanzada(List<Libro> biblioteca,
                                              string[] autores = null,
                                              string[] categorias = null,
                                              int? añoInicio = null,
                                              int? añoFin = null)
    {
        // Usar LINQ con múltiples filtros opcionales
        // Filtrar por autores si se especifica
        // Filtrar por categorías si se especifica
        // Filtrar por rango de años si se especifica
    }
}
```

---

## Parte 5: Integración y Casos Reales (15 puntos)

### 5.1 Flujo Completo de Préstamo (8 puntos)

```csharp
public class ServicioPrestamo
{
    private readonly GestorBiblioteca _gestor;
    
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
```

### 5.2 Sistema de Notificaciones (7 puntos)

```csharp
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
        
        return notificaciones;
    }
    
    public static void EnviarNotificaciones(List<Notificacion> notificaciones)
    {
        // TODO: Agrupar por usuario
        // Enviar resumen consolidado por email (simulado)
        // Mostrar estadísticas de envío
    }
}

public class Notificacion
{
    public Usuario Usuario { get; set; }
    public string Tipo { get; set; } // "recordatorio", "urgente", "multa"
    public string Mensaje { get; set; }
    public DateTime FechaCreacion { get; set; }
}
```

---

## Criterios de Evaluación

### Distribución de Puntos (100 puntos total):

| Área | Puntos | Criterios |
|------|---------|-----------|
| **Parte 1: Fundamentos** | 15 | Validaciones, estructuras de control, manejo de tipos |
| **Parte 2: Métodos** | 15 | Parámetros opcionales, out, extensiones, modularización |
| **Parte 3: OOP** | 25 | Herencia, polimorfismo, encapsulación, diseño de clases |
| **Parte 4: Colecciones** | 20 | LINQ, operaciones complejas, manejo eficiente |
| **Parte 5: Integración** | 15 | Flujos completos, casos reales, arquitectura |
| **Calidad de Código** | 10 | Nombres descriptivos, comentarios, estructura limpia |

### Rúbrica de Calificación:

- **90-100 puntos:** Excelente - Dominio completo de todos los conceptos
- **80-89 puntos:** Muy Bueno - Dominio sólido con pequeños errores
- **70-79 puntos:** Bueno - Conceptos básicos correctos, algunas deficiencias
- **60-69 puntos:** Suficiente - Implementación parcial pero funcional
- **Menos de 60:** Insuficiente - Requiere refuerzo en conceptos fundamentales

### Aspectos Clave a Evaluar:

1. **Corrección Técnica:** ¿El código compila y ejecuta correctamente?
2. **Aplicación de Conceptos:** ¿Se utilizan apropiadamente las características de C#?
3. **Diseño de Solución:** ¿La arquitectura es lógica y mantenible?
4. **Calidad del Código:** ¿Es legible, bien estructurado y comentado?
5. **Completitud:** ¿Se implementaron todos los requisitos solicitados?

---

## Datos de Prueba Sugeridos

```csharp
// Para facilitar las pruebas, utiliza estos datos de ejemplo:

var libros = new List<Libro>
{
    new LibroFisico("978-0134685991", "Clean Code", "Robert Martin", 2008, "Programación"),
    new LibroDigital("978-0135957059", "The Pragmatic Programmer", "Hunt & Thomas", 2019, "Programación"),
    new AudioLibro("978-1617294136", "Soft Skills", "John Sonmez", 2014, "Carrera")
};

var usuarios = new List<Usuario>
{
    new Usuario(1, "Ana García", "ana@email.com", "estudiante"),
    new Usuario(2, "Carlos López", "carlos@email.com", "docente"),
    new Usuario(3, "María Rodríguez", "maria@email.com", "publico")
};
```

---

## Entrega Final

### Archivos a Entregar:
1. **Código fuente completo** con todas las clases implementadas
2. **Método Main** con demostración de funcionalidades clave
3. **Comentarios explicativos** en secciones complejas
4. **README breve** explicando decisiones de diseño importantes

### Formato de Entrega:
- Proyecto de Visual Studio compilable
- Código bien formateado y comentado
- Demostración de funcionamiento en el método Main

**¡Éxito en tu examen!**