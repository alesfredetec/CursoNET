# EJERCICIO INTEGRADOR 1: Sistema de Pagos QR - Expansión Comercial

**Duración:** 60 minutos  
**Nivel:** Desarrollador Junior  
**Contexto:** Fintexa - Plataforma de Medios de Pago

---

## 1. CONTEXTO DEL CLIENTE/NEGOCIO

**Fintexa** es una empresa líder en medios de pago digitales que opera una plataforma de alta disponibilidad procesando miles de transacciones diarias. Actualmente maneja pagos a través de tarjetas, wallets digitales, códigos QR y terminales POS, con integraciones directas a Visa y Coelsa.

La empresa ha identificado una **oportunidad de crecimiento significativa** en el segmento de pagos QR, especialmente para comercios pequeños y medianos que buscan alternativas de pago digitales accesibles y seguras.

### Situación Actual
El sistema actual de pagos QR de Fintexa solo maneja **códigos QR estáticos básicos** para pagos de monto fijo. Los comercios han expresado la necesidad de mayor flexibilidad y funcionalidades avanzadas para competir en el mercado digital.

### Oportunidad de Mercado
El análisis de mercado indica que los comercios requieren:
- Códigos QR dinámicos con montos variables
- Pagos recurrentes por suscripciones
- Integración con programas de lealtad
- Procesamiento más rápido y confiable
- Mejor experiencia para el cliente final

---

## 2. NECESIDADES IDENTIFICADAS

### Problemática Empresarial
1. **Limitación de Producto**: Solo se soporta un tipo de código QR, limitando las opciones para comercios
2. **Pérdida de Competitividad**: Competidores ofrecen soluciones más flexibles y completas
3. **Insatisfacción del Cliente**: Comercios solicitan constantemente nuevas funcionalidades
4. **Crecimiento Estancado**: El segmento QR no está creciendo al ritmo esperado

### Impacto en el Negocio
- **Retención de clientes en riesgo**: 15% de comercios considerando cambiar de proveedor
- **Oportunidad de ingresos**: Se estima 40% de incremento en transacciones QR con nuevas funcionalidades
- **Posición competitiva**: Riesgo de perder liderazgo en el segmento de pagos digitales

---

## 3. REQUERIMIENTOS DEL SISTEMA

### Funcionalidades Requeridas

#### A. Tipos de Códigos QR a Soportar
1. **QR Estático**: Monto fijo, reutilizable (funcionalidad actual)
2. **QR Dinámico**: Monto variable definido por el comercio
3. **QR Recurrente**: Para suscripciones y pagos periódicos

#### B. Proceso de Validación
- Validar formato y estructura del código QR
- Verificar disponibilidad de fondos del pagador
- Confirmar límites de transacción del comercio
- Aplicar reglas de seguridad PCI DSS

#### C. Integración con Sistemas Existentes
- Conexión con procesador de pagos Visa/Coelsa
- Registro en sistema de auditoría y compliance
- Notificaciones en tiempo real a comercios y usuarios

### Restricciones Técnicas
- **Tiempo de respuesta**: Máximo 3 segundos por transacción
- **Disponibilidad**: 99.9% uptime
- **Seguridad**: Cumplimiento PCI DSS Level 1
- **Concurrencia**: Soporte para 1,000 transacciones simultáneas

---

## 4. CRITERIOS DE ÉXITO

### Métricas de Negocio
- **Adopción**: 80% de comercios existentes migren a nuevos tipos de QR en 3 meses
- **Crecimiento**: 25% incremento en volumen de transacciones QR mensual
- **Satisfacción**: NPS de comercios > 8.5
- **Retención**: Reducir churn de comercios a < 2% mensual

### Métricas Técnicas
- **Performance**: 95% de transacciones completadas en < 2 segundos
- **Confiabilidad**: 0 incidentes críticos relacionados con procesamiento QR
- **Escalabilidad**: Sistema soporta 3x el volumen actual sin degradación

---

## 5. ENTREGABLES ESPERADOS

### Fase 1: Análisis (15 minutos)
**Entregable**: Documento de análisis de requisitos
- Identificación de requisitos funcionales y no funcionales
- Matriz de trazabilidad de necesidades del negocio vs. requisitos técnicos
- Análisis de riesgos y dependencias críticas

### Fase 2: Diseño (20 minutos)
**Entregable**: Diseño de arquitectura de la solución
- Diagrama de componentes del sistema
- Definición de interfaces y contratos
- Modelo de datos para nuevos tipos de QR
- Estrategia de manejo de diferentes tipos de validación

### Fase 3: Implementación (20 minutos)
**Entregable**: Código base funcional
- Implementación de procesador de códigos QR
- Sistema de validaciones por tipo de QR
- Integración con servicios de pago existentes
- Casos de prueba básicos

### Fase 4: Validación (5 minutos)
**Entregable**: Plan de validación
- Checklist de cumplimiento de requisitos
- Estrategia de testing para diferentes escenarios
- Métricas de calidad de código

---

## RECURSOS DISPONIBLES

### Información Técnica
- API existente de procesamiento de pagos Fintexa
- Documentación de integración Visa/Coelsa
- Estándares de seguridad PCI DSS aplicables
- Especificaciones técnicas de códigos QR ISO/IEC 18004

### Herramientas de Desarrollo
- .NET Core 8
- SQL Server para persistencia
- Azure Service Bus para mensajería
- SonarQube para análisis de calidad de código

---

## NOTAS IMPORTANTES

- **Enfoque en Calidad**: El código debe ser mantenible y extensible para futuras funcionalidades
- **Seguridad Prioritaria**: Toda implementación debe considerar las mejores prácticas de seguridad financiera
- **Escalabilidad**: Diseñar pensando en el crecimiento exponencial del volumen de transacciones
- **Experiencia del Usuario**: Priorizar simplicidad y velocidad en el procesamiento

---

**IMPORTANTE**: Este ejercicio simula un caso real de desarrollo en Fintexa. Se espera que el desarrollador demuestre capacidad de análisis, diseño estructurado y implementación siguiendo buenas prácticas de desarrollo de software empresarial.