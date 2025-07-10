using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace GarbageCollectionEjercicios
{
    /// <summary>
    /// TÉCNICAS AVANZADAS DE OPTIMIZACIÓN GC
    /// 
    /// OPTIMIZACIONES APLICADAS:
    /// ✅ ArrayPool para reutilización de arrays
    /// ✅ Struct vs Class decisions optimizadas
    /// ✅ Span<T> y Memory<T> para zero-allocation
    /// ✅ Object pooling avanzado
    /// ✅ GC tuning y configuración
    /// ✅ LOH (Large Object Heap) management
    /// ✅ Value types optimization
    /// </summary>

    // ✅ OPTIMIZACIÓN 1: ArrayPool para arrays temporales
    public class ArrayPoolOptimization
    {
        private static readonly ArrayPool<byte> BytePool = ArrayPool<byte>.Shared;
        private static readonly ArrayPool<int> IntPool = ArrayPool<int>.Shared;

        // ❌ ANTES: Crear arrays constantemente
        public byte[] ProcessDataBad(int size)
        {
            var buffer = new byte[size]; // ❌ Nueva allocation cada vez
            
            // Process data
            for (int i = 0; i < size; i++)
            {
                buffer[i] = (byte)(i % 256);
            }
            
            return buffer; // ❌ Array se va al heap
        }

        // ✅ DESPUÉS: Usar ArrayPool
        public void ProcessDataGood(int size, Action<ReadOnlySpan<byte>> processor)
        {
            var buffer = BytePool.Rent(size); // ✅ Reutilizar array del pool
            try
            {
                // Process data
                for (int i = 0; i < size; i++)
                {
                    buffer[i] = (byte)(i % 256);
                }
                
                // ✅ Usar Span para zero-copy
                processor(buffer.AsSpan(0, size));
            }
            finally
            {
                BytePool.Return(buffer); // ✅ Devolver al pool
            }
        }

        public void ProcessLargeDataSet()
        {
            // Procesar 1000 arrays de 1MB cada uno
            for (int i = 0; i < 1000; i++)
            {
                ProcessDataGood(1024 * 1024, data => {
                    // Process the data
                    Console.WriteLine($"Processed {data.Length} bytes");
                });
            }
            // ✅ RESULTADO: Solo ~10 arrays allocated total vs 1000
        }
    }

    // ✅ OPTIMIZACIÓN 2: Struct vs Class optimization
    public readonly struct Point3D // ✅ struct para datos pequeños inmutables
    {
        public readonly float X, Y, Z;
        
        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public float DistanceTo(Point3D other)
        {
            var dx = X - other.X;
            var dy = Y - other.Y;
            var dz = Z - other.Z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }

    public readonly struct BoundingBox // ✅ struct compuesto
    {
        public readonly Point3D Min, Max;
        
        public BoundingBox(Point3D min, Point3D max)
        {
            Min = min;
            Max = max;
        }
        
        public bool Contains(Point3D point)
        {
            return point.X >= Min.X && point.X <= Max.X &&
                   point.Y >= Min.Y && point.Y <= Max.Y &&
                   point.Z >= Min.Z && point.Z <= Max.Z;
        }
    }

    // ✅ OPTIMIZACIÓN 3: Zero-allocation string processing
    public static class StringOptimizations
    {
        // ❌ ANTES: String operations create many temporary objects
        public static List<string> SplitStringBad(string input, char separator)
        {
            var result = new List<string>();
            var parts = input.Split(separator); // ❌ Creates array + strings
            
            foreach (var part in parts)
            {
                if (!string.IsNullOrWhiteSpace(part))
                {
                    result.Add(part.Trim().ToUpperInvariant()); // ❌ More string allocations
                }
            }
            
            return result;
        }

        // ✅ DESPUÉS: Use Span<char> for zero allocation
        public static void ProcessStringGood(ReadOnlySpan<char> input, char separator, 
            Action<ReadOnlySpan<char>> processor)
        {
            int start = 0;
            
            for (int i = 0; i <= input.Length; i++)
            {
                if (i == input.Length || input[i] == separator)
                {
                    var segment = input.Slice(start, i - start);
                    
                    // ✅ Trim without allocation
                    segment = segment.Trim();
                    
                    if (!segment.IsEmpty)
                    {
                        processor(segment); // ✅ No string allocation
                    }
                    
                    start = i + 1;
                }
            }
        }

        // ✅ String interning for repeated strings
        private static readonly ConcurrentDictionary<string, string> StringCache = new();
        
        public static string InternString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
                
            return StringCache.GetOrAdd(value, v => v);
        }
    }

    // ✅ OPTIMIZACIÓN 4: Object pooling avanzado con políticas
    public class AdvancedObjectPool<T> : IDisposable where T : class, new()
    {
        private readonly ConcurrentQueue<T> _objects = new();
        private readonly Func<T> _objectGenerator;
        private readonly Action<T> _resetAction;
        private readonly int _maxSize;
        private int _currentCount;

        public AdvancedObjectPool(
            Func<T> objectGenerator = null,
            Action<T> resetAction = null,
            int maxSize = 100)
        {
            _objectGenerator = objectGenerator ?? (() => new T());
            _resetAction = resetAction ?? (_ => { });
            _maxSize = maxSize;
        }

        public T Get()
        {
            if (_objects.TryDequeue(out T item))
            {
                Interlocked.Decrement(ref _currentCount);
                return item;
            }

            return _objectGenerator();
        }

        public void Return(T item)
        {
            if (item == null || _currentCount >= _maxSize)
                return;

            _resetAction(item);
            _objects.Enqueue(item);
            Interlocked.Increment(ref _currentCount);
        }

        public void Dispose()
        {
            if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
            {
                while (_objects.TryDequeue(out T item))
                {
                    (item as IDisposable)?.Dispose();
                }
            }
        }
    }

    // ✅ OPTIMIZACIÓN 5: Memory<T> para async scenarios
    public class MemoryOptimizedProcessor
    {
        private readonly AdvancedObjectPool<byte[]> _bufferPool;

        public MemoryOptimizedProcessor()
        {
            _bufferPool = new AdvancedObjectPool<byte[]>(
                objectGenerator: () => new byte[4096],
                resetAction: buffer => Array.Clear(buffer, 0, buffer.Length),
                maxSize: 50
            );
        }

        public async Task<int> ProcessStreamAsync(Stream input, Stream output)
        {
            var buffer = _bufferPool.Get();
            try
            {
                var memory = buffer.AsMemory();
                int totalBytes = 0;
                int bytesRead;

                while ((bytesRead = await input.ReadAsync(memory)) > 0)
                {
                    // ✅ Memory<T> permite async sin pinning
                    await output.WriteAsync(memory.Slice(0, bytesRead));
                    totalBytes += bytesRead;
                }

                return totalBytes;
            }
            finally
            {
                _bufferPool.Return(buffer);
            }
        }
    }

    // ✅ OPTIMIZACIÓN 6: LOH management
    public class LargeObjectOptimization
    {
        // ✅ Pool para large objects (>85KB)
        private readonly AdvancedObjectPool<byte[]> _largeBufferPool;

        public LargeObjectOptimization()
        {
            _largeBufferPool = new AdvancedObjectPool<byte[]>(
                objectGenerator: () => new byte[1024 * 1024], // 1MB buffer
                resetAction: buffer => { /* Optional cleanup */ },
                maxSize: 10 // Limit LOH objects
            );
        }

        public void ProcessLargeData()
        {
            var buffer = _largeBufferPool.Get();
            try
            {
                // Process large data without creating new LOH objects
                ProcessBuffer(buffer);
            }
            finally
            {
                _largeBufferPool.Return(buffer);
            }
        }

        private void ProcessBuffer(byte[] buffer)
        {
            // Simulate processing
            for (int i = 0; i < buffer.Length; i += 1024)
            {
                // Process chunk
            }
        }

        // ✅ Monitor LOH size
        public void MonitorLOHSize()
        {
            var gen2Size = GC.GetTotalMemory(false);
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            var afterGC = GC.GetTotalMemory(true);
            
            Console.WriteLine($"Memory before GC: {gen2Size:N0} bytes");
            Console.WriteLine($"Memory after GC: {afterGC:N0} bytes");
            Console.WriteLine($"LOH approximate size: {gen2Size - afterGC:N0} bytes");
        }
    }

    // ✅ OPTIMIZACIÓN 7: GC tuning y configuración
    public static class GCOptimizationSettings
    {
        public static void OptimizeForThroughput()
        {
            // ✅ Para applications que priorizan throughput sobre latency
            GCSettings.LatencyMode = GCLatencyMode.Batch;
            Console.WriteLine("GC optimized for throughput");
        }

        public static void OptimizeForLowLatency()
        {
            // ✅ Para applications que necesitan baja latency
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            Console.WriteLine("GC optimized for low latency");
        }

        public static void OptimizeForInteractive()
        {
            // ✅ Para UI applications
            GCSettings.LatencyMode = GCLatencyMode.Interactive;
            Console.WriteLine("GC optimized for interactive applications");
        }

        public static void ShowGCSettings()
        {
            Console.WriteLine($"GC Latency Mode: {GCSettings.LatencyMode}");
            Console.WriteLine($"Server GC: {GCSettings.IsServerGC}");
            Console.WriteLine($"Gen 0 collections: {GC.CollectionCount(0)}");
            Console.WriteLine($"Gen 1 collections: {GC.CollectionCount(1)}");
            Console.WriteLine($"Gen 2 collections: {GC.CollectionCount(2)}");
        }
    }

    // ✅ OPTIMIZACIÓN 8: Value types performance
    public readonly struct OptimizedVector3
    {
        public readonly float X, Y, Z;

        public OptimizedVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // ✅ Operators optimizados
        public static OptimizedVector3 operator +(OptimizedVector3 a, OptimizedVector3 b)
            => new OptimizedVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static OptimizedVector3 operator *(OptimizedVector3 v, float scalar)
            => new OptimizedVector3(v.X * scalar, v.Y * scalar, v.Z * scalar);

        // ✅ No boxing para ToString()
        public override string ToString()
            => $"({X:F2}, {Y:F2}, {Z:F2})";
    }

    // ✅ OPTIMIZACIÓN 9: Benchmark y profiling
    public class GCPerformanceBenchmark
    {
        private const int IterationCount = 100000;

        public void BenchmarkAllocations()
        {
            Console.WriteLine("Starting GC performance benchmark...");

            // Warmup
            for (int i = 0; i < 1000; i++)
            {
                var dummy = new object();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var startMemory = GC.GetTotalMemory(false);
            var startGen0 = GC.CollectionCount(0);
            var startGen1 = GC.CollectionCount(1);
            var startGen2 = GC.CollectionCount(2);
            var startTime = DateTime.UtcNow;

            // Test allocations
            for (int i = 0; i < IterationCount; i++)
            {
                var data = new byte[1024]; // 1KB allocations
                ProcessData(data);
            }

            var endTime = DateTime.UtcNow;
            var endMemory = GC.GetTotalMemory(false);
            var endGen0 = GC.CollectionCount(0);
            var endGen1 = GC.CollectionCount(1);
            var endGen2 = GC.CollectionCount(2);

            Console.WriteLine($"Time: {(endTime - startTime).TotalMilliseconds:F2}ms");
            Console.WriteLine($"Memory delta: {endMemory - startMemory:N0} bytes");
            Console.WriteLine($"Gen 0 collections: {endGen0 - startGen0}");
            Console.WriteLine($"Gen 1 collections: {endGen1 - startGen1}");
            Console.WriteLine($"Gen 2 collections: {endGen2 - startGen2}");
        }

        public void BenchmarkOptimizedAllocations()
        {
            Console.WriteLine("Starting optimized benchmark...");

            var pool = ArrayPool<byte>.Shared;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var startMemory = GC.GetTotalMemory(false);
            var startGen0 = GC.CollectionCount(0);
            var startTime = DateTime.UtcNow;

            // Test optimized allocations
            for (int i = 0; i < IterationCount; i++)
            {
                var data = pool.Rent(1024);
                try
                {
                    ProcessData(data);
                }
                finally
                {
                    pool.Return(data);
                }
            }

            var endTime = DateTime.UtcNow;
            var endMemory = GC.GetTotalMemory(false);
            var endGen0 = GC.CollectionCount(0);

            Console.WriteLine($"Optimized time: {(endTime - startTime).TotalMilliseconds:F2}ms");
            Console.WriteLine($"Optimized memory delta: {endMemory - startMemory:N0} bytes");
            Console.WriteLine($"Optimized Gen 0 collections: {endGen0 - startGen0}");
        }

        private void ProcessData(byte[] data)
        {
            // Simulate processing
            if (data.Length > 0)
                data[0] = 42;
        }
    }

    // ✅ OPTIMIZACIÓN 10: Demo completo
    public class GCOptimizationDemo
    {
        public void RunOptimizationDemo()
        {
            Console.WriteLine("=== GC Optimization Demonstration ===");

            // 1. Array pooling demo
            Console.WriteLine("\n1. Array Pooling:");
            var arrayOpt = new ArrayPoolOptimization();
            arrayOpt.ProcessLargeDataSet();

            // 2. String optimization demo
            Console.WriteLine("\n2. String Processing:");
            var testString = "apple,banana,cherry,date,elderberry";
            StringOptimizations.ProcessStringGood(testString, ',', span => {
                Console.WriteLine($"Processed: {span.ToString()}");
            });

            // 3. Object pooling demo
            Console.WriteLine("\n3. Object Pooling:");
            using (var pool = new AdvancedObjectPool<StringBuilder>(
                objectGenerator: () => new StringBuilder(),
                resetAction: sb => sb.Clear(),
                maxSize: 10))
            {
                for (int i = 0; i < 5; i++)
                {
                    var sb = pool.Get();
                    try
                    {
                        sb.Append($"Message {i}");
                        Console.WriteLine(sb.ToString());
                    }
                    finally
                    {
                        pool.Return(sb);
                    }
                }
            }

            // 4. GC settings demo
            Console.WriteLine("\n4. GC Settings:");
            GCOptimizationSettings.ShowGCSettings();

            // 5. Performance benchmark
            Console.WriteLine("\n5. Performance Benchmark:");
            var benchmark = new GCPerformanceBenchmark();
            
            Console.WriteLine("Regular allocations:");
            benchmark.BenchmarkAllocations();
            
            Console.WriteLine("\nOptimized allocations:");
            benchmark.BenchmarkOptimizedAllocations();

            Console.WriteLine("\n=== Demo completed ===");
        }
    }

    /*
    RESULTADOS DE OPTIMIZACIÓN:

    ✅ ARRAY POOLING:
    - Reduce allocations 90%+
    - Menos presión en Gen 0
    - Mejor performance en loops

    ✅ SPAN<T> / MEMORY<T>:
    - Zero-allocation string processing
    - No boxing/unboxing
    - Stack-based operations

    ✅ STRUCT OPTIMIZATION:
    - Value types en stack
    - No GC pressure para datos pequeños
    - Cache-friendly memory layout

    ✅ OBJECT POOLING:
    - Reutilización de objetos costosos
    - Configurable pool size
    - Automatic cleanup

    ✅ LOH MANAGEMENT:
    - Pool para objetos >85KB
    - Reduce fragmentación
    - Better memory utilization

    ✅ GC TUNING:
    - Latency modes apropiados
    - Configuration-based optimization
    - Application-specific settings

    MÉTRICAS TÍPICAS DE MEJORA:
    - Memory allocations: 70-90% reducción
    - GC Gen 0 collections: 60-80% reducción
    - GC Gen 2 collections: 50-70% reducción
    - Performance: 20-50% mejora
    - Memory usage: 30-60% reducción

    CUÁNDO APLICAR:
    ✅ High-performance applications
    ✅ Memory-constrained environments
    ✅ Real-time applications
    ✅ Server applications con alta carga
    ✅ Mobile applications

    MONITOREO:
    - PerfView para ETW analysis
    - dotMemory para memory profiling
    - Application Insights metrics
    - Custom performance counters

    PRÓXIMA CLASE:
    Indexación SQL - Database performance optimization
    */
}