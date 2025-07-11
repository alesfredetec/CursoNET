# EJERCICIO 2: Casos de Uso Formales Estructurados

## Escenario: Sistema de Procesamiento de Pagos para FinTech

**RESULTADO DEL ANÁLISIS:**
Este documento presenta los casos de uso formales derivados de la descripción informal del sistema de pagos.

---

## 1. IDENTIFICACIÓN DE ACTORES

### Actores Primarios:
- **Comercio:** Negocio que vende productos/servicios
- **Cliente:** Persona que realiza compras
- **Administrador FinTech:** Empleado que supervisa operaciones

### Actores Secundarios:
- **Banco Emisor:** Banco que emitió la tarjeta del cliente
- **Procesador de Pagos:** Entidad que procesa transacciones
- **Sistema de Detección de Fraude:** Sistema automatizado de seguridad
- **Sistema de Notificaciones:** Servicio de emails/webhooks

---

## 2. CATÁLOGO DE CASOS DE USO

### Casos de Uso Principales:

#### CU-001: Procesar Pago con Tarjeta
- **Actor Principal:** Cliente
- **Actores Secundarios:** Comercio, Banco Emisor, Procesador de Pagos
- **Nivel:** Nivel de Usuario
- **Complejidad:** Alta

#### CU-002: Registrar Comercio
- **Actor Principal:** Comercio
- **Actores Secundarios:** Administrador FinTech
- **Nivel:** Nivel de Usuario
- **Complejidad:** Media

#### CU-003: Consultar Transacciones
- **Actor Principal:** Comercio
- **Actores Secundarios:** Ninguno
- **Nivel:** Nivel de Usuario
- **Complejidad:** Baja

#### CU-004: Reversar Pago
- **Actor Principal:** Comercio
- **Actores Secundarios:** Banco Emisor, Cliente
- **Nivel:** Nivel de Usuario
- **Complejidad:** Alta

#### CU-005: Monitorear Fraudes
- **Actor Principal:** Administrador FinTech
- **Actores Secundarios:** Sistema de Detección de Fraude
- **Nivel:** Nivel de Usuario
- **Complejidad:** Media

---

## 3. ESPECIFICACIÓN DETALLADA DE CASOS DE USO

### CU-001: Procesar Pago con Tarjeta

#### Información General:
- **Código:** CU-001
- **Nombre:** Procesar Pago con Tarjeta
- **Descripción:** Permite al cliente realizar un pago usando tarjeta de crédito o débito
- **Actor Principal:** Cliente
- **Actores Secundarios:** Comercio, Banco Emisor, Procesador de Pagos
- **Tipo:** Primario
- **Complejidad:** Alta

#### Precondiciones:
- El comercio está registrado y activo en el sistema
- El cliente tiene una tarjeta válida
- El sistema de procesamiento está disponible
- El comercio ha iniciado una transacción de venta

#### Postcondiciones:
- **Éxito:** El pago se procesa exitosamente, se descuenta dinero de la cuenta del cliente, se notifica al comercio
- **Fracaso:** Se rechaza el pago, se notifica el motivo al cliente y comercio

#### Flujo Principal:
1. El comercio inicia una transacción de pago enviando datos al sistema
2. El sistema valida los datos de la transacción (monto, moneda, comercio)
3. El cliente ingresa datos de su tarjeta (número, fecha vencimiento, CVV)
4. El sistema valida el formato de los datos de la tarjeta
5. El sistema cifra los datos sensibles de la tarjeta
6. El sistema consulta el procesador de pagos correspondiente
7. El procesador envía la autorización al banco emisor
8. El banco emisor valida fondos y límites de crédito
9. El banco emisor autoriza la transacción
10. El procesador confirma la autorización al sistema
11. El sistema registra la transacción como exitosa
12. El sistema notifica al comercio vía webhook
13. El sistema envía comprobante al cliente vía email

#### Flujos Alternativos:
- **FA-001:** Datos de tarjeta inválidos
  - En el paso 4, si los datos no son válidos:
    - 4a. El sistema rechaza la transacción
    - 4b. El sistema notifica el error específico
    - 4c. El caso de uso termina

- **FA-002:** Fondos insuficientes
  - En el paso 8, si no hay fondos suficientes:
    - 8a. El banco rechaza la transacción
    - 8b. El sistema registra el rechazo
    - 8c. El sistema notifica al comercio y cliente
    - 8d. El caso de uso termina

- **FA-003:** Transacción sospechosa de fraude
  - En el paso 6, si se detecta fraude:
    - 6a. El sistema bloquea la transacción
    - 6b. El sistema notifica al administrador
    - 6c. El sistema requiere verificación adicional
    - 6d. El caso de uso se suspende hasta verificación

#### Flujos de Excepción:
- **FE-001:** Falla de comunicación con banco
  - En el paso 7, si no hay conexión:
    - 7a. El sistema reintenta 3 veces
    - 7b. Si persiste la falla, marca como "pendiente"
    - 7c. El sistema programa reintento automático
    - 7d. El sistema notifica error temporal al comercio

#### Reglas de Negocio:
- **RN-001:** El monto máximo por transacción es $10,000 USD
- **RN-002:** Se permite máximo 3 intentos de pago fallidos por hora
- **RN-003:** Las transacciones se procesan en tiempo real (< 3 segundos)
- **RN-004:** Los datos de tarjeta se cifran con AES-256
- **RN-005:** Se aplica 3D Secure para montos > $100 USD

#### Requerimientos Especiales:
- **Rendimiento:** Tiempo de respuesta < 3 segundos
- **Seguridad:** Cumplimiento PCI DSS Level 1
- **Disponibilidad:** 99.9% uptime
- **Auditoría:** Todas las transacciones se registran

---

### CU-002: Registrar Comercio

#### Información General:
- **Código:** CU-002
- **Nombre:** Registrar Comercio
- **Descripción:** Permite a un nuevo comercio registrarse en la plataforma
- **Actor Principal:** Comercio
- **Actores Secundarios:** Administrador FinTech
- **Tipo:** Primario
- **Complejidad:** Media

#### Precondiciones:
- El comercio no está registrado previamente
- El comercio tiene documentación legal válida
- El administrador tiene permisos de aprobación

#### Postcondiciones:
- **Éxito:** El comercio queda registrado y puede procesar pagos
- **Fracaso:** El registro se rechaza con motivo específico

#### Flujo Principal:
1. El comercio accede al formulario de registro
2. El comercio completa información básica (nombre, email, teléfono)
3. El comercio sube documentación legal (RUT, escritura, poderes)
4. El comercio configura datos bancarios para liquidación
5. El comercio acepta términos y condiciones
6. El sistema valida formato de datos ingresados
7. El sistema ejecuta verificaciones KYC automáticas
8. El sistema genera caso de revisión manual
9. El administrador revisa documentación y verifica datos
10. El administrador aprueba el registro
11. El sistema genera credenciales API para el comercio
12. El sistema envía credenciales y documentación de integración
13. El comercio queda activo para procesar pagos

#### Reglas de Negocio:
- **RN-006:** Todo comercio debe pasar verificación KYC
- **RN-007:** Se requiere cuenta bancaria empresarial
- **RN-008:** La documentación debe estar vigente (< 90 días)
- **RN-009:** El proceso de aprobación toma máximo 72 horas

---

### CU-003: Consultar Transacciones

#### Información General:
- **Código:** CU-003
- **Nombre:** Consultar Transacciones
- **Descripción:** Permite al comercio consultar el historial de sus transacciones
- **Actor Principal:** Comercio
- **Actores Secundarios:** Ninguno
- **Tipo:** Primario
- **Complejidad:** Baja

#### Precondiciones:
- El comercio está autenticado en el sistema
- El comercio tiene transacciones registradas

#### Postcondiciones:
- **Éxito:** Se muestra el listado de transacciones solicitadas
- **Fracaso:** Se muestra mensaje de error específico

#### Flujo Principal:
1. El comercio accede al dashboard de transacciones
2. El comercio selecciona filtros de búsqueda (fecha, estado, monto)
3. El sistema valida permisos de acceso
4. El sistema consulta base de datos de transacciones
5. El sistema aplica filtros seleccionados
6. El sistema presenta resultados paginados
7. El comercio puede exportar datos a Excel/CSV
8. El sistema genera reporte con los datos solicitados

#### Reglas de Negocio:
- **RN-010:** Solo se muestran transacciones propias del comercio
- **RN-011:** Los datos se conservan por 7 años
- **RN-012:** Se permite exportar máximo 10,000 registros por consulta

---

## 4. MATRIZ DE TRAZABILIDAD

| Caso de Uso | Requisito Funcional | Requisito No Funcional | Complejidad | Prioridad |
|-------------|-------------------|----------------------|-------------|-----------|
| CU-001 | RF-001, RF-002, RF-003 | RNF-001, RNF-002, RNF-003 | Alta | Crítica |
| CU-002 | RF-004, RF-005 | RNF-004, RNF-005 | Media | Alta |
| CU-003 | RF-006, RF-007 | RNF-006 | Baja | Media |
| CU-004 | RF-008, RF-009 | RNF-001, RNF-002 | Alta | Alta |
| CU-005 | RF-010, RF-011 | RNF-007, RNF-008 | Media | Crítica |

---

## 5. ANÁLISIS DE MEJORAS

### Información Faltante en Descripción Original:
1. **Reglas de negocio específicas:** Montos máximos, límites de intentos
2. **Flujos de excepción:** Manejo de errores de comunicación
3. **Requerimientos de seguridad:** Cifrado, autenticación, auditoría
4. **Criterios de aceptación:** Métricas de rendimiento y disponibilidad
5. **Integraciones técnicas:** APIs específicas, formatos de datos

### Stakeholders Adicionales Identificados:
- **Compliance Officer:** Para verificar cumplimiento regulatorio
- **Risk Manager:** Para configurar reglas de detección de fraude
- **Technical Support:** Para soporte de integración de comercios
- **Legal Department:** Para términos y condiciones

### Dependencias Técnicas Críticas:
- **Sistema de Cifrado:** Para proteger datos sensibles
- **Base de Datos Transaccional:** Para garantizar ACID
- **Sistema de Colas:** Para procesar webhooks asincrónicos
- **Sistema de Monitoreo:** Para detectar anomalías en tiempo real

---

**LECCIONES APRENDIDAS:**
1. Las descripciones informales omiten flujos críticos de excepción
2. Los stakeholders técnicos y legales son fundamentales
3. Los requisitos no funcionales son tan importantes como los funcionales
4. El contexto financiero requiere consideraciones especiales de seguridad
5. La trazabilidad entre casos de uso y requisitos es esencial

**PRÓXIMO EJERCICIO:** `02-UserStories-Template.md` para convertir casos de uso en historias de usuario ágiles.