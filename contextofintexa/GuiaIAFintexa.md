## Guía Estructurada del Ecosistema Fintexa

El ecosistema Fintexa es una plataforma financiera integral construida con una arquitectura de microservicios distribuidos, incluyendo Servicios Core, Backend for Frontend (BFF), Servicios Compartidos, Aplicaciones Frontend y Servicios de Infraestructura. Sigue patrones arquitectónicos como Clean Architecture, CQRS, DDD, Event-Driven Architecture y el patrón BFF.

Se compone de seis ecosistemas principales: Bind Aceptador, Wallet Service, CVU Collect, Bind Configuration, Boton Simple (Legacy) y ArchivosRI.

### 1. Bind Aceptador (Payment Acceptor)

Este es el ecosistema más grande y complejo, responsable de todos los aspectos de la aceptación de pagos, desde la incorporación de comercios hasta el procesamiento de transacciones y la liquidación. Incluye más de 60 proyectos.

#### Servicios Principales del Ecosistema Bind Aceptador:

1.  **Bff.BackofficeComercio**
    *   **Tipo de Servicio**: Backend-for-Frontend (BFF).
    *   **Descripción Funcional**: Proporciona la API de backend específicamente para la aplicación web del backoffice de comercios. Agrega datos de varios servicios core para presentar una vista unificada a los comercios, permitiéndoles gestionar sus tiendas, terminales (cajas), y ver sus transacciones y roles.
    *   **Endpoints Clave**:
        *   `ComercioController`: Gestión de comercios, sucursales y cajas (operaciones CRUD, validación de nombres, obtención de roles y configuraciones de entidad).
        *   `EchoController`: Endpoints de prueba y diagnóstico (echo, fecha/hora UTC, Swagger, eventos de log).
        *   `FileController`: Obtención de imágenes.
        *   `LiquidacionController`: Gestión de liquidaciones (obtener listas, generar, exportar a Excel).
        *   `LoginController`: Autenticación y gestión de sesiones de usuario (login, refresh, logout, validación de token y captcha).
        *   `OrdenVentaController`: Gestión de órdenes de venta (generar cerrada pendiente, eliminar).
        *   `PagoController`: Gestión de pagos y devoluciones (obtener pago, realizar devolución, obtener devolución).
        *   `PaymentController`: Creación y consulta de enlaces de pago.
        *   `PdfController`: Generación de documentos PDF (a partir de ruta de archivo/plantilla o URL).
        *   `PosicionamientoController`: Obtención de información geográfica (provincias y localidades).
        *   `PosParametroController`: Obtención de parámetros de posición (ej. color por entidad).
        *   `QrController`: Generación de códigos QR estáticos.
        *   `ReporteController`: Gestión y descarga de reportes (obtener listado, crear, obtener estados/tipos, descargar).
        *   `TransaccionController`: Consulta de transacciones, estados y medios de pago; exportación de datos de transacciones a Excel.
        *   `UsuarioController`: Gestión de usuarios (alta, modificación, eliminación, consulta de permisos, gestión de contraseñas).
        *   `WalletController`: Gestión de cuentas de billetera, saldos, movimientos, CVU/CBU, alias y operaciones de transferencia.
    *   **Dominio Primario**: **Comercio (Merchant)**. Gestiona la estructura jerárquica del comerciante: Comercio -> Sucursal -> Caja. También maneja configuraciones y roles específicos del comerciante. Sus subdominios reflejan la amplitud de su funcionalidad: Comercio, Contracargo, Files, OrdenVenta, Payment, Posicionamiento, PosParametro, Qr, Reportes, Transaccion, Usuario y Wallet.
    *   **Alcance del Servicio/Proyecto**: Proporciona una interfaz completa para la administración de la estructura comercial y la gestión de acceso y configuración a nivel de entidad. Es la capa de agregación y adaptación para el backoffice de comercios.
    *   **Dependencias**: Interactúa con `Shared.Comercio` y `Shared.AccessManagement`. Realiza llamadas a varios servicios de backend para procesar solicitudes. Forma parte del `namespace` de Kubernetes `bind-prod`.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Servicio .NET 6. Utiliza MediatR para manejar solicitudes y consultas. El dominio es amplio y modular.

2.  **Bff.CardNotPresent**
    *   **Tipo de Servicio**: Backend-for-Frontend (BFF).
    *   **Descripción Funcional**: Diseñado para escenarios de pago sin tarjeta presente (ej. transacciones de comercio electrónico). Maneja la creación de enlaces de pago, la tokenización de tarjetas para uso futuro y el procesamiento de pagos simples y directos.
    *   **Endpoints Clave**:
        *   `CardsController`: Gestión de tarjetas tokenizadas (tokenizar, borrar, obtener por token/ID de cliente).
        *   `DeudaController`: Creación y obtención de enlaces de pago vinculados a deudas (ej. "Botón Simple 2.0").
        *   `EchoController`: Endpoints de prueba y diagnóstico.
        *   `PaymentsController`: Creación de pagos, cancelaciones/devoluciones, obtención de pagos completos y creación de tokens de pago; procesamiento de pagos simples.
    *   **Dominio Primario**: **Payment**. Gestiona la entidad `Payment`, que representa una intención de pago o un enlace de pago. Maneja cancelaciones de pago (reembolsos) y la creación de tokens de pago. Sus subdominios incluyen Card, Deuda, Identity, Payment y Enumeraciones.
    *   **Alcance del Servicio/Proyecto**: Orquesta pagos sin tarjeta presente. Proporciona la lógica central para procesar pagos y devoluciones en un entorno "Card Not Present".
    *   **Dependencias**: Se comunica con otros servicios de backend para procesar solicitudes. Depende de `Shared.CardVault` para el almacenamiento seguro/tokenización de tarjetas. Forma parte del `namespace` de Kubernetes `bind-prod`.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Servicio .NET 6. Utiliza MediatR para manejar comandos.

3.  **Bff.CardPresent**
    *   **Tipo de Servicio**: Backend-for-Frontend (BFF).
    *   **Descripción Funcional**: Proporciona la API de backend para escenarios de pago con tarjeta presente, como terminales de punto de venta (POS) físicos. Maneja solicitudes de pago directas, devoluciones y ofrece endpoints para consultar el historial de transacciones y enviar recibos de pago por correo electrónico.
    *   **Endpoints Clave**:
        *   `DevolucionController`: Realiza devoluciones de pagos con tarjeta.
        *   `EchoController`: Endpoints de prueba y diagnóstico.
        *   `EmailController`: Envío de correos electrónicos.
        *   `LoginController`: Autenticación y gestión de sesiones de usuario, incluyendo login para usuarios de comercio.
        *   `OrdenController`: Generación y obtención de órdenes.
        *   `PagoController`: Realiza pagos con tarjeta y envía comprobantes de pago.
        *   `ParametersController`: Obtiene parámetros de configuración para POS y pagos.
        *   `PromocionController`: Evalúa promociones.
        *   `TransaccionController`: Consulta de transacciones, envío de logs de transacciones y obtención de resúmenes de lote.
    *   **Dominio Primario**: **Pago (Payment)** y **Transaccion (Transaction)**. Maneja directamente la iniciación de pagos y devoluciones, y proporciona capacidades de consulta sobre el historial de transacciones. Sus subdominios incluyen AccessManagement, CardBusinessRules, CardOrchestrator, Comercio, Notificacion, OrdenVenta, PagoParameters, PosParameters, Qr y Transaccion.
    *   **Alcance del Servicio/Proyecto**: Actúa como el punto de entrada principal para terminales POS. Procesa pagos directos con tarjeta y gestiona la emisión de comprobantes.
    *   **Dependencias**: Utiliza MediatR para enviar comandos (`PagoCommand` y `DevolucionCommand`) al backend para su procesamiento. Se integra con `PaymentAcceptor.CardOrchestrator` para la orquestación de pagos/devoluciones. Forma parte del `namespace` de Kubernetes `bind-prod`.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Servicio .NET 6. Su dominio es extenso y detallado, reflejando la complejidad de las operaciones de pago con tarjeta presente, incluyendo estructuras que se asemejan a estándares de mensajería financiera (como ISO 20022 en `CardOrchestrator`).

4.  **BFF.MobileNotPresent**
    *   **Tipo de Servicio**: Backend-for-Frontend (BFF).
    *   **Descripción Funcional**: Adaptado para clientes de aplicaciones móviles que realizan transacciones sin tarjeta presente. Ofrece dos funcionalidades principales: crear un token de pago para un conjunto de detalles de pago y procesar un pago simple y directo.
    *   **Dominio Primario**: **Payment**. Se centra en la iniciación de pagos y la creación de tokens de pago, que pueden utilizarse para operaciones de pago posteriores.
    *   **Alcance del Servicio/Proyecto**: Sirve como una API segura y simplificada para aplicaciones móviles.
    *   **Dependencias**: Utiliza MediatR para enviar comandos para crear tokens de pago y procesar pagos simples. Maneja el mapeo de las "claims" requeridas del contexto de seguridad del usuario a los comandos. Forma parte del `namespace` de Kubernetes `bind-prod`.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Servicio .NET 6.

5.  **PaymentAcceptor.CardBusinessRules**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Es el "cerebro" del sistema de enrutamiento de pagos y evaluación de reglas. Permite a los administradores crear y gestionar grupos de reglas de negocio (`GrupoReglas`) y asociarlas con comercios específicos (`Comercio`) o entidades completas. Estas reglas determinan qué procesador de pago debe usarse para una transacción, basándose en varios criterios.
    *   **Endpoints Clave**: `GrupoReglasController` (gestiona grupos de reglas).
    *   **Dominio Primario**: **Reglas (Rules)**. Gestiona las entidades `GrupoReglas` (Grupo de Reglas) y `Regla` (Regla). Proporciona operaciones CRUD completas para estas entidades y sus asociaciones.
    *   **Alcance del Servicio/Proyecto**: Sistema centralizado para gestionar las reglas de negocio de pago. Es crítico para la flexibilidad y extensibilidad de la plataforma de pago.
    *   **Dependencias**: Utiliza MediatR para manejar la creación, modificación y consulta de grupos de reglas y sus asociaciones. Forma parte del `namespace` de Kubernetes `bind-prod`.

6.  **PaymentAcceptor.CardOrchestrator**
    *   **Tipo de Servicio**: Core Service (Orquestador).
    *   **Descripción Funcional**: Es el orquestador central para todas las transacciones de pago basadas en tarjeta. Gestiona todo el ciclo de vida del pago, desde la recepción de la solicitud inicial hasta la coordinación con otros servicios para la validación de reglas, el procesamiento y la finalización. Maneja varios tipos de transacciones: pagos, reembolsos, cancelaciones y reversiones.
    *   **Endpoints Clave**:
        *   `PagoController`: Punto de entrada principal para acciones relacionadas con pagos (iniciar pago, pagos con débito, reembolsos, cancelaciones, reversiones).
        *   `CardWorkflowController`: Gestiona la entidad `Transaccion` (recuperar lista, crear, actualizar, obtener específica).
    *   **Dominio Primario**: **Orquestación de Pagos**. Gestiona la entidad `Payment` (que representa el estado del proceso de orquestación) y coordina la creación y actualización de la entidad `Transaccion` en el servicio `CardWorkflow`. Los modelos de dominio clave incluyen `Payment.cs` (entidad de dominio principal para `CardOrchestrator`, actúa como máquina de estados) y `Transaccion` (del `CardWorkflow`, registro detallado de la transacción financiera). Los contratos de eventos como `PaymentEvent.cs` se publican para señalar el inicio de un nuevo proceso de orquestación de pago.
    *   **Alcance del Servicio/Proyecto**: Coordina las interacciones entre microservicios especializados para un procesamiento de pagos correcto, seguro y eficiente. Publica eventos para notificar al sistema sobre el progreso de las transacciones. Es crucial para desacoplar la lógica de orquestación del procesamiento de pagos real. Forma parte del `namespace` de Kubernetes `bind-prod`.
    *   **Dependencias**: Orquesta el flujo de pago llamando a los servicios `Comercio` (validación de comerciante/tienda/terminal), `IssuerIdentification` (información del emisor de la tarjeta), `CardBusinessRules` (reglas/procesador) y `CardWorkflow` (creación de transacciones en estado pendiente). Publica `PaymentEvent` en el bus de eventos. Depende de `PaymentAcceptor.Transacciones` para los datos core de las transacciones.

7.  **PaymentAcceptor.CardWorkflow**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Responsable de gestionar el estado y el ciclo de vida de una entidad `Transaccion` (Transacción). Proporciona las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) core para las transacciones y es la fuente autoritativa para los datos de transacción.
    *   **Endpoints Clave**: `TransaccionController` (gestiona datos de transacción).
    *   **Dominio Primario**: **Transaccion (Transaction)**. Posee la entidad `Transaccion` y gestiona sus transiciones de estado (ej. de Pendiente a Aprobado o Rechazado).
    *   **Alcance del Servicio/Proyecto**: Actúa como el centro de datos para la información de transacciones.
    *   **Dependencias**: El servicio `CardOrchestrator` llama a este servicio para crear una transacción al inicio de un flujo de pago y luego la actualiza a medida que avanza el flujo. Utiliza MediatR para manejar comandos para crear y actualizar transacciones. Forma parte del `namespace` de Kubernetes `bind-prod`.

8.  **PaymentAcceptor.Conciliacion**
    *   **Tipo de Servicio**: No Implementado.
    *   **Descripción Funcional**: Parece ser un marcador de posición para un futuro servicio de conciliación. La conciliación es el proceso de igualar transacciones entre diferentes sistemas (ej. registros internos vs. extractos bancarios) para asegurar su consistencia.
    *   **Dominio Primario**: **Conciliacion (Reconciliation)**.
    *   **Alcance del Servicio/Proyecto**: El proyecto existe pero no contiene código fuente.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Forma parte del `namespace` de Kubernetes `bind-prod`.

9.  **PaymentAcceptor.Deuda**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Gestiona el concepto de `Deuda` (Deuda u obligación). Se utiliza para crear, actualizar y consultar deudas, que pueden representar una variedad de obligaciones financieras (ej. un pago a realizar por un producto o servicio). También maneja la gestión del estado de estas deudas (ej. pendiente, pagada, vencida).
    *   **Endpoints Clave**: `DeudaController` (gestiona el ciclo de vida de la deuda).
    *   **Dominio Primario**: **Deuda (Debt)**. Posee la entidad `Deuda` y proporciona un conjunto completo de endpoints para gestionar su ciclo de vida.
    *   **Alcance del Servicio/Proyecto**: Proporciona una API dedicada para gestionar deudas financieras. Es una pieza central para métodos de pago no inmediatos, como enlaces de pago o planes de cuotas.
    *   **Dependencias**: Utiliza MediatR para manejar comandos para crear, actualizar y eliminar deudas, así como consultas para recuperar información de deudas. Forma parte del `namespace` de Kubernetes `bind-prod`.

10. **PaymentAcceptor.Iep**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Responsable de manejar los Pagos Electrónicos Inmediatos (IEP), un sistema de pago en tiempo real en Argentina. Proporciona endpoints para resolver códigos QR y recuperar información de pedidos estandarizada, que son pasos clave en el flujo de pago de IEP.
    *   **Endpoints Clave**: `IepController` (maneja operaciones IEP).
    *   **Dominio Primario**: **IEP (Immediate Electronic Payment)**. Gestiona la lógica para interpretar códigos QR de IEP y obtener los detalles de pago asociados.
    *   **Alcance del Servicio/Proyecto**: Se integra con la red IEP. Es esencial para habilitar pagos con código QR a través del "rail" de IEP.
    *   **Dependencias**: Utiliza MediatR para manejar consultas para resolver datos QR y recuperar información de pedidos. Forma parte del `namespace` de Kubernetes `bind-prod`.

11. **PaymentAcceptor.Liquidacion**
    *   **Tipo de Servicio**: No Implementado.
    *   **Descripción Funcional**: Probablemente destinado a manejar la liquidación de fondos a los comercios. Este proceso implica el cálculo de los montos finales a pagar a los comercios después de deducir tarifas, comisiones y otros cargos.
    *   **Dominio Primario**: **Liquidacion (Settlement)**.
    *   **Alcance del Servicio/Proyecto**: El proyecto existe pero no contiene código fuente.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Forma parte del `namespace` de Kubernetes `bind-prod`.

12. **PaymentAcceptor.Notificacion**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Responsable de gestionar y enviar varios tipos de notificaciones relacionadas con eventos de pago. Admite la consulta de notificaciones históricas y el envío de nuevas notificaciones, incluyendo webhooks a sistemas externos.
    *   **Endpoints Clave**: `NotificationController` (gestiona notificaciones).
    *   **Dominio Primario**: **Notificacion (Notification)**. Gestiona la entidad `Notification` y maneja la lógica para entregar mensajes a diferentes destinos (sistemas internos, webhooks externos).
    *   **Alcance del Servicio/Proyecto**: Actúa como un centro de notificaciones centralizado.
    *   **Dependencias**: Utiliza MediatR para manejar comandos y consultas relacionados con notificaciones. Demuestra capacidades de publicación directa de webhooks. Forma parte del `namespace` de Kubernetes `bind-prod`.

13. **PaymentAcceptor.Promotions**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Gestiona campañas promocionales y su aplicación a transacciones de pago. Permite la creación, actualización, activación y eliminación de campañas, que pueden definirse por varios criterios como tipo, código, descripción, alcance, canal, método de pago y procesador. Proporciona reglas configurables (motor de condiciones flexible), descuentos dinámicos (cálculos en tiempo real), límites de uso (control de frecuencia y montos), segmentación (por grupo de usuarios) y A/B testing para experimentos de marketing.
    *   **Endpoints Clave**: `CampaniaController` (gestiona campañas).
    *   **Dominio Primario**: **Campania (Campaign)**. Posee la entidad `Campania` y proporciona un conjunto completo de endpoints para gestionar el ciclo de vida de las campañas promocionales.
    *   **Alcance del Servicio/Proyecto**: Componente crítico para implementar estrategias de precios flexibles y dinámicas dentro de la plataforma de pago.
    *   **Dependencias**: Utiliza MediatR para manejar comandos y consultas relacionados con campañas promocionales. Forma parte del `namespace` de Kubernetes `bind-prod`.

14. **PaymentAcceptor.Qr**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Dedicado a la generación y validación de códigos QR estáticos para la aceptación de pagos. Permite a los comercios generar códigos QR vinculados a sus cajas registradoras y permite que otros sistemas validen estos códigos QR y recuperen la información del comerciante asociada.
    *   **Endpoints Clave**: `QRController` (gestiona códigos QR).
    *   **Dominio Primario**: **QR (Quick Response) Code** y **Comercio (Merchant)**. Gestiona la generación de códigos QR y los vincula a los datos del comerciante y la caja registradora.
    *   **Alcance del Servicio/Proyecto**: Facilita el procesamiento de pagos a través de códigos QR.
    *   **Dependencias**: Utiliza MediatR para manejar comandos para la generación y validación de códigos QR. Se integra con los datos del comerciante. Forma parte del `namespace` de Kubernetes `bind-prod`.

15. **PaymentAcceptor.Rendicion**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Gestiona los procesos de liquidación y la generación de informes financieros. Crea informes detallados para liquidaciones, contracargos e información relacionada con impuestos (IIBB, IVA, Ganancias).
    *   **Endpoints Clave**: `RendicionController` (gestiona rendiciones).
    *   **Dominio Primario**: **Rendicion (Settlement/Reconciliation)** e **Impuestos (Taxes)**. Gestiona la generación de informes detallados para liquidaciones, contracargos e información fiscal.
    *   **Alcance del Servicio/Proyecto**: Orquesta la generación de varios informes financieros. Es crucial para la transparencia financiera y el cumplimiento normativo.
    *   **Dependencias**: Interactúa con los datos de transacciones para producir archivos de conciliación completos. Utiliza MediatR. Forma parte del `namespace` de Kubernetes `bind-prod`.

16. **PaymentAcceptor.Transacciones**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Es el repositorio central de todas las transacciones de pago dentro del ecosistema Bind Aceptador. Proporciona operaciones CRUD completas para transacciones, incluyendo operaciones por lotes y el manejo de propiedades de transacción adicionales.
    *   **Endpoints Clave**: `TransaccionesController` (gestiona transacciones).
    *   **Dominio Primario**: **Transaccion (Transaction)**. Posee la entidad `Transaccion` core y gestiona su persistencia y recuperación. Interactúa con las entidades `OrdenVenta` (Orden de Venta) y `Contracargo` (Contracargo).
    *   **Alcance del Servicio/Proyecto**: Actúa como el sistema de registro para los datos de transacciones. Diseñado para la eficiencia con soporte para operaciones individuales y por lotes.
    *   **Dependencias**: Utiliza MediatR para manejar comandos de gestión de transacciones y consultas para recuperar detalles. Forma parte del `namespace` de Kubernetes `bind-prod`.

17. **PaymentAcceptor.TransactionQuery**
    *   **Tipo de Servicio**: Core Service (enfocado en consultas).
    *   **Descripción Funcional**: Dedicado a proporcionar sólidas capacidades de consulta para los datos de transacciones. Permite recuperar listas de transacciones con opciones de filtrado y paginación, exportar datos de transacciones a CSV y generar informes de transacciones. Optimizado para operaciones de lectura.
    *   **Endpoints Clave**: `TransaccionesController`.
    *   **Dominio Primario**: **Transaccion (Transaction) Query**. Se centra en la recuperación y presentación de datos de transacciones, a menudo agregando información del servicio `PaymentAcceptor.Transacciones`.
    *   **Alcance del Servicio/Proyecto**: Diseñado para necesidades de recuperación de datos e informes de alto rendimiento.
    *   **Dependencias**: Implementa el lado de consulta del patrón CQRS para datos de transacciones. Utiliza MediatR para consultas. Agrega información de `PaymentAcceptor.Transacciones`. Forma parte del `namespace` de Kubernetes `bind-prod`.

18. **PaymentAcceptor.WorkFlowPagos**
    *   **Tipo de Servicio**: Core Service (Workflow/Orquestación).
    *   **Descripción Funcional**: Es un motor de flujo de trabajo crítico para varios tipos de pago, centrándose particularmente en pagos con código QR (Tx 3.1), actualizaciones de DEBIN y confirmaciones de pago privadas. Orquesta flujos de pago complejos que implican múltiples pasos e integraciones externas. Define y ejecuta los flujos de trabajo para operaciones de pago, integrándose con otros servicios y gestionando estados.
    *   **Endpoints Clave**: `PagoController`, `PagoPrivadoController`.
    *   **Dominio Primario**: **Pago (Payment)** y **Transaccion (Transaction)**. Gestiona la progresión de las intenciones de pago, confirmaciones, reversiones y contracargos para métodos de pago QR y privados. También maneja la acreditación de pagos rechazados y procesos de liquidación de PMC (Payment Management Company).
    *   **Alcance del Servicio/Proyecto**: Actúa como intermediario, recibiendo solicitudes de pago y coordinando con otros servicios (ej. para DEBIN, contracargos y liquidadores externos). Expone endpoints para flujos de pago tanto públicos como privados.
    *   **Dependencias**: Utiliza MediatR para gestionar intrincados flujos de trabajo de pago. Coordina con otros servicios (ej. para DEBIN, contracargos y liquidadores externos). Forma parte del `namespace` de Kubernetes `bind-prod`.

19. **Shared.AccessManagement**
    *   **Tipo de Servicio**: Shared Service (Autenticación y Autorización).
    *   **Descripción Funcional**: Proporciona gestión centralizada de identidades y accesos para toda la plataforma Fintexa. Maneja la autenticación de usuarios, la gestión de miembros (usuarios dentro de organizaciones), la jerarquía de organizaciones, el control de acceso basado en roles (RBAC) y la gestión de credenciales (cambios de contraseña, OTP). Admite organizaciones multinivel y diferentes aplicaciones. También proporciona gestión de JWT (emisión y validación de tokens) y Single Sign-On (SSO) en múltiples aplicaciones. Incluye una pista de auditoría para el registro de accesos y permisos.
    *   **Endpoints Clave**: `AccionController`, `CredencialesController`, `LoginController`, `MiembroController`, `OrganizacionController`, `OtpController`, `PermisoController` y `RolController`.
    *   **Dominio Primario**: **Gestión de Identidad y Acceso**. Gestiona las entidades `Miembro` (Miembro/Usuario), `Organizacion` (Organización), `Rol` (Rol), `Permiso` (Permiso) y `Credenciales` (Credenciales). Es la fuente autoritativa para los permisos de acceso al sistema.
    *   **Alcance del Servicio/Proyecto**: Servicio fundamental para la seguridad en toda la plataforma.
    *   **Dependencias**: Utiliza MediatR para manejar comandos y consultas. Es utilizado por `Bff.BackofficeComercio`. Forma parte de los `namespaces` de Kubernetes `bind-prod` y `wallet-prod`.

20. **Shared.ApiBank.Api**
    *   **Tipo de Servicio**: Shared Service (Integración Bancaria Externa).
    *   **Descripción Funcional**: Actúa como una interfaz estandarizada para la integración con sistemas bancarios externos. Proporciona una API unificada para realizar diversas operaciones bancarias, incluyendo transferencias relacionadas con CVU, operaciones DEBIN (Débito Inmediato) y transferencias de fondos generales. Abstrae las complejidades e implementaciones específicas de diferentes APIs bancarias. Es un servicio de abstracción para la integración con múltiples bancos, normalización de protocolos y balanceo de carga en CVUCollect.
    *   **Endpoints Clave**: `BilleteraController`, `DebinController` y `TransferenciaController`.
    *   **Dominio Primario**: **Integración Bancaria**. Gestiona las interacciones relacionadas con las entidades `CVU` (Clave Virtual Uniforme), `DEBIN` (Débito Inmediato) y `Transferencia` (Transferencia), actuando como un proxy para los sistemas bancarios externos.
    *   **Alcance del Servicio/Proyecto**: Crucial para habilitar transacciones financieras fluidas e integraciones con el ecosistema bancario más amplio.
    *   **Dependencias**: Utiliza MediatR para procesar solicitudes de operaciones bancarias. Traduce solicitudes internas en llamadas a APIs bancarias externas. Forma parte del `namespace` de Kubernetes `bind-prod`.

21. **Shared.AuthExternalService**
    *   **Tipo de Servicio**: Shared Service (Autenticación Externa).
    *   **Descripción Funcional**: Maneja el proceso de emisión de tokens para acceso externo. Asegura que solo las aplicaciones y entidades autorizadas puedan obtener tokens. Proporciona un servicio para la autenticación y federación de identidades con sistemas externos.
    *   **Dominio Primario**: **Autenticación/Autorización (Externa)**. Gestiona la emisión de tokens basada en parámetros como organización, propietario, aplicación y entidad.
    *   **Alcance del Servicio/Proyecto**: Crítico para asegurar el acceso a la API desde socios y aplicaciones externas.
    *   **Dependencias**: Proporciona endpoints para la generación de tokens. Forma parte del `namespace` de Kubernetes `bind-prod`.

22. **Shared.BulkUploadProcess**
    *   **Tipo de Servicio**: Shared Service (Procesamiento de Datos Masivo).
    *   **Descripción Funcional**: Facilita la carga masiva y el procesamiento de datos, específicamente para crear `Cajas` (Cajas Registradoras) en lotes. Permite la carga de archivos CSV que contienen detalles de cajas registradoras para una creación eficiente de múltiples registros.
    *   **Endpoints Clave**: `ProcesamientoController` (maneja el procesamiento por lotes de cajas registradoras).
    *   **Dominio Primario**: **Carga Masiva (Bulk Upload)** y **Comercio (Merchant)**. Maneja el procesamiento de datos masivos relacionados con entidades de comerciantes, particularmente cajas registradoras.
    *   **Alcance del Servicio/Proyecto**: Proporciona una creación eficiente de cajas registradoras por lotes.
    *   **Dependencias**: Utiliza MediatR para manejar `ProcesarCajasBatchCommand`. Recibe un `IFormFile` (archivo CSV) como entrada, lo analiza y procesa cada registro para crear cajas registradoras. Proporciona endpoints para consultar el estado de estos procesos por lotes. Forma parte del `namespace` de Kubernetes `bind-prod`.

23. **Shared.CardVault**
    *   **Tipo de Servicio**: Shared Service (Tokenización Segura de Tarjetas).
    *   **Descripción Funcional**: Proporciona tokenización y almacenamiento seguro de datos de tarjetas de crédito/débito. Asegura el cumplimiento de PCI DSS al almacenar información sensible de la tarjeta en una bóveda cifrada. Gestiona las entidades de tarjeta y token de pago, asegurando su creación, recuperación y eliminación seguras.
    *   **Dominio Primario**: **Card Tokenization** y **Secure Card Storage**. Gestiona las entidades `Card` y `PaymentToken`.
    *   **Alcance del Servicio/Proyecto**: Fundamental para reducir el alcance de PCI DSS de otros servicios en la plataforma.
    *   **Dependencias**: Utiliza MediatR para manejar comandos y consultas. Interactúa con un almacén de datos seguro (probablemente MongoDB). Es utilizado por `Bff.CardNotPresent`. Forma parte del `namespace` de Kubernetes `bind-prod`.

24. **Shared.CloudServiceInfrastructure**
    *   **Tipo de Servicio**: Shared Service (Gestión de Infraestructura).
    *   **Descripción Funcional**: Proporciona capacidades core de gestión de infraestructura. Incluye la creación y aprobación de consumidores (probablemente para colas de mensajes u otros recursos compartidos) y la restauración de bases de datos. Actúa como un servicio de utilidad para gestionar los componentes subyacentes de la infraestructura en la nube.
    *   **Endpoints Clave**: `ConsumerController`, `RestoreDDBBController`.
    *   **Dominio Primario**: **Gestión de Infraestructura**. Gestiona las entidades `Consumer` (que representan clientes de recursos de infraestructura compartidos) y orquesta los procesos de restauración de bases de datos.
    *   **Alcance del Servicio/Proyecto**: Probablemente utilizado por administradores o procesos automatizados para gestionar los recursos en la nube de la plataforma.
    *   **Dependencias**: Utiliza MediatR para manejar comandos para crear/aprobar consumidores e iniciar la restauración de bases de datos. Forma parte del `namespace` de Kubernetes `bind-prod`.

25. **Shared.CloudServiceInfrastructure.Consumers**
    *   **Tipo de Servicio**: Aplicación Consumidora (Scripts de Python).
    *   **Descripción Funcional**: No es un microservicio .NET, sino una colección de scripts de Python. Diseñados para interactuar y gestionar consumidores, probablemente relacionados con colas de mensajes u otros componentes de infraestructura. Incluye scripts para crear, eliminar y recuperar información de consumidores.
    *   **Dominio Primario**: **Gestión de Consumidores (con scripts)**. Proporciona utilidades de línea de comandos para gestionar consumidores.
    *   **Alcance del Servicio/Proyecto**: Es una aplicación de utilidad más que un servicio API independiente.
    *   **Dependencias**: Interactúa con el servicio `Shared.CloudServiceInfrastructure` o directamente con la infraestructura subyacente para gestionar consumidores.

26. **Shared.Coelsa.Alias**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona el ciclo de vida de los alias para cuentas bancarias (CBU/CVU) dentro de la red Coelsa. Permite la creación, modificación y eliminación de alias, así como la consulta de alias activos e inactivos por CUIT o por el propio alias. Admite la reasignación de alias a diferentes cuentas.
    *   **Endpoints Clave**: `AliasController` (gestiona alias).
    *   **Dominio Primario**: **Gestión de Alias**. Gestiona la entidad `Alias`, que vincula un alias legible por humanos a un identificador de cuenta bancaria (CBU/CVU).
    *   **Alcance del Servicio/Proyecto**: Crucial para proporcionar identificadores fáciles de usar para cuentas bancarias en el sistema financiero argentino.
    *   **Dependencias**: Utiliza MediatR para manejar comandos y consultas para la gestión de alias. Interactúa con la red Coelsa. Forma parte del `namespace` de Kubernetes `bind-prod`.

27. **Shared.Comercio**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Es la autoridad central para gestionar la información de comerciantes (`Comercio`), sucursales (`Sucursal`) y cajas registradoras (`Caja`). Proporciona operaciones CRUD completas para estas entidades, incluyendo creación, actualizaciones y eliminación. También maneja la asociación de CVU con cajas registradoras, gestiona notificaciones para comerciantes/sucursales y habilita/deshabilita procesadores de pago y la recaudación por transferencia para comerciantes. Gestiona la incorporación de comerciantes, sucursales, terminales POS, datos maestros y validaciones regulatorias.
    *   **Endpoints Clave**: `CajaController`, `ComercioController`, `ComisionController` y `SucursalController` (gestionan la jerarquía del comerciante).
    *   **Dominio Primario**: **Comercio (Merchant)**. Gestiona la estructura jerárquica de los comerciantes, sus ubicaciones físicas (sucursales) y puntos de venta (cajas registradoras). Maneja configuraciones relacionadas como comisiones y notificaciones.
    *   **Alcance del Servicio/Proyecto**: Fundamental para la columna vertebral operativa de la plataforma de pago.
    *   **Dependencias**: Utiliza MediatR para manejar comandos y consultas. Se integra con sistemas externos como Coelsa para la incorporación de comerciantes. Es utilizado por `Bff.BackofficeComercio`. Forma parte del `namespace` de Kubernetes `bind-prod`.

28. **Shared.ComercioQuery**
    *   **Tipo de Servicio**: Shared Service (enfocado en consultas).
    *   **Descripción Funcional**: Probablemente una contraparte de solo lectura del servicio `Shared.Comercio`. Proporciona capacidades de consulta optimizadas para datos de comerciantes, sucursales y cajas registradoras. Utilizado por otros servicios o aplicaciones que necesitan recuperar información relacionada con comerciantes sin realizar operaciones de escritura.
    *   **Endpoints Clave**: `DemoController`, `SubDemoController` (probablemente marcadores de posición).
    *   **Dominio Primario**: **Comercio (Merchant) Query**. Se centra en la recuperación eficiente de datos relacionados con comerciantes.
    *   **Alcance del Servicio/Proyecto**: Adhiere al patrón CQRS, separando los modelos de lectura de los modelos de escritura para una mejor escalabilidad y rendimiento de las operaciones de lectura intensiva.
    *   **Dependencias**: Implementa manejadores de consultas para varios aspectos de los datos de comerciantes. Forma parte del `namespace` de Kubernetes `bind-prod`.

29. **Shared.Comisiones**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Responsable de gestionar y calcular comisiones relacionadas con transacciones financieras. Maneja la creación y gestión de reglas de comisión. Proporciona funcionalidades para calcular, recalcular y consultar comisiones basándose en varios criterios (ej. comerciante, tipo de transacción, MCC).
    *   **Endpoints Clave**: `ComercioController`, `LiqComisionesController` (gestionan comisiones).
    *   **Dominio Primario**: **Comision (Commission)**. Gestiona las entidades `Comision` y sus reglas y cálculos asociados.
    *   **Alcance del Servicio/Proyecto**: Crucial para la conciliación financiera y la gestión de ingresos de la plataforma.
    *   **Dependencias**: Utiliza MediatR para manejar comandos y consultas para la gestión de comisiones. Forma parte del `namespace` de Kubernetes `bind-prod`.

30. **Shared.ComplianceCheck**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Probablemente responsable de realizar varias verificaciones de cumplimiento en datos o transacciones.
    *   **Dominio Primario**: **Compliance**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

31. **Shared.CustomTemplates**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona plantillas personalizadas, posiblemente para correos electrónicos, informes u otra generación de contenido dinámico.
    *   **Dominio Primario**: **Templating**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

32. **Shared.Cvu**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona operaciones relacionadas con CVU (Clave Virtual Uniforme), probablemente para validación, búsqueda o gestión general.
    *   **Dominio Primario**: **CVU**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

33. **Shared.Cvu.Generator**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Responsable específicamente de generar CVUs.
    *   **Dominio Primario**: **Generación de CVU**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

34. **Shared.Debin**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona operaciones DEBIN (Débito Inmediato), posiblemente creación, cancelación o consultas de estado.
    *   **Dominio Primario**: **DEBIN**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

35. **Shared.EmailService**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Proporciona capacidades de envío de correo electrónico a otros servicios del ecosistema.
    *   **Dominio Primario**: **Comunicación por Email**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

36. **Shared.FileManager**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona el almacenamiento de archivos, la recuperación y posiblemente otras operaciones relacionadas con archivos.
    *   **Dominio Primario**: **Gestión de Archivos**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

37. **Shared.GlobalProcesing**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Probablemente se integra con un procesador de pagos específico para el procesamiento global.
    *   **Dominio Primario**: **Procesamiento de Pagos**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

38. **Shared.InternalAudit**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Maneja el registro y seguimiento de auditorías internas para el cumplimiento y la trazabilidad.
    *   **Dominio Primario**: **Pista de Auditoría**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

39. **Shared.IssuerIdentification**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Consulta el servicio `IssuerIdentification` para obtener información sobre el emisor de la tarjeta basándose en el BIN de la tarjeta. Servicio para la identificación y validación de emisores de tarjetas y cuentas, se integra con bases de datos externas.
    *   **Dominio Primario**: **Identificación de Emisor**.
    *   **Dependencias**: Utilizado por `PaymentAcceptor.CardOrchestrator`.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

40. **Shared.PagoExterno**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona y procesa pagos a entidades externas, incluyendo conciliación e informes.
    *   **Dominio Primario**: **Pagos Externos**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

41. **Shared.PaymentsObservability**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Proporciona monitoreo en tiempo real, recolección de métricas, sistema de alertas avanzado, paneles de observabilidad y análisis de rendimiento para operaciones de pago.
    *   **Dominio Primario**: **Observabilidad y Monitoreo de Pagos**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

42. **Shared.Pdf**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Maneja la generación de PDF, incluye un sistema de plantillas, admite firmas digitales, compresión y procesamiento por lotes.
    *   **Dominio Primario**: **Generación de PDF**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

43. **Shared.Posicionamiento**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Proporciona información geográfica (provincias, localidades).
    *   **Dominio Primario**: **Datos Geográficos**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

44. **Shared.ReportManager**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona informes, probablemente definiéndolos, generándolos y distribuyéndolos.
    *   **Dominio Primario**: **Gestión de Informes**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

45. **Shared.Retencion**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Gestiona las retenciones de impuestos, posiblemente para cálculos, aplicando reglas o generando datos relacionados.
    *   **Dominio Primario**: **Retención de Impuestos**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

46. **Shared.Vault.Admin**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Funciones administrativas para la Bóveda, probablemente gestionando claves, acceso o configuraciones.
    *   **Dominio Primario**: **Administración de Bóvedas**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

47. **Shared.Vault.Card**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Proporciona tokenización PCI, almacenamiento seguro de tarjetas, gestión de usuarios y cumplimiento PCI DSS.
    *   **Dominio Primario**: **Bóveda de Tarjetas**, **Almacenamiento Seguro de Tarjetas**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

48. **Shared.VaultManager**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Orquesta, monitorea y administra múltiples bóvedas de datos sensibles.
    *   **Dominio Primario**: **Gestión de Bóvedas**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.

49. **Shared.WebhookSender**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Permite el envío asíncrono y garantizado de notificaciones a sistemas externos. Incluye lógica de reintentos automáticos y configuración personalizada por comercio.
    *   **Dominio Primario**: **Sistema de Notificaciones Webhook**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos del ecosistema Bind Aceptador.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Arquitectura basada en eventos, .NET 6/8, integración HTTP.

50. **Web.BackofficeComercio**
    *   **Tipo de Servicio**: Interfaz Web.
    *   **Descripción Funcional**: Aplicación web frontend para el backoffice de comercios.
    *   **Dominio Primario**: **Interfaz de Usuario del Backoffice del Comerciante**.
    *   **Dependencias**: Interactúa con `Bff.BackofficeComercio` (implícito).
    *   **Alcance del Servicio/Proyecto**: Una de las interfaces web del ecosistema Bind Aceptador.

51. **Web.CardNotPresent**
    *   **Tipo de Servicio**: Interfaz Web.
    *   **Descripción Funcional**: Aplicación web frontend para escenarios de pago sin tarjeta presente.
    *   **Dominio Primario**: **Interfaz de Usuario de Pagos sin Tarjeta Presente**.
    *   **Dependencias**: Interactúa con `Bff.CardNotPresent` (implícito).
    *   **Alcance del Servicio/Proyecto**: Una de las interfaces web del ecosistema Bind Aceptador.

52. **Web.SimpleButton**
    *   **Tipo de Servicio**: Interfaz Web.
    *   **Descripción Funcional**: Aplicación web frontend para la solución de pagos "Boton Simple".
    *   **Dominio Primario**: **Interfaz de Usuario de Pagos de Botón Simple**.
    *   **Dependencias**: Interactúa con `Bff.SimpleButton` (implícito).
    *   **Alcance del Servicio/Proyecto**: Una de las interfaces web del ecosistema Bind Aceptador.

53. **SimpleButton.Payment**
    *   **Tipo de Servicio**: Solución de Pago de Botón Simple.
    *   **Descripción Funcional**: Servicio legacy para el procesamiento básico de pagos, tokenización de tarjetas, liquidación y generación de informes. Incluye integración con sistemas legacy y manejo de eventos.
    *   **Dominio Primario**: **Motor de Pagos Simple**.
    *   **Alcance del Servicio/Proyecto**: Proporciona el procesamiento de pagos para la solución legacy de "Boton Simple".
    *   **Arquitectura/Detalles Técnicos Relevantes**: Servicio legacy.

54. **Bff.SimpleButton**
    *   **Tipo de Servicio**: Servicio BFF.
    *   **Descripción Funcional**: Backend for Frontend para la solución "Boton Simple".
    *   **Dominio Primario**: **Agregación de Botón Simple**.
    *   **Alcance del Servicio/Proyecto**: Agrega y adapta la funcionalidad del "Boton Simple" para el frontend.

55. **AKS-Manifests (Bind Aceptador)**
    *   **Dominio**: Orquestación y despliegue.
    *   **Descripción Funcional**: Manifiestos de Kubernetes para desplegar los servicios de Bind Aceptador en Azure AKS. Incluye configuraciones de recursos, políticas de escalado, verificaciones de salud y redes.
    *   **Alcance del Servicio/Proyecto**: Permite despliegues consistentes y seguros en la nube. Forma parte de la infraestructura para el `namespace` de Kubernetes `bind-prod`.

56. **ArquetipoBase (Bind Aceptador)**
    *   **Dominio**: Plantilla base de proyectos.
    *   **Descripción Funcional**: Proporciona la estructura inicial y las mejores prácticas para nuevos microservicios en el ecosistema. Incluye Clean Architecture, configuración de CI/CD, pruebas y ejemplos de integración. Incluye scaffolding de proyectos, plantillas de código, patrones de arquitectura y configuración de pruebas.
    *   **Alcance del Servicio/Proyecto**: Plantilla reutilizable para un desarrollo de servicios consistente en todo el ecosistema.

57. **Fake.Coelsa**
    *   **Dominio**: Simulación de integración COELSA.
    *   **Descripción Funcional**: Servicio de pruebas para simular la integración con COELSA, permitiendo validaciones y pruebas sin afectar los entornos de producción.
    *   **Alcance del Servicio/Proyecto**: Facilita el desarrollo y las pruebas de servicios que dependen de Coelsa.

58. **Infra (Bind Aceptador)**
    *   **Dominio**: Infraestructura compartida.
    *   **Descripción Funcional**: Scripts, configuraciones y recursos de infraestructura reutilizables para todos los servicios del ecosistema. Incluye automatización, monitoreo y gestión de entornos. Proporciona Infrastructure as Code (Terraform, plantillas ARM), gestión de entornos, pipelines de CI/CD, gestión centralizada de configuraciones y configuración de monitoreo.
    *   **Alcance del Servicio/Proyecto**: Proporciona la infraestructura subyacente para el `namespace` de Kubernetes `bind-prod`.

59. **Jobs.Bind**
    *   **Dominio**: Jobs de integración BIND.
    *   **Descripción Funcional**: Procesos por lotes y jobs para sincronización, conciliación y mantenimiento de datos con BIND. Automatiza tareas recurrentes y asegura la integridad de los datos.
    *   **Alcance del Servicio/Proyecto**: Procesos de backend para la consistencia y el mantenimiento de los datos.

60. **Prueba.Repo (Bind Aceptador)**
    *   **Dominio**: Repositorio de pruebas.
    *   **Descripción Funcional**: Proyectos de ejemplo y pruebas de integración, validación de nuevas funcionalidades y experimentación. Incluye ejemplos de pruebas, ejemplos de integración, documentación de ejemplos, mejores prácticas y pruebas de concepto.
    *   **Alcance del Servicio/Proyecto**: Apoya los esfuerzos de desarrollo y garantía de calidad.

---

### 2. ArchivosRI (Regulatory Information)

Este ecosistema es responsable de la gestión y el procesamiento de la información regulatoria, los informes y el cumplimiento normativo. Específicamente, genera archivos de cumplimiento normativo para las autoridades financieras argentinas (ej. AFIP). Incluye más de 3 proyectos.

#### Servicios Principales del Ecosistema ArchivosRI:

1.  **Shared.RegulatoryInformation**
    *   **Tipo de Servicio**: Core Service (Generación de Archivos Regulatorios), API principal.
    *   **Descripción Funcional**: Genera varios archivos de cumplimiento normativo requeridos por las autoridades financieras argentinas, como AFIP F8126, AFIP F8125, Saldos Clientes, Padrón 8601 e Infestadística. Proporciona endpoints para activar la generación de estos archivos y ponerlos a disposición en una ubicación de almacenamiento. Responsable de informes regulatorios, recopilación de datos requeridos, monitoreo de cumplimiento, gestión de archivos, envío a organismos, validación de datos y gestión de plantillas.
    *   **Endpoints Clave**:
        *   `FileGeneratorController`: Genera archivos relacionados con información regulatoria y financiera (F8126, F8125, SaldosClientes, Padrón 8601, INFESTADISTICA).
        *   `TestController`: Pruebas de conectividad y salud del servicio (sube archivos de prueba al almacenamiento).
        *   `ExecutorController`: (Funcionalidad no detallada explícitamente, pero implicada por el nombre en la lista de `Controllers`).
    *   **Dominio Primario**: **Informes Regulatorios**. Gestiona las entidades `AFIPF8126`, `AFIPF8125`, `SaldosClientes`, `Padron8601` e `Infestadistica`, que representan las estructuras de datos para los informes regulatorios. Las entidades de dominio clave incluyen `Comprobante`, `Cuenta`, `CuentaCVU`, `Operacion`, `OperacionTransferencia`, `Organizacion`, `Parametro`, `PSP`, `RIHistorico`, `RIRegistroTemporalF8126`, `SaldoHistorico`, `TipoComprobante` y `TipoOperacion`.
    *   **Alcance del Servicio/Proyecto**: Automatización de la generación y disponibilización de informes y archivos regulatorios/financieros en un *storage*. Actúa como un sistema de soporte para el cumplimiento normativo y la auditoría interna, centralizando la lógica de generación de estos archivos. Es crítico para asegurar el cumplimiento de la plataforma con las regulaciones financieras locales.
    *   **Dependencias**: Utiliza MediatR para comandos y consultas. Sus componentes incluyen `Shared.RegulatoryInformation.Api`, `Application`, `Domain`, `DataAccess.EntityFramework`, `EventBus`, `Common`, `Shared.Cache` y `Shared.Contract`. Forma parte del `namespace` de Kubernetes `bind-prod` (implícito por el contexto de que `ArchivosRI` a menudo se asocia con `Bind Aceptador`).
    *   **Arquitectura/Detalles Técnicos Relevantes**: Servicio .NET 8. Sigue una Arquitectura Limpia.

2.  **ArchivosRI (proyecto específico/lógica)**
    *   **Dominio**: Procesamiento específico de archivos regulatorios.
    *   **Descripción Funcional**: Procesamiento de archivos, validación de formato, extracción de datos, manejo de errores de procesamiento y procesamiento por lotes para archivos RI.
    *   **Alcance del Servicio/Proyecto**: Maneja aspectos específicos del procesamiento de archivos de información regulatoria.

3.  **Infra (ArchivosRI)**
    *   **Dominio**: Infraestructura específica para ArchivosRI.
    *   **Descripción Funcional**: Infrastructure as Code (IaC), scripts de despliegue, gestión de configuración, configuración de entornos y configuración de monitoreo para ArchivosRI.
    *   **Alcance del Servicio/Proyecto**: Proporciona una gestión de infraestructura dedicada para el ecosistema ArchivosRI.

---

### 3. Wallet Service

El proyecto Wallet Service es un sistema integral que abarca diversas funcionalidades relacionadas con la gestión de billeteras electrónicas, operaciones financieras, cálculo de costos y la integración con sistemas externos. Es una plataforma financiera completa y moderna, que maneja transacciones de billetera, QR, POS e integraciones con Visa/Coelsa. Incluye más de 45 proyectos.

#### Servicios Principales del Ecosistema Wallet Service:

1.  **Wallet.BFF**
    *   **Tipo de Servicio**: Backend for Frontend (BFF).
    *   **Descripción Funcional**: Proporciona una API unificada para la gestión de comprobantes, cuentas (incluyendo CVU y saldos), operaciones financieras (transferencias, pagos QR, Debin), préstamos, tarjetas y usuarios. Es la interfaz principal para las aplicaciones de usuario final.
    *   **Endpoints Clave**:
        *   `ComprobanteController`: Obtención de comprobantes por ID o por filtros.
        *   `CuentaController`: Gestión de cuentas, CVU, saldos, asignación de alias, creación y eliminación de cuentas y CVU, y obtención de destinatarios frecuentes y parámetros.
        *   `EchoController`: Endpoint de prueba.
        *   `OperacionController`: Obtención de operaciones y movimientos, realización de transferencias, pagos QR y gestión de Debin.
        *   `PrestamoController`: Gestión de préstamos (verificación de CUIL, cuotas habilitadas, cálculo de financiación, disponible e inserción).
        *   `TarjetaController`: Gestión de tarjetas (alta, obtención por cliente o token, eliminación, generación de token de pago y procesamiento de cash-in).
        *   `TinController`: Solicitud de QR de TIN y obtención de parámetros de TIN.
        *   `UsuarioController`: Login, recuperación de contraseña, cambio de credenciales y logout.
    *   **Dominio Primario**: **Gestión de Cuentas de Billetera**, **Operaciones Financieras**, **Gestión de Tarjetas**, **Autenticación de Usuario**. El subdominio `Entities/Base` define entidades base con capacidades de auditoría.
    *   **Alcance del Servicio/Proyecto**: Proporciona una gestión integral de las cuentas de billetera, incluyendo la creación, consulta, actualización y eliminación de cuentas y CVU, así como la gestión de saldos y destinatarios frecuentes. Es la lógica central para las operaciones financieras.
    *   **Dependencias**: Se integra con `MsComprobante`, `MsBind`, `MsComprobanteQueries`, `MsArdid`, `MsDispatcher`, `MsCalculadorCostos`, `MsSiscri`, `MsPosicionamiento` y `MsInvestmentService`. Utiliza MediatR (implícito por arquitectura estándar). Forma parte del `namespace` de Kubernetes `wallet-prod`.

2.  **Wallet.Bind**
    *   **Tipo de Servicio**: Core Service (Integración).
    *   **Descripción Funcional**: Se encarga de la integración con el sistema Bind. Gestiona cuentas bancarias, CVU, alias, Debin y transferencias (entrantes y salientes). Es el puente entre la billetera y la infraestructura bancaria.
    *   **Endpoints Clave**:
        *   `AccountController`: Obtiene información de cuentas por alias o CBU/CVU, y lista cuentas con saldo. También obtiene transacciones de cuentas bancarias.
        *   `CvuController`: Crea, asigna/actualiza alias, actualiza y elimina CVU para clientes.
        *   `DebinController`: Crea solicitudes de Debin y suscripciones de Debin, y obtiene Debin por ID.
        *   `EchoController`: Endpoints de prueba y diagnóstico.
        *   `IncomingTransferController`: Obtiene transferencias entrantes por ID.
        *   `TransferController`: Obtiene listados de transferencias, obtiene transferencias por ID, realiza transferencias salientes y crea transferencias RXT (Request for Transfer).
        *   `WebHookController`: Recibe notificaciones de transferencias entrantes a través de webhooks.
    *   **Dominio Primario**: **Integración Bancaria (Coelsa)**, **Gestión de CVU**, **DEBIN**, **Transferencias**.
    *   **Alcance del Servicio/Proyecto**: Proporciona acceso a la información de cuentas y transacciones bancarias a través de la integración con Coelsa. Gestiona el ciclo de vida de los CVU y alias.
    *   **Dependencias**: Interactúa con Coelsa. Utiliza MediatR (implícito por arquitectura estándar). Forma parte del `namespace` de Kubernetes `wallet-prod`.

3.  **Wallet.CalculadorCostos**
    *   **Tipo de Servicio**: Core Service (Cálculo de Costos).
    *   **Descripción Funcional**: Servicio especializado en el cálculo de costos asociados a transacciones y clientes. Gestiona cargos, clientes, segmentos y tipos de transacción para aplicar políticas de precios y comisiones flexibles.
    *   **Endpoints Clave**:
        *   `CargoController`: Gestión de cargos (creación, obtención por ID, actualización, eliminación) y obtención de cargos por tipo de transacción.
        *   `ClienteController`: Gestión de clientes (creación, obtención por ID, actualización, eliminación), asignación/desasignación de cargos y segmentos a clientes/perfiles, y obtención de clientes por ID externo.
        *   `EchoController`: Endpoints de prueba y diagnóstico.
        *   `OperacionController`: Calcula los costos para una operación.
        *   `SegmentoController`: Gestión de segmentos (creación, obtención por ID, actualización, eliminación) y asignación/desasignación de cargos a segmentos.
        *   `TipoTransaccionController`: Gestión de tipos de transacción (creación, obtención por ID, actualización, eliminación).
    *   **Dominio Primario**: **Cálculo de Costos**, **Cargos**, **Clientes**, **Segmentos**, **Tipos de Transacción**. El subdominio `Entities/Base` define entidades base con capacidades de auditoría.
    *   **Alcance del Servicio/Proyecto**: Es el corazón del servicio, encargado de aplicar la lógica de cálculo de costos a las operaciones. Proporciona una gestión completa de los clientes y su relación con cargos y segmentos.
    *   **Dependencias**: Utiliza MediatR (implícito por arquitectura estándar). Interactúa con otros servicios que necesitan cálculo de costos. Forma parte del `namespace` de Kubernetes `wallet-prod`.

4.  **Wallet.Cuenta**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Crea una nueva cuenta, aplica reglas de negocio (ej. CUIT/CUIL único), persiste la entidad de cuenta y publica `CuentaCreadaEvent`. Gestión completa del ciclo de vida de las cuentas de usuario, onboarding, CVU, cumplimiento y autenticación.
    *   **Endpoints Clave**: `CuentaController` (punto de entrada principal para la gestión de cuentas).
    *   **Dominio Primario**: **Cuenta**. Raíz agregada para el dominio de cuentas, encapsulando datos, lógica de negocio y reglas de validación. Incluye propiedades como CuitCuil, Nombre, Apellido, Email, Celular, FechaNacimiento, esPEP, esFACTA, esUIF. Contiene métodos de fábrica estáticos (`AddCuenta`, `UpdateCuenta`, `DeleteCuenta`).
    *   **Alcance del Servicio/Proyecto**: Servicio core responsable de la gestión de cuentas. Genera un evento crítico (`CuentaCreadaEvent`) para todo el ecosistema de la billetera.
    *   **Dependencias**: Llama a servicios de verificación para reglas de negocio (ej. unicidad de CUIT/CUIL). Publica `CuentaCreadaEvent` en el bus de eventos. Otros servicios como `Wallet.Operaciones` o `Wallet.Notificaciones` se suscribirían a `CuentaCreadaEvent` para crear registros de saldo, enviar correos electrónicos de bienvenida o provisionar recursos.
    *   **Arquitectura/Detalles Técnicos Relevantes**: Utiliza Arquitectura Limpia y patrones CQRS. Utiliza MediatR.

5.  **AKS-Manifests (Wallet Service)**
    *   **Dominio**: Orquestación y despliegue.
    *   **Descripción Funcional**: Manifiestos de Kubernetes para desplegar los servicios de Wallet Service en Azure AKS. Incluye configuraciones de recursos, políticas de escalado, verificaciones de salud y redes.
    *   **Alcance del Servicio/Proyecto**: Permite despliegues consistentes y seguros en la nube. Forma parte de la infraestructura para el `namespace` de Kubernetes `wallet-prod`.

6.  **Fintexa.Nugets**
    *   **Dominio**: Paquetes NuGet compartidos.
    *   **Descripción Funcional**: Repositorio de librerías y utilidades comunes distribuidas como paquetes NuGet para su uso en varios microservicios de Wallet.
    *   **Alcance del Servicio/Proyecto**: Promueve la reutilización del código y la estandarización.

7.  **Infra (Wallet Service)**
    *   **Dominio**: Infraestructura compartida.
    *   **Descripción Funcional**: Scripts, configuraciones y recursos de infraestructura reutilizables para todos los servicios de Wallet. Incluye automatización, monitoreo y gestión de entornos. Proporciona Infrastructure as Code (Terraform, plantillas ARM), gestión de entornos, pipelines de CI/CD, gestión centralizada de configuraciones y configuración de monitoreo.
    *   **Alcance del Servicio/Proyecto**: Proporciona la infraestructura subyacente para el `namespace` de Kubernetes `wallet-prod`.

8.  **Jobs.Bind (Wallet Service)**
    *   **Dominio**: Jobs de integración BIND.
    *   **Descripción Funcional**: Procesos por lotes y jobs para sincronización, conciliación y mantenimiento de datos con BIND en el contexto de Wallet.
    *   **Alcance del Servicio/Proyecto**: Procesos de backend para la consistencia y el mantenimiento de los datos en Wallet.

9.  **Jobs.Internal**
    *   **Dominio**: Jobs internos.
    *   **Descripción Funcional**: Procesos internos de mantenimiento, limpieza y optimización de datos en Wallet Service.
    *   **Alcance del Servicio/Proyecto**: Asegura la salud y el rendimiento de los datos dentro del ecosistema Wallet.

10. **Prueba.Repo (Wallet Service)**
    *   **Dominio**: Repositorio de pruebas.
    *   **Descripción Funcional**: Proyectos de ejemplo y pruebas de integración, validación de nuevas funcionalidades y experimentación en Wallet Service.
    *   **Alcance del Servicio/Proyecto**: Apoya los esfuerzos de desarrollo y garantía de calidad para Wallet.

11. **Shared.Crypto.Lirium**
    *   **Dominio**: Integración cripto.
    *   **Descripción Funcional**: Servicio para la integración con Lirium, permitiendo operaciones cripto y gestión de billeteras digitales.
    *   **Alcance del Servicio/Proyecto**: Extiende la funcionalidad de Wallet a la criptomoneda.

12. **Shared.Debin (Wallet Service)**
    *   **Tipo de Servicio**: Shared Service (DEBIN).
    *   **Descripción Funcional**: Servicio compartido para operaciones DEBIN dentro del ecosistema Wallet.
    *   **Dominio Primario**: **DEBIN**.
    *   **Alcance del Servicio/Proyecto**: Centraliza la lógica DEBIN para los servicios de Wallet.

13. **Shared.DispatcherCoelsaBind**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Probablemente despacha mensajes/solicitudes entre los sistemas Coelsa y Bind.
    *   **Dominio Primario**: **Despachador de Integración**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos de Wallet Service.

14. **Shared.InvestmentService**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Proporciona funcionalidades relacionadas con la inversión.
    *   **Dominio Primario**: **Inversión**.
    *   **Dependencias**: Forma parte de Wallet Service, se integra con `Wallet.BFF`.

15. **Shared.PixRoaming**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Probablemente maneja funcionalidades de roaming relacionadas con PIX (sistema de pago instantáneo brasileño), posiblemente para transferencias internacionales o interoperabilidad.
    *   **Dominio Primario**: **Pagos Transfronterizos/Roaming**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos de Wallet Service.

16. **Shared.QueueSentinel**
    *   **Tipo de Servicio**: Shared Service.
    *   **Descripción Funcional**: Probablemente monitorea y gestiona colas de mensajes, asegurando su salud y correcto funcionamiento.
    *   **Dominio Primario**: **Monitoreo/Gestión de Colas**.
    *   **Alcance del Servicio/Proyecto**: Forma parte de los servicios compartidos de Wallet Service.

17. **Shared.SwaggerDocGenerator**
    *   **Dominio**: Herramienta de documentación.
    *   **Descripción Funcional**: Servicio para fusionar y generar documentación Swagger unificada para las APIs de Wallet.
    *   **Alcance del Servicio/Proyecto**: Centraliza la generación de documentación de API para el ecosistema Wallet.

18. **Wallet.APP**
    *   **Dominio**: Aplicación cliente.
    *   **Descripción Funcional**: Aplicación móvil (MAUI) para usuarios finales de la billetera digital, permitiendo la gestión de cuentas, operaciones y consultas.
    *   **Alcance del Servicio/Proyecto**: Interfaz de usuario principal para el Wallet Service.

19. **Wallet.AppSDK**
    *   **Dominio**: SDK de integración.
    *   **Descripción Funcional**: Kit de desarrollo para integrar funcionalidades de Wallet en aplicaciones externas.
    *   **Alcance del Servicio/Proyecto**: Permite la integración de terceros con Wallet Service.

20. **Wallet.AzureFunction**
    *   **Dominio**: Funciones serverless.
    *   **Descripción Funcional**: Conjunto de Azure Functions para procesamiento serverless, integración con eventos y automatización de tareas en Wallet.
    *   **Alcance del Servicio/Proyecto**: Proporciona capacidades de procesamiento escalables y basadas en eventos.

21. **Wallet.CargaMasiva**
    *   **Dominio**: Carga masiva de datos para Wallet.
    *   **Descripción Funcional**: Creación masiva de cuentas, importación masiva de datos, validación de archivos masivos, seguimiento del progreso y generación de informes de errores detallados. Servicio para la carga y procesamiento masivo de datos en Wallet.
    *   **Alcance del Servicio/Proyecto**: Maneja eficientemente operaciones de datos a gran escala para Wallet.

22. **Wallet.Comprobante**
    *   **Dominio**: Generación y gestión de comprobantes fiscales.
    *   **Descripción Funcional**: Integración AFIP (generación de comprobantes fiscales), generación de PDF, envío automático por correo electrónico, almacenamiento digital de comprobantes y un motor de consulta para comprobantes históricos. Servicio para la generación, almacenamiento y consulta de comprobantes de operaciones.
    *   **Alcance del Servicio/Proyecto**: Asegura el cumplimiento fiscal y proporciona acceso a comprobantes históricos.

23. **Wallet.Comprobante.Queries**
    *   **Dominio**: Servicio de consultas de comprobantes.
    *   **Descripción Funcional**: Consultas de comprobantes, motor de búsqueda avanzado, filtros complejos, capacidades de exportación y trazabilidad de consultas. Servicio para consultas avanzadas sobre comprobantes, filtros e informes.
    *   **Alcance del Servicio/Proyecto**: Optimizado para operaciones de lectura en comprobantes.

24. **Wallet.Consentimiento**
    *   **Dominio**: Gestión de consentimientos.
    *   **Descripción Funcional**: Servicio para gestionar los consentimientos de los usuarios, flujos de autorización y auditoría.
    *   **Alcance del Servicio/Proyecto**: Asegura el cumplimiento de la privacidad de datos y las regulaciones de consentimiento del usuario.

25. **Wallet.Cuenta.Queries**
    *   **Dominio**: Servicio de consultas optimizado para cuentas.
    *   **Descripción Funcional**: API de solo lectura, consultas optimizadas para alto rendimiento, consultas analíticas complejas, vistas materializadas y múltiples niveles de caché.
    *   **Alcance del Servicio/Proyecto**: Diseñado para operaciones de lectura de alto rendimiento en datos de cuentas.

26. **Wallet.Notificaciones**
    *   **Dominio**: Sistema integral de notificaciones.
    *   **Descripción Funcional**: Multicanal (Email, SMS, Push, Webhooks), plantillas personalizables por evento, programación, seguimiento de entrega y gestión de preferencias de usuario.
    *   **Alcance del Servicio/Proyecto**: Sistema de notificaciones completo para el ecosistema Wallet.

27. **Wallet.Operaciones.Queries**
    *   **Dominio**: Servicio de consultas optimizado para operaciones.
    *   **Descripción Funcional**: Consultas de transacciones optimizadas, acceso a datos históricos, estados en tiempo real, análisis de operaciones y alto rendimiento para consultas.
    *   **Alcance del Servicio/Proyecto**: Optimizado para operaciones de lectura en transacciones financieras.

28. **Wallet.Reportes**
    *   **Tipo de Servicio**: Servicio de Soporte.
    *   **Descripción Funcional**: Gestiona informes para el ecosistema Wallet.
    *   **Dominio Primario**: **Informes**.
    *   **Alcance del Servicio/Proyecto**: Proporciona funcionalidades de informes para el Wallet Service.

---

### 4. CVU Collect

Este ecosistema gestiona operaciones masivas de CVU, integración bancaria y automatización de transferencias. Es complejo y cubre una amplia gama de entidades financieras, actuando como un intermediario clave en el ecosistema de pagos para integraciones bancarias y gestión de webhooks. Incluye más de 8 proyectos.

#### Servicios Principales del Ecosistema CVU Collect:

1.  **Middleware.Aggregator**
    *   **Dominio**: Agregación de cuentas CVU.
    *   **Descripción Funcional**: Busca, consolida y normaliza información de cuentas CVU de múltiples fuentes.
    *   **Endpoints Clave**: `AggregatorController` (implícito, mencionado como controlador en el directorio).
    *   **Alcance del Servicio/Proyecto**: Centraliza y estandariza los datos de las cuentas CVU.
    *   **Dependencias**: Utiliza MediatR (implícito por arquitectura estándar). Forma parte del `namespace` de Kubernetes `cvucollect-prod` (implícito por el nombre del ecosistema).

2.  **Middleware.BulkUploadProcess**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Carga archivos de procesamiento de CVU en lotes. Inicia el procesamiento de datos de CVU previamente cargados.
    *   **Endpoints Clave**:
        *   `DemoController` (`Demo2Controller`): CRUD básico para una entidad "Demo2" genérica (probablemente ejemplo/prueba).
        *   `EchoController`: Endpoints de prueba y diagnóstico.
        *   `LoadFileController`: Carga un lote de procesamiento de CVU y guarda el lote en la base de datos.
        *   `ProcesamientoController`: Procesa un lote de CVU.
        *   `SubDemoController` (`SubDemo2Controller`): CRUD básico para "SubDemo2" asociada a una "Demo2" principal (probablemente ejemplo de entidad anidada).
    *   **Dominio Primario**: **Procesamiento de Carga Masiva**, **CVU**. Las entidades incluyen `Batch`, `BatchItem`, `CvuBatchItem`, `Demo2` y `SubDemo2`.
    *   **Alcance del Servicio/Proyecto**: Permite la ingesta masiva de datos de CVU a través de archivos.
    *   **Dependencias**: Utiliza MediatR (implícito por arquitectura estándar). Forma parte del `namespace` de Kubernetes `cvucollect-prod` (implícito por el nombre del ecosistema).

3.  **Middleware.Financial**
    *   **Tipo de Servicio**: Core Service.
    *   **Descripción Funcional**: Servicios intermedios para operaciones financieras, control de flujo y validación de transacciones.
    *   **Dominio Primario**: **Lotes de CVU**, **Transferencias**. Las entidades incluyen `CvuBatch`, `CvuBatchItem`, `CvuBatchItemStatus` (enumeración), `CvuBatchStatus` (enumeración), `OriginDebit` (comentado) y `Transference`.
    *   **Alcance del Servicio/Proyecto**: Desempeña un papel en las operaciones financieras, el control de flujo y la validación de transacciones dentro de CVUCollect.
    *   **Dependencias**: Utiliza MediatR (implícito por arquitectura estándar). Forma parte del `namespace` de Kubernetes `cvucollect-prod` (implícito por el nombre del ecosistema).

4.  **StateMonitor**
    *   **Tipo de Servicio**: Servicio Principal.
    *   **Descripción Funcional**: Monitorea el estado de las operaciones dentro de CVUCollect.
    *   **Dominio Primario**: **Monitoreo de Estado**.
    *   **Alcance del Servicio/Proyecto**: Controla el estado operativo del ecosistema CVUCollect.

5.  **AKS-Manifests (CVU Collect)**
    *   **Dominio**: Orquestación y despliegue.
    *   **Descripción Funcional**: Manifiestos de Kubernetes para desplegar los servicios de CVUCollect en Azure AKS.
    *   **Alcance del Servicio/Proyecto**: Proporciona infraestructura para el `namespace` de Kubernetes `cvucollect-prod` (implícito).

6.  **Infra (CVU Collect)**
    *   **Dominio**: Infraestructura compartida.
    *   **Descripción Funcional**: Scripts y configuraciones reutilizables para los servicios de CVUCollect. Proporciona Infrastructure as Code (Terraform, plantillas ARM), gestión de entornos, pipelines de CI/CD, gestión centralizada de configuraciones y configuración de monitoreo.
    *   **Alcance del Servicio/Proyecto**: Proporciona infraestructura compartida para CVUCollect.

7.  **Prueba.Repo (CVU Collect)**
    *   **Dominio**: Repositorio de pruebas.
    *   **Descripción Funcional**: Proyectos de ejemplo y pruebas de integración en CVUCollect. Incluye ejemplos de pruebas, ejemplos de integración, documentación de ejemplos, mejores prácticas y pruebas de concepto.
    *   **Alcance del Servicio/Proyecto**: Apoya los esfuerzos de desarrollo y garantía de calidad para CVUCollect.

8.  **Shared.ApiBank (CVU Collect)**
    *   **Dominio**: Integración bancaria.
    *   **Descripción Funcional**: Servicio de abstracción para la integración con múltiples bancos, normalización de protocolos y balanceo de carga en CVUCollect.
    *   **Alcance del Servicio/Proyecto**: Centraliza la integración bancaria para los servicios de CVUCollect. (Es el mismo que `Shared.ApiBank.Api` pero contextualizado para CVUCollect).

---

### 5. Bind Configuration

Este ecosistema proporciona una gestión centralizada de configuraciones, conexiones, endpoints y parámetros de servicio. Incluye más de 5 proyectos, un Frontend React + Vite y un BFF especializado.

#### Servicios Principales del Ecosistema Bind Configuration:

1.  **Bind.Configuration.BFF.Api** (referido como Bind.Configuration.BFF en la documentación)
    *   **Tipo de Servicio**: Backend for Frontend (BFF). BFF centralizado para las configuraciones del sistema y los datos maestros.
    *   **Descripción Funcional**: Gestiona acuerdos, datos anexos, sucursales, rubros, cajas registradoras, usuarios centralizadores, acuerdos comerciales, comisiones, dispositivos, entidades, grupos de entidades, acuerdos de grupo, días festivos, operaciones, métodos de pago, posicionamientos, procesadores, QRs, informes, grupos de reglas, reglas, especificaciones, impuestos, transacciones y billeteras. También maneja la autenticación de usuarios, la recuperación de contraseñas y la validación de políticas de contraseñas.
    *   **Endpoints Clave**:
        *   `AuthenticationController`: Login, logout, recuperación de contraseña, cambio de contraseña, validación de políticas de contraseña.
        *   `AgreementsController`: Gestión de convenios (CRUD, filtrar, por código).
        *   `AnnexController`: Proporciona datos maestros y catálogos (categorías de IVA, rubros, tipos de cuenta, condiciones de IIBB, Sicore, tipos de persona, canales, formas de pago, tipos de transacción, agentes de retención).
        *   `BranchesController`: Gestión de sucursales (obtener, crear, modificar, eliminar, exportar CSV).
        *   `CaptionsController`: Obtiene rubros (generales, por comercio).
        *   `CashesController`: Gestión de cajas (obtener, crear, modificar, eliminar, exportar CSV, descargar QR en PDF).
        *   `CentralizerUserController`: Gestión de usuarios administradores centralizadores (CRUD, restablecer contraseña).
        *   `ChannelsController`: Gestión de canales de pago y configuraciones (habilitar QR/MPOS/Botón Simple, obtener canales/procesadores, crear/actualizar configuraciones de canales).
        *   `CommercesAgreementsController`: Gestión de relaciones comercio-convenio (crear, eliminar, editar).
        *   `CommercesController`: Gestión de comercios (obtener, eliminar, actualizar datos/comisiones/rubros/impuestos/especificaciones, crear, exportar, gestionar usuarios administradores, restablecer contraseña, obtener/descargar rendiciones, habilitar/deshabilitar recaudación por transferencia (RXT)).
        *   `CommissionsController`: Obtiene listas de comisiones.
        *   `DevicesController`: Obtiene dispositivos disponibles para asociar.
        *   `EntitiesController`: Gestión de entidades (CRUD, exportar CSV, especificaciones, plantillas de correo electrónico, usuarios de entidad, verificación, agregar canales).
        *   `EntitiesGroupsController`: Gestión de relaciones entidad-grupo (obtener, crear, eliminar, actualizar).
        *   `FilesController`: Gestión de archivos de cobro (obtener, descargar), archivos de cobro de billetera (obtener, descargar) e imágenes.
        *   `GroupsAgreementsController`: Gestión de relaciones grupo-convenio (crear, editar, eliminar, obtener).
        *   `GroupsController`: Gestión de grupos (CRUD, filtrar, por código).
        *   `HolidaysController`: Gestión de feriados (obtener por año, crear, actualizar, eliminar).
        *   `OperationsController`: Obtiene movimientos de cuenta, exporta movimientos a CSV, obtiene detalles/saldo de cuenta, obtiene tipos de comprobantes.
        *   `PaymentMethodsController`: Obtiene una lista de métodos de pago.
        *   `PositioningsController`: Obtiene información geográfica (países, provincias, localidades).
        *   `ProcessorsController`: Obtiene una lista de procesadores.
        *   `QrsController`: Genera códigos QR para un comercio.
        *   `ReportsController`: Gestión de informes (obtener tipos/estados, obtener todos, crear, descargar archivos).
        *   `RuleGroupsController`: Gestión de grupos de reglas (CRUD, obtener por comercio/entidad, asignar/desasignar a comercio/entidad, agregar/eliminar reglas).
        *   `RulesController`: Gestión de reglas individuales (CRUD, filtrar, por ID).
        *   `SpecificationsController`: Obtiene listas de especificaciones (generales, por entidad, por código de entidad).
        *   `TaxesController`: Obtiene listas de impuestos.
        *   `TransactionsController`: Obtiene transacciones (paginadas o por ID), exporta a CSV, obtiene el estado de la transacción y procesa reembolsos.
        *   `VoucherController`: Creación de comprobantes.
        *   `WalletsController`: Gestión de billeteras (obtener billeteras Coelsa, billeteras activas, eliminar, crear) y gestión de segmentos de billeteras (listar, actualizar).
    *   **Dominio Primario**: **Gestión de Configuraciones**, **Datos Maestros**, **Autenticación y Autorización**, **Estructura Comercial** (Acuerdos, Sucursales, Cajas, Comercios, Canales), **Reglas**, **Entidades**, **Informes**, **Operaciones Financieras**, **Datos Geográficos**, **Billeteras**. Incluye subdominios para Autenticación, Acuerdos, Anexos, Sucursales, Canales, Comercios, Entidades, Grupos de Entidades, Archivos, Reglas, Especificaciones y Billeteras.
    *   **Alcance del Servicio/Proyecto**: Proporciona un BFF centralizado y completo para las configuraciones del sistema y los datos maestros. Permite una configuración flexible y detallada de todo el ecosistema Bind.
    *   **Dependencias**: Utiliza MediatR (implícito por arquitectura estándar). Forma parte del `namespace` de Kubernetes `bind-config-prod` (implícito).

2.  **AKS-Manifests (Bind Configuration)**
    *   **Dominio**: Orquestación y despliegue.
    *   **Descripción Funcional**: Manifiestos de Kubernetes para desplegar los servicios de Bind Configuration en Azure AKS.
    *   **Alcance del Servicio/Proyecto**: Proporciona infraestructura para el `namespace` de Kubernetes `bind-config-prod` (implícito).

3.  **BFF.Admin (Frontend)**
    *   **Tipo de Servicio**: Frontend React + Vite.
    *   **Descripción Funcional**: Interfaz de usuario para el sistema de configuración de Bind.
    *   **Dominio Primario**: **Interfaz de Usuario de Administración de Configuración**.
    *   **Dependencias**: Interactúa con `Bind.Configuration.BFF.Api`.
    *   **Alcance del Servicio/Proyecto**: Proporciona la interfaz de usuario para la administración de configuraciones del ecosistema Bind.

4.  **Prueba.Repo (Bind Configuration)**
    *   **Dominio**: Repositorio de pruebas.
    *   **Descripción Funcional**: Proyectos de ejemplo y pruebas de integración en Bind Configuration. Incluye ejemplos de pruebas, ejemplos de integración, documentación de ejemplos, mejores prácticas y pruebas de concepto.
    *   **Alcance del Servicio/Proyecto**: Apoya los esfuerzos de desarrollo y garantía de calidad para Bind Configuration.

---

### 6. Boton Simple (Legacy)

Esta es una solución legacy para pagos simples, integración rápida y mínima con el ecosistema Fintexa. Incluye más de 15 proyectos, como servicios core legacy, servicios de bóveda, servicios de formularios y procesadores legacy [2io**: API de la bóveda de datos sensibles.
    *   **Descripción Funcional**: Almacenamiento seguro, cifrado de datos, gestión de claves, control de acceso y pista de auditoría para datos sensibles. Servicio para el almacenamiento seguro, cifrado y gestión de datos sensibles en Boton Simple.
    *   **Alcance del Servicio/Proyecto**: Gestiona datos sensibles para el sistema de pago legacy.

3.  **BotonSimple.Boveda.Backoffice**
    *   **Dominio**: Backoffice de la bóveda.
    *   **Descripción Funcional**: Administración de la bóveda, gestión de usuarios, políticas de seguridad e informes de auditoría y configuración del sistema. Interfaz administrativa para gestionar usuarios de la bóveda, políticas de seguridad e informes.
    *   **Alcance del Servicio/Proyecto**: Proporciona la interfaz de usuario administrativa para la bóveda de datos sensibles.

4.  **BotonSimple.Boveda.Identity**
    *   **Dominio**: Gestión de identidades para la bóveda.
    *   **Descripción Funcional**: Verificación de identidad, tokens de acceso, autenticación multifactor, gestión de sesiones y manejo de eventos. Servicio para la verificación de identidad, autenticación multifactor y gestión de sesiones dentro de la bóveda.
    *   **Alcance del Servicio/Proyecto**: Gestiona identidades para un acceso seguro a la bóveda.

5.  **BotonSimple.Boveda.Iframe**
    *   **Dominio**: Interfaz iframe para la bóveda.
    *   **Descripción Funcional**: Interfaz incrustada, comunicación segura, manejo de origen cruzado (CORS), diseño responsivo y seguridad web. Servicio para la integración segura de la bóveda mediante iframes, manejo de CORS y comunicación segura.
    *   **Alcance del Servicio/Proyecto**: Proporciona una interfaz de usuario segura y empotrada para las interacciones con la bóveda.

6.  **BotonSimple.Identity**
    *   **Dominio**: Gestión de identidades legacy.
    *   **Descripción Funcional**: Servicio para la gestión básica de usuarios, autenticación y sesiones en Boton Simple.
    *   **Alcance del Servicio/Proyecto**: Maneja la identidad para el sistema legacy.

7.  **BotonSimple.Notificacion**
    *   **Dominio**: Notificaciones legacy.
    *   **Descripción Funcional**: Servicio para el envío de notificaciones multicanal (correo electrónico, SMS) y manejo de eventos de notificación.
    *   **Alcance del Servicio/Proyecto**: Proporciona capacidades de notificación para el sistema legacy.

8.  **BotonSimple.Payment**
    *   **Tipo de Servicio**: Core Service (Legacy).
    *   **Dominio**: Motor de pagos simple.
    *   **Descripción Funcional**: Servicio para el procesamiento básico de pagos, tokenización de tarjetas,a los aspectos de seguridad del sistema legacy.

10. **BotonSimple.VaultManager**
    *   **Dominio**: Gestión de vaults legacy.
    *   **Descripción Funcional**: Servicio para orquestar, monitorear y administrar vaults de datos sensibles en Boton Simple.
    *   **Alcance del Servicio/Proyecto**: Gestiona vaults de datos sensibles para el sistema legacy.

11. **Shared.Cybersource**
    *   **Dominio**: Integración Cybersource.
    *   **Descripción Funcional**: Servicio para la integración con el procesador Cybersource, procesamiento de pagos y detección de fraude.
    *   **Alcance del Servicio/Proyecto**: Proporciona procesamiento de pagos a través de Cybersource para el sistema legacy.

12. **Shared.Decidir**
    *   **Dominio**: Integración Decidir.
    *   **Descripción Funcional**: Procesamiento de pagos a través de Decidir, métodos de pago locales argentinos, integración con la API de Decidir, liquidación local y cumplimiento local. Servicio para la integración con el procesador Decidir, métodos de pago locales y liquidación de transacciones.
    *   **Alcance del Servicio/Proyecto**: Proporciona procesamiento de pagos a través de Decidir para el sistema legacy.

13. **AKS-Manifests (Boton Simple)**
    *   **Dominio**: Orquestación y despliegue.
    *   **Descripción Funcional**: Manifiestos de Kubernetes para desplegar los servicios de Boton Simple en Azure AKS.
    *   **Alcance del Servicio/Proyecto**: Proporciona infraestructura para el `namespace` de Kubernetes `botonsimple-prod`.

 
15. **Infra (boton Simple)**
    *   **Dominio**: Repositorio de pruebas.
    *   **Descripción Funcional**: Proyectos de ejemplo y pruebas de integración en Boton Simple. Incluye ejemplos de pruebas, ejemplos de integración, documentación de ejemplos, mejores prácticas y pruebas de concepto.
    *   **Alcance del Servicio/Proyecto**: Apoya los esfuerzos de desarrollo y garantía de calidad para Boton Simple.

## fuentes

Me has proporcionado una valiosa colección de documentos técnicos y análisis detallados del ecosistema Fintexa, incluyendo:
*   `ANALISIS_GENERAL1.md`
*   `ANALISIS_LIQ_IMP_CALCULAR.md`
*   `Analisis_Completo_Ecosistema_Fintexa.md`
*   `Analisis_Configuraciones_Ecosistema_Fintexa.md`
*   `Analisis_Detallado_Proyecto_por_Proyecto_Fintexa.md`
*   `ArchivosRI_Analysis.md`
*   `Bind_Aceptador_Analysis.md`
*   `Bind_Configuration_Analysis.md`
*   `CHANGELOG_Analisis_Fintexa.md`
*   `CVUCollect_Analysis.md`
*   `Contextoarquitectura.md`
*   `DOCUMENTACION_git_analysis_ia_doc2025.md`
*   `Fintexa_Full_Ecosystem_Analysis.md`
*   `GEMINI.md`
*   `GUIA_AUTENTICACION_AZURE_DEVOPS.md`
*   `README_FINTEXA_DOCUMENTACION_IA.md`
*   `README_git_analysis_ia_doc2025.md`
*   `Sistema Integral Retenciones y Percepciones_.md`
*   `Wallet_Service_Analysis.md`
*   `project_descriptions.md`
 
 