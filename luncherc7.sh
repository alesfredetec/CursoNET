#!/bin/bash
# Launcher multiplataforma para Context7 (Bash/WSL o PowerShell/Windows)
# Uso: ./luncherc7.sh "palabra_clave"

QUERY="$1"
if [ -z "$QUERY" ]; then
  echo "[luncherc7] Uso: ./luncherc7.sh <nombre_libreria>"
  exit 1
fi

# Detectar si estamos en WSL/Linux o Windows
if grep -qi microsoft /proc/version 2>/dev/null; then
  # WSL/Linux
  if [ -x ./context7_query_creativo.sh ]; then
    ./context7_query_creativo.sh "$QUERY"
  else
    echo "[luncherc7] No se encontró context7_query_creativo.sh en el directorio actual."
    exit 2
  fi
else
  # Windows (PowerShell)
  if [ -f ./context7_query_creativo.ps1 ]; then
    pwsh -NoProfile -Command "& './context7_query_creativo.ps1' -query '$QUERY'"
  else
    echo "[luncherc7] No se encontró context7_query_creativo.ps1 en el directorio actual."
    exit 2
  fi
fi
