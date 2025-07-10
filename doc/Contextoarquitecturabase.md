 Contextoarquitectura.md
 ----------------------------
 Contexto del Proyecto
 sistema financiero de medios de pago en arquitectura de microservicios con .NET Core 8, SQL Server, y Azure AKS. El sistema maneja transacciones de pagos, wallet, QR, POS e integraciones con Visa/Coelsa, bajo estándares PCI DSS, OWASP Top 10, CWE Top 25 y OWASP ASVS.

Arquitectura Base
 solución {Proyecto.Apiname} para gestionar {EntidadEjemplo} siguiendo arquitectura limpia en capas:

Presentación: {Proyecto.Apiname}.Api

Controladores RESTful con endpoints CRUD
Rate-limiting y manejo de concurrencia
Middlewares para logging y excepciones


Aplicación: {Proyecto.Apiname}.Application

Patrón CQRS con Commands y Queries
MediatR para comunicación desacoplada
Validadores con FluentValidation


Dominio: {Proyecto.Apiname}.Domain
 

## Visión General de la Arquitectura

Fintexa implementa una arquitectura de microservicios moderna para aplicaciones financieras y medios de pago, con las siguientes características fundamentales:

- *Tecnología base*: .NET Core 6/8, SQL Server, Azure AKS, MassTransit/RabbitMQ, Redis
- *Enfoque*: Alta concurrencia, seguridad (PCI DSS), y alto rendimiento
- *Patrón organizativo*: Arquitectura limpia con separación clara de responsabilidades
- *Comunicación*: Sincrónica (REST API) y asincrónica (mensajería basada en eventos)
- *Seguridad*: Cumplimiento con PCI DSS, OWASP Top 10, CWE Top 25 y OWASP ASVS

## Estructura de Proyecto Estándar

Cada microservicio sigue esta estructura de proyectos y namespaces:


{Proyecto.Apiname}/
├── {Proyecto.Apiname}.Api                 # Controladores REST, middleware, configuración
├── {Proyecto.Apiname}.Application         # Casos de uso, comandos/queries (CQRS)
├── {Proyecto.Apiname}.Domain              # Entidades y reglas de negocio
├── {Proyecto.Apiname}.DataAccess.EntityFramework    # Implementación de repositorios
├── {Proyecto.Apiname}.DataAccess.Interface          # Interfaces de repositorios
├── {Proyecto.Apiname}.Common              # Configuraciones, constantes y excepciones
├── {Proyecto.Apiname}.EventBus            # Publicación y consumo de eventos
├── Shared.Cache                           # Servicios de caché compartidos
├── Shared.Contract                        # Contratos y DTOs compartidos
└── {Proyecto.Apiname}.Test/               # Tests unitarios, integración e infraestructura


## Principios Arquitectónicos Clave

1. *Segregación de responsabilidades* (SOLID):
   - Cada componente tiene una única responsabilidad bien definida
   - Las interfaces definen contratos claros entre componentes
   - Los componentes dependen de abstracciones, no implementaciones concretas

2. *CQRS (Command Query Responsibility Segregation)*:
   - Separación de operaciones de lectura (Queries) y escritura (Commands)
   - Optimización específica para cada tipo de operación
   - Mediator pattern para desacoplamiento entre controladores y handlers

3. *Domain-Driven Design*:
   - Modelado de entidades ricas que encapsulan comportamiento
   - Agregados y entidades con invariantes de dominio
   - Eventos de dominio para notificar cambios en estado

4. *Microservicios*:
   - Servicios autónomos con bases de datos independientes
   - Comunicación mediante APIs RESTful y eventos
   - Despliegue independiente y escalado independiente

## Patrones de Implementación

### Capa de API (Presentación)
- Controladores RESTful con endpoints bien definidos
- Middleware para autenticación, logging y manejo de excepciones
- Filtros de acción para validación y manejo de errores
- Documentación Swagger/OpenAPI

### Capa de Aplicación
- Patrón Mediator (usando MediatR)
- Commands para operaciones de escritura
- Queries para operaciones de lectura
- Validadores de FluentValidation
- DTOs para transferencia de datos

### Capa de Dominio
- Entidades ricas con comportamiento y validación
- Value Objects para conceptos inmutables
- Domain Events para notificar cambios
- Domain Services para lógica compleja
- Excepciones de dominio específicas

### Capa de Infraestructura
- Repository Pattern para acceso a datos
- Unit of Work para transacciones
- Event Bus para publicación y consumo de eventos
- Servicios de caché distribuido

## Estrategias de Alta Concurrencia y Rendimiento

1. *Optimización de base de datos*:
   - Índices estratégicos
   - Separación de lectura/escritura
   - Caching multinivel

2. *Procesamiento asíncrono*:
   - Respuesta inmediata (202 Accepted)
   - Procesamiento en segundo plano
   - Notificaciones push/websocket

3. *Escalabilidad horizontal*:
   - Auto-scaling de pods en AKS
   - Balanceo de carga
   - Stateless design

4. *Resilience Patterns*:
   - Circuit Breaker (Polly)
   - Retry con backoff exponencial
   - Timeouts configurables
   - Bulkhead para aislamiento de fallos

## Estándares de Seguridad

1. *PCI DSS*:
   - Cifrado de datos sensibles
   - Control de acceso estricto
   - Auditoría y logging

2. *OWASP Top 10*:
   - Protección contra inyecciones
   - Autenticación robusta
   - Autorización por roles y claims

3. *Mejores prácticas de seguridad*:
   - HTTPS obligatorio
   - Validación de entrada estricta
   - CORS configurado adecuadamente
   - Headers de seguridad HTTP

## Observabilidad

1. *Logging*:
   - Serilog para logging estructurado
   - Centralización de logs
   - Correlación de requests

2. *Métricas*:
   - Latencia de endpoints
   - Tasas de error
   - Uso de recursos

3. *Tracing*:
   - Distributed tracing
   - Seguimiento de transacciones

## Instrucciones para Generación de Código con IA

Cuando solicites generación de código para este proyecto, sigue estas pautas:

1. *Especifica el nombre del proyecto y la entidad*: 
   - Nombre del proyecto: {Proyecto.Apiname}
   - Entidad principal: {EntidadEjemplo}

2. *Indica el tipo de componente*:
   - Controllers y endpoints REST
   - Commands/Queries y sus handlers
   - Domain entities con validación
   - Implementación de repositorios
   - Eventos y publicación/suscripción
   - Configuración de middleware

3. *Estilo de Código*:
   - Usa C# 10/11 con características modernas
   - Prefiere IasyncEenumerable, Task/async/await batch read para operaciones asíncronas 
   - Sigue convenciones Microsoft para nomenclatura
   - Incluye documentación XML en APIs públicas
   - Aplicar principios SOLID

4. *Ejemplos Relevantes*:
   - Especifica casos de uso concretos
   - Define los campos/propiedades de las entidades
   - Describe flujos de datos y transacciones
 