# EJERCICIO 2: Descripción Informal de Funcionalidad

## Escenario: Sistema de Procesamiento de Pagos para FinTech

**INSTRUCCIONES:**
1. Lee la descripción informal de funcionalidad del sistema
2. Identifica los actores, casos de uso y flujos principales
3. Analiza qué información falta para crear casos de uso formales
4. Compara tu análisis con `02-UseCase-Example-After.md`

---

## Descripción Informal del Sistema de Pagos

### Contexto del Negocio
**FinTech:** Una empresa de tecnología financiera que ofrece servicios de procesamiento de pagos digitales para comercios electrónicos y físicos.

### Funcionalidad Descrita por el Product Owner:

*"Necesitamos un sistema que permita a los comercios procesar pagos de sus clientes. Los comercios se registran en nuestra plataforma y configuran sus métodos de pago. Cuando un cliente compra algo, el comercio usa nuestro sistema para procesar el pago."*

*"Los clientes pueden pagar con tarjeta de crédito, débito, transferencia bancaria o billeteras digitales. El sistema debe validar los datos de la tarjeta y autorizar el pago con el banco. Si todo está bien, se descuenta el dinero y se le avisa al comercio que puede entregar el producto."*

*"También necesitamos que los comercios puedan ver sus ventas, generar reportes y hacer seguimiento de los pagos. Los administradores de nuestra empresa deben poder monitorear todas las transacciones y detectar fraudes."*

*"Ah, y los clientes a veces quieren devolver productos, entonces necesitamos poder reversar pagos. Y los comercios quieren que les depositemos el dinero de sus ventas cada día."*

### Información Técnica Adicional:
- Los pagos se procesan en tiempo real
- Se debe integrar con múltiples bancos y procesadores de pago
- El sistema debe cumplir con estándares PCI DSS
- Los comercios acceden vía API REST y dashboard web
- Se requiere notificaciones por email y webhook

---

## Preguntas de Análisis

**IDENTIFICAR ACTORES:**
1. ¿Quiénes son los usuarios del sistema?
2. ¿Qué roles tienen cada uno?
3. ¿Hay sistemas externos que interactúan?

**IDENTIFICAR CASOS DE USO:**
1. ¿Qué acciones principales puede realizar cada actor?
2. ¿Cuáles son los flujos principales de negocio?
3. ¿Qué casos de uso están implícitos pero no mencionados?

**ANALIZAR FLUJOS:**
1. ¿Cuál es el flujo principal de procesamiento de pagos?
2. ¿Qué puede salir mal en cada paso?
3. ¿Qué flujos alternativos existen?

**IDENTIFICAR INFORMACIÓN FALTANTE:**
1. ¿Qué precondiciones no están claras?
2. ¿Qué postcondiciones falta definir?
3. ¿Qué reglas de negocio faltan?
4. ¿Qué excepciones no están contempladas?

---

## Retos Adicionales

**ANÁLISIS DE STAKEHOLDERS:**
- ¿Qué información adicional necesitarías de cada stakeholder?
- ¿Qué preguntas harías al Product Owner?
- ¿Qué departamentos necesitas involucrar?

**IDENTIFICAR COMPLEJIDADES:**
- ¿Qué integraciones técnicas son críticas?
- ¿Qué regulaciones legales aplican?
- ¿Qué aspectos de seguridad son críticos?

**PRIORIZACIÓN:**
- ¿Cuáles casos de uso son más críticos?
- ¿Cuáles pueden ser MVP vs features avanzadas?
- ¿Qué dependencias técnicas existen?

---

## Contexto Financiero Específico

### Regulaciones Aplicables:
- **PCI DSS:** Estándares de seguridad para datos de tarjetas
- **AML:** Prevención de lavado de dinero
- **KYC:** Conocimiento del cliente
- **GDPR:** Protección de datos personales

### Métricas de Negocio:
- **Tiempo de procesamiento:** < 3 segundos
- **Tasa de aprobación:** > 95%
- **Disponibilidad:** 99.9%
- **Detección de fraude:** > 99%

### Stakeholders Financieros:
- **Compliance Officer:** Cumplimiento regulatorio
- **Risk Manager:** Gestión de riesgos
- **Treasury:** Gestión de liquidez
- **Auditor:** Controles internos

---

**TIEMPO ESTIMADO:** 60-90 minutos
**DIFICULTAD:** Intermedio-Avanzado
**SKILLS:** Análisis de requisitos, modelado de casos de uso, conocimiento financiero

**PRÓXIMO PASO:** Ver `02-UseCase-Example-After.md` para los casos de uso formales estructurados.