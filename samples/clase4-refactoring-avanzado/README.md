# Clase 4: Refactoring Avanzado para Desarrolladores Jr → Semi-Sr

## 📋 Descripción
Esta carpeta contiene ejercicios prácticos para dominar técnicas avanzadas de refactoring, modernización de código legacy y aplicación de principios SOLID en proyectos reales.

## 🎯 Objetivos de Aprendizaje
- Identificar code smells y anti-patterns en código legacy
- Aplicar refactoring patterns de forma segura y sistemática
- Modernizar código legacy con técnicas step-by-step
- Implementar principios SOLID en código existente
- Usar design patterns para mejorar arquitectura
- Crear tests que faciliten refactoring seguro

## 📁 Estructura de Archivos

### Ejercicio 1: Legacy Code Modernization
- `01-LegacyCode-Before.cs` - Sistema legacy con múltiples problemas
- `01-LegacyCode-After.cs` - Modernización completa step-by-step
- `01-Refactoring-Steps.md` - Documentación del proceso de refactoring

### Ejercicio 2: Design Patterns Introduction
- `02-DesignPatterns-Before.cs` - Código procedural problemático
- `02-DesignPatterns-After.cs` - Aplicación de patterns (Factory, Observer, Command)
- `02-Patterns-Guide.md` - Guía de cuándo usar cada pattern

### Ejercicio 3: SOLID Principles Application
- `03-SOLID-Violations-Before.cs` - Violaciones flagrantes de SOLID
- `03-SOLID-Violations-After.cs` - Aplicación correcta de principios SOLID
- `03-SOLID-Checklist.md` - Checklist para validar cumplimiento

### Ejercicio 4: Refactoring Patterns Catalog
- `04-RefactoringCatalog.md` - Catálogo de refactoring patterns comunes
- `04-CodeSmells-Detection.cs` - Ejemplos de code smells y sus soluciones

## 🛠️ Herramientas y Técnicas

### Code Smells a Identificar
- **Long Method** - Métodos de más de 20-30 líneas
- **Large Class** - Clases que hacen demasiado
- **Duplicate Code** - Código repetido en múltiples lugares
- **Long Parameter List** - Métodos con muchos parámetros
- **Data Class** - Clases que solo contienen datos
- **God Object** - Objetos que controlan todo

### Refactoring Patterns
- **Extract Method** - Extraer funcionalidad a métodos separados
- **Extract Class** - Separar responsabilidades en clases
- **Move Method** - Mover métodos a clases más apropiadas
- **Replace Parameter with Method Call** - Simplificar parámetros
- **Introduce Parameter Object** - Agrupar parámetros relacionados
- **Replace Conditional with Polymorphism** - Usar herencia en lugar de if/switch

### Design Patterns para Refactoring
- **Factory Method** - Para creación de objetos
- **Strategy Pattern** - Para algoritmos intercambiables
- **Observer Pattern** - Para notificaciones
- **Command Pattern** - Para acciones desacopladas
- **Template Method** - Para workflows similares

## 💡 Tips para Desarrolladores Jr

### Proceso de Refactoring Seguro
1. **Escribir Tests Primero** - Asegurar comportamiento actual
2. **Cambios Pequeños** - Un refactoring a la vez
3. **Ejecutar Tests Frecuentemente** - Validar que no rompemos nada
4. **Commit Frecuente** - Para poder rollback si es necesario
5. **Documentar Cambios** - Explicar el por qué del refactoring

### Red Flags - Cuándo NO Refactorizar
- **Cerca de deadlines** - Refactoring requiere tiempo
- **Sin tests** - Es muy riesgoso sin validación automática
- **Funcionalidad inestable** - Mejor estabilizar primero
- **Código que no se toca** - Si funciona y no se modifica, dejarlo

### Cuándo SÍ Refactorizar
- **Antes de agregar features** - Para facilitar el cambio
- **Cuando hay bugs recurrentes** - Mejora calidad
- **Código difícil de entender** - Para mejorar mantenibilidad
- **Performance issues** - Para optimizar arquitectura

## 📚 Recursos de Consulta
- [Refactoring: Improving the Design of Existing Code - Martin Fowler](https://refactoring.com/)
- [Clean Code: A Handbook of Agile Software Craftsmanship - Robert Martin](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350884)
- [Design Patterns: Elements of Reusable Object-Oriented Software](https://www.amazon.com/Design-Patterns-Elements-Reusable-Object-Oriented/dp/0201633612)
- [Working Effectively with Legacy Code - Michael Feathers](https://www.amazon.com/Working-Effectively-Legacy-Michael-Feathers/dp/0131177052)

## 🎯 Métricas de Éxito
- **Cyclomatic Complexity** < 10 por método
- **Class Coupling** bajo (< 5 dependencies por clase)
- **Code Coverage** > 80% en código refactorizado
- **SonarLint Issues** = 0 en código nuevo
- **Method Length** < 20 líneas promedio
- **Class Size** < 200 líneas para clases de negocio

## 🚀 Challenge Final
Al completar todos los ejercicios, serás capaz de:
- Tomar cualquier código legacy y modernizarlo sistemáticamente
- Aplicar design patterns apropiados para resolver problemas comunes
- Escribir código que cumple principios SOLID
- Identificar y resolver code smells automáticamente
- Refactorizar con confianza usando testing

## 🎓 Próximos Pasos
- Clase 5: Garbage Collection - Optimización de memoria
- Aplicar técnicas aprendidas en proyectos reales
- Crear templates de refactoring reutilizables
- Establecer proceso de code review enfocado en calidad

**TIEMPO ESTIMADO TOTAL:** 3-4 horas de práctica intensiva
**DIFICULTAD:** Intermedio-Avanzado  
**PREREQUISITOS:** Clases 1-3 completadas