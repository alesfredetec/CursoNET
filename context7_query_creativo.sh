#!/bin/bash
# Uso: ./context7_query_creativo.sh "palabra_clave"
if ! command -v jq >/dev/null 2>&1; then
  echo "❌ Dependencia faltante: 'jq' no está instalado.\nPor favor, ejecuta: sudo apt update && sudo apt install -y jq" >&2
  exit 2
fi
# --- Validación de conectividad a Internet/Context7 ---
 
if [ -z "$1" ]; then
  echo '{"error":"Uso: context7_query_creativo.sh <nombre_libreria>"}'
  exit 1
fi

query="$1"
timestamp=$(date +"%Y%m%d_%H%M%S")
file="context7_response_${timestamp}.md"

search_url="https://context7.com/api/v1/search?query=$(printf '%s' "$query" | jq -sRr @uri)"
id=$(curl -s "$search_url" | jq -r '.results[0].id // empty')

if [ -n "$id" ]; then
  docs_url="https://context7.com/api/v1${id}"
  content=$(curl -s "$docs_url")
  {
    echo "# 📚 Consulta Context7: *$query*"
    echo ""
    echo "> Generado: $(date '+%Y-%m-%d %H:%M:%S')"
    echo "> Archivo: \`$file\`"
    echo ""
    echo "---"
    echo ""
    echo "$content"
    echo ""
    echo "---"
    echo "_¿Te fue útil? ¡Comparte este snippet con tu equipo!_"
  } > "$file"
  cat "$file"
  command -v paplay >/dev/null && paplay /usr/share/sounds/freedesktop/stereo/complete.oga || echo -e "\a"
else
  {
    echo "# ❌ Error"
    echo "No se encontró la librería '$query' en Context7."
    echo ""
    echo "_¿Quizá quisiste decir otra cosa?_"
  } | tee "$file"
  echo -e "\a"
fi
