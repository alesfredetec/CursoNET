class MarkdownDownloadButton extends HTMLElement {
  constructor() {
    super();
    const shadow = this.attachShadow({ mode: 'open' });
    const src = this.getAttribute('src');
    const label =   '⬇️';
    const button = document.createElement('a');
    button.href = src;
    button.download = '';
    button.textContent = label;
    button.setAttribute('class', 'download-md-btn');
    button.setAttribute('style', `margin-left:1px;padding:2px 2px;border-radius:6px;background:#22c55e;color:#fff;border:none;cursor:pointer;font-size:1rem;text-decoration:none;display:inline-block;`);
    const style = document.createElement('style');
    style.textContent = `
      .download-md-btn:hover {
        background: #16a34a;
        color: #fff;
        text-decoration: none;
      }
    `;
    shadow.appendChild(style);
    shadow.appendChild(button);
  }
}
customElements.define('markdown-download-button', MarkdownDownloadButton);
