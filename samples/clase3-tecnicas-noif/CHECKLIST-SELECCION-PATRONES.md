# Checklist para Decidir Qué Patrón "No-If" Utilizar

Este checklist te ayudará a identificar el patrón más adecuado para eliminar condicionales en diferentes situaciones. Utiliza estas preguntas como guía para seleccionar el mejor enfoque en tus proyectos.

## 1. Patrón Diccionario

**Usa este patrón cuando:**

- [ ] Necesitas mapear un valor (clave) a una acción o comportamiento
- [ ] Los comportamientos son sencillos y pueden expresarse como funciones o delegados
- [ ] El conjunto de opciones es conocido y relativamente estable
- [ ] El comportamiento no requiere mantener un estado complejo
- [ ] Quieres una solución simple con poco código adicional

**Ejemplo de caso de uso:** Sistemas de menús, calculadoras, mapeo de comandos a acciones

## 2. Patrón Estrategia

**Usa este patrón cuando:**

- [ ] Tienes diferentes algoritmos o lógicas para resolver un mismo problema
- [ ] Los algoritmos son complejos y tienen múltiples líneas de código
- [ ] Necesitas que los algoritmos sean intercambiables en tiempo de ejecución
- [ ] Quieres encapsular cada algoritmo en su propia clase
- [ ] Los algoritmos pueden compartir una interfaz común pero con diferente implementación

**Ejemplo de caso de uso:** Algoritmos de descuento, estrategias de navegación, métodos de pago

## 3. Patrón Estado

**Usa este patrón cuando:**

- [ ] El comportamiento de un objeto cambia según su estado interno
- [ ] Tienes múltiples condicionales basados en variables de estado
- [ ] Necesitas gestionar transiciones entre estados de forma clara
- [ ] El objeto tiene un ciclo de vida definido con diferentes etapas
- [ ] Cada estado tiene su propio conjunto de comportamientos y reglas

**Ejemplo de caso de uso:** Flujos de trabajo, máquinas de estados, procesos con etapas definidas

## 4. Polimorfismo

**Usa este patrón cuando:**

- [ ] Haces muchas comprobaciones de tipo (is, instanceof, typeof)
- [ ] Tienes una jerarquía natural de objetos con comportamientos específicos
- [ ] Necesitas extender el sistema con nuevos tipos frecuentemente
- [ ] El comportamiento está intrínsecamente ligado al tipo de objeto
- [ ] Quieres aprovechar al máximo la orientación a objetos

**Ejemplo de caso de uso:** Sistemas de entidades, jerarquías de elementos UI, procesamiento de diferentes tipos

## 5. Métodos de Extensión

**Usa este patrón cuando:**

- [ ] Necesitas crear una API fluida y encadenable
- [ ] Quieres extender tipos existentes sin modificarlos
- [ ] Tienes operaciones de transformación o validación en cadena
- [ ] Prefieres un enfoque más funcional que orientado a objetos
- [ ] Quieres centralizar la lógica condicional en métodos reutilizables

**Ejemplo de caso de uso:** Validación de datos, transformación de objetos, operaciones en colecciones

## Preguntas para afinar tu elección

1. **¿Cómo es la estructura del problema?**
   - Selección entre opciones → Diccionario
   - Variaciones de un algoritmo → Estrategia
   - Comportamiento basado en estado → Estado
   - Comportamiento basado en tipo → Polimorfismo
   - Transformaciones en cadena → Métodos de extensión

2. **¿Cómo es el contexto de extensibilidad?**
   - Necesitas agregar nuevas claves/acciones → Diccionario
   - Necesitas agregar nuevos algoritmos → Estrategia
   - Necesitas agregar nuevos estados → Estado
   - Necesitas agregar nuevos tipos → Polimorfismo
   - Necesitas agregar nuevas transformaciones → Métodos de extensión

3. **¿Cuánta complejidad estás dispuesto a aceptar?**
   - Mínima (Solución sencilla) → Diccionario
   - Baja (Interfaces simples) → Métodos de extensión
   - Media (Clases con una responsabilidad) → Estrategia
   - Alta (Jerarquía de clases) → Estado o Polimorfismo

## Combinaciones comunes

A veces, la mejor solución es combinar varios patrones:

- **Diccionario de Estrategias**: Utiliza un diccionario para seleccionar entre diferentes estrategias
- **Estado con Polimorfismo**: Utiliza polimorfismo para implementar diferentes estados
- **Estrategias con Métodos de Extensión**: Implementa estrategias que se pueden encadenar como métodos de extensión

## Consejo final

Recuerda que no siempre es necesario eliminar todos los condicionales. A veces, un simple if-else puede ser más claro y mantenible que un patrón complejo. Usa estos patrones cuando aporten valor real en términos de legibilidad, mantenibilidad o extensibilidad.
