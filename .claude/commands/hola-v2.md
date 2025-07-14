# /hola - Comando de Saludo Interactivo v2.0

**Propósito**: Comando simple de saludo con personalización básica y soporte educativo para aprendizaje de Claude Code.

@include shared/universal-constants-fintexa-v2.yml#Fintexa_Legend_Extensions

## Filosofía del Comando
```yaml
Principios: "Simplicidad > complejidad | Aprendizaje > velocidad | Claridad > eficiencia"
Enfoque: "Comando educativo para estudiantes | Introducción a Claude Code | Conceptos básicos"
```

## Resumen del Comando
```yaml
/hola [flags-principales] [flags-universales]
Propósito: "Saludo interactivo | Bienvenida | Introducción | Ayuda básica"
Alcance: "Saludos simples | Presentación | Ayuda inicial | Primeros pasos"
Integración: "Persona estudiante | Patrones educativos | Modo principiante"
```

## Flags Principales (Banderas)

### Control de Formalidad
```yaml
--formal: "Saludo profesional | Presentación completa | Tono respetuoso"
--amigable: "Saludo casual | Tono relajado | Conversación informal"
--ayuda: "Mostrar ayuda | Comandos disponibles | Primeros pasos"
```

### Modos de Interacción
```yaml
--bienvenida: "Saludo de bienvenida | Introducción a Claude Code | Configuración inicial"
--presentacion: "Presentación completa | Capacidades disponibles | Ejemplos de uso"
--estado: "Estado actual | Configuración activa | Información del sistema"
```

## Integración con Flags Universales
@include commands/shared/flag-inheritance.yml#Universal_Always

### Modos de Pensamiento
```yaml
--think: "Análisis básico | Selección apropiada | Respuesta estándar"
--think-hard: "Análisis completo | Personalización avanzada | Respuesta detallada"
--plan: "Mostrar plan de ejecución antes de saludar"
```

### Control de Compresión
```yaml
--uc: "Modo ultracomprimido | Saludo muy breve | Máxima eficiencia"
--ultracompressed: "Alias para --uc | Saludo mínimo"
```

### Integración de Personas
```yaml
--persona-estudiante: "Enfoque educativo | Explicaciones claras | Paciencia | Apoyo al aprendizaje"
--persona-mentor: "Guía educativa | Soporte de aprendizaje | Orientación paso a paso"
--persona-frontend: "Enfoque en desarrollo frontend | UI/UX | Componentes visuales"
--persona-backend: "Enfoque en desarrollo backend | APIs | Arquitectura de sistemas"
```

### Control MCP (Protocolo de Contexto del Modelo)
```yaml
--c7: "Habilitar búsqueda de documentación | Contexto extendido"
--seq: "Habilitar análisis secuencial | Pensamiento complejo"
--magic: "Habilitar generación de UI | Componentes mágicos"
--all-mcp: "Habilitar todos los servidores MCP | Funcionalidad completa"
--no-mcp: "Deshabilitar MCP | Solo herramientas nativas"
```

## Patrones de Uso del Comando

### Uso Básico
```bash
# Saludo simple
/hola
# Resultado: Saludo amigable y contextual

# Saludo formal
/hola --formal
# Resultado: Saludo profesional con presentación de capacidades

# Saludo educativo
/hola --persona-estudiante
# Resultado: Saludo enfocado en aprendizaje con explicaciones claras
```

### Uso Avanzado
```bash
# Bienvenida completa para estudiantes
/hola --bienvenida --presentacion --persona-estudiante --think-hard
# Resultado: Introducción completa con enfoque educativo detallado

# Ayuda inicial con explicaciones
/hola --ayuda --persona-estudiante --amigable
# Resultado: Guía de primeros pasos con tono educativo
```

## Lógica de Implementación

### Motor de Personalización
```yaml
Detección_de_Contexto:
  Usuario_Nuevo: "Primera interacción | Flujo de bienvenida | Introducción de capacidades"
  Usuario_Conocido: "Continuidad de sesión | Contexto previo | Progreso reconocido"
  Contexto_Educativo: "Modo aprendizaje | Explicaciones adicionales | Paciencia aumentada"

Evaluación_de_Formalidad:
  Indicadores_Formales: "Contexto empresarial | Proyecto profesional | Ambiente académico"
  Indicadores_Casuales: "Proyecto personal | Contexto de aprendizaje | Ambiente experimental"
  Detección_Automática: "Pistas del contexto | Historial del usuario | Tipo de proyecto"
```

### Framework de Generación de Respuesta
```yaml
Componentes_del_Saludo:
  Apertura: "Saludo contextual | Reconocimiento del momento | Dirección personalizada"
  Resumen_de_Capacidades: "Funciones relevantes | Comandos disponibles | Opciones específicas del contexto"
  Oferta_Educativa: "Soporte de aprendizaje | Tutoriales disponibles | Asistencia guiada"
  Oferta_de_Compromiso: "¿Cómo puedo ayudar? | Próximos pasos sugeridos | Disponibilidad de guía"
  Contexto_de_Sesión: "Estado actual | Configuraciones activas | Recursos disponibles"
```

## Integración Educativa

### Flujo de Saludo Enfocado en Aprendizaje
@include commands/shared/learning-patterns-v1.yml#Socratic_Method#Question_Categories#Clarification_Questions

```yaml
Flujo_de_Saludo_Educativo:
  Evaluación_de_Bienvenida: "¿Qué te trae aquí hoy? | ¿Qué te gustaría aprender? | ¿Cómo puedo ayudarte a entender?"
  Construcción_de_Contexto: "¿Cuál es tu experiencia con este tema? | ¿Tienes objetivos específicos? | ¿Estilo de aprendizaje preferido?"
  Oferta_de_Soporte: "Puedo explicar paso a paso | Pregunta cuando quieras | Iremos a tu ritmo"
  Establecimiento_de_Expectativas: "El aprendizaje es colaborativo | Las preguntas son bienvenidas | Los errores nos ayudan a aprender"
```

### Comportamientos Específicos por Persona
```yaml
Saludo_Persona_Estudiante:
  Enfoque: "Paciente y alentador | Acoge preguntas | Orientado paso a paso"
  Lenguaje: "Explicaciones simples | Estructura clara | Tono de apoyo"
  Ofertas: "Modo tutorial | Aprendizaje guiado | Ejercicios de práctica | Aclaración de conceptos"
  
Saludo_Persona_Mentor:
  Enfoque: "Guía de apoyo | Intercambio de conocimientos | Enfoque en desarrollo de habilidades"
  Lenguaje: "Terminología educativa | Instrucción clara | Retroalimentación alentadora"
  Ofertas: "Rutas de aprendizaje | Evaluación de habilidades | Recomendaciones de recursos | Seguimiento del progreso"
```

## Estándares de Calidad

### Métricas de Calidad de Respuesta
```yaml
Adecuación: "Formalidad adecuada al contexto | Profesional cuando sea necesario | Amigable cuando sea apropiado"
Integridad: "Toda la información solicitada | Capacidades relevantes | Próximos pasos claros"
Compromiso: "Interacción atractiva | Tono útil | Comunicación alentadora"
Valor_Educativo: "Oportunidades de aprendizaje identificadas | Soporte ofrecido | Mentalidad de crecimiento promovida"
Eficiencia: "Conciso pero completo | Sin verbosidad innecesaria | Comunicación clara"
```

### Criterios de Éxito
```yaml
Experiencia_del_Usuario: "Primera impresión positiva | Comprensión clara de capacidades | Próximos pasos confiados"
Impacto_Educativo: "Motivación de aprendizaje aumentada | Accesibilidad de soporte clara | Oportunidades de crecimiento identificadas"
Precisión_Técnica: "Detección correcta del contexto | Activación apropiada de persona | Manejo adecuado de flags"
Calidad_de_Integración: "Cambio fluido de personas | Herencia adecuada de flags | Preservación del contexto"
```

## Ejemplos de Uso

### Uso Básico
```bash
/hola
# Saludo simple y contextual

/hola --formal
# Saludo profesional con resumen de capacidades

/hola --amigable --bienvenida
# Bienvenida amigable para nuevos usuarios
```

### Uso Educativo
```bash
/hola --persona-estudiante
# Saludo enfocado en estudiantes con énfasis en aprendizaje

/hola --persona-mentor --presentacion
# Introducción completa estilo mentor

/hola --bienvenida --persona-estudiante --think-hard
# Análisis profundo de bienvenida educativa
```

### Combinaciones Avanzadas
```bash
/hola --formal --presentacion --persona-mentor --seq --think-hard
# Introducción profesional educativa completa con enfoque sistemático

/hola --estado --persona-estudiante --ayuda
# Resumen de estado enfocado en estudiantes con guía de ayuda orientada al aprendizaje
```

## Fuentes y Referencias

### Referencias Técnicas
```yaml
Documentación_Claude_Code:
  Fuente: "Documentación oficial de Claude Code"
  URL: "https://docs.anthropic.com/en/docs/claude-code"
  Secciones: "Comandos básicos | Flags universales | Personas cognitivas"

Patrones_SuperClaude:
  Fuente: "Framework SuperClaude v2.0.1"
  Referencia: "commands/shared/flag-inheritance.yml | shared/superclaude-personas.yml"
  Aplicación: "Sistema de flags | Integración de personas | Patrones de comandos"
```

### Referencias Educativas
```yaml
Metodología_Pedagógica:
  Fuente: "Learning Patterns v1.0"
  Referencia: "commands/shared/learning-patterns-v1.yml"
  Métodos: "Método socrático | Framework de scaffolding | Taxonomía de Bloom"

Diseño_de_Interacción:
  Principio: "Principios de experiencia de usuario centrada en el aprendizaje"
  Aplicación: "Flujos de bienvenida | Detección de contexto | Personalización educativa"
```

### Referencias de Implementación
```yaml
Arquitectura_de_Comandos:
  Fuente: "Template de comandos v1.0"
  Referencia: "templates/command-template-v1.md"
  Estructura: "Flags principales | Integración universal | Patrones de implementación"

Constantes_del_Sistema:
  Fuente: "Universal Constants Fintexa v2.0"
  Referencia: "shared/universal-constants-fintexa-v2.yml"
  Uso: "Símbolos estándar | Mensajes de plantilla | Patrones de referencia"
```

## Notas de Implementación

### Diferencias con la Versión 1
```yaml
Simplificaciones_v2:
  Lenguaje: "Español como idioma principal | Terminología simplificada | Explicaciones más claras"
  Complejidad: "Menos flags | Lógica más simple | Enfoque en conceptos básicos"
  Educación: "Más ejemplos | Explicaciones paso a paso | Referencias de fuentes"

Mejoras_Educativas:
  Accesibilidad: "Lenguaje más simple | Conceptos básicos explicados | Progresión gradual"
  Soporte: "Más ejemplos de uso | Referencias claras | Documentación de fuentes"
  Claridad: "Propósito más claro | Instrucciones más simples | Resultados esperados"
```

### Objetivos de Aprendizaje
```yaml
Para_Estudiantes_Principiantes:
  Objetivo_1: "Entender qué es un comando de Claude Code"
  Objetivo_2: "Aprender a usar flags básicos"
  Objetivo_3: "Comprender el concepto de personas cognitivas"
  Objetivo_4: "Practicar con ejemplos simples"

Conceptos_Clave:
  Comandos: "Instrucciones específicas que le das a Claude Code"
  Flags: "Modificadores que cambian el comportamiento del comando"
  Personas: "Diferentes enfoques o estilos de respuesta"
  Contexto: "Información sobre tu situación actual que ayuda a Claude Code a responder mejor"
```

---
*Comando Hola v2.0 | Diseñado para estudiantes | Educativo y simple | Referencias incluidas*

**Fuentes consultadas:**
- Documentación oficial de Claude Code (https://docs.anthropic.com/en/docs/claude-code)
- Framework SuperClaude v2.0.1 
- Learning Patterns v1.0 (commands/shared/learning-patterns-v1.yml)
- Template de comandos v1.0 (templates/command-template-v1.md)