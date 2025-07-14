# EJERCICIO 1: Clasificación de Requisitos - RESPUESTAS

## Escenario: Sistema de Pagos para Negocios

**Para estudiantes Junior:**
Aquí están las respuestas correctas con explicaciones.

---

## RESPUESTAS CORRECTAS


> **🟦 TABLA DE RESPUESTAS CORRECTAS**

| Nº  | Requisito                                          | Tipo                   | Explicación                                |
|:---:|:---------------------------------------- ----------|:---------------------:|:--------------------------------------------|
|  1  | Los usuarios pueden registrar su negocio           | 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |
|  2  | La página debe cargar en menos de 3 segundos       | 🔵 **NO FUNCIONAL**   | Describe QUÉ TAN BIEN (velocidad)           |
|  3  | Los usuarios pueden pagar con tarjeta de crédito   | 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |
|  4  | El sistema debe estar disponible 24 horas al día   | 🔵 **NO FUNCIONAL**   | Describe QUÉ TAN BIEN (disponibilidad)      |
|  5  | Los usuarios pueden ver sus ventas del día         | 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |
|  6  | Los datos deben estar protegidos con cifrado       | 🔵 **NO FUNCIONAL**   | Describe QUÉ TAN BIEN (seguridad)           |
|  7  | Los usuarios pueden devolver dinero a clientes     | 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |
|  8  | El sistema debe soportar 1000 usuarios simultáneos | 🔵 **NO FUNCIONAL**   | Describe QUÉ TAN BIEN (capacidad)           |
|  9  | Los usuarios pueden descargar reportes en Excel    | 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |
| 10  | La interfaz debe ser fácil de usar                 | 🔵 **NO FUNCIONAL**   | Describe QUÉ TAN BIEN (usabilidad)          |
| 11  | Los usuarios pueden buscar transacciones por fecha | 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |
| 12  | El sistema debe cumplir con las leyes de privacidad| 🔵 **NO FUNCIONAL**   | Describe QUÉ TAN BIEN (cumplimiento)        |
| 13  | Los usuarios pueden recibir notificaciones    email| 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |
| 14  | El sistema debe funcionar en Chrome, Firefox Safari| 🔵 **NO FUNCIONAL**   | Describe QUÉ TAN BIEN (compatibilidad)      |
| 15  | Los usuarios pueden cancelar transacciones         | 🟢 **FUNCIONAL**      | Describe QUÉ puede hacer el usuario         |

---

## RESPUESTAS A EJERCICIOS PRÁCTICOS

### Situación 1:
*"Juan quiere que sus clientes puedan pagar con tarjeta Visa y MasterCard"*

**Respuesta:** ✅ **FUNCIONAL**
**¿Por qué?** Describe QUÉ puede hacer el sistema (aceptar pagos con esas tarjetas)

### Situación 2:
*"Los pagos deben procesarse en menos de 5 segundos"*

**Respuesta:** ✅ **NO FUNCIONAL**
**¿Por qué?** Describe QUÉ TAN BIEN debe funcionar (velocidad)

### Situación 3:
*"Juan quiere ver un reporte de sus ventas mensuales"*

**Respuesta:** ✅ **FUNCIONAL**
**¿Por qué?** Describe QUÉ puede hacer Juan (ver reportes)

---

## CATEGORÍAS DE REQUISITOS NO FUNCIONALES

### Clasificación correcta:


> **🟦 CLASIFICACIÓN DE REQUISITOS NO FUNCIONALES**

| Requisito                                      | Categoría                     | Explicación                        |
|:-----------------------------------------------|:-----------------------------:|:-----------------------------------|
| "La página debe cargar en menos de 3 segundos" | 🟠 **VELOCIDAD/RENDIMIENTO**  | Habla de tiempo de respuesta      |
| "Los datos deben estar protegidos con cifrado" | 🔴 **SEGURIDAD**              | Habla de protección de información|
| "El sistema debe estar disponible 24 hs al día"| 🟣 **DISPONIBILIDAD**         | Habla de tiempo de funcionamiento |
| "La interfaz debe ser fácil de usar"           | 🟡 **USABILIDAD**             | Habla de facilidad de uso         |
| "Debe funcionar en Chrome, Firefox y Safari"   | 🟤 **COMPATIBILIDAD**         | Habla de navegadores soportados   |
| "Debe cumplir con las leyes de privacidad"     | 🟢 **CUMPLIMIENTO**           | Habla de regulaciones legales     |

---

## ANÁLISIS DE ERRORES COMUNES

### Error frecuente en #10: "La interfaz debe ser fácil de usar"
**¿Por qué es NO FUNCIONAL?**
- No dice QUÉ hace la interfaz
- Dice QUÉ TAN BIEN debe funcionar (fácil)
- Es una característica de calidad

**¿Cómo mejorarlo?**
- Vago ❌: "Debe ser fácil de usar"
- Específico ✅: "Un usuario nuevo debe poder registrarse en menos de 5 minutos"

### Error frecuente en #8: "1000 usuarios simultáneos"
**¿Por qué es NO FUNCIONAL?**
- No dice QUÉ hacen los usuarios
- Dice QUÉ TAN BIEN debe responder el sistema (capacidad)
- Es una restricción de rendimiento

---

## PATRONES PARA IDENTIFICAR TIPOS

### Palabras clave para FUNCIONAL:
- **"puede"**: "El usuario puede registrarse"
- **"debe poder"**: "El sistema debe poder procesar pagos"
- **"permite"**: "El sistema permite descargar reportes"
- **"incluye"**: "El sistema incluye notificaciones"

### Palabras clave para NO FUNCIONAL:
- **Tiempo**: "en menos de X segundos", "durante X horas"
- **Cantidad**: "hasta X usuarios", "máximo X transacciones"
- **Calidad**: "seguro", "fácil", "confiable", "rápido"
- **Restricción**: "debe cumplir", "compatible con", "disponible"

---

## EJERCICIOS ADICIONALES

### Ejercicio 1: Convierte estos requisitos vagos en específicos

**Vago:** "El sistema debe ser rápido"
**Específico:** _________________________________

**Vago:** "Debe ser seguro"
**Específico:** _________________________________

**Vago:** "Debe funcionar bien"
**Específico:** _________________________________

### Respuestas sugeridas:
- **Rápido:** "Las páginas deben cargar en menos de 3 segundos"
- **Seguro:** "Las contraseñas deben estar cifradas"
- **Funcionar bien:** "Debe estar disponible 99% del tiempo"

---

## CASOS DIFÍCILES DE CLASIFICAR

### Caso 1: "El sistema debe enviar emails automáticamente"
**¿Funcional o No Funcional?**
- **Funcional** ✅: Describe QUÉ hace (enviar emails)
- **"Automáticamente"** describe CÓMO, pero sigue siendo una función

### Caso 2: "Los reportes deben generarse en formato PDF"
**¿Funcional o No Funcional?**
- **Funcional** ✅: Describe QUÉ hace (generar reportes)
- **"En formato PDF"** especifica el resultado

### Caso 3: "El sistema debe ser intuitivo"
**¿Funcional o No Funcional?**
- **No Funcional** ✅: "Intuitivo" es una calidad, no una función
- **Mejora:** "Los usuarios deben poder completar tareas sin ayuda"

---

## VALIDACIÓN CON USUARIOS REALES

### Preguntas para confirmar FUNCIONALES:
- "¿Realmente necesitas que el sistema haga esto?"
- "¿Cuándo usarías esta función?"
- "¿Qué pasaría si no existiera?"

### Preguntas para confirmar NO FUNCIONALES:
- "¿Qué tan rápido necesitas que sea?"
- "¿Qué nivel de seguridad necesitas?"
- "¿Cuántos usuarios van a usar esto al mismo tiempo?"

---

## IMPACTO EN EL DESARROLLO

### Requisitos FUNCIONALES afectan:
- **¿Qué programar?** Las funciones y pantallas
- **¿Qué probar?** Que cada función haga lo correcto
- **¿Qué documentar?** Cómo usar cada función

### Requisitos NO FUNCIONALES afectan:
- **¿Cómo programar?** La arquitectura y tecnología
- **¿Qué probar?** Que funcione bajo condiciones extremas
- **¿Qué documentar?** Los límites y restricciones

---

## CONSEJOS PARA EL MUNDO REAL

### 1. Siempre pregunta "¿Para qué?"
Si no entiendes por qué necesitan algo, pregunta.

### 2. Convierte lo vago en específico
"Rápido" no sirve. "Menos de 3 segundos" sí.

### 3. Prioriza lo importante
No todo puede ser "súper rápido" y "súper seguro".

### 4. Valida con ejemplos
"¿Te refieres a que Juan pueda hacer X en menos de Y segundos?"

### 5. Documenta las decisiones
"Decidimos que 3 segundos es aceptable porque..."

---

## HERRAMIENTAS ÚTILES

### Para organizar:
- **Planillas Excel:** Columnas para tipo, prioridad, estado
- **Jira/Trello:** Para tickets y seguimiento
- **Confluence:** Para documentación

### Para validar:
- **Entrevistas:** Hablar con usuarios reales
- **Prototipos:** Mostrar ejemplos
- **Métricas:** Medir sistemas existentes

---

## PRÓXIMOS PASOS

### Para seguir mejorando:
1. **Practica con proyectos reales:** Cada requisito que veas, clasifícalo
2. **Habla con usuarios:** Entiende qué necesitan realmente
3. **Mide sistemas existentes:** ¿Qué tan rápido es "rápido"?
4. **Colabora con el equipo:** Diferentes perspectivas ayudan

### Habilidades relacionadas:
- **Escritura técnica:** Documentar requisitos claramente
- **Entrevistas:** Obtener información de usuarios
- **Análisis:** Identificar patrones y dependencias

---

**LECCIÓN APRENDIDA:**
La diferencia entre requisitos funcionales y no funcionales es fundamental para construir sistemas que no solo funcionen, sino que funcionen BIEN.

**SIGUIENTE EJERCICIO:** `02-UseCase-Simple-Before.md` para aprender a describir cómo los usuarios usan el sistema.