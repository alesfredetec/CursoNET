using System;
using System.Collections.Generic;

namespace TecnicasNoIf.EjerciciosJunior
{
    /// <summary>
    /// PROBLEMA: Un gestor de tareas con muchas condiciones if-else para manejar los estados
    /// 
    /// ISSUES:
    /// - Muchos if-else anidados para verificar el estado actual
    /// - Lógica de transición de estados dispersa por todo el código
    /// - Difícil agregar nuevos estados o comportamientos
    /// - El código se vuelve cada vez más complejo con cada nuevo requisito
    /// </summary>
    public class GestorTareas
    {
        // Lista de tareas
        private readonly List<Tarea> _tareas = new List<Tarea>();
        
        // Contador para generar IDs únicos
        private int _nextId = 1;
        
        /// <summary>
        /// Agrega una nueva tarea
        /// </summary>
        public int CrearTarea(string titulo, string descripcion)
        {
            // Crear tarea con estado inicial "Pendiente"
            var tarea = new Tarea
            {
                Id = _nextId++,
                Titulo = titulo,
                Descripcion = descripcion,
                Estado = "Pendiente",
                FechaCreacion = DateTime.Now
            };
            
            _tareas.Add(tarea);
            Console.WriteLine($"Tarea #{tarea.Id} creada: {titulo}");
            return tarea.Id;
        }
        
        /// <summary>
        /// Obtiene la lista de todas las tareas
        /// </summary>
        public List<Tarea> ObtenerTareas()
        {
            return _tareas;
        }
        
        /// <summary>
        /// Cambia el estado de una tarea
        /// </summary>
        public bool CambiarEstado(int id, string nuevoEstado)
        {
            var tarea = _tareas.Find(t => t.Id == id);
            if (tarea == null)
            {
                Console.WriteLine($"Error: No se encontró la tarea con ID {id}");
                return false;
            }
            
            // ❌ PROBLEMA: Muchas condiciones if-else para validar transiciones
            if (nuevoEstado == "EnProceso")
            {
                if (tarea.Estado == "Pendiente")
                {
                    tarea.Estado = nuevoEstado;
                    tarea.FechaInicio = DateTime.Now;
                    Console.WriteLine($"Tarea #{id} iniciada: {tarea.Titulo}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: No se puede iniciar la tarea #{id} porque está en estado {tarea.Estado}");
                    return false;
                }
            }
            else if (nuevoEstado == "Completada")
            {
                if (tarea.Estado == "EnProceso")
                {
                    tarea.Estado = nuevoEstado;
                    tarea.FechaFinalizacion = DateTime.Now;
                    Console.WriteLine($"Tarea #{id} completada: {tarea.Titulo}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: No se puede completar la tarea #{id} porque está en estado {tarea.Estado}");
                    return false;
                }
            }
            else if (nuevoEstado == "Cancelada")
            {
                if (tarea.Estado != "Completada")
                {
                    tarea.Estado = nuevoEstado;
                    tarea.FechaCancelacion = DateTime.Now;
                    Console.WriteLine($"Tarea #{id} cancelada: {tarea.Titulo}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: No se puede cancelar la tarea #{id} porque ya está completada");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Error: Estado '{nuevoEstado}' no reconocido");
                return false;
            }
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
            
            Console.WriteLine($"=== Tarea #{tarea.Id}: {tarea.Titulo} ===");
            Console.WriteLine($"Descripción: {tarea.Descripcion}");
            Console.WriteLine($"Estado: {tarea.Estado}");
            Console.WriteLine($"Creada: {tarea.FechaCreacion}");
            
            // ❌ PROBLEMA: Más condicionales para mostrar información según estado
            if (tarea.Estado == "EnProceso" || tarea.Estado == "Completada")
            {
                Console.WriteLine($"Fecha inicio: {tarea.FechaInicio}");
            }
            
            if (tarea.Estado == "Completada")
            {
                Console.WriteLine($"Fecha finalización: {tarea.FechaFinalizacion}");
                TimeSpan duracion = tarea.FechaFinalizacion.Value - tarea.FechaInicio.Value;
                Console.WriteLine($"Duración total: {duracion.TotalHours:F1} horas");
            }
            
            if (tarea.Estado == "Cancelada")
            {
                Console.WriteLine($"Fecha cancelación: {tarea.FechaCancelacion}");
            }
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
            
            // ❌ PROBLEMA: Otro switch (implícito) para determinar acciones disponibles
            var acciones = new List<string>();
            
            if (tarea.Estado == "Pendiente")
            {
                acciones.Add("Iniciar");
                acciones.Add("Cancelar");
            }
            else if (tarea.Estado == "EnProceso")
            {
                acciones.Add("Completar");
                acciones.Add("Cancelar");
            }
            else if (tarea.Estado == "Completada")
            {
                // No hay acciones disponibles
            }
            else if (tarea.Estado == "Cancelada")
            {
                // No hay acciones disponibles
            }
            
            return acciones;
        }
    }
    
    /// <summary>
    /// Clase que representa una tarea
    /// </summary>
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }  // "Pendiente", "EnProceso", "Completada", "Cancelada"
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
    }
}
