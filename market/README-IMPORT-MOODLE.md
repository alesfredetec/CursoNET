# Curso Moodle POC — Import Guide

> Guia paso a paso para subir el POC del curso Fintexa Marketplace V2 a Moodle (versiones 3.x y 4.x). Incluye 2 opciones de import + troubleshooting comun.

---

## Contenido del POC

```
curso-moodle-poc/
├── README-IMPORT-MOODLE.md       (este archivo)
├── modulo-01-quickstart.html     (4 slides Q01-Q04 + cover + cierre · 11 min)
├── modulo-02-onboarding.html     (5 slides O01-O20 + cover + cierre · 14 min)
├── imgdeck/init1/                (7 PNG cinematic 16:9 ~20MB)
└── curso-moodle-poc.zip          (paquete listo para subir)
```

**Total:** 2 modulos · 9 slides + 4 cubierta · ~25 min curso · ~20 MB

---

## Pre-requisitos

| Item | Detalle |
|------|---------|
| Moodle version | 3.9+ (probado en 4.1+) |
| Permisos | Profesor o Admin del curso destino |
| Browser alumno | Chrome, Edge, Firefox, Safari modernos (TTS audio requiere Chrome/Edge para mejor calidad voz neural) |
| HTTPS | Recomendado para que Web Speech API (TTS) funcione sin restricciones |
| Plugin H5P | NO requerido — el HTML es standalone |
| Plugin SCORM | NO requerido — POC va como HTML simple |

---

## Empaquetado en ZIP (obligatorio)

Moodle no permite subir HTML suelto si tiene assets locales (imagenes en subfolders). El POC tiene `imgdeck/init1/*.PNG` referenciadas desde los HTMLs, asi que **debe subirse como ZIP** que Moodle descomprime.

### Generar el ZIP

**Linux / macOS / Git Bash:**
```bash
cd docs/E_market_fintexa_v2/curso-moodle-poc
zip -r curso-moodle-poc.zip \
  modulo-01-quickstart.html \
  modulo-02-onboarding.html \
  imgdeck/
```

**PowerShell (Windows):**
```powershell
cd docs/E_market_fintexa_v2/curso-moodle-poc
Compress-Archive -Path modulo-01-quickstart.html, modulo-02-onboarding.html, imgdeck `
                 -DestinationPath curso-moodle-poc.zip -Force
```

**Resultado:** `curso-moodle-poc.zip` ~18-20 MB.

### Estructura del ZIP

```
curso-moodle-poc.zip
├── modulo-01-quickstart.html     (root del ZIP)
├── modulo-02-onboarding.html
└── imgdeck/init1/*.PNG
```

Cuando Moodle descomprime, los paths relativos `imgdeck/init1/Diapositiva1.PNG` resuelven OK desde los HTMLs.

---

## Opcion A — Recurso "Archivo" (RECOMENDADA)

Sube el ZIP, Moodle lo descomprime, el alumno abre el HTML en pestaña nueva o ventana propia.

**Ventajas:** navegacion full Reveal.js (← → / espacio / fullscreen / TTS), calidad pantalla maxima, no hay scroll-dentro-de-scroll.

**Desventajas:** se abre en ventana nueva (rompe el flujo Moodle si el alumno cierra y vuelve).

### Pasos en Moodle 4.x

1. Entrar al curso destino → click **"Activar edicion"** (top-right)
2. En la seccion donde queres el modulo → click **"Añadir actividad o recurso"**
3. Filtro **"Recurso"** → seleccionar **"Archivo"**
4. Configurar:
   - **Nombre:** `Modulo 1 — Quickstart Primera Sesion (11 min)`
   - **Descripcion:** `4 slides + cover + cierre. Cualquier rol. 10 minutos al primer comando productivo.`
   - **Mostrar descripcion en pagina del curso:** marcar
5. Seccion **"Contenido"** → arrastrar `curso-moodle-poc.zip` a la zona de upload
6. Una vez subido → click sobre el ZIP → seleccionar **"Descomprimir"** (Moodle expande el ZIP)
7. Click sobre `modulo-01-quickstart.html` → seleccionar **"Establecer como archivo principal"**
8. Seccion **"Apariencia"**:
   - **Mostrar:** `En ventana emergente` (recomendado para Reveal.js)
   - O **"Forzar descarga"** si queres que el alumno baje el archivo
9. **Guardar cambios y volver al curso**
10. Repetir pasos 1-9 para `modulo-02-onboarding.html`. Como ya esta el ZIP descomprimido en el primer recurso, podes:
    - **Opcion 1:** subir el mismo ZIP de nuevo y elegir HTML modulo 2 como principal
    - **Opcion 2:** copiar `modulo-02-onboarding.html` desde "Archivos del curso" y crear nuevo Recurso apuntando solo al modulo 2 (mas limpio)

### Resultado

El alumno ve 2 enlaces:
- **Modulo 1 — Quickstart Primera Sesion (11 min)** → click → ventana nueva con Reveal.js full
- **Modulo 2 — Onboarding generico (14 min)** → click → ventana nueva con Reveal.js full

---

## Opcion B — Recurso "Pagina" con iframe embed

Crea una Pagina Moodle con un `<iframe>` que carga el HTML embebido.

**Ventajas:** integrado al flujo Moodle (alumno no sale del LMS), navegacion Moodle (anterior/siguiente actividad) sigue funcionando.

**Desventajas:** scroll dentro de scroll si el HTML no escala bien al iframe, TTS audio puede tener latencia mayor, fullscreen Reveal.js puede no funcionar dentro de iframe en algunos browsers.

### Pasos

1. **Subir el ZIP a "Archivos del curso"** primero (Banco de archivos):
   - Curso → Administracion → Banco de contenido → Subir → seleccionar `curso-moodle-poc.zip`
   - Click derecho → Descomprimir
   - Anotar la URL publica del HTML (sera algo como `https://moodle.fintexa.tech/draftfile.php/.../modulo-01-quickstart.html`)
2. **Activar edicion** en el curso → **Añadir actividad o recurso** → **Pagina**
3. Configurar:
   - **Nombre:** `Modulo 1 — Quickstart (embebido)`
   - **Descripcion:** breve
4. Seccion **"Contenido"** → cambiar a vista codigo HTML (boton `</>`) y pegar:
   ```html
   <div style="position:relative; width:100%; padding-bottom:56.25%; height:0; overflow:hidden;">
     <iframe
       src="modulo-01-quickstart.html"
       style="position:absolute; top:0; left:0; width:100%; height:100%; border:0;"
       allowfullscreen
       allow="autoplay; fullscreen; speaker">
     </iframe>
   </div>
   ```
5. **Apariencia** → **"Mostrar nombre de la pagina":** opcional · **"Mostrar descripcion":** opcional
6. **Guardar cambios y volver al curso**
7. Repetir para modulo 2 con `src="modulo-02-onboarding.html"`

**Notas tecnicas iframe:**
- El `padding-bottom: 56.25%` mantiene aspect 16:9 responsivo (9/16 = 0.5625)
- `allow="speaker"` habilita Web Speech API en algunos browsers (Chrome lo permite por default si la pagina padre es HTTPS)
- Si Moodle bloquea iframes a archivos del mismo dominio: Admin → Seguridad → Politicas del sitio → permitir `<iframe>` y `src` relativo a `pluginfile.php`

---

## Troubleshooting

### El HTML carga pero las imagenes no se ven

- **Causa:** el ZIP no se descomprimio o las paths quedaron rotos
- **Fix:** En Moodle, ir al recurso → editar → "Archivos" → verificar que `imgdeck/init1/Diapositiva*.PNG` esten presentes. Si no, re-subir el ZIP y descomprimir explicitamente

### El boton Audio (TTS) no funciona

- **Causa:** Web Speech API requiere HTTPS en algunos browsers (Safari especialmente)
- **Fix:** verificar que el Moodle este servido por HTTPS (no `http://`). Si es HTTP, los browsers modernos bloquean Web Speech API por seguridad
- **Alternativa:** usar el boton "Notas" (panel inferior con texto) que funciona sin HTTPS

### TTS suena robotico o sin acento espanol

- **Causa:** el SO del alumno no tiene voces espanolas instaladas
- **Fix Windows 11:** Settings → Time & Language → Language → Add language → Spanish (Argentina/Mexico/Spain) → instalar paquete de voces
- **Fix macOS:** System Preferences → Accessibility → Spoken Content → System Voice → Customize → Spanish voices
- **Fix Linux:** instalar `speech-dispatcher` + `mbrola-es*` packages

### Reveal.js no carga (slides en blanco)

- **Causa:** el browser bloquea el CDN `cdn.jsdelivr.net` (politica empresarial / firewall)
- **Fix:** descargar Reveal.js localmente y reemplazar las URLs CDN por paths relativos:
  ```bash
  # En el folder del POC:
  mkdir -p libs/reveal.js
  cd libs/reveal.js
  curl -o reveal.css   https://cdn.jsdelivr.net/npm/reveal.js@5.1.0/dist/reveal.css
  curl -o reveal.js    https://cdn.jsdelivr.net/npm/reveal.js@5.1.0/dist/reveal.js
  curl -o black.css    https://cdn.jsdelivr.net/npm/reveal.js@5.1.0/dist/theme/black.css
  curl -o notes.js     https://cdn.jsdelivr.net/npm/reveal.js@5.1.0/plugin/notes/notes.js
  curl -o markdown.js  https://cdn.jsdelivr.net/npm/reveal.js@5.1.0/plugin/markdown/markdown.js
  ```
  Luego en los HTMLs cambiar `https://cdn.jsdelivr.net/npm/reveal.js@5.1.0/...` por `libs/reveal.js/...`

### Los fonts (Cormorant Garamond / Inter) se ven raros

- **Causa:** Google Fonts bloqueado por firewall corporativo
- **Fix:** mismo patron que Reveal.js — descargar TTF/WOFF localmente y embeberlos via `@font-face`. Alternativa: el HTML cae a fonts del SO (system-ui, serif, monospace) sin romper

### El modulo se ve cortado verticalmente en pantallas chicas

- **Causa:** Reveal.js esta configurado a 1280×720 fijos
- **Fix:** ajustar `Reveal.initialize({ width: 1280, height: 720, minScale: 0.2, maxScale: 2.0 })` segun tu audiencia. Para pantallas <1080p, bajar height a 640 o usar `disableLayout: false`

### El alumno reporta "no veo la narracion escrita"

- **Causa:** el alumno no ve los botones top-right (puede estar en mobile o pantalla muy chica)
- **Fix:** explicar en la descripcion del recurso Moodle que hay 2 botones top-right: **Notas** (panel texto) y **Audio** (TTS voz)
- **Alt:** agregar instructivo en el primer slide del modulo

### Quiero medir completion / score (tracking)

- POC actual no tiene tracking SCORM. El alumno puede abrir el HTML, navegarlo, cerrarlo, y Moodle no registra avance
- **Fix fase 2:** wrappear el HTML en SCORM 2004 ZIP usando libreria `reveal.js-scorm` o packager manual
- Esto agrega `<script>` que reporta a Moodle: slide actual, tiempo en cada slide, completion, score si hay quiz embebido
- Estimado: ~1h por modulo

---

## Hosting alternativo (sin subir a Moodle)

Si tu Moodle tiene restricciones de tamaño o no permite descomprimir ZIPs, podes hostear los HTMLs en otro lado y embeberlos como iframe:

### Opcion 1 — Azure Blob Storage (recomendado para Fintexa)

```bash
# Crear container con acceso publico
az storage container create --name onboarding-curso --public-access blob \
  --account-name fintexamarketplace

# Subir archivos
az storage blob upload-batch -d onboarding-curso \
  --source docs/E_market_fintexa_v2/curso-moodle-poc \
  --account-name fintexamarketplace
```

URL publica: `https://fintexamarketplace.blob.core.windows.net/onboarding-curso/modulo-01-quickstart.html`

**Pros:** control de retention, audit trail, compliance Fintexa, sin limites tamaño Moodle.
**Cons:** requiere setup Azure (10 min una vez).

### Opcion 2 — GitHub Pages

```bash
# Crear repo publico (o privado con plan)
gh repo create fintexa-onboarding-curso --public
cd docs/E_market_fintexa_v2/curso-moodle-poc
git init && git add . && git commit -m "POC curso Moodle" && git push
gh repo edit --enable-pages --pages-branch main --pages-path /
```

URL: `https://{user}.github.io/fintexa-onboarding-curso/modulo-01-quickstart.html`

**Pros:** gratis, deploy instant, CDN global.
**Cons:** publico (o pagar GitHub Team para privado), no hay control compliance Fintexa.

### Embedear desde Moodle

Una vez hosteado, en Moodle Pagina → editor codigo → iframe:
```html
<iframe src="https://fintexamarketplace.blob.core.windows.net/onboarding-curso/modulo-01-quickstart.html"
        style="width:100%; height:600px; border:0;" allowfullscreen></iframe>
```

---

## Roadmap fase 2 (despues del POC)

| # | Mejora | Tiempo estimado | Beneficio |
|---|--------|-----------------|-----------|
| 1 | Compresion PNG → JPG (de 20MB a 4MB) | 5 min | Carga inicial mas rapida |
| 2 | Reveal.js + Google Fonts hosted local (offline) | 30 min | Funciona sin internet, sin firewall issues |
| 3 | SCORM 2004 packaging para tracking | 1-2h | Moodle registra completion/score |
| 4 | Quiz Moodle nativo por modulo (5 preguntas) | 15 min/modulo | Evaluacion + nota en gradebook |
| 5 | Audio MP3 pre-grabado (reemplaza TTS) | 30-60 min/modulo | Calidad audio profesional |
| 6 | Modulo 3 — rol especifico (15 variantes) | 30 min/rol | Cobertura completa onboarding por rol |
| 7 | H5P interactivo (drag-drop, flashcards) | 1h/modulo | Mas interactividad self-paced |

---

## Atajos de teclado dentro del HTML

| Tecla | Accion |
|-------|--------|
| `→` `Espacio` `PageDown` | Siguiente slide |
| `←` `PageUp` | Slide anterior |
| `Home` / `End` | Primera / ultima slide |
| `F` | Fullscreen |
| `S` | Speaker view (ventana aparte con notes + timer) |
| `Esc` / `O` | Overview todas las slides |
| `Ctrl+P` | Print → PDF (export Moodle si necesario) |

Botones overlay top-right del HTML:
- **Notas:** toggle panel inferior con narracion completa (cream background, texto oscuro, sans-serif legible)
- **Audio:** TTS Web Speech API (espanol, rate 1.25, sanitizado) — pulsa cyan mientras habla

---

## Soporte

- Issues / mejoras del POC: `docs/E_market_fintexa_v2/curso-moodle-poc/`
- Preguntas sobre integracion Moodle Fintexa: equipo de Capital Humano + IT
- Documentacion Reveal.js completa: https://revealjs.com

---

*Curso Moodle POC v1.0 · Marketplace v2026.504.2-beta · Fintexa.tech Ia-Team · 2026-05-04*
