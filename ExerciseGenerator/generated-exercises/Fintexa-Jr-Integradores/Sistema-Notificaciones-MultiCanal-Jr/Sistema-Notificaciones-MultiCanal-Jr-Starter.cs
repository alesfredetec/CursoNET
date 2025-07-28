using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fintexa.NotificationSystem
{
    // Enums para definir tipos y canales de notificación
    public enum NotificationType
    {
        TransactionConfirmation,
        SecurityAlert,
        WelcomeMessage,
        PaymentReminder,
        AccountUpdate
    }

    public enum NotificationChannel
    {
        Email,
        SMS,
        WhatsApp,
        PushNotification
    }

    public enum NotificationStatus
    {
        Pending,
        Sent,
        Delivered,
        Failed,
        Fallback
    }

    // Modelos de datos
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<NotificationChannel> PreferredChannels { get; set; } = new List<NotificationChannel>();
        public Dictionary<NotificationType, NotificationChannel> ChannelPreferences { get; set; } = new Dictionary<NotificationType, NotificationChannel>();
    }

    public class NotificationRequest
    {
        public Guid UserId { get; set; }
        public NotificationType Type { get; set; }
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        public NotificationChannel? PreferredChannel { get; set; }
        public DateTime ScheduledFor { get; set; } = DateTime.Now;
        public bool RequireDeliveryConfirmation { get; set; } = false;
    }

    public class NotificationResult
    {
        public Guid NotificationId { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationChannel ChannelUsed { get; set; }
        public List<NotificationChannel> AttemptedChannels { get; set; } = new List<NotificationChannel>();
        public string DeliveryDetails { get; set; }
        public DateTime SentAt { get; set; }
        public TimeSpan ProcessingTime { get; set; }
    }

    // TODO: Implementar la interfaz que define el contrato para las estrategias de canal
    public interface INotificationChannel
    {
        // TODO: Agregar método para envío asíncrono
        // Debe incluir: recipient, subject, content, metadata opcional
        // Debe retornar: bool indicando éxito

        // TODO: Agregar método para verificar disponibilidad del canal
        // Debe retornar: bool indicando si el canal está disponible

        // TODO: Agregar método para obtener nombre del canal
        // Debe retornar: string con nombre descriptivo

        // TODO: Agregar método para obtener tipo de canal
        // Debe retornar: NotificationChannel enum

        // TODO: Agregar método para obtener tiempo promedio de entrega
        // Debe retornar: TimeSpan con tiempo estimado
    }

    // TODO: Implementar estrategia para notificaciones por Email
    public class EmailNotificationChannel : INotificationChannel
    {
        // TODO: Definir constantes para tasa de éxito (95%)
        
        // TODO: Implementar SendAsync
        // - Simular latencia de 200ms
        // - Validar formato de email
        // - Simular envío con tasa de éxito del 95%
        
        // TODO: Implementar IsAvailable
        // - Email siempre disponible (return true)
        
        // TODO: Implementar GetChannelName
        // - Retornar "SendGrid Email Service"
        
        // TODO: Implementar GetChannelType
        // - Retornar NotificationChannel.Email
        
        // TODO: Implementar GetAverageDeliveryTime
        // - Retornar 2 minutos
        
        // TODO: Método privado para validar email
        // - Verificar que contenga @ y .
    }

    // TODO: Implementar estrategia para notificaciones por SMS
    public class SMSNotificationChannel : INotificationChannel
    {
        // TODO: Definir constantes para tasa de éxito (98%)
        
        // TODO: Implementar SendAsync
        // - Simular latencia de 150ms
        // - Validar formato de teléfono
        // - Limitar contenido a 160 caracteres
        // - Simular envío con tasa de éxito del 98%
        
        // TODO: Implementar IsAvailable
        // - SMS generalmente disponible (return true)
        
        // TODO: Implementar GetChannelName
        // - Retornar "Twilio SMS Gateway"
        
        // TODO: Implementar GetChannelType
        // - Retornar NotificationChannel.SMS
        
        // TODO: Implementar GetAverageDeliveryTime
        // - Retornar 30 segundos
    }

    // TODO: Implementar estrategia para notificaciones por WhatsApp
    public class WhatsAppNotificationChannel : INotificationChannel
    {
        // TODO: Definir constantes para tasa de éxito (92%)
        
        // TODO: Implementar SendAsync
        // - Simular latencia de 300ms
        // - Validar número WhatsApp (debe empezar con +)
        // - Formatear contenido con markdown (*título*)
        // - Simular envío con tasa de éxito del 92%
        
        // TODO: Implementar IsAvailable
        // - WhatsApp disponible solo de 6 AM a 11 PM
        
        // TODO: Implementar GetChannelName
        // - Retornar "WhatsApp Business API"
        
        // TODO: Implementar GetChannelType
        // - Retornar NotificationChannel.WhatsApp
        
        // TODO: Implementar GetAverageDeliveryTime
        // - Retornar 15 segundos
    }

    // TODO: Implementar estrategia para notificaciones Push
    public class PushNotificationChannel : INotificationChannel
    {
        // TODO: Definir constantes para tasa de éxito (85%)
        
        // TODO: Implementar SendAsync
        // - Simular latencia de 100ms
        // - Validar device token (mínimo 32 caracteres)
        // - Limitar subject a 50 caracteres
        // - Limitar content a 100 caracteres
        // - Simular envío con tasa de éxito del 85%
        
        // TODO: Implementar otros métodos de la interfaz
    }

    // TODO: Implementar contexto que utiliza Dictionary + Strategy para manejar notificaciones multi-canal
    public class NotificationManager
    {
        // TODO: Definir Dictionary<NotificationChannel, INotificationChannel> para mapear canales
        
        // TODO: Definir Dictionary<NotificationType, List<NotificationChannel>> para preferencias por defecto
        
        // TODO: Constructor que inicialice ambos diccionarios
        // - Registrar todos los canales en el primer Dictionary
        // - Configurar preferencias por defecto para cada tipo de notificación:
        //   * TransactionConfirmation: Email, SMS
        //   * SecurityAlert: SMS, Email, PushNotification
        //   * WelcomeMessage: Email, WhatsApp
        //   * PaymentReminder: WhatsApp, SMS, Email
        //   * AccountUpdate: Email, PushNotification
        
        // TODO: Implementar SendNotificationAsync(NotificationRequest request, User user)
        // - Validar parámetros de entrada
        // - Determinar canales a intentar usando GetChannelsToTry
        // - Generar contenido usando GenerateNotificationContent
        // - Implementar sistema de fallback:
        //   * Intentar cada canal en orden
        //   * Si uno falla, continuar con el siguiente
        //   * Parar cuando uno tenga éxito
        // - Actualizar estadísticas de usuario si es exitoso
        // - Retornar NotificationResult con detalles completos
        
        // TODO: Implementar GetChannelsToTry(NotificationRequest request, User user)
        // - Prioridad 1: Canal específico del request si está en preferencias del usuario
        // - Prioridad 2: Preferencia específica del usuario para el tipo
        // - Prioridad 3: Canales preferidos generales del usuario
        // - Prioridad 4: Fallback a preferencias por defecto del sistema
        // - Retornar lista sin duplicados
        
        // TODO: Implementar GetRecipientForChannel(NotificationChannel channel, User user)
        // - Email: user.Email
        // - SMS: user.PhoneNumber
        // - WhatsApp: user.PhoneNumber con formato internacional (+1...)
        // - Push: mock device token usando user.Id
        
        // TODO: Implementar GenerateNotificationContent(NotificationType type, Dictionary<string, object> data, string userName)
        // - Generar subject y content específicos para cada tipo
        // - Usar datos del Dictionary para personalizar mensajes
        // - Incluir nombre del usuario en todos los mensajes
        
        // TODO: Implementar RegisterChannel para extensibilidad
        // - Permitir agregar nuevos canales dinámicamente
        
        // TODO: Implementar GetChannelStatistics
        // - Retornar información de todos los canales registrados
    }

    // TODO: Clase de prueba para validar la implementación
    public class NotificationManagerTests
    {
        public static async Task RunTests()
        {
            // TODO: Crear instancia de NotificationManager
            
            // TODO: Definir casos de prueba:
            // Caso 1: Email para confirmación de transacción
            // Caso 2: SMS fallback cuando email falla
            // Caso 3: WhatsApp para mensaje de bienvenida
            // Caso 4: Push notification para actualización de cuenta
            
            // TODO: Para cada caso de prueba:
            // - Crear User con preferencias específicas
            // - Crear NotificationRequest apropiado
            // - Llamar SendNotificationAsync
            // - Validar resultado esperado
            // - Mostrar detalles en consola
            
            // TODO: Mostrar resumen de resultados
            // - Pruebas ejecutadas vs exitosas
            // - Porcentaje de éxito
            // - Métricas de impacto esperado
            
            // TODO: Mostrar estadísticas de canales
        }

        // TODO: Implementar TestExtensibility
        // - Demostrar cómo agregar un nuevo canal (Telegram)
        // - Crear TelegramNotificationChannel
        // - Registrar en el manager
        // - Probar funcionalidad
    }

    // TODO: Programa principal
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("🔔 Fintexa - Sistema de Notificaciones Multi-Canal");
            Console.WriteLine("Implementación con Dictionary + Strategy Pattern");
            Console.WriteLine("================================================\n");
            
            // TODO: Ejecutar pruebas principales
            
            // TODO: Demostrar extensibilidad
            
            // TODO: Mostrar resumen final del sistema implementado
            
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

/*
EJERCICIO: SISTEMA DE NOTIFICACIONES MULTI-CANAL

OBJETIVO:
Implementar un sistema de notificaciones multi-canal usando Dictionary + Strategy Pattern
que permita enviar notificaciones por diferentes canales con sistema de fallback automático.

PATRONES A IMPLEMENTAR:
✅ Dictionary Pattern: Para mapear canales y preferencias
✅ Strategy Pattern: Para algoritmos específicos de cada canal
✅ Fallback System: Intento automático de canales alternativos

FUNCIONALIDADES REQUERIDAS:
1. Soporte para 4 canales: Email, SMS, WhatsApp, Push
2. Preferencias de usuario por tipo de notificación
3. Sistema de fallback automático
4. Validaciones específicas por canal
5. Extensibilidad para nuevos canales

MÉTRICAS DE ÉXITO:
- Tasa de entrega >85% con fallback
- Tiempo de procesamiento <300ms
- Soporte para preferencias personalizadas
- Extensibilidad verificada

TIPS PARA LA IMPLEMENTACIÓN:
- Usar async/await para simulaciones de latencia
- Implementar validaciones realistas por canal
- Considerar limitaciones de cada canal (SMS 160 chars, etc.)
- Manejar errores sin bloquear otros canales
- Implementar logging detallado para debugging

¡Éxito implementando el sistema de comunicación del futuro!
*/