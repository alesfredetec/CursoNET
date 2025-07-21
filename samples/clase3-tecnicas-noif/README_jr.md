# Técnicas NoIf para Desarrolladores Junior

Este módulo contiene ejercicios simplificados para introducir a desarrolladores junior en técnicas para eliminar condicionales complejos utilizando patrones de diseño básicos.

## Objetivos de aprendizaje

1. Entender los problemas de los condicionales complejos (switch/if-else)
2. Aprender patrones básicos para reemplazar condicionales
3. Comprender las ventajas de código más limpio y mantenible
4. Practicar refactorización paso a paso

## Ejercicios incluidos

### 1. MiniCalculadora
- **Nivel:** Muy básico
- **Patrón:** Dictionary Pattern (Patrón Diccionario)
- **Problema:** Selección de operaciones matemáticas con switch
- **Solución:** Mapear nombres de operaciones a funciones en un diccionario

### 2. GestorTareas
- **Nivel:** Intermedio (aprendiz)
- **Patrón:** State Pattern simplificado (Patrón Estado)
- **Problema:** Gestión de estados de tareas con múltiples if-else
- **Solución:** Encapsular comportamiento por estado en clases separadas

### 3. CalculadoraDescuentos
- **Nivel:** Intermedio (aprendiz)
- **Patrón:** Strategy Pattern (Patrón Estrategia)
- **Problema:** Diferentes algoritmos de descuento con if-else
- **Solución:** Encapsular algoritmos en clases separadas e intercambiables

### 4. AnimalesZoo
- **Nivel:** Intermedio (aprendiz)
- **Patrón:** Polymorphism (Polimorfismo)
- **Problema:** Type checking excesivo para determinar comportamiento
- **Solución:** Crear una jerarquía de clases con métodos abstractos

### 5. ValidacionFormulario
- **Nivel:** Intermedio (aprendiz)
- **Patrón:** Extension Methods (Métodos de Extensión)
- **Problema:** Validaciones con múltiples if-else anidados
- **Solución:** Crear una API fluida con métodos de extensión encadenables

## Estructura de los ejercicios

Cada ejercicio incluye:

1. **Archivo Before.cs**
   - Código con condicionales complejos (switch/if-else)
   - Comentarios que explican los problemas

2. **Archivo After.cs**
   - Código refactorizado usando el patrón apropiado
   - Comentarios detallados explicando cada paso
   - Diagrama del patrón (como ASCII art)

3. **Sección de explicación**
   - Ventajas del patrón
   - Cuándo usarlo
   - Conceptos clave

## Cómo usar estos ejercicios

1. **Estudio individual:**
   - Examinar primero el archivo Before.cs para entender el problema
   - Intentar pensar en soluciones antes de ver After.cs
   - Estudiar After.cs con sus comentarios paso a paso

2. **En clase:**
   - Explicación del instructor sobre el patrón
   - Demostración de la refactorización paso a paso
   - Ejercicios prácticos adicionales

3. **Práctica guiada:**
   - Completar ejercicios similares con ayuda del instructor
   - Identificar casos reales donde aplicar estos patrones

## Recomendaciones para instructores

- Enfatizar el "por qué" detrás de cada refactorización
- Mostrar ejemplos visuales (diagramas) antes de código
- Realizar la refactorización paso a paso en vivo
- Relacionar con situaciones reales de desarrollo
- Asignar ejercicios adicionales similares pero distintos

## Progresión de aprendizaje recomendada

Para estudiantes que están comenzando con patrones de diseño:

1. Iniciar con el patrón Dictionary (MiniCalculadora) - el más sencillo conceptualmente
2. Continuar con el patrón Strategy (CalculadoraDescuentos) - introduce la separación de algoritmos
3. Seguir con el patrón State (GestorTareas) - profundiza en el manejo de estados
4. Avanzar con el Polimorfismo puro (AnimalesZoo) - integra los conceptos anteriores en una jerarquía de clases
5. Finalizar con los Métodos de Extensión (ValidacionFormulario) - introduce programación funcional y APIs fluidas
6. Una vez dominados estos patrones básicos, avanzar a los ejercicios más complejos en la carpeta clase3-tecnicas-noif

## Preguntas frecuentes para discusión en clase

1. ¿Cuándo vale la pena refactorizar un switch/if-else y cuándo es mejor dejarlo como está?
2. ¿Qué beneficios específicos aporta cada patrón en términos de mantenibilidad?
3. ¿Cómo afectan estos patrones al rendimiento de la aplicación?
4. ¿Cuáles son los trade-offs entre complejidad del código y flexibilidad?
5. ¿En qué situaciones es más apropiado utilizar métodos de extensión frente a otros patrones?
6. ¿Cómo influyen estas técnicas en la legibilidad del código para otros desarrolladores?

## Material adicional

Para ayudar en la selección del patrón más adecuado según el contexto, se ha incluido un documento adicional:

- [CHECKLIST-SELECCION-PATRONES.md](CHECKLIST-SELECCION-PATRONES.md): Una guía práctica que te ayudará a decidir qué patrón utilizar en cada situación, con criterios claros y ejemplos.
