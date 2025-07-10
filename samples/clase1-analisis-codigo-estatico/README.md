# Clase 1: Análisis de Código Estático

## 📋 Descripción
Esta carpeta contiene ejemplos prácticos para dominar el análisis estático de código usando SonarLint, métricas de complejidad ciclomática y técnicas de refactoring.

## 🎯 Objetivos de Aprendizaje
- Configurar y usar SonarLint efectivamente
- Calcular e interpretar complejidad ciclomática
- Identificar y resolver code smells
- Aplicar mejores prácticas de calidad de código

## 📁 Estructura de Archivos

### Ejercicio 1: Problemas Básicos con SonarLint
- `01-BadCodeExample.cs` - Código con múltiples problemas detectados por SonarLint
- `01-BadCodeExample-Fixed.cs` - Versión corregida con explicaciones detalladas

### Ejercicio 2: UserProfile con Issues Complejos
- `02-UserProfile-Before.cs` - Código del ejercicio con múltiples problemas
- `02-UserProfile-After.cs` - Código refactorizado siguiendo mejores prácticas

### Ejercicio 3: Configuración y Setup
- `03-SonarLint-Setup.md` - Guía completa de instalación y configuración
- `03-EditorConfig-Example.editorconfig` - Archivo de configuración de reglas

### Ejercicio 4: Métricas y Análisis
- `04-ComplexityAnalysis.cs` - Ejemplos para calcular complejidad ciclomática
- `04-MetricsReport.md` - Interpretación de métricas de código

## 🔧 Herramientas Requeridas
- Visual Studio 2019+ con SonarLint
- .NET 6.0+
- Analizadores NuGet (SonarAnalyzer.CSharp)

## 💡 Tips para Desarrolladores
1. **Instala SonarLint desde el día 1** - Prevenir es mejor que corregir
2. **Usa Quick Actions (Alt+Enter)** - Visual Studio resuelve muchos problemas automáticamente
3. **Configura reglas de equipo** - Consistencia con .editorconfig
4. **No ignores warnings** - Cada advertencia es una oportunidad de mejora

## 📚 Fuentes de Consulta
- [SonarLint Documentation](https://www.sonarlint.org/visualstudio)
- [SonarQube C# Rules](https://rules.sonarsource.com/csharp)
- [Microsoft Code Analysis](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/)
- [Clean Code Principles](https://blog.cleancoder.com/)

## 🎯 Próximos Pasos
- Clase 2: Análisis de Requisitos
- Aplicar técnicas en proyectos reales
- Configurar CI/CD con quality gates