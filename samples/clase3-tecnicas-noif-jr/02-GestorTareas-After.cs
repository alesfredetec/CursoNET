using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJunior
{
    /// <summary>
    /// SOLUCIÓN: Gestor de tareas usando State Pattern simplificado
    /// 
    /// MEJORAS:
    /// ✅ Eliminación de if-else anidados para manejo de estados
    /// ✅ Cada estado tiene su propio comportamiento encapsulado
    /// ✅ Transiciones de estado claras y centralizadas
    /// ✅ Fácil agregar nuevos estados sin modificar código existente
    /// ✅ Reglas de negocio claramente definidas por estado
    /// </summary>
    public class GestorTareasMejorado
    {
        // Lista de tareas
        private readonly List<TareaMejorada> _tareas = new List<TareaMejorada>();
        
        // Contador para generar IDs únicos
        private int _nextId = 1;
        
        /// <summary>
        /// Agrega una nueva tarea
        /// </summary>
        public int CrearTarea(string titulo, string descripcion)
        {
            // Crear tarea con estado inicial "Pendiente"
            var tarea = new TareaMejorada(_nextId++, titulo, descripcion);
            _tareas.Add(tarea);
            Console.WriteLine($"Tarea #{tarea.Id} creada: {titulo}");
            return tarea.Id;
        }
        
        /// <summary>
        /// Obtiene la lista de todas las tareas
        /// </summary>
        public List<TareaMejorada> ObtenerTareas()
        {
            return _tareas;
        }
        
        /// <summary>
        /// Ejecuta una acción sobre una tarea
        /// </summary>
        public bool EjecutarAccion(int id, string accion)
        {
            var tarea = _tareas.Find(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"Error: No se encontró la tarea con ID {id}");
                return false;
            }
            
            // La acción se delega al estado actual de la tarea
            return tarea.EjecutarAccion(accion);
        }
        
        /// <summary>
        /// Muestra los detalles de una tarea
        /// </summary>
        public void MostrarDetalleTarea(int id)
        {
            var tarea = _tareas.Find(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"Error: No se encontró la tarea con ID {id}");
                return;
            }
            
            // El estado actual se encarga de mostrar los detalles
            tarea.MostrarDetalles();
        }
        
        /// <summary>
        /// Obtiene las acciones disponibles según el estado de la tarea
        /// </summary>
        public List<string> ObtenerAccionesDisponibles(int id)
        {
            var tarea = _tareas.Find(t => t.Id == id);
            if (tarea == null)
            {
                return new List<string>();
            }
            
            // El estado actual determina qué acciones están disponibles
            return tarea.ObtenerAccionesDisponibles();
        }
    }
    
    /// <summary>
    /// Clase que representa una tarea con estado
    /// </summary>
    public class TareaMejorada
    {
        public int Id { get; }
        public string Titulo { get; }
        public string Descripcion { get; }
        
        // PASO 1: Referencia al estado actual de la tarea
        public EstadoTarea EstadoActual { get; private set; }
        
        // Fechas de seguimiento
        public DateTime FechaCreacion { get; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        
        // Historia de transiciones para auditoría
        public List<TransicionEstado> HistorialTransiciones { get; } = new List<TransicionEstado>();
        
        public TareaMejorada(int id, string titulo, string descripcion)
        {
            Id = id;
            Titulo = titulo;
            Descripcion = descripcion;
            FechaCreacion = DateTime.Now;
            
            // PASO 2: Inicializamos con estado "Pendiente"
            CambiarEstado(new EstadoPendiente(this));
        }
        
        /// <summary>
        /// PASO 3: Método para cambiar de un estado a otro
        /// </summary>
        public void CambiarEstado(EstadoTarea nuevoEstado)
        {
            string estadoAnterior = EstadoActual?.ObtenerNombre() ?? "Ninguno";
            EstadoActual = nuevoEstado;
            
            // Registrar la transición para auditoría
            HistorialTransiciones.Add(new TransicionEstado(
                estadoAnterior,
                nuevoEstado.ObtenerNombre(),
                DateTime.Now
            ));
        }
        
        /// <summary>
        /// Ejecuta una acción - delegada al estado actual
        /// </summary>
        public bool EjecutarAccion(string accion)
        {
            return EstadoActual.EjecutarAccion(accion);
        }
        
        /// <summary>
        /// Obtiene las acciones disponibles - delegada al estado actual
        /// </summary>
        public List<string> ObtenerAccionesDisponibles()
        {
            return EstadoActual.ObtenerAccionesDisponibles();
        }
        
        /// <summary>
        /// Muestra los detalles - delegada al estado actual
        /// </summary>
        public void MostrarDetalles()
        {
            Console.WriteLine($"=== Tarea #{Id}: {Titulo} ===");
            Console.WriteLine($"Descripción: {Descripcion}");
            Console.WriteLine($"Estado: {EstadoActual.ObtenerNombre()}");
            Console.WriteLine($"Creada: {FechaCreacion}");
            
            // El estado actual se encarga de mostrar información adicional
            EstadoActual.MostrarDetallesAdicionales();
        }
    }
    
    /// <summary>
    /// PASO 4: Clase para registrar transiciones de estado
    /// </summary>
    public class TransicionEstado
    {
        public string EstadoAnterior { get; }
        public string EstadoNuevo { get; }
        public DateTime FechaTransicion { get; }
        
        public TransicionEstado(string estadoAnterior, string estadoNuevo, DateTime fechaTransicion)
        {
            EstadoAnterior = estadoAnterior;
            EstadoNuevo = estadoNuevo;
            FechaTransicion = fechaTransicion;
        }
    }
    
    /// <summary>
    /// PASO 5: Clase base abstracta para todos los estados
    /// </summary>
    public abstract class EstadoTarea
    {
        // Referencia a la tarea que contiene este estado
        protected readonly TareaMejorada Tarea;
        
        protected EstadoTarea(TareaMejorada tarea)
        {
            Tarea = tarea;
        }
        
        // Métodos que todos los estados deben implementar
        public abstract string ObtenerNombre();
        public abstract List<string> ObtenerAccionesDisponibles();
        public abstract bool EjecutarAccion(string accion);
        public abstract void MostrarDetallesAdicionales();
    }
    
    /// <summary>
    /// PASO 6: Implementación del estado "Pendiente"
    /// </summary>
    public class EstadoPendiente : EstadoTarea
    {
        public EstadoPendiente(TareaMejorada tarea) : base(tarea) {}
        
        public override string ObtenerNombre() => "Pendiente";
        
        public override List<string> ObtenerAccionesDisponibles()
        {
            return new List<string> { "Iniciar", "Cancelar" };
        }
        
        public override bool EjecutarAccion(string accion)
        {
            switch (accion)
            {
                case "Iniciar":
                    Tarea.FechaInicio = DateTime.Now;
                    Tarea.CambiarEstado(new EstadoEnProceso(Tarea));
                    Console.WriteLine($"Tarea #{Tarea.Id} iniciada: {Tarea.Titulo}");
                    return true;
                    
                case "Cancelar":
                    Tarea.FechaCancelacion = DateTime.Now;
                    Tarea.CambiarEstado(new EstadoCancelada(Tarea));
                    Console.WriteLine($"Tarea #{Tarea.Id} cancelada: {Tarea.Titulo}");
                    return true;
                    
                default:
                    Console.WriteLine($"Error: Acción '{accion}' no permitida en estado Pendiente");
                    return false;
            }
        }
        
        public override void MostrarDetallesAdicionales()
        {
            // En estado pendiente no hay detalles adicionales
        }
    }
    
    /// <summary>
    /// PASO 7: Implementación del estado "EnProceso"
    /// </summary>
    public class EstadoEnProceso : EstadoTarea
    {
        public EstadoEnProceso(TareaMejorada tarea) : base(tarea) {}
        
        public override string ObtenerNombre() => "EnProceso";
        
        public override List<string> ObtenerAccionesDisponibles()
        {
            return new List<string> { "Completar", "Cancelar" };
        }
        
        public override bool EjecutarAccion(string accion)
        {
            switch (accion)
            {
                case "Completar":
                    Tarea.FechaFinalizacion = DateTime.Now;
                    Tarea.CambiarEstado(new EstadoCompletada(Tarea));
                    Console.WriteLine($"Tarea #{Tarea.Id} completada: {Tarea.Titulo}");
                    return true;
                    
                case "Cancelar":
                    Tarea.FechaCancelacion = DateTime.Now;
                    Tarea.CambiarEstado(new EstadoCancelada(Tarea));
                    Console.WriteLine($"Tarea #{Tarea.Id} cancelada: {Tarea.Titulo}");
                    return true;
                    
                default:
                    Console.WriteLine($"Error: Acción '{accion}' no permitida en estado EnProceso");
                    return false;
            }
        }
        
        public override void MostrarDetallesAdicionales()
        {
            Console.WriteLine($"Fecha inicio: {Tarea.FechaInicio}");
            TimeSpan tiempoTranscurrido = DateTime.Now - Tarea.FechaInicio.Value;
            Console.WriteLine($"Tiempo transcurrido: {tiempoTranscurrido.TotalHours:F1} horas");
        }
    }
    
    /// <summary>
    /// PASO 8: Implementación del estado "Completada"
    /// </summary>
    public class EstadoCompletada : EstadoTarea
    {
        public EstadoCompletada(TareaMejorada tarea) : base(tarea) {}
        
        public override string ObtenerNombre() => "Completada";
        
        public override List<string> ObtenerAccionesDisponibles()
        {
            // No hay acciones disponibles en estado completado
            return new List<string>();
        }
        
        public override bool EjecutarAccion(string accion)
        {
            Console.WriteLine($"Error: No se pueden realizar acciones en una tarea completada");
            return false;
        }
        
        public override void MostrarDetallesAdicionales()
        {
            Console.WriteLine($"Fecha inicio: {Tarea.FechaInicio}");
            Console.WriteLine($"Fecha finalización: {Tarea.FechaFinalizacion}");
            TimeSpan duracion = Tarea.FechaFinalizacion.Value - Tarea.FechaInicio.Value;
            Console.WriteLine($"Duración total: {duracion.TotalHours:F1} horas");
        }
    }
    
    /// <summary>
    /// PASO 9: Implementación del estado "Cancelada"
    /// </summary>
    public class EstadoCancelada : EstadoTarea
    {
        public EstadoCancelada(TareaMejorada tarea) : base(tarea) {}
        
        public override string ObtenerNombre() => "Cancelada";
        
        public override List<string> ObtenerAccionesDisponibles()
        {
            // No hay acciones disponibles en estado cancelado
            return new List<string>();
        }
        
        public override bool EjecutarAccion(string accion)
        {
            Console.WriteLine($"Error: No se pueden realizar acciones en una tarea cancelada");
            return false;
        }
        
        public override void MostrarDetallesAdicionales()
        {
            Console.WriteLine($"Fecha cancelación: {Tarea.FechaCancelacion}");
            
            if (Tarea.FechaInicio.HasValue)
            {
                Console.WriteLine($"Fecha inicio: {Tarea.FechaInicio}");
                TimeSpan tiempoActivo = Tarea.FechaCancelacion.Value - Tarea.FechaInicio.Value;
                Console.WriteLine($"Tiempo activo antes de cancelar: {tiempoActivo.TotalHours:F1} horas");
            }
        }
    }
    
    /*
    DIAGRAMA DEL PATRÓN STATE:
    
    ┌──────────────┐     tiene     ┌──────────────┐
    │  TareaMejorada ├────────────>│  EstadoTarea  │
    └───────┬───────┘    estado    └───────┬───────┘
            │                              │
            │                              │
            │        ┌─────────────────────┼─────────────────────┐
            │        │                     │                     │
    ┌───────▼───────▼─┐     ┌─────────────▼───────┐     ┌───────▼───────────┐
    │  EstadoPendiente │     │  EstadoEnProceso   │     │  EstadoCompletada │
    └─────────────────┘     └───────────────────┘     └───────────────────┘
    
    
    VENTAJAS DEL PATRÓN STATE:
    
    1. ENCAPSULAMIENTO:
       - Cada estado encapsula su propio comportamiento
       - Las reglas de transición están claramente definidas
       
    2. ELIMINACIÓN DE CONDICIONALES:
       - Reemplazamos if/switch por polimorfismo
       - Cada estado decide qué acciones permite
       
    3. EXTENSIBILIDAD:
       - Fácil agregar nuevos estados sin modificar código existente
       - Solo necesitamos crear una nueva clase de estado
       
    4. CLARIDAD:
       - Las transiciones entre estados son explícitas
       - El código es más fácil de entender y mantener
       
    CUÁNDO USAR ESTE PATRÓN:
    - Cuando un objeto cambia su comportamiento según su estado interno
    - Cuando hay muchas transiciones de estado con reglas específicas
    - Para eliminar condicionales complejos basados en estado
    
    CONCEPTOS CLAVE:
    - Context: La clase que contiene el estado (TareaMejorada)
    - State: La interfaz o clase abstracta para todos los estados (EstadoTarea)
    - ConcreteState: Implementaciones específicas de los estados (EstadoPendiente, etc.)
    */
}
