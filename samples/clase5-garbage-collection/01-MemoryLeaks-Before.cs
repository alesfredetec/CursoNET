using System;
using System.Collections.Generic;
using System.Timers;
using System.IO;

namespace GarbageCollectionEjercicios
{
    /// <summary>
    /// CÓDIGO PROBLEMÁTICO - Múltiples memory leaks
    /// 
    /// PROBLEMS IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Event handlers no desregistrados
    /// ❌ PROBLEMA 2: Static collections que crecen infinitamente  
    /// ❌ PROBLEMA 3: IDisposable no implementado
    /// ❌ PROBLEMA 4: Timers no disposed
    /// ❌ PROBLEMA 5: Large objects innecesarios
    /// ❌ PROBLEMA 6: Circular references
    /// ❌ PROBLEMA 7: Finalizers problemáticos
    /// </summary>

    // ❌ PROBLEMA 1: Static cache sin límite
    public static class GlobalCache
    {
        private static Dictionary<string, object> _cache = new Dictionary<string, object>();
        
        public static void AddToCache(string key, object value)
        {
            // ❌ MEMORY LEAK: Cache crece infinitamente
            _cache[key] = value;
            Console.WriteLine($"Cache size: {_cache.Count}");
        }
        
        public static T GetFromCache<T>(string key)
        {
            return _cache.ContainsKey(key) ? (T)_cache[key] : default(T);
        }
        
        // ❌ PROBLEMA: No hay método para limpiar cache
        // ❌ PROBLEMA: No hay expiración de entries
        // ❌ PROBLEMA: No hay límite de memoria
    }

    // ❌ PROBLEMA 2: Publisher sin cleanup de subscribers
    public class EventPublisher
    {
        public event EventHandler<DataEventArgs> DataReceived;
        
        public void PublishData(string data)
        {
            DataReceived?.Invoke(this, new DataEventArgs { Data = data });
        }
        
        // ❌ PROBLEMA: No método para desregistrar eventos
        // ❌ PROBLEMA: Weak references no utilizadas
    }
    
    public class DataEventArgs : EventArgs
    {
        public string Data { get; set; }
    }

    // ❌ PROBLEMA 3: Subscriber que nunca se desregistra
    public class DataSubscriber
    {
        private EventPublisher _publisher;
        private List<string> _receivedData = new List<string>();
        
        public DataSubscriber(EventPublisher publisher)
        {
            _publisher = publisher;
            // ❌ MEMORY LEAK: Event handler registrado pero nunca desregistrado
            _publisher.DataReceived += OnDataReceived;
        }
        
        private void OnDataReceived(object sender, DataEventArgs e)
        {
            // ❌ MEMORY LEAK: Lista crece sin límite
            _receivedData.Add(e.Data);
            Console.WriteLine($"Received: {e.Data}. Total: {_receivedData.Count}");
        }
        
        // ❌ PROBLEMA: No IDisposable implementado
        // ❌ PROBLEMA: No cleanup en destructor
    }

    // ❌ PROBLEMA 4: Timer sin disposal
    public class PeriodicProcessor
    {
        private Timer _timer;
        private List<byte[]> _processedData = new List<byte[]>();
        
        public void StartProcessing()
        {
            _timer = new Timer(1000); // 1 segundo
            _timer.Elapsed += ProcessData;
            _timer.Start();
        }
        
        private void ProcessData(object sender, ElapsedEventArgs e)
        {
            // ❌ MEMORY LEAK: Crear large objects innecesariamente
            byte[] largeData = new byte[1024 * 1024]; // 1MB cada vez
            new Random().NextBytes(largeData);
            
            // ❌ MEMORY LEAK: Lista crece sin límite
            _processedData.Add(largeData);
            
            Console.WriteLine($"Processed data chunks: {_processedData.Count}");
        }
        
        public void StopProcessing()
        {
            _timer?.Stop();
            // ❌ PROBLEMA: Timer no disposed - memory leak
            // ❌ PROBLEMA: _processedData no limpiada
        }
    }

    // ❌ PROBLEMA 5: File stream sin using/disposal
    public class FileProcessor
    {
        private FileStream _fileStream;
        private StreamWriter _writer;
        
        public void OpenFile(string filePath)
        {
            // ❌ MEMORY LEAK: Streams no disposed correctamente
            _fileStream = new FileStream(filePath, FileMode.Create);
            _writer = new StreamWriter(_fileStream);
        }
        
        public void WriteData(string data)
        {
            _writer?.WriteLine(data);
            _writer?.Flush();
        }
        
        public void CloseFile()
        {
            // ❌ PROBLEMA: No disposal pattern correcto
            _writer?.Close();
            _fileStream?.Close();
            // ❌ Streams pueden no liberarse completamente
        }
        
        // ❌ PROBLEMA: No finalizer para cleanup de emergencia
    }

    // ❌ PROBLEMA 6: Circular references
    public class Parent
    {
        public List<Child> Children { get; set; } = new List<Child>();
        public string Name { get; set; }
        
        public void AddChild(Child child)
        {
            Children.Add(child);
            child.Parent = this; // ❌ Circular reference
        }
    }
    
    public class Child
    {
        public Parent Parent { get; set; } // ❌ Strong reference al parent
        public string Name { get; set; }
        
        // ❌ PROBLEMA: Circular reference impide GC
        // ❌ PROBLEMA: No weak references utilizadas
    }

    // ❌ PROBLEMA 7: Finalizer problemático
    public class ProblematicResource
    {
        private IntPtr _unmanagedResource;
        private List<byte[]> _largeDataList = new List<byte[]>();
        
        public ProblematicResource()
        {
            // Simular recurso no administrado
            _unmanagedResource = System.Runtime.InteropServices.Marshal.AllocHGlobal(1024 * 1024);
            
            // ❌ MEMORY LEAK: Crear large objects en constructor
            for (int i = 0; i < 100; i++)
            {
                _largeDataList.Add(new byte[1024 * 1024]); // 100MB total
            }
        }
        
        // ❌ PROBLEMA: Finalizer sin GC.SuppressFinalize
        ~ProblematicResource()
        {
            // ❌ PROBLEMA: Trabajo pesado en finalizer
            Console.WriteLine("Finalizing ProblematicResource...");
            
            // ❌ PROBLEMA: Puede lanzar exceptions en finalizer thread
            if (_unmanagedResource != IntPtr.Zero)
            {
                System.Runtime.InteropServices.Marshal.FreeHGlobal(_unmanagedResource);
            }
            
            // ❌ PROBLEMA: Acceso a managed objects en finalizer
            _largeDataList.Clear();
        }
        
        // ❌ PROBLEMA: No IDisposable implementado
    }

    // ❌ PROBLEMA 8: String concatenation ineficiente
    public class StringProcessor
    {
        public string ProcessLargeText(List<string> textParts)
        {
            string result = "";
            
            // ❌ MEMORY LEAK: String concatenation crea muchos objects temporales
            foreach (var part in textParts)
            {
                result += part + Environment.NewLine; // Cada += crea nueva string
            }
            
            // ❌ PROBLEMA: Para 1000 strings, crea ~500,000 objects temporales
            return result;
        }
        
        public void GenerateReport()
        {
            var parts = new List<string>();
            
            // Generar 10,000 strings
            for (int i = 0; i < 10000; i++)
            {
                parts.Add($"Line {i}: {DateTime.Now} - Some data here");
            }
            
            // ❌ MEMORY PRESSURE: Esto genera millones de strings temporales
            string report = ProcessLargeText(parts);
            
            Console.WriteLine($"Report generated: {report.Length} characters");
        }
    }

    // ❌ PROBLEMA 9: Collections que mantienen references innecesarias
    public class CacheManager
    {
        private static Dictionary<string, WeakReference> _instanceCache = 
            new Dictionary<string, WeakReference>();
        private static List<object> _strongReferences = new List<object>(); // ❌ PROBLEM!
        
        public static void RegisterInstance(string key, object instance)
        {
            _instanceCache[key] = new WeakReference(instance);
            
            // ❌ MEMORY LEAK: Strong reference impide GC
            _strongReferences.Add(instance);
        }
        
        public static T GetInstance<T>(string key) where T : class
        {
            if (_instanceCache.ContainsKey(key))
            {
                return _instanceCache[key].Target as T;
            }
            return null;
        }
        
        // ❌ PROBLEMA: No cleanup de dead weak references
        // ❌ PROBLEMA: Strong references list crece infinitamente
    }

    // ❌ PROBLEMA 10: Demo class que usa todas las clases problemáticas
    public class MemoryLeakDemo
    {
        public void RunLeakyCode()
        {
            Console.WriteLine("Starting memory leak demonstration...");
            
            // 1. Cache leak
            for (int i = 0; i < 1000; i++)
            {
                GlobalCache.AddToCache($"key_{i}", new byte[1024 * 100]); // 100KB cada uno
            }
            
            // 2. Event handler leak
            var publisher = new EventPublisher();
            for (int i = 0; i < 100; i++)
            {
                var subscriber = new DataSubscriber(publisher);
                publisher.PublishData($"Data {i}");
                // ❌ Subscribers nunca se cleanup
            }
            
            // 3. Timer leak
            var processors = new List<PeriodicProcessor>();
            for (int i = 0; i < 10; i++)
            {
                var processor = new PeriodicProcessor();
                processor.StartProcessing();
                processors.Add(processor);
                // ❌ Timers siguen corriendo y consumiendo memoria
            }
            
            // 4. File stream leak
            for (int i = 0; i < 50; i++)
            {
                var fileProcessor = new FileProcessor();
                fileProcessor.OpenFile($"temp_file_{i}.txt");
                fileProcessor.WriteData("Some data");
                // ❌ Files no closed correctamente
            }
            
            // 5. Circular reference leak
            for (int i = 0; i < 100; i++)
            {
                var parent = new Parent { Name = $"Parent_{i}" };
                for (int j = 0; j < 10; j++)
                {
                    var child = new Child { Name = $"Child_{j}" };
                    parent.AddChild(child);
                }
                // ❌ Circular references impiden GC
            }
            
            // 6. Problematic resource leak
            for (int i = 0; i < 10; i++)
            {
                var resource = new ProblematicResource();
                // ❌ Resources no disposed, van a finalizer queue
            }
            
            // 7. String processing leak
            var stringProcessor = new StringProcessor();
            stringProcessor.GenerateReport();
            
            // 8. Cache manager leak
            for (int i = 0; i < 500; i++)
            {
                var obj = new object();
                CacheManager.RegisterInstance($"instance_{i}", obj);
            }
            
            Console.WriteLine("Memory leak demonstration completed.");
            Console.WriteLine($"GC Gen 0 collections: {GC.CollectionCount(0)}");
            Console.WriteLine($"GC Gen 1 collections: {GC.CollectionCount(1)}");
            Console.WriteLine($"GC Gen 2 collections: {GC.CollectionCount(2)}");
            Console.WriteLine($"Total memory: {GC.GetTotalMemory(false):N0} bytes");
            
            // ❌ RESULTADO: Memoria alta, GC frecuente, performance degradada
        }
    }

    /*
    MEMORY LEAKS IDENTIFICADOS:

    1. STATIC COLLECTIONS:
       - GlobalCache crece infinitamente
       - No expiración ni límites de memoria
       - Objects nunca removidos

    2. EVENT HANDLERS:
       - Subscribers registrados pero nunca desregistrados
       - Publisher mantiene references a dead objects
       - Previene GC de subscriber objects

    3. TIMERS Y THREADS:
       - Timers no disposed correctamente
       - Background threads mantienen objects alive
       - Callbacks mantienen referencias

    4. UNMANAGED RESOURCES:
       - File streams no disposed
       - IntPtr no liberados
       - No using statements

    5. CIRCULAR REFERENCES:
       - Parent-Child bidirectional references
       - Objects no pueden ser GC collected
       - Memory consumption creciente

    6. FINALIZERS PROBLEMÁTICOS:
       - Trabajo pesado en finalizer thread
       - No GC.SuppressFinalize() llamado
       - Objects van a finalizer queue innecesariamente

    7. STRING CONCATENATION:
       - Millones de strings temporales
       - Alta presión en Gen 0 GC
       - Performance degradation severa

    8. WEAK REFERENCES MAL USADAS:
       - Strong references adicionales eliminan beneficio
       - Dead references no cleaned up
       - Cache memory leak

    SÍNTOMAS EN PRODUCCIÓN:
    - OutOfMemoryException
    - Performance degradation
    - GC pressure alta
    - Application hangs
    - Resource exhaustion

    OBJETIVO DEL EJERCICIO:
    Identificar y resolver todos estos memory leaks usando:
    ✅ IDisposable pattern
    ✅ Using statements
    ✅ Weak references apropiadas
    ✅ Event handler cleanup
    ✅ Proper finalizer implementation
    ✅ Memory-efficient algorithms

    TIEMPO ESTIMADO: 60 minutos
    DIFICULTAD: Intermedio-Avanzado
    */
}