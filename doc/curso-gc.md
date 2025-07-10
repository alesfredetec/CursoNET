Curso Práctico de Garbage Collection (GC) en .NETIntroducción: ¿Por qué necesitamos un "Recolector de Basura"?En lenguajes como C o C++, el programador es responsable de solicitar memoria al sistema operativo y, lo que es más importante, de liberarla cuando ya no la necesita. Olvidar liberar memoria causa "fugas de memoria" (memory leaks), que eventualmente agotan la memoria del sistema..NET introduce el concepto de memoria gestionada (managed memory). El programador crea objetos (new MyClass()), y el entorno de .NET, a través del Garbage Collector (GC), se encarga automáticamente de encontrar y eliminar los objetos que ya no están en uso. Esto simplifica enormemente el desarrollo y previene la mayoría de las fugas de memoria.El objetivo de este curso es entender cómo funciona este proceso automático y qué responsabilidades todavía tenemos como desarrolladores.Clase 1: Fundamentos del GC - ¿Cómo funciona la magia?Objetivos:Entender qué es el Heap y las Generaciones.Visualizar el proceso de "Marcar y Compactar".Identificar cuándo un objeto es elegible para ser recolectado.Paso 1: El Montón de Memoria (Managed Heap)Cuando creas un objeto con new, .NET le asigna un espacio de memoria en un área llamada Managed Heap. Imagínalo como un gran bloque de memoria reservado para tu aplicación.Paso 2: El Principio Clave - La Alcanzabilidad (Reachability)El GC es como un detective. Para decidir si un objeto sigue vivo, intenta "alcanzarlo" desde un punto de partida seguro. Estos puntos de partida se llaman raíces (GC Roots). Las raíces son:Variables locales en el stack de un método que se está ejecutando.Variables estáticas globales.Referencias a objetos en la cola de finalización.Regla de Oro: Si el GC, partiendo de una raíz, puede seguir una cadena de referencias y llegar a tu objeto, el objeto está VIVO. Si no hay ningún camino para llegar a él, el objeto está MUERTO y es "basura" elegible para recolección.Ejemplo Práctico:public void MyMethod()
{
    Customer customerA = new Customer(); // customerA es una raíz. El objeto Customer está VIVO.
    Customer customerB = new Customer(); // customerB es una raíz. El objeto Customer está VIVO.

    customerA.BestFriend = customerB; // Ahora el objeto de customerA tiene una referencia al de customerB.

} // Fin del método
Cuando MyMethod termina, las variables locales customerA y customerB desaparecen del stack. Ya no son raíces. Si ningún otro objeto en la aplicación tiene una referencia a estos dos objetos Customer, se vuelven inalcanzables. En la próxima recolección, el GC los eliminará.Paso 3: El Sistema de Generaciones - Una Optimización InteligenteEl GC de .NET es muy eficiente porque parte de una observación: "la mayoría de los objetos mueren jóvenes". Los objetos que creas dentro de un método suelen tener una vida muy corta.Para aprovechar esto, el Heap se divide en tres Generaciones:Generación 0 (Gen 0): ¡La guardería! Aquí nacen todos los objetos nuevos. Es pequeña y se recolecta con mucha frecuencia. La mayoría de los objetos (más del 90%) mueren aquí.Generación 1 (Gen 1): Los supervivientes. Un objeto que sobrevive a una recolección en Gen 0 es "promovido" a Gen 1. Actúa como un búfer y se recolecta con menos frecuencia.Generación 2 (Gen 2): Los veteranos. Un objeto que sobrevive a una recolección en Gen 1 es promovido a Gen 2. Aquí viven los objetos de larga duración (como objetos estáticos, servicios singleton, etc.). Las recolecciones de Gen 2 son las más "caras" (lentas) y menos frecuentes.[Imagen de Diagrama de las Generaciones del GC. Objetos nuevos entran en Gen 0. Algunos sobreviven y son promovidos a Gen 1. Pocos sobreviven y son promovidos a Gen 2.]Paso 4: El Proceso de "Marcar y Compactar"Cuando el GC se ejecuta (por ejemplo, en Gen 0):Fase de Marcado (Mark): El GC recorre el grafo de objetos desde las raíces y "marca" todos los objetos que puede alcanzar como VIVOS.Fase de Barrido (Sweep): Todos los objetos que no fueron marcados son considerados basura. El GC los elimina.Fase de Compactación (Compact): Para evitar que la memoria quede fragmentada (llena de agujeros), el GC mueve todos los objetos vivos juntos, uno al lado del otro, al principio del segmento de memoria. Esto hace que las futuras asignaciones de memoria sean extremadamente rápidas.Clase 2: Recursos No Gestionados y el Patrón IDisposableObjetivos:Entender qué son los recursos no gestionados.Implementar correctamente la interfaz IDisposable.Usar la declaración using para garantizar la liberación de recursos.Paso 1: El Límite del GC - Recursos No GestionadosEl GC es fantástico para la memoria, pero no sabe nada sobre otros recursos del sistema operativo que tu aplicación podría estar usando. Estos se llaman recursos no gestionados (unmanaged resources).Ejemplos comunes:Conexiones a bases de datos.Identificadores de archivos (File handles).Conexiones de red (Sockets).Objetos gráficos (pinceles, fuentes).Problema: Si abres un archivo pero no le dices explícitamente al sistema operativo que lo cierre, el archivo puede permanecer bloqueado, consumiendo recursos valiosos. El GC no cerrará el archivo por ti.Paso 2: La Solución - El Patrón IDisposablePara manejar esto, .NET proporciona la interfaz IDisposable. Es un contrato simple con un solo método: void Dispose();.Regla de Oro: Si una clase que usas implementa IDisposable, tú eres responsable de llamar a su método Dispose() cuando termines de usarla.La forma más segura y recomendada de hacer esto es con la declaración using.Ejemplo Práctico:// System.IO.StreamReader implementa IDisposable porque maneja un recurso no gestionado (un archivo).

// --- FORMA CORRECTA ---
public void ReadFile()
{
    // La declaración 'using' garantiza que reader.Dispose() se llamará automáticamente
    // al final del bloque, incluso si ocurre una excepción.
    using (StreamReader reader = new StreamReader("C:\\miarchivo.txt"))
    {
        string content = reader.ReadToEnd();
        Console.WriteLine(content);
    } // <-- reader.Dispose() se llama aquí automáticamente.
}

// --- FORMA INCORRECTA (¡NO HACER!) ---
public void ReadFileBad()
{
    StreamReader reader = new StreamReader("C:\\miarchivo.txt");
    string content = reader.ReadToEnd();
    Console.WriteLine(content);
    // ¿Qué pasa si aquí ocurre una excepción? reader.Dispose() nunca se llama.
    // ¡Tenemos una fuga de recursos!
}
Paso 3: Implementando IDisposable en tus Propias ClasesSi tu clase contiene un campo que es IDisposable, tu clase también debería ser IDisposable para permitir que sus usuarios la limpien correctamente.Ejemplo Paso a Paso:// Imagina que creamos una clase que envuelve una conexión a una base de datos.
// DbConnection es una clase de .NET que implementa IDisposable.

public class DatabaseConnector : IDisposable
{
    private System.Data.SqlClient.SqlConnection _connection;
    private bool _disposed = false; // Para evitar llamadas múltiples a Dispose

    public DatabaseConnector(string connectionString)
    {
        // Creamos y abrimos el recurso no gestionado.
        _connection = new System.Data.SqlClient.SqlConnection(connectionString);
        _connection.Open();
        Console.WriteLine("Conexión a la base de datos abierta.");
    }

    // El método público Dispose, parte de la interfaz IDisposable.
    public void Dispose()
    {
        // Llama a nuestro método de limpieza y le dice al GC
        // que no necesita llamar al finalizador (si lo hubiera).
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Método de limpieza protegido y virtual.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return; // Si ya se limpió, no hacer nada.

        if (disposing)
        {
            // Liberar recursos gestionados (el objeto _connection).
            if (_connection != null)
            {
                _connection.Dispose();
                Console.WriteLine("Recursos gestionados liberados.");
            }
        }

        // En una implementación más compleja, aquí se liberarían recursos no gestionados
        // directamente (punteros, etc.), pero es poco común en C# moderno.

        _disposed = true;
        Console.WriteLine("Conexión a la base de datos cerrada.");
    }
}
Ejercicio Propuesto 1:Crea una clase FileWriter que en su constructor reciba una ruta de archivo y cree un StreamWriter. Implementa IDisposable en tu clase para asegurarte de que el StreamWriter siempre se cierre correctamente. Escribe un pequeño programa que use tu clase FileWriter dentro de un bloque using.Clase 3: Cuando la Memoria se Agota - OutOfMemoryException (OOM)Objetivos:Entender las causas comunes de una excepción OutOfMemoryException.Simular un OOM por asignación de un objeto muy grande.Simular un OOM por una fuga de memoria gestionada (objetos que no se liberan).Paso 1: ¿Por qué ocurre un OOM?Un OutOfMemoryException puede ocurrir por dos razones principales:No hay un bloque contiguo de memoria lo suficientemente grande: Estás intentando crear un objeto (generalmente un array) que es tan grande que el sistema operativo no puede encontrar un "hueco" de memoria libre y contiguo para alojarlo.Agotamiento total de la memoria: Tu aplicación ha consumido toda la memoria virtual disponible. Esto suele ser el resultado de una fuga de memoria gestionada: sigues creando objetos y manteniendo referencias a ellos, por lo que el GC nunca puede liberarlos.Paso 2: Simulación de OOM por Objeto GrandeEste es el caso más simple. Intentaremos crear un array de bytes que es demasiado grande.Ejemplo Práctico:// --- CUIDADO: Este código está diseñado para fallar. ---
public class OomSimulator
{
    public static void SimulateBigObjectOom()
    {
        try
        {
            Console.WriteLine("Intentando asignar un array gigante...");
            
            // Intentamos crear un array de 2 GB (int.MaxValue bytes).
            // En una máquina de 32 bits, o una con memoria limitada, esto fallará.
            byte[] bigArray = new byte[int.MaxValue]; 
            
            Console.WriteLine("¡Asignación exitosa! (Esto es poco probable)");
        }
        catch (OutOfMemoryException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("¡EXCEPCIÓN OutOfMemoryException CAPTURADA!");
            Console.WriteLine(ex.Message);
            Console.WriteLine("---------------------------------------");
            Console.ResetColor();
        }
    }
}
Cómo ejecutarlo: Llama a OomSimulator.SimulateBigObjectOom() desde tu método Main. Probablemente verás la excepción inmediatamente.Paso 3: Simulación de OOM por Fuga de Memoria GestionadaEste escenario es más realista y peligroso. No fallará inmediatamente, sino que consumirá memoria lentamente hasta que el sistema colapse.El Patrón de la Fuga: Un objeto de larga duración (como una lista estática) mantiene referencias a objetos de corta duración, impidiendo que el GC los recolecte.Ejemplo Práctico:// --- CUIDADO: Este código consumirá mucha memoria. ---
public class OomSimulator
{
    // Una lista estática es una raíz del GC y vivirá por siempre.
    private static List<byte[]> _leakyList = new List<byte[]>();

    public static void SimulateMemoryLeakOom()
    {
        Console.WriteLine("Iniciando simulación de fuga de memoria. Presiona Enter para detener.");
        Console.WriteLine("Observa el consumo de memoria de la aplicación en el Administrador de Tareas.");

        int i = 0;
        while (!Console.KeyAvailable)
        {
            // En cada iteración, creamos un objeto de 1 MB.
            byte[] memoryChunk = new byte[1024 * 1024]; // 1 MB
            
            // ¡LA FUGA ESTÁ AQUÍ!
            // Añadimos el objeto a nuestra lista estática. Ahora el objeto
            // es alcanzable desde una raíz y NUNCA será recolectado,
            // incluso si la variable 'memoryChunk' desaparece.
            _leakyList.Add(memoryChunk);
            
            i++;
            Console.WriteLine($"Memoria asignada: {i} MB");
            
            // Pequeña pausa para no colapsar instantáneamente.
            Thread.Sleep(50);
        }

        Console.WriteLine("Simulación detenida.");
    }
}
Cómo ejecutarlo: Llama a OomSimulator.SimulateMemoryLeakOom(). Abre el Administrador de Tareas de tu sistema operativo y observa cómo el uso de memoria de tu aplicación crece sin parar. Dependiendo de tu RAM, después de unos segundos o minutos, la aplicación se cerrará con un OutOfMemoryException.Ejercicio Propuesto 2Toma el código del "Paso 3" (SimulateMemoryLeakOom).Identifica la línea exacta que causa la fuga de memoria.Corrige el código. El bucle debería seguir asignando memoria, pero esta debería poder ser recolectada por el GC. (Pista: ¿Qué pasa si no añades el objeto a la lista?).Ejecuta la versión corregida. Observa en el Administrador de Tareas. El uso de memoria debería subir y bajar (a medida que el GC hace su trabajo), pero no debería crecer indefinidamente.