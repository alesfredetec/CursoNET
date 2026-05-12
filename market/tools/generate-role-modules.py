"""
Generador de modulos SCORM por rol — Fintexa Marketplace V2 onboarding.

Lee el template (basado en Dev Junior POC validado) + 14 configs de rol
+ genera 14 carpetas modulo-roles/NN-slug/ con index.html + imsmanifest.xml.

Empaquetado SCORM ZIP se hace aparte con PowerShell loop (ver README).

Usage: python tools/generate-role-modules.py
"""
import os
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent  # curso-moodle-poc/
OUTPUT_BASE = ROOT / "modulo-roles"

# ─────────────────────────────────────────────────────────────────────
# TEMPLATE HTML (basado en POC Dev Junior validado)
# ─────────────────────────────────────────────────────────────────────

HTML_TEMPLATE = r"""<!DOCTYPE html>
<html lang="es">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Fintexa Marketplace V2 — Rol {role_title} · Onboarding</title>
<link href="https://fonts.googleapis.com/css2?family=Cormorant+Garamond:wght@500;600;700&family=Inter:wght@300;400;500;600;700&family=JetBrains+Mono:wght@400;500&display=swap" rel="stylesheet">
<style>
  :root {{
    --concrete: #0A0E14;
    --concrete-2: #12181F;
    --concrete-3: #1A2028;
    --amber: #E8B86A;
    --amber-deep: #c4943a;
    --cyan: #5FB8D4;
    --green: #7DB87D;
    --red: #D44A4A;
    --gold: #d4a843;
    --text: #E8E8E8;
    --text-dim: #8A9098;
    --border: rgba(232, 184, 106, 0.2);
  }}
  * {{ box-sizing: border-box; margin: 0; padding: 0; }}
  html, body {{
    height: 100%;
    background: var(--concrete);
    color: var(--text);
    font-family: 'Inter', system-ui, sans-serif;
    line-height: 1.6;
  }}
  body {{
    display: flex;
    flex-direction: column;
    background:
      radial-gradient(ellipse at top, rgba(232, 184, 106, 0.05) 0%, transparent 60%),
      radial-gradient(ellipse at bottom, rgba(95, 184, 212, 0.04) 0%, transparent 60%),
      var(--concrete);
  }}
  a {{ color: var(--cyan); text-decoration: none; border-bottom: 1px dotted var(--cyan); }}
  h1, h2, h3 {{
    font-family: 'Cormorant Garamond', serif;
    color: var(--amber);
    font-weight: 600;
    text-shadow: 0 0 30px rgba(232, 184, 106, 0.2);
  }}
  h1 {{ font-size: 2.6em; line-height: 1.15; margin-bottom: 0.4em; }}
  h2 {{ font-size: 1.7em; color: var(--cyan); margin-top: 0.7em; margin-bottom: 0.4em; }}
  h3 {{ font-size: 1.2em; color: var(--amber); margin-top: 1.2em; margin-bottom: 0.4em; }}
  code {{
    font-family: 'JetBrains Mono', monospace;
    color: var(--amber);
    background: var(--concrete-3);
    padding: 1px 6px;
    border-radius: 2px;
    font-size: 0.92em;
  }}
  pre {{
    background: var(--concrete-2);
    border: 1px solid var(--border);
    border-radius: 4px;
    padding: 14px 18px;
    overflow-x: auto;
    margin: 1em 0;
    box-shadow: 0 4px 20px rgba(0,0,0,0.4);
  }}
  pre code {{
    font-family: 'JetBrains Mono', monospace;
    color: var(--text);
    background: transparent;
    padding: 0;
    font-size: 0.85em;
    line-height: 1.5;
    white-space: pre;
  }}
  pre::-webkit-scrollbar {{ width: 8px; height: 8px; }}
  pre::-webkit-scrollbar-track {{ background: var(--concrete); }}
  pre::-webkit-scrollbar-thumb {{ background: var(--amber-deep); border-radius: 4px; }}

  .topbar {{
    height: 56px;
    background: var(--concrete-2);
    border-bottom: 1px solid var(--border);
    display: flex;
    align-items: center;
    padding: 0 32px;
    flex-shrink: 0;
    gap: 18px;
  }}
  .topbar .brand {{
    font-family: 'Cormorant Garamond', serif;
    font-size: 17px;
    color: var(--amber);
    letter-spacing: 1px;
    flex: 1;
  }}
  .topbar .progress {{
    display: flex;
    align-items: center;
    gap: 8px;
    font-family: 'JetBrains Mono', monospace;
    font-size: 12px;
    color: var(--text-dim);
  }}
  .topbar .progress-bar {{
    width: 180px;
    height: 4px;
    background: var(--concrete-3);
    border-radius: 2px;
    overflow: hidden;
  }}
  .topbar .progress-fill {{
    height: 100%;
    background: linear-gradient(to right, var(--amber), var(--cyan));
    transition: width 300ms ease-out;
  }}
  .topbar .scorm-status {{
    display: none;
    font-family: 'JetBrains Mono', monospace;
    font-size: 10px;
    color: var(--text-dim);
    border: 1px solid var(--border);
    padding: 3px 8px;
    border-radius: 3px;
  }}
  .topbar .scorm-status.show-debug {{ display: inline-block; }}
  .topbar .scorm-status.connected {{ color: var(--green); border-color: var(--green); }}
  .topbar .scorm-status.disconnected {{ color: var(--text-dim); border-color: var(--text-dim); }}
  .topbar .scorm-status.error {{ color: var(--red); border-color: var(--red); display: inline-block; }}

  .stage {{
    flex: 1;
    overflow-y: auto;
    padding: 40px 32px 100px 32px;
    display: flex;
    justify-content: center;
  }}
  .stage::-webkit-scrollbar {{ width: 10px; }}
  .stage::-webkit-scrollbar-track {{ background: var(--concrete); }}
  .stage::-webkit-scrollbar-thumb {{ background: var(--amber-deep); border-radius: 5px; }}

  .container {{
    max-width: 980px;
    width: 100%;
  }}
  .section {{ display: none; animation: fadeIn 350ms ease-out; }}
  .section.active {{ display: block; }}
  @keyframes fadeIn {{
    from {{ opacity: 0; transform: translateY(10px); }}
    to {{ opacity: 1; transform: translateY(0); }}
  }}

  .cover {{
    text-align: center;
    padding: 80px 20px;
  }}
  .cover h1 {{
    font-size: 3em;
    background: linear-gradient(135deg, var(--amber) 0%, var(--gold) 100%);
    -webkit-background-clip: text;
    background-clip: text;
    -webkit-text-fill-color: transparent;
    text-shadow: none;
  }}
  .cover .subtitle {{
    color: var(--text-dim);
    font-family: 'Inter', sans-serif;
    font-weight: 300;
    font-size: 14px;
    letter-spacing: 2.5px;
    text-transform: uppercase;
    margin-bottom: 1.5em;
  }}
  .cover .archetype {{
    display: inline-block;
    border: 1px solid var(--amber);
    color: var(--amber);
    padding: 8px 20px;
    font-family: 'JetBrains Mono', monospace;
    font-size: 13px;
    letter-spacing: 2px;
    margin-top: 1.5em;
    text-transform: uppercase;
  }}
  .cover .meta {{
    color: var(--text-dim);
    font-size: 14px;
    line-height: 2;
    margin-top: 3em;
    max-width: 600px;
    margin-left: auto;
    margin-right: auto;
  }}
  .cover .meta strong {{ color: var(--cyan); }}

  .content-slide .header-bar {{
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1px solid var(--border);
    padding-bottom: 10px;
    margin-bottom: 24px;
  }}
  .content-slide .deck-tag {{
    color: var(--amber);
    font-size: 11px;
    letter-spacing: 2px;
    text-transform: uppercase;
    font-weight: 500;
  }}
  .content-slide .slide-id {{
    color: var(--text-dim);
    font-family: 'JetBrains Mono', monospace;
    font-size: 11px;
    letter-spacing: 1px;
  }}
  .callout {{
    border-left: 3px solid var(--amber);
    background: rgba(232, 184, 106, 0.06);
    padding: 14px 18px;
    margin: 1em 0;
    font-size: 0.95em;
  }}
  .callout-warn {{
    border-left-color: var(--red);
    background: rgba(212, 74, 74, 0.07);
  }}
  .callout-warn::before {{ content: "!  "; color: var(--red); font-weight: 700; }}
  .callout-pci {{
    border-left-color: var(--red);
    background: rgba(212, 74, 74, 0.1);
    font-weight: 500;
  }}
  .callout-pci::before {{ content: "[PCI] "; color: var(--red); font-weight: 700; }}

  .quiz-intro {{
    background: var(--concrete-2);
    border: 1px solid var(--border);
    border-radius: 6px;
    padding: 20px 24px;
    margin-bottom: 24px;
  }}
  .quiz-intro p {{ margin: 0; color: var(--text-dim); font-size: 0.95em; }}
  .quiz-intro strong {{ color: var(--cyan); }}
  .question {{
    background: var(--concrete-2);
    border: 1px solid var(--border);
    border-radius: 6px;
    padding: 20px 24px;
    margin-bottom: 18px;
    transition: border-color 200ms;
  }}
  .question.answered {{ border-color: var(--amber); }}
  .question.correct {{ border-color: var(--green); background: rgba(125, 184, 125, 0.05); }}
  .question.incorrect {{ border-color: var(--red); background: rgba(212, 74, 74, 0.05); }}
  .question .q-num {{
    font-family: 'JetBrains Mono', monospace;
    color: var(--amber);
    font-size: 12px;
    letter-spacing: 1.5px;
    margin-bottom: 8px;
  }}
  .question .q-text {{
    font-size: 1.05em;
    color: var(--text);
    margin-bottom: 16px;
    font-weight: 500;
  }}
  .options {{ display: flex; flex-direction: column; gap: 8px; }}
  .option {{
    display: flex;
    align-items: center;
    padding: 10px 14px;
    border: 1px solid var(--border);
    border-radius: 4px;
    cursor: pointer;
    transition: all 150ms;
    background: var(--concrete);
  }}
  .option:hover:not(.disabled) {{
    border-color: var(--cyan);
    background: var(--concrete-3);
  }}
  .option.selected {{
    border-color: var(--amber);
    background: rgba(232, 184, 106, 0.08);
  }}
  .option.disabled {{ cursor: not-allowed; opacity: 0.7; }}
  .option.show-correct {{
    border-color: var(--green);
    background: rgba(125, 184, 125, 0.12);
  }}
  .option.show-incorrect {{
    border-color: var(--red);
    background: rgba(212, 74, 74, 0.08);
  }}
  .option input[type="radio"] {{
    appearance: none;
    width: 18px;
    height: 18px;
    border: 1px solid var(--text-dim);
    border-radius: 50%;
    margin-right: 12px;
    position: relative;
    cursor: pointer;
    flex-shrink: 0;
  }}
  .option input[type="radio"]:checked {{
    border-color: var(--amber);
  }}
  .option input[type="radio"]:checked::after {{
    content: '';
    position: absolute;
    top: 3px;
    left: 3px;
    width: 10px;
    height: 10px;
    border-radius: 50%;
    background: var(--amber);
  }}
  .option .opt-text {{ flex: 1; color: var(--text); }}
  .option .marker {{
    font-family: 'JetBrains Mono', monospace;
    font-size: 11px;
    margin-left: 10px;
  }}
  .option.show-correct .marker {{ color: var(--green); }}
  .option.show-incorrect .marker {{ color: var(--red); }}
  .feedback {{
    margin-top: 12px;
    padding: 10px 14px;
    border-radius: 4px;
    font-size: 0.9em;
    display: none;
  }}
  .feedback.show {{ display: block; }}
  .feedback.correct {{ background: rgba(125, 184, 125, 0.1); color: var(--green); border: 1px solid var(--green); }}
  .feedback.incorrect {{ background: rgba(212, 74, 74, 0.08); color: var(--red); border: 1px solid var(--red); }}

  .done {{
    text-align: center;
    padding: 60px 20px;
  }}
  .score-display {{
    font-family: 'Cormorant Garamond', serif;
    font-size: 5em;
    font-weight: 700;
    margin: 30px 0;
  }}
  .score-display.passed {{
    background: linear-gradient(135deg, var(--green) 0%, var(--cyan) 100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
  }}
  .score-display.failed {{ color: var(--red); }}
  .score-detail {{
    color: var(--text-dim);
    font-size: 16px;
    margin-bottom: 20px;
  }}
  .verdict {{
    display: inline-block;
    border: 1px solid;
    padding: 10px 24px;
    font-family: 'JetBrains Mono', monospace;
    font-size: 13px;
    letter-spacing: 2px;
    text-transform: uppercase;
    margin: 20px 0;
  }}
  .verdict.passed {{ border-color: var(--green); color: var(--green); }}
  .verdict.failed {{ border-color: var(--red); color: var(--red); }}

  .actionbar {{
    height: 72px;
    background: var(--concrete-2);
    border-top: 1px solid var(--border);
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 0 32px;
    flex-shrink: 0;
    gap: 16px;
  }}
  .actionbar .step-info {{
    color: var(--text-dim);
    font-family: 'JetBrains Mono', monospace;
    font-size: 12px;
    letter-spacing: 1px;
  }}
  .btn {{
    background: var(--concrete-3);
    border: 1px solid var(--border);
    color: var(--text);
    font-family: 'Inter', sans-serif;
    font-size: 14px;
    font-weight: 500;
    padding: 10px 22px;
    border-radius: 4px;
    cursor: pointer;
    transition: all 150ms;
    letter-spacing: 0.5px;
  }}
  .btn:hover:not(:disabled) {{
    border-color: var(--amber);
    color: var(--amber);
    background: var(--concrete);
  }}
  .btn:disabled {{ opacity: 0.4; cursor: not-allowed; }}
  .btn-primary {{
    background: var(--amber);
    color: var(--concrete);
    border-color: var(--amber);
    font-weight: 600;
  }}
  .btn-primary:hover:not(:disabled) {{
    background: var(--gold);
    color: var(--concrete);
  }}
  .btn-success {{
    background: var(--green);
    color: var(--concrete);
    border-color: var(--green);
    font-weight: 600;
  }}
  .btn-action-row {{ display: flex; gap: 10px; }}

  /* Narration controls */
  #narration-toolbar {{
    position: fixed;
    top: 64px;
    right: 12px;
    z-index: 100;
    display: flex;
    gap: 8px;
  }}
  #narration-toolbar button {{
    background: var(--concrete-2);
    border: 1px solid var(--border);
    color: var(--text);
    font-family: 'Inter', sans-serif;
    font-size: 12px;
    padding: 6px 12px;
    border-radius: 3px;
    cursor: pointer;
    transition: all 150ms;
    letter-spacing: 0.5px;
  }}
  #narration-toolbar button:hover {{
    background: var(--concrete-3);
    border-color: var(--amber);
    color: var(--amber);
  }}
  #narration-toolbar button.active {{
    background: var(--amber);
    color: var(--concrete);
    border-color: var(--amber);
    font-weight: 600;
  }}
  #narration-toolbar button.speaking {{
    background: var(--cyan);
    color: var(--concrete);
    border-color: var(--cyan);
    font-weight: 600;
    animation: pulse-cyan 1.5s ease-in-out infinite;
  }}
  @keyframes pulse-cyan {{
    0%, 100% {{ box-shadow: 0 0 0 0 rgba(95, 184, 212, 0.4); }}
    50% {{ box-shadow: 0 0 0 8px rgba(95, 184, 212, 0); }}
  }}

  #narration-panel {{
    position: fixed;
    bottom: 72px;
    left: 0;
    right: 0;
    background: #FAF7F0;
    border-top: 3px solid var(--amber);
    border-bottom: 1px solid var(--border);
    color: #1A1A1A;
    padding: 18px 36px 18px 36px;
    font-family: 'Inter', system-ui, sans-serif;
    font-size: 17px;
    line-height: 1.6;
    max-height: 38vh;
    overflow-y: auto;
    z-index: 99;
    box-shadow: 0 -8px 30px rgba(0,0,0,0.5), 0 -2px 20px rgba(232,184,106,0.15);
    display: none;
    animation: slideUpPanel 250ms ease-out;
  }}
  #narration-panel.open {{ display: block; }}
  @keyframes slideUpPanel {{
    from {{ transform: translateY(100%); opacity: 0; }}
    to {{ transform: translateY(0); opacity: 1; }}
  }}
  #narration-panel .label {{
    font-family: 'Inter', sans-serif;
    font-size: 11px;
    color: var(--amber-deep);
    letter-spacing: 2.5px;
    text-transform: uppercase;
    display: block;
    margin-bottom: 12px;
    font-weight: 700;
    border-bottom: 1px solid rgba(232, 184, 106, 0.4);
    padding-bottom: 8px;
  }}
  #narration-panel .content {{ color: #1A1A1A; font-weight: 400; }}
  #narration-panel .content p {{ margin: 0.6em 0; color: #1A1A1A; }}
  #narration-panel .content p:first-child {{ margin-top: 0; }}
  #narration-panel .content p:last-child {{ margin-bottom: 0; }}
  #narration-panel::-webkit-scrollbar {{ width: 10px; }}
  #narration-panel::-webkit-scrollbar-track {{ background: #EDE8DA; }}
  #narration-panel::-webkit-scrollbar-thumb {{ background: var(--amber-deep); border-radius: 5px; }}

  aside.tts, aside.notes {{ display: none; }}
</style>
</head>
<body>

<div class="topbar">
  <div class="brand">FINTEXA MARKETPLACE V2 · Curso Onboarding</div>
  <div class="progress">
    <span id="step-label">Paso 1 / 4</span>
    <div class="progress-bar"><div class="progress-fill" id="progress-fill" style="width:25%"></div></div>
  </div>
  <div class="scorm-status disconnected" id="scorm-status">SCORM: standalone</div>
</div>

<div id="narration-toolbar">
  <button id="btn-notes" title="Mostrar/ocultar narracion escrita">Notas</button>
  <button id="btn-tts" title="Reproducir narracion por audio (TTS)">Audio</button>
</div>
<div id="narration-panel">
  <span class="label">Narracion · seccion actual</span>
  <div class="content" id="narration-content"></div>
</div>

<div class="stage">
  <div class="container">

    <!-- COVER -->
    <section class="section cover active" data-step="cover">
      <div class="subtitle">Fintexa Marketplace V2 · Onboarding por Rol</div>
      <h1>Modulo Rol<br>{role_title}</h1>
      <div class="archetype">{role_archetype}</div>
      <div class="meta">
        <strong>1 slide rol + quiz 5 preguntas</strong> · ~5-7 min<br>
        Aprobacion: 70% (3 de 5 correctas)<br>
        {cover_meta}
      </div>
      <aside class="tts">
{tts_cover}
      </aside>
      <aside class="notes">
{notes_cover}
      </aside>
    </section>

    <!-- CONTENT SLIDE -->
    <section class="section content-slide" data-step="content">
      <div class="header-bar">
        <span class="deck-tag">FINTEXA MARKETPLACE V2 · ONBOARDING</span>
        <span class="slide-id">{slide_id} · Rol {role_title} · {slide_duration} lectura</span>
      </div>
      <h2>Rol {role_title} · {role_archetype}</h2>

{content_html}

      <aside class="tts">
{tts_content}
      </aside>
      <aside class="notes">
{notes_content}
      </aside>
    </section>

    <!-- QUIZ -->
    <section class="section quiz" data-step="quiz">
      <h2>Quiz · Verificacion del rol {role_title}</h2>
      <div class="quiz-intro">
        <p><strong>5 preguntas multi-choice.</strong> Aprobacion >= 70% (3 de 5 correctas). Una respuesta por pregunta. Cuando respondas las 5, click en <strong>"Verificar respuestas"</strong> para ver tu nota.</p>
      </div>

{quiz_html}

      <aside class="tts">
Cinco preguntas multi choice basadas en el slide del rol. Aprobacion al setenta porciento. Una respuesta por pregunta. Cuando respondas las cinco, click en verificar respuestas para ver tu nota.
      </aside>
      <aside class="notes">
El quiz tiene 5 preguntas multi-choice extraidas del contenido del slide del rol {role_title}. Cada pregunta tiene 4 opciones, una correcta.

Aprobacion mayor o igual a 70 por ciento — tres de cinco preguntas correctas.

Cuando clickees "Verificar respuestas", vas a ver inmediatamente cuales acertaste, cuales fallaste, y la respuesta correcta de cada una. Despues podes pasar al resultado y ver tu nota final.

Tip: si no estas seguro de alguna respuesta, podes volver a la seccion anterior con el boton "Anterior" abajo, repasar el contenido del slide, y volver al quiz. Tu progreso queda guardado.
      </aside>
    </section>

    <!-- DONE -->
    <section class="section done" data-step="done">
      <div class="subtitle">Resultado del modulo</div>
      <h1 id="done-title">Modulo completado</h1>
      <div class="score-display" id="score-display">--</div>
      <div class="score-detail" id="score-detail">Procesando...</div>
      <div class="verdict" id="verdict-badge">--</div>
      <div class="meta" style="margin-top: 40px; color: var(--text-dim); font-size: 14px;">
        Tu progreso fue reportado a Moodle (si esta corriendo dentro de un curso SCORM).<br>
        Podes cerrar esta ventana o repasar el contenido con el boton "Revisar" abajo.
      </div>
      <aside class="tts">
Modulo del rol {role_title} completado. Tu nota fue calculada y reportada al sistema. Podes cerrar la ventana o revisar el contenido.
      </aside>
      <aside class="notes">
Modulo terminado.

Tu nota fue calculada como porcentaje de respuestas correctas — multiplicado por veinte para llegar a la escala 0 a 100. Si estas en Moodle, el resultado se reporta al gradebook automaticamente y aparece en la seccion Calificaciones del curso.

La nota minima de aprobacion es 70 por ciento. Si no aprobaste, podes volver a hacer el quiz despues de revisar el contenido — el modulo registra el mejor intento.

Proximo paso recomendado: aplicar lo aprendido en tu jornada diaria del rol. Si todavia tenes dudas, hablar con tu Tech Lead, PM o Capital Humano.
      </aside>
    </section>

  </div>
</div>

<div class="actionbar">
  <div class="step-info" id="step-info">Cover · Modulo Rol {role_title}</div>
  <div class="btn-action-row">
    <button class="btn" id="btn-prev" onclick="goPrev()" disabled>&larr; Anterior</button>
    <button class="btn btn-primary" id="btn-next" onclick="goNext()">Comenzar &rarr;</button>
  </div>
</div>

<script>
// ── SCORM 1.2 wrapper ─────────────────────────────────────────────────
const SCORM = {{
  api: null,
  initialized: false,
  startTime: Date.now(),
  findAPI() {{
    let win = window;
    let depth = 0;
    while (win && depth < 10) {{
      if (win.API) return win.API;
      try {{ if (win.parent !== win) win = win.parent; else break; }}
      catch (e) {{ break; }}
      depth++;
    }}
    try {{ if (window.opener && window.opener.API) return window.opener.API; }} catch (e) {{}}
    return null;
  }},
  init() {{
    this.api = this.findAPI();
    if (!this.api) {{ console.info('[SCORM] standalone mode'); return false; }}
    try {{
      const ok = this.api.LMSInitialize('');
      this.initialized = (ok === 'true' || ok === true);
      if (this.initialized) {{
        this.api.LMSSetValue('cmi.core.lesson_status', 'incomplete');
        this.commit();
      }}
      return this.initialized;
    }} catch (e) {{ console.warn('[SCORM] init error:', e); return false; }}
  }},
  setStatus(status) {{ if (!this.initialized) return; try {{ this.api.LMSSetValue('cmi.core.lesson_status', status); this.commit(); }} catch (e) {{}} }},
  setScore(score) {{
    if (!this.initialized) return;
    try {{
      this.api.LMSSetValue('cmi.core.score.raw', String(Math.round(score)));
      this.api.LMSSetValue('cmi.core.score.min', '0');
      this.api.LMSSetValue('cmi.core.score.max', '100');
      this.commit();
    }} catch (e) {{}}
  }},
  setSessionTime() {{
    if (!this.initialized) return;
    try {{
      const seconds = Math.floor((Date.now() - this.startTime) / 1000);
      const h = Math.floor(seconds / 3600);
      const m = Math.floor((seconds % 3600) / 60);
      const s = seconds % 60;
      const time = String(h).padStart(4, '0') + ':' + String(m).padStart(2, '0') + ':' + String(s).padStart(2, '0');
      this.api.LMSSetValue('cmi.core.session_time', time);
      this.commit();
    }} catch (e) {{}}
  }},
  commit() {{ if (this.initialized) {{ try {{ this.api.LMSCommit(''); }} catch (e) {{}} }} }},
  finish() {{ if (this.initialized) {{ try {{ this.setSessionTime(); this.commit(); this.api.LMSFinish(''); }} catch (e) {{}} }} }}
}};

const STEPS = ['cover', 'content', 'quiz', 'done'];
const STEP_LABELS = {{
  'cover': 'Cover · Modulo Rol {role_title}',
  'content': 'Contenido · Slide rol {slide_id}',
  'quiz': 'Quiz · 5 preguntas multi-choice',
  'done': 'Resultado · Score + reporte SCORM'
}};
let currentStep = 0;
let quizSubmitted = false;
let quizScore = 0;

function goToStep(idx) {{
  if (idx < 0 || idx >= STEPS.length) return;
  document.querySelectorAll('.section').forEach(s => s.classList.remove('active'));
  document.querySelector(`.section[data-step="${{STEPS[idx]}}"]`).classList.add('active');
  currentStep = idx;
  updateNav();
  document.querySelector('.stage').scrollTop = 0;
}}

function updateNav() {{
  const stepName = STEPS[currentStep];
  document.getElementById('step-label').textContent = `Paso ${{currentStep + 1}} / ${{STEPS.length}}`;
  document.getElementById('step-info').textContent = STEP_LABELS[stepName] || '';
  document.getElementById('progress-fill').style.width = (((currentStep + 1) / STEPS.length) * 100) + '%';
  const btnPrev = document.getElementById('btn-prev');
  const btnNext = document.getElementById('btn-next');
  btnPrev.disabled = (currentStep === 0);
  if (stepName === 'cover') {{
    btnNext.textContent = 'Comenzar →'; btnNext.disabled = false; btnNext.className = 'btn btn-primary';
  }} else if (stepName === 'content') {{
    btnNext.textContent = 'Ir al quiz →'; btnNext.disabled = false; btnNext.className = 'btn btn-primary';
  }} else if (stepName === 'quiz') {{
    if (quizSubmitted) {{
      btnNext.textContent = 'Ver resultado →'; btnNext.disabled = false; btnNext.className = 'btn btn-primary';
    }} else {{
      btnNext.textContent = 'Verificar respuestas'; btnNext.disabled = !areAllQuestionsAnswered(); btnNext.className = 'btn btn-success';
    }}
  }} else if (stepName === 'done') {{
    btnNext.textContent = 'Revisar contenido'; btnNext.disabled = false; btnNext.className = 'btn';
  }}
}}

function goPrev() {{ if (currentStep > 0) goToStep(currentStep - 1); }}
function goNext() {{
  const stepName = STEPS[currentStep];
  if (stepName === 'quiz' && !quizSubmitted) {{ submitQuiz(); return; }}
  if (stepName === 'done') {{ goToStep(1); return; }}
  if (currentStep < STEPS.length - 1) goToStep(currentStep + 1);
}}

function areAllQuestionsAnswered() {{
  for (let i = 1; i <= 5; i++) {{
    const checked = document.querySelector(`input[name="q${{i}}"]:checked`);
    if (!checked) return false;
  }}
  return true;
}}

document.addEventListener('change', (e) => {{
  if (e.target.matches('input[type="radio"]')) {{
    const q = e.target.closest('.question');
    if (q) q.classList.add('answered');
    q.querySelectorAll('.option').forEach(o => o.classList.remove('selected'));
    e.target.closest('.option').classList.add('selected');
    updateNav();
  }}
}});

function submitQuiz() {{
  let correct = 0;
  document.querySelectorAll('.question').forEach((q) => {{
    const correctAnswer = q.dataset.correct;
    const checked = q.querySelector('input[type="radio"]:checked');
    const userAnswer = checked ? checked.value : null;
    const isCorrect = (userAnswer === correctAnswer);
    if (isCorrect) correct++;
    q.querySelectorAll('.option').forEach(opt => {{
      const radio = opt.querySelector('input[type="radio"]');
      const marker = opt.querySelector('.marker');
      opt.classList.add('disabled');
      radio.disabled = true;
      if (radio.value === correctAnswer) {{
        opt.classList.add('show-correct');
        marker.textContent = '✓ correcta';
      }} else if (radio.value === userAnswer && !isCorrect) {{
        opt.classList.add('show-incorrect');
        marker.textContent = '✗ tu respuesta';
      }}
    }});
    const fb = q.querySelector('.feedback');
    if (isCorrect) {{
      q.classList.add('correct');
      fb.className = 'feedback correct show';
      fb.textContent = '✓ Correcto.';
    }} else {{
      q.classList.add('incorrect');
      fb.className = 'feedback incorrect show';
      fb.textContent = '✗ Tu respuesta no es la correcta. Repasa la seccion del slide.';
    }}
  }});
  quizScore = Math.round((correct / 5) * 100);
  quizSubmitted = true;
  const passed = quizScore >= 70;
  const scoreEl = document.getElementById('score-display');
  scoreEl.textContent = quizScore;
  scoreEl.className = 'score-display ' + (passed ? 'passed' : 'failed');
  document.getElementById('score-detail').textContent = correct + ' de 5 respuestas correctas (' + quizScore + '%) — aprobacion >= 70%';
  const verdictEl = document.getElementById('verdict-badge');
  verdictEl.textContent = passed ? '✓ Aprobado' : '✗ No aprobado · revisar contenido';
  verdictEl.className = 'verdict ' + (passed ? 'passed' : 'failed');
  document.getElementById('done-title').textContent = passed ? 'Modulo completado' : 'Modulo no aprobado';
  SCORM.setScore(quizScore);
  SCORM.setStatus(passed ? 'passed' : 'failed');
  SCORM.setSessionTime();
  SCORM.commit();
  updateNav();
}}

// ── Narration TTS + Notes panel ───────────────────────────────────────
const NARRATION = {{
  btnTts: null, btnNotes: null, panel: null, contentEl: null,
  currentUtterance: null, panelOpen: false, speaking: false,
  init() {{
    this.btnTts = document.getElementById('btn-tts');
    this.btnNotes = document.getElementById('btn-notes');
    this.panel = document.getElementById('narration-panel');
    this.contentEl = document.getElementById('narration-content');
    this.btnTts.addEventListener('click', () => {{ if (this.speaking) this.stopTts(); else this.speakCurrent(); }});
    this.btnNotes.addEventListener('click', () => this.togglePanel());
    if ('speechSynthesis' in window) {{
      window.speechSynthesis.onvoiceschanged = () => {{}};
      window.speechSynthesis.getVoices();
    }}
    window.addEventListener('beforeunload', () => this.stopTts());
  }},
  getCurrentSection() {{ return document.querySelector('.section.active'); }},
  getNotesText() {{
    const sect = this.getCurrentSection();
    if (!sect) return '';
    const notes = sect.querySelector('aside.notes');
    return notes ? notes.textContent.trim() : '';
  }},
  getTtsText() {{
    const sect = this.getCurrentSection();
    if (!sect) return '';
    const ttsEl = sect.querySelector('aside.tts');
    const fallback = sect.querySelector('aside.notes');
    const raw = ttsEl ? ttsEl.textContent.trim() : (fallback ? fallback.textContent.trim() : '');
    return this.sanitizeForTTS(raw);
  }},
  sanitizeForTTS(text) {{
    if (!text) return '';
    return text
      .replace(/\/([a-zA-Z][\w-]*)/g, 'comando $1')
      .replace(/\s--([a-zA-Z][\w-]*)/g, ' modo $1')
      .replace(/\{{([^}}]+)\}}/g, '$1')
      .replace(/>=/g, ' mayor o igual a ')
      .replace(/<=/g, ' menor o igual a ')
      .replace(/(\s)>(\s|\d)/g, '$1mayor que $2')
      .replace(/(\s)<(\s|\d)/g, '$1menor que $2')
      .replace(/[\\\/]/g, ' ')
      .replace(/[(){{}}\[\]]/g, ' ')
      .replace(/[#@$%^&*+|~`=_]/g, ' ')
      .replace(/[\u{{1F534}}⚠\u{{1F512}}✓✗⏸▶\u{{1F4DD}}\u{{1F50A}}⚪\u{{1F7E2}}\u{{1F7E1}}\u{{1F7E0}}\u{{1F195}}]/gu, '')
      .replace(/-?->/g, ' a ')
      .replace(/<-?-/g, ' desde ')
      .replace(/\s+/g, ' ')
      .trim();
  }},
  getNotesHTML() {{
    const text = this.getNotesText();
    if (!text) return '<em style="color:#8A9098">Esta seccion no tiene narracion adicional.</em>';
    return text.split(/\n\s*\n/).map(p => `<p>${{p.replace(/\n/g, ' ').trim()}}</p>`).join('');
  }},
  updatePanelLabel() {{
    const sect = this.getCurrentSection();
    const label = this.panel.querySelector('.label');
    let title = 'Narracion · seccion actual';
    if (sect) {{
      const stepName = sect.dataset.step || '';
      const stepLabel = STEP_LABELS[stepName] || stepName;
      title = 'Narracion · ' + stepLabel;
    }}
    if (label) label.textContent = title;
  }},
  updatePanelContent() {{
    this.updatePanelLabel();
    this.contentEl.innerHTML = this.getNotesHTML();
  }},
  togglePanel() {{
    this.panelOpen = !this.panelOpen;
    if (this.panelOpen) {{
      this.updatePanelContent();
      this.panel.classList.add('open');
      this.btnNotes.classList.add('active');
      this.btnNotes.textContent = 'Cerrar';
    }} else {{
      this.panel.classList.remove('open');
      this.btnNotes.classList.remove('active');
      this.btnNotes.textContent = 'Notas';
    }}
  }},
  stopTts() {{
    if (window.speechSynthesis) window.speechSynthesis.cancel();
    this.speaking = false;
    if (this.btnTts) {{ this.btnTts.classList.remove('speaking'); this.btnTts.textContent = 'Audio'; }}
  }},
  speakCurrent() {{
    if (!('speechSynthesis' in window)) {{ alert('Tu browser no soporta Web Speech API. Usa el boton Notas.'); return; }}
    const text = this.getTtsText();
    if (!text) {{ alert('Esta seccion no tiene narracion para reproducir.'); return; }}
    this.stopTts();
    this.currentUtterance = new SpeechSynthesisUtterance(text);
    const voices = window.speechSynthesis.getVoices();
    const esVoice = voices.find(v => /es[_-]?(AR|ES|MX|US|CL|419)/i.test(v.lang)) || voices.find(v => v.lang.startsWith('es'));
    if (esVoice) this.currentUtterance.voice = esVoice;
    this.currentUtterance.lang = esVoice ? esVoice.lang : 'es-AR';
    this.currentUtterance.rate = 1.25;
    this.currentUtterance.pitch = 1.0;
    this.currentUtterance.onend = () => {{ this.speaking = false; this.btnTts.classList.remove('speaking'); this.btnTts.textContent = 'Audio'; }};
    this.currentUtterance.onerror = () => {{ this.speaking = false; this.btnTts.classList.remove('speaking'); this.btnTts.textContent = 'Audio'; }};
    window.speechSynthesis.speak(this.currentUtterance);
    this.speaking = true;
    this.btnTts.classList.add('speaking');
    this.btnTts.textContent = 'Detener';
  }},
  onSectionChanged() {{
    this.stopTts();
    if (this.panelOpen) this.updatePanelContent();
  }}
}};

const _origGoToStep = goToStep;
goToStep = function(idx) {{ _origGoToStep(idx); if (NARRATION) NARRATION.onSectionChanged(); }};

document.addEventListener('DOMContentLoaded', () => {{ SCORM.init(); NARRATION.init(); updateNav(); }});
window.addEventListener('beforeunload', () => SCORM.finish());
document.addEventListener('keydown', (e) => {{
  if (e.target.matches('input, textarea')) return;
  if (e.key === 'ArrowRight' && !document.getElementById('btn-next').disabled) goNext();
  else if (e.key === 'ArrowLeft' && !document.getElementById('btn-prev').disabled) goPrev();
}});
</script>
</body>
</html>
"""

# ─────────────────────────────────────────────────────────────────────
# imsmanifest.xml SCORM 1.2 template
# ─────────────────────────────────────────────────────────────────────

MANIFEST_TEMPLATE = """<?xml version="1.0" standalone="no" ?>
<manifest identifier="com.fintexa.curso.{manifest_id}"
          version="1.0"
          xmlns="http://www.imsproject.org/xsd/imscp_rootv1p1p2"
          xmlns:adlcp="http://www.adlnet.org/xsd/adlcp_rootv1p2"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xsi:schemaLocation="http://www.imsproject.org/xsd/imscp_rootv1p1p2 imscp_rootv1p1p2.xsd
                              http://www.adlnet.org/xsd/adlcp_rootv1p2 adlcp_rootv1p2.xsd">
  <metadata>
    <schema>ADL SCORM</schema>
    <schemaversion>1.2</schemaversion>
  </metadata>
  <organizations default="ORG-FINTEXA-{slug_upper}">
    <organization identifier="ORG-FINTEXA-{slug_upper}">
      <title>{manifest_title}</title>
      <item identifier="ITEM-MOD-{slug_upper}" identifierref="RES-{slug_upper}" isvisible="true">
        <title>{manifest_subtitle}</title>
        <adlcp:masteryscore>70</adlcp:masteryscore>
      </item>
    </organization>
  </organizations>
  <resources>
    <resource identifier="RES-{slug_upper}" type="webcontent" adlcp:scormtype="sco" href="index.html">
      <file href="index.html"/>
    </resource>
  </resources>
</manifest>
"""

# ─────────────────────────────────────────────────────────────────────
# Helper: render quiz HTML from list of questions
# ─────────────────────────────────────────────────────────────────────

def render_quiz_html(questions):
    """questions: list of dicts {q: text, options: {a/b/c/d}, correct: 'a'}"""
    parts = []
    for i, q in enumerate(questions, start=1):
        opts = q['options']
        opts_html = []
        for letter in ['a', 'b', 'c', 'd']:
            text = opts[letter]
            opts_html.append(
                f'<label class="option"><input type="radio" name="q{i}" value="{letter}">'
                f'<span class="opt-text">{text}</span><span class="marker"></span></label>'
            )
        parts.append(f'''
      <div class="question" data-q="{i}" data-correct="{q['correct']}">
        <div class="q-num">PREGUNTA {i} / 5</div>
        <div class="q-text">{q['q']}</div>
        <div class="options">
          {''.join(opts_html)}
        </div>
        <div class="feedback"></div>
      </div>''')
    return '\n'.join(parts)

# ─────────────────────────────────────────────────────────────────────
# 14 ROLE CONFIGS
# ─────────────────────────────────────────────────────────────────────

ROLES = [
    # ───────────── O04 CEO/CTO/C-Level ─────────────
    {
        'folder': '01-ceo-cto',
        'slug': 'ceo-cto',
        'role_title': 'CEO / CTO / C-Level',
        'role_archetype': 'El Hiper-Lider',
        'slide_id': 'O04',
        'slide_duration': '2 min',
        'cover_meta': 'Decisiones estrategicas · governance · visibilidad portfolio 135+ servicios',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Decisiones estrategicas + governance + visibilidad portfolio (135+ microservicios). Audit trail firmado para defender ante el Board.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp version                       validar marketplace + alcance
2.  /fguide soy PM                       coach modo ejecutivo + flujo board ready
3.  /fwarroom --preset=board {*}         simular decision multi-stakeholder</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Board Review Ejecutivo
  /fwarroom --preset=board {detalle >20 lineas} -> /fdebate-ia {md}

Auditoria Portfolio Tecnico
  /fanalyze --mode portfolio {md} -> /freview --full

Decision Arquitectonica Cross-Team
  /fanalyze REFACTOR {path} -> /fwarroom council -> /fdebate-ia -> /fsign weight 900</code></pre>

      <div class="callout-pci">
        Cualquier skip de regla weight 900 o mayor requiere firma digital — diez anios de retencion BCRA.
      </div>

      <h3>Concepto clave</h3>
      <div class="callout">
        <em>"Si sos CEO o CTO, el marketplace te da tres cosas: visibilidad agregada del portfolio sin meterte en codigo, simulacion multi-stakeholder antes de decidir, y audit trail firmado para defender la decision ante el Board."</em>
      </div>''',
        'tts_cover': 'Modulo del rol CEO o CTO. Cinco a siete minutos. Decisiones estrategicas con visibilidad de portfolio.',
        'notes_cover': 'Bienvenido al modulo del rol CEO o CTO del curso Fintexa Marketplace V2.\n\nEn este modulo vas a recorrer la presentacion del rol del C-Level, validar tu comprension con un quiz corto de 5 preguntas, y al final ver tu nota.',
        'tts_content': 'CEO o CTO, el marketplace te da tres cosas. Visibilidad agregada del portfolio sin meterte en codigo. Simulacion multi-stakeholder antes de decidir. Audit trail firmado para defender la decision ante el Board. La regla critica: cualquier skip de regla weight 900 o mas requiere firma digital. Diez anios de retencion BCRA.',
        'notes_content': 'El rol CEO o CTO es el Hiper-Lider del marketplace. Toma decisiones estrategicas con visibilidad agregada del portfolio.\n\nTres comandos universales: fhelp version, fguide soy PM con coach ejecutivo, y fwarroom preset board para simular decisiones multi-stakeholder antes de comprometer recursos.\n\nFlujos diarios principales: Board Review Ejecutivo combinando fwarroom y fdebate-ia. Auditoria Portfolio Tecnico con fanalyze mode portfolio y freview full. Decision Arquitectonica Cross-Team con cuatro pasos firmados.\n\nRegla inviolable: el marketplace tiene weights desde 400 hasta 9999. Las que tienen 900 o mas requieren firma digital con fsign — eso es lo que se retiene 10 anios para BCRA. Sin firma, no pasa el commit. Esa es la cadena que defiende decisiones cross-team ante auditorias.',
        'quiz': [
            {'q': 'Cual es el primer comando para validar el marketplace y alcance?', 'options': {'a': '/fdev DAD-X', 'b': '/fhelp version', 'c': '/fwarroom council', 'd': '/fanalyze portfolio'}, 'correct': 'b'},
            {'q': 'Que preset de fwarroom usa el CEO o CTO para simular decision multi-stakeholder?', 'options': {'a': '--preset=board', 'b': '--preset=architecture', 'c': '--preset=incident', 'd': '--preset=sprint'}, 'correct': 'a'},
            {'q': 'Para audit trail BCRA de decisiones arquitectonicas, que comando firma con weight obligatorio?', 'options': {'a': '/fsign --weight 400', 'b': '/fsign --weight 700', 'c': '/fsign --weight 900', 'd': '/fsign --weight 100'}, 'correct': 'c'},
            {'q': 'Cuantos anios de retencion exige BCRA para audit trail de decisiones financieras?', 'options': {'a': '3 anios', 'b': '5 anios', 'c': '7 anios', 'd': '10 anios'}, 'correct': 'd'},
            {'q': 'Cual es el alcance del portfolio que ve un CEO o CTO en el marketplace?', 'options': {'a': '20+ microservicios', 'b': '50+ microservicios', 'c': '135+ microservicios', 'd': '500+ microservicios'}, 'correct': 'c'},
        ],
    },
    # ───────────── O05 CFO ─────────────
    {
        'folder': '02-cfo',
        'slug': 'cfo',
        'role_title': 'CFO',
        'role_archetype': 'El Estratega de Recursos',
        'slide_id': 'O05',
        'slide_duration': '2 min',
        'cover_meta': 'Cost of Quality · ROI marketplace · KPIs adopcion cuantitativos',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Cost of Quality + ROI + metricas de adopcion. Mide drift score, coverage, retrabajo evitado.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp version                          14 plugins disponibles
2.  /fguide soy CFO --coach=desempeno       coach desempeno + KPIs cuantitativos
3.  /fchangelog --format PO                 release notes audiencia ejecutiva</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Cost of Quality Review
  /fsqa cierre -> /fchangelog --format PO xN -> /fwarroom council {board}

Cierre Multi-Audiencia (post-release)
  /fchangelog DAD-XXXX --format all   (-60% tiempo vs 3 invocaciones separadas)</code></pre>

      <h3>KPIs adopcion medibles</h3>
      <pre><code>Commands/usuario/dia    >=5
Flujos/dia              >1
Time-to-first-command   <30 min
ROI cuantificable       drift evitado + retrabajo evitado</code></pre>

      <div class="callout-warn">
        NUNCA tres invocaciones separadas de /fchangelog para PO, QA y Dev. Una sola con --format all reduce 60 por ciento el tiempo de cierre.
      </div>''',
        'tts_cover': 'Modulo del rol CFO. Cinco a siete minutos. Cost of Quality y ROI marketplace.',
        'notes_cover': 'Bienvenido al modulo del rol CFO del curso Fintexa Marketplace V2.\n\nEn este modulo vas a entender como el marketplace genera metricas cuantificables de calidad y adopcion. Validacion con quiz de 5 preguntas.',
        'tts_content': 'Para CFO, el marketplace es una herramienta de medicion. Cost of Quality dejo de ser un slide subjetivo. Hay metricas cuantificables: drift score, coverage, retrabajo evitado. La regla operativa: nunca tres invocaciones separadas de fchangelog para PO, QA y Dev. Una sola con modo format all reduce sesenta por ciento el tiempo de cierre.',
        'notes_content': 'El rol CFO es el Estratega de Recursos del marketplace. Mide ROI y Cost of Quality con metricas cuantificables.\n\nTres comandos universales para iniciar: fhelp version para ver los 14 plugins disponibles, fguide soy CFO con coach desempeno y KPIs cuantitativos, y fchangelog format PO que genera release notes para audiencia ejecutiva.\n\nFlujos diarios: Cost of Quality Review combinando fsqa cierre con fchangelog format PO repetido y fwarroom council para Board. Cierre Multi-Audiencia post-release con fchangelog format all que genera PO, QA y DEV en una sola invocacion — 60 porciento menos tiempo.\n\nKPIs medibles: 5 comandos por usuario por dia minimo, mas de 1 flujo completado por dia, time to first command menor a 30 minutos, y ROI cuantificable como drift evitado mas retrabajo evitado.',
        'quiz': [
            {'q': 'Que flag de /fchangelog reduce 60% el tiempo de cierre multi-audiencia?', 'options': {'a': '--format PO', 'b': '--format QA', 'c': '--format all', 'd': '--format dev'}, 'correct': 'c'},
            {'q': 'Que KPI mide el tiempo desde inicio sesion hasta el primer comando productivo?', 'options': {'a': 'TTFC (Time to First Command)', 'b': 'TTM (Time to Market)', 'c': 'TTL (Time to Live)', 'd': 'TTV (Time to Value)'}, 'correct': 'a'},
            {'q': 'Que coach mode usa CFO para enfocar en KPIs cuantitativos?', 'options': {'a': 'ontologico', 'b': 'desempeno', 'c': 'ejecutivo', 'd': 'hibrido'}, 'correct': 'b'},
            {'q': 'Cual es el KPI minimo de comandos por usuario por dia?', 'options': {'a': '>=2', 'b': '>=3', 'c': '>=5', 'd': '>=10'}, 'correct': 'c'},
            {'q': 'Que genera /fchangelog --format PO?', 'options': {'a': 'casos de prueba para QA', 'b': 'release notes audiencia ejecutiva', 'c': 'spec tecnica para Dev', 'd': 'audit log para BCRA'}, 'correct': 'b'},
        ],
    },
    # ───────────── O06 CISO/SecOps ─────────────
    {
        'folder': '03-ciso',
        'slug': 'ciso',
        'role_title': 'CISO / SecOps',
        'role_archetype': 'El Juez de Hierro',
        'slide_id': 'O06',
        'slide_duration': '2 min',
        'cover_meta': 'PCI DSS v4 attestation · OWASP · threat modeling · incident response',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>PCI DSS v4 attestation + OWASP Top 10 + threat modeling + incident response. Compliance gravitas inmutable.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp version            verificar reglas PCI activas
2.  /fguide soy CISO PCI      coach flujo compliance
3.  /fd-secscan               scan Trivy + SonarQube + CVE</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>PCI DSS v4 Attestation
  /fd-secscan -> /freview --full -> /fsign sign --weight 900

SecOps Incident Response (por evento)
  /fd-incident p0 -> /freview --CRITICAL -> postmortem PCI Req 12.10.1-6

Evidence (QSA externo)
  /fanalyze --mode compliance {md} -> /freview --full xN</code></pre>

      <div class="callout-pci">
        Regla critica: PCI inviolable peso 9999 — cualquier sospecha de PAN o CVV en queries es STOP inmediato.
      </div>
      <div class="callout-warn">
        P0 pagos down >2min: notificacion BCRA en menos de 30 min (DEVOPS-03 + Com. "A" 7724).
      </div>''',
        'tts_cover': 'Modulo del rol CISO o SecOps. Compliance PCI DSS v4 y respuesta a incidentes.',
        'notes_cover': 'Bienvenido al modulo del rol CISO o SecOps. Compliance gravitas inmutable. Validacion con 5 preguntas sobre PCI DSS y incident response.',
        'tts_content': 'Para CISO, dos reglas. PCI inviolable peso nueve mil novecientos noventa y nueve. Cualquier sospecha de PAN o CVV en queries es STOP inmediato. Y P cero pagos caidos por mas de dos minutos. Notificacion BCRA en menos de treinta minutos. No negociable. El postmortem incluye los seis campos PCI DSS doce punto diez. Diez anios de retencion.',
        'notes_content': 'El rol CISO es el Juez de Hierro del marketplace. Compliance PCI DSS v4, OWASP Top 10, threat modeling, e incident response.\n\nTres comandos para iniciar: fhelp version para verificar reglas PCI activas. fguide soy CISO con foco PCI. fd-secscan que ejecuta Trivy + SonarQube + analisis CVE.\n\nFlujos diarios: PCI DSS Attestation con fd-secscan, freview full, y fsign sign weight 900. Incident Response P0 con fd-incident, freview CRITICAL, y postmortem PCI Requisito 12.10. Evidence para QSA externo con fanalyze mode compliance y freview full repetido.\n\nReglas inviolables: PCI weight 9999 — la mas alta del marketplace. Sospecha de PAN o CVV en queries es STOP inmediato. P0 pagos caidos mas de 2 minutos requiere notificacion BCRA en menos de 30 minutos por Comunicacion A 7724. Postmortem cubre los 6 campos PCI DSS Req 12.10.1 hasta 12.10.6.',
        'quiz': [
            {'q': 'Que peso tiene la regla PCI inviolable en el marketplace?', 'options': {'a': '400', 'b': '900', 'c': '1000', 'd': '9999'}, 'correct': 'd'},
            {'q': 'P0 pagos down mas de 2 minutos: en cuanto tiempo se notifica a BCRA?', 'options': {'a': 'Menos de 15 min', 'b': 'Menos de 30 min', 'c': 'Menos de 60 min', 'd': 'Menos de 24 horas'}, 'correct': 'b'},
            {'q': 'Cual es el flujo correcto de PCI DSS v4 Attestation?', 'options': {'a': '/fdev a /fdrift a /freview', 'b': '/fd-secscan a /freview --full a /fsign sign --weight 900', 'c': '/freq a /fspec a /fdev', 'd': '/fhelp a /fguide a /fanalyze'}, 'correct': 'b'},
            {'q': 'Que requisito PCI DSS cubre el postmortem de incidente?', 'options': {'a': '3.2.1-6', 'b': '8.1-8.6', 'c': '12.10.1-6', 'd': '4.2-4.8'}, 'correct': 'c'},
            {'q': 'Que herramientas combina /fd-secscan?', 'options': {'a': 'kubectl + helm + ArgoCD', 'b': 'Trivy + SonarQube + analisis CVE', 'c': 'Jenkins + Nexus + Nessus', 'd': 'Burp Suite + Wireshark + Metasploit'}, 'correct': 'b'},
        ],
    },
    # ───────────── O07 PM/Delivery ─────────────
    {
        'folder': '04-pm',
        'slug': 'pm',
        'role_title': 'PM / Delivery Manager',
        'role_archetype': 'El Sintetizador',
        'slide_id': 'O07',
        'slide_duration': '2 min',
        'cover_meta': 'Sprint planning · DoR gates · drift check obligatorio · cierre formal',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Sprint planning + DoR + daily + drift check + trazabilidad cross-equipo.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp pipeline                  flow E2E
2.  /fticket DAD-X --mode completo   ticket enriquecido
3.  /fsqa DoR DAD-X                  primer gate DoR</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Sprint Planning + DoR
  /fticket medio -> /fsqa DoR -> /freq --auto -> /fspec

Daily + Drift Check
  /fticket {tid} + /fdrift

Sprint Review
  /fchangelog --format all -> /frelease xN -> /fretro --full</code></pre>

      <div class="callout-warn">
        /fdrift es OBLIGATORIO entre /fforensic y /freview. Si score baja de 80, el feature NO avanza.
      </div>''',
        'tts_cover': 'Modulo del rol PM o Delivery Manager. Sprint planning, DoR gates y drift check.',
        'notes_cover': 'Bienvenido al modulo del rol PM. Orquestacion de sprint con gates DoR y drift obligatorio. Quiz de 5 preguntas al final.',
        'tts_content': 'PM tiene una tarea simple y una compleja. La simple: gates DoR antes de estimar. Cinco de nivel uno y diez de nivel dos, no se discuten. La compleja: cuando un dev pregunta puedo saltar el comando fdrift, la respuesta siempre es no. El drift es gate obligatorio entre forensic y review. Si el score baja de ochenta, el feature no avanza.',
        'notes_content': 'El rol PM Delivery Manager es el Sintetizador del marketplace. Orquesta sprint, DoR, daily, y drift check.\n\nTres comandos para iniciar: fhelp pipeline para ver el flujo end-to-end completo, fticket DAD-X mode completo para enriquecer ticket, y fsqa DoR para el primer gate Definition of Ready.\n\nFlujos diarios: Sprint Planning con fticket medio, fsqa DoR (5 gates nivel 1 + 10 nivel 2), freq auto y fspec. Daily check con fticket y fdrift. Sprint Review con fchangelog format all, frelease repetido, y fretro full.\n\nRegla critica: fdrift es obligatorio entre fforensic y freview. Sin excepciones. Si el drift score baja de 80, el feature no avanza al merge — se detiene hasta que el dev haga fdev fix con los DRIFT-IDs especificos.',
        'quiz': [
            {'q': 'Cuantos gates DoR hay en total entre nivel 1 y nivel 2?', 'options': {'a': '5 totales', 'b': '10 totales', 'c': '15 totales', 'd': '20 totales'}, 'correct': 'c'},
            {'q': 'Entre que dos comandos es OBLIGATORIO ejecutar /fdrift?', 'options': {'a': '/fdev y /fforensic', 'b': '/fforensic y /freview', 'c': '/freq y /fspec', 'd': '/freview y /fchangelog'}, 'correct': 'b'},
            {'q': 'Si el drift score baja de cuanto el feature NO avanza al merge?', 'options': {'a': '60', 'b': '70', 'c': '80', 'd': '90'}, 'correct': 'c'},
            {'q': 'Para Sprint Review cierre, que comando consolida changelog multi-audiencia?', 'options': {'a': '/fchangelog --format PO', 'b': '/fchangelog --format dev', 'c': '/fchangelog --format all', 'd': '/fchangelog --format QA'}, 'correct': 'c'},
            {'q': 'Cual es el flujo correcto de Sprint Planning?', 'options': {'a': '/fdev a /freview a /fchangelog', 'b': '/fticket medio a /fsqa DoR a /freq --auto a /fspec', 'c': '/fhelp a /fguide a /fcompact', 'd': '/fscan a /fdoc a /ftrace'}, 'correct': 'b'},
        ],
    },
    # ───────────── O08 Product Owner ─────────────
    {
        'folder': '05-po',
        'slug': 'po',
        'role_title': 'Product Owner',
        'role_archetype': 'El Diplomatico del Vacio',
        'slide_id': 'O08',
        'slide_duration': '2 min',
        'cover_meta': 'Grooming · criterios aceptacion · changelog multi-audiencia · stakeholder',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Grooming + criterios aceptacion + changelog multi-audiencia + comunicacion stakeholder.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fticket DAD-X completo        ticket enriquecido + contexto
2.  /freq DAD-X refinamiento       entrevista interactiva criterios
3.  /fchangelog --format PO        release notes audiencia ejecutiva</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Grooming + Changelog PO
  /fticket completo -> /freq -> /fchangelog --format PO

Iteracion Spec (cambios scope)
  /fspec --iteration v1 v2

Cierre Multi-Audiencia (post-release)
  /fchangelog DAD-XXXX --format all</code></pre>

      <div class="callout-warn">
        NUNCA /fspec para modificar spec existente version 2 o mas. Usar --iteration para iterar (BP-21).
      </div>
      <div class="callout">
        <em>Tip persona-detection: confiar en el ajuste automatico de tono — el skill detecta audiencia (Board/Tech/Customer), no hay que configurarlo manual (BP-57).</em>
      </div>''',
        'tts_cover': 'Modulo del rol Product Owner. Grooming, criterios aceptacion y changelog multi-audiencia.',
        'notes_cover': 'Bienvenido al modulo del rol PO. Diplomatico del Vacio que adapta tono a la audiencia. Quiz de 5 preguntas.',
        'tts_content': 'Para PO hay tres atajos no obvios. Primero: cuando comando freq pregunta A B C, podes responder en prosa libre con decision mas fundamento, no solo si. Segundo: nunca uses comando fspec para iterar una spec existente, esta hecho para crear nuevas. Usa modo iteration. Tercero: confia en el persona-detection automatico. El skill ajusta el tono solo, no hay que configurarlo.',
        'notes_content': 'El rol Product Owner es el Diplomatico del Vacio. Adapta tono y comunicacion segun audiencia: Board, Tech, Customer.\n\nTres comandos para iniciar: fticket DAD-X completo para enriquecer ticket, freq DAD-X para refinamiento de criterios de aceptacion, y fchangelog format PO para release notes ejecutivas.\n\nFlujos diarios: Grooming combinando fticket, freq y fchangelog format PO. Iteracion Spec con fspec mode iteration cuando hay cambios de scope. Cierre Multi-Audiencia post-release con fchangelog format all que genera versiones PO, QA y DEV en una sola invocacion.\n\nReglas criticas: BP-21 dice nunca uses fspec para modificar spec existente version 2 o mayor — fspec siempre crea nuevas, para iterar usar el flag iteration. BP-57 dice confiar en persona-detection automatico, el skill detecta audiencia y ajusta tono sin configuracion manual.',
        'quiz': [
            {'q': 'Cuando NUNCA usar /fspec sin --iteration?', 'options': {'a': 'Para tickets DAD nuevos', 'b': 'Para modificar spec existente version 2 o mas', 'c': 'Para validar HANDOFF-CARD', 'd': 'Para crear test cases'}, 'correct': 'b'},
            {'q': 'Cuantas audiencias maneja /fchangelog --format all en una sola invocacion?', 'options': {'a': '1 (DEV)', 'b': '2 (PO + DEV)', 'c': '3 (PO + QA + DEV)', 'd': '4 (PO + QA + DEV + Board)'}, 'correct': 'c'},
            {'q': 'Para iterar una spec existente, que flag usar?', 'options': {'a': '--update', 'b': '--iteration v1 v2', 'c': '--patch', 'd': '--modify'}, 'correct': 'b'},
            {'q': 'Como debe responder el PO ante gaps que pregunta /freq?', 'options': {'a': 'Solo Si o No', 'b': 'Solo A B C', 'c': 'ID + decision + razon (BP-06)', 'd': 'Solo email al Tech Lead'}, 'correct': 'c'},
            {'q': 'Que hace persona-detection automatico de /fchangelog?', 'options': {'a': 'Detecta el rol del PR reviewer', 'b': 'Ajusta tono segun audiencia (Board/Tech/Customer) sin configurar', 'c': 'Identifica el componente afectado', 'd': 'Asigna prioridad al ticket'}, 'correct': 'b'},
        ],
    },
    # ───────────── O09 Capital Humano ─────────────
    {
        'folder': '06-capital-humano',
        'slug': 'capital-humano',
        'role_title': 'Capital Humano',
        'role_archetype': 'El Arquitecto Social',
        'slide_id': 'O09',
        'slide_duration': '2 min',
        'cover_meta': 'Onboarding nuevos · retro mensual · adopcion · forge skill propio',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Onboarding + retro mensual + adopcion + integracion HR con tecnico.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp version           validar marketplace
2.  /fguide soy nuevo        vivir el onboarding como newjoiner
3.  /fretro --full           retro sprint cerrado</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Onboarding (por nuevo empleado)
  /fguide soy nuevo (rol especifico)

Retrospectiva (cada 2 semanas / sprint cerrado)
  /fretro --full -> /fxnewskill (opcional si patron 3+ veces) -> /fretro --anexo-only</code></pre>

      <h3>KPIs adopcion sana</h3>
      <pre><code>Time-to-first-command   <30 min
Commands/usuario/dia    >=5
Feedback primer flujo   >=4 / 5</code></pre>

      <div class="callout">
        <em>Super-poder Capital Humano: vivir el onboarding como newjoiner antes de que entre alguien nuevo. /fguide soy nuevo es esa empatia operativa.</em>
      </div>''',
        'tts_cover': 'Modulo del rol Capital Humano. Onboarding nuevos y retro mensual.',
        'notes_cover': 'Bienvenido al modulo Capital Humano. Arquitecto Social que estructura onboarding 2 semanas. Quiz de 5 preguntas.',
        'tts_content': 'Capital Humano tiene un super poder. Vivir el onboarding como new joiner antes de que entre alguien nuevo. Comando fguide soy nuevo es esa empatia operativa. Y la retro mensual no es ritual: si un patron se repite tres veces, el equipo puede forjar un skill propio con comando fxnewskill. Eso es marketplace evolucionando con el equipo, no impuesto desde afuera.',
        'notes_content': 'El rol Capital Humano es el Arquitecto Social del marketplace. Estructura onboarding nuevos, retro mensual, y mide adopcion.\n\nTres comandos para iniciar: fhelp version para validar marketplace, fguide soy nuevo para vivir el onboarding como newjoiner antes de que entre alguien nuevo, y fretro full para retrospectiva sprint cerrado.\n\nFlujos diarios: Onboarding por nuevo empleado con fguide soy mas el rol especifico. Retrospectiva mensual con fretro full mas fxnewskill opcional cuando un patron se repite 3 veces o mas, mas fretro anexo only para sesiones siguientes.\n\nKPIs adopcion sana: TTFC menor a 30 minutos, comandos por usuario por dia mayor o igual a 5, feedback primer flujo mayor o igual a 4 sobre 5. Si en 15 dias alguien no completo su primer artefacto, hay problema de proceso, no de persona.',
        'quiz': [
            {'q': 'Cual es el super-poder de Capital Humano segun el slide?', 'options': {'a': 'Asignar tickets a devs', 'b': 'Vivir onboarding como newjoiner antes que entre alguien nuevo', 'c': 'Generar reportes de gradebook', 'd': 'Validar PRs de Devs Junior'}, 'correct': 'b'},
            {'q': 'Si un patron se repite 3 veces, que comando forja un skill propio del equipo?', 'options': {'a': '/fdev', 'b': '/fxnewskill', 'c': '/fanalyze REFACTOR', 'd': '/fretro --anexo'}, 'correct': 'b'},
            {'q': 'Cual es el KPI de feedback minimo del primer flujo?', 'options': {'a': '>=2/5', 'b': '>=3/5', 'c': '>=4/5', 'd': 'Sin minimo'}, 'correct': 'c'},
            {'q': 'Cuantas semanas dura el plan onboarding estructurado?', 'options': {'a': '1 semana', 'b': '2 semanas', 'c': '4 semanas', 'd': '12 semanas'}, 'correct': 'b'},
            {'q': 'Para retro de sesiones siguientes (no la primera), que flag usar?', 'options': {'a': '--full', 'b': '--quick', 'c': '--anexo-only', 'd': '--summary'}, 'correct': 'c'},
        ],
    },
    # ───────────── O10 Tech Lead ─────────────
    {
        'folder': '07-tech-lead',
        'slug': 'tech-lead',
        'role_title': 'Tech Lead / Arquitecto',
        'role_archetype': 'El Estratega de Sombras',
        'slide_id': 'O10',
        'slide_duration': '2 min',
        'cover_meta': 'PR review · decisiones arq · ADRs firmados · coherencia tecnica',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>PR review tecnico + decisiones arq + ADRs firmados + coherencia tecnica cross-team.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp pipeline             flow E2E completo
2.  /freview --full DAD-X       review 101 items con dictamen
3.  /fanalyze --mode REFACTOR   analisis brownfield + plan evolutivo</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>PR Review + ADR
  /freview --full -> si arq -> /fsign sign --weight 900

Decision Arquitectonica Cross-Team
  /fanalyze REFACTOR -> /fwarroom council -> /fdebate-ia -> /fsign --weight 900

Validacion Adversarial Pre-Implementacion (spec ambigua)
  /fdebate-ia FEATURE.md -> si fracturas -> /fspec --handoff -> /fdev</code></pre>

      <div class="callout-warn">
        Antes de /fdev con scope ambiguo, /fdebate-ia primero (BP-10). Evita refactors no solicitados.
      </div>''',
        'tts_cover': 'Modulo del rol Tech Lead o Arquitecto. PR review y decisiones arquitectonicas firmadas.',
        'notes_cover': 'Bienvenido al modulo Tech Lead. Estratega de Sombras que orquesta decisiones arq cross-team. Quiz de 5 preguntas.',
        'tts_content': 'Tech Lead tiene el patron cross-team mas valioso del marketplace. War room para decidir el tema macro. Debate IA para validar el artefacto que sale. Firma digital para audit trail. Tres comandos. Una decision defendible diez anios. Y antes de comando fdev con scope ambiguo, siempre comando fdebate-ia primero. Evita refactors no solicitados que despues nadie quiere revertir.',
        'notes_content': 'El rol Tech Lead es el Estratega de Sombras. Observa, sintetiza, decide arquitectura cross-team con audit trail firmado.\n\nTres comandos para iniciar: fhelp pipeline para ver el flujo completo end-to-end, freview full para PR review 101 items, y fanalyze mode REFACTOR para analisis brownfield con plan evolutivo.\n\nFlujos diarios: PR Review combinando freview full con fsign weight 900 si la decision es arquitectonica. Decision Arquitectonica Cross-Team con fanalyze REFACTOR, fwarroom council, fdebate-ia, y fsign weight 900 — los cuatro pasos generan audit trail defendible 10 anios. Validacion Adversarial Pre-Implementacion con fdebate-ia sobre FEATURE punto md para detectar fracturas en spec ambigua antes de que llegue a fdev.\n\nRegla BP-10: antes de fdev con scope ambiguo, fdebate-ia primero. Esto evita refactors no solicitados que despues nadie quiere revertir.',
        'quiz': [
            {'q': 'Cual es el patron cross-team mas valioso del marketplace para Tech Lead?', 'options': {'a': '/fdev mas /freview mas /fchangelog', 'b': '/fwarroom mas /fdebate-ia mas /fsign', 'c': '/fhelp mas /fguide mas /frecovery', 'd': '/fscan mas /fdoc mas /ftrace'}, 'correct': 'b'},
            {'q': 'Antes de /fdev con scope ambiguo, que comando ejecutar primero (BP-10)?', 'options': {'a': '/fanalyze REFACTOR', 'b': '/fdebate-ia FEATURE.md', 'c': '/fwarroom council', 'd': '/freview --full'}, 'correct': 'b'},
            {'q': 'Decisiones arquitectonicas weight 900 o mayor requieren?', 'options': {'a': 'Aprobacion verbal del Tech Lead', 'b': 'Email al equipo', 'c': '/fsign sign --weight 900', 'd': 'Comment en el PR'}, 'correct': 'c'},
            {'q': 'Que produce /fanalyze --mode REFACTOR?', 'options': {'a': 'Tests xUnit', 'b': 'HANDOFF-CARD', 'c': 'Analisis brownfield + plan evolutivo', 'd': 'Postmortem PCI'}, 'correct': 'c'},
            {'q': 'Para audit trail defendible 10 anios ante BCRA, cual es la cadena completa?', 'options': {'a': '/fdev mas /freview', 'b': '/fwarroom council mas /fdebate-ia mas /fsign weight 900', 'c': '/fhelp mas /fguide', 'd': '/fchangelog mas /frelease'}, 'correct': 'b'},
        ],
    },
    # ───────────── O11 Dev Senior ─────────────
    {
        'folder': '08-dev-senior',
        'slug': 'dev-senior',
        'role_title': 'Dev Senior',
        'role_archetype': 'El Constructor Implacable',
        'slide_id': 'O11',
        'slide_duration': '2 min',
        'cover_meta': 'Features E2E · bug fix prod · brownfield · mentor junior',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Features end-to-end + bug fix produccion + brownfield multi-servicio + mentor de Dev Junior.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp pipeline       15 steps del feature E2E
2.  /fspec DAD-X --form   generar HANDOFF-CARD primer ticket
3.  /fdev DAD-X           primer feature implementacion supervisada</code></pre>

      <h3>Flujo diario principal · Feature E2E robusto (15 steps)</h3>
      <pre><code>/freq -> /fanalyze -> /fspec -> /fforensic spec
  -> /fdev -> /fdrift -> /ftest -> /freview --full -> /fforensic
  -> /fchangelog --format all -> /frelease -> /fretro</code></pre>

      <div class="callout-warn">Path absoluto al HANDOFF-CARD siempre (BP-02)</div>
      <div class="callout-warn">/fforensic 0 hallazgos en TODAS las severidades (BP-46)</div>
      <div class="callout-warn">Cierre formal SIEMPRE: /frelease + /fretro post-merge (BP-23)</div>
      <div class="callout-warn">Drift &lt;80 -> /fdev --fix DRIFT-001,002 (BP-04)</div>''',
        'tts_cover': 'Modulo del rol Dev Senior. Features E2E con 15 steps y mentor de juniors.',
        'notes_cover': 'Bienvenido al modulo Dev Senior. Constructor Implacable con disciplina E2E. Quiz de 5 preguntas.',
        'tts_content': 'Dev senior, cuatro reglas que evitan el noventa por ciento de los retrabajos. Path absoluto al HANDOFF-CARD. Comando fdev DAD-X solo no funciona. Forensic en cero hallazgos en todas las severidades, incluyendo LOW. Cierre formal siempre: comando frelease y fretro no son opcionales. Y si el drift baja, no re-expliquen contexto: comando fdev modo fix con los IDs especificos.',
        'notes_content': 'El rol Dev Senior es el Constructor Implacable. Implementa features end-to-end con disciplina de gates.\n\nTres comandos para iniciar: fhelp pipeline para ver los 15 steps del feature E2E, fspec DAD-X mode form para generar HANDOFF-CARD del primer ticket, y fdev DAD-X para primer feature implementacion supervisada.\n\nFlujo diario principal feature E2E robusto en 15 steps: freq, fanalyze, fspec, fforensic spec, fdev, fdrift, ftest, freview full, fforensic post-fix, fchangelog format all, frelease, fretro.\n\nCuatro reglas no negociables. BP-02: path absoluto al HANDOFF-CARD siempre — fdev sin path completo no funciona, infiere mal. BP-46: forensic 0 hallazgos en TODAS las severidades, incluyendo LOW. BP-23: cierre formal siempre con frelease mas fretro post-merge. BP-04: si drift score baja de 80, fdev mode fix con DRIFT-IDs especificos, no re-explicar contexto.',
        'quiz': [
            {'q': 'Cuantos steps tiene el flujo Feature E2E robusto del Dev Senior?', 'options': {'a': '8 steps', 'b': '12 steps', 'c': '15 steps', 'd': '20 steps'}, 'correct': 'c'},
            {'q': 'Cuantos hallazgos debe tener /fforensic en TODAS las severidades segun BP-46?', 'options': {'a': '0 hallazgos', 'b': 'Menor a 5', 'c': 'Solo CRITICAL en 0', 'd': 'No aplica'}, 'correct': 'a'},
            {'q': 'Si drift score baja de 80, que comando con que flag (BP-04)?', 'options': {'a': '/fdev --redo', 'b': '/fdev --fix DRIFT-IDs', 'c': '/fdrift --recompute', 'd': '/freview --critical'}, 'correct': 'b'},
            {'q': 'Que dos comandos forman el cierre formal post-merge (BP-23)?', 'options': {'a': '/fchangelog mas /freview', 'b': '/frelease mas /fretro', 'c': '/fdev mas /ftest', 'd': '/fhelp mas /fcompact'}, 'correct': 'b'},
            {'q': 'Para /fdev funcionar correctamente, que regla aplica al HANDOFF-CARD (BP-02)?', 'options': {'a': 'Path relativo al directorio actual', 'b': 'Path absoluto al HANDOFF-CARD siempre', 'c': 'Path no necesario, lo infiere automatico', 'd': 'Solo nombre del archivo HANDOFF.json'}, 'correct': 'b'},
        ],
    },
    # ───────────── O13 Frontend Architect ─────────────
    {
        'folder': '09-frontend-arq',
        'slug': 'frontend-arq',
        'role_title': 'Frontend Architect',
        'role_archetype': 'El Arquitecto de Realidades',
        'slide_id': 'O13',
        'slide_duration': '2 min',
        'cover_meta': 'Portales fintech · BFF Authorize · CSP/SRI · pen-test continuo',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Portales frontend fintech + pen-test continuo. Stack Next.js 16 + React 19 + MUI 7.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp version              validar marketplace
2.  /ffrontend-architect        arquitectura senior frontend
3.  /fscaffold-frontend         Next.js 16 + React 19 + MUI 7</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Frontend Migration / Audit
  /ffrontend-architect -> audit -> migrate -> secure -> test -> pentest -> review

Widget / feature nuevo
  /fscaffold-frontend + BFF backend

Feature Frontend E2E
  /fanalyze -> /fspec -> /fdev -> /fforensic -> /freview --full</code></pre>

      <div class="callout-pci">BFF Authorize peso 1000 — todo endpoint que toca BFF necesita [Authorize] o [AllowAnonymous] explicito (FXQ-API-04 L0).</div>
      <div class="callout">Referencia canonica verificada: Web.Portal20 de Bind Aceptador.</div>''',
        'tts_cover': 'Modulo del rol Frontend Architect. Portales fintech con BFF Authorize y pen-test.',
        'notes_cover': 'Bienvenido al modulo Frontend Architect. Arquitecto de Realidades con compliance integrada. Quiz de 5 preguntas.',
        'tts_content': 'Frontend Architect: la regla mas alta de tu stack es BFF Authorize peso mil. Todo endpoint que toca BFF necesita Authorize o AllowAnonymous explicito. Nunca implicito. Y para PCI scope IN siempre comando freview modo full, los ciento un items completos, nunca el default. La referencia canonica es Web Portal veinte de Bind Aceptador.',
        'notes_content': 'El rol Frontend Architect es el Arquitecto de Realidades. Disena portales fintech con compliance PCI DSS v4 integrada y pen-test continuo.\n\nTres comandos para iniciar: fhelp version para validar marketplace, ffrontend-architect que activa arquitectura senior, y fscaffold-frontend que genera el stack Next punto js 16 mas React 19 mas MUI 7.\n\nFlujos diarios: Frontend Migration con secuencia ffrontend-architect, audit, migrate, secure, test, pentest, review. Widget nuevo combinando fscaffold-frontend mas BFF backend. Feature Frontend E2E con fanalyze, fspec, fdev, fforensic, freview full.\n\nReglas inviolables. BFF Authorize weight 1000 nivel L0: todo endpoint BFF requiere atributo Authorize o AllowAnonymous explicito, nunca implicito. Eso es FXQ-API-04. Para PCI scope IN, siempre freview full con los 101 items, nunca el default 26 items. La referencia verificada del stack es Web Portal20 del proyecto Bind Aceptador.',
        'quiz': [
            {'q': 'Que peso tiene la regla BFF Authorize del Frontend Architect?', 'options': {'a': '400 (L3)', 'b': '700 (L1)', 'c': '900 (L0)', 'd': '1000 (L0)'}, 'correct': 'd'},
            {'q': 'Para PCI scope IN, que tipo de review usar siempre?', 'options': {'a': '/freview --critical (26 items)', 'b': '/freview --full (101 items)', 'c': '/freview --quick', 'd': '/freview --auto'}, 'correct': 'b'},
            {'q': 'Cual es el stack de /fscaffold-frontend?', 'options': {'a': 'Vue 3 + Nuxt + Tailwind', 'b': 'Next.js 16 + React 19 + MUI 7', 'c': 'Angular 17 + RxJS + Material', 'd': 'Svelte 5 + SvelteKit'}, 'correct': 'b'},
            {'q': 'Cual es la referencia canonica verificada del stack frontend?', 'options': {'a': 'Cuenta.Portal Wallet', 'b': 'Web.Portal20 de Bind Aceptador', 'c': 'PaymentAcceptor.UI', 'd': 'Stub generic'}, 'correct': 'b'},
            {'q': 'El pen-test del frontend es?', 'options': {'a': 'Anual con QSA externo', 'b': 'Trimestral interno', 'c': 'Continuo (specter orbital adversario)', 'd': 'Solo en releases mayores'}, 'correct': 'c'},
        ],
    },
    # ───────────── O14 QA Engineer/Lead ─────────────
    {
        'folder': '10-qa',
        'slug': 'qa',
        'role_title': 'QA Engineer / QA Lead',
        'role_archetype': 'El Inquisidor Tecnico',
        'slide_id': 'O14',
        'slide_duration': '2 min',
        'cover_meta': 'DoR gates · 7 bloques casos · dictamen PROD · Quality Gate',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>DoR gates + casos de prueba 7 bloques + dictamen incidente PROD + Quality Gate.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp pipeline QA       flow E2E QA
2.  /fsqa DoR DAD-X         primer gate DoR
3.  /ftest                  generar tests xUnit + Moq + WireMock</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>QA Coverage (sprint planning a cierre)
  /fsqa -> /fsqa DoR -> /fsqa casos -> /fsqa subtarea

Dictamen Error PROD (incidente)
  /fsqa-readonly --with-qa --with-antecedentes -> /fsqa error produccion

Quality pre-merge main
  /fdrift -> /freview --full -> /ftest cobertura</code></pre>

      <div class="callout-warn">Golden Rule S4: read-only Jira por default — escritura SOLO con confirmacion humana explicita.</div>
      <div class="callout-warn">Tests ratio 1:3 minimo entre codigo y tests (BP-47). Coverage >=85% para merge a main (BP-48).</div>''',
        'tts_cover': 'Modulo del rol QA Engineer o Lead. DoR gates, casos 7 bloques y Quality Gate.',
        'notes_cover': 'Bienvenido al modulo QA. Inquisidor Tecnico con rigor forensic. Quiz de 5 preguntas.',
        'tts_content': 'QA, dos numeros no negociables: ratio uno a tres minimo entre codigo y tests, cobertura mayor o igual a ochenta y cinco por ciento para merge a main. Y la Golden Rule cuatro: nunca escritura a Jira sin confirmacion humana explicita. El dictamen sobre incidente PROD usa comando fsqa-readonly precisamente por eso. Analisis con cero efectos colaterales.',
        'notes_content': 'El rol QA Engineer o Lead es el Inquisidor Tecnico. Aplica rigor forensic con DoR gates inviolables.\n\nTres comandos para iniciar: fhelp pipeline QA para ver el flujo end-to-end, fsqa DoR DAD-X para gate Definition of Ready, y ftest que genera tests xUnit con Moq y WireMock.\n\nFlujos diarios: QA Coverage con secuencia fsqa, fsqa DoR, fsqa casos en 7 bloques (FAPI/FSQL/FFLOW/Integracion/Seguridad/Regresion/E2E), fsqa subtarea. Dictamen Error PROD con fsqa-readonly mas flags combinables with-qa y with-antecedentes (cache 24 horas), seguido de fsqa error produccion para emitir dictamen oficial. Quality pre-merge a main combinando fdrift, freview full y ftest cobertura.\n\nReglas no negociables. Golden Rule S4: read-only Jira por default, escritura SOLO con confirmacion humana explicita. BP-47: ratio 1:3 minimo entre codigo y tests. BP-48: coverage mayor o igual a 85 por ciento para merge a main. Para analisis incidente PROD sin efectos colaterales, fsqa-readonly es la herramienta correcta.',
        'quiz': [
            {'q': 'Cual es el ratio minimo entre codigo y tests segun BP-47?', 'options': {'a': '1:1', 'b': '1:2', 'c': '1:3', 'd': '1:5'}, 'correct': 'c'},
            {'q': 'Cual es la cobertura minima requerida para merge a main (BP-48)?', 'options': {'a': '>=70%', 'b': '>=80%', 'c': '>=85%', 'd': '>=95%'}, 'correct': 'c'},
            {'q': 'Que dice la Golden Rule S4 sobre Jira?', 'options': {'a': 'Escritura libre, lectura restringida', 'b': 'Read-only por default, escritura SOLO con confirmacion humana', 'c': 'Solo Tech Lead puede escribir', 'd': 'Cualquier QA puede modificar tickets'}, 'correct': 'b'},
            {'q': 'Cuantos bloques de casos de prueba genera /fsqa casos?', 'options': {'a': '3 bloques', 'b': '5 bloques', 'c': '7 bloques (FAPI/FSQL/FFLOW/Integracion/Seguridad/Regresion/E2E)', 'd': '10 bloques'}, 'correct': 'c'},
            {'q': 'Para analisis de incidente PROD sin efectos colaterales, que comando?', 'options': {'a': '/fsqa error', 'b': '/fsqa-readonly --with-qa --with-antecedentes', 'c': '/fdev --debug', 'd': '/fanalyze incident'}, 'correct': 'b'},
        ],
    },
    # ───────────── O15 SRE/DevOps/Oncall ─────────────
    {
        'folder': '11-sre',
        'slug': 'sre',
        'role_title': 'SRE / DevOps / Oncall',
        'role_archetype': 'La Inteligencia Fria',
        'slide_id': 'O15',
        'slide_duration': '2 min',
        'cover_meta': 'Incident response · AKS deploy · runbooks · GitOps ArgoCD',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Incident response + deploy AKS + runbooks + GitOps ArgoCD. Cold precision oncall.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp fd-incident + fd-aks
2.  /fd-aks                           kubectl + HPA + PDB + 0-downtime
3.  /fd-incident p0                   primer triage P0/P1</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>SRE Oncall + Deploy
  /fd-aks + /fd-obs   monitoring continuo

Incident Response P0/P1
  /fd-incident p0 -> triage + runbook + postmortem PCI Req 12.10

Observability
  /fd-obs   Fluent Bit + Serilog + Elasticsearch + Grafana + SLO

GitOps Deploy
  /fd-argocd   ApplicationSet + Rollouts + Kargo promotion</code></pre>

      <div class="callout-pci">P0 PAGOS DOWN >2min -> Incident notification BCRA <30min (DEVOPS-03).</div>
      <div class="callout-warn">Para canal Teams usar /fteams msg con titulo y mensaje claros (patron H-01 sesion 56).</div>''',
        'tts_cover': 'Modulo del rol SRE DevOps Oncall. Incident response y deploy AKS.',
        'notes_cover': 'Bienvenido al modulo SRE. Inteligencia Fria con sub-2-minute response. Quiz de 5 preguntas.',
        'tts_content': 'SRE oncall: tres reglas criticas. P cero pagos caidos por mas de dos minutos: BCRA en menos de treinta minutos. No es flexible. Postmortem con seis campos PCI DSS doce punto diez. Diez anios retencion. Para alertar al equipo usa comando fteams modo msg con titulo y mensaje claros.',
        'notes_content': 'El rol SRE DevOps Oncall es La Inteligencia Fria. Cold precision en respuesta sub-2-minute.\n\nTres comandos para iniciar: fhelp con focus fd-incident y fd-aks, fd-aks que provee kubectl mas HPA mas PDB mas zero-downtime deploy, y fd-incident p0 para primer triage.\n\nFlujos diarios: SRE Oncall combinando fd-aks con fd-obs para monitoring continuo. Incident Response P0/P1 con fd-incident p0 mas triage mas runbook mas postmortem PCI Req 12.10. Observability con fd-obs que cubre Fluent Bit, Serilog, Elasticsearch, Grafana y SLO. GitOps Deploy con fd-argocd que orquesta ApplicationSet, Argo Rollouts y Kargo promotion.\n\nReglas inviolables: P0 pagos caidos mas de 2 minutos requiere notificacion BCRA en menos de 30 minutos por DEVOPS-03 y Comunicacion A 7724. Postmortem con 6 campos PCI DSS Req 12.10.1 al 12.10.6 retenidos 10 anios. Para canal Teams usar fteams msg con titulo y mensaje claros — fnotify es solo toast LOCAL del SO, no canal Teams (patron H-01 sesion 56 cont).',
        'quiz': [
            {'q': 'P0 pagos caidos mas de 2 minutos requiere notificacion BCRA en cuanto tiempo?', 'options': {'a': 'Menos de 5 min', 'b': 'Menos de 15 min', 'c': 'Menos de 30 min', 'd': 'Menos de 60 min'}, 'correct': 'c'},
            {'q': 'Postmortem cubre que requisito PCI DSS con cuantos campos?', 'options': {'a': '4.2 con 4 campos', 'b': '8.1 con 8 campos', 'c': '12.10 con 6 campos (12.10.1-6)', 'd': '3.2 con 2 campos'}, 'correct': 'c'},
            {'q': 'Para alertar al canal Teams en escalamiento, que comando usar?', 'options': {'a': '/fnotify msg (toast SO)', 'b': '/fteams msg (canal Teams)', 'c': '/fhelp version', 'd': '/fcompact'}, 'correct': 'b'},
            {'q': 'Que provee /fd-aks?', 'options': {'a': 'Solo kubectl basico', 'b': 'kubectl + HPA + PDB + zero-downtime deploy', 'c': 'Helm charts solo', 'd': 'Solo logs view'}, 'correct': 'b'},
            {'q': 'Que orquesta /fd-argocd?', 'options': {'a': 'Solo ArgoCD CLI', 'b': 'ApplicationSet + Argo Rollouts + Kargo promotion', 'c': 'Solo Helm', 'd': 'Jenkins pipelines'}, 'correct': 'b'},
        ],
    },
    # ───────────── O16 Soporte N1/N2 ─────────────
    {
        'folder': '13-soporte',
        'slug': 'soporte',
        'role_title': 'Soporte N1 / N2',
        'role_archetype': 'El Nodo de Informacion',
        'slide_id': 'O16',
        'slide_duration': '2 min',
        'cover_meta': 'Triage inbound · escalamiento · alerta canal · handoff dev',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Triage inbound + escalamiento N1/N2 + alerta canal + handoff dev oncall trazable.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp version
2.  /fticket DAD-X --mode forensic       commits relacionados + historial
3.  /fwarroom msg (practicar)            alertar canal con tribunal</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Triage Inbound
  /fticket forensic -> /fsqa-readonly -> /fd-incident -> /fwarroom msg

Escalamiento N1/N2 a dev oncall
  /fsqa-readonly --with-qa --with-antecedentes -> /fticket completo
    -> /fscan -> /fwarroom msg "Escalamiento {id}" "Dev oncall review {link}"</code></pre>

      <div class="callout">Aprovechar flags combinables: /fsqa-readonly --with-qa --with-antecedentes en una sola invocacion (cache 24h).</div>''',
        'tts_cover': 'Modulo del rol Soporte N1 N2. Triage inbound y escalamiento dev oncall.',
        'notes_cover': 'Bienvenido al modulo Soporte. Nodo de Informacion con escalamiento trazable. Quiz de 5 preguntas.',
        'tts_content': 'Soporte N uno N dos: para alertar al dev oncall, comando fwarroom modo msg con titulo y ticket. Y aprovechen los flags combinables de comando fsqa-readonly: with-qa con with-antecedentes en una sola invocacion trae historia, antecedentes y casos QA. Cache veinticuatro horas.',
        'notes_content': 'El rol Soporte N1/N2 es el Nodo de Informacion. Triage inbound con escalamiento trazable a dev oncall.\n\nTres comandos para iniciar: fhelp version, fticket DAD-X mode forensic que trae commits relacionados mas historial, y fwarroom msg para practicar alertar canal con tribunal de 103 arquetipos.\n\nFlujos diarios: Triage Inbound con secuencia fticket forensic, fsqa-readonly, fd-incident, fwarroom msg. Escalamiento N1/N2 a dev oncall con fsqa-readonly mas flags combinables with-qa y with-antecedentes (una sola invocacion, cache 24 horas), seguido de fticket completo, fscan del servicio, y fwarroom msg con titulo Escalamiento mas ID y mensaje Dev oncall review mas link al ticket.\n\nLeccion clave: aprovechar los flags combinables del marketplace. fsqa-readonly with-qa with-antecedentes en una sola invocacion trae historia completa, antecedentes y casos QA cacheados 24 horas — mas eficiente que 3 invocaciones separadas.',
        'quiz': [
            {'q': 'Para alertar al dev oncall en escalamiento, que comando usar?', 'options': {'a': '/fnotify msg', 'b': '/fwarroom msg con titulo y ticket', 'c': '/fhelp version', 'd': '/fdev --escalate'}, 'correct': 'b'},
            {'q': 'Cuales son los flags combinables de /fsqa-readonly mas eficientes?', 'options': {'a': '--quick --cache', 'b': '--with-qa --with-antecedentes (cache 24h)', 'c': '--full --debug', 'd': '--readonly --noerror'}, 'correct': 'b'},
            {'q': 'Que incluye /fticket --mode forensic?', 'options': {'a': 'Solo el ticket', 'b': 'Solo descripcion', 'c': 'Commits relacionados + historial', 'd': 'Solo asignado actual'}, 'correct': 'c'},
            {'q': 'El flujo escalamiento N1/N2 a dev oncall inicia con que comando?', 'options': {'a': '/fdev', 'b': '/fsqa-readonly --with-qa --with-antecedentes', 'c': '/freview --full', 'd': '/fchangelog'}, 'correct': 'b'},
            {'q': 'Cuantos arquetipos tiene el tribunal de /fwarroom?', 'options': {'a': '15 arquetipos', 'b': '50 arquetipos', 'c': '103 arquetipos', 'd': '200 arquetipos'}, 'correct': 'c'},
        ],
    },
    # ───────────── O17 Analista Funcional ─────────────
    {
        'folder': '14-analista',
        'slug': 'analista',
        'role_title': 'Analista Funcional',
        'role_archetype': 'El Intelecto Relampago',
        'slide_id': 'O17',
        'slide_duration': '2 min',
        'cover_meta': 'Specs · reverse-eng SDD · spec iterativa · Debate adversarial',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Specs forense + reverse-engineering SDD 14 secciones + spec iterativa + Debate adversarial pre-implementacion.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp pipeline      flow E2E analisis
2.  /freq DAD-X          entrevista automatica criterios aceptacion
3.  /fdoc                SDD 14 secciones sobre 1 servicio</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Reverse-Engineering SDD
  /fscan {svc} -> /fdoc -> /ftrace

Spec + Normativa
  /freq -> /fspec -> /fdebate-ia -> /fspec --handoff -> /ftrace --matrix

Spec Iterativa (cambios scope)
  /fspec --iteration v2 v1.md</code></pre>

      <div class="callout-warn">Antes de spec brownfield: leer appsettings.*.json + AppConstants.cs siempre (BP-01).</div>
      <div class="callout-warn">HANDOFF-CARD = contrato versionado. Una vez en READY_FOR_DEV, cambios = nuevo HANDOFF v+1 (BP-13).</div>''',
        'tts_cover': 'Modulo del rol Analista Funcional. Specs forense y SDD 14 secciones.',
        'notes_cover': 'Bienvenido al modulo Analista Funcional. Intelecto Relampago con specs precisas. Quiz de 5 preguntas.',
        'tts_content': 'Analista funcional, dos habitos que evitan tres pasadas de drift. Antes de generar spec brownfield: leer todos los appsettings punto json y AppConstants punto c sharp. Sin esto se generan errores tontos que cuestan iteraciones. Y el HANDOFF-CARD es contrato versionado: una vez en READY FOR DEV no se edita, cambios significan nuevo HANDOFF version mas uno. La trazabilidad es lo que defiende ante BCRA.',
        'notes_content': 'El rol Analista Funcional es el Intelecto Relampago. Specs forense con reverse-engineering rapido y validacion adversarial.\n\nTres comandos para iniciar: fhelp pipeline para ver el flujo E2E de analisis, freq DAD-X para entrevista automatica con criterios de aceptacion, y fdoc que genera SDD 14 secciones sobre un servicio.\n\nFlujos diarios: Reverse-Engineering SDD con secuencia fscan del servicio, fdoc, ftrace para validar trazabilidad. Spec mas Normativa con freq, fspec, fdebate-ia para validacion adversarial, fspec mode handoff, ftrace mode matrix. Spec Iterativa con cambios de scope usa fspec mode iteration v2 sobre v1 punto md.\n\nDos habitos criticos que evitan 3 pasadas de drift. BP-01: antes de spec brownfield siempre leer todos los appsettings dot json y AppConstants dot cs. Sin esto se generan errores tontos. BP-13: HANDOFF-CARD es contrato versionado, una vez en estado READY FOR DEV no se edita — cambios significan nuevo HANDOFF version mas uno. Esa trazabilidad es lo que defiende ante BCRA en auditorias.',
        'quiz': [
            {'q': 'Cuantas secciones tiene el SDD que genera /fdoc?', 'options': {'a': '7 secciones', 'b': '10 secciones', 'c': '14 secciones', 'd': '20 secciones'}, 'correct': 'c'},
            {'q': 'Antes de generar spec brownfield, que es obligatorio leer (BP-01)?', 'options': {'a': 'Solo el ticket Jira', 'b': 'appsettings.*.json + AppConstants.cs', 'c': 'Solo el README del servicio', 'd': 'Solo los logs recientes'}, 'correct': 'b'},
            {'q': 'HANDOFF-CARD en estado READY_FOR_DEV se edita o se versiona (BP-13)?', 'options': {'a': 'Se edita libremente', 'b': 'Se versiona — nuevo HANDOFF v+1 si hay cambios', 'c': 'Solo PO puede editarlo', 'd': 'No se puede modificar nunca'}, 'correct': 'b'},
            {'q': 'Para spec iterativa con cambios de scope, que flag usar?', 'options': {'a': '/fspec --update', 'b': '/fspec --iteration v1 v2', 'c': '/fspec --patch', 'd': '/fspec --modify'}, 'correct': 'b'},
            {'q': 'Para validacion adversarial pre-implementacion de la spec, que comando?', 'options': {'a': '/fanalyze REFACTOR', 'b': '/fdebate-ia FEATURE.md', 'c': '/fwarroom council', 'd': '/freview --critical'}, 'correct': 'b'},
        ],
    },
    # ───────────── O18 Compliance/QSA ─────────────
    {
        'folder': '15-compliance-qsa',
        'slug': 'compliance-qsa',
        'role_title': 'Compliance / Auditor QSA',
        'role_archetype': 'El Juez Externo',
        'slide_id': 'O18',
        'slide_duration': '2 min',
        'cover_meta': 'Auditoria PCI v4 · QSA externo · evidence · attestation',
        'content_html': '''
      <h3>Quien soy</h3>
      <p>Auditoria externa PCI DSS v4 + QSA + evidence collection + attestation periodica.</p>

      <h3>3 comandos para iniciar (Semana 1)</h3>
      <pre><code>1.  /fhelp version
2.  /fanalyze --mode compliance {req}    analisis compliance regulatorio
3.  /freview --full                      review 101 items full</code></pre>

      <h3>Flujos diarios del rol</h3>
      <pre><code>Auditoria PCI v4
  /fanalyze --mode compliance -> /freview --full xN -> /fsign verify --all

QSA externo
  /fscan -> /fanalyze compliance -> /freview --full -> /fsign verify --all

Verificacion firmantes
  /fsign list + /fsign verify --all</code></pre>

      <div class="callout-pci">PCI INVIOLABLE peso 9999 — la regla mas alta del marketplace.</div>
      <div class="callout-warn">Retention 10 anios — git history + audit trail /fsign append-only + FXQ-META-04 provenance (SHA-256 + fingerprint git).</div>''',
        'tts_cover': 'Modulo del rol Compliance o Auditor QSA. PCI DSS v4 attestation y evidence externo.',
        'notes_cover': 'Bienvenido al modulo Compliance QSA. Juez Externo con scrutiny inmutable. Quiz de 5 preguntas.',
        'tts_content': 'Compliance y auditoria externa. La regla mas alta del marketplace es PCI inviolable peso nueve mil novecientos noventa y nueve. Cualquier sospecha es STOP y escalacion. La retencion BCRA de diez anios no es promesa: esta en git history mas comando fsign audit trail append-only. Y FXQ-META-cero-cuatro provenance firma cada artefacto con SHA-doscientos cincuenta y seis y fingerprint git. Eso es lo que ven los auditores QSA externos.',
        'notes_content': 'El rol Compliance Auditor QSA es el Juez Externo. Audit periodico con scrutiny inmutable y evidence verificable.\n\nTres comandos para iniciar: fhelp version, fanalyze mode compliance sobre el requerimiento regulatorio, y freview full con los 101 items completos.\n\nFlujos diarios: Auditoria PCI v4 con fanalyze mode compliance, freview full repetido por servicio, y fsign verify all que verifica todas las firmas digitales. QSA externo con secuencia fscan, fanalyze compliance, freview full, fsign verify all. Verificacion firmantes con fsign list y fsign verify all para ver el estado de todas las firmas.\n\nReglas inviolables: PCI weight 9999 — la mas alta del marketplace. Cualquier sospecha de PAN o CVV es STOP y escalacion. Retencion BCRA 10 anios no es promesa: esta garantizada por git history mas fsign audit trail append-only. FXQ-META-04 provenance firma cada artefacto generado con SHA-256 mas fingerprint git mas timestamp ISO 8601 — eso es lo que ven y verifican los auditores QSA externos cuando piden evidence.',
        'quiz': [
            {'q': 'Cual es la regla mas alta del marketplace por peso?', 'options': {'a': 'BFF Authorize weight 1000', 'b': 'PCI inviolable peso 9999', 'c': 'CQRS weight 800', 'd': 'Idempotency weight 800'}, 'correct': 'b'},
            {'q': 'Como se garantiza la retencion BCRA de 10 anios?', 'options': {'a': 'Solo en BD relacional con backups', 'b': 'git history + /fsign audit trail append-only', 'c': 'Solo en SharePoint con permisos', 'd': 'Solo email archivado'}, 'correct': 'b'},
            {'q': 'Que firma cada artefacto generado con SHA-256 + fingerprint git + timestamp?', 'options': {'a': 'FXQ-PCI-01', 'b': 'FXQ-SEC-01', 'c': 'FXQ-META-04 provenance', 'd': 'FXQ-API-04'}, 'correct': 'c'},
            {'q': 'Que comando verifica todas las firmas digitales del repo?', 'options': {'a': '/fsign list', 'b': '/fsign verify --all', 'c': '/fsign request', 'd': '/fsign approve'}, 'correct': 'b'},
            {'q': 'Cual es el flujo correcto de QSA externo?', 'options': {'a': '/fdev a /freview a /fchangelog', 'b': '/fscan a /fanalyze compliance a /freview --full a /fsign verify --all', 'c': '/freq a /fspec a /fdev', 'd': '/fhelp a /fguide a /fcompact'}, 'correct': 'b'},
        ],
    },
]

# ─────────────────────────────────────────────────────────────────────
# GENERATE
# ─────────────────────────────────────────────────────────────────────

def generate_role_module(role):
    """Generate index.html + imsmanifest.xml for a role."""
    folder = OUTPUT_BASE / role['folder']
    folder.mkdir(parents=True, exist_ok=True)

    quiz_html = render_quiz_html(role['quiz'])
    slug_upper = role['slug'].upper().replace('-', '-')

    html = HTML_TEMPLATE.format(
        role_title=role['role_title'],
        role_archetype=role['role_archetype'],
        slide_id=role['slide_id'],
        slide_duration=role['slide_duration'],
        cover_meta=role['cover_meta'],
        content_html=role['content_html'],
        quiz_html=quiz_html,
        tts_cover=role['tts_cover'],
        notes_cover=role['notes_cover'],
        tts_content=role['tts_content'],
        notes_content=role['notes_content'],
    )
    (folder / 'index.html').write_text(html, encoding='utf-8')

    manifest = MANIFEST_TEMPLATE.format(
        manifest_id=f"rol-{role['slug']}",
        slug_upper=slug_upper,
        manifest_title=f"Fintexa Marketplace V2 — Rol {role['role_title']}",
        manifest_subtitle=f"Onboarding {role['role_title']} · Slide rol + Quiz",
    )
    (folder / 'imsmanifest.xml').write_text(manifest, encoding='utf-8')

    return folder


def main():
    print(f"Output base: {OUTPUT_BASE}")
    print(f"Generating {len(ROLES)} role modules...\n")
    for role in ROLES:
        folder = generate_role_module(role)
        print(f"  [OK] {folder.relative_to(OUTPUT_BASE.parent)} · {role['role_title']} ({role['slide_id']})")
    print(f"\nDone. {len(ROLES)} modulos generados en {OUTPUT_BASE}")
    print("Empaquetar con PowerShell loop:")
    print(r"  Get-ChildItem modulo-roles -Directory | ForEach-Object { Compress-Archive -Path $_/index.html, $_/imsmanifest.xml -DestinationPath exports/rol-$($_.Name).scorm.zip -Force }")


if __name__ == '__main__':
    main()
