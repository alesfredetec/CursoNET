# PLANTILLA: User Stories para Sistemas FinTech

## Escenario: Sistema de Procesamiento de Pagos para FinTech

**PROPÓSITO:**
Esta plantilla muestra cómo convertir casos de uso formales en historias de usuario ágiles, manteniendo la trazabilidad y completitud.

---

## 1. FORMATO ESTÁNDAR DE USER STORIES

### Estructura Connextra:
```
Como [tipo de usuario]
Quiero [funcionalidad/objetivo]
Para [beneficio/valor de negocio]
```

### Elementos Complementarios:
- **Criterios de Aceptación:** Condiciones que deben cumplirse
- **Definición de Terminado:** Checklist de calidad
- **Estimación:** Story Points o tiempo
- **Prioridad:** Impacto en el negocio
- **Dependencias:** Relaciones con otras historias

---

## 2. EJEMPLOS DE USER STORIES

### Epic: Procesamiento de Pagos

#### US-001: Procesar Pago con Tarjeta de Crédito
**Como** comercio registrado en la plataforma
**Quiero** procesar pagos de mis clientes con tarjeta de crédito
**Para** completar las ventas de manera segura y eficiente

**Criterios de Aceptación:**
- [ ] **Dado** que soy un comercio autenticado
- [ ] **Y** tengo una transacción de venta activa
- [ ] **Cuando** envío los datos de pago del cliente
- [ ] **Entonces** el sistema debe validar los datos de la tarjeta
- [ ] **Y** debe procesar el pago en menos de 3 segundos
- [ ] **Y** debe notificarme el resultado vía webhook
- [ ] **Y** debe enviar comprobante al cliente vía email

**Escenarios de Excepción:**
- [ ] **Dado** que los datos de la tarjeta son inválidos
- [ ] **Cuando** intento procesar el pago
- [ ] **Entonces** debo recibir un mensaje de error específico
- [ ] **Y** la transacción debe ser rechazada

- [ ] **Dado** que la tarjeta no tiene fondos suficientes
- [ ] **Cuando** intento procesar el pago
- [ ] **Entonces** debo recibir notificación de fondos insuficientes
- [ ] **Y** el cliente debe ser informado del rechazo

**Definición de Terminado:**
- [ ] Código desarrollado y revisado
- [ ] Pruebas unitarias implementadas (cobertura > 80%)
- [ ] Pruebas de integración con procesador de pagos
- [ ] Pruebas de seguridad PCI DSS
- [ ] Documentación API actualizada
- [ ] Validación con comercio piloto

**Estimación:** 13 Story Points
**Prioridad:** Crítica
**Dependencias:** US-006 (Autenticación de Comercio)

---

#### US-002: Procesar Pago con Transferencia Bancaria
**Como** comercio registrado en la plataforma
**Quiero** ofrecer transferencia bancaria como opción de pago
**Para** ampliar las opciones de pago de mis clientes

**Criterios de Aceptación:**
- [ ] **Dado** que soy un comercio autenticado
- [ ] **Y** tengo configurada la opción de transferencia bancaria
- [ ] **Cuando** un cliente selecciona esta opción de pago
- [ ] **Entonces** el sistema debe generar datos bancarios únicos
- [ ] **Y** debe proporcionar referencia de pago al cliente
- [ ] **Y** debe monitorear la recepción del pago
- [ ] **Y** debe confirmar automáticamente cuando se reciba el dinero

**Escenarios Adicionales:**
- [ ] **Dado** que el pago no se recibe en 24 horas
- [ ] **Cuando** expira el tiempo límite
- [ ] **Entonces** la transacción debe marcarse como expirada
- [ ] **Y** debe notificarse al comercio y cliente

**Estimación:** 8 Story Points
**Prioridad:** Media
**Dependencias:** US-001, US-007 (Integración Bancaria)

---

#### US-003: Reversar Pago Procesado
**Como** comercio registrado en la plataforma
**Quiero** reversar pagos ya procesados
**Para** manejar devoluciones y cancelaciones de pedidos

**Criterios de Aceptación:**
- [ ] **Dado** que tengo una transacción exitosa
- [ ] **Y** han pasado menos de 30 días desde el pago
- [ ] **Cuando** solicito reversar el pago
- [ ] **Entonces** el sistema debe validar mi autorización
- [ ] **Y** debe procesar la devolución con el banco
- [ ] **Y** debe notificarme el resultado
- [ ] **Y** debe informar al cliente sobre la devolución

**Reglas de Negocio:**
- [ ] Solo se pueden reversar pagos de los últimos 30 días
- [ ] El monto a reversar no puede exceder el pago original
- [ ] Se requiere justificación para auditoría
- [ ] Las reversiones parciales están permitidas

**Estimación:** 8 Story Points
**Prioridad:** Alta
**Dependencias:** US-001

---

### Epic: Gestión de Comercios

#### US-004: Registrar Nuevo Comercio
**Como** dueño de un negocio
**Quiero** registrarme en la plataforma de pagos
**Para** comenzar a procesar pagos de mis clientes

**Criterios de Aceptación:**
- [ ] **Dado** que soy un comercio no registrado
- [ ] **Cuando** completo el formulario de registro
- [ ] **Entonces** debo proporcionar información legal de mi empresa
- [ ] **Y** debo cargar documentación oficial vigente
- [ ] **Y** debo configurar cuenta bancaria para liquidación
- [ ] **Y** debo aceptar términos y condiciones
- [ ] **Y** el sistema debe validar la información KYC
- [ ] **Y** debo recibir confirmación de registro en 72 horas

**Criterios de Validación KYC:**
- [ ] RUT/NIT empresarial válido
- [ ] Escritura de constitución vigente
- [ ] Poderes de representación legal
- [ ] Cuenta bancaria empresarial verificada
- [ ] Verificación de antecedentes comerciales

**Estimación:** 13 Story Points
**Prioridad:** Alta
**Dependencias:** Ninguna

---

#### US-005: Consultar Mis Transacciones
**Como** comercio registrado en la plataforma
**Quiero** consultar el historial de mis transacciones
**Para** hacer seguimiento de mis ventas y conciliaciones

**Criterios de Aceptación:**
- [ ] **Dado** que soy un comercio autenticado
- [ ] **Cuando** accedo al dashboard de transacciones
- [ ] **Entonces** debo ver solo mis transacciones
- [ ] **Y** debo poder filtrar por fecha, estado, monto
- [ ] **Y** debo poder exportar datos a Excel/CSV
- [ ] **Y** debo ver detalles completos de cada transacción
- [ ] **Y** el sistema debe paginar resultados (50 por página)

**Filtros Disponibles:**
- [ ] Rango de fechas (desde/hasta)
- [ ] Estado (exitosa, fallida, pendiente, reversada)
- [ ] Rango de montos (mínimo/máximo)
- [ ] Método de pago (tarjeta, transferencia, etc.)
- [ ] Moneda (USD, EUR, COP, etc.)

**Estimación:** 5 Story Points
**Prioridad:** Media
**Dependencias:** US-004

---

### Epic: Administración y Monitoreo

#### US-006: Detectar Transacciones Fraudulentas
**Como** administrador de la plataforma
**Quiero** detectar automáticamente transacciones sospechosas
**Para** prevenir fraudes y proteger a comercios y clientes

**Criterios de Aceptación:**
- [ ] **Dado** que se procesa una transacción
- [ ] **Cuando** el sistema evalúa patrones de fraude
- [ ] **Entonces** debe analizar velocidad de transacciones
- [ ] **Y** debe verificar geolocalización inusual
- [ ] **Y** debe evaluar montos atípicos
- [ ] **Y** debe verificar patrones de comportamiento
- [ ] **Y** debe bloquear transacciones sospechosas
- [ ] **Y** debe notificar al equipo de riesgos

**Reglas de Detección:**
- [ ] Más de 5 transacciones en 1 minuto desde misma IP
- [ ] Transacciones desde países de alto riesgo
- [ ] Montos 10x superiores al promedio del comercio
- [ ] Múltiples tarjetas diferentes desde mismo dispositivo
- [ ] Patrones de datos personales inconsistentes

**Estimación:** 21 Story Points
**Prioridad:** Crítica
**Dependencias:** US-001, Integración con Sistema de Riesgos

---

## 3. PLANTILLA DE USER STORY

### Información Básica:
```
ID: US-XXX
Título: [Título descriptivo]
Epic: [Epic al que pertenece]
```

### Historia:
```
Como [tipo de usuario]
Quiero [funcionalidad/objetivo]
Para [beneficio/valor de negocio]
```

### Criterios de Aceptación:
```
Escenario: [Nombre del escenario]
Dado que [precondición]
Y [precondición adicional]
Cuando [acción del usuario]
Entonces [resultado esperado]
Y [resultado adicional]
```

### Información Técnica:
```
Estimación: [Story Points]
Prioridad: [Crítica/Alta/Media/Baja]
Dependencias: [IDs de historias relacionadas]
Componentes: [Sistemas/módulos afectados]
```

### Definición de Terminado:
```
- [ ] Código desarrollado y revisado
- [ ] Pruebas unitarias (cobertura > 80%)
- [ ] Pruebas de integración
- [ ] Pruebas de seguridad
- [ ] Documentación actualizada
- [ ] Validación con usuario final
```

### Notas Técnicas:
```
- APIs involucradas: [Lista]
- Integraciones: [Sistemas externos]
- Consideraciones de seguridad: [Aspectos críticos]
- Consideraciones de rendimiento: [Métricas esperadas]
```

---

## 4. CHECKLIST DE CALIDAD PARA USER STORIES

### Criterios INVEST:
- [ ] **Independent:** La historia es independiente de otras
- [ ] **Negotiable:** Los detalles pueden ser discutidos
- [ ] **Valuable:** Aporta valor al negocio
- [ ] **Estimable:** Se puede estimar el esfuerzo
- [ ] **Small:** Es lo suficientemente pequeña para un sprint
- [ ] **Testable:** Se puede probar y verificar

### Completitud:
- [ ] Tiene criterios de aceptación claros
- [ ] Incluye escenarios de excepción
- [ ] Define reglas de negocio relevantes
- [ ] Especifica requerimientos no funcionales
- [ ] Identifica dependencias técnicas
- [ ] Incluye consideraciones de seguridad

### Trazabilidad:
- [ ] Se relaciona con requisitos funcionales
- [ ] Mapea a casos de uso específicos
- [ ] Considera aspectos regulatorios
- [ ] Alinea con objetivos de negocio
- [ ] Incluye métricas de éxito

---

## 5. EJEMPLO DE BACKLOG PRIORIZADO

### Sprint 1 (MVP):
1. **US-004:** Registrar Nuevo Comercio (13 pts)
2. **US-001:** Procesar Pago con Tarjeta (13 pts)
3. **US-006:** Detectar Transacciones Fraudulentas (21 pts)

### Sprint 2:
1. **US-003:** Reversar Pago Procesado (8 pts)
2. **US-005:** Consultar Mis Transacciones (5 pts)
3. **US-002:** Procesar Pago con Transferencia (8 pts)

### Backlog Futuro:
- Integración con billeteras digitales
- Sistema de reportes avanzados
- API para desarrolladores
- App móvil para comercios

---

**HERRAMIENTAS RECOMENDADAS:**
- **Jira:** Para gestión de backlog
- **Confluence:** Para documentación
- **SpecFlow:** Para pruebas BDD
- **Postman:** Para testing de APIs

**PRÓXIMO EJERCICIO:** `03-NonFunctional-Requirements.md` para especificar requisitos no funcionales críticos.