# EJERCICIO 3: Complejidad de Código - Guía para Principiantes

## ¿Qué es la Complejidad de Código?

**Complejidad** = ¿Qué tan difícil es entender tu código?

### Ejemplo fácil:
```csharp
// SIMPLE (Complejidad = 1)
public void Saludar()
{
    Console.WriteLine("Hola!");
}

// COMPLICADO (Complejidad = 5)
public void SaludarConCondiciones(string nombre, bool esMañana, bool esVip)
{
    if (nombre != null)
    {
        if (esMañana)
        {
            if (esVip)
            {
                Console.WriteLine($"Buenos días, estimado {nombre}!");
            }
            else
            {
                Console.WriteLine($"Buenos días, {nombre}!");
            }
        }
        else
        {
            Console.WriteLine($"Hola {nombre}!");
        }
    }
}
```

---

## ¿Por qué importa la Complejidad?

### Código simple:
✅ **Fácil de leer** - Cualquiera lo entiende rápido  
✅ **Fácil de probar** - Pocos casos de prueba  
✅ **Fácil de mantener** - Cambios simples y rápidos  
✅ **Menos bugs** - Menos lugares donde pueden ocurrir errores  

### Código complejo:
❌ **Difícil de leer** - Necesitas tiempo para entenderlo  
❌ **Difícil de probar** - Muchos casos de prueba  
❌ **Difícil de mantener** - Cambios riesgosos  
❌ **Más bugs** - Muchos lugares donde pueden fallar  

---

## Complejidad Ciclomática Explicada Simple

### ¿Qué es?
Es un **número** que te dice cuántos **caminos diferentes** puede tomar tu código.

### ¿Cómo se calcula?
**Empieza con 1** y suma 1 por cada:
- `if`
- `else if`
- `while`
- `for`
- `foreach`
- `switch` (cada `case`)
- `&&` (and)
- `||` (or)
- `?` (operador ternario)

### Ejemplo paso a paso:
```csharp
public bool PuedeComprar(int edad, decimal dinero, bool tieneId)
{
    // Empezamos con 1
    
    if (edad >= 18)          // +1 = 2
    {
        if (dinero > 100)    // +1 = 3
        {
            if (tieneId)     // +1 = 4
            {
                return true;
            }
        }
    }
    return false;
}
// Complejidad Total = 4
```

---

## Escala de Complejidad

| Complejidad | Nivel | Qué significa |
|-------------|-------|---------------|
| 1-5 | 🟢 **Fácil** | Código simple, fácil de entender |
| 6-10 | 🟡 **Moderado** | Código aceptable, pero puedes mejorarlo |
| 11-15 | 🟠 **Complicado** | Código difícil, necesita refactoring |
| 16+ | 🔴 **Muy complicado** | Código muy difícil, urgente refactoring |

---

## Ejercicio Práctico: Medir Complejidad

### Paso 1: Código para analizar
Crea un archivo `ComplejidadEjemplo.cs`:

```csharp
using System;

public class ComplejidadEjemplo
{
    // MÉTODO 1: Calcula tu complejidad
    public string ClasificarEdad(int edad)
    {
        if (edad < 13)
        {
            return "Niño";
        }
        else if (edad < 18)
        {
            return "Adolescente";
        }
        else if (edad < 65)
        {
            return "Adulto";
        }
        else
        {
            return "Adulto Mayor";
        }
    }
    
    // MÉTODO 2: Calcula tu complejidad
    public bool PuedeConducir(int edad, bool tieneLicencia, bool tieneAnteojos, bool necesitaAnteojos)
    {
        if (edad >= 18)
        {
            if (tieneLicencia)
            {
                if (necesitaAnteojos)
                {
                    if (tieneAnteojos)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    // MÉTODO 3: Calcula tu complejidad
    public decimal CalcularDescuento(decimal precio, string tipoCliente, bool esFinDeSemana)
    {
        decimal descuento = 0;
        
        if (tipoCliente == "VIP")
        {
            descuento = 0.20m;
        }
        else if (tipoCliente == "Premium")
        {
            descuento = 0.15m;
        }
        else if (tipoCliente == "Regular")
        {
            descuento = 0.10m;
        }
        
        if (esFinDeSemana && descuento > 0)
        {
            descuento += 0.05m;
        }
        
        return precio * descuento;
    }
}
```

### Paso 2: Calcular complejidad manualmente
Para cada método, cuenta:
1. **Empezar con 1**
2. **Sumar 1** por cada `if`, `else if`, `&&`, `||`
3. **Anotar el total**

**Tus respuestas:**
- `ClasificarEdad`: ____
- `PuedeConducir`: ____
- `CalcularDescuento`: ____

### Paso 3: Verificar con Visual Studio
1. **Compilar** el proyecto
2. **Ir a** Analyze → Calculate Code Metrics
3. **Comparar** tus cálculos con los resultados

---

## Técnicas para Reducir Complejidad

### 1. Guard Clauses (Validaciones tempranas)

**❌ Antes (Complejidad = 4):**
```csharp
public void ProcesarUsuario(string nombre, int edad)
{
    if (nombre != null)
    {
        if (edad > 0)
        {
            if (edad < 120)
            {
                Console.WriteLine($"Procesando {nombre}, edad {edad}");
            }
        }
    }
}
```

**✅ Después (Complejidad = 1):**
```csharp
public void ProcesarUsuario(string nombre, int edad)
{
    if (nombre == null) return;
    if (edad <= 0) return;
    if (edad >= 120) return;
    
    Console.WriteLine($"Procesando {nombre}, edad {edad}");
}
```

### 2. Extraer Métodos

**❌ Antes (Complejidad = 8):**
```csharp
public bool PuedeComprar(int edad, decimal dinero, bool tieneId, string producto)
{
    if (edad >= 18)
    {
        if (dinero > 50)
        {
            if (tieneId)
            {
                if (producto == "alcohol" || producto == "tabaco")
                {
                    if (edad >= 21)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
    }
    return false;
}
```

**✅ Después (Complejidad = 2 + 2 = 4 total):**
```csharp
public bool PuedeComprar(int edad, decimal dinero, bool tieneId, string producto)
{
    if (!CumpleRequisitosBasicos(edad, dinero, tieneId))
        return false;
    
    return PuedeComprarProducto(edad, producto);
}

private bool CumpleRequisitosBasicos(int edad, decimal dinero, bool tieneId) // Complejidad = 2
{
    return edad >= 18 && dinero > 50 && tieneId;
}

private bool PuedeComprarProducto(int edad, string producto) // Complejidad = 2
{
    if (producto == "alcohol" || producto == "tabaco")
    {
        return edad >= 21;
    }
    return true;
}
```

### 3. Usar Switch en lugar de múltiples if

**❌ Antes (Complejidad = 4):**
```csharp
public string ObtenerDiaSemana(int dia)
{
    if (dia == 1)
        return "Lunes";
    else if (dia == 2)
        return "Martes";
    else if (dia == 3)
        return "Miércoles";
    else
        return "Desconocido";
}
```

**✅ Después (Complejidad = 4, pero más claro):**
```csharp
public string ObtenerDiaSemana(int dia)
{
    return dia switch
    {
        1 => "Lunes",
        2 => "Martes",
        3 => "Miércoles",
        _ => "Desconocido"
    };
}
```

---

## Ejercicio de Refactoring

### Código a mejorar:
```csharp
public class SistemaCalificaciones
{
    public string CalcularCalificacion(int puntos, bool esExamen, bool entregaATiempo, 
                                     bool participaEnClase, int faltas)
    {
        if (puntos >= 0 && puntos <= 100)
        {
            if (esExamen)
            {
                if (puntos >= 90)
                {
                    if (entregaATiempo)
                    {
                        return "A+";
                    }
                    else
                    {
                        return "A";
                    }
                }
                else if (puntos >= 80)
                {
                    if (participaEnClase && faltas < 3)
                    {
                        return "B+";
                    }
                    else
                    {
                        return "B";
                    }
                }
                else if (puntos >= 70)
                {
                    if (faltas < 5)
                    {
                        return "C";
                    }
                    else
                    {
                        return "D";
                    }
                }
                else
                {
                    return "F";
                }
            }
            else
            {
                if (puntos >= 85)
                {
                    return "A";
                }
                else if (puntos >= 70)
                {
                    return "B";
                }
                else if (puntos >= 60)
                {
                    return "C";
                }
                else
                {
                    return "F";
                }
            }
        }
        return "Error";
    }
}
```

### Tu tarea:
1. **Calcula** la complejidad actual
2. **Identifica** qué hace el código
3. **Refactoriza** usando las técnicas aprendidas
4. **Verifica** que la complejidad sea menor

### Pistas para mejorar:
- Usa **Guard Clauses** para validaciones
- **Extrae métodos** para casos específicos
- **Simplifica** las condiciones anidadas

---

## Herramientas para Medir Complejidad

### En Visual Studio:
1. **Analyze** → **Calculate Code Metrics**
2. **Resultados** en ventana Code Metrics
3. **Ordenar** por complejidad ciclomática

### Extensiones útiles:
- **SonarLint**: Marca métodos complejos
- **Code Metrics**: Análisis en tiempo real
- **Refactoring Tools**: Ayuda con mejoras

### Métricas importantes:
- **Complejidad Ciclomática**: < 10 por método
- **Líneas de código**: < 50 por método
- **Número de parámetros**: < 5 por método

---

## Consejos Prácticos

### Para principiantes:
1. **Empieza simple**: Métodos que hagan una sola cosa
2. **Cuenta los if**: Si tienes más de 3-4, considera refactoring
3. **Pregúntate**: ¿Puedo explicar este método en una oración?
4. **Divide y conquista**: Métodos grandes en métodos pequeños

### Reglas de oro:
- **Un método = una responsabilidad**
- **Máximo 3 niveles de anidación**
- **Si usas "y" al describir un método, probablemente haga demasiado**
- **Los nombres deben ser claros y específicos**

### Cuándo refactorizar:
- **Complejidad > 10**: Urgente
- **Complejidad 6-10**: Cuando tengas tiempo
- **Método > 50 líneas**: Muy probable que esté haciendo demasiado
- **Difícil de testear**: Señal de alta complejidad

---

## Ejercicio Final

### Crea tu propio análisis:
1. **Toma** un método que hayas escrito recientemente
2. **Calcula** su complejidad ciclomática
3. **Identifica** oportunidades de mejora
4. **Refactoriza** usando las técnicas aprendidas
5. **Mide** nuevamente y compara

### Reflexiona:
- ¿El código mejorado es más fácil de entender?
- ¿Sería más fácil de probar?
- ¿Otro desarrollador lo entendería rápidamente?

---

**¡Felicitaciones!** 🎉 Ya sabes medir y mejorar la complejidad de tu código.

**Siguiente ejercicio**: Crear un checklist de calidad para todos tus proyectos.