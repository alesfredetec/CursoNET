# EJERCICIO 3: ¿Qué tan bueno debe ser nuestro sistema?

## Escenario: Sistema de Pagos para Negocios

**Para estudiantes Junior:**
Además de las funciones, necesitamos definir qué tan bien debe funcionar nuestro sistema.

---

## ¿Qué significa "Requisitos de Calidad"?

### Ejemplo fácil:
Cuando compras un carro, no solo preguntas "¿Tiene ruedas?" También preguntas:
- ¿Es seguro?
- ¿Es rápido?
- ¿Gasta poca gasolina?
- ¿Es fácil de manejar?

**Lo mismo pasa con nuestro sistema:**
- ¿Es seguro para los pagos?
- ¿Es rápido para procesar?
- ¿Funciona siempre?
- ¿Es fácil de usar?

---

## TIPOS DE REQUISITOS DE CALIDAD

### 1. VELOCIDAD ⚡
**Pregunta:** ¿Qué tan rápido debe ser?

**Ejemplos:**
- El pago debe completarse en menos de 5 segundos
- La página debe cargar en menos de 3 segundos
- Juan debe poder ver sus ventas en menos de 2 segundos

**¿Por qué importa?**
Si es lento, María se aburre y no compra. Juan se molesta y se cambia a otro sistema.

### 2. SEGURIDAD 🔒
**Pregunta:** ¿Qué tan seguro debe ser?

**Ejemplos:**
- Los números de tarjeta deben estar encriptados
- Solo Juan puede ver las ventas de su tienda
- Pedro debe usar contraseña para entrar al sistema

**¿Por qué importa?**
Si no es seguro, los criminales pueden robar dinero y datos.

### 3. DISPONIBILIDAD 🌐
**Pregunta:** ¿Cuánto tiempo debe estar funcionando?

**Ejemplos:**
- El sistema debe funcionar 24 horas al día
- Máximo 1 hora de fallas por mes
- Si se cae, debe recuperarse en 5 minutos

**¿Por qué importa?**
Si se cae cuando María quiere pagar, Juan pierde ventas.

### 4. FACILIDAD DE USO 👥
**Pregunta:** ¿Qué tan fácil debe ser de usar?

**Ejemplos:**
- Un nuevo usuario debe poder registrarse en 5 minutos
- Los botones deben ser grandes y claros
- Los mensajes de error deben ser fáciles de entender

**¿Por qué importa?**
Si es difícil, la gente no lo usa.

---

## EJERCICIO PRÁCTICO

### Situación:
*"Juan se queja de que el sistema es muy lento. María dice que no entiende cómo pagar. Pedro dice que se cae mucho el sistema."*

**Tu trabajo:**
Para cada problema, escribe qué requisito de calidad falta:

1. **"El sistema es muy lento"**
   - Tipo de requisito: _______________
   - Solución: _______________

2. **"No entiendo cómo pagar"**
   - Tipo de requisito: _______________
   - Solución: _______________

3. **"Se cae mucho el sistema"**
   - Tipo de requisito: _______________
   - Solución: _______________

---

## PLANTILLA SIMPLE

### Para cada requisito de calidad:

**1. ¿Qué aspecto?** (Velocidad, Seguridad, Disponibilidad, Facilidad)

**2. ¿Qué medimos?** (Segundos, Porcentaje, Número de errores)

**3. ¿Cuál es el objetivo?** (Menos de 3 segundos, 99% del tiempo, etc.)

**4. ¿Cómo lo probamos?** (Cronómetro, Herramientas, Pruebas con usuarios)

**5. ¿Qué pasa si no se cumple?** (Usuarios se van, pérdida de dinero, etc.)

---

## EJEMPLOS COMPLETADOS

### Ejemplo 1: Velocidad de Pago

**¿Qué aspecto?** Velocidad

**¿Qué medimos?** Tiempo desde que María hace clic "Pagar" hasta que ve "Pago exitoso"

**¿Cuál es el objetivo?** Menos de 3 segundos

**¿Cómo lo probamos?** Con cronómetro, simulando 100 pagos

**¿Qué pasa si no se cumple?** María se aburre y no compra

### Ejemplo 2: Seguridad de Datos

**¿Qué aspecto?** Seguridad

**¿Qué medimos?** Que los números de tarjeta estén encriptados

**¿Cuál es el objetivo?** 100% de los datos protegidos

**¿Cómo lo probamos?** Revisando que no se vean números reales en la base de datos

**¿Qué pasa si no se cumple?** Los criminales pueden robar datos

### Ejemplo 3: Disponibilidad del Sistema

**¿Qué aspecto?** Disponibilidad

**¿Qué medimos?** Tiempo que el sistema está funcionando

**¿Cuál es el objetivo?** 99% del tiempo (7 horas de fallas al mes máximo)

**¿Cómo lo probamos?** Monitoreando 24/7 si responde

**¿Qué pasa si no se cumple?** Juan pierde ventas, clientes se van

---

## TU TURNO - EJERCICIOS

### Ejercicio 1: Fácil de Usar
Juan es nuevo con computadoras. Quiere registrar su tienda pero no sabe cómo.

**Escribe el requisito:**
- Aspecto: _______________
- Medimos: _______________
- Objetivo: _______________
- Probamos: _______________
- Si no se cumple: _______________

### Ejercicio 2: Muchos Usuarios
En Black Friday, 1000 personas van a pagar al mismo tiempo.

**Escribe el requisito:**
- Aspecto: _______________
- Medimos: _______________
- Objetivo: _______________
- Probamos: _______________
- Si no se cumple: _______________

### Ejercicio 3: Datos Correctos
Juan quiere estar seguro de que el dinero que ve en el sistema es correcto.

**Escribe el requisito:**
- Aspecto: _______________
- Medimos: _______________
- Objetivo: _______________
- Probamos: _______________
- Si no se cumple: _______________

---

## LISTA DE VERIFICACIÓN

### Antes de entregar el sistema, pregunta:

**Velocidad:**
- [ ] ¿Los pagos son rápidos? (menos de 5 segundos)
- [ ] ¿Las páginas cargan rápido? (menos de 3 segundos)
- [ ] ¿Los reportes se generan rápido? (menos de 10 segundos)

**Seguridad:**
- [ ] ¿Los datos están protegidos?
- [ ] ¿Solo usuarios autorizados pueden entrar?
- [ ] ¿Hay registros de quién hizo qué?

**Disponibilidad:**
- [ ] ¿Funciona 24/7?
- [ ] ¿Se recupera rápido si se cae?
- [ ] ¿Hay plan B si algo falla?

**Facilidad:**
- [ ] ¿Es fácil registrarse?
- [ ] ¿Los botones son claros?
- [ ] ¿Los mensajes se entienden?

---

## CONSEJOS PARA JUNIORS

### 1. Piensa en el usuario final
¿Qué necesita Juan para estar contento? ¿Qué molesta a María?

### 2. Sé específico
"Debe ser rápido" ❌
"Debe responder en menos de 3 segundos" ✅

### 3. Piensa en situaciones extremas
¿Qué pasa en Black Friday? ¿Qué pasa si se cae internet?

### 4. Define cómo vas a medir
Si no puedes medirlo, no puedes mejorarlo.

### 5. Prioriza lo más importante
No todo puede ser "súper rápido" y "súper seguro". ¿Qué es más importante?

---

## HERRAMIENTAS SIMPLES

### Para medir velocidad:
- **Cronómetro** en el celular
- **Herramientas del navegador** (F12)
- **Preguntar a los usuarios** "¿Te parece rápido?"

### Para medir seguridad:
- **Revisar** que no se vean contraseñas en pantalla
- **Preguntar** "¿Te sientes seguro usando esto?"
- **Verificar** que solo usuarios autorizados pueden entrar

### Para medir disponibilidad:
- **Ping** para ver si responde
- **Revisar logs** de errores
- **Preguntar** "¿Cuándo no pudiste entrar?"

### Para medir facilidad:
- **Observar** a usuarios nuevos
- **Preguntar** "¿Qué parte fue difícil?"
- **Contar** cuántos clics necesita cada tarea

---

**TIEMPO:** 45 minutos
**DIFICULTAD:** Básico
**OBJETIVO:** Entender qué hace un sistema "bueno"

**SIGUIENTE PASO:** `04-Trazabilidad-Simple.md` para aprender a organizar todo lo que hicimos.