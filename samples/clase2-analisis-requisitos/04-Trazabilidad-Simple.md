# EJERCICIO 4: ¿Cómo organizamos todo lo que hicimos?

## Escenario: Sistema de Pagos para Negocios

**Para estudiantes Junior:**
Ahora que sabemos qué funciones necesitamos y qué tan bueno debe ser el sistema, necesitamos organizar todo.

---

## ¿Qué es "Trazabilidad"?

### Ejemplo fácil:
Cuando cocinas, sigues una receta:
- **Ingredientes** (qué necesitas)
- **Pasos** (qué hacer)
- **Resultado** (qué obtienes)

**En nuestro sistema:**
- **Requisitos** (qué necesita el usuario)
- **Funciones** (qué hace el sistema)
- **Código** (cómo lo programamos)

**Trazabilidad = Conectar todo**
Si Juan dice "quiero ver mis ventas", debemos poder rastrear:
1. ¿Qué función del sistema lo hace?
2. ¿Qué parte del código lo programa?
3. ¿Cómo probamos que funciona?

---

## ¿POR QUÉ ES IMPORTANTE?

### Situación real:
*"El jefe dice: 'Juan se queja de que no puede ver sus ventas de ayer. ¿Qué función está mal?'"*

**Sin trazabilidad:** 😵
"No sé... tengo que revisar todo el código..."

**Con trazabilidad:** 😊
"Es la función 'Consultar Ventas'. Está en el archivo VentasController.cs, línea 45."

### Beneficios:
1. **Cambios rápidos:** Si Juan pide algo nuevo, sabemos qué tocar
2. **Menos errores:** No se nos olvida nada
3. **Fácil mantenimiento:** Otros programadores entienden el código

---

## TABLA SIMPLE DE TRAZABILIDAD

### Formato básico:

| ¿Qué necesita el usuario? | ¿Qué función? | ¿Qué archivo? | ¿Cómo probamos? |
|---------------------------|---------------|---------------|-----------------|
| Juan quiere registrarse   | Registro      | RegistroController.cs | Prueba-Registro |
| María quiere pagar        | Pago          | PagoController.cs     | Prueba-Pago     |
| Juan quiere ver ventas    | Consultas     | VentasController.cs   | Prueba-Ventas   |

---

## EJEMPLO COMPLETO

### Requisito: "Juan quiere ver sus ventas"

**1. ¿Qué necesita Juan?**
- Ver cuánto dinero recibió
- Filtrar por fecha (hoy, ayer, esta semana)
- Descargar un reporte en Excel

**2. ¿Qué función del sistema?**
- Función: "Consultar Ventas"
- Pantalla: Dashboard de ventas
- Botón: "Ver mis ventas"

**3. ¿Qué archivo del código?**
- Archivo: `VentasController.cs`
- Método: `ObtenerVentas()`
- Base de datos: tabla `transacciones`

**4. ¿Cómo probamos?**
- Prueba: "Probar consulta de ventas"
- Verificar: Que muestre las ventas correctas
- Verificar: Que los filtros funcionen

**5. ¿Qué puede salir mal?**
- Error: "No hay ventas para mostrar"
- Error: "Fecha inválida"
- Error: "No se puede descargar Excel"

---

## TU TURNO - EJERCICIO

### Requisito: "María quiere pagar con tarjeta"

**Completa la tabla:**

| Aspecto | Tu respuesta |
|---------|-------------|
| **¿Qué necesita María?** | _________________ |
| **¿Qué función del sistema?** | _________________ |
| **¿Qué archivo del código?** | _________________ |
| **¿Cómo probamos?** | _________________ |
| **¿Qué puede salir mal?** | _________________ |

### Requisito: "Juan quiere devolver dinero a un cliente"

**Completa la tabla:**

| Aspecto | Tu respuesta |
|---------|-------------|
| **¿Qué necesita Juan?** | _________________ |
| **¿Qué función del sistema?** | _________________ |
| **¿Qué archivo del código?** | _________________ |
| **¿Cómo probamos?** | _________________ |
| **¿Qué puede salir mal?** | _________________ |

---

## DEPENDENCIAS (¿Qué necesita qué?)

### Ejemplo fácil:
Para hacer un sándwich necesitas:
1. **Primero:** Comprar pan
2. **Después:** Preparar ingredientes
3. **Por último:** Armar el sándwich

**En nuestro sistema:**
Para que María pueda pagar, necesitamos:
1. **Primero:** Juan debe estar registrado
2. **Después:** El sistema debe validar la tarjeta
3. **Por último:** Confirmar el pago

### Mapa de dependencias:

```
Registrar Juan → Pagar María → Ver Ventas Juan
      ↓              ↓             ↓
   Validar KYC → Validar Tarjeta → Generar Reporte
```

### Tabla de dependencias:

| Función | Necesita que esté listo | ¿Por qué? |
|---------|------------------------|-----------|
| Pagar   | Registrar comercio     | Sin Juan registrado, no puede recibir pagos |
| Ver Ventas | Pagar               | Sin pagos, no hay ventas que mostrar |
| Devolver | Pagar                | Solo se puede devolver lo que se pagó |

---

## EJERCICIO PRÁCTICO

### Situación:
*"El jefe dice: 'Vamos a cambiar cómo funciona el pago. Ahora también queremos aceptar transferencias bancarias.'"*

**Tu trabajo:**
1. **¿Qué funciones se afectan?**
   - Función principal: _______________
   - Funciones relacionadas: _______________

2. **¿Qué archivos hay que tocar?**
   - Archivo principal: _______________
   - Archivos relacionados: _______________

3. **¿Qué pruebas hay que hacer?**
   - Prueba nueva: _______________
   - Pruebas que cambiar: _______________

4. **¿Qué puede salir mal?**
   - Con la nueva función: _______________
   - Con las funciones existentes: _______________

---

## PLANTILLA PARA ORGANIZAR

### Para cada función del sistema:

**FUNCIÓN:** _______________

**USUARIOS:**
- Usuario principal: _______________
- Usuarios secundarios: _______________

**ARCHIVOS DE CÓDIGO:**
- Controlador: _______________
- Servicio: _______________
- Base de datos: _______________

**PRUEBAS:**
- Prueba principal: _______________
- Pruebas de error: _______________

**DEPENDENCIAS:**
- Necesita: _______________
- La necesitan: _______________

**CAMBIOS FUTUROS:**
- Qué se puede modificar: _______________
- Qué NO se debe tocar: _______________

---

## HERRAMIENTAS SIMPLES

### Para organizar:
- **Excel o Google Sheets:** Para hacer tablas
- **Papel y lápiz:** Para dibujar conexiones
- **Sticky notes:** Para mover ideas

### Para rastrear cambios:
- **Comentarios en el código:** Para recordar qué hace cada parte
- **Nombres claros:** `PagoController` es mejor que `Controller1`
- **Carpetas organizadas:** `Pagos/`, `Ventas/`, `Usuarios/`

### Para comunicar con el equipo:
- **Documentos compartidos:** Que todos puedan ver
- **Reuniones cortas:** Para revisar cambios
- **Mensajes claros:** "Cambié la función de pagos"

---

## CONSEJOS PARA JUNIORS

### 1. Mantén todo simple
No hagas conexiones complicadas. Si no lo entiendes tú, otros tampoco.

### 2. Usa nombres claros
`FuncionParaQueJuanVeaSusVentas` es mejor que `Funcion1`

### 3. Documenta mientras trabajas
No dejes la documentación para el final.

### 4. Pregunta si no entiendes
"¿Esta función para qué sirve?" es mejor que adivinar.

### 5. Revisa regularmente
Cada semana, revisa que todo siga conectado correctamente.

---

## LISTA DE VERIFICACIÓN

### Antes de entregar, verifica que:

**Completitud:**
- [ ] Cada requisito tiene su función
- [ ] Cada función tiene su archivo de código
- [ ] Cada archivo tiene su prueba
- [ ] Cada prueba tiene su resultado

**Claridad:**
- [ ] Los nombres son descriptivos
- [ ] Las conexiones son obvias
- [ ] Otros programadores pueden entender

**Corrección:**
- [ ] No hay funciones "huérfanas" (sin requisito)
- [ ] No hay requisitos "huérfanos" (sin función)
- [ ] Las dependencias son correctas

**Mantenibilidad:**
- [ ] Es fácil agregar nuevas funciones
- [ ] Es fácil modificar funciones existentes
- [ ] Es fácil encontrar dónde está cada cosa

---

## PRÓXIMOS PASOS

### En el trabajo real:
1. **Crea tu tabla** desde el primer día
2. **Actualízala** cada vez que cambies algo
3. **Compártela** con tu equipo
4. **Úsala** para planificar cambios

### Para seguir aprendiendo:
1. **Herramientas profesionales:** Jira, Trello, Azure DevOps
2. **Documentación técnica:** Como escribir documentos claros
3. **Gestión de proyectos:** Como organizar el trabajo en equipo

---

**TIEMPO:** 30 minutos
**DIFICULTAD:** Básico
**OBJETIVO:** Organizar y conectar todo lo que aprendimos

**PRÓXIMO PASO:** ¡Aplicar todo esto en un proyecto real!