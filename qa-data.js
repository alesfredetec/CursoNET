const qaData = [
    {
        "category": "Análisis Estático",
        "question": "¿Qué es la complejidad ciclomática y por qué es importante?",
        "answer": "Mide el número de caminos de ejecución independientes en un método. Una complejidad alta (>10) indica que el código es difícil de leer, probar y mantener, aumentando el riesgo de bugs.",
        "source": "Thomas J. McCabe, Sr.",
        "sourceUrl": "https://en.wikipedia.org/wiki/Cyclomatic_complexity"
    },
    {
        "category": "Análisis Estático",
        "question": "Nombra 3 tipos de problemas que detecta el análisis estático.",
        "answer": "1. **Bugs:** Errores lógicos (ej. uso de variable nula). 2. **Vulnerabilidades:** Fallos de seguridad (ej. SQL Injection). 3. **Code Smells:** Problemas de diseño (ej. métodos largos, código duplicado)."
    },
    {
        "category": "Análisis Estático",
        "question": "¿Qué son las Guard Clauses y qué problema resuelven?",
        "answer": "Son verificaciones de precondiciones al inicio de un método para salir temprano (return/throw). Resuelven el problema del anidamiento profundo de `if`s (código de flecha), mejorando la legibilidad.",
        "source": "Martin Fowler, Refactoring.Guru",
        "sourceUrl": "https://refactoring.guru/replace-nested-conditional-with-guard-clauses"
    },
    {
        "category": "Análisis Estático",
        "question": "¿Qué es la técnica 'Extract Method'?",
        "answer": "Consiste en tomar un fragmento de código cohesivo dentro de un método largo y moverlo a su propio método privado con un nombre descriptivo. Ayuda a cumplir el Principio de Responsabilidad Única (SRP).",
        "source": "Martin Fowler, Refactoring.Guru",
        "sourceUrl": "https://refactoring.guru/extract-method"
    },
    {
        "category": "Análisis Estático",
        "question": "¿Qué es un 'code smell'? Da dos ejemplos.",
        "answer": "Un 'code smell' no es un bug, sino una señal de un problema de diseño más profundo. Ejemplos: **Long Method** (un método que hace demasiadas cosas) y **Duplicate Code** (código copiado y pegado en varios lugares).",
        "source": "Martin Fowler & Kent Beck",
        "sourceUrl": "https://martinfowler.com/bliki/CodeSmell.html"
    },
    {
        "category": "Análisis Estático",
        "question": "¿Cómo ayuda SonarLint en el desarrollo diario?",
        "answer": "Proporciona análisis en tiempo real dentro del IDE (Visual Studio), detectando problemas de calidad, bugs y vulnerabilidades mientras se escribe el código, lo que permite corregirlos de inmediato."
    },
    {
        "category": "Análisis Estático",
        "question": "¿Qué es el Índice de Mantenibilidad?",
        "answer": "Es una métrica calculada (generalmente de 0 a 100) que indica la facilidad de mantener el código. Un valor alto es bueno. Se basa en métricas como la complejidad ciclomática y las líneas de código."
    },
    {
        "category": "Análisis Estático",
        "question": "¿Por qué es preferible usar constantes en lugar de 'magic numbers'?",
        "answer": "Las constantes con nombres descriptivos (ej. `MaxLoginAttempts`) hacen el código más legible, evitan errores de tipeo y centralizan los valores, facilitando su modificación en un solo lugar."
    },
    {
        "category": "Análisis Estático",
        "question": "¿Qué problema soluciona la técnica 'Introduce Class'?",
        "answer": "Soluciona el problema de una clase que tiene demasiadas responsabilidades (God Class). Se extrae un conjunto de datos y métodos relacionados a una nueva clase, mejorando la cohesión y el SRP."
    },
    {
        "category": "Análisis Estático",
        "question": "¿Qué es un 'Quality Gate' en un pipeline de CI/CD?",
        "answer": "Es un conjunto de condiciones de calidad (ej. complejidad ciclomática < 10, cobertura de tests > 80%) que el código debe cumplir para pasar a la siguiente etapa del pipeline (ej. despliegue)."
    },
    {
        "category": "Requisitos",
        "question": "Diferencia entre Requisitos Funcionales (FR) y No Funcionales (NFR).",
        "answer": "Los **FR** definen **qué** debe hacer el sistema (ej. 'el usuario puede iniciar sesión'). Los **NFR** definen **cómo de bien** debe hacerlo (ej. 'el login debe tardar menos de 1 segundo')."
    },
    {
        "category": "Requisitos",
        "question": "¿Cuál es el formato de una Historia de Usuario (Connextra)?",
        "answer": "**Como** [tipo de usuario], **quiero** [realizar una acción], **para** [obtener un beneficio]. Este formato centra el requisito en el valor para el usuario.",
        "source": "Mike Cohn, \"User Stories Applied\"",
        "sourceUrl": "https://www.mountaingoatsoftware.com/agile/user-stories"
    },
    {
        "category": "Requisitos",
        "question": "¿Qué es Gherkin y para qué se utiliza?",
        "answer": "Es un lenguaje legible por humanos para escribir Criterios de Aceptación en formato **Given-When-Then**. Se utiliza en BDD (Behavior Driven Development) para definir el comportamiento esperado del sistema de forma clara y automatizable."
    },
    {
        "category": "Requisitos",
        "question": "¿Qué significa el acrónimo INVEST para las Historias de Usuario?",
        "answer": "Significa: **I**ndependiente, **N**egociable, **V**aliosa, **E**stimable, **S**mall (Pequeña), y **T**esteable. Son los criterios de una buena historia de usuario.",
        "source": "Bill Wake",
        "sourceUrl": "https://xp123.com/articles/invest-in-good-stories-and-smart-tasks/"
    },
    {
        "category": "Requisitos",
        "question": "¿Por qué son críticos los NFRs en sistemas de alta concurrencia?",
        "answer": "Porque definen atributos como rendimiento (TPS), disponibilidad (uptime) y escalabilidad. Un sistema que es funcionalmente correcto pero no puede manejar la carga de usuarios es inútil en producción."
    },
    {
        "category": "Requisitos",
        "question": "¿Qué es una Matriz de Trazabilidad de Requisitos?",
        "answer": "Es un documento que mapea y sigue la vida de un requisito, conectándolo con su origen, diseño, código y casos de prueba. Asegura que todos los requisitos han sido implementados y probados."
    },
    {
        "category": "Requisitos",
        "question": "¿Qué es la 'Definition of Done' (DoD)?",
        "answer": "Es un checklist de criterios que una historia de usuario debe cumplir para ser considerada completa. Incluye aspectos como código terminado, pruebas unitarias pasadas, revisión de código y documentación actualizada."
    },
    {
        "category": "Requisitos",
        "question": "Da un ejemplo de un requisito de seguridad no funcional.",
        "answer": "'El sistema debe encriptar todas las contraseñas de usuario en la base de datos usando el algoritmo bcrypt.' o 'Todas las comunicaciones entre el cliente y el servidor deben usar TLS 1.3.'"
    },
    {
        "category": "Requisitos",
        "question": "¿Qué es un SLI, SLO y SLA?",
        "answer": "**SLI (Indicador):** Una métrica (ej. latencia). **SLO (Objetivo):** El objetivo para esa métrica (ej. latencia < 200ms). **SLA (Acuerdo):** Un contrato con consecuencias si el SLO no se cumple (ej. penalización económica)."
    },
    {
        "category": "Requisitos",
        "question": "¿Qué es un ADR (Architecture Decision Record)?",
        "answer": "Es un documento corto que captura una decisión arquitectónica importante, su contexto, las alternativas consideradas y las consecuencias de la decisión. Ayuda a mantener un registro del porqué de la arquitectura."
    },
    {
        "category": "Técnicas No If",
        "question": "¿Cómo se puede reemplazar un `switch` usando un diccionario?",
        "answer": "Creando un `Dictionary<TKey, TValue>` donde la clave es la condición del `case` y el valor es una `Action` o `Func<>` que ejecuta la lógica correspondiente. Esto cumple el Principio de Abierto/Cerrado."
    },
    {
        "category": "Técnicas No If",
        "question": "¿Cuándo es apropiado usar el Patrón Strategy?",
        "answer": "Cuando tienes un conjunto de algoritmos intercambiables para una misma tarea y quieres que el cliente elija cuál usar en tiempo de ejecución. Ideal para lógicas de negocio complejas y variables (ej. cálculo de envíos, métodos de pago).",
        "source": "Gang of Four, Design Patterns",
        "sourceUrl": "https://refactoring.guru/design-patterns/strategy"
    },
    {
        "category": "Técnicas No If",
        "question": "¿Qué problema soluciona el Patrón State?",
        "answer": "Soluciona el problema de un objeto cuyo comportamiento cambia drásticamente según su estado interno. Encapsula cada estado en su propia clase, eliminando condicionales complejos que comprueban el estado actual.",
        "source": "Gang of Four, Design Patterns",
        "sourceUrl": "https://refactoring.guru/design-patterns/state"
    },
    {
        "category": "Técnicas No If",
        "question": "¿Cómo ayuda el polimorfismo a eliminar condicionales?",
        "answer": "Permite que diferentes objetos respondan al mismo mensaje (llamada de método) de formas específicas a su tipo. Reemplaza `if (obj is TypeA)` con una llamada a un método virtual o de interfaz, donde cada clase implementa su propia lógica."
    },
    {
        "category": "Técnicas No If",
        "question": "¿Qué ventaja ofrece usar `Func<>` en un Dictionary para un Factory?",
        "answer": "Permite la 'creación perezosa' (lazy creation). El objeto no se instancia hasta que la función es explícitamente llamada, lo que es más eficiente si no todos los tipos se usan siempre."
    },
    {
        "category": "Técnicas No If",
        "question": "En el Patrón State, ¿quién es responsable de la transición de estado?",
        "answer": "La responsabilidad recae en la clase de estado concreta. Cuando se ejecuta una acción, el estado actual no solo realiza la lógica, sino que también decide cuál será el siguiente estado y le indica al objeto de contexto que realice la transición."
    },
    {
        "category": "Técnicas No If",
        "question": "¿Cuál es la diferencia fundamental entre el Patrón Strategy y el Patrón State?",
        "answer": "En **Strategy**, el cliente generalmente elige y pasa el algoritmo a usar. En **State**, el propio objeto de contexto o sus estados gestionan las transiciones de un estado a otro internamente."
    },
    {
        "category": "Técnicas No If",
        "question": "¿Qué es un 'code smell' de type checking?",
        "answer": "Es el uso repetido de `if (obj is TypeA)` o `switch (obj.Type)` para cambiar el comportamiento. Es una señal de que se debería usar polimorfismo."
    },
    {
        "category": "Técnicas No If",
        "question": "¿Se pueden combinar estos patrones?",
        "answer": "Sí. Es muy común usar un **Factory** (implementado con un Dictionary) para crear diferentes **Strategies** y pasarlas a un objeto de contexto."
    },
    {
        "category": "Técnicas No If",
        "question": "¿Qué principio SOLID se aplica más directamente con estas técnicas?",
        "answer": "El **Principio de Abierto/Cerrado (OCP)**, ya que permiten añadir nueva funcionalidad (nuevas estrategias, estados o tipos) sin modificar el código existente que los utiliza."
    },
    {
        "category": "SOLID",
        "question": "Explica el Principio de Responsabilidad Única (SRP).",
        "answer": "Una clase debe tener una, y solo una, razón para cambiar. Esto significa que debe tener una única responsabilidad o propósito bien definido.",
        "source": "Robert C. Martin (Uncle Bob)",
        "sourceUrl": "https://blog.cleancoder.com/uncle-bob/2014/05/08/SingleReponsibilityPrinciple.html"
    },
    {
        "category": "SOLID",
        "question": "¿Qué es el Principio de Abierto/Cerrado (OCP)?",
        "answer": "Las entidades de software (clases, módulos, funciones) deben estar abiertas para la extensión, pero cerradas para la modificación. Se logra mediante abstracciones y patrones como Strategy o Decorator.",
        "source": "Robert C. Martin (Uncle Bob)",
        "sourceUrl": "https://blog.cleancoder.com/uncle-bob/2014/05/12/TheOpenClosedPrinciple.html"
    },
    {
        "category": "SOLID",
        "question": "¿Qué implica el Principio de Inversión de Dependencias (DIP)?",
        "answer": "Los módulos de alto nivel no deben depender de los módulos de bajo nivel. Ambos deben depender de abstracciones. Las abstracciones no deben depender de los detalles; los detalles deben depender de las abstracciones.",
        "source": "Robert C. Martin (Uncle Bob)",
        "sourceUrl": "https://blog.cleancoder.com/uncle-bob/2014/05/14/TheDependencyInversionPrinciple.html"
    },
    {
        "category": "SOLID",
        "question": "Describe el Principio de Sustitución de Liskov (LSP).",
        "answer": "Los subtipos deben ser sustituibles por sus tipos base sin alterar la corrección del programa. Si una clase `Pato` hereda de `Ave`, un `Pato` debe poder usarse en cualquier lugar donde se espere un `Ave`.",
        "source": "Robert C. Martin (Uncle Bob)",
        "sourceUrl": "https://blog.cleancoder.com/uncle-bob/2014/05/11/LiskovSubstitutionPrinciple.html"
    },
    {
        "category": "SOLID",
        "question": "¿Qué problema resuelve el Principio de Segregación de Interfaces (ISP)?",
        "answer": "Evita que los clientes se vean obligados a depender de interfaces que no utilizan. Es mejor tener muchas interfaces pequeñas y específicas que una sola interfaz grande y genérica.",
        "source": "Robert C. Martin (Uncle Bob)",
        "sourceUrl": "https://blog.cleancoder.com/uncle-bob/2014/05/10/InterfaceSegregationPrinciple.html"
    },
    {
        "category": "SOLID",
        "question": "¿Cómo se relaciona el Patrón Factory con el OCP?",
        "answer": "Un Factory permite añadir nuevos tipos de productos sin modificar el código cliente que los solicita. El cliente sigue pidiendo productos al factory, y solo el factory necesita ser modificado (o extendido) para conocer el nuevo tipo."
    },
    {
        "category": "SOLID",
        "question": "¿Cómo ayuda la Inyección de Dependencias a cumplir con DIP?",
        "answer": "La Inyección de Dependencias es la técnica principal para implementar DIP. En lugar de que una clase cree sus propias dependencias (concreciones), estas se le 'inyectan' desde fuera como abstracciones (interfaces), invirtiendo el control."
    },
    {
        "category": "SOLID",
        "question": "¿Qué patrón de diseño es una manifestación directa del OCP?",
        "answer": "El Patrón Strategy es un ejemplo clásico. El contexto está cerrado a modificaciones, pero puedes extender el sistema añadiendo nuevas clases de estrategia."
    },
    {
        "category": "SOLID",
        "question": "¿Un método con 100 líneas viola necesariamente el SRP?",
        "answer": "No necesariamente, pero es un fuerte indicio (code smell). Si esas 100 líneas implementan un único algoritmo cohesivo, podría estar bien. Sin embargo, es muy probable que esté realizando múltiples tareas (validación, cálculo, formato, etc.) y sí viole el SRP."
    },
    {
        "category": "SOLID",
        "question": "¿Cómo se relaciona el Patrón Observer con el DIP?",
        "answer": "El sujeto (publisher) no depende de los observadores concretos, sino de una abstracción (interfaz `IObserver`). Esto permite que cualquier clase que implemente la interfaz se suscriba, cumpliendo con el DIP."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Qué son las generaciones en el GC de .NET y para qué sirven?",
        "answer": "Son divisiones lógicas del heap (Gen 0, 1, 2) para optimizar la recolección. Se basan en la hipótesis de que los objetos jóvenes mueren rápido. El GC recolecta la Gen 0 (objetos nuevos) con más frecuencia, lo que es más rápido y eficiente."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Qué es un recurso no gestionado y cómo se debe manejar?",
        "answer": "Es un recurso que no está bajo el control directo del CLR, como handles de archivos, conexiones de red o de base de datos. Se deben manejar implementando la interfaz `IDisposable` y liberando el recurso en el método `Dispose()`, preferiblemente usando un bloque `using`."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Cuál es la causa más común de memory leaks en C# con manejadores de eventos?",
        "answer": "Ocurre cuando un objeto de larga vida (el publicador) mantiene una referencia a un objeto de corta vida (el suscriptor) a través de un evento. Si el suscriptor no se desregistra del evento (`-=`), el GC no puede recolectarlo, creando una fuga de memoria."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Para qué sirve `GC.SuppressFinalize(this)`?",
        "answer": "Se llama en el método `Dispose()` para indicarle al GC que el objeto ya ha sido limpiado y no necesita ser puesto en la cola de finalización. Es una optimización que evita que el objeto sea promovido innecesariamente a la siguiente generación."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Qué es el Large Object Heap (LOH) y qué implicaciones tiene?",
        "answer": "Es un área del heap para objetos grandes (>85KB). El LOH no se compacta, lo que puede causar fragmentación de memoria. Las recolecciones en el LOH son costosas (siempre son de Gen 2), por lo que se deben evitar alocaciones frecuentes de objetos grandes."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Cuándo deberías usar un `struct` en lugar de una `class`?",
        "answer": "Usa un `struct` para tipos pequeños que son lógicamente valores únicos e inmutables (como `Point`, `Color`). Los structs son tipos de valor y se alocan en el stack (si son variables locales), evitando la presión sobre el GC."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Qué ventaja ofrece `ArrayPool`?",
        "answer": "Permite reutilizar arrays grandes en lugar de crear nuevos constantemente. Esto reduce drásticamente las alocaciones en el Large Object Heap (LOH), disminuye la fragmentación y reduce la frecuencia de las costosas recolecciones de Gen 2."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Qué es una WeakReference y para qué se usa?",
        "answer": "Es una referencia a un objeto que no impide que el GC lo recolecte. Se usa comúnmente para implementar cachés que almacenan objetos grandes o costosos, permitiendo que la memoria se libere si hay presión en el sistema."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Qué es el patrón Dispose(bool disposing)?",
        "answer": "Es el patrón de implementación estándar de `IDisposable`. El método `Dispose()` público llama a `Dispose(true)` y a `GC.SuppressFinalize()`. El finalizador llama a `Dispose(false)`. El booleano `disposing` distingue entre la limpieza de recursos gestionados y no gestionados."
    },
    {
        "category": "Garbage Collection",
        "question": "¿Qué es `IAsyncDisposable` y cuándo se usa?",
        "answer": "Es una interfaz para la limpieza asíncrona de recursos. Se usa cuando la liberación de un recurso es una operación de I/O, como vaciar un buffer a un archivo o cerrar una conexión de red de forma gradual. Se utiliza con la sintaxis `await using`."
    },
    {
        "category": "SQL e Índices",
        "question": "Diferencia clave entre un índice Clustered y uno Non-Clustered.",
        "answer": "Una tabla solo puede tener **un** índice Clustered, ya que este define el orden físico de los datos en el disco. Puede tener **múltiples** índices Non-Clustered, que son estructuras separadas con punteros a las filas de datos."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Qué es un 'Covering Index' y qué problema soluciona?",
        "answer": "Es un índice Non-Clustered que contiene todas las columnas requeridas por una consulta (usando la cláusula `INCLUDE`). Soluciona el problema de los 'Key Lookups', permitiendo que la consulta se resuelva leyendo únicamente el índice, sin tocar la tabla principal, lo que mejora drásticamente el rendimiento."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Por qué es importante el orden de las columnas en un índice compuesto?",
        "answer": "Porque el índice solo se puede usar eficientemente si la consulta filtra por la columna más a la izquierda del índice (o una combinación que empiece por ella). La columna con mayor selectividad (más valores únicos) debe ir primero para filtrar la mayor cantidad de datos posible."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Qué es un 'Table Scan' en un plan de ejecución y por qué es malo?",
        "answer": "Un 'Table Scan' (o 'Clustered Index Scan') significa que SQL Server está leyendo cada una de las filas de la tabla para encontrar los datos solicitados. Es malo porque es la operación más lenta e ineficiente, especialmente en tablas grandes, y es un claro indicador de que falta un índice adecuado."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Qué es la fragmentación de índices y cómo se soluciona?",
        "answer": "Ocurre cuando el orden lógico de las páginas del índice no coincide con el orden físico en el disco, debido a muchas operaciones de INSERT, UPDATE, DELETE. Se soluciona con `REORGANIZE` (para fragmentación baja/media) o `REBUILD` (para fragmentación alta)."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Qué es la selectividad de un índice?",
        "answer": "Es una medida de cuán únicos son los valores en una columna. Una columna con alta selectividad (como un email) es un buen candidato para un índice porque cada valor reduce drásticamente el número de filas a buscar."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Qué es un 'Key Lookup' y cómo se evita?",
        "answer": "Es una operación costosa que ocurre cuando un índice Non-Clustered no contiene todas las columnas que la consulta necesita. SQL Server usa el índice para encontrar las filas y luego debe ir a la tabla principal para obtener los datos restantes. Se evita usando un Covering Index."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Qué es un 'Filtered Index'?",
        "answer": "Es un índice Non-Clustered optimizado que solo incluye filas que cumplen una condición específica (una cláusula `WHERE`). Son más pequeños y eficientes para consultas que operan sobre un subconjunto conocido de datos (ej. `WHERE IsActive = 1`)."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Cómo se configuran los índices en Entity Framework Core?",
        "answer": "Se configuran en el método `OnModelCreating` del `DbContext` usando la API fluida, por ejemplo: `modelBuilder.Entity<Product>().HasIndex(p => p.Sku);`."
    },
    {
        "category": "SQL e Índices",
        "question": "¿Qué son las Vistas de Gestión Dinámica (DMV) en SQL Server?",
        "answer": "Son vistas del sistema que exponen información interna sobre el estado y rendimiento del servidor. Son cruciales para el diagnóstico, por ejemplo, `sys.dm_db_missing_index_details` sugiere índices que podrían mejorar el rendimiento."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Qué es el patrón EAV y cuál es su principal desventaja?",
        "answer": "EAV (Entity-Attribute-Value) es un patrón donde los datos se almacenan en tres tablas: Entidades, Atributos y Valores. Su principal desventaja es el pésimo rendimiento, ya que para reconstruir una sola entidad se requieren múltiples JOINs, lo que lo hace impráctico para la mayoría de los sistemas."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "Describe el modelo de base de datos híbrido con JSON.",
        "answer": "Combina lo mejor de ambos mundos: una tabla con columnas fijas, normalizadas y fuertemente tipadas para los datos 'core' (críticos y de búsqueda), y una columna de tipo JSON para almacenar atributos flexibles y dinámicos. Esto proporciona rendimiento y flexibilidad."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Cómo se puede indexar eficientemente una columna JSON en SQL Server?",
        "answer": "Creando propiedades computadas que extraen valores escalares del JSON usando `JSON_VALUE`, y luego creando un índice estándar sobre esa propiedad computada. Esto permite búsquedas rápidas sobre campos específicos dentro del JSON."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Qué es un 'Meta-Modelo' en el diseño de bases de datos?",
        "answer": "Es un 'esquema como dato'. Consiste en tener tablas (ej. `MetaEntities`, `MetaAttributes`) que describen la estructura de otras entidades dinámicas. La aplicación lee estos metadatos para generar UI, validaciones y lógica de forma dinámica, sin necesidad de cambiar el código o el esquema de la base de datos."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Qué estrategia se usa para evitar la pérdida de datos al eliminar un atributo en un sistema basado en metadatos?",
        "answer": "Se utiliza el borrado suave (soft delete). En lugar de un `DELETE` físico de la tabla de metadatos, se marca el atributo como inactivo (ej. con una columna `IsActive = 0`). La UI deja de mostrarlo para nuevos registros, pero los datos existentes permanecen intactos."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Por qué es crucial cachear los metadatos en una arquitectura dirigida por metadatos?",
        "answer": "Porque las tablas de metadatos se leen constantemente para generar UI y aplicar validaciones, pero cambian muy poco. Cachearlas en memoria (ej. Redis o IMemoryCache) evita miles de consultas a la base de datos y hace que la aplicación sea mucho más rápida."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿En qué se diferencia el soporte JSON de PostgreSQL (JSONB) del de SQL Server?",
        "answer": "PostgreSQL usa un formato binario optimizado (JSONB) y soporta índices GIN, que pueden indexar toda la estructura del documento JSON de manera eficiente para búsquedas por cualquier clave o valor, lo que es muy poderoso."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Qué es la gobernanza de datos en un esquema dinámico?",
        "answer": "Es el conjunto de políticas y procesos para controlar cómo evoluciona el esquema dinámico. Incluye flujos de aprobación para nuevos atributos, versionado de esquemas, análisis de impacto de los cambios y estrategias de migración de datos."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Para qué tipo de aplicación es ideal un diseño basado en metadatos?",
        "answer": "Es ideal para sistemas que requieren alta flexibilidad y personalización, como plataformas SaaS multi-tenant, sistemas de gestión de contenido (CMS), constructores de formularios (form builders) o plataformas low-code/no-code."
    },
    {
        "category": "Diseño de BD Avanzado",
        "question": "¿Cuál es el principal desafío de rendimiento del patrón EAV que el modelo híbrido con JSON resuelve?",
        "answer": "El principal desafío de EAV es la necesidad de realizar múltiples y costosos JOINs para reconstruir una sola entidad. El modelo híbrido resuelve esto almacenando todos los atributos flexibles en una única columna JSON, eliminando la necesidad de JOINs para esos datos."
    }
];
