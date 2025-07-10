# GEMINI.md - Guía para la Asistencia de IA

## Contexto del Proyecto

Este proyecto contiene los materiales para un curso de Análisis y Diseño de Software enfocado en desarrolladores Junior que aspiran a un nivel Semi-Senior. El curso es impartido por arquitectos y se centra en la escritura de código limpio, mantenible y extensible.

## Principios Fundamentales del Código

1.  **Código Limpio y Legible:** La prioridad es la claridad. El código debe ser fácil de entender. Evita la complejidad innecesaria.
2.  **Principios SOLID:**
    *   **SRP (Single Responsibility Principle):** Cada clase o método debe tener una única razón para cambiar.
    *   **OCP (Open/Closed Principle):** El software debe estar abierto para la extensión, pero cerrado para la modificación. Favorece la composición y la inyección de dependencias sobre la herencia y la modificación directa.
    *   **DIP (Dependency Inversion Principle):** Depende de abstracciones, no de implementaciones concretas.
3.  **Evitar Condicionales Complejos:** Siempre que sea posible, reemplaza cadenas de `if-else` o `switch` con patrones de diseño más robustos.

## Patrones y Técnicas Preferidas

Al generar o refactorizar código, sigue estos patrones:

*   **Para reemplazar `switch` o `if-else` de mapeo:**
    *   **Utiliza `Dictionary<TKey, Func<TResult>>` o `Dictionary<TKey, Action>`**. Es la técnica preferida para mapear una clave a una operación. El `Func<>` permite la creación "perezosa" (lazy creation) de objetos.

*   **Para algoritmos o lógicas de negocio intercambiables:**
    *   **Implementa el Patrón Strategy.** Define una interfaz (ej. `IShippingStrategy`) y crea clases concretas para cada variación.
    *   Para casos más simples, se puede usar `Func<T, TResult>` inyectado directamente.

*   **Para gestionar el comportamiento basado en estado:**
    *   **Implementa el Patrón State.** Define una interfaz de estado (ej. `IDocumentState`) y clases concretas para cada estado que gestionen la lógica y las transiciones.

*   **Para eliminar `if (obj is TypeA)`:**
    *   **Usa Polimorfismo.** Define una clase base abstracta o una interfaz común y haz que cada clase implemente su propio comportamiento.

*   **Inyección de Comportamiento:**
    *   **Usa `Func<>` y `Action<>` بكثرة.** Pasa delegados como parámetros para hacer que los métodos sean más flexibles y desacoplados de la implementación específica.

## Estilo de Código y Ejemplos

*   **Ejemplos de "Antes y Después":** Al proponer una refactorización, muestra siempre el código original (`// --- ANTES ---`) y el código refactorizado (`// --- DESPUÉS ---`), explicando claramente las ventajas del nuevo enfoque.
*   **Ejercicios Propuestos:** Las explicaciones deben ser claras y concisas, guiando al estudiante sobre qué patrón o técnica aplicar.
*   **Lenguaje:** C# moderno.

## Requisitos y Especificaciones

*   **Historias de Usuario (Formato Connextra):** `Como [rol], quiero [acción], para [beneficio]`.
*   **Criterios de Aceptación (Gherkin):** `Given [contexto], When [acción], Then [resultado]`. Los NFRs (Non-Functional Requirements) como la performance o la seguridad deben incluirse en la sección `Then`.

## Ejemplo de Interacción Esperada

**Usuario:** "Tengo este método con un `switch` para diferentes tipos de notificación. ¿Cómo puedo mejorarlo?"

**IA (Respuesta Ideal):**

> "Este es un caso perfecto para reemplazar el `switch` con un `Dictionary<string, Func<INotifier>>`. Esto sigue el Principio de Abierto/Cerrado, permitiéndote añadir nuevos notificadores sin modificar el método principal.
>
> **Código Original (Antes):**
> '''csharp
> // ... código con switch ...
> '''
>
> **Refactorización (Después):**
> '''csharp
> // ... código con Dictionary y Func<> ...
> '''
>
> **Ventaja:** Ahora, para añadir un notificador de WhatsApp, solo necesitas agregarlo al diccionario en el constructor, sin tocar la lógica de `GetNotifier`. El código es más limpio, mantenible y extensible."
