# Curso Moodle — Modulos por Rol (SCORM 1.2)

> Guia de import para los modulos rol-especificos empaquetados como SCORM 1.2 ZIP. Diferencia clave vs el README principal: estos modulos reportan **completion + score** automaticamente al gradebook de Moodle.

---

## Que diferencia hay con los modulos 1/2 (Recurso Archivo)

| Aspecto | Modulos 1/2 (Recurso Archivo) | Modulos por Rol (SCORM) |
|---------|-------------------------------|--------------------------|
| Formato | HTML standalone + assets | Paquete SCORM 1.2 ZIP |
| Activity Moodle | "Recurso → Archivo" | "Actividad → Paquete SCORM" |
| Tracking completion | NO (solo "visto") | SI (auto: incomplete → passed/failed) |
| Score en gradebook | NO | SI (0-100) |
| Multi-intentos | NO aplica | SI (configurable) |
| Tiempo de sesion | NO se mide | SI se reporta a Moodle |
| Quiz integrado | NO | SI (5 preguntas multi-choice) |
| Mastery score | NO aplica | 70% (3 de 5) |
| Aplicacion | Onboarding general (cualquier rol) | Validacion del rol especifico |

**Recomendacion:** los 2 formatos coexisten en el mismo curso Moodle. Modulos 1/2 al inicio (general), modulos por rol despues (validacion + nota).

---

## Pre-requisitos Moodle

| Item | Detalle |
|------|---------|
| Moodle version | 3.5+ (SCORM 1.2 player incluido nativo, no requiere plugin) |
| Permisos | Profesor / Editor del curso destino |
| HTTPS | **Obligatorio** para que SCORM API se inicialice (algunos browsers bloquean cross-origin sin TLS) |
| Browser alumno | Chrome / Edge / Firefox / Safari modernos |

---

## Contenido del SCORM ZIP

Cada modulo de rol tiene este contenido minimal:

```
rol-NN-{nombre}.scorm.zip
├── imsmanifest.xml         (1 KB · SCORM 1.2 manifest, declarado masteryscore=70)
└── index.html              (~42 KB · cover + slide rol + quiz + cierre + SCORM API + TTS + Notas)
```

---

## Opcion A — Subir UN modulo (POC: Dev Junior)

### Pasos en Moodle 4.x (similar 3.x)

1. Curso destino → click **"Activar edicion"** (top-right)
2. En la seccion donde queres el modulo → click **"Añadir actividad o recurso"**
3. Filtrar **"Actividades"** → seleccionar **"Paquete SCORM"** (no "Recurso → Archivo")
4. Configurar **General**:
   - **Nombre:** `Onboarding Rol — Dev Junior`
   - **Descripcion:** `Slide del rol Dev Junior + quiz 5 preguntas. Aprobacion 70%. ~5-7 min.`
   - **Mostrar descripcion en pagina del curso:** marcar
5. Seccion **"Paquete"** → arrastrar `rol-12-dev-junior.scorm.zip` a la zona upload
   - Moodle valida automaticamente el `imsmanifest.xml` al subir
   - Si el ZIP esta corrupto o falta el manifest, Moodle rechaza con mensaje claro
6. Seccion **"Apariencia"**:
   - **Mostrar paquete:** `Ventana emergente` (recomendado, pantalla full Reveal/SCORM)
   - **Tamano emergente:** `1024 x 768` o `Pantalla completa`
   - **Mostrar nombre actividad:** SI
   - **Saltar pagina de bienvenida:** NO (deja que el alumno vea el cover)
   - **Ocultar boton de previsualizacion:** SI (alumno entra directo al modo evaluado)
7. Seccion **"Disponibilidad"** (opcional, recomendado):
   - **Restringir acceso por grupo/rol:** asignar al grupo "Devs Junior" si tenes grupos por rol creados
8. Seccion **"Calificacion"**:
   - **Metodo de calificacion:** `Calificacion mas alta` (si permitis re-intentos)
   - **Calificacion maxima:** `100`
   - **Calificacion para aprobar:** `70`
9. Seccion **"Gestion del intento"**:
   - **Numero maximo de intentos:** `Sin limite` (recomendado para self-paced) o `3` si queres limite
   - **Calificacion de los intentos:** `Calificacion mas alta`
10. Seccion **"Configuracion de compatibilidad"** (avanzado, opcional):
    - **Forzar nuevo intento:** `Cuando se complete el anterior` (alumno no puede empezar otro mientras el anterior esta abierto)
    - **Bloquear despues del ultimo intento:** SI si pusiste limite
11. **Guardar cambios y volver al curso**

### Validacion post-import

1. Click sobre el modulo en el curso (como alumno o "Cambiar rol → Estudiante")
2. Click **"Entrar"** o **"Comenzar nuevo intento"**
3. Se abre la ventana SCORM con el HTML
4. Verificar que el indicador SCORM (oculto por default, pero log en console) diga `connected`:
   - F12 → Console → buscar `SCORM init OK` o equivalente
5. Navegar: cover → contenido → quiz → submit
6. Score se reporta a Moodle automaticamente
7. Cerrar ventana SCORM
8. Curso → **Calificaciones** → verificar la nota aparece en la columna del modulo

---

## Opcion B — Subir TODOS los modulos por rol (15 paquetes)

Una vez tengas los 15 ZIPs SCORM (uno por rol), opciones para escalar:

### B.1 — Manual (recomendado para POC + validacion final)

Repetir Opcion A para cada modulo. Tiempo: ~5 min/modulo × 15 = ~75 min Capital Humano.

### B.2 — Restore desde curso template (para multi-empresa o multi-cohorte)

Si vas a replicar el curso a multiples instancias Moodle Fintexa:

1. Crear el curso UNA vez con los 15 modulos manualmente
2. Exportar el curso completo: **Curso → Administracion → Restaurar / Backup → Hacer backup**
3. Para nuevas instancias: **Restaurar** desde el backup → todos los modulos + estructura quedan importados
4. Bonus: incluye configuracion de calificaciones, restricciones, grupos

### B.3 — Bulk import via Moodle CLI (admin only)

```bash
# En el server Moodle
cd /var/www/moodle
sudo -u www-data php admin/cli/upgrade.php
sudo -u www-data php admin/cli/scheduled_task.php --execute="\\core\\task\\file_trash_cleanup_task"

# Para cada ZIP:
sudo -u www-data php local/scorm_import/cli/import.php \
  --courseid=42 \
  --filepath=/tmp/scorm-zips/rol-NN.scorm.zip
```

Requiere plugin `local_scorm_import` o similar — consultar IT.

---

## Asignacion por rol (recomendado)

Si Capital Humano quiere que cada alumno solo vea el modulo de SU rol:

1. Curso → **Administracion → Usuarios → Grupos** → crear grupos por rol:
   - "Dev Junior"
   - "Tech Lead"
   - "QA"
   - ... (15 grupos)
2. Asignar usuarios a sus grupos respectivos
3. En cada modulo SCORM → **Restricciones de acceso → Restriccion → Grupo** → seleccionar el grupo correspondiente
4. Resultado: el alumno solo ve el modulo de su rol al entrar al curso

**Alternativa simpler:** si los alumnos pueden ver cualquier modulo (curiosidad) pero solo se les califica el suyo, dejar todos visibles y asignar via "Cohort" o "Profile field" custom.

---

## Calificaciones en gradebook

Despues de que el alumno completa el quiz, Moodle registra automaticamente:

| Campo | Origen | Ejemplo |
|-------|--------|---------|
| **cmi.core.lesson_status** | JS reporta `passed` o `failed` segun >= 70% | `passed` |
| **cmi.core.score.raw** | JS reporta el score 0-100 | `80` |
| **cmi.core.score.min / max** | JS reporta `0 / 100` | `0 / 100` |
| **cmi.core.session_time** | JS reporta el tiempo de sesion en HHHH:MM:SS | `0000:05:23` |

### Donde lo ve Capital Humano

1. Curso → **Calificaciones** (Administracion del curso)
2. Vista por defecto: tabla alumnos × actividades
3. Columna del modulo SCORM muestra la nota (0-100)
4. Click sobre el nombre del modulo → ve detalle de intentos por alumno:
   - Fecha/hora de inicio
   - Tiempo total
   - Score
   - Estado (`passed` / `failed` / `incomplete`)
   - Respuestas pregunta-por-pregunta (si SCORM lo reporta — POC actual no, pero se puede agregar)

### Reportes utiles

- **Curso → Reportes → Vista general de calificaciones** — todas las notas en una grilla
- **Reporte de actividad de SCORM** — ver intentos individuales por alumno
- **Curso → Reportes → Logs** — eventos de inicio/fin de cada modulo SCORM

---

## Troubleshooting

### El SCORM no inicia / pantalla en blanco

- **Causa:** browser bloquea Web Speech API o el manifest XML tiene error de validacion
- **Fix:**
  1. Abrir el modulo, F12 → Console → revisar errores
  2. Si dice "SCORM API not found": el alumno abrio el HTML directo (no por Moodle SCORM player) — explicarle que debe entrar via el modulo del curso
  3. Si dice "manifest schema error": re-empaquetar el ZIP — `imsmanifest.xml` debe estar en la raiz del ZIP, no en subfolder
  4. Si dice "CORS blocked": el Moodle es HTTP, no HTTPS — TLS obligatorio para SCORM activity en muchos browsers modernos

### Score no se registra en gradebook

- **Causa:** SCORM Init falla silently o el alumno cierra la ventana antes de Finish
- **Fix:**
  1. Verificar que el alumno haya clickeado "Verificar respuestas" (no solo respondido)
  2. Verificar que la ventana SCORM se cerro normalmente (X o boton Cerrar)
  3. Moodle → Curso → Reportes → Logs SCORM → buscar el alumno → ver si hubo `LMSCommit` y `LMSFinish`
  4. Si no hubo Finish: el alumno cerro abruptamente. El score se grabo en el ultimo `LMSCommit` (cada vez que Set... value), pero `lesson_status` puede haber quedado `incomplete`

### El alumno aprueba pero Moodle dice "incomplete"

- **Causa:** mastery score Moodle vs SCORM no estan alineados
- **Fix:** verificar que en la actividad SCORM Moodle, **"Calificacion para aprobar"** este en `70` y que en `imsmanifest.xml` el `<adlcp:masteryscore>70</adlcp:masteryscore>` coincida

### Quiz no se puede repetir / boton no aparece

- **Causa:** "Numero maximo de intentos" esta en `1` y el alumno ya consumio el intento
- **Fix:**
  1. Como profesor: **Modulo SCORM → Borrar intento del alumno** (Administracion del modulo → Intentos del usuario → Eliminar)
  2. O cambiar config: aumentar a `3` o `Sin limite`

### Audio TTS no funciona dentro de Moodle SCORM player

- **Causa:** SCORM player abre el HTML en iframe, algunos browsers bloquean Web Speech API en iframes
- **Fix:**
  1. Verificar que Moodle este en HTTPS (no HTTP)
  2. Apariencia del modulo SCORM → **"Mostrar paquete"** = `Ventana emergente` (no iframe). Web Speech API funciona en ventana emergente
  3. Si igual no funciona, el alumno puede usar **Notas** (panel texto) que no requiere TTS

### El indicador "SCORM: standalone" aparece dentro de Moodle

- **Esto es bug de findAPI**, no del SCORM en si
- **Causa:** el HTML esta en un iframe muy anidado y `findAPI()` no llega al window del LMS
- **Fix:** abrir como ventana emergente (no iframe) — opcion en Apariencia del modulo

---

## Para escalar a los 14 roles restantes

Cuando este POC valide en Moodle real, generamos los 14 restantes con:

1. Mismo template `index.html` (cambiando solo contenido del slide + 5 preguntas del quiz)
2. Mismo `imsmanifest.xml` template (cambiando solo `identifier` y `title`)
3. Mismo empaquetado SCORM ZIP

Folders por rol:

```
modulo-roles/
├── 01-ceo-cto/             (slide O04)
├── 02-cfo/                 (slide O05)
├── 03-ciso/                (slide O06)
├── 04-pm/                  (slide O07)
├── 05-po/                  (slide O08)
├── 06-capital-humano/      (slide O09)
├── 07-tech-lead/           (slide O10)
├── 08-dev-senior/          (slide O11)
├── 12-dev-junior/  ✅ POC  (slide O12)
├── 09-frontend-arq/        (slide O13)
├── 10-qa/                  (slide O14)
├── 11-sre/                 (slide O15)
├── 13-soporte/             (slide O16)
├── 14-analista/            (slide O17)
└── 15-compliance-qsa/      (slide O18)
```

Exports: 15 ZIPs en `exports/` listos para Capital Humano.

---

## Roadmap fase 2 (despues del POC validado)

| # | Mejora | Tiempo | Beneficio |
|---|--------|--------|-----------|
| 1 | Generar los 14 roles restantes con mismo template | 30 min/rol = 7h IA | Cobertura completa |
| 2 | Reportar respuestas pregunta-por-pregunta a SCORM | 30 min | Capital Humano ve detalle de cada respuesta del alumno |
| 3 | Quiz pool de 10 preguntas, random 5 al alumno | 1h/rol | Reduce trampa entre alumnos del mismo rol |
| 4 | Audio MP3 pre-grabado por slide (reemplaza TTS) | 30-60 min/rol | Calidad audio profesional |
| 5 | Multi-idioma (es-AR / es-MX / en-US) | 1h/idioma | Capital Humano internacional |
| 6 | Dashboard de progreso del curso completo | 2h | Analytics adopcion 15 roles |

---

## Atajos teclado dentro del modulo SCORM

| Tecla | Accion |
|-------|--------|
| `→` | Siguiente seccion (cover → contenido → quiz → done) |
| `←` | Seccion anterior |
| Click `Notas` | Toggle panel narracion escrita |
| Click `Audio` | Reproducir narracion TTS (rate 1.25) |

---

## Soporte

- POC files: `docs/E_market_fintexa_v2/curso-moodle-poc/modulo-roles/`
- Issues import Moodle: equipo Capital Humano + IT
- Documentacion SCORM 1.2: https://scorm.com/scorm-explained/technical-scorm/run-time/run-time-reference/
- Validador SCORM: https://cloud.scorm.com/sc/guest/SignInForm

---

*Curso Moodle POC — SCORM v1.0 · Marketplace v2026.504.2-beta · Fintexa.tech Ia-Team · 2026-05-04*
