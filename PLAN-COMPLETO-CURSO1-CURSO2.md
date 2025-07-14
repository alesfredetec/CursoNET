# 🎯 Plan Completo y Guía Integrada - Curso 1 & 2

## 📊 Información General

**Programa**: CursoNET - Promoción Junior a Semi-Senior  
**Instructor**: Alejandro Sfrede - Área de Arquitectura  
**Modalidad**: Presencial/Virtual con presentaciones interactivas  
**Duración Total**: 6-8 horas académicas  
**Audiencia**: Desarrolladores .NET junior en entornos empresariales  

---


## 🎯 CLASE 1: ANÁLISIS DE CÓDIGO ESTÁTICO

### 📋 Información General
**Duración**: 3-4 horas  
**Formato**: 18 slides interactivas HTML + ejercicios prácticos  
**Objetivo**: Dominar herramientas de análisis estático para escribir código mantenible  

### 🎯 Objetivos Específicos
- **Dominar SonarLint** para análisis en tiempo real
- **Calcular complejidad ciclomática** y reducirla sistemáticamente
- **Aplicar técnicas de refactoring** (Guard Clauses, Extract Method)
- **Implementar patrones anti-complejidad** (Strategy, Polimorfismo)

### 🏗️ Estructura Detallada

#### **🔍 Módulo 1: Fundamentos del Análisis Estático (45 min)**
##### Slides 1-3: Introducción y Conceptos
- **Slide 1**: Presentación del curso y objetivos
- **Slide 2**: ¿Qué es análisis estático? Diferencia con testing
- **Slide 3**: Instalación y configuración de SonarLint

##### Ejercicio Práctico 1 (30 min)
- **Archivo**: `samples/clase1-analisis-codigo-estatico/01-BadCodeExample-Simple.cs`
- **Objetivo**: Identificar y corregir 12 problemas básicos
- **Herramientas**: SonarLint, Error List, Quick Actions
- **Resultados**: Código limpio sin advertencias

#### **🧮 Módulo 2: Complejidad Ciclomática (60 min)**
##### Slides 4-6: Teoría y Cálculo
- **Slide 4**: Definición y importancia de la complejidad
- **Slide 5**: Cálculo paso a paso con ejemplos
- **Slide 6**: Herramientas de medición en Visual Studio

##### Ejercicio Práctico 2 (30 min)
- **Archivo**: `samples/clase1-analisis-codigo-estatico/03-Complejidad-Simple.md`
- **Objetivo**: Calcular complejidad de métodos reales
- **Herramientas**: Code Metrics, calculadora manual
- **Meta**: Reducir complejidad de 15+ a <5

#### **🔧 Módulo 3: Técnicas de Refactoring (90 min)**
##### Slides 7-8: Guard Clauses y Extract Method
- **Slide 7**: Eliminación del "Arrow Pattern" con Guard Clauses
- **Slide 8**: Dividir métodos complejos con Extract Method

##### Ejercicio Práctico 3 (45 min)
- **Archivo**: `samples/clase1-analisis-codigo-estatico/01-BadCodeExample-After.cs`
- **Objetivo**: Refactorizar método con complejidad 12+ a 3-4
- **Técnicas**: Guard Clauses, Extract Method, métodos privados
- **Validación**: Ejecutar tests, verificar funcionalidad

#### **🎨 Módulo 4: Patrones Anti-Complejidad (75 min)**
##### Slides 9-11: Strategy Pattern
- **Slide 9**: Problemas de switch/if-else largos
- **Slide 10**: Implementación del patrón Strategy
- **Slide 11**: Uso de diccionarios para mapear estrategias

##### Ejercicio Práctico 4 (45 min)
- **Objetivo**: Refactorizar ReportGenerator con Strategy Pattern
- **Resultado**: Complejidad constante O(1) vs O(n)
- **Extensibilidad**: Fácil agregar nuevos tipos de reporte

#### **🛠️ Módulo 5: Herramientas Avanzadas (45 min)**
##### Slides 12-13: Ecosistema de Calidad
- **Slide 12**: Herramientas .NET Core (SonarCloud, CodeMaid)
- **Slide 13**: Tips de productividad y shortcuts

##### Configuración Avanzada (30 min)
- **EditorConfig** para reglas de equipo
- **Analizadores NuGet** personalizados
- **Integración CI/CD** con quality gates

#### **🏦 Módulo 6: Casos Empresariales (45 min)**
##### Slides 14-15: Ejemplos Reales
- **Slide 14**: Sistema bancario de validación de transacciones
- **Slide 15**: E-commerce con calculadora de precios

##### Ejercicio Final (30 min)
- **Contexto**: Sistema de pagos QR
- **Objetivo**: Aplicar todas las técnicas aprendidas
- **Métrica**: Complejidad promedio <5, 0 code smells

#### **📚 Módulo 7: Consolidación (30 min)**
##### Slides 16-18: Recursos y Próximos Pasos
- **Slide 16**: Siglas y términos del mercado
- **Slide 17**: Checklist del desarrollador
- **Slide 18**: Conclusiones y recursos adicionales

### 📊 Evaluación Clase 1
#### Criterios de Dominio:
- [ ] **SonarLint**: Instalación y uso sin ayuda
- [ ] **Complejidad**: Cálculo manual correcto
- [ ] **Refactoring**: Aplicación de Guard Clauses y Extract Method
- [ ] **Patrones**: Implementación de Strategy Pattern
- [ ] **Herramientas**: Integración con workflow de desarrollo

#### Métricas de Éxito:
- **Complejidad ciclomática**: De 15+ a <5
- **Code smells**: De 12+ a 0
- **Mantenibilidad**: De <60 a >85
- **Tiempo de corrección**: De 2h a 30 min

---

## 🎯 CLASE 2: ANÁLISIS DE REQUISITOS

### 📋 Información General
**Duración**: 3-4 horas  
**Formato**: 14 slides interactivas HTML + ejercicios prácticos  
**Objetivo**: Dominar análisis de requisitos para sistemas empresariales  

### 🎯 Objetivos Específicos
- **Distinguir requisitos funcionales vs no funcionales**
- **Crear user stories** siguiendo metodología ágil
- **Especificar NFRs medibles** con métricas específicas
- **Establecer matrices de trazabilidad** y dependencias

### 🏗️ Estructura Detallada

#### **📋 Módulo 1: Fundamentos de Requisitos (60 min)**
##### Slides 1-3: Conceptos Básicos
- **Slide 1**: Introducción y objetivos del análisis
- **Slide 2**: Diferencia entre requisitos funcionales y no funcionales
- **Slide 3**: Stakeholders y técnicas de elicitación

##### Ejercicio Práctico 1 (30 min)
- **Archivo**: `samples/clase2-analisis-requisitos/01-Requirements-Simple.md`
- **Objetivo**: Clasificar 15 requisitos básicos
- **Contexto**: Sistema de procesamiento de pagos
- **Resultado**: Tabla organizada por categorías

#### **📝 Módulo 2: Casos de Uso y User Stories (75 min)**
##### Slides 4-6: Especificación Funcional
- **Slide 4**: Casos de uso: actores, flujos principales y alternos
- **Slide 5**: User stories: formato Connextra y criterios de aceptación
- **Slide 6**: Técnicas de redacción y priorización

##### Ejercicio Práctico 2 (45 min)
- **Archivo**: `samples/clase2-analisis-requisitos/02-UseCase-Simple-Before.md`
- **Objetivo**: Convertir descripción informal en casos de uso
- **Plantilla**: `02-UserStories-Template.md`
- **Resultado**: 5 user stories con criterios de aceptación

#### **⚡ Módulo 3: Requisitos No Funcionales (90 min)**
##### Slides 7-9: Calidad y Restricciones
- **Slide 7**: Categorías de NFRs (rendimiento, seguridad, escalabilidad)
- **Slide 8**: Métricas específicas y umbrales
- **Slide 9**: Compliance y regulaciones (PCI DSS, GDPR)

##### Ejercicio Práctico 3 (60 min)
- **Archivo**: `samples/clase2-analisis-requisitos/03-NonFunctional-Requirements.md`
- **Objetivo**: Definir 17 NFRs con métricas específicas
- **Contexto**: Sistema de pagos con 10,000 TPS
- **Checklist**: `03-NFR-Checklist.md`

#### **🔗 Módulo 4: Trazabilidad y Dependencias (75 min)**
##### Slides 10-12: Gestión de Requisitos
- **Slide 10**: Matrices de trazabilidad: requisitos → casos de uso → tests
- **Slide 11**: Análisis de dependencias e impactos
- **Slide 12**: Herramientas de gestión (Jira, Azure DevOps)

##### Ejercicio Práctico 4 (45 min)
- **Archivo**: `samples/clase2-analisis-requisitos/04-Traceability-Matrix.md`
- **Objetivo**: Crear matriz completa de trazabilidad
- **Herramientas**: Excel, Jira, Confluence
- **Resultado**: Mapeo completo requisitos → implementación

#### **🌐 Módulo 5: Contexto Empresarial (60 min)**
##### Slides 13-14: Casos Reales
- **Slide 13**: Sistema bancario: regulaciones y compliance
- **Slide 14**: FinTech: escalabilidad y disponibilidad

##### Ejercicio Final (30 min)
- **Contexto**: Wallet digital con integración bancaria
- **Objetivo**: Análisis completo de requisitos
- **Entregables**: Funcionales, NFRs, casos de uso, trazabilidad

### 📊 Evaluación Clase 2
#### Criterios de Dominio:
- [ ] **Clasificación**: Distinguir funcionales vs no funcionales
- [ ] **User Stories**: Formato Connextra con criterios de aceptación
- [ ] **NFRs**: Especificación con métricas medibles
- [ ] **Trazabilidad**: Matrices completas de seguimiento
- [ ] **Herramientas**: Uso de Jira/Azure DevOps

#### Métricas de Éxito:
- **Precisión clasificación**: >90% correcta
- **Completitud user stories**: Todos los criterios INVEST
- **NFRs medibles**: 100% con métricas específicas
- **Trazabilidad**: Cobertura completa requisitos → tests

---

## 🔄 INTEGRACIÓN CURSO 1 & 2

### 🎯 Sinergia Entre Clases
#### **Flujo de Trabajo Completo**:
1. **Análisis de Requisitos** → Casos de uso detallados
2. **Diseño de Código** → Estructura basada en requisitos
3. **Implementación** → Código limpio y mantenible
4. **Validación** → Tests que cubren casos de uso

#### **Métricas Integradas**:
- **Trazabilidad**: Requisito → Código → Test
- **Complejidad**: Métodos alineados con casos de uso
- **Calidad**: Code smells = 0, NFRs cumplidos

### 🛠️ Herramientas Unificadas
#### **Pipeline Completo**:
- **Análisis**: Jira (requisitos) → SonarLint (código)
- **Desarrollo**: Visual Studio con extensiones de calidad
- **Validación**: Tests automatizados + métricas de código
- **Entrega**: CI/CD con quality gates

### 📚 Proyecto Integrador
#### **Contexto**: Sistema de Pagos QR Enterprise
- **Requisitos**: 25 funcionales + 17 NFRs
- **Implementación**: Código con complejidad <5
- **Validación**: Tests con 90% cobertura
- **Entrega**: Pipeline automatizado con calidad

---

## 📈 PLAN DE EVALUACIÓN INTEGRADA

### 🎯 Evaluación por Competencias

#### **Competencia 1: Análisis Técnico**
- **Peso**: 25%
- **Criterios**:
  - Clasificación correcta de requisitos
  - Cálculo de complejidad ciclomática
  - Identificación de code smells
- **Instrumento**: Ejercicios prácticos

#### **Competencia 2: Implementación**
- **Peso**: 40%
- **Criterios**:
  - Código limpio sin advertencias SonarLint
  - Aplicación de patrones de refactoring
  - Cumplimiento de NFRs
- **Instrumento**: Proyecto integrador

#### **Competencia 3: Proceso y Herramientas**
- **Peso**: 25%
- **Criterios**:
  - Uso correcto de herramientas
  - Documentación de requisitos
  - Trazabilidad completa
- **Instrumento**: Entregables documentados

#### **Competencia 4: Análisis Crítico**
- **Peso**: 10%
- **Criterios**:
  - Justificación de decisiones técnicas
  - Propuestas de mejora
  - Detección de riesgos
- **Instrumento**: Presentación oral

### 📊 Métricas de Éxito Integradas
#### **Técnicas**:
- **Complejidad promedio**: <5 en todos los métodos
- **Code smells**: 0 en código entregado
- **NFRs**: 100% cumplidos con métricas
- **Trazabilidad**: Cobertura completa requisitos → tests

#### **Profesionales**:
- **Tiempo de desarrollo**: -30% vs baseline
- **Bugs en producción**: -50% vs promedio
- **Velocidad de nuevos features**: +40%
- **Satisfacción del equipo**: >85%

---

## 🚀 RECURSOS Y MATERIALES

### 📚 Materiales por Clase
#### **Clase 1 - Análisis de Código Estático**:
- **Presentación**: `doc/codestatico-presentacion.html` (18 slides)
- **Documentación**: `doc/curso.codestatico.md`
- **Ejercicios**: `samples/clase1-analisis-codigo-estatico/`
- **Herramientas**: SonarLint, Visual Studio Code Metrics

#### **Clase 2 - Análisis de Requisitos**:
- **Presentación**: `doc/requisitos-presentacion.html` (14 slides)
- **Ejercicios**: `samples/clase2-analisis-requisitos/`
- **Plantillas**: User Stories, Casos de Uso, NFRs
- **Herramientas**: Jira, Confluence, Azure DevOps

### 🛠️ Configuración Técnica
#### **Herramientas Requeridas**:
- **IDE**: Visual Studio 2022 Community+
- **Extensiones**: SonarLint, CodeMaid, Roslynator
- **Gestión**: Jira, Azure DevOps, GitHub
- **Análisis**: SonarCloud, Code Metrics

#### **Configuración Avanzada**:
- **EditorConfig**: Reglas de código unificadas
- **Directory.Build.props**: Configuración de solución
- **Quality Gates**: Umbrales automáticos
- **Pipeline CI/CD**: Integración completa

### 📖 Recursos Adicionales
#### **Libros Recomendados**:
- "Clean Code" - Robert C. Martin
- "Software Requirements" - Karl Wiegers
- "Refactoring" - Martin Fowler
- "BABOK Guide" - IIBA

#### **Herramientas Profesionales**:
- **Enterprise**: SonarQube, NDepend, ReqSuite
- **Análisis**: PVS-Studio, Veracode, Checkmarx
- **Gestión**: Jira Portfolio, Azure DevOps Services
- **Documentación**: Confluence, GitLab Wiki, Notion

---

## 💡 METODOLOGÍA PEDAGÓGICA

### 🎯 Enfoques de Enseñanza

#### **Learning by Doing**:
- **Ejercicios prácticos** en cada módulo
- **Código real** de sistemas financieros
- **Herramientas profesionales** desde día 1
- **Feedback inmediato** con métricas objetivas

#### **Casos de Estudio Empresariales**:
- **Contexto FinTech**: Pagos, wallets, transferencias
- **Regulaciones reales**: PCI DSS, GDPR, ISO 27001
- **Escalabilidad**: 10,000+ TPS, 99.9% uptime
- **Equipos grandes**: 50+ desarrolladores

#### **Progresión Gradual**:
- **Conceptos básicos** → **Aplicación práctica**
- **Herramientas simples** → **Configuración avanzada**
- **Ejercicios guiados** → **Proyectos independientes**
- **Feedback individual** → **Presentaciones grupales**

### 📊 Adaptación por Nivel

#### **Desarrolladores Junior**:
- **Explicaciones paso a paso** con capturas de pantalla
- **Ejemplos cotidianos** para conceptos abstractos
- **Plantillas pre-completadas** para acelerar aprendizaje
- **Validación frecuente** de comprensión

#### **Desarrolladores Semi-Senior**:
- **Casos complejos** con múltiples stakeholders
- **Análisis de trade-offs** y decisiones arquitectónicas
- **Herramientas enterprise** y configuración avanzada
- **Liderazgo técnico** en ejercicios grupales

---

## 🎯 OBJETIVOS DE PROMOCIÓN

### 📈 Perfil Junior → Semi-Senior

#### **Competencias Desarrolladas**:
- **Análisis técnico**: Evaluar calidad de código objetivamente
- **Pensamiento arquitectónico**: Decisiones basadas en requisitos
- **Herramientas profesionales**: Dominio de SonarLint, Jira, CI/CD
- **Liderazgo técnico**: Establecer estándares en equipos

#### **Indicadores de Promoción**:
- **Código autónomo**: Sin revisiones por calidad
- **Mentoring**: Ayuda a desarrolladores junior
- **Propuestas técnicas**: Iniciativas de mejora
- **Comunicación**: Justifica decisiones técnicamente

### 🏆 Certificación y Reconocimiento

#### **Certificado de Completitud**:
- **Requisitos**: 90% en evaluaciones integradas
- **Validación**: Proyecto real implementado
- **Reconocimiento**: Área de Arquitectura
- **Vigencia**: 2 años con actualización continua

#### **Pathway de Crecimiento**:
- **Semi-Senior**: Dominio de calidad y requisitos
- **Senior**: Diseño arquitectónico y patrones
- **Tech Lead**: Liderazgo técnico y mentoring
- **Arquitecto**: Decisiones empresariales y estrategia

---

## 🔄 MEJORA CONTINUA

### 📊 Métricas de Efectividad

#### **Estudiantes**:
- **Satisfacción**: Encuesta post-curso >85%
- **Aplicación**: Uso en proyectos reales >90%
- **Promoción**: Ascenso en 6 meses >80%
- **Retención**: Permanencia en empresa >95%

#### **Empresas**:
- **Calidad**: Reducción bugs producción -50%
- **Velocidad**: Desarrollo nuevos features +40%
- **Costos**: Mantenimiento de código -30%
- **Satisfacción**: Equipos de desarrollo >85%

### 🔧 Actualizaciones Programadas

#### **Contenido**:
- **Herramientas**: Nuevas versiones SonarLint, .NET
- **Casos de estudio**: Actualizaciones empresariales
- **Regulaciones**: Cambios PCI DSS, GDPR
- **Mejores prácticas**: Evolución estándares industria

#### **Metodología**:
- **Feedback estudiantes**: Incorporación trimestral
- **Tendencias mercado**: Análisis anual
- **Tecnologías emergentes**: Evaluación semestral
- **Competencias futuras**: Roadmap a 2 años

---

**Versión**: 1.0  
**Fecha**: Julio 2025  
**Autor**: Alejandro Sfrede - Área de Arquitectura  
**Próxima Revisión**: Octubre 2025  

*Este plan integrado está diseñado para maximizar el impacto educativo y acelerar la promoción profesional de desarrolladores .NET en entornos empresariales.*