# EJERCICIO 2: Funciones del Sistema - RESPUESTAS

## Escenario: Sistema de Pagos para Negocios

**Para estudiantes Junior:**
Aquí están las respuestas y explicaciones del ejercicio.

---

## RESPUESTAS

### Pregunta 1: ¿Quién usa el sistema?

**Usuarios identificados:**
- ✅ **Dueño de tienda:** Juan (registra su negocio)
- ✅ **Cliente:** María (paga sus compras)
- ✅ **Administrador:** Pedro (supervisa el sistema)

**¿Por qué es importante identificar usuarios?**
Porque cada usuario necesita funciones diferentes. Juan necesita ver sus ventas, María necesita pagar fácil, Pedro necesita controlar todo.

---

### Pregunta 2: ¿Qué puede hacer cada persona?

**Dueño de tienda (Juan):**
- Acción 1: **Registrar su negocio** en el sistema
- Acción 2: **Ver sus ventas** del día/semana/mes
- Acción 3: **Devolver dinero** a clientes insatisfechos

**Cliente que compra (María):**
- Acción 1: **Pagar con tarjeta** sus compras
- Acción 2: **Recibir confirmación** de pago exitoso

**Administrador (Pedro):**
- Acción 1: **Aprobar** nuevos negocios
- Acción 2: **Revisar** transacciones sospechosas

---

### Pregunta 3: ¿Qué pasos sigue cada acción?

**Ejemplo: "Pagar con tarjeta"**
1. **María selecciona** "Pagar con tarjeta"
2. **María escribe** número de tarjeta, fecha, CVV
3. **El sistema valida** que los datos estén correctos
4. **El sistema pregunta al banco** si tiene dinero
5. **El banco confirma** o rechaza el pago
6. **María recibe** confirmación por email
7. **Juan ve** la venta en su dashboard

**¿Por qué escribimos los pasos?**
Para no olvidar nada importante y para que el programador sepa exactamente qué hacer.

---

### Pregunta 4: ¿Qué puede salir mal?

**Problemas identificados:**
1. **La tarjeta no tiene dinero** → El banco rechaza el pago
2. **Se cayó internet** → No podemos comunicarnos con el banco
3. **Se escribió mal el número** → Los datos no son válidos

**¿Cómo los solucionamos?**
1. **Sin dinero:** Mostramos mensaje claro a María
2. **Sin internet:** Guardamos el pago para reintentarlo después
3. **Datos mal:** Validamos antes de enviar al banco

---

## CONCEPTOS IMPORTANTES

### ¿Qué es un "Caso de Uso"?
Es una descripción de cómo alguien usa el sistema para lograr algo útil.

**Ejemplo simple:**
- **Quien:** Juan (dueño de tienda)
- **Qué:** Quiere ver sus ventas
- **Para qué:** Saber cuánto dinero ganó

### ¿Qué es un "Usuario"?
Es una persona que interactúa con nuestro sistema.

**Tipos de usuarios:**
- **Primario:** Usa el sistema directamente (Juan, María)
- **Secundario:** Ayuda que funcione (Pedro, el banco)

### ¿Qué es el "Flujo Principal"?
Son los pasos normales cuando todo sale bien.

### ¿Qué son las "Excepciones"?
Son los pasos cuando algo sale mal.

---

## EJEMPLOS PRÁCTICOS

### Caso de Uso: Registrar Negocio

**Usuario:** Juan (dueño de tienda)
**Objetivo:** Poder recibir pagos de clientes

**Flujo Principal:**
1. Juan entra a nuestra página web
2. Juan hace clic en "Registrar mi negocio"
3. Juan completa formulario con datos de su tienda
4. Juan sube fotos de sus documentos
5. Pedro (admin) revisa los documentos
6. Pedro aprueba el registro
7. Juan recibe email con acceso al sistema

**¿Qué puede salir mal?**
- **Documentos borrosos:** Pedro rechaza y pide fotos nuevas
- **Datos incompletos:** El sistema no deja enviar el formulario
- **Negocio ya registrado:** El sistema muestra mensaje de error

---

### Caso de Uso: Ver Ventas

**Usuario:** Juan (dueño de tienda)
**Objetivo:** Saber cuánto dinero recibió

**Flujo Principal:**
1. Juan entra a su dashboard
2. Juan selecciona "Ver mis ventas"
3. Juan elige fecha (hoy, esta semana, este mes)
4. El sistema muestra lista de ventas
5. Juan puede ver detalles de cada venta
6. Juan puede descargar reporte en Excel

**¿Qué puede salir mal?**
- **Sin ventas:** El sistema muestra "No hay ventas en este período"
- **Muchas ventas:** El sistema muestra páginas (1, 2, 3...)
- **Error en servidor:** El sistema muestra "Intente más tarde"

---

## PLANTILLA COMPLETA

### Nombre: Pagar con Tarjeta

**¿Quién la usa?** María (cliente)

**¿Para qué la usa?** Para pagar sus compras online

**Pasos que sigue:**
1. María selecciona productos y va a pagar
2. María elige "Pagar con tarjeta"
3. María escribe datos de su tarjeta
4. El sistema valida los datos
5. El sistema pregunta al banco
6. El banco aprueba o rechaza
7. María recibe confirmación

**¿Qué puede salir mal?** 
- Tarjeta sin fondos
- Datos incorrectos
- Problemas de conexión

**¿Cómo lo solucionamos?**
- Mensajes claros de error
- Validación antes de enviar
- Reintentos automáticos

---

## CONSEJOS PARA JUNIORS

### 1. Siempre pregunta "¿Para qué?"
No solo identifiques QUÉ hace el usuario, sino PARA QUÉ lo hace.

### 2. Piensa en el usuario final
¿Es fácil para Juan ver sus ventas? ¿Entiende María cómo pagar?

### 3. No olvides los casos de error
Los usuarios cometen errores. El sistema debe ayudarles.

### 4. Usa palabras simples
Evita términos técnicos. Escribe como si le explicaras a tu abuela.

### 5. Valida con ejemplos reales
"Juan quiere ver sus ventas de ayer" es mejor que "el usuario consulta transacciones".

---

## PRÓXIMOS PASOS

### En el trabajo real:
1. **Habla con usuarios reales** (Juan, María)
2. **Observa cómo trabajan** actualmente
3. **Pregunta por sus problemas** actuales
4. **Propón soluciones** simples

### Para seguir aprendiendo:
1. **User Stories:** Convertir casos de uso en historias
2. **Wireframes:** Dibujar pantallas del sistema
3. **Prototipos:** Crear versiones de prueba

**SIGUIENTE EJERCICIO:** `03-NFR-Simple.md` para aprender sobre requisitos de calidad.