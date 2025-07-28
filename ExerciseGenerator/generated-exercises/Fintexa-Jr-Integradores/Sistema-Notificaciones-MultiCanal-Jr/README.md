# Ejercicio Integrador Jr: Sistema de Notificaciones Multi-Canal - Fintexa

## Historia Empresarial

### El Contexto de Crisis en Comunicación

**Abril 2024 - Centro de Operaciones de Fintexa, Piso 12**

Sofía Martín, Head of Customer Experience, miraba con preocupación los datos en su dashboard. Los números eran devastadores: **73% de los usuarios no abría los emails de transacciones**, **1,200 usuarios habían cancelado sus cuentas el mes pasado** citando "falta de comunicación oportuna" como razón principal.

"Estamos perdiendo clientes por algo que podríamos resolver", murmuró mientras revisaba los reportes de satisfacción que llegaban cada lunes con más quejas.

### Los Protagonistas

**Sofía Martín - Head of Customer Experience**: Responsable de la satisfacción del cliente, bajo presión por las altas tasas de churn y quejas de comunicación.

**Roberto Silva - DevOps Engineer**: Encargado de mantener la infraestructura de notificaciones, frustrado por la rigidez del sistema actual.

**Lucía Herrera - Product Manager**: Define los requerimientos de comunicación multicanal, necesita flexibilidad para lanzar nuevos canales rápidamente.

**Andrea López - Desarrolladora Jr**: Asignada para crear el nuevo sistema de notificaciones multi-canal. Su segundo proyecto importante en Fintexa.

### La Crisis del Canal Único

El sistema actual de Fintexa dependía exclusivamente de **SendGrid para emails**, una solución que había funcionado durante 2 años pero que ya no satisfacía las necesidades del negocio en crecimiento.

### El Problema Actual

**El Proceso de Notificación Rígido:**

1. **Canal Único**: Solo emails a través de SendGrid
2. **Sin Personalización**: Mismo template para todos los usuarios
3. **Sin Fallback**: Si SendGrid falla, no hay notificación
4. **Sin Preferencias**: Usuarios no pueden elegir cómo recibir notificaciones
5. **Sin Trazabilidad**: No se sabe si las notificaciones fueron recibidas

### Las Métricas del Desastre

Los números no mentían:
- **73% de emails no abiertos** (industry average: 22%)
- **1,200 usuarios perdidos/mes** por mala comunicación
- **47% de usuarios jóvenes** (18-25) nunca revisan email
- **89% de transacciones** sin confirmación leída por el usuario
- **$450,000 USD/mes** en pérdidas por usuarios que cancelan
- **156 quejas semanales** sobre "no recibir notificaciones importantes"

### La Presión del Mercado

Durante la reunión trimestral de abril, el CEO fue directo: "Nuestros competidores tienen WhatsApp, SMS, push notifications, in-app messages... nosotros tenemos solo email. Los usuarios de 2024 no viven en su bandeja de entrada. Necesitamos evolucionar o morir."

## Documentación Técnica Actual

### Arquitectura Actual de Notificaciones

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Transaction   │    │   Notification  │    │    SendGrid     │
│   Service       │────│   Service       │────│   (Email Only)  │
│   (.NET Core 8) │    │   (.NET Core 8) │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
```

### El Sistema Actual Problemático

```csharp
public class NotificationService
{
    private readonly IEmailSender _emailSender;

    public NotificationService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task SendTransactionNotification(Transaction transaction, User user)
    {
        // Solo email - sin opciones
        var emailContent = $"Transaction of ${transaction.Amount} processed";
        await _emailSender.SendEmailAsync(user.Email, "Transaction Processed", emailContent);
    }

    public async Task SendWelcomeNotification(User user)
    {
        // Solo email - hardcodeado
        var emailContent = "Welcome to Fintexa!";
        await _emailSender.SendEmailAsync(user.Email, "Welcome", emailContent);
    }

    // Más métodos similares, todos hardcodeados para email...
}
```

**Problemas Identificados:**
- **Acoplamiento fuerte** a un solo canal (email)
- **No extensible** para agregar nuevos canales
- **Sin personalización** por preferencias de usuario
- **Sin manejo de fallos** o canales de respaldo
- **Código duplicado** en cada tipo de notificación

### Tipos de Notificaciones Requeridas

```csharp
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
    PushNotification,
    InApp
}

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<NotificationChannel> PreferredChannels { get; set; }
    public Dictionary<NotificationType, NotificationChannel> ChannelPreferences { get; set; }
}

public class NotificationRequest
{
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public Dictionary<string, object> Data { get; set; }
    public NotificationChannel? PreferredChannel { get; set; }
    public DateTime ScheduledFor { get; set; } = DateTime.Now;
}
```

## Conversaciones con Stakeholders

### Reunión 1: Análisis de la Crisis (10 de Abril, 2024)

**Sofía (Head of CX)**: Roberto, necesito entender por qué estamos tan limitados en notificaciones. ¿No podemos simplemente agregar WhatsApp?

**Roberto (DevOps)**: Sofía, el problema es arquitectural. Todo está hardcodeado para SendGrid. Para agregar WhatsApp tendríamos que reescribir toda la clase NotificationService.

**Lucía (Product Manager)**: Eso no es sostenible. Necesito poder lanzar canales nuevos cada trimestre según las preferencias de nuestros usuarios.

**Sofía**: ¿Qué me dices de los datos de uso? ¿Los usuarios jóvenes realmente no usan email?

**Lucía**: Los datos son claros:
- **18-25 años**: 12% abren emails, 89% responden WhatsApp
- **26-40 años**: 45% abren emails, 67% prefieren SMS
- **40+ años**: 78% abren emails, 23% usan WhatsApp

**Roberto**: Necesitamos un sistema que pueda usar múltiples canales simultáneamente y que permita failover automático.

### Reunión 2: Definición de Requerimientos (15 de Abril, 2024)

**Andrea (Desarrolladora Jr)**: Lucía, revisé los requerimientos. ¿Realmente necesitamos 5 canales diferentes desde el día 1?

**Lucía**: Empezamos con 3: Email, SMS y WhatsApp. Pero el sistema debe permitir agregar más sin tocar código existente.

**Roberto**: Andrea, te sugiero usar un patrón que combine Dictionary para mapear canales con Strategy para implementaciones específicas.

**Andrea**: ¿Dictionary + Strategy? ¿No sería más simple un switch-case?

**Roberto**: No. El switch-case nos llevaría al mismo problema actual. Con Dictionary puedes mapear tipos de notificación a canales preferidos, y con Strategy cada canal tiene su implementación específica.

**Sofía**: ¿Cómo manejamos las preferencias de usuario?

**Andrea**: Cada usuario puede tener preferencias por tipo de notificación. Por ejemplo, transacciones por SMS, pero alertas de seguridad por WhatsApp.

**Lucía**: Perfecto. Y necesitamos fallback: si el canal preferido falla, intenta con el siguiente.

### Reunión 3: Prioridades y Casos de Uso (20 de Abril, 2024)

**Sofía**: ¿Cuáles son los casos de uso críticos que debemos resolver primero?

**Andrea**: Según el análisis:
1. **Confirmación de transacciones**: 45,000 diarias
2. **Alertas de seguridad**: 1,500 semanales
3. **Mensajes de bienvenida**: 800 mensuales
4. **Recordatorios de pago**: 3,200 mensuales

**Roberto**: ¿Cómo validaremos que funciona?

**Andrea**: Implementaré un sistema de mock providers para testing, y métricas para monitorear tasas de entrega por canal.

**Lucía**: ¿Qué pasa si un usuario no tiene configuradas preferencias?

**Andrea**: Reglas por defecto:
- **Transacciones**: Email
- **Seguridad**: Email + SMS (doble canal)
- **Bienvenida**: Canal que usó para registrarse
- **Recordatorios**: Canal más usado por el usuario

**Sofía**: ¿Cuánto tiempo tomará tener el sistema funcionando?

**Andrea**: Con Dictionary + Strategy, 5 semanas. La primera semana para el framework, 2 semanas para implementar los 3 canales, 2 semanas para testing y optimización.

## Fases del Ejercicio

### Fase 1: Análisis (15 minutos)

**Objetivo**: Analizar el problema y diseñar la solución usando Dictionary + Strategy Pattern.

**Actividades**:
1. **Identificar las variaciones** (5 min):
   - ¿Qué cambia entre diferentes canales de notificación?
   - ¿Cómo mapear tipos de notificación con canales?
   - ¿Dónde aplicar preferences de usuario?

2. **Definir las abstracciones** (5 min):
   - ¿Cuál será la interfaz común para canales?
   - ¿Cómo estructurar el Dictionary para mapeo?
   - ¿Qué parámetros necesita cada strategy?

3. **Planificar la arquitectura** (5 min):
   - ¿Cuántas clases de strategy necesitamos?
   - ¿Cómo configurar preferencias de usuario?
   - ¿Dónde implementar la lógica de fallback?

**Entregables**:
- Diagrama de clases con Dictionary + Strategy
- Mapeo de NotificationType → NotificationChannel
- Plan de implementación de fallback

### Fase 2: Diseño (15 minutos)

**Objetivo**: Crear el diseño detallado de interfaces, Dictionary y strategies.

**Actividades**:
1. **Diseñar INotificationChannel** (5 min):
   - Método para enviar notificaciones
   - Validación de disponibilidad del canal
   - Manejo de respuestas y errores

2. **Diseñar el NotificationManager** (5 min):
   - Dictionary para mapear canales
   - Lógica de selección por preferencias
   - Sistema de fallback entre canales

3. **Planificar las strategies concretas** (5 min):
   - EmailNotificationChannel
   - SMSNotificationChannel  
   - WhatsAppNotificationChannel
   - Configuración específica de cada una

**Entregables**:
- Interfaces bien definidas
- Estructura del Dictionary y mapeos
- Flujo de fallback documentado

### Fase 3: Implementación (30 minutos)

**Objetivo**: Implementar la solución completa usando Dictionary + Strategy.

**Actividades**:
1. **Implementar la interfaz base** (8 min):
   - `INotificationChannel`
   - Definir contratos y tipos de respuesta

2. **Implementar strategies concretas** (15 min):
   - `EmailNotificationChannel`
   - `SMSNotificationChannel`
   - `WhatsAppNotificationChannel`
   - Mock implementations para testing

3. **Implementar NotificationManager** (7 min):
   - Dictionary para mapear canales
   - Lógica de preferencias de usuario
   - Sistema de fallback automático

**Entregables**:
- Código funcionando con 3 canales
- Sistema de preferencias implementado
- Fallback automático operativo

### Fase 4: Refactoring y Testing (10 minutos)

**Objetivo**: Optimizar código y asegurar calidad para producción.

**Actividades**:
1. **Revisar y optimizar** (4 min):
   - Eliminar código duplicado
   - Mejorar manejo de errores
   - Optimizar performance del Dictionary

2. **Testing completo** (4 min):
   - Pruebas unitarias para cada canal
   - Pruebas de integración con fallback
   - Validación de preferencias de usuario

3. **Documentación** (2 min):
   - Guía de uso del sistema
   - Ejemplos de extensión con nuevos canales

**Entregables**:
- Código refactorizado y optimizado
- Suite de pruebas completa
- Documentación técnica

## Criterios de Evaluación

### Técnico (70%)
- **Implementación correcta de Dictionary + Strategy** (25%)
- **Sistema de fallback funcional** (20%)
- **Manejo de preferencias de usuario** (15%)
- **Código limpio y extensible** (10%)

### Funcional (30%)
- **Envío correcto por cada canal** (15%)
- **Aplicación correcta de preferencias** (10%)
- **Fallback automático funcional** (5%)

### Bonus (10%)
- **Factory Pattern para canales**
- **Configuración externa de mapeos**
- **Métricas y logging implementado**

## Solución Completa

### Diseño Final

El sistema implementado combina Dictionary Pattern para mapear tipos de notificación con canales, y Strategy Pattern para encapsular la lógica específica de cada canal:

1. **Dictionary**: Mapea NotificationType → List<NotificationChannel> para preferencias
2. **Strategy**: Cada canal tiene su implementación específica
3. **Fallback**: Intenta canales en orden de preferencia hasta éxito
4. **Extensibilidad**: Nuevos canales requieren solo nueva Strategy

### Arquitectura de la Solución

```
┌─────────────────────────────────────────┐
│         NotificationManager            │
│         (Context + Dictionary)          │
├─────────────────────────────────────────┤
│ - channels: Dict<Channel, IStrategy>    │
│ - preferences: Dict<Type, List<Channel>>│
│ + SendNotification(request, user)       │
│ + ConfigureUserPreferences(user, prefs) │
└─────────────────────────────────────────┘
                    │
                    ▼
┌─────────────────────────────────────────┐
│       INotificationChannel             │
├─────────────────────────────────────────┤
│ + SendAsync(content, recipient)         │
│ + IsAvailable()                         │
│ + GetChannelInfo()                      │
└─────────────────────────────────────────┘
                    △
                    │
    ┌───────────────┼─────────────────┐
    │               │                 │
    ▼               ▼                 ▼
┌─────────┐  ┌─────────────┐  ┌─────────────┐
│Email    │  │SMS          │  │WhatsApp     │
│Channel  │  │Channel      │  │Channel      │
│Strategy │  │Strategy     │  │Strategy     │
└─────────┘  └─────────────┘  └─────────────┘
```

### Impacto del Proyecto

**Resultados después de 3 meses de implementación:**
- **89% tasa de apertura** de notificaciones (vs 27% anterior)
- **200 usuarios recuperados** que habían cancelado por mala comunicación
- **92% satisfacción** en comunicación (vs 31% anterior)  
- **3 segundos** para agregar un nuevo canal de notificación
- **15 canales** disponibles (empezamos con 1)

**Testimonio de Sofía (Head of CX)**:
*"La transformación fue radical. Los usuarios ahora reciben notificaciones donde y como quieren. Las quejas por comunicación bajaron 94%. Andrea creó un sistema que cambió nuestra relación con los clientes."*

**Testimonio de Lucía (Product Manager)**:
*"Ahora lanzamos canales nuevos en días, no meses. La combinación de Dictionary + Strategy nos dio la flexibilidad que necesitábamos para competir en el mercado actual."*

El Dictionary + Strategy Pattern demostró ser la solución perfecta para un sistema de comunicación moderna, mostrando cómo dos patrones trabajando juntos pueden resolver problemas complejos de manera elegante y escalable.