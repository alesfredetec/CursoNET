@echo off
echo 🔧 Compilando el proyecto...
dotnet build

if %errorlevel% neq 0 (
    echo ❌ Error en la compilacion. Revise los errores arriba.
    pause
    exit /b 1
)

echo ✅ Compilacion exitosa!
echo.
echo 🚀 Ejecutando el programa...
echo.
dotnet run

pause