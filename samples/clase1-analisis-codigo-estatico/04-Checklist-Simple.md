# EJERCICIO 4: Checklist de Calidad - Guía para Principiantes

## ¿Qué es un Checklist de Calidad?

Es una **lista de verificación** que usas antes de entregar tu código. Como una lista de tareas que debes completar para asegurar que tu código sea bueno.

### ¿Por qué usar un checklist?
- ✅ **No olvidas** revisar cosas importantes
- ✅ **Entregas** código más profesional
- ✅ **Evitas** errores comunes
- ✅ **Ahorras tiempo** en correcciones después

---

## Checklist Básico para Principiantes

### 📋 Antes de entregar tu código, verifica:

#### 1. **Funcionalidad** ✅
- [ ] El código hace lo que se pidió
- [ ] Probé todas las funciones principales
- [ ] Funciona con datos normales
- [ ] Funciona con casos extremos (números muy grandes, textos vacíos, etc.)

#### 2. **SonarLint** ✅
- [ ] Instalé SonarLint en Visual Studio
- [ ] No hay líneas subrayadas en rojo
- [ ] Resolví todas las advertencias importantes
- [ ] El Error List muestra 0 problemas de SonarLint

#### 3. **Nombres y Claridad** ✅
- [ ] Los nombres de clases empiezan con mayúscula: `CustomerService`
- [ ] Los nombres de métodos empiezan con mayúscula: `CalcularDescuento`
- [ ] Los nombres de variables empiezan con minúscula: `customerName`
- [ ] Los nombres explican qué hace cada cosa
- [ ] No hay nombres como `a`, `temp`, `data` sin explicación

#### 4. **Validaciones** ✅
- [ ] Valido que los parámetros no sean null
- [ ] Valido rangos de números (edad no negativa, etc.)
- [ ] Valido que las cadenas no estén vacías cuando sea necesario
- [ ] Manejo errores con try-catch cuando es necesario

#### 5. **Complejidad** ✅
- [ ] Mis métodos no tienen más de 3-4 if anidados
- [ ] Cada método hace una sola cosa
- [ ] Puedo explicar qué hace cada método en una oración
- [ ] No hay métodos de más de 30-40 líneas

#### 6. **Formato y Estilo** ✅
- [ ] El código está bien indentado
- [ ] Uso espacios consistentes
- [ ] No hay líneas muy largas (más de 120 caracteres)
- [ ] Agrupé código relacionado

---

## Checklist Detallado por Categorías

### 🔧 **Configuración del Proyecto**
```
□ SonarLint instalado y funcionando
□ Proyecto compila sin errores
□ Proyecto compila sin warnings importantes
□ Referencias/NuGet packages actualizados
□ .gitignore configurado correctamente
```

### 📝 **Calidad del Código**
```
□ Uso PascalCase para clases y métodos
□ Uso camelCase para variables y parámetros
□ Nombres descriptivos y claros
□ Sin código comentado (código que no se usa)
□ Comentarios solo donde es realmente necesario
```

### 🛡️ **Validaciones y Seguridad**
```
□ Validación de parámetros null
□ Validación de rangos y valores
□ Manejo apropiado de excepciones
□ No hay información sensible en el código
□ Uso de string.IsNullOrWhiteSpace() para strings
```

### 📊 **Rendimiento**
```
□ Uso interpolación de strings: $"Hola {nombre}"
□ No concateno strings en loops
□ Uso StringBuilder para múltiples concatenaciones
□ Cierro recursos (using statements)
□ No hay código duplicado
```

### 🧪 **Pruebas y Funcionalidad**
```
□ Probé con datos normales
□ Probé con datos extremos
□ Probé con datos inválidos
□ Todas las funciones principales funcionan
□ La aplicación no se cuelga
```

---

## Ejemplo Práctico: Aplicar el Checklist

### Código para revisar:
```csharp
public class calculadora
{
    public double dividir(double a, double b)
    {
        return a / b;
    }
    
    public string obtenerSaludo(string nombre)
    {
        return "Hola " + nombre + "!";
    }
    
    public bool esValido(bool valor)
    {
        if (valor == true)
            return true;
        else
            return false;
    }
}
```

### Aplicando el checklist:

#### ❌ **Problemas encontrados:**
1. **Nombres**: `calculadora` debería ser `Calculadora`
2. **Nombres**: `dividir` debería ser `Dividir`
3. **Nombres**: `obtenerSaludo` debería ser `ObtenerSaludo`
4. **Validación**: `dividir` no valida división por cero
5. **Rendimiento**: `obtenerSaludo` usa concatenación lenta
6. **Lógica**: `esValido` tiene comparación innecesaria

#### ✅ **Código corregido:**
```csharp
public class Calculadora
{
    public double Dividir(double a, double b)
    {
        if (b == 0)
            throw new ArgumentException("No se puede dividir por cero");
        
        return a / b;
    }
    
    public string ObtenerSaludo(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío");
        
        return $"Hola {nombre}!";
    }
    
    public bool EsValido(bool valor)
    {
        return valor;
    }
}
```

---

## Herramientas para Automatizar el Checklist

### 1. **SonarLint** (Automático)
- Detecta problemas de naming
- Identifica code smells
- Sugiere mejoras de rendimiento

### 2. **Code Cleanup en Visual Studio**
- **Cómo usar**: Clic derecho → Code Cleanup
- **Qué hace**: Formatea código automáticamente
- **Configura**: Tools → Options → Code Cleanup

### 3. **EditorConfig**
Crea un archivo `.editorconfig`:
```ini
[*.cs]
# Indentación
indent_style = space
indent_size = 4

# Fin de línea
end_of_line = crlf
insert_final_newline = true

# Naming rules
dotnet_naming_rule.classes_should_be_pascalcase.rule = pascalcase
dotnet_naming_rule.methods_should_be_pascalcase.rule = pascalcase
```

### 4. **Snippets para Validaciones**
Crea snippets para validaciones comunes:
```csharp
// Snippet para validar null
if (parameter == null)
    throw new ArgumentNullException(nameof(parameter));

// Snippet para validar string vacío
if (string.IsNullOrWhiteSpace(text))
    throw new ArgumentException("Text cannot be empty", nameof(text));
```

---

## Checklist para Diferentes Tipos de Código

### 🌐 **Para Aplicaciones Web (ASP.NET)**
```
□ Validación de inputs del usuario
□ Manejo de errores HTTP
□ No hay información sensible en logs
□ Uso de HTTPS en producción
□ Validación de autorización
```

### 📱 **Para Aplicaciones de Escritorio**
```
□ Manejo de eventos de UI
□ Validación de formularios
□ Manejo de hilos (threading)
□ Liberación de recursos
□ Experiencia de usuario fluida
```

### 📊 **Para APIs**
```
□ Documentación de endpoints
□ Validación de parámetros
□ Códigos de estado HTTP correctos
□ Manejo de errores consistente
□ Versionado de API
```

### 🗃️ **Para Código de Base de Datos**
```
□ Parámetros en consultas (no concatenación)
□ Manejo de transacciones
□ Cierre de conexiones
□ Validación de datos
□ Índices apropiados
```

---

## Creando tu Checklist Personal

### Paso 1: Identifica tus errores comunes
- **Mira** tu código anterior
- **Identifica** qué problemas se repiten
- **Agrega** esos puntos a tu checklist

### Paso 2: Adapta según tu proyecto
- **Web**: Agrega validaciones de seguridad
- **Desktop**: Agrega manejo de UI
- **API**: Agrega documentación de endpoints

### Paso 3: Evoluciona tu checklist
- **Agrega** nuevos puntos cuando aprendas
- **Quita** puntos que ya dominas completamente
- **Actualiza** según las mejores prácticas

---

## Ejemplo de Checklist Personalizado

### Mi Checklist - Aplicación Web
```
🔧 CONFIGURACIÓN:
□ SonarLint: 0 problemas
□ Compila sin errores
□ Packages NuGet actualizados

📝 CÓDIGO:
□ Nombres en PascalCase/camelCase
□ Métodos < 30 líneas
□ Complejidad < 10
□ Sin código duplicado

🛡️ VALIDACIONES:
□ Inputs de usuario validados
□ Parámetros null verificados
□ Rangos de números validados
□ Manejo de excepciones

🌐 WEB ESPECÍFICO:
□ Autorización en endpoints
□ Validación de modelos
□ Manejo de errores HTTP
□ Logs sin información sensible

🧪 PRUEBAS:
□ Casos normales probados
□ Casos extremos probados
□ Casos de error probados
□ Performance aceptable
```

---

## Integrando el Checklist en tu Workflow

### 🔄 **Proceso sugerido:**

1. **Antes de empezar**: Revisa el checklist para recordar qué hacer
2. **Durante desarrollo**: Usa SonarLint para detectar problemas inmediatamente
3. **Antes de commit**: Pasa por todo el checklist
4. **Antes de entregar**: Revisión final completa

### 📅 **Frecuencia:**
- **Diario**: SonarLint y formato básico
- **Por feature**: Checklist completo
- **Por release**: Revisión exhaustiva

### 👥 **En equipo:**
- **Comparte** tu checklist con el equipo
- **Haz** code reviews usando el checklist
- **Actualiza** el checklist basado en experiencias

---

## Checklist de Emergencia (Mínimo)

### Cuando tienes muy poco tiempo:
```
□ Compila sin errores
□ SonarLint: 0 problemas críticos
□ Nombres de métodos/clases correctos
□ Validar parámetros null
□ Funciona con datos de prueba
```

### 🚨 **Señales de alarma:**
- Más de 10 problemas de SonarLint
- Métodos de más de 50 líneas
- Nombres como `Method1`, `temp`, `data`
- Aplicación se cuelga con datos normales
- Más de 5 niveles de if anidados

---

## Ejercicio Final: Crear tu Checklist

### Tu tarea:
1. **Toma** un proyecto tuyo existente
2. **Aplica** el checklist básico
3. **Identifica** 3-5 problemas comunes en tu código
4. **Crea** tu checklist personalizado
5. **Úsalo** en tu próximo proyecto

### Plantilla para tu checklist:
```
MI CHECKLIST DE CALIDAD

🔧 CONFIGURACIÓN:
□ _________________
□ _________________

📝 CÓDIGO:
□ _________________
□ _________________

🛡️ VALIDACIONES:
□ _________________
□ _________________

🧪 PRUEBAS:
□ _________________
□ _________________

📋 ESPECÍFICO DE MI PROYECTO:
□ _________________
□ _________________
```

---

**¡Felicitaciones!** 🎉 Ya tienes las herramientas para crear código de calidad profesional.

**Recuerda**: La calidad del código es un hábito. Úsalo consistentemente y verás la diferencia.

**Próximo paso**: Aplicar todo lo aprendido en un proyecto real.