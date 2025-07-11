# EJERCICIO 1: ¿Qué hace funcional y qué hace no funcional?

## Escenario: Sistema de Pagos para Negocios

**Para estudiantes Junior:**
Necesitamos separar los requisitos en dos grupos: los que dicen QUÉ hace el sistema y los que dicen QUÉ TAN BIEN lo hace.

---

## ¿QUÉ SON LOS REQUISITOS?

### Ejemplo fácil:
Cuando pides una pizza:
- **Funcional:** "Quiero una pizza de pepperoni" (QUÉ quieres)
- **No funcional:** "Que llegue caliente en 30 minutos" (QUÉ TAN BIEN la quieres)

**En nuestro sistema:**
- **Funcional:** "Los usuarios pueden pagar con tarjeta" (QUÉ hace)
- **No funcional:** "El pago debe ser rápido y seguro" (QUÉ TAN BIEN lo hace)

---

## LISTA DE REQUISITOS PARA CLASIFICAR

### Instrucciones:
Lee cada requisito y marca si es **FUNCIONAL** o **NO FUNCIONAL**

| # | Requisito | Funcional | No Funcional |
|---|-----------|-----------|-------------|
| 1 | Los usuarios pueden registrar su negocio | ⬜ | ⬜ |
| 2 | La página debe cargar en menos de 3 segundos | ⬜ | ⬜ |
| 3 | Los usuarios pueden pagar con tarjeta de crédito | ⬜ | ⬜ |
| 4 | El sistema debe estar disponible 24 horas al día | ⬜ | ⬜ |
| 5 | Los usuarios pueden ver sus ventas del día | ⬜ | ⬜ |
| 6 | Los datos deben estar protegidos con cifrado | ⬜ | ⬜ |
| 7 | Los usuarios pueden devolver dinero a clientes | ⬜ | ⬜ |
| 8 | El sistema debe soportar 1000 usuarios simultáneos | ⬜ | ⬜ |
| 9 | Los usuarios pueden descargar reportes en Excel | ⬜ | ⬜ |
| 10 | La interfaz debe ser fácil de usar | ⬜ | ⬜ |
| 11 | Los usuarios pueden buscar transacciones por fecha | ⬜ | ⬜ |
| 12 | El sistema debe cumplir con las leyes de privacidad | ⬜ | ⬜ |
| 13 | Los usuarios pueden recibir notificaciones por email | ⬜ | ⬜ |
| 14 | El sistema debe funcionar en Chrome, Firefox y Safari | ⬜ | ⬜ |
| 15 | Los usuarios pueden cancelar transacciones | ⬜ | ⬜ |

---

## PREGUNTAS PARA AYUDARTE

### Para identificar FUNCIONAL:
- ¿Describe algo que el usuario PUEDE HACER?
- ¿Responde a "QUÉ hace el sistema"?
- ¿Es una acción o característica?

### Para identificar NO FUNCIONAL:
- ¿Describe QUÉ TAN BIEN debe funcionar?
- ¿Habla de velocidad, seguridad, usabilidad?
- ¿Es una restricción o calidad?

---

## EJERCICIO PRÁCTICO

### Situación 1:
*"Juan quiere que sus clientes puedan pagar con tarjeta Visa y MasterCard"*

**Pregunta:** ¿Es funcional o no funcional?
**Respuesta:** _______________
**¿Por qué?** _______________

### Situación 2:
*"Los pagos deben procesarse en menos de 5 segundos"*

**Pregunta:** ¿Es funcional o no funcional?
**Respuesta:** _______________
**¿Por qué?** _______________

### Situación 3:
*"Juan quiere ver un reporte de sus ventas mensuales"*

**Pregunta:** ¿Es funcional o no funcional?
**Respuesta:** _______________
**¿Por qué?** _______________

---

## CATEGORÍAS DE REQUISITOS NO FUNCIONALES

### Cuando encuentres un requisito NO FUNCIONAL, clasifícalo:

**VELOCIDAD/RENDIMIENTO:**
- Tiempo de respuesta
- Velocidad de carga
- Tiempo de procesamiento

**SEGURIDAD:**
- Protección de datos
- Autenticación
- Autorización

**DISPONIBILIDAD:**
- Tiempo de funcionamiento
- Recuperación ante fallas
- Horarios de operación

**USABILIDAD:**
- Facilidad de uso
- Diseño intuitivo
- Accesibilidad

**COMPATIBILIDAD:**
- Navegadores soportados
- Dispositivos compatibles
- Versiones de software

**CUMPLIMIENTO:**
- Regulaciones legales
- Estándares de industria
- Políticas empresariales

---

## TU TURNO - CLASIFICACIÓN AVANZADA

### Para cada requisito NO FUNCIONAL, identifica la categoría:

| Requisito | Categoría |
|-----------|-----------|
| "La página debe cargar en menos de 3 segundos" | _________ |
| "Los datos deben estar protegidos con cifrado" | _________ |
| "El sistema debe estar disponible 24 horas al día" | _________ |
| "La interfaz debe ser fácil de usar" | _________ |
| "Debe funcionar en Chrome, Firefox y Safari" | _________ |
| "Debe cumplir con las leyes de privacidad" | _________ |

---

## ERRORES COMUNES

### ❌ Error 1: Confundir acciones con calidad
**Incorrecto:** "El sistema debe ser seguro" (muy vago)
**Correcto:** "Los números de tarjeta deben estar cifrados"

### ❌ Error 2: Mezclar funcional con no funcional
**Incorrecto:** "Los usuarios pueden pagar rápidamente"
**Correcto:** 
- **Funcional:** "Los usuarios pueden pagar"
- **No funcional:** "El pago debe completarse en menos de 5 segundos"

### ❌ Error 3: Ser demasiado técnico
**Incorrecto:** "Debe usar AES-256 encryption"
**Correcto:** "Los datos sensibles deben estar protegidos"

---

## CONSEJOS 

### 1. Usa la pregunta mágica
"¿Esto me dice QUÉ hace o QUÉ TAN BIEN lo hace?"

### 2. Busca palabras clave
- **Funcional:** "puede", "debe poder", "permite"
- **No funcional:** "rápido", "seguro", "disponible", "fácil"

### 3. Piensa en el usuario
- **Funcional:** "Juan quiere hacer X"
- **No funcional:** "Juan quiere que X sea rápido/seguro/fácil"

### 4. Sé específico
"Rápido" no dice nada. "Menos de 3 segundos" es claro.

### 5. Pregunta "¿Por qué?"
Si no entiendes por qué es importante, pregunta al usuario.

---

## PLANTILLA PARA NUEVOS REQUISITOS

### Cuando te den un requisito nuevo:

**PASO 1:** Escribe el requisito tal como te lo dijeron
**PASO 2:** Pregunta: "¿Esto describe QUÉ hace o QUÉ TAN BIEN lo hace?"
**PASO 3:** Clasifica como Funcional o No Funcional
**PASO 4:** Si es No Funcional, identifica la categoría
**PASO 5:** Verifica que sea específico y medible

**Ejemplo:**
- **Paso 1:** "El sistema debe ser rápido"
- **Paso 2:** Habla de QUÉ TAN BIEN → No Funcional
- **Paso 3:** No Funcional ✓
- **Paso 4:** Categoría: Velocidad/Rendimiento
- **Paso 5:** Cambiar a: "Las páginas deben cargar en menos de 3 segundos"

---

## VALIDACIÓN CON USUARIOS

### Preguntas para confirmar requisitos:

**Para FUNCIONALES:**
- "¿Quieres que el sistema haga X?"
- "¿Cómo usarías esta función?"
- "¿Qué esperas que pase cuando haces X?"

**Para NO FUNCIONALES:**
- "¿Qué tan rápido necesitas que sea?"
- "¿Qué tan seguro debe ser?"
- "¿Cuándo necesitas que esté disponible?"

---

## HERRAMIENTAS ÚTILES

### Para organizar requisitos:
- **Excel o Google Sheets:** Para hacer listas
- **Sticky notes:** Para agrupar requisitos similares
- **Pizarra:** Para dibujar conexiones

### Para validar con usuarios:
- **Entrevistas:** Hablar directamente con Juan
- **Encuestas:** Preguntar a muchos usuarios
- **Observación:** Ver cómo trabajan actualmente

--- 

**TIEMPO:** 30 minutos
**DIFICULTAD:** Básico
**OBJETIVO:** Separar QUÉ hace el sistema de QUÉ TAN BIEN lo hace

**SIGUIENTE PASO:** Ver las respuestas en el archivo `01-Requirements-Simple-After.md`