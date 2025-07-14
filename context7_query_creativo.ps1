# Uso: .\context7_query_creativo.ps1 "palabra_clave"

# --- Validación de conectividad a Internet/Context7 ---
try {
    $null = Invoke-WebRequest -Uri "https://context7.com" -Method Head -TimeoutSec 5 -ErrorAction Stop
} catch {
    Write-Host "❌ No hay conexión a Internet o el servicio Context7 no responde." -ForegroundColor Red
    exit 3
}

param(
  [Parameter(Mandatory=$true)]
  [string]$query
)

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$file = "context7_response_${timestamp}.md"

$searchUrl = "https://context7.com/api/v1/search?query=$([uri]::EscapeDataString($query))"
$searchResult = Invoke-RestMethod -Uri $searchUrl -UseBasicParsing
if ($searchResult.results -and $searchResult.results.Count -gt 0) {
    $id = $searchResult.results[0].id
    $docsUrl = "https://context7.com/api/v1$id"
    $content = Invoke-RestMethod -Uri $docsUrl -UseBasicParsing
    $header = @"
# 📚 Consulta Context7: *$query*

> Generado: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
> Archivo: `$file`

---

"@
    $footer = @"

---

_¿Te fue útil? ¡Comparte este snippet con tu equipo!_
"@
    $header | Out-File -Encoding utf8 $file
    $content | Out-File -Encoding utf8 -Append $file
    $footer | Out-File -Encoding utf8 -Append $file
    Get-Content $file
    [console]::beep(1000,300)
} else {
    $msg = @"
# ❌ Error
No se encontró la librería '$query' en Context7.

_¿Quizá quisiste decir otra cosa?_
"@
    $msg | Out-File -Encoding utf8 $file
    Write-Output $msg
    [console]::beep(400,500)
}
