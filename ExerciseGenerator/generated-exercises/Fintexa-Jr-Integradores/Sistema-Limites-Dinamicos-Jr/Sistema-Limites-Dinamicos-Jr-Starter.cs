using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fintexa.DynamicLimitsSystem
{
    // Enums para definir estados de riesgo y tipos de transacción
    public enum UserRiskState
    {
        NewUser,        // Usuario recién registrado (0-7 días)
        Regular,        // Usuario con historial normal (>7 días, sin incidentes)
        Trusted,        // Usuario veterano con excelente historial (>6 meses)
        Suspicious,     // Usuario con patrones sospechosos detectados
        Restricted      // Usuario con actividad fraudulenta confirmada
    }

    public enum TransactionResult
    {
        Approved,
        Rejected,
        RequiresAdditionalAuth,
        RequiresManualReview
    }

    public enum ValidationReason
    {
        Success,
        AmountExceedsLimit,
        DailyLimitExceeded,
        TransactionCountExceeded,
        HourlyLimitExceeded,
        CooldownPeriodActive,
        UserStateRestricted,
        AdditionalAuthRequired
    }

    // Modelos de datos
    public class UserContext
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public UserRiskState CurrentState { get; set; }
        public DateTime AccountCreated { get; set; }
        public int DaysSinceLastIncident { get; set; }
        public decimal AverageMonthlyVolume { get; set; }
        public int SuccessfulTransactions { get; set; }
        public int FailedTransactions { get; set; }
        public List<SuspiciousActivity> RecentFlags { get; set; } = new List<SuspiciousActivity>();
        public DateTime LastTransactionTime { get; set; }
        public decimal DailySpent { get; set; } = 0m;
        public int TransactionsToday { get; set; } = 0;
        public int TransactionsThisHour { get; set; } = 0;
        public DateTime LastStateChange { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }

    public class SuspiciousActivity
    {
        public DateTime DetectedAt { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int SeverityScore { get; set; }
    }

    public class DynamicLimits
    {
        public decimal SingleTransactionLimit { get; set; }
        public decimal DailyLimit { get; set; }
        public int MaxTransactionsPerDay { get; set; }
        public int MaxTransactionsPerHour { get; set; }
        public bool RequiresAdditionalAuth { get; set; }
        public TimeSpan CooldownBetweenTransactions { get; set; }
        public string StateDescription { get; set; }
    }

    public class TransactionRequest
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public string IPAddress { get; set; }
        public string DeviceId { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }

    public class ValidationResult
    {
        public TransactionResult Result { get; set; }
        public ValidationReason Reason { get; set; }
        public string Message { get; set; }
        public DynamicLimits AppliedLimits { get; set; }
        public UserRiskState UserState { get; set; }
        public UserRiskState? NewStateRecommendation { get; set; }
        public Dictionary<string, object> Details { get; set; } = new Dictionary<string, object>();
        public TimeSpan ProcessingTime { get; set; }
    }

    // TODO: Implementar la interfaz que define el contrato para los estados de riesgo
    public interface IUserRiskState
    {
        // TODO: Agregar método para validar transacciones de forma asíncrona
        // Debe recibir: decimal amount, UserContext context
        // Debe retornar: Task<ValidationResult>

        // TODO: Agregar método para obtener límites del estado
        // Debe retornar: DynamicLimits

        // TODO: Agregar método para evaluar transiciones de estado
        // Debe recibir: UserContext context
        // Debe retornar: Task<UserRiskState?> (null si no hay transición)

        // TODO: Agregar método para obtener nombre del estado
        // Debe retornar: string

        // TODO: Agregar método para obtener descripción del estado
        // Debe retornar: string

        // TODO: Agregar método para obtener tiempo mínimo en el estado
        // Debe retornar: TimeSpan
    }

    // TODO: Implementar estado NewUser (restrictivo por seguridad)
    public class NewUserState : IUserRiskState
    {
        // TODO: Definir límites estáticos para nuevos usuarios:
        // - SingleTransactionLimit: $200
        // - DailyLimit: $1,000
        // - MaxTransactionsPerDay: 5
        // - MaxTransactionsPerHour: 2
        // - RequiresAdditionalAuth: false
        // - CooldownBetweenTransactions: 5 minutos

        // TODO: Implementar ValidateTransactionAsync
        // - Medir tiempo de procesamiento
        // - Simular latencia de 50ms
        // - Validar límite por transacción
        // - Validar límite diario
        // - Validar número de transacciones diarias
        // - Validar transacciones por hora
        // - Validar cooldown entre transacciones
        // - Evaluar posible transición de estado
        // - Retornar resultado apropiado con detalles

        // TODO: Implementar GetLimits
        // - Retornar límites definidos

        // TODO: Implementar EvaluateStateTransitionAsync
        // - NewUser → Regular: 7+ días Y 20+ transacciones exitosas Y 0 flags
        // - Retornar null si no hay transición

        // TODO: Implementar otros métodos de la interfaz
        // - GetStateName: "New User"
        // - GetStateDescription: descripción apropiada
        // - GetMinimumStateTime: 7 días
    }

    // TODO: Implementar estado RegularUser (límites normales)
    public class RegularUserState : IUserRiskState
    {
        // TODO: Definir límites para usuarios regulares:
        // - SingleTransactionLimit: $2,000
        // - DailyLimit: $10,000
        // - MaxTransactionsPerDay: 20
        // - MaxTransactionsPerHour: 10
        // - RequiresAdditionalAuth: false
        // - CooldownBetweenTransactions: 1 minuto

        // TODO: Implementar ValidateTransactionAsync
        // - Simular latencia de 30ms
        // - Implementar todas las validaciones como NewUser pero con límites más altos
        // - Evaluar posible transición de estado

        // TODO: Implementar EvaluateStateTransitionAsync
        // - Regular → Trusted: 180+ días Y $50K+ volumen Y 90+ días sin incidentes Y 0 flags
        // - Regular → Suspicious: si DetectSuspiciousActivity retorna true
        // - Implementar DetectSuspiciousActivity (método privado)

        // TODO: Implementar DetectSuspiciousActivity
        // - 3+ flags en últimas 24 horas
        // - >10% tasa de fallos vs éxitos
        // - Retornar bool

        // TODO: Implementar otros métodos de la interfaz
        // - Tiempo mínimo: 1 día
    }

    // TODO: Implementar estado TrustedUser (límites altos)
    public class TrustedUserState : IUserRiskState
    {
        // TODO: Definir límites para usuarios confiables:
        // - SingleTransactionLimit: $10,000
        // - DailyLimit: $50,000
        // - MaxTransactionsPerDay: 50
        // - MaxTransactionsPerHour: 25
        // - RequiresAdditionalAuth: false
        // - CooldownBetweenTransactions: 30 segundos

        // TODO: Implementar ValidateTransactionAsync
        // - Simular latencia de 20ms (procesamiento más rápido)
        // - Para límite por hora: RequiresManualReview en lugar de rechazo
        // - Incluir beneficios de usuario trusted en detalles

        // TODO: Implementar EvaluateStateTransitionAsync
        // - Trusted → Suspicious: solo por DetectCriticalSuspiciousActivity
        // - Implementar DetectCriticalSuspiciousActivity (2+ flags críticos en 6 horas)

        // TODO: Implementar otros métodos de la interfaz
        // - Tiempo mínimo: 30 días
    }

    // TODO: Implementar estado SuspiciousUser (límites bajos + 2FA)
    public class SuspiciousUserState : IUserRiskState
    {
        // TODO: Definir límites para usuarios sospechosos:
        // - SingleTransactionLimit: $100
        // - DailyLimit: $500
        // - MaxTransactionsPerDay: 3
        // - MaxTransactionsPerHour: 1
        // - RequiresAdditionalAuth: true
        // - CooldownBetweenTransactions: 30 minutos

        // TODO: Implementar ValidateTransactionAsync
        // - Simular latencia de 100ms (verificaciones adicionales)
        // - Todas las transacciones requieren autenticación adicional
        // - Límites muy restrictivos
        // - Resultado típico: RequiresAdditionalAuth o RequiresManualReview

        // TODO: Implementar EvaluateStateTransitionAsync
        // - Suspicious → Restricted: si ConfirmFraudulentActivity retorna true
        // - Suspicious → Regular: 30+ días sin incidentes Y 0 flags (manual review)
        // - Implementar ConfirmFraudulentActivity (1+ flag de fraude con severity 9+)

        // TODO: Implementar otros métodos de la interfaz
        // - Tiempo mínimo: 7 días
    }

    // TODO: Implementar estado RestrictedUser (límites mínimos)
    public class RestrictedUserState : IUserRiskState
    {
        // TODO: Definir límites para usuarios restringidos:
        // - SingleTransactionLimit: $50
        // - DailyLimit: $100
        // - MaxTransactionsPerDay: 1
        // - MaxTransactionsPerHour: 1
        // - RequiresAdditionalAuth: true
        // - CooldownBetweenTransactions: 1 hora

        // TODO: Implementar ValidateTransactionAsync
        // - Simular latencia de 200ms (verificaciones extensivas)
        // - Prácticamente todas las transacciones: RequiresManualReview
        // - Razón: UserStateRestricted
        // - Incluir información de contacto para restauración

        // TODO: Implementar EvaluateStateTransitionAsync
        // - Restricted → Regular: solo manual, tras investigación completa
        // - Retornar null (no transiciones automáticas)

        // TODO: Implementar otros métodos de la interfaz
        // - Tiempo mínimo: 90 días
    }

    // TODO: Implementar contexto que mantiene estado actual y maneja transiciones (Context del State Pattern)
    public class UserRiskContext
    {
        // TODO: Definir estado actual (IUserRiskState)
        
        // TODO: Definir datos del usuario (UserContext)
        
        // TODO: Definir Dictionary de todos los estados posibles
        
        // TODO: Definir lista de historial de transiciones
        
        // TODO: Constructor que reciba UserContext
        // - Inicializar Dictionary con todos los estados
        // - Establecer estado inicial basado en _userData.CurrentState
        
        // TODO: Implementar ValidateTransactionAsync
        // - Validar parámetro no nulo
        // - Actualizar contexto para transacción actual
        // - Validar usando estado actual
        // - Considerar transición de estado si hay recomendación
        // - Actualizar estadísticas si transacción aprobada
        // - Retornar resultado
        
        // TODO: Implementar UpdateContextForCurrentTransaction
        // - Resetear contadores diarios si es nuevo día
        // - Resetear contador horario si es nueva hora
        // - Agregar metadata de transacción actual
        
        // TODO: Implementar UpdateUserStatisticsAfterApproval
        // - Actualizar DailySpent, TransactionsToday, TransactionsThisHour
        // - Actualizar LastTransactionTime y SuccessfulTransactions
        // - Actualizar AverageMonthlyVolume (simulado)
        
        // TODO: Implementar TransitionToStateAsync
        // - Verificar si ya está en ese estado
        // - Validar que el estado existe
        // - Verificar tiempo mínimo en estado actual
        // - Realizar la transición
        // - Registrar en historial
        // - Log de transición
        
        // TODO: Implementar métodos de información:
        // - GetCurrentState(), GetCurrentStateName()
        // - GetCurrentLimits(), GetUserData()
        // - GetTransitionHistory(), EvaluateStateTransitionAsync()
    }

    // TODO: Definir clase StateTransition para historial
    public class StateTransition
    {
        // TODO: Propiedades para FromState, ToState, TransitionTime, Reason, TriggeredBy
    }

    // TODO: Implementar servicio principal que integra el sistema de límites dinámicos
    public class DynamicLimitsService
    {
        // TODO: Definir Dictionary para contextos de usuario
        
        // TODO: Definir contadores para métricas del servicio
        
        // TODO: Implementar ValidateTransactionAsync
        // - Validar parámetro no nulo
        // - Incrementar contador de transacciones
        // - Obtener o crear contexto de usuario
        // - Validar usando el contexto
        // - Actualizar métricas del servicio
        // - Retornar resultado
        
        // TODO: Implementar GetOrCreateUserContext
        // - Buscar contexto existente
        // - Si no existe, crear con CreateMockUserData
        // - Agregar al Dictionary y retornar
        
        // TODO: Implementar CreateMockUserData
        // - Generar datos simulados basados en userId
        // - Determinar estado inicial usando DetermineInitialState
        // - Incluir variabilidad realista en los datos
        
        // TODO: Implementar DetermineInitialState
        // - <7 días: NewUser
        // - <180 días: Regular
        // - >=180 días: Trusted
        
        // TODO: Implementar UpdateServiceMetrics
        // - Simular detección de fraude prevenido
        // - Simular reducción de falsos positivos
        
        // TODO: Implementar métodos de administración:
        // - ForceStateTransitionAsync (manual)
        // - GetStateDistribution
        // - GetServiceMetrics
    }

    // TODO: Definir clase ServiceMetrics
    public class ServiceMetrics
    {
        // TODO: Propiedades para TransactionsProcessed, FraudsPrevented
        // TODO: FalsePositivesReduced, ActiveUsers, StateDistribution
    }

    // TODO: Clase de prueba para validar la implementación
    public class DynamicLimitsTests
    {
        public static async Task RunTests()
        {
            // TODO: Crear instancia de DynamicLimitsService
            
            // TODO: Definir casos de prueba con diferentes escenarios:
            // Caso 1: Usuario nuevo - transacción pequeña (aprobada)
            // Caso 2: Usuario nuevo - transacción que excede límite (rechazada)
            // Caso 3: Usuario regular - transacción normal (aprobada)
            // Caso 4: Usuario trusted - transacción alta (aprobada)
            
            // TODO: Para cada caso de prueba:
            // - Crear TransactionRequest apropiado
            // - Llamar ValidateTransactionAsync
            // - Verificar resultado esperado
            // - Mostrar detalles completos en consola
            // - Validar que los límites aplicados son correctos
            
            // TODO: Mostrar resumen de resultados
            // - Pruebas ejecutadas vs exitosas
            // - Porcentaje de éxito
            // - Métricas del servicio
            // - Distribución de estados
            // - Impacto esperado del sistema
        }

        // TODO: Implementar TestStateTransitions
        // - Simular usuario evolucionando a través de estados
        // - NewUser → Regular → Trusted
        // - Mostrar cómo límites se adaptan automáticamente
        // - Demostrar transiciones basadas en comportamiento
    }

    // TODO: Programa principal
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("🛡️  Fintexa - Sistema de Límites Dinámicos");
            Console.WriteLine("Implementación con State Pattern");
            Console.WriteLine("=====================================\n");
            
            // TODO: Ejecutar pruebas principales
            
            // TODO: Demostrar transiciones de estado
            
            // TODO: Mostrar resumen final del sistema implementado
            
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

/*
EJERCICIO: SISTEMA DE LÍMITES DINÁMICOS

OBJETIVO:
Implementar un sistema de límites dinámicos usando State Pattern que adapte
automáticamente los límites de transacción según el nivel de riesgo del usuario.

PATRÓN A IMPLEMENTAR:
✅ State Pattern: Estados que encapsulan comportamiento y límites específicos

ESTADOS REQUERIDOS:
1. NewUser: Límites restrictivos ($200 max, 5 tx/día)
2. Regular: Límites estándar ($2,000 max, 20 tx/día)
3. Trusted: Límites altos ($10,000 max, 50 tx/día)
4. Suspicious: Límites muy bajos + 2FA ($100 max, 3 tx/día)
5. Restricted: Límites mínimos + revisión manual ($50 max, 1 tx/día)

TRANSICIONES AUTOMÁTICAS:
- NewUser → Regular: 7+ días + 20+ transacciones exitosas
- Regular → Trusted: 180+ días + $50K+ volumen + historial limpio
- Cualquier estado → Suspicious: Patrones sospechosos detectados
- Suspicious → Restricted: Actividad fraudulenta confirmada

FUNCIONALIDADES REQUERIDAS:
1. Límites adaptativos por estado de riesgo
2. Transiciones automáticas basadas en comportamiento
3. Validaciones específicas por estado
4. Cooldown variable entre transacciones
5. Tiempo mínimo en cada estado

MÉTRICAS DE ÉXITO:
- 78% reducción en fraude exitoso
- 91% reducción en falsos positivos
- <50ms tiempo de evaluación
- Transiciones fluidas entre estados

VALIDACIONES POR ESTADO:
- Límite por transacción individual
- Límite diario acumulado
- Número máximo de transacciones por día/hora
- Cooldown entre transacciones
- Requerimientos de autenticación adicional

TIPS PARA LA IMPLEMENTACIÓN:
- Cada estado debe manejar sus propias reglas de validación
- Implementar thread-safety en transiciones
- Incluir historial completo de cambios de estado
- Considerar tiempo mínimo en cada estado
- Manejar casos edge como usuarios que regresan después de inactividad
- Implementar logging detallado para auditoría

¡Éxito creando el sistema antifraude inteligente del futuro!
*/