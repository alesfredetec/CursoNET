# Ejercicio Integrador Jr: Sistema de Límites Dinámicos - Fintexa

## Historia Empresarial

### El Contexto de Crisis de Fraude

**Junio 2024 - Centro de Seguridad de Fintexa, Alerta Roja**

Rodrigo Vega, Head of Risk Management, miraba con preocupación el dashboard de seguridad que mostraba números alarmantes en tiempo real. En las últimas 4 semanas, **los fraudes habían aumentado un 300%**, y el sistema de límites fijos estaba demostrando ser completamente inadecuado para el panorama actual de amenazas.

"Nuestros límites estáticos están permitiendo que los fraudsters operen libremente, mientras bloquean a usuarios legítimos", murmuró mientras revisaba los reportes de la madrugada.

### Los Protagonistas

**Rodrigo Vega - Head of Risk Management**: Responsable de prevenir fraude financiero, bajo presión extrema por el aumento exponencial de actividad fraudulenta.

**Elena Castro - Senior Security Engineer**: Especialista en sistemas de detección de fraude, frustrada por las limitaciones del sistema de límites fijos.

**Pablo Hernández - Data Scientist**: Analiza patrones de comportamiento de usuarios para definir niveles de riesgo dinámicos.

**Sofía Ramírez - Desarrolladora Jr**: Asignada para implementar el sistema de límites dinámicos usando State Pattern. Su proyecto más complejo y de mayor impacto hasta ahora.

### La Crisis de los Límites Estáticos

El sistema actual de Fintexa utilizaba **límites fijos para todos los usuarios**, una aproximación que había funcionado cuando procesaban 5,000 transacciones diarias, pero que ahora con **45,000+ transacciones diarias** generaba dos problemas críticos:

1. **Usuarios legítimos bloqueados**: Límites muy restrictivos frustraban a usuarios genuinos
2. **Fraudsters pasando inadvertidos**: Límites muy permisivos permitían actividad fraudulenta

### El Problema Actual

**El Sistema de Límites Rígido:**

```csharp
public class CurrentLimitSystem
{
    // Límites fijos para TODOS los usuarios
    private const decimal DAILY_LIMIT = 5000m;
    private const decimal TRANSACTION_LIMIT = 1000m;
    private const int MAX_TRANSACTIONS_PER_DAY = 10;

    public bool IsTransactionAllowed(decimal amount, User user)
    {
        // Mismos límites para usuario de 5 años vs usuario de 1 día
        return amount <= TRANSACTION_LIMIT && 
               user.DailySpent <= DAILY_LIMIT &&
               user.TransactionsToday <= MAX_TRANSACTIONS_PER_DAY;
    }
}
```

### Las Métricas del Desastre

Los números eran devastadores:
- **300% aumento** en actividad fraudulenta en 4 semanas
- **$1.2M USD perdidos** por fraudes no detectados
- **2,800 usuarios legítimos** bloqueados semanalmente
- **67% de usuarios nuevos** abandonan tras primer bloqueo
- **43 minutos promedio** para resolver falsos positivos
- **$340,000 USD** en reembolsos por fraudes exitosos

### La Necesidad de Límites Inteligentes

Durante la reunión de crisis de junio, Rodrigo fue claro: "Necesitamos límites que **se adapten al comportamiento del usuario**. Un usuario con 3 años de historial limpio debería tener límites diferentes a alguien que creó su cuenta ayer. Los límites deben cambiar según el riesgo."

## Documentación Técnica Actual

### Arquitectura Actual Problemática

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Transaction   │    │   Fixed         │    │   Block/Allow   │
│   Request       │────│   Limits        │────│   (Binary)      │
│                 │    │   ($1000 max)   │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

### El Sistema Actual Sin Inteligencia

```csharp
public class TransactionValidationService
{
    private readonly IUserRepository _userRepository;

    public TransactionValidationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ValidationResult> ValidateTransaction(TransactionRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        // Límites fijos sin considerar el contexto del usuario
        if (request.Amount > 1000m)
        {
            return ValidationResult.Rejected("Amount exceeds limit");
        }

        if (user.DailySpent + request.Amount > 5000m)
        {
            return ValidationResult.Rejected("Daily limit exceeded");
        }

        if (user.TransactionsToday >= 10)
        {
            return ValidationResult.Rejected("Transaction count exceeded");
        }

        return ValidationResult.Approved();
    }
}
```

**Problemas Críticos Identificados:**
- **Sin contexto de usuario**: Nuevos vs veteranos tratados igual
- **Sin adaptabilidad**: Límites nunca cambian según comportamiento
- **Sin inteligencia**: No considera historial ni patrones
- **Falsos positivos altos**: Usuarios legítimos constantemente bloqueados
- **Falsos negativos altos**: Fraudsters pasan los límites básicos

### Estados de Riesgo Requeridos

```csharp
public enum UserRiskState
{
    NewUser,        // Usuario recién registrado (0-7 días)
    Regular,        // Usuario con historial normal (>7 días, sin incidentes)
    Trusted,        // Usuario veterano con excelente historial (>6 meses)
    Suspicious,     // Usuario con patrones sospechosos detectados
    Restricted      // Usuario con actividad fraudulenta confirmada
}

public class UserContext
{
    public Guid UserId { get; set; }
    public UserRiskState CurrentState { get; set; }
    public DateTime AccountCreated { get; set; }
    public int DaysSinceLastIncident { get; set; }
    public decimal AverageMonthlyVolume { get; set; }
    public int SuccessfulTransactions { get; set; }
    public int FailedTransactions { get; set; }
    public List<SuspiciousActivity> RecentFlags { get; set; }
}

public class DynamicLimits
{
    public decimal SingleTransactionLimit { get; set; }
    public decimal DailyLimit { get; set; }
    public int MaxTransactionsPerDay { get; set; }
    public int MaxTransactionsPerHour { get; set; }
    public bool RequiresAdditionalAuth { get; set; }
    public TimeSpan CooldownBetweenTransactions { get; set; }
}
```

## Conversaciones con Stakeholders

### Reunión 1: Análisis de Crisis de Fraude (8 de Junio, 2024)

**Rodrigo (Head of Risk)**: Elena, explícame por qué nuestros límites fijos no están funcionando contra el fraude moderno.

**Elena (Security Engineer)**: Rodrigo, los fraudsters han evolucionado. Saben que nuestro límite máximo es $1,000, entonces hacen múltiples transacciones de $999. Mientras tanto, bloqueamos a un usuario legítimo que quiere transferir $1,500 a su familia.

**Pablo (Data Scientist)**: Los datos son claros: un usuario con 2 años de historial limpio tiene **0.01% probabilidad de fraude**, mientras que un usuario de 1 día tiene **15.3% probabilidad**. Sin embargo, ambos tienen los mismos límites.

**Rodrigo**: ¿Qué propones?

**Elena**: Límites que cambien según el estado de riesgo del usuario. Un usuario trusted debería poder hacer transacciones de $10,000, mientras que un usuario new debería estar limitado a $200.

**Pablo**: Exacto. Y los límites deben **transicionar automáticamente** según el comportamiento. Un usuario new que completa 50 transacciones sin problemas debería pasar a regular.

### Reunión 2: Definición de Estados de Riesgo (13 de Junio, 2024)

**Sofía (Desarrolladora Jr)**: Pablo, revisé los datos. ¿Realmente necesitamos 5 estados diferentes de riesgo?

**Pablo**: Los datos justifican cada estado:
- **NewUser**: Primeros 7 días, límite $200/transacción, $1,000/día
- **Regular**: >7 días sin incidentes, límite $2,000/transacción, $10,000/día  
- **Trusted**: >6 meses + $50K+ volumen limpio, límite $10,000/transacción, $50,000/día
- **Suspicious**: Patrones detectados, límite $100/transacción, requiere 2FA
- **Restricted**: Actividad fraudulenta confirmada, límite $50/transacción, cooldown 1 hora

**Elena**: Sofía, necesitas usar State Pattern. Cada estado maneja sus propios límites y transiciones.

**Sofía**: ¿State Pattern? ¿No sería más simple un switch-case con los estados?

**Elena**: No. Con State Pattern, cada estado encapsula su comportamiento específico y maneja sus propias transiciones. Es mucho más mantenible y extensible.

**Rodrigo**: ¿Cómo garantizamos que las transiciones sean correctas?

**Sofía**: Cada estado define cuándo puede transicionar a otros estados. Por ejemplo, NewUser → Regular requiere 7 días + 20 transacciones exitosas.

### Reunión 3: Reglas de Transición (18 de Junio, 2024)

**Elena**: ¿Cuáles son las reglas exactas para cambiar de estado?

**Pablo**: Las reglas basadas en datos:

**NewUser → Regular:**
- 7+ días de antigüedad
- 20+ transacciones exitosas
- 0 reportes de fraude

**Regular → Trusted:**
- 180+ días de antigüedad  
- $50,000+ volumen histórico limpio
- 0 incidentes en últimos 90 días

**Cualquier estado → Suspicious:**
- 3+ transacciones rechazadas en 1 hora
- Patrón de montos sospechoso detectado
- Cambio de IP/dispositivo inusual

**Suspicious → Restricted:**
- Confirmación de actividad fraudulenta
- Múltiples reportes de comercios

**Restricted → Regular:**
- Solo manual, tras investigación completa

**Sofía**: ¿Cómo validamos que funciona?

**Elena**: Dataset de 100,000 transacciones históricas. Aplicamos el nuevo sistema y medimos reducción de falsos positivos/negativos.

**Rodrigo**: ¿Cuánto tiempo para implementar completamente?

**Sofía**: Con State Pattern, 5 semanas: 1 semana para el framework de estados, 2 semanas para implementar los 5 estados, 1 semana para reglas de transición, 1 semana para testing y optimización.

## Fases del Ejercicio

### Fase 1: Análisis (15 minutos)

**Objetivo**: Analizar el problema y diseñar la solución usando State Pattern.

**Actividades**:
1. **Identificar los estados** (5 min):
   - ¿Qué estados de riesgo necesitamos?
   - ¿Qué comportamientos son específicos de cada estado?
   - ¿Cómo deben transicionar entre estados?

2. **Definir las abstracciones** (5 min):
   - ¿Cuál será la interfaz común para estados?
   - ¿Qué información necesita cada estado?
   - ¿Dónde vive la lógica de transición?

3. **Planificar la arquitectura** (5 min):
   - ¿Cómo estructurar el Context (usuario)?
   - ¿Cuántas clases de estado concretas?
   - ¿Cómo manejar los cambios de estado?

**Entregables**:
- Diagrama de estados y transiciones
- Lista de límites por estado
- Plan de implementación de transiciones

### Fase 2: Diseño (15 minutos)

**Objetivo**: Crear el diseño detallado de interfaces y máquina de estados.

**Actividades**:
1. **Diseñar IUserRiskState** (5 min):
   - Métodos para validar transacciones
   - Obtener límites específicos del estado
   - Evaluar transiciones a otros estados

2. **Diseñar UserRiskContext** (5 min):
   - Contexto que mantiene el estado actual
   - Lógica para cambiar estados
   - Historial de transiciones

3. **Planificar estados concretos** (5 min):
   - NewUserState (límites restrictivos)
   - RegularUserState (límites normales)
   - TrustedUserState (límites altos)
   - SuspiciousUserState (límites muy bajos + 2FA)
   - RestrictedUserState (límites mínimos + cooldown)

**Entregables**:
- Interfaces bien definidas
- Estructura de la máquina de estados
- Reglas de transición documentadas

### Fase 3: Implementación (30 minutos)

**Objetivo**: Implementar la solución completa usando State Pattern.

**Actividades**:
1. **Implementar interfaz base** (8 min):
   - `IUserRiskState`
   - `UserRiskContext`
   - Tipos y estructuras de datos

2. **Implementar estados concretos** (15 min):
   - `NewUserState`
   - `RegularUserState`
   - `TrustedUserState`
   - `SuspiciousUserState`
   - `RestrictedUserState`

3. **Implementar lógica de transiciones** (7 min):
   - Evaluar condiciones de cambio de estado
   - Aplicar transiciones automáticas
   - Integrar con servicio de validación

**Entregables**:
- Máquina de estados funcionando
- 5 estados implementados con sus límites
- Transiciones automáticas operativas

### Fase 4: Testing y Validación (10 minutos)

**Objetivo**: Validar efectividad contra fraude y usabilidad.

**Actividades**:
1. **Testing de estados** (4 min):
   - Pruebas unitarias para cada estado
   - Pruebas de transiciones válidas/inválidas
   - Validación de límites por estado

2. **Testing de efectividad** (4 min):
   - Simulación con datos históricos
   - Medición de falsos positivos/negativos
   - Validación de detección de fraude

3. **Optimización final** (2 min):
   - Performance de evaluación de estados
   - Logging de transiciones para auditoría
   - Dashboard de monitoreo de estados

**Entregables**:
- Sistema validado contra fraude
- Métricas de efectividad
- Documentación de operación

## Criterios de Evaluación

### Técnico (70%)
- **Implementación correcta del State Pattern** (25%)
- **Transiciones de estado funcionales** (20%)
- **Límites dinámicos por estado** (15%)
- **Integración con validación de transacciones** (10%)

### Funcional (30%)
- **Reducción de falsos positivos** (15%)
- **Detección efectiva de patrones sospechosos** (10%)
- **Límites adaptativos operativos** (5%)

### Bonus (10%)
- **Dashboard de monitoreo de estados**
- **Métricas de efectividad en tiempo real**
- **Configuración externa de reglas de transición**

## Solución Completa

### Diseño Final

El sistema implementado utiliza State Pattern para crear límites dinámicos que se adaptan automáticamente al nivel de riesgo del usuario:

1. **Estados Inteligentes**: Cada estado encapsula límites y comportamientos específicos
2. **Transiciones Automáticas**: Estados cambian según el comportamiento del usuario
3. **Límites Adaptativos**: Los límites se ajustan automáticamente al riesgo
4. **Detección Proactiva**: Identifica patrones sospechosos para cambiar estados

### Arquitectura de la Solución

```
┌─────────────────────────────────────────┐
│         UserRiskContext                 │ 
│         (Context)                       │
├─────────────────────────────────────────┤
│ - currentState: IUserRiskState          │
│ - userData: UserContext                 │
│ + ValidateTransaction(request)          │
│ + TransitionToState(newState)           │
│ + GetCurrentLimits()                    │
└─────────────────────────────────────────┘
                    │
                    ▼
┌─────────────────────────────────────────┐
│        IUserRiskState                   │
├─────────────────────────────────────────┤
│ + ValidateTransaction(amount, context)  │
│ + GetLimits()                           │
│ + EvaluateStateTransition(context)      │
│ + GetStateName()                        │
└─────────────────────────────────────────┘
                    △
                    │
    ┌───────────────┼─────────────────┬─────────────────┐
    │               │                 │                 │
    ▼               ▼                 ▼                 ▼
┌─────────┐  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐
│NewUser  │  │Regular      │  │Trusted      │  │Suspicious   │
│State    │  │State        │  │State        │  │State        │
│$200 max │  │$2K max      │  │$10K max     │  │$100 max+2FA │
└─────────┘  └─────────────┘  └─────────────┘  └─────────────┘
                                                       │
                                                       ▼
                                                ┌─────────────┐
                                                │Restricted   │
                                                │State        │
                                                │$50 max      │
                                                └─────────────┘
```

### Impacto del Proyecto

**Resultados después de 3 meses de implementación:**
- **78% reducción** en actividad fraudulenta exitosa
- **91% reducción** en falsos positivos (usuarios legítimos bloqueados)
- **$800,000 USD ahorrados** en fraudes prevenidos
- **89% mejora** en satisfacción de usuarios (menos bloqueos)
- **<50ms** tiempo de evaluación de límites dinámicos

**Testimonio de Rodrigo (Head of Risk)**:
*"El sistema de límites dinámicos transformó nuestra capacidad antifraude. Los usuarios legítimos ahora tienen la flexibilidad que necesitan, mientras que los fraudsters encuentran límites inteligentes que se adaptan a sus patrones. Sofía creó una solución que realmente entiende el comportamiento del usuario."*

**Testimonio de Elena (Security Engineer)**:
*"State Pattern fue la elección perfecta. Cada estado de riesgo maneja sus propios límites y transiciones de manera elegante. Agregar nuevos estados o modificar reglas es trivial. El sistema se adapta automáticamente al panorama cambiante de amenazas."*

El State Pattern demostró ser la solución ideal para límites dinámicos, mostrando cómo un patrón de diseño puede crear sistemas inteligentes que se adaptan automáticamente al comportamiento del usuario, mejorando tanto la seguridad como la experiencia de usuario.