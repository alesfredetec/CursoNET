Curso Intensivo: Técnicas "No If" para un Código Limpio y FlexibleIntroducción: ¿Por Qué Evitar los if?Los if-else y switch son herramientas básicas de programación, pero su uso excesivo conduce a lo que se conoce como Complejidad Ciclomática. Un método con muchos caminos condicionales es:Difícil de leer y entender: La lógica se ramifica y se vuelve enrevesada.Difícil de mantener: Añadir una nueva condición implica modificar el código existente, lo que es propenso a errores y viola el Principio de Abierto/Cerrado.Difícil de probar: Se necesita una prueba unitaria para cada rama del if.El objetivo de este curso no es eliminar todos los if, sino aprender técnicas y patrones para reemplazar la lógica de negocio compleja y condicional por diseños más robustos, flexibles y orientados a objetos.Clase 1: Adiós al switch - Diccionarios al RescateObjetivo: Reemplazar estructuras switch o cadenas de if-else que mapean una clave a una acción o valor, utilizando la simplicidad y eficiencia de un Dictionary.El Problema: Un "Factory" RígidoImagina que tienes una fábrica que crea diferentes tipos de objetos de notificación según un string.// --- ANTES ---
public interface INotifier { void Notify(string message); }
public class EmailNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"EMAIL: {m}"); }
public class SmsNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"SMS: {m}"); }
public class PushNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"PUSH: {m}"); }

public class NotifierFactory
{
    // Este método viola el Principio de Abierto/Cerrado.
    // Si añadimos un nuevo notificador (ej. WhatsApp), TENEMOS que modificar esta clase.
    public INotifier GetNotifier(string notificationType)
    {
        switch (notificationType.ToLower())
        {
            case "email":
                return new EmailNotifier();
            case "sms":
                return new SmsNotifier();
            case "push":
                return new PushNotifier();
            default:
                throw new ArgumentException("Tipo de notificación no válido", nameof(notificationType));
        }
    }
}
La Solución Paso a Paso: Mapeo con DiccionariosPaso 1: Identificar el Mapeo. La lógica es simple: una clave string se mapea a la creación de un objeto. Esto es un caso de uso perfecto para un diccionario.Paso 2: Crear el Diccionario. Crearemos un Dictionary<string, Func<INotifier>>. La clave será el tipo de notificación, y el valor será una función que sabe cómo crear el notificador correspondiente. Usar Func<> nos da "creación perezosa" (lazy creation), el objeto solo se instancia cuando se necesita.Paso 3: Refactorizar el Método. El switch desaparece y es reemplazado por una simple búsqueda en el diccionario.// --- DESPUÉS ---
public class NotifierFactory
{
    private readonly Dictionary<string, Func<INotifier>> _notifiers;

    public NotifierFactory()
    {
        // El diccionario se inicializa una sola vez en el constructor.
        _notifiers = new Dictionary<string, Func<INotifier>>
        {
            { "email", () => new EmailNotifier() },
            { "sms", () => new SmsNotifier() },
            { "push", () => new PushNotifier() }
        };
    }

    public INotifier GetNotifier(string notificationType)
    {
        // El switch se reemplaza por una búsqueda. Es más eficiente y limpio.
        if (_notifiers.TryGetValue(notificationType.ToLower(), out var createNotifierFunc))
        {
            return createNotifierFunc();
        }
        
        throw new ArgumentException("Tipo de notificación no válido", nameof(notificationType));
    }
}
Ventaja: Para añadir un WhatsAppNotifier, ya no modificamos el método GetNotifier. Simplemente añadimos una nueva entrada al diccionario en el constructor. El código cumple con el Principio de Abierto/Cerrado.Ejemplo Práctico 2: Procesador de ComandosAntes:public void ExecuteCommand(string command)
{
    if (command == "play") Console.WriteLine("Reproduciendo...");
    else if (command == "pause") Console.WriteLine("Pausado.");
    else if (command == "stop") Console.WriteLine("Detenido.");
}
Después (usando Dictionary<string, Action>):private readonly Dictionary<string, Action> _commands;
public CommandProcessor()
{
    _commands = new Dictionary<string, Action>
    {
        { "play", () => Console.WriteLine("Reproduciendo...") },
        { "pause", () => Console.WriteLine("Pausado.") },
        { "stop", () => Console.WriteLine("Detenido.") }
    };
}
public void ExecuteCommand(string command)
{
    if (_commands.TryGetValue(command, out var action))
    {
        action();
    }
}
Ejercicio Propuesto 1Refactoriza el siguiente método que calcula el resultado de una operación matemática básica. Usa un Dictionary<char, Func<int, int, int>>.Código a refactorizar:public int Calculate(char operation, int a, int b)
{
    switch (operation)
    {
        case '+':
            return a + b;
        case '-':
            return a - b;
        case '*':
            return a * b;
        case '/':
            return a / b;
        default:
            throw new InvalidOperationException();
    }
}
Clase 2: El Patrón StrategyObjetivo: Reemplazar bloques if-else complejos que representan diferentes algoritmos o estrategias de negocio, encapsulando cada algoritmo en su propia clase.El Problema: Cálculo de Costos de EnvíoTenemos un método para calcular el costo de envío de un paquete, y la lógica cambia según la compañía de transporte seleccionada.// --- ANTES ---
public class Order { public decimal WeightInKg { get; set; } /* ... */ }

public class ShippingCalculator
{
    // Cada vez que añadimos una nueva compañía, este método crece y se vuelve más frágil.
    public decimal CalculateShippingCost(Order order, string provider)
    {
        decimal cost = 0;
        if (provider == "FedEx")
        {
            // Lógica compleja de FedEx
            cost = order.WeightInKg * 3.5m + 5;
        }
        else if (provider == "UPS")
        {
            // Lógica de UPS
            cost = order.WeightInKg * 4.2m;
        }
        else if (provider == "DHL")
        {
            // Lógica de DHL
            cost = 15 + (order.WeightInKg > 10 ? (order.WeightInKg - 10) * 2 : 0);
        }
        return cost;
    }
}
La Solución Paso a Paso: El Patrón StrategyPaso 1: Definir la Interfaz de la Estrategia. Creamos una interfaz que define el contrato para todas nuestras estrategias de cálculo.public interface IShippingStrategy
{
    decimal CalculateCost(Order order);
}
Paso 2: Crear Clases de Estrategia Concretas. Cada rama del if se convierte en su propia clase que implementa la interfaz.public class FedExShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(Order order) => order.WeightInKg * 3.5m + 5;
}
public class UPSShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(Order order) => order.WeightInKg * 4.2m;
}
public class DHLShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(Order order) => 15 + (order.WeightInKg > 10 ? (order.WeightInKg - 10) * 2 : 0);
}
Paso 3: Usar la Estrategia en la Clase de Contexto. La clase ShippingCalculator (o la propia clase Order) ya no contiene la lógica if. Simplemente usa el objeto de estrategia que se le proporciona.// --- DESPUÉS ---
public class ShippingCalculator
{
    // El método ahora depende de la ABSTRACCIÓN, no de la implementación.
    public decimal CalculateShippingCost(Order order, IShippingStrategy strategy)
    {
        return strategy.CalculateCost(order);
    }
}
¿Cómo se usa? El código cliente es responsable de elegir la estrategia correcta, a menudo usando el patrón Factory que vimos en la Clase 1.// Código cliente
var order = new Order { WeightInKg = 5 };
var calculator = new ShippingCalculator();

// Elegimos la estrategia (podría venir de un Factory)
IShippingStrategy fedexStrategy = new FedExShippingStrategy();

// Y calculamos. ¡El calculador no sabe qué estrategia es!
decimal cost = calculator.CalculateShippingCost(order, fedexStrategy); // cost = 22.5
Ventaja: Para añadir una nueva compañía "Estafeta", creamos la clase EstafetaShippingStrategy. La clase ShippingCalculator no se modifica en absoluto.Ejercicio Propuesto 2Refactoriza el siguiente método que exporta datos a diferentes formatos usando el Patrón Strategy.Código a refactorizar:public class Report { /* ... datos del reporte ... */ }
public class Exporter
{
    public void Export(Report report, string format)
    {
        if (format == "PDF")
        {
            Console.WriteLine("Lógica para crear un archivo PDF...");
        }
        else if (format == "CSV")
        {
            Console.WriteLine("Lógica para crear un archivo CSV...");
        }
        else if (format == "JSON")
        {
            Console.WriteLine("Lógica para serializar a JSON...");
        }
    }
}
Tu tarea: Crea una interfaz IExportStrategy y clases concretas como PdfExportStrategy, etc.Clase 3: El Patrón StateObjetivo: Eliminar condicionales que gestionan el comportamiento de un objeto basado en su estado interno. El patrón permite a un objeto alterar su comportamiento cuando su estado interno cambia.El Problema: Gestión de un DocumentoUn objeto Document puede estar en diferentes estados ("Borrador", "Publicado", "Archivado"). Los métodos como Publish() o Archive() están llenos de ifs para verificar el estado actual antes de realizar una acción.// --- ANTES ---
public class Document
{
    public string State { get; private set; } = "Borrador";

    public void Publish()
    {
        if (State == "Borrador")
        {
            Console.WriteLine("Publicando el documento...");
            State = "Publicado";
        }
        else if (State == "Publicado")
        {
            Console.WriteLine("Error: El documento ya está publicado.");
        }
        else if (State == "Archivado")
        {
            Console.WriteLine("Error: Un documento archivado no puede ser publicado.");
        }
    }
    // ... métodos Archive(), etc. con lógica similar
}
Problema: La lógica de transición de estados está dispersa y es compleja. Añadir un nuevo estado (ej. "En Revisión") requiere modificar todos los métodos.La Solución Paso a Paso: El Patrón StatePaso 1: Definir la Interfaz de Estado. Define una interfaz con los mismos métodos que dependen del estado en la clase principal.public interface IDocumentState
{
    void Publish(Document document);
    void Archive(Document document);
}
Paso 2: Crear Clases de Estado Concretas. Cada estado se convierte en su propia clase. Cada clase implementa el comportamiento específico para ese estado y gestiona la transición al siguiente estado.public class DraftState : IDocumentState
{
    public void Publish(Document document)
    {
        Console.WriteLine("Publicando el documento...");
        document.SetState(new PublishedState()); // Transición al siguiente estado
    }
    public void Archive(Document document)
    {
        Console.WriteLine("Archivando el borrador...");
        document.SetState(new ArchivedState());
    }
}

public class PublishedState : IDocumentState
{
    public void Publish(Document document) => Console.WriteLine("Error: El documento ya está publicado.");
    public void Archive(Document document)
    {
        Console.WriteLine("Archivando el documento publicado...");
        document.SetState(new ArchivedState());
    }
}

// ... Y la clase ArchivedState ...
Paso 3: Modificar la Clase de Contexto. La clase Document ahora delega las llamadas a su objeto de estado actual.// --- DESPUÉS ---
public class Document
{
    private IDocumentState _currentState;

    public Document()
    {
        _currentState = new DraftState(); // Estado inicial
    }

    // Método para que los estados puedan cambiar el estado del documento
    public void SetState(IDocumentState newState) => _currentState = newState;

    // Los métodos ahora simplemente DELEGAN la llamada al estado actual.
    public void Publish() => _currentState.Publish(this);
    public void Archive() => _currentState.Archive(this);
}
Ventaja: Toda la lógica de un estado particular está contenida en una sola clase. Para añadir un estado "En Revisión", creamos la clase ReviewState y modificamos solo los estados que pueden transicionar a él, sin tocar la clase Document.Ejercicio Propuesto 3Refactoriza la gestión de un ticket de soporte técnico. El ticket puede estar en los estados "Abierto", "En Progreso" y "Cerrado".Código a refactorizar:public class SupportTicket
{
    public string Status { get; private set; } = "Abierto";

    public void AssignToDeveloper()
    {
        if (Status == "Abierto")
        {
            Console.WriteLine("Asignando ticket...");
            Status = "En Progreso";
        }
        else { Console.WriteLine("Acción no permitida."); }
    }
    
    public void Resolve()
    {
        if (Status == "En Progreso")
        {
            Console.WriteLine("Resolviendo ticket...");
            Status = "Cerrado";
        }
        else { Console.WriteLine("Acción no permitida."); }
    }
}
Tu tarea: Implementa el patrón State con una interfaz ITicketState y clases concretas.Clase 4: Polimorfismo - El "No If" de la OOPObjetivo: Reemplazar condicionales que verifican el tipo de un objeto (if (obj is TypeA)) utilizando el comportamiento polimórfico fundamental de la Programación Orientada a Objetos.El Problema: Procesando diferentes formas geométricasTenemos una lista de figuras y queremos calcular el área total, pero la fórmula del área es diferente para cada tipo de figura.// --- ANTES ---
public class Square { public double Side { get; set; } }
public class Circle { public double Radius { get; set; } }
public class Rectangle { public double Width { get; set; } public double Height { get; set; } }

public class AreaCalculator
{
    public double CalculateTotalArea(List<object> shapes)
    {
        double totalArea = 0;
        foreach (var shape in shapes)
        {
            // ¡Esto es un "code smell" terrible!
            // Es frágil y viola OCP. Añadir un Triángulo requiere modificar este bucle.
            if (shape is Square s)
            {
                totalArea += s.Side * s.Side;
            }
            else if (shape is Circle c)
            {
                totalArea += Math.PI * c.Radius * c.Radius;
            }
            else if (shape is Rectangle r)
            {
                totalArea += r.Width * r.Height;
            }
        }
        return totalArea;
    }
}
La Solución Paso a Paso: PolimorfismoPaso 1: Crear una Abstracción Común. Se define una clase base o interfaz que todas las figuras implementarán. Esta abstracción tendrá el método que necesitamos: CalculateArea().public abstract class Shape
{
    public abstract double CalculateArea();
}
Paso 2: Heredar e Implementar. Cada clase de figura hereda de la clase base y proporciona su propia implementación de la fórmula del área.public class Square : Shape
{
    public double Side { get; set; }
    public override double CalculateArea() => Side * Side;
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double CalculateArea() => Math.PI * Radius * Radius;
}

public class Rectangle : Shape
{
    public double Width { get; set; } public double Height { get; set; }
    public override double CalculateArea() => Width * Height;
}
Paso 3: Refactorizar el Bucle. El bucle se vuelve trivial. Ya no necesita saber el tipo concreto de cada objeto. Simplemente llama al método CalculateArea() y el polimorfismo se encarga del resto.// --- DESPUÉS ---
public class AreaCalculator
{
    // La lista ahora es de tipo Shape, nuestra abstracción.
    public double CalculateTotalArea(List<Shape> shapes)
    {
        double totalArea = 0;
        foreach (var shape in shapes)
        {
            // No hay if. Simplemente llamamos al método.
            // .NET elige la implementación correcta en tiempo de ejecución.
            totalArea += shape.CalculateArea();
        }
        return totalArea;
    }
}
// También se puede simplificar con LINQ:
// public double CalculateTotalArea(List<Shape> shapes) => shapes.Sum(s => s.CalculateArea());
Ventaja: Para añadir una clase Triangle, creamos public class Triangle : Shape e implementamos su CalculateArea(). La clase AreaCalculator no necesita ningún cambio. El polimorfismo es la forma más natural y poderosa de eliminar condicionales basados en tipos.Ejercicio Propuesto 4Refactoriza un sistema que gestiona diferentes tipos de empleados. Cada tipo tiene una forma diferente de calcular su bonus.Código a refactorizar:public class Manager { public decimal Salary { get; set; } }
public class Developer { public decimal Salary { get; set; } public int LinesOfCode { get; set; } }
public class SalesPerson { public decimal Commission { get; set; } }

public class BonusCalculator
{
    public decimal CalculateBonus(object employee)
    {
        if (employee is Manager m) return m.Salary * 0.20m;
        if (employee is Developer d) return d.Salary * 0.10m + d.LinesOfCode * 0.01m;
        if (employee is SalesPerson s) return s.Commission * 0.30m;
        return 0;
    }
}
Tu tarea: Crea una clase base abstracta Employee con un método abstracto CalculateBonus() y refactoriza el sistema.Curso Intensivo: Técnicas "No If" para un Código Limpio y FlexibleIntroducción: ¿Por Qué Evitar los if?Los if-else y switch son herramientas básicas de programación, pero su uso excesivo conduce a lo que se conoce como Complejidad Ciclomática. Un método con muchos caminos condicionales es:Difícil de leer y entender: La lógica se ramifica y se vuelve enrevesada.Difícil de mantener: Añadir una nueva condición implica modificar el código existente, lo que es propenso a errores y viola el Principio de Abierto/Cerrado.Difícil de probar: Se necesita una prueba unitaria para cada rama del if.El objetivo de este curso no es eliminar todos los if, sino aprender técnicas y patrones para reemplazar la lógica de negocio compleja y condicional por diseños más robustos, flexibles y orientados a objetos.Clase 1: Adiós al switch - Diccionarios al RescateObjetivo: Reemplazar estructuras switch o cadenas de if-else que mapean una clave a una acción o valor, utilizando la simplicidad y eficiencia de un Dictionary.El Problema: Un "Factory" RígidoImagina que tienes una fábrica que crea diferentes tipos de objetos de notificación según un string.// --- ANTES ---
public interface INotifier { void Notify(string message); }
public class EmailNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"EMAIL: {m}"); }
public class SmsNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"SMS: {m}"); }
public class PushNotifier : INotifier { public void Notify(string m) => Console.WriteLine($"PUSH: {m}"); }

public class NotifierFactory
{
    // Este método viola el Principio de Abierto/Cerrado.
    // Si añadimos un nuevo notificador (ej. WhatsApp), TENEMOS que modificar esta clase.
    public INotifier GetNotifier(string notificationType)
    {
        switch (notificationType.ToLower())
        {
            case "email":
                return new EmailNotifier();
            case "sms":
                return new SmsNotifier();
            case "push":
                return new PushNotifier();
            default:
                throw new ArgumentException("Tipo de notificación no válido", nameof(notificationType));
        }
    }
}
La Solución Paso a Paso: Mapeo con DiccionariosPaso 1: Identificar el Mapeo. La lógica es simple: una clave string se mapea a la creación de un objeto. Esto es un caso de uso perfecto para un diccionario.Paso 2: Crear el Diccionario. Crearemos un Dictionary<string, Func<INotifier>>. La clave será el tipo de notificación, y el valor será una función que sabe cómo crear el notificador correspondiente. Usar Func<> nos da "creación perezosa" (lazy creation), el objeto solo se instancia cuando se necesita.Paso 3: Refactorizar el Método. El switch desaparece y es reemplazado por una simple búsqueda en el diccionario.// --- DESPUÉS ---
public class NotifierFactory
{
    private readonly Dictionary<string, Func<INotifier>> _notifiers;

    public NotifierFactory()
    {
        // El diccionario se inicializa una sola vez en el constructor.
        _notifiers = new Dictionary<string, Func<INotifier>>
        {
            { "email", () => new EmailNotifier() },
            { "sms", () => new SmsNotifier() },
            { "push", () => new PushNotifier() }
        };
    }

    public INotifier GetNotifier(string notificationType)
    {
        // El switch se reemplaza por una búsqueda. Es más eficiente y limpio.
        if (_notifiers.TryGetValue(notificationType.ToLower(), out var createNotifierFunc))
        {
            return createNotifierFunc();
        }
        
        throw new ArgumentException("Tipo de notificación no válido", nameof(notificationType));
    }
}
Ventaja: Para añadir un WhatsAppNotifier, ya no modificamos el método GetNotifier. Simplemente añadimos una nueva entrada al diccionario en el constructor. El código cumple con el Principio de Abierto/Cerrado.Ejemplo Práctico 2: Procesador de ComandosAntes:public void ExecuteCommand(string command)
{
    if (command == "play") Console.WriteLine("Reproduciendo...");
    else if (command == "pause") Console.WriteLine("Pausado.");
    else if (command == "stop") Console.WriteLine("Detenido.");
}
Después (usando Dictionary<string, Action>):private readonly Dictionary<string, Action> _commands;
public CommandProcessor()
{
    _commands = new Dictionary<string, Action>
    {
        { "play", () => Console.WriteLine("Reproduciendo...") },
        { "pause", () => Console.WriteLine("Pausado.") },
        { "stop", () => Console.WriteLine("Detenido.") }
    };
}
public void ExecuteCommand(string command)
{
    if (_commands.TryGetValue(command, out var action))
    {
        action();
    }
}
Ejercicio Propuesto 1Refactoriza el siguiente método que calcula el resultado de una operación matemática básica. Usa un Dictionary<char, Func<int, int, int>>.Código a refactorizar:public int Calculate(char operation, int a, int b)
{
    switch (operation)
    {
        case '+':
            return a + b;
        case '-':
            return a - b;
        case '*':
            return a * b;
        case '/':
            return a / b;
        default:
            throw new InvalidOperationException();
    }
}
Clase 2: El Patrón StrategyObjetivo: Reemplazar bloques if-else complejos que representan diferentes algoritmos o estrategias de negocio, encapsulando cada algoritmo en su propia clase.El Problema: Cálculo de Costos de EnvíoTenemos un método para calcular el costo de envío de un paquete, y la lógica cambia según la compañía de transporte seleccionada.// --- ANTES ---
public class Order { public decimal WeightInKg { get; set; } /* ... */ }

public class ShippingCalculator
{
    // Cada vez que añadimos una nueva compañía, este método crece y se vuelve más frágil.
    public decimal CalculateShippingCost(Order order, string provider)
    {
        decimal cost = 0;
        if (provider == "FedEx")
        {
            // Lógica compleja de FedEx
            cost = order.WeightInKg * 3.5m + 5;
        }
        else if (provider == "UPS")
        {
            // Lógica de UPS
            cost = order.WeightInKg * 4.2m;
        }
        else if (provider == "DHL")
        {
            // Lógica de DHL
            cost = 15 + (order.WeightInKg > 10 ? (order.WeightInKg - 10) * 2 : 0);
        }
        return cost;
    }
}
La Solución Paso a Paso: El Patrón StrategyPaso 1: Definir la Interfaz de la Estrategia. Creamos una interfaz que define el contrato para todas nuestras estrategias de cálculo.public interface IShippingStrategy
{
    decimal CalculateCost(Order order);
}
Paso 2: Crear Clases de Estrategia Concretas. Cada rama del if se convierte en su propia clase que implementa la interfaz.public class FedExShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(Order order) => order.WeightInKg * 3.5m + 5;
}
public class UPSShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(Order order) => order.WeightInKg * 4.2m;
}
public class DHLShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(Order order) => 15 + (order.WeightInKg > 10 ? (order.WeightInKg - 10) * 2 : 0);
}
Paso 3: Usar la Estrategia en la Clase de Contexto. La clase ShippingCalculator (o la propia clase Order) ya no contiene la lógica if. Simplemente usa el objeto de estrategia que se le proporciona.// --- DESPUÉS ---
public class ShippingCalculator
{
    // El método ahora depende de la ABSTRACCIÓN, no de la implementación.
    public decimal CalculateShippingCost(Order order, IShippingStrategy strategy)
    {
        return strategy.CalculateCost(order);
    }
}
¿Cómo se usa? El código cliente es responsable de elegir la estrategia correcta, a menudo usando el patrón Factory que vimos en la Clase 1.// Código cliente
var order = new Order { WeightInKg = 5 };
var calculator = new ShippingCalculator();

// Elegimos la estrategia (podría venir de un Factory)
IShippingStrategy fedexStrategy = new FedExShippingStrategy();

// Y calculamos. ¡El calculador no sabe qué estrategia es!
decimal cost = calculator.CalculateShippingCost(order, fedexStrategy); // cost = 22.5
Ventaja: Para añadir una nueva compañía "Estafeta", creamos la clase EstafetaShippingStrategy. La clase ShippingCalculator no se modifica en absoluto.Ejercicio Propuesto 2Refactoriza el siguiente método que exporta datos a diferentes formatos usando el Patrón Strategy.Código a refactorizar:public class Report { /* ... datos del reporte ... */ }
public class Exporter
{
    public void Export(Report report, string format)
    {
        if (format == "PDF")
        {
            Console.WriteLine("Lógica para crear un archivo PDF...");
        }
        else if (format == "CSV")
        {
            Console.WriteLine("Lógica para crear un archivo CSV...");
        }
        else if (format == "JSON")
        {
            Console.WriteLine("Lógica para serializar a JSON...");
        }
    }
}
Tu tarea: Crea una interfaz IExportStrategy y clases concretas como PdfExportStrategy, etc.Clase 3: El Patrón StateObjetivo: Eliminar condicionales que gestionan el comportamiento de un objeto basado en su estado interno. El patrón permite a un objeto alterar su comportamiento cuando su estado interno cambia.El Problema: Gestión de un DocumentoUn objeto Document puede estar en diferentes estados ("Borrador", "Publicado", "Archivado"). Los métodos como Publish() o Archive() están llenos de ifs para verificar el estado actual antes de realizar una acción.// --- ANTES ---
public class Document
{
    public string State { get; private set; } = "Borrador";

    public void Publish()
    {
        if (State == "Borrador")
        {
            Console.WriteLine("Publicando el documento...");
            State = "Publicado";
        }
        else if (State == "Publicado")
        {
            Console.WriteLine("Error: El documento ya está publicado.");
        }
        else if (State == "Archivado")
        {
            Console.WriteLine("Error: Un documento archivado no puede ser publicado.");
        }
    }
    // ... métodos Archive(), etc. con lógica similar
}
Problema: La lógica de transición de estados está dispersa y es compleja. Añadir un nuevo estado (ej. "En Revisión") requiere modificar todos los métodos.La Solución Paso a Paso: El Patrón StatePaso 1: Definir la Interfaz de Estado. Define una interfaz con los mismos métodos que dependen del estado en la clase principal.public interface IDocumentState
{
    void Publish(Document document);
    void Archive(Document document);
}
Paso 2: Crear Clases de Estado Concretas. Cada estado se convierte en su propia clase. Cada clase implementa el comportamiento específico para ese estado y gestiona la transición al siguiente estado.public class DraftState : IDocumentState
{
    public void Publish(Document document)
    {
        Console.WriteLine("Publicando el documento...");
        document.SetState(new PublishedState()); // Transición al siguiente estado
    }
    public void Archive(Document document)
    {
        Console.WriteLine("Archivando el borrador...");
        document.SetState(new ArchivedState());
    }
}

public class PublishedState : IDocumentState
{
    public void Publish(Document document) => Console.WriteLine("Error: El documento ya está publicado.");
    public void Archive(Document document)
    {
        Console.WriteLine("Archivando el documento publicado...");
        document.SetState(new ArchivedState());
    }
}

// ... Y la clase ArchivedState ...
Paso 3: Modificar la Clase de Contexto. La clase Document ahora delega las llamadas a su objeto de estado actual.// --- DESPUÉS ---
public class Document
{
    private IDocumentState _currentState;

    public Document()
    {
        _currentState = new DraftState(); // Estado inicial
    }

    // Método para que los estados puedan cambiar el estado del documento
    public void SetState(IDocumentState newState) => _currentState = newState;

    // Los métodos ahora simplemente DELEGAN la llamada al estado actual.
    public void Publish() => _currentState.Publish(this);
    public void Archive() => _currentState.Archive(this);
}
Ventaja: Toda la lógica de un estado particular está contenida en una sola clase. Para añadir un estado "En Revisión", creamos la clase ReviewState y modificamos solo los estados que pueden transicionar a él, sin tocar la clase Document.Ejercicio Propuesto 3Refactoriza la gestión de un ticket de soporte técnico. El ticket puede estar en los estados "Abierto", "En Progreso" y "Cerrado".Código a refactorizar:public class SupportTicket
{
    public string Status { get; private set; } = "Abierto";

    public void AssignToDeveloper()
    {
        if (Status == "Abierto")
        {
            Console.WriteLine("Asignando ticket...");
            Status = "En Progreso";
        }
        else { Console.WriteLine("Acción no permitida."); }
    }
    
    public void Resolve()
    {
        if (Status == "En Progreso")
        {
            Console.WriteLine("Resolviendo ticket...");
            Status = "Cerrado";
        }
        else { Console.WriteLine("Acción no permitida."); }
    }
}
Tu tarea: Implementa el patrón State con una interfaz ITicketState y clases concretas.Clase 4: Polimorfismo - El "No If" de la OOPObjetivo: Reemplazar condicionales que verifican el tipo de un objeto (if (obj is TypeA)) utilizando el comportamiento polimórfico fundamental de la Programación Orientada a Objetos.El Problema: Procesando diferentes formas geométricasTenemos una lista de figuras y queremos calcular el área total, pero la fórmula del área es diferente para cada tipo de figura.// --- ANTES ---
public class Square { public double Side { get; set; } }
public class Circle { public double Radius { get; set; } }
public class Rectangle { public double Width { get; set; } public double Height { get; set; } }

public class AreaCalculator
{
    public double CalculateTotalArea(List<object> shapes)
    {
        double totalArea = 0;
        foreach (var shape in shapes)
        {
            // ¡Esto es un "code smell" terrible!
            // Es frágil y viola OCP. Añadir un Triángulo requiere modificar este bucle.
            if (shape is Square s)
            {
                totalArea += s.Side * s.Side;
            }
            else if (shape is Circle c)
            {
                totalArea += Math.PI * c.Radius * c.Radius;
            }
            else if (shape is Rectangle r)
            {
                totalArea += r.Width * r.Height;
            }
        }
        return totalArea;
    }
}
La Solución Paso a Paso: PolimorfismoPaso 1: Crear una Abstracción Común. Se define una clase base o interfaz que todas las figuras implementarán. Esta abstracción tendrá el método que necesitamos: CalculateArea().public abstract class Shape
{
    public abstract double CalculateArea();
}
Paso 2: Heredar e Implementar. Cada clase de figura hereda de la clase base y proporciona su propia implementación de la fórmula del área.public class Square : Shape
{
    public double Side { get; set; }
    public override double CalculateArea() => Side * Side;
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double CalculateArea() => Math.PI * Radius * Radius;
}

public class Rectangle : Shape
{
    public double Width { get; set; } public double Height { get; set; }
    public override double CalculateArea() => Width * Height;
}
Paso 3: Refactorizar el Bucle. El bucle se vuelve trivial. Ya no necesita saber el tipo concreto de cada objeto. Simplemente llama al método CalculateArea() y el polimorfismo se encarga del resto.// --- DESPUÉS ---
public class AreaCalculator
{
    // La lista ahora es de tipo Shape, nuestra abstracción.
    public double CalculateTotalArea(List<Shape> shapes)
    {
        double totalArea = 0;
        foreach (var shape in shapes)
        {
            // No hay if. Simplemente llamamos al método.
            // .NET elige la implementación correcta en tiempo de ejecución.
            totalArea += shape.CalculateArea();
        }
        return totalArea;
    }
}
// También se puede simplificar con LINQ:
// public double CalculateTotalArea(List<Shape> shapes) => shapes.Sum(s => s.CalculateArea());
Ventaja: Para añadir una clase Triangle, creamos public class Triangle : Shape e implementamos su CalculateArea(). La clase AreaCalculator no necesita ningún cambio. El polimorfismo es la forma más natural y poderosa de eliminar condicionales basados en tipos.Ejercicio Propuesto 4Refactoriza un sistema que gestiona diferentes tipos de empleados. Cada tipo tiene una forma diferente de calcular su bonus.Código a refactorizar:public class Manager { public decimal Salary { get; set; } }
public class Developer { public decimal Salary { get; set; } public int LinesOfCode { get; set; } }
public class SalesPerson { public decimal Commission { get; set; } }

public class BonusCalculator
{
    public decimal CalculateBonus(object employee)
    {
        if (employee is Manager m) return m.Salary * 0.20m;
        if (employee is Developer d) return d.Salary * 0.10m + d.LinesOfCode * 0.01m;
        if (employee is SalesPerson s) return s.Commission * 0.30m;
        return 0;
    }
} 
Tu tarea: Crea una clase base abstracta Employee con un método abstracto CalculateBonus() y refactoriza el sistema.