# EJERCICIO 2: Configurar SonarLint - Guía para Principiantes

## ¿Qué es SonarLint?

**SonarLint** es como un corrector ortográfico, pero para código. Te ayuda a:
- 🔍 Encontrar errores antes de que causen problemas
- 📝 Escribir código más limpio y fácil de leer
- 🚀 Mejorar el rendimiento de tus aplicaciones
- 🛡️ Evitar problemas de seguridad

---

## Instalación Paso a Paso

### Paso 1: Abrir Visual Studio
1. Abre Visual Studio 2019 o 2022
2. Si no lo tienes, descarga Visual Studio Community (es gratis)

### Paso 2: Instalar SonarLint
1. Ve al menú **Extensions** → **Manage Extensions**
2. En la pestaña **Online**, busca "SonarLint"
3. Haz clic en **Download** en "SonarLint for Visual Studio"
4. **Cierra Visual Studio** para que se instale
5. Reinicia Visual Studio

### Paso 3: Verificar que funciona
1. Abre cualquier proyecto de C#
2. Ve a **View** → **Error List**
3. Deberías ver una pestaña nueva llamada **SonarLint**

---

## Tu Primer Análisis

### Ejercicio Práctico
Vamos a crear un archivo con problemas para que SonarLint los detecte:

1. **Crea un nuevo archivo** llamado `PruebaCalidad.cs`
2. **Copia este código** (tiene problemas a propósito):

```csharp
using System;

public class PruebaCalidad
{
    private string dato; // Variable que no se usa
    
    public void metodo(string texto) // Nombre incorrecto
    {
        if (texto == null)
        {
            // No hacer nada
        }
        
        Console.WriteLine("Hola" + ", " + texto); // Forma lenta
    }
    
    public bool EsValido(bool valor)
    {
        if (valor == true) // Comparación innecesaria
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
```

3. **Guarda el archivo** (Ctrl+S)

### ¿Qué deberías ver?
- **Líneas subrayadas** de colores (rojo, amarillo, azul)
- **Números en la barra lateral** indicando problemas
- **Lista de problemas** en Error List

---

## Entendiendo los Problemas

### Colores de SonarLint:
- 🔴 **Rojo**: Errores graves o bugs
- 🟡 **Amarillo**: Advertencias o code smells
- 🔵 **Azul**: Sugerencias de mejora

### Ejemplo de problemas típicos:

#### 1. Campo no usado
```csharp
private string dato; // ❌ SonarLint: "Remove this unused private field"
```
**Solución**: Eliminar si no se usa, o usarlo en el código.

#### 2. Nombre incorrecto
```csharp
public void metodo(string texto) // ❌ Debería ser "Metodo"
```
**Solución**: Cambiar a PascalCase: `public void Metodo(string texto)`

#### 3. Concatenación lenta
```csharp
Console.WriteLine("Hola" + ", " + texto); // ❌ Forma lenta
```
**Solución**: Usar interpolación: `Console.WriteLine($"Hola, {texto}");`

#### 4. Comparación innecesaria
```csharp
if (valor == true) // ❌ Redundante
```
**Solución**: Usar directamente: `if (valor)`

---

## Arreglar Problemas Fácilmente

### Opción 1: Alt+Enter (Recomendado)
1. **Coloca el cursor** en la línea con problema
2. **Presiona Alt+Enter**
3. **Selecciona la corrección** sugerida
4. **Presiona Enter** para aplicar

### Opción 2: Menú contextual
1. **Clic derecho** en la línea con problema
2. **Selecciona "Quick Actions and Refactorings"**
3. **Elige la corrección**

### Opción 3: Bombilla
1. **Haz clic** en la bombilla 💡 que aparece
2. **Selecciona la corrección**

---

## Configuración Básica

### Cambiar nivel de análisis
1. Ve a **Tools** → **Options**
2. Busca **SonarLint**
3. Puedes cambiar:
   - **Nivel de análisis**: Básico, Completo
   - **Idioma**: Para mensajes en español
   - **Reglas activas**: Qué problemas detectar

### Archivo .editorconfig (Opcional)
Crea un archivo `.editorconfig` en la raíz de tu proyecto:

```ini
# Configuración básica para C#
[*.cs]
# Espacios vs tabs
indent_style = space
indent_size = 4

# Fin de línea
end_of_line = crlf

# Convenciones de nombres
dotnet_naming_rule.methods_should_be_pascal_case.rule = pascal_case
dotnet_naming_rule.methods_should_be_pascal_case.symbols = method_symbols
dotnet_naming_rule.methods_should_be_pascal_case.style = pascal_case_style

dotnet_naming_symbols.method_symbols.applicable_kinds = method
dotnet_naming_style.pascal_case_style.capitalization = pascal_case
```

---

## Ejercicio de Práctica

### Código para arreglar:
Crea un nuevo archivo `EjercicioPractica.cs` y copia este código:

```csharp
using System;

public class EjercicioPractica
{
    private string region = "Norte";
    private int numero;
    
    public void procesarDatos(string info)
    {
        var mensaje = "Procesando";
        
        if (info != null)
        {
            Console.WriteLine(mensaje + ": " + info);
        }
        
        var resultado = calcularValor();
        Console.WriteLine(resultado);
    }
    
    private int calcularValor()
    {
        return 42;
    }
    
    public bool validarEstado(bool activo)
    {
        if (activo == true)
        {
            return true;
        }
        return false;
    }
}
```

### Tu tarea:
1. **Identifica** todos los problemas que marca SonarLint
2. **Cuenta** cuántos problemas hay
3. **Arregla** cada problema usando Alt+Enter
4. **Verifica** que llegues a 0 problemas

### Problemas esperados:
- Campo `region` no usado
- Campo `numero` no usado
- Nombre `procesarDatos` debería ser `ProcesarDatos`
- Concatenación lenta de strings
- Comparación innecesaria con `true`
- Método `calcularValor` debería ser `CalcularValor`

---

## Consejos para Principiantes

### 1. No te agobies
- **Empieza poco a poco**: Arregla un problema a la vez
- **Pregunta si no entiendes**: Es normal no entender todo al principio
- **Practica regularmente**: Úsalo en todos tus proyectos

### 2. Prioriza los problemas
- **Primero los rojos**: Bugs y errores graves
- **Después los amarillos**: Problemas de rendimiento
- **Por último los azules**: Mejoras de estilo

### 3. Aprende los patrones
- **Nombres en PascalCase**: `MiMetodo`, `MiPropiedad`
- **Validar parámetros**: Siempre verificar null
- **Usar interpolación**: `$"Hola {nombre}"` es mejor que `"Hola " + nombre`

### 4. Configura tu entorno
- **Instala SonarLint desde el día 1**
- **Activa Error List automáticamente**
- **Usa .editorconfig para consistencia**

---

## Problemas Comunes y Soluciones

### "No veo problemas de SonarLint"
- **Verifica** que esté instalado: Extensions → Manage Extensions
- **Reinicia** Visual Studio completamente
- **Compila** el proyecto: Build → Build Solution

### "Hay demasiados problemas"
- **Empieza** por archivos pequeños
- **Filtra** por tipo: Solo errores primero
- **Arregla** uno por vez, no todos juntos

### "No entiendo qué significa un problema"
- **Busca** el código del problema (ej: S1144)
- **Lee** la documentación: https://rules.sonarsource.com/csharp
- **Pregunta** en foros o a compañeros

---

## Próximos Pasos

### Después de dominar lo básico:
1. **Configura reglas personalizadas** para tu equipo
2. **Integra** con tu sistema de control de versiones
3. **Aprende** sobre métricas de código (complejidad, duplicación)
4. **Explora** SonarQube para análisis más avanzados

### Mantén el hábito:
- **Revisa** SonarLint antes de hacer commit
- **Corrige** problemas tan pronto como aparezcan
- **Comparte** conocimiento con tu equipo

---

**¡Felicitaciones!** 🎉 Ya sabes usar SonarLint para escribir código más limpio y profesional.

**Siguiente ejercicio**: Análisis de métricas de código y complejidad ciclomática.