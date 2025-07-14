// Componente MarkdownViewer
class MarkdownViewer extends HTMLElement {
  constructor() {
    super();
    this.attachShadow({mode: 'open'});
    this.container = document.createElement('div');
    this.container.className = 'markdown-viewer-container';
    // Agregar estilos internos al shadowRoot
    const style = document.createElement('style');
    style.textContent = `
      .markdown-viewer-container {
        background: rgba(255,255,255,0.95);
        border-radius: 12px;
        padding: 24px;
        margin: 20px 0;
        box-shadow: 0 2px 12px rgba(0,0,0,0.07);
        max-width: 900px;
        overflow-x: auto;
      }
      .markdown-viewer-container h1, .markdown-viewer-container h2, .markdown-viewer-container h3 {
        color: #4f46e5;
        margin-top: 1.5em;
      }
      .markdown-viewer-container table {
        border-collapse: collapse;
        width: 100%;
        margin: 1em 0;
      }
      .markdown-viewer-container th, .markdown-viewer-container td {
        border: 1px solid #d1d5db;
        padding: 8px 12px;
        text-align: left;
      }
      .markdown-viewer-container tr:nth-child(even) {
        background: #f3f4f6;
      }
      .markdown-viewer-container code {
        background: #f1f5f9;
        color: #d97706;
        padding: 2px 6px;
        border-radius: 4px;
      }
      .markdown-viewer-container pre {
        background: #1e293b;
        color: #fbbf24;
        padding: 12px;
        border-radius: 6px;
        overflow-x: auto;
      }
    `;
    this.shadowRoot.appendChild(style);
    this.shadowRoot.appendChild(this.container);
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
    if (window.marked) {
      this.container.innerHTML = window.marked.parse(md);
    } else {
      this.container.innerHTML = '<b>Error:</b> No se encontró la librería <code>marked</code>.';
    }
  }
}
customElements.define('markdown-viewer', MarkdownViewer);

// Uso: <markdown-viewer src="ruta/al/archivo.md"></markdown-viewer> o <markdown-viewer content="# Título\nTexto..."></markdown-viewer>
