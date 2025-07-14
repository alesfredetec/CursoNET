# 🎓 Guía del Profesor - CursoNET

## 📋 Información General

**Curso**: CursoNET - Desarrollo .NET para Promoción Junior a Semi-Senior  
**Dirigido por**: Área de Arquitectura  
**Instructor**: Alejandro Sfrede  
**Audiencia**: Desarrolladores .NET junior en entornos empresariales  
**Objetivo**: Promoción a nivel semi-senior mediante habilidades arquitectónicas prácticas  
**Duración**: 7 clases (14-20 horas académicas)  
**Modalidad**: Presencial/Virtual con presentaciones interactivas  

## 🎯 Objetivos de Aprendizaje

### Objetivos Generales
- Dominar herramientas y técnicas de análisis de código estático
- Aplicar metodologías modernas de análisis de requisitos
- Implementar patrones de refactoring avanzados
- Optimizar rendimiento y gestión de memoria
- Diseñar bases de datos eficientes y escalables

### Competencias Clave
- **Análisis técnico**: Evaluación de calidad de código con métricas
- **Arquitectura**: Aplicación de patrones de diseño enterprise
- **Performance**: Optimización de memoria y consultas SQL
- **Metodología**: Requisitos funcionales y no funcionales
- **Herramientas**: SonarLint, Visual Studio, SSMS, EF Core

## 📚 Programa del Curso

### **Clase 1: Análisis de Código Estático**
**Archivo**: `doc/codestatico-presentacion.html`  
**Duración**: 2-3 horas  
**Formato**: 18 slides interactivas  

#### Contenido Principal
- **SonarLint**: Configuración y análisis en tiempo real
- **Complejidad ciclomática**: Cálculo y reducción (objetivo: 15+ → 2-3)
- **Code smells**: Identificación y corrección automática
- **Patrones de refactoring**: Extract Method, Extract Class, Replace Conditional
- **Integración CI/CD**: Quality gates y métricas de calidad

#### Ejercicios Prácticos
- **Carpeta**: `samples/clase1-analisis-codigo-estatico/`
- **Antes/Después**: Comparación de código con mejoras cuantificables
- **Herramientas**: SonarLint instalado y configurado

#### Evaluación
- Análisis de código con reducción de complejidad ciclomática
- Configuración de quality gates en pipeline CI/CD
- Identificación y corrección de code smells

---

### **Clase 2: Análisis de Requisitos**
**Archivo**: `doc/requisitos-presentacion.html`  
**Duración**: 2-3 horas  
**Formato**: 14 slides interactivas  

#### Contenido Principal
- **Requisitos funcionales vs no funcionales**
- **User Stories**: Formato Connextra y criterios de aceptación
- **BDD/Gherkin**: Especificación ejecutable
- **NFRs para microservicios**: Azure AKS, observabilidad
- **Casos de estudio**: Sistemas de pagos y financieros

#### Ejercicios Prácticos
- **Carpeta**: `samples/clase2-analisis-requisitos/`
- **Casos reales**: Análisis de sistemas de pagos
- **Templates**: User stories y matrices de trazabilidad

#### Evaluación
- Creación de user stories con criterios de aceptación
- Especificación de NFRs para microservicios
- Análisis de dependencias de requisitos

---

### **Clase 3: Técnicas "No If"**
**Archivo**: `doc/noif-presentacion.html`  
**Duración**: 3-4 horas  
**Formato**: 20 slides interactivas  

#### Contenido Principal
- **Dictionary Pattern**: Reemplazo de switch statements
- **Strategy Pattern**: Encapsulación de algoritmos
- **State Pattern**: Comportamiento basado en estado interno
- **Polimorfismo**: Eliminación de condicionales basadas en tipo

#### Ejercicios Prácticos
- **Carpeta**: `samples/clase3-tecnicas-noif/`
- **4 patrones implementados**: Código ejecutable antes/después
- **Casos reales**: Procesamiento de pagos y validaciones

#### Evaluación
- Implementación de al menos 2 patrones "No If"
- Refactoring de código condicional complejo
- Análisis de mantenibilidad y extensibilidad

---

### **Clase 4: Refactoring Avanzado**
**Archivo**: `doc/refactoring-presentacion.html`  
**Duración**: 3-4 horas  
**Formato**: 20 slides interactivas  

#### Contenido Principal
- **Extract Method**: Técnicas sistemáticas
- **Introduce Class**: Principio de responsabilidad única
- **Func<T> y Action<T>**: Flexibilidad funcional
- **Caso de estudio**: Sistema de procesamiento de órdenes
- **Patrones avanzados**: Strategy, Factory, Pipeline

#### Ejercicios Prácticos
- **Carpeta**: `samples/clase4-refactoring-avanzado/`
- **Transformación completa**: Código procedural → funcional/OO
- **Principios SOLID**: Implementación práctica

#### Evaluación
- Refactoring de código legacy siguiendo SOLID
- Implementación de patrones Factory y Strategy
- Análisis de mejoras en mantenibilidad

---

### **Clase 5: Garbage Collection**
**Archivo**: `doc/gc-presentacion.html`  
**Duración**: 2-3 horas  
**Formato**: 18 slides interactivas  

#### Contenido Principal
- **Fundamentos GC**: Heap, generaciones (Gen 0/1/2), mark-and-compact
- **Patrón IDisposable**: Recursos no administrados, using statements
- **OutOfMemoryException**: Escenarios y simulación de memory leaks
- **Optimización de rendimiento**: Técnicas avanzadas

#### Ejercicios Prácticos
- **Carpeta**: `samples/clase5-garbage-collection/`
- **Simulación de memory leaks**: Código antes/después
- **Implementación IDisposable**: Casos reales

#### Evaluación
- Análisis de consumo de memoria con herramientas
- Implementación correcta de IDisposable
- Optimización de código con memory leaks

---

### **Clase 6: Indexación SQL y EF Core**
**Archivo**: `doc/cursosql2-presentacion.html`  
**Duración**: 3-4 horas  
**Formato**: 18 slides interactivas  

#### Contenido Principal
- **Análisis de performance**: Identificación de consultas lentas
- **Estrategias de indexación**: Sistemas de alto tráfico
- **EF Core**: Mejores prácticas para aplicaciones enterprise
- **Optimización**: Técnicas avanzadas de tuning

#### Ejercicios Prácticos
- **Carpeta**: `samples/clase6-indexacion-sql/`
- **Consultas lentas**: Análisis y optimización
- **Casos reales**: Sistemas financieros de alto volumen

#### Evaluación
- Análisis de planes de ejecución SQL
- Creación de índices optimizados
- Configuración de EF Core para performance

---

### **Clase 7: Diseño de BD con Metadatos**
**Archivo**: `doc/sqldisenio-presentacion.html`  
**Duración**: 3-4 horas  
**Formato**: 18 slides interactivas  

#### Contenido Principal
- **Arquitectura evolutiva**: Esquemas adaptables
- **Patrón EAV**: Entity-Attribute-Value implementación
- **Modelos híbridos**: Combinación de enfoques
- **Metadata-driven**: Operaciones CRUD dinámicas

#### Ejercicios Prácticos
- **Carpeta**: `samples/clase7-diseno-bd-metadatos/`
- **Patrón EAV**: Implementación completa
- **Casos empresariales**: Sistemas configurables

#### Evaluación
- Diseño de esquema EAV funcional
- Implementación de CRUD metadata-driven
- Análisis de ventajas/desventajas del patrón

## 🛠️ Herramientas y Recursos

### **Herramientas Requeridas**
- **Visual Studio 2022** (Community o superior)
- **SonarLint** (extensión instalada)
- **SQL Server Management Studio** (SSMS)
- **Git** (control de versiones)
- **Navegador web moderno** (Chrome, Firefox, Edge)

### **Recursos del Curso**
- **Portal principal**: `index.html` - Hub de navegación
- **Presentaciones**: Archivos HTML interactivos en `doc/`
- **Ejercicios**: Código fuente en `samples/`
- **Documentación**: `CLAUDE.md` y `README.md`

### **Navegación de Presentaciones**
- **Teclado**: Flechas (anterior/siguiente), Spacebar (siguiente), Home (portal)
- **Mouse**: Botones Previous/Next, barra de progreso clicable
- **Móvil**: Gestos de swipe, botones táctiles

## 📊 Metodología de Enseñanza

### **Enfoque Didáctico**
- **Learning by doing**: Práctica inmediata con cada concepto
- **Antes/Después**: Comparaciones visuales con mejoras cuantificables
- **Casos reales**: Ejemplos de sistemas financieros y enterprise
- **Progresión lógica**: Cada clase construye sobre la anterior

### **Estructura de Clase**
1. **Introducción** (15 min): Contexto y objetivos
2. **Teoría** (30 min): Conceptos fundamentales
3. **Demostración** (30 min): Código en vivo
4. **Práctica** (45 min): Ejercicios dirigidos
5. **Evaluación** (15 min): Revisión y feedback
6. **Cierre** (5 min): Resumen y próximos pasos

### **Técnicas Instruccionales**
- **Código en vivo**: Demostración con herramientas reales
- **Pair programming**: Trabajo colaborativo en ejercicios
- **Code review**: Análisis grupal de soluciones
- **Métricas objetivas**: Medición de mejoras (complejidad, performance)

## 🎯 Evaluación y Seguimiento

### **Criterios de Evaluación**
- **Comprensión teórica** (30%): Conceptos y principios
- **Implementación práctica** (50%): Código funcional y optimizado
- **Análisis crítico** (20%): Capacidad de evaluación y mejora

### **Métodos de Evaluación**
- **Ejercicios prácticos**: Implementación de patrones y optimizaciones
- **Análisis de código**: Revisión de métricas y calidad
- **Casos de estudio**: Resolución de problemas reales
- **Presentaciones**: Explicación de soluciones implementadas

### **Seguimiento de Progreso**
- **Por clase**: Completitud de ejercicios y comprensión
- **Acumulativo**: Construcción de habilidades progresivas
- **Portfolio**: Colección de código refactorizado y optimizado

## 💡 Consejos para el Instructor

### **Preparación**
- **Verificar herramientas**: SonarLint, SSMS, Visual Studio funcionando
- **Revisar ejercicios**: Código de ejemplo ejecutable
- **Preparar datos**: Casos reales y métricas de baseline
- **Configurar entorno**: Proyector, navegación por teclado

### **Durante la Clase**
- **Demostrar en vivo**: Usar herramientas reales, no solo slides
- **Medir mejoras**: Mostrar métricas antes/después cuantificables
- **Conectar con experiencia**: Relacionar con proyectos enterprise
- **Fomentar participación**: Preguntas, code review, pair programming

### **Adaptaciones**
- **Nivel técnico**: Ajustar profundidad según audiencia
- **Tiempo**: Flexibilidad en ejercicios según ritmo del grupo
- **Herramientas**: Alternativas si hay problemas técnicos
- **Ejemplos**: Adaptar casos de uso al contexto empresarial

## 📈 Métricas de Éxito

### **Indicadores Clave**
- **Complejidad ciclomática**: Reducción promedio de 15+ a 2-3
- **Code coverage**: Incremento en cobertura de pruebas
- **Performance**: Mejoras medibles en tiempo de ejecución
- **Mantenibilidad**: Reducción de deuda técnica

### **Resultados Esperados**
- **Promoción profesional**: 80% de estudiantes logran ascenso en 6 meses
- **Aplicación práctica**: Implementación inmediata en proyectos actuales
- **Conocimiento enterprise**: Comprensión de arquitectura y patrones
- **Herramientas**: Dominio de SonarLint, SSMS, Visual Studio

## 🔄 Mejora Continua

### **Feedback del Estudiante**
- **Post-clase**: Evaluación de contenido y metodología
- **Seguimiento**: Aplicación práctica en proyectos reales
- **Sugerencias**: Mejoras para futuras ediciones

### **Actualización de Contenido**
- **Tecnologías**: Mantener ejemplos con versiones actuales
- **Casos de estudio**: Incorporar nuevos ejemplos enterprise
- **Herramientas**: Actualizar configuraciones y mejores prácticas

---

**Autor**: Alejandro Sfrede - Área de Arquitectura  
**Versión**: 1.0  
**Fecha**: Julio 2025  
**Licencia**: Uso interno empresarial  

*Esta guía está diseñada para maximizar el impacto educativo y la aplicación práctica del conocimiento adquirido en entornos profesionales reales.*