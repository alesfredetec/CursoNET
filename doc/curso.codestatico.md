Curso Práctico: Análisis de Código y Reducción de Complejidad Escribiendo Código que Otros Puedan Entender y MantenerIntroducción: ¿Por qué mi código de ayer parece escrito por otra persona?A todos nos ha pasado: volvemos a ver un código que escribimos hace seis meses y nos cuesta entenderlo. La causa principal suele ser una alta complejidad. El análisis estático de código es el proceso de usar herramientas automáticas para analizar nuestro código fuente (sin ejecutarlo) en busca de errores, vulnerabilidades y, lo más importante para este curso, "malos olores" (code smells) que indican un diseño deficiente.Nuestro objetivo será dominar una métrica clave, la Complejidad Ciclomática, para identificar puntos problemáticos y aprender técnicas concretas para solucionarlos.Clase 1: El Guardián Silencioso - Introducción al Análisis EstáticoObjetivos:Entender qué es y para qué sirve el análisis estático de código.Instalar y configurar SonarLint en Visual Studio como nuestro analizador en tiempo real.Aprender a interpretar las sugerencias básicas de la herramienta.Paso 1: ¿Qué es el Análisis Estático?Es como tener un compañero de equipo robot, increíblemente meticuloso, que revisa tu código mientras escribes. Busca problemas potenciales basándose en un conjunto de reglas. A diferencia de las pruebas unitarias (que verifican el comportamiento del código al ejecutarlo), el análisis estático revisa la estructura y la calidad del código mismo.Categorías de Problemas que Detecta:Bugs: Errores lógicos que probablemente causarán un comportamiento incorrecto (ej. una variable null que se usa sin verificar).Vulnerabilidades: Fallos de seguridad (ej. una consulta SQL construida con concatenación de strings).Code Smells (Malos Olores): El foco de nuestro curso. No son errores, pero son señales de un problema de diseño más profundo que hará el código difícil de mantener (ej. un método demasiado largo, código duplicado, alta complejidad).Paso 2: Instalando Nuestra Herramienta - SonarLintSonarLint es una extensión gratuita para Visual Studio que nos dará feedback instantáneo.Abre Visual Studio.Ve a Extensiones > Administrar extensiones.Busca "SonarLint" en la pestaña "En línea".Descárgala e instálala. (Necesitarás reiniciar Visual Studio).[Imagen de la ventana de Extensiones de Visual Studio mostrando SonarLint]Una vez instalado, verás que SonarLint subraya tu código con "gusanitos" de diferentes colores (azul, verde, amarillo) y muestra los problemas en la ventana "Lista de errores"
.Paso 3: Un Primer Vistazo - Detectando Problemas SimplesEjemplo de Código con Problemas:public class BadCodeExample
{
    private string name; // SonarLint: "Remove this unused private field 'name'."

    public void GreetUser(string user)
    {
        // SonarLint: "Parameter 'user' can be made readonly."
        var greeting = "Hello"; // SonarLint: "Make 'greeting' a constant."
        
        Console.WriteLine(greeting + ", " + user);
    }
}
Análisis:private string name;: SonarLint detecta que este campo privado nunca se usa. Es código muerto que solo añade ruido.string user: Como el parámetro user no se modifica dentro del método, SonarLint sugiere marcarlo como readonly (una buena práctica en versiones más antiguas) o simplemente reconoce que no se reasigna.var greeting = "Hello";: Como esta variable nunca cambia, puede ser una constante (const), lo que hace la intención más clara.Ejercicio Propuesto 1Asegúrate de tener SonarLint instalado.Crea un nuevo proyecto de consola en C#.Copia y pega el siguiente código en tu archivo Program.cs.Revisa todas las advertencias que SonarLint genera en la "Lista de errores".Intenta solucionar cada una de las advertencias usando las "Acciones rápidas y refactorizaciones" (el icono del foco o bombilla) que Visual Studio te ofrece.Código para el ejercicio:public class UserProfile
{
    public string Name; // Un campo público
    private int age;    // Un campo privado no utilizado

    public UserProfile(string name)
    {
        Name = name;
    }

    public string GetProfileInfo(bool includeAge)
    {
        string result = "User: " + this.Name;
        if (includeAge == true) // Condición redundante
        {
            // Lógica que nunca se usa
        }
        return result;
    }
}
Clase 2: La Métrica Reina - ¿Qué es la Complejidad Ciclomática?Objetivos:Entender qué mide la Complejidad Ciclomática.Aprender a calcularla (o al menos a estimarla mentalmente).Comprender por qué una complejidad alta es un indicador de problemas futuros.Paso 1: Definiendo la ComplejidadLa Complejidad Ciclomática mide el número de "caminos" linealmente independientes a través del código de un método. En términos simples, es un recuento de la cantidad de decisiones que toma tu código.Un método sin if, for, while, etc., tiene una complejidad de 1 (un solo camino de principio a fin).Cada vez que añades un punto de decisión, la complejidad aumenta.La Regla del Pulgar (versión simplificada):Comienza con 1 (por el método en sí).Añade +1 por cada uno de estos: if, else if, for, foreach, while, do-while, case (en un switch), catch.Añade +1 por cada operador condicional ternario (? :) y por cada operador && y || en una condición.Paso 2: ¿Por Qué nos Importa?Un método con alta complejidad es:Difícil de leer y entender: Tienes que mantener muchos caminos posibles en tu cabeza.Difícil de probar: Para tener una cobertura de código completa, necesitas al menos un caso de prueba por cada camino posible. ¡Un método con complejidad 20 necesitaría 20 pruebas!Propenso a errores: Es mucho más probable que introduzcas un bug en un método complejo al intentar modificarlo.Umbrales Generalmente Aceptados:1-10: Bueno. El método es simple y fácil de gestionar.11-20: Complejo. Empieza a ser difícil de entender y probar. Señal de que necesita refactorización.>20: Muy Complejo. Alto riesgo. Refactorizar es urgente.Paso 3: Calculando la Complejidad - Un Ejemplo Paso a PasoMétodo de Ejemplo:public string GetUserStatus(User user)
{
    // 1 (punto de partida)
    if (user == null) // +1
    {
        return "Invalid";
    }

    if (user.IsAdmin) // +1
    {
        return "Admin";
    }

    if (user.LoginCount > 100 && user.LastLogin > DateTime.Now.AddDays(-30)) // +2 (uno por el '&&')
    {
        return "Active";
    }
    
    return "Inactive";
}
Cálculo: 1 (base) + 1 (if null) + 1 (if admin) + 1 (if login) + 1 (&&) = 5. La complejidad ciclomática de este método es 5. Es manejable.Ejercicio Propuesto 2Calcula la complejidad ciclomática del siguiente método. Justifica tu recuento.Código a analizar:public decimal CalculateDiscount(Order order, Customer customer)
{
    // 1 (base)
    decimal discount = 0;
    if (customer.IsVip) // +1
    {
        discount = 0.15m;
    }

    foreach (var lineItem in order.LineItems) // +1
    {
        if (lineItem.Product.IsOnSale || lineItem.Quantity > 10) // +2
        {
            discount += 0.05m;
        }
    }

    if (order.TotalAmount > 1000) // +1
    {
        return discount + 0.02m;
    }
    else
    {
        return discount;
    }
}
Clase 3: Técnicas de Refactoring para Reducir la ComplejidadObjetivos:Usar las herramientas de Visual Studio para medir la complejidad.Aplicar la técnica "Extraer Método" para simplificar un método largo.Aplicar "Cláusulas de Guarda" (Guard Clauses) para reducir el anidamiento.Paso 1: Medir en Lugar de AdivinarVisual Studio tiene una herramienta incorporada para medir la complejidad.Haz clic derecho en tu proyecto o solución en el "Explorador de soluciones".Selecciona Analizar > Calcular métricas de código.Se abrirá una ventana "Resultados de las métricas de código" que te mostrará, entre otras cosas, la Complejidad Ciclomática para cada método.Paso 2: Ejemplo Guiado - Refactorizando un Método ComplejoCódigo Inicial (Complejidad Alta):public void ProcessUser(User user)
{
    if (user != null)
    {
        if (user.IsActive)
        {
            if (user.HasProfile)
            {
                // Compleja lógica de procesamiento del perfil
                Console.WriteLine($"Procesando perfil para {user.Name}...");
                var data = FetchDataFromDatabase(user.Id);
                if(data != null) 
                {
                    // Más lógica...
                    Console.WriteLine("Datos procesados.");
                }

                // Lógica de envío de email
                Console.WriteLine("Enviando email de bienvenida...");
            }
            else
            {
                Console.WriteLine("Usuario no tiene perfil.");
            }
        }
        else
        {
            Console.WriteLine("Usuario inactivo.");
        }
    }
    else
    {
        throw new ArgumentNullException(nameof(user));
    }
}
Este código sufre de anidamiento profundo (el "código de flecha").Refactorización 1: Aplicar Cláusulas de Guarda (Guard Clauses)Las cláusulas de guarda verifican las precondiciones al inicio del método y salen temprano. Esto elimina el primer nivel de anidamiento.public void ProcessUser(User user)
{
    // Cláusulas de guarda
    if (user == null) throw new ArgumentNullException(nameof(user));
    if (!user.IsActive)
    {
        Console.WriteLine("Usuario inactivo.");
        return;
    }
    if (!user.HasProfile)
    {
        Console.WriteLine("Usuario no tiene perfil.");
        return;
    }

    // El código principal ahora no está anidado
    // Compleja lógica de procesamiento del perfil
    Console.WriteLine($"Procesando perfil para {user.Name}...");
    var data = FetchDataFromDatabase(user.Id);
    if (data != null)
    {
        // Más lógica...
        Console.WriteLine("Datos procesados.");
    }
    
    // Lógica de envío de email
    Console.WriteLine("Enviando email de bienvenida...");
}
Refactorización 2: Aplicar "Extraer Método"Identificamos bloques de lógica cohesivos y los extraemos a sus propios métodos privados y bien nombrados.public void ProcessUser(User user)
{
    if (user == null) throw new ArgumentNullException(nameof(user));
    if (!user.IsActive) { /* ... */ return; }
    if (!user.HasProfile) { /* ... */ return; }

    // El método principal ahora es un resumen de alto nivel de lo que hace
    ProcessUserProfile(user);
    SendWelcomeEmail(user);
}

private void ProcessUserProfile(User user)
{
    Console.WriteLine($"Procesando perfil para {user.Name}...");
    var data = FetchDataFromDatabase(user.Id);
    if (data != null)
    {
        // Más lógica...
        Console.WriteLine("Datos procesados.");
    }
}

private void SendWelcomeEmail(User user)
{
    Console.WriteLine($"Enviando email de bienvenida para el usuario {user.Name}...");
}
Resultado: La complejidad del método ProcessUser ha disminuido drásticamente. Ahora es fácil de leer y cada método extraído tiene una única responsabilidad y una complejidad muy baja.Ejercicio Propuesto 3Refactoriza el siguiente método usando Cláusulas de Guarda y la técnica de Extraer Método para reducir su complejidad.Código a refactorizar:public bool SubmitApplication(ApplicationForm form)
{
    if (form != null)
    {
        // Validar nombre
        if (!string.IsNullOrWhiteSpace(form.ApplicantName))
        {
            // Validar edad
            if (form.ApplicantAge >= 18)
            {
                // Procesar y guardar en DB
                Console.WriteLine("Guardando aplicación en la base de datos...");
                var applicationId = SaveToDatabase(form);
                
                // Enviar notificaciones
                Console.WriteLine("Enviando email al aplicante...");
                Console.WriteLine("Notificando al departamento de RRHH...");
                
                return true;
            }
            else
            {
                Console.WriteLine("Error: El aplicante es menor de edad.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Error: El nombre del aplicante es requerido.");
            return false;
        }
    }
    return false;
}
Clase 4: Patrones de Diseño "Anti-Complejidad"Objetivos:Ir más allá del refactoring simple y aplicar patrones de diseño para eliminar la causa raíz de la complejidad.Aprender a reemplazar condicionales con Polimorfismo y el Patrón Strategy.El Problema: Lógica Condicional Basada en un "Tipo"A menudo, la alta complejidad proviene de un gran switch o cadena if-else if que cambia el comportamiento del sistema basándose en el valor de una variable (un enum, un string, etc.).Ejemplo: Un sistema que calcula el costo de una factura según el tipo de cliente.public enum CustomerType { Standard, Premium, Vip }
public class InvoiceCalculator
{
    // Este método viola el Principio de Abierto/Cerrado.
    // Añadir un tipo "Corporate" requiere modificarlo.
    public decimal CalculateTotal(decimal amount, CustomerType type)
    {
        switch (type)
        {
            case CustomerType.Standard:
                return amount;
            case CustomerType.Premium:
                return amount * 0.9m; // 10% descuento
            case CustomerType.Vip:
                return amount * 0.8m; // 20% descuento
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}
La Solución: Patrón StrategyEncapsulamos cada rama de la lógica en su propia clase ("estrategia").Paso 1: Crear la Interfaz de la Estrategiapublic interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal amount);
}
Paso 2: Crear Estrategias Concretaspublic class StandardDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount) => amount;
}
public class PremiumDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount) => amount * 0.9m;
}
public class VipDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount) => amount * 0.8m;
}
Paso 3: Usar un Diccionario o Factory para Elegir la Estrategiapublic class InvoiceCalculator
{
    private readonly Dictionary<CustomerType, IDiscountStrategy> _strategies;

    public InvoiceCalculator()
    {
        _strategies = new Dictionary<CustomerType, IDiscountStrategy>
        {
            { CustomerType.Standard, new StandardDiscountStrategy() },
            { CustomerType.Premium, new PremiumDiscountStrategy() },
            { CustomerType.Vip, new VipDiscountStrategy() }
        };
    }

    public decimal CalculateTotal(decimal amount, CustomerType type)
    {
        // Complejidad = 1 (después de la validación)
        if (_strategies.TryGetValue(type, out var strategy))
        {
            return strategy.ApplyDiscount(amount);
        }
        throw new NotSupportedException("Tipo de cliente no soportado.");
    }
}
Resultado: El método CalculateTotal ahora tiene una complejidad mínima. Para añadir un nuevo tipo de cliente Corporate, simplemente creamos la clase CorporateDiscountStrategy y la añadimos al diccionario. No modificamos el código existente.Ejercicio Propuesto 4Tienes un método que genera diferentes tipos de reportes (PDF, CSV, Excel) basándose en un parámetro.Código a refactorizar:public class ReportGenerator
{
    public byte[] GenerateReport(string reportType, ReportData data)
    {
        if (reportType == "PDF")
        {
            // Lógica compleja para generar un PDF...
            Console.WriteLine("Generando PDF...");
            return new byte[0];
        }
        else if (reportType == "CSV")
        {
            // Lógica compleja para generar un CSV...
            Console.WriteLine("Generando CSV...");
            return new byte[0];
        }
        else if (reportType == "Excel")
        {
            // Lógica compleja para generar un Excel...
            Console.WriteLine("Generando Excel...");
            return new byte[0];
        }
        return null;
    }
}

Tu Tarea: Refactoriza este método usando el Patrón Strategy. Crea una interfaz IReportStrategy y clases concretas. Usa un diccionario para mapear el string del tipo de reporte a la estrategia correcta.

otras hrramientas y libreris para .net core .

tips