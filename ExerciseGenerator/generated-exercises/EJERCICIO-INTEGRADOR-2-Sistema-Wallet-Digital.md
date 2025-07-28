# EJERCICIO INTEGRADOR 2: Sistema de Wallet Digital - Segmentación de Usuarios

**Duración:** 60 minutos  
**Nivel:** Desarrollador Junior  
**Contexto:** Fintexa - Plataforma de Medios de Pago

---

## 1. CONTEXTO DEL CLIENTE/NEGOCIO

**Fintexa** es una empresa líder en medios de pago digitales que opera una plataforma de alta disponibilidad procesando miles of transacciones diarias. Actualmente maneja pagos a través de tarjetas, wallets digitales, códigos QR y terminales POS, con integraciones directas a Visa y Coelsa.

La empresa ha identificado una **oportunidad estratégica crítica** en el segmento de wallets digitales, donde la competencia está ganando terreno ofreciendo experiencias diferenciadas según el perfil del usuario y sus necesidades financieras específicas.

### Situación Actual
El sistema actual de wallet digital de Fintexa maneja **únicamente cuentas básicas** con límites fijos y funcionalidades estándar para todos los usuarios. Esta aproximación "one-size-fits-all" está generando insatisfacción en segmentos de clientes con necesidades específicas.

### Oportunidad de Mercado
El análisis de comportamiento de usuarios indica que diferentes segmentos requieren:
- Límites de transacción adaptativos según perfil y historial
- Velocidad de procesamiento diferenciada por tipo de cuenta
- Servicios premium para usuarios corporativos y de alto volumen
- Flexibilidad en métodos de recarga y retiro de fondos
- Gestión automatizada de límites basada en comportamiento histórico

---

## 2. NECESIDADES IDENTIFICADAS

### Problemática Empresarial
1. **Falta de Diferenciación**: Todos los usuarios tienen la misma experiencia, independientemente de su perfil
2. **Gestión Manual Ineficiente**: Los límites se ajustan manualmente, generando demoras operativas
3. **Pérdida de Usuarios Premium**: Empresas y usuarios de alto volumen migran a competidores
4. **Oportunidades de Revenue Perdidas**: No se monetiza adecuadamente el valor diferencial por segmento

### Impacto en el Negocio
- **Retención comprometida**: 22% de usuarios empresariales considerando otras plataformas
- **Crecimiento limitado**: Techo de ingresos por falta de servicios premium
- **Eficiencia operativa**: 40% del tiempo de soporte dedicado a ajustes manuales de límites
- **Competitividad**: Desventaja frente a plataformas con ofertas segmentadas

---

## 3. REQUERIMIENTOS DEL SISTEMA

### Funcionalidades Requeridas

#### A. Tipos de Cuentas a Soportar
1. **Cuenta Personal**: Usuarios individuales con límites estándar y funcionalidades básicas
2. **Cuenta Empresarial**: Empresas con límites elevados y herramientas de gestión avanzadas
3. **Cuenta Premium**: Usuarios de alto volumen con límites adaptativos y procesamiento prioritario

#### B. Operaciones de Wallet
- **Recarga de Fondos**: Desde múltiples fuentes (tarjetas, transferencias, proveedores externos)
- **Retiro de Fondos**: A cuentas bancarias con diferentes tiempos según tipo de cuenta
- **Transferencias**: Entre wallets Fintexa con validaciones específicas por segmento
- **Consultas de Saldo**: Con historiales detallados según nivel de cuenta

#### C. Sistema de Límites Dinámicos
- Cálculo automático de límites basado en historial de transacciones
- Ajustes en tiempo real según comportamiento y tipo de cuenta
- Escalamiento automático para usuarios que califican para upgrades

#### D. Integración con Proveedores
- Conexión con múltiples proveedores de fondeo (bancos, procesadores, fintechs)
- Enrutamiento inteligente según disponibilidad y costos por proveedor
- Fallback automático ante indisponibilidad de proveedores primarios

### Restricciones Técnicas
- **Tiempo de respuesta**: Máximo 2 segundos para operaciones básicas, 5 segundos para cálculos de límites
- **Disponibilidad**: 99.9% uptime con recuperación automática ante fallos
- **Seguridad**: Cumplimiento PCI DSS Level 1 y regulaciones financieras locales
- **Concurrencia**: Soporte para 2,000 operaciones simultáneas de wallet

---

## 4. CRITERIOS DE ÉXITO

### Métricas de Negocio
- **Adopción Premium**: 15% de usuarios personales upgradeen a cuentas premium en 6 meses
- **Retención Empresarial**: Incrementar retención de cuentas empresariales a 95%
- **Revenue Growth**: 35% incremento en ingresos por comisiones de wallet
- **Satisfacción**: NPS usuarios premium > 9.0, empresariales > 8.5

### Métricas Operativas
- **Automatización**: 80% reducción en intervenciones manuales para ajuste de límites
- **Eficiencia**: 90% de operaciones completadas dentro de límites de tiempo establecidos
- **Disponibilidad de Proveedores**: 99.5% disponibilidad agregada del sistema de fondeo
- **Escalabilidad**: Sistema soporta 5x volumen actual sin impacto en performance

---

## 5. ENTREGABLES ESPERADOS

### Fase 1: Análisis (15 minutos)
**Entregable**: Documento de análisis de requisitos para wallet segmentado
- Identificación de requisitos funcionales específicos por tipo de cuenta
- Matriz de trazabilidad entre necesidades de negocio y capacidades técnicas
- Análisis de dependencias críticas con proveedores externos
- Definición de reglas de negocio para límites dinámicos

### Fase 2: Diseño (20 minutos)
**Entregable**: Arquitectura de la solución de wallet multi-segmento
- Diseño de componentes para gestión diferenciada de cuentas
- Modelo de datos para saldos, límites y configuraciones por tipo
- Definición de interfaces para integración con proveedores múltiples
- Estrategia de cálculo automático de límites y upgrades de cuenta

### Fase 3: Implementación (20 minutos)
**Entregable**: Código base del sistema de wallet segmentado
- Motor de procesamiento de operaciones por tipo de cuenta
- Sistema de validación de límites dinámicos
- Integración con proveedores de fondeo con failover
- Lógica de negocio para automatización de límites

### Fase 4: Validación (5 minutos)
**Entregable**: Estrategia de validación integral
- Checklist de cumplimiento por tipo de cuenta
- Escenarios de testing para operaciones multi-proveedor
- Métricas de performance y calidad de código
- Plan de testing de límites dinámicos y escalamientos

---

## RECURSOS DISPONIBLES

### Información Técnica
- API existente de procesamiento de transacciones Fintexa
- Documentación de integración con proveedores de fondeo actuales
- Estándares de seguridad PCI DSS para manejo de fondos digitales
- Especificaciones técnicas de wallets digitales y regulaciones financieras

### Herramientas de Desarrollo
- .NET Core 8 con Entity Framework para persistencia
- SQL Server para gestión de saldos y configuraciones
- Azure Service Bus para notificaciones en tiempo real
- Redis para caché de límites y configuraciones frecuentes

---

## NOTAS IMPORTANTES

- **Integridad Financiera**: Toda operación debe mantener consistencia de saldos bajo cualquier escenario
- **Experiencia Diferenciada**: Cada tipo de cuenta debe ofrecer valor específico y medible
- **Automatización Inteligente**: Minimizar intervención manual priorizando algoritmos confiables
- **Flexibilidad de Proveedores**: Diseñar para fácil integración de nuevos proveedores de fondeo

---

**IMPORTANTE**: Este ejercicio simula un caso real de desarrollo en Fintexa enfocado en la evolución del producto wallet. Se espera que el desarrollador demuestre capacidad de diseño de sistemas financieros complejos, manejo de reglas de negocio dinámicas y arquitectura resiliente para integración con múltiples proveedores.