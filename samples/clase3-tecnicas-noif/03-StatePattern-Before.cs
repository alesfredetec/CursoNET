using System;

namespace TecnicasNoIf.Ejercicios
{
    /// <summary>
    /// PROBLEMA: Máquina de estado implementada con if/switch complejos
    /// 
    /// ISSUES IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Switch gigante para manejar estados
    /// ❌ PROBLEMA 2: Transiciones de estado hard-coded y frágiles
    /// ❌ PROBLEMA 3: Lógica de validación esparcida y duplicada
    /// ❌ PROBLEMA 4: Difícil agregar nuevos estados sin tocar código existente
    /// ❌ PROBLEMA 5: Testing complejo - muchos paths de ejecución
    /// ❌ PROBLEMA 6: No hay logging ni tracking de transiciones
    /// </summary>
    public class OrderProcessorProblematic
    {
        // ❌ PROBLEMA 1: Estados como enum sin comportamiento
        public enum OrderStatus
        {
            Pending,
            PaymentProcessing,
            PaymentConfirmed,
            Preparing,
            Shipped,
            Delivered,
            Cancelled,
            Refunded
        }

        public class Order
        {
            public int Id { get; set; }
            public OrderStatus Status { get; set; }
            public decimal Total { get; set; }
            public DateTime CreatedAt { get; set; }
            public string CustomerEmail { get; set; }
            public string TrackingNumber { get; set; }
            public string CancellationReason { get; set; }
        }

        /// <summary>
        /// Procesa pago con switch gigante - ANTI-PATTERN
        /// </summary>
        public bool ProcessPayment(Order order, decimal paymentAmount)
        {
            // ❌ PROBLEMA 2: Switch para cada operación - violación DRY
            switch (order.Status)
            {
                case OrderStatus.Pending:
                    if (paymentAmount >= order.Total)
                    {
                        order.Status = OrderStatus.PaymentProcessing;
                        Console.WriteLine($"Order {order.Id}: Payment processing started");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Order {order.Id}: Insufficient payment amount");
                        return false;
                    }

                case OrderStatus.PaymentProcessing:
                    Console.WriteLine($"Order {order.Id}: Payment already in progress");
                    return false;

                case OrderStatus.PaymentConfirmed:
                case OrderStatus.Preparing:
                case OrderStatus.Shipped:
                case OrderStatus.Delivered:
                    Console.WriteLine($"Order {order.Id}: Payment already processed");
                    return false;

                case OrderStatus.Cancelled:
                    Console.WriteLine($"Order {order.Id}: Cannot process payment for cancelled order");
                    return false;

                case OrderStatus.Refunded:
                    Console.WriteLine($"Order {order.Id}: Order already refunded");
                    return false;

                default:
                    throw new InvalidOperationException($"Unknown order status: {order.Status}");
            }
        }

        /// <summary>
        /// Confirma pago - MISMO SWITCH repetido
        /// </summary>
        public bool ConfirmPayment(Order order)
        {
            // ❌ PROBLEMA 3: Duplicación de switch con validaciones similares
            switch (order.Status)
            {
                case OrderStatus.Pending:
                    Console.WriteLine($"Order {order.Id}: Cannot confirm payment - no payment started");
                    return false;

                case OrderStatus.PaymentProcessing:
                    order.Status = OrderStatus.PaymentConfirmed;
                    Console.WriteLine($"Order {order.Id}: Payment confirmed, preparing order");
                    return true;

                case OrderStatus.PaymentConfirmed:
                    Console.WriteLine($"Order {order.Id}: Payment already confirmed");
                    return false;

                case OrderStatus.Preparing:
                case OrderStatus.Shipped:
                case OrderStatus.Delivered:
                    Console.WriteLine($"Order {order.Id}: Payment was already confirmed");
                    return false;

                case OrderStatus.Cancelled:
                case OrderStatus.Refunded:
                    Console.WriteLine($"Order {order.Id}: Cannot confirm payment for cancelled/refunded order");
                    return false;

                default:
                    throw new InvalidOperationException($"Unknown order status: {order.Status}");
            }
        }

        /// <summary>
        /// Inicia preparación - TERCER SWITCH igual
        /// </summary>
        public bool StartPreparation(Order order)
        {
            // ❌ PROBLEMA 4: Para agregar nuevo estado = tocar TODOS los switches
            switch (order.Status)
            {
                case OrderStatus.Pending:
                case OrderStatus.PaymentProcessing:
                    Console.WriteLine($"Order {order.Id}: Cannot start preparation - payment not confirmed");
                    return false;

                case OrderStatus.PaymentConfirmed:
                    order.Status = OrderStatus.Preparing;
                    Console.WriteLine($"Order {order.Id}: Preparation started");
                    return true;

                case OrderStatus.Preparing:
                    Console.WriteLine($"Order {order.Id}: Already being prepared");
                    return false;

                case OrderStatus.Shipped:
                case OrderStatus.Delivered:
                    Console.WriteLine($"Order {order.Id}: Already shipped/delivered");
                    return false;

                case OrderStatus.Cancelled:
                case OrderStatus.Refunded:
                    Console.WriteLine($"Order {order.Id}: Cannot prepare cancelled/refunded order");
                    return false;

                default:
                    throw new InvalidOperationException($"Unknown order status: {order.Status}");
            }
        }

        /// <summary>
        /// Envía pedido - CUARTO SWITCH
        /// </summary>
        public bool ShipOrder(Order order, string trackingNumber)
        {
            // ❌ PROBLEMA 5: Validaciones complejas anidadas en cada switch
            switch (order.Status)
            {
                case OrderStatus.Pending:
                case OrderStatus.PaymentProcessing:
                case OrderStatus.PaymentConfirmed:
                    Console.WriteLine($"Order {order.Id}: Cannot ship - not prepared yet");
                    return false;

                case OrderStatus.Preparing:
                    if (string.IsNullOrWhiteSpace(trackingNumber))
                    {
                        Console.WriteLine($"Order {order.Id}: Cannot ship without tracking number");
                        return false;
                    }
                    order.Status = OrderStatus.Shipped;
                    order.TrackingNumber = trackingNumber;
                    Console.WriteLine($"Order {order.Id}: Shipped with tracking {trackingNumber}");
                    return true;

                case OrderStatus.Shipped:
                    Console.WriteLine($"Order {order.Id}: Already shipped");
                    return false;

                case OrderStatus.Delivered:
                    Console.WriteLine($"Order {order.Id}: Already delivered");
                    return false;

                case OrderStatus.Cancelled:
                case OrderStatus.Refunded:
                    Console.WriteLine($"Order {order.Id}: Cannot ship cancelled/refunded order");
                    return false;

                default:
                    throw new InvalidOperationException($"Unknown order status: {order.Status}");
            }
        }

        /// <summary>
        /// Entrega pedido - QUINTO SWITCH
        /// </summary>
        public bool DeliverOrder(Order order)
        {
            switch (order.Status)
            {
                case OrderStatus.Pending:
                case OrderStatus.PaymentProcessing:
                case OrderStatus.PaymentConfirmed:
                case OrderStatus.Preparing:
                    Console.WriteLine($"Order {order.Id}: Cannot deliver - not shipped yet");
                    return false;

                case OrderStatus.Shipped:
                    order.Status = OrderStatus.Delivered;
                    Console.WriteLine($"Order {order.Id}: Successfully delivered");
                    return true;

                case OrderStatus.Delivered:
                    Console.WriteLine($"Order {order.Id}: Already delivered");
                    return false;

                case OrderStatus.Cancelled:
                case OrderStatus.Refunded:
                    Console.WriteLine($"Order {order.Id}: Cannot deliver cancelled/refunded order");
                    return false;

                default:
                    throw new InvalidOperationException($"Unknown order status: {order.Status}");
            }
        }

        /// <summary>
        /// Cancela pedido - SEXTO SWITCH con lógica compleja
        /// </summary>
        public bool CancelOrder(Order order, string reason)
        {
            // ❌ PROBLEMA 6: Lógica de negocio compleja mezclada con control de estado
            switch (order.Status)
            {
                case OrderStatus.Pending:
                case OrderStatus.PaymentProcessing:
                    // Cancelación simple - no hay costo
                    order.Status = OrderStatus.Cancelled;
                    order.CancellationReason = reason;
                    Console.WriteLine($"Order {order.Id}: Cancelled (no charge) - {reason}");
                    return true;

                case OrderStatus.PaymentConfirmed:
                case OrderStatus.Preparing:
                    // Cancelación con posible cargo
                    order.Status = OrderStatus.Cancelled;
                    order.CancellationReason = reason;
                    Console.WriteLine($"Order {order.Id}: Cancelled (may charge cancellation fee) - {reason}");
                    // TODO: Implementar lógica de cargo por cancelación
                    return true;

                case OrderStatus.Shipped:
                    // No se puede cancelar una vez enviado - debe usar refund
                    Console.WriteLine($"Order {order.Id}: Cannot cancel shipped order - use refund instead");
                    return false;

                case OrderStatus.Delivered:
                    Console.WriteLine($"Order {order.Id}: Cannot cancel delivered order - use refund instead");
                    return false;

                case OrderStatus.Cancelled:
                    Console.WriteLine($"Order {order.Id}: Already cancelled");
                    return false;

                case OrderStatus.Refunded:
                    Console.WriteLine($"Order {order.Id}: Already refunded");
                    return false;

                default:
                    throw new InvalidOperationException($"Unknown order status: {order.Status}");
            }
        }

        /// <summary>
        /// Procesa reembolso - SÉPTIMO SWITCH
        /// </summary>
        public bool ProcessRefund(Order order, string reason)
        {
            switch (order.Status)
            {
                case OrderStatus.Pending:
                case OrderStatus.PaymentProcessing:
                    Console.WriteLine($"Order {order.Id}: No payment to refund");
                    return false;

                case OrderStatus.PaymentConfirmed:
                case OrderStatus.Preparing:
                case OrderStatus.Shipped:
                case OrderStatus.Delivered:
                    // Puede procesar reembolso
                    order.Status = OrderStatus.Refunded;
                    order.CancellationReason = reason;
                    Console.WriteLine($"Order {order.Id}: Refund processed - {reason}");
                    return true;

                case OrderStatus.Cancelled:
                    // Depends - si ya se cobró algo, puede hacer refund
                    Console.WriteLine($"Order {order.Id}: Cannot refund cancelled order");
                    return false;

                case OrderStatus.Refunded:
                    Console.WriteLine($"Order {order.Id}: Already refunded");
                    return false;

                default:
                    throw new InvalidOperationException($"Unknown order status: {order.Status}");
            }
        }

        /// <summary>
        /// Obtiene acciones disponibles - OCTAVO SWITCH
        /// </summary>
        public string[] GetAvailableActions(Order order)
        {
            // ❌ PROBLEMA: Otro switch más para mantener sincronizado
            switch (order.Status)
            {
                case OrderStatus.Pending:
                    return new[] { "ProcessPayment", "CancelOrder" };

                case OrderStatus.PaymentProcessing:
                    return new[] { "ConfirmPayment", "CancelOrder" };

                case OrderStatus.PaymentConfirmed:
                    return new[] { "StartPreparation", "CancelOrder", "ProcessRefund" };

                case OrderStatus.Preparing:
                    return new[] { "ShipOrder", "CancelOrder", "ProcessRefund" };

                case OrderStatus.Shipped:
                    return new[] { "DeliverOrder", "ProcessRefund" };

                case OrderStatus.Delivered:
                    return new[] { "ProcessRefund" };

                case OrderStatus.Cancelled:
                    return new[] { }; // No actions available

                case OrderStatus.Refunded:
                    return new[] { }; // No actions available

                default:
                    return new[] { };
            }
        }

        /// <summary>
        /// Valida transición - MÁS LÓGICA CONDICIONAL
        /// </summary>
        public bool CanTransitionTo(Order order, OrderStatus newStatus)
        {
            // ❌ PROBLEMA: Matriz de transiciones hard-coded
            var current = order.Status;

            if (current == OrderStatus.Pending)
            {
                return newStatus == OrderStatus.PaymentProcessing || newStatus == OrderStatus.Cancelled;
            }

            if (current == OrderStatus.PaymentProcessing)
            {
                return newStatus == OrderStatus.PaymentConfirmed || newStatus == OrderStatus.Cancelled;
            }

            if (current == OrderStatus.PaymentConfirmed)
            {
                return newStatus == OrderStatus.Preparing || 
                       newStatus == OrderStatus.Cancelled || 
                       newStatus == OrderStatus.Refunded;
            }

            if (current == OrderStatus.Preparing)
            {
                return newStatus == OrderStatus.Shipped || 
                       newStatus == OrderStatus.Cancelled || 
                       newStatus == OrderStatus.Refunded;
            }

            if (current == OrderStatus.Shipped)
            {
                return newStatus == OrderStatus.Delivered || newStatus == OrderStatus.Refunded;
            }

            if (current == OrderStatus.Delivered)
            {
                return newStatus == OrderStatus.Refunded;
            }

            // Estados finales - no pueden transicionar
            return false;
        }
    }

    /*
    PROBLEMAS PRINCIPALES:

    1. MANTENIMIENTO IMPOSIBLE:
       - 8 métodos con switches similares
       - Agregar nuevo estado = modificar 8+ lugares
       - Cambiar lógica de transición = buscar en todos los switches

    2. COMPLEJIDAD EXTREMA:
       - Cada método tiene complejidad ciclomática > 8
       - Lógica de validación duplicada
       - Difícil seguir el flujo de estados

    3. FRAGILIDAD:
       - Fácil introducir bugs al cambiar un switch
       - No hay garantía de consistencia entre métodos
       - Testing requiere cubrir todos los paths

    4. VIOLACIÓN PRINCIPIOS:
       - Single Responsibility: cada método hace demasiado
       - Open/Closed: no extensible sin modificar
       - DRY: lógica de validación repetida

    5. ESCALABILIDAD NULA:
       - No se puede agregar estados dinámicamente
       - Lógica de negocio hard-coded
       - No hay logging ni auditoría de transiciones

    6. TESTING NIGHTMARE:
       - Cada método necesita test para todos los estados
       - Difícil mock de comportamientos específicos
       - Setup complejo para casos edge

    OBJETIVO DEL EJERCICIO:
    Refactorizar usando State Pattern para:
    ✅ Encapsular comportamiento por estado
    ✅ Eliminar switches duplicados
    ✅ Hacer extensible para nuevos estados
    ✅ Centralizar lógica de transiciones
    ✅ Facilitar testing aislado

    TIEMPO ESTIMADO: 75 minutos
    DIFICULTAD: Avanzado
    */
}