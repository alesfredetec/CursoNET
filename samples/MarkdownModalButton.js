// MarkdownModalButton.js
// Botón reutilizable para abrir un modal de Markdown con solo el path
// Requiere MarkdownModalViewer.js y marked.js

class MarkdownModalButton extends HTMLElement {
  constructor() {
    super();
    this.attachShadow({mode: 'open'});
    const btn = document.createElement('button');
    btn.className = 'mmv-btn';
    btn.innerHTML = '👁️';
    btn.onclick = () => {
      const path = this.getAttribute('src');
      if (path) window.openMarkdownModal({src: path});
    };
    const style = document.createElement('style');
    style.textContent = `
      .mmv-btn {
        margin-top: 1px;
        padding: 2px 2px;
        border-radius: 1px;
        background: #4f46e5;
        color: #fff;
        border: none;
        cursor: pointer;
        font-size: 1rem;
        transition: background 0.2s;
      }
      .mmv-btn:hover {
        background: #6366f1;
      }
    `;
    this.shadowRoot.appendChild(style);
    this.shadowRoot.appendChild(btn);
  }
  static get observedAttributes() { return ['src', 'label']; }
  attributeChangedCallback(name, oldValue, newValue) {
    if (name === 'label' && this.shadowRoot) {
      this.shadowRoot.querySelector('button').innerHTML = '👁️';
    }
  }
}
customElements.define('markdown-modal-button', MarkdownModalButton);
// Uso: <markdown-modal-button src="./archivo.md" label="👁️ Ver en modal"></markdown-modal-button>
