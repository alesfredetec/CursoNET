// MarkdownModalViewer.js
// Componente modal para visualizar Markdown con scroll, centrado y botón copiar
// Requiere marked.js

class MarkdownModalViewer extends HTMLElement {
  constructor() {
    super();
    this.attachShadow({mode: 'open'});
    // Modal wrapper
    const modalBg = document.createElement('div');
    modalBg.className = 'mmv-modal-bg';
    modalBg.addEventListener('click', (e) => {
      if (e.target === modalBg) this.close();
    });
    // Modal content
    const modal = document.createElement('div');
    modal.className = 'mmv-modal';
    // Header
    this.header = document.createElement('div');
    this.header.className = 'mmv-header';
    this.header.innerHTML = '<span>📄 Vista Markdown</span>';
    // Close button
    const closeBtn = document.createElement('button');
    closeBtn.className = 'mmv-close';
    closeBtn.innerHTML = '&times;';
    closeBtn.title = 'Cerrar';
    closeBtn.onclick = () => this.close();
    this.header.appendChild(closeBtn);
    // Copy button
    this.copyBtn = document.createElement('button');
    this.copyBtn.className = 'mmv-copy';
    this.copyBtn.innerHTML = '📋 Copiar';
    this.copyBtn.title = 'Copiar Markdown';
    this.copyBtn.onclick = () => this.copyMarkdown();
    this.header.appendChild(this.copyBtn);
    // Markdown content
    this.container = document.createElement('div');
    this.container.className = 'mmv-content';
    // Append
    modal.appendChild(this.header);
    modal.appendChild(this.container);
    modalBg.appendChild(modal);
    this.shadowRoot.appendChild(this.styles());
    this.shadowRoot.appendChild(modalBg);
    // Guardar referencia
    this.modalBg = modalBg;
    this.mdRaw = '';
  }

  static get observedAttributes() {
    return ['src', 'content'];
  }

  connectedCallback() {
    this.render();
  }

  attributeChangedCallback(name, oldValue, newValue) {
    if (oldValue !== newValue) {
      this.render();
    }
  }

  async render() {
    let md = '';
    if (this.hasAttribute('content')) {
      md = this.getAttribute('content');
    } else if (this.hasAttribute('src')) {
      const url = this.getAttribute('src');
      try {
        const res = await fetch(url);
        md = await res.text();
      } catch (e) {
        md = `Error cargando archivo: ${url}`;
      }
    } else {
      md = 'No se ha especificado contenido Markdown.';
    }
    this.mdRaw = md;
    if (window.marked) {
      this.container.innerHTML = window.marked.parse(md);
    } else {
      this.container.innerHTML = '<b>Error:</b> No se encontró la librería <code>marked</code>.';
    }
  }

  open() {
    this.modalBg.style.display = 'flex';
    document.body.style.overflow = 'hidden';
  }

  close() {
    this.modalBg.style.display = 'none';
    document.body.style.overflow = '';
  }

  copyMarkdown() {
    if (!navigator.clipboard) {
      alert('Clipboard API no soportada');
      return;
    }
    navigator.clipboard.writeText(this.mdRaw).then(() => {
      this.copyBtn.innerText = '✅ Copiado';
      setTimeout(() => { this.copyBtn.innerText = '📋 Copiar'; }, 1200);
    });
  }

  styles() {
    const style = document.createElement('style');
    style.textContent = `
      .mmv-modal-bg {
        position: fixed;
        top: 0; left: 0; right: 0; bottom: 0;
        background: rgba(30, 41, 59, 0.75);
        z-index: 9999;
        display: none;
        align-items: center;
        justify-content: center;
        transition: background 0.2s;
      }
      .mmv-modal {
        background: #fff;
        border-radius: 14px;
        max-width: 900px;
        width: 95vw;
        max-height: 90vh;
        box-shadow: 0 8px 32px rgba(0,0,0,0.18);
        display: flex;
        flex-direction: column;
        overflow: hidden;
        animation: mmv-fadein 0.2s;
      }
      @keyframes mmv-fadein {
        from { transform: scale(0.95); opacity: 0; }
        to { transform: scale(1); opacity: 1; }
      }
      .mmv-header {
        display: flex;
        align-items: center;
        justify-content: space-between;
        background: #4f46e5;
        color: #fff;
        padding: 12px 20px;
        font-size: 1.1rem;
        font-weight: 500;
      }
      .mmv-close {
        background: none;
        border: none;
        color: #fff;
        font-size: 2rem;
        cursor: pointer;
        margin-left: 10px;
        transition: color 0.2s;
      }
      .mmv-close:hover {
        color: #f87171;
      }
      .mmv-copy {
        background: #fbbf24;
        border: none;
        color: #333;
        font-size: 1rem;
        border-radius: 6px;
        padding: 6px 16px;
        margin-left: 10px;
        cursor: pointer;
        transition: background 0.2s;
      }
      .mmv-copy:hover {
        background: #fde68a;
      }
      .mmv-content {
        padding: 24px 24px 18px 24px;
        overflow-y: auto;
        background: #f8fafc;
        color: #222;
        font-size: 1.05rem;
        max-height: 70vh;
      }
      .mmv-content h1, .mmv-content h2, .mmv-content h3 {
        color: #4f46e5;
        margin-top: 1.5em;
      }
      .mmv-content table {
        border-collapse: collapse;
        width: 100%;
        margin: 1em 0;
      }
      .mmv-content th, .mmv-content td {
        border: 1px solid #d1d5db;
        padding: 8px 12px;
        text-align: left;
      }
      .mmv-content tr:nth-child(even) {
        background: #f3f4f6;
      }
      .mmv-content code {
        background: #f1f5f9;
        color: #d97706;
        padding: 2px 6px;
        border-radius: 4px;
      }
      .mmv-content pre {
        background: #1e293b;
        color: #fbbf24;
        padding: 12px;
        border-radius: 6px;
        overflow-x: auto;
      }
    `;
    return style;
  }
}
customElements.define('markdown-modal-viewer', MarkdownModalViewer);

// Función global para abrir el modal fácilmente desde cualquier botón
window.openMarkdownModal = function({src, content}) {
  let modal = document.querySelector('markdown-modal-viewer');
  if (!modal) {
    modal = document.createElement('markdown-modal-viewer');
    document.body.appendChild(modal);
  }
  if (src) {
    modal.setAttribute('src', src);
    modal.removeAttribute('content');
  } else if (content) {
    modal.setAttribute('content', content);
    modal.removeAttribute('src');
  }
  modal.open();
};

// Uso: window.openMarkdownModal({src: './archivo.md'}) o window.openMarkdownModal({content: '# Título\nTexto...'})
