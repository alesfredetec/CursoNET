using System;
using System.Collections.Generic;

namespace TecnicasNoIf.Ejercicios
{
    /// <summary>
    /// SOLUCIÓN: State Pattern para eliminar switches de máquina de estado
    /// 
    /// MEJORAS APLICADAS:
    /// ✅ MEJORA 1: Interface común para todos los estados
    /// ✅ MEJORA 2: Cada estado encapsula su comportamiento
    /// ✅ MEJORA 3: Eliminados todos los switches duplicados
    /// ✅ MEJORA 4: Transiciones centralizadas y type-safe
    /// ✅ MEJORA 5: Extensible para nuevos estados sin modificar existentes
    /// ✅ MEJORA 6: Logging y auditoría integrados
    /// ✅ MEJORA 7: Testing aislado por estado
    /// </summary>

    // ✅ MEJORA 1: Order como Context que delega a States
    public class OrderImproved
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CustomerEmail { get; set; }
        public string TrackingNumber { get; set; }
        public string CancellationReason { get; set; }

        // ✅ MEJORA 2: Estado actual como objeto con comportamiento
        public IOrderState CurrentState { get; private set; }

        // History para auditoría
        public List<StateTransition> TransitionHistory { get; } = new();

        public OrderImproved(int id, decimal total, string customerEmail)
        {
            Id = id;
            Total = total;
            CustomerEmail = customerEmail;
            CreatedAt = DateTime.UtcNow;
            
            // Estado inicial
            TransitionTo(new PendingState());
        }

        /// <summary>
        /// ✅ MEJORA 3: Transición centralizada con logging
        /// </summary>
        public void TransitionTo(IOrderState newState)
        {
            var previousState = CurrentState?.GetType().Name ?? "None";
            CurrentState = newState;
            CurrentState.SetContext(this);
            
            // Auditoría de transiciones
            TransitionHistory.Add(new StateTransition
            {
                FromState = previousState,
                ToState = newState.GetType().Name,
                Timestamp = DateTime.UtcNow
            });

            Console.WriteLine($"Order {Id}: {previousState} → {newState.GetType().Name}");
        }

        // ✅ MEJORA 4: Delegación a estado actual - sin switches
        public bool ProcessPayment(decimal paymentAmount) => CurrentState.ProcessPayment(paymentAmount);
        public bool ConfirmPayment() => CurrentState.ConfirmPayment();
        public bool StartPreparation() => CurrentState.StartPreparation();
        public bool ShipOrder(string trackingNumber) => CurrentState.ShipOrder(trackingNumber);
        public bool DeliverOrder() => CurrentState.DeliverOrder();
        public bool CancelOrder(string reason) => CurrentState.CancelOrder(reason);
        public bool ProcessRefund(string reason) => CurrentState.ProcessRefund(reason);
        public string[] GetAvailableActions() => CurrentState.GetAvailableActions();
        public string GetStatusName() => CurrentState.GetStatusName();
    }

    /// <summary>
    /// ✅ MEJORA 5: Interface común para todos los estados
    /// </summary>
    public interface IOrderState
    {
        void SetContext(OrderImproved order);
        string GetStatusName();
        string[] GetAvailableActions();
        
        // Operaciones de estado - cada estado decide si las acepta
        bool ProcessPayment(decimal paymentAmount);
        bool ConfirmPayment();
        bool StartPreparation();
        bool ShipOrder(string trackingNumber);
        bool DeliverOrder();
        bool CancelOrder(string reason);
        bool ProcessRefund(string reason);
    }

    /// <summary>
    /// ✅ MEJORA 6: Estado base con comportamiento común
    /// </summary>
    public abstract class OrderStateBase : IOrderState
    {
        protected OrderImproved Order { get; private set; }

        public void SetContext(OrderImproved order)
        {
            Order = order;
        }

        public abstract string GetStatusName();
        public abstract string[] GetAvailableActions();

        // Implementación por defecto - no permitido
        public virtual bool ProcessPayment(decimal paymentAmount)
        {
            Console.WriteLine($"Order {Order.Id}: Cannot process payment in {GetStatusName()} state");
            return false;
        }

        public virtual bool ConfirmPayment()
        {
            Console.WriteLine($"Order {Order.Id}: Cannot confirm payment in {GetStatusName()} state");
            return false;
        }

        public virtual bool StartPreparation()
        {
            Console.WriteLine($"Order {Order.Id}: Cannot start preparation in {GetStatusName()} state");
            return false;
        }

        public virtual bool ShipOrder(string trackingNumber)
        {
            Console.WriteLine($"Order {Order.Id}: Cannot ship in {GetStatusName()} state");
            return false;
        }

        public virtual bool DeliverOrder()
        {
            Console.WriteLine($"Order {Order.Id}: Cannot deliver in {GetStatusName()} state");
            return false;
        }

        public virtual bool CancelOrder(string reason)
        {
            Console.WriteLine($"Order {Order.Id}: Cannot cancel in {GetStatusName()} state");
            return false;
        }

        public virtual bool ProcessRefund(string reason)
        {
            Console.WriteLine($"Order {Order.Id}: Cannot process refund in {GetStatusName()} state");
            return false;
        }
    }

    /// <summary>
    /// ✅ MEJORA 7: Estado Pending - comportamiento específico encapsulado
    /// </summary>
    public class PendingState : OrderStateBase
    {
        public override string GetStatusName() => "Pending";
        public override string[] GetAvailableActions() => new[] { "ProcessPayment", "CancelOrder" };

        public override bool ProcessPayment(decimal paymentAmount)
        {
            if (paymentAmount >= Order.Total)
            {
                Order.TransitionTo(new PaymentProcessingState());
                Console.WriteLine($"Order {Order.Id}: Payment processing started");
                return true;
            }
            
            Console.WriteLine($"Order {Order.Id}: Insufficient payment amount");
            return false;
        }

        public override bool CancelOrder(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new CancelledState());
            Console.WriteLine($"Order {Order.Id}: Cancelled (no charge) - {reason}");
            return true;
        }
    }

    /// <summary>
    /// ✅ MEJORA 8: Estado PaymentProcessing
    /// </summary>
    public class PaymentProcessingState : OrderStateBase
    {
        public override string GetStatusName() => "PaymentProcessing";
        public override string[] GetAvailableActions() => new[] { "ConfirmPayment", "CancelOrder" };

        public override bool ConfirmPayment()
        {
            Order.TransitionTo(new PaymentConfirmedState());
            Console.WriteLine($"Order {Order.Id}: Payment confirmed, ready for preparation");
            return true;
        }

        public override bool CancelOrder(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new CancelledState());
            Console.WriteLine($"Order {Order.Id}: Cancelled during payment processing - {reason}");
            return true;
        }
    }

    /// <summary>
    /// ✅ MEJORA 9: Estado PaymentConfirmed
    /// </summary>
    public class PaymentConfirmedState : OrderStateBase
    {
        public override string GetStatusName() => "PaymentConfirmed";
        public override string[] GetAvailableActions() => new[] { "StartPreparation", "CancelOrder", "ProcessRefund" };

        public override bool StartPreparation()
        {
            Order.TransitionTo(new PreparingState());
            Console.WriteLine($"Order {Order.Id}: Preparation started");
            return true;
        }

        public override bool CancelOrder(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new CancelledState());
            Console.WriteLine($"Order {Order.Id}: Cancelled (may charge cancellation fee) - {reason}");
            return true;
        }

        public override bool ProcessRefund(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new RefundedState());
            Console.WriteLine($"Order {Order.Id}: Refund processed - {reason}");
            return true;
        }
    }

    /// <summary>
    /// ✅ MEJORA 10: Estado Preparing
    /// </summary>
    public class PreparingState : OrderStateBase
    {
        public override string GetStatusName() => "Preparing";
        public override string[] GetAvailableActions() => new[] { "ShipOrder", "CancelOrder", "ProcessRefund" };

        public override bool ShipOrder(string trackingNumber)
        {
            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                Console.WriteLine($"Order {Order.Id}: Cannot ship without tracking number");
                return false;
            }

            Order.TrackingNumber = trackingNumber;
            Order.TransitionTo(new ShippedState());
            Console.WriteLine($"Order {Order.Id}: Shipped with tracking {trackingNumber}");
            return true;
        }

        public override bool CancelOrder(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new CancelledState());
            Console.WriteLine($"Order {Order.Id}: Cancelled during preparation (may charge cancellation fee) - {reason}");
            return true;
        }

        public override bool ProcessRefund(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new RefundedState());
            Console.WriteLine($"Order {Order.Id}: Refund processed - {reason}");
            return true;
        }
    }

    /// <summary>
    /// ✅ MEJORA 11: Estado Shipped
    /// </summary>
    public class ShippedState : OrderStateBase
    {
        public override string GetStatusName() => "Shipped";
        public override string[] GetAvailableActions() => new[] { "DeliverOrder", "ProcessRefund" };

        public override bool DeliverOrder()
        {
            Order.TransitionTo(new DeliveredState());
            Console.WriteLine($"Order {Order.Id}: Successfully delivered");
            return true;
        }

        public override bool ProcessRefund(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new RefundedState());
            Console.WriteLine($"Order {Order.Id}: Refund processed - {reason}");
            return true;
        }
    }

    /// <summary>
    /// ✅ MEJORA 12: Estado Delivered
    /// </summary>
    public class DeliveredState : OrderStateBase
    {
        public override string GetStatusName() => "Delivered";
        public override string[] GetAvailableActions() => new[] { "ProcessRefund" };

        public override bool ProcessRefund(string reason)
        {
            Order.CancellationReason = reason;
            Order.TransitionTo(new RefundedState());
            Console.WriteLine($"Order {Order.Id}: Refund processed - {reason}");
            return true;
        }
    }

    /// <summary>
    /// ✅ MEJORA 13: Estados finales - sin acciones disponibles
    /// </summary>
    public class CancelledState : OrderStateBase
    {
        public override string GetStatusName() => "Cancelled";
        public override string[] GetAvailableActions() => new string[0]; // Estado final
    }

    public class RefundedState : OrderStateBase
    {
        public override string GetStatusName() => "Refunded";
        public override string[] GetAvailableActions() => new string[0]; // Estado final
    }

    /// <summary>
    /// ✅ MEJORA 14: Auditoría de transiciones
    /// </summary>
    public class StateTransition
    {
        public string FromState { get; set; }
        public string ToState { get; set; }
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// ✅ MEJORA 15: Factory para crear estados - extensibilidad
    /// </summary>
    public static class OrderStateFactory
    {
        private static readonly Dictionary<string, Func<IOrderState>> StateCreators = new()
        {
            ["Pending"] = () => new PendingState(),
            ["PaymentProcessing"] = () => new PaymentProcessingState(),
            ["PaymentConfirmed"] = () => new PaymentConfirmedState(),
            ["Preparing"] = () => new PreparingState(),
            ["Shipped"] = () => new ShippedState(),
            ["Delivered"] = () => new DeliveredState(),
            ["Cancelled"] = () => new CancelledState(),
            ["Refunded"] = () => new RefundedState()
        };

        public static IOrderState CreateState(string stateName)
        {
            if (StateCreators.TryGetValue(stateName, out var creator))
            {
                return creator();
            }
            
            throw new ArgumentException($"Unknown state: {stateName}");
        }

        public static void RegisterState(string stateName, Func<IOrderState> creator)
        {
            StateCreators[stateName] = creator;
        }

        public static string[] GetAvailableStates() => StateCreators.Keys.ToArray();
    }

    /// <summary>
    /// ✅ MEJORA 16: Processor mejorado sin switches
    /// </summary>
    public class OrderProcessorImproved
    {
        private readonly List<OrderImproved> _orders = new();

        public OrderImproved CreateOrder(decimal total, string customerEmail)
        {
            var order = new OrderImproved(_orders.Count + 1, total, customerEmail);
            _orders.Add(order);
            return order;
        }

        public OrderImproved GetOrder(int orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }

        /// <summary>
        /// ✅ MEJORA 17: Reporting sin switches
        /// </summary>
        public Dictionary<string, int> GetOrderStatistics()
        {
            return _orders
                .GroupBy(o => o.GetStatusName())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// ✅ MEJORA 18: Workflow automation - ejemplo de extensión
        /// </summary>
        public void ProcessPendingOrders()
        {
            var pendingOrders = _orders.Where(o => o.GetStatusName() == "Pending");
            
            foreach (var order in pendingOrders)
            {
                // Auto-process payment for demo
                if (order.ProcessPayment(order.Total))
                {
                    Console.WriteLine($"Auto-processed payment for order {order.Id}");
                }
            }
        }
    }

    /*
    COMPARACIÓN DE MÉTRICAS:

    ANTES (OrderProcessorProblematic):
    ❌ Métodos con switches: 8
    ❌ Complejidad ciclomática promedio: 8-12
    ❌ Líneas por método: 30-50
    ❌ Estados hard-coded: enum sin comportamiento
    ❌ Mantenimiento: Muy difícil - cambios en 8+ lugares
    ❌ Testing: 50+ casos por método

    DESPUÉS (OrderProcessorImproved + States):
    ✅ Métodos con switches: 0
    ✅ Complejidad ciclomática máxima: 3
    ✅ Líneas por método: 5-15
    ✅ Estados: objetos con comportamiento encapsulado
    ✅ Mantenimiento: Fácil - un estado a la vez
    ✅ Testing: 5-8 casos por estado aislado

    BENEFICIOS DEL STATE PATTERN:

    1. ENCAPSULACIÓN POR ESTADO:
       - Cada estado maneja su comportamiento
       - Lógica relacionada agrupada
       - Sin código duplicado

    2. EXTENSIBILIDAD:
       - Nuevos estados sin modificar existentes
       - StateFactory para registro dinámico
       - Transiciones configurables

    3. MANTENIBILIDAD:
       - Cambios en un estado no afectan otros
       - Testing aislado por estado
       - Código auto-documentado

    4. AUDITORÍA:
       - Historia de transiciones
       - Logging integrado
       - Tracking de workflows

    5. TYPE SAFETY:
       - Transiciones controladas por código
       - No magic strings para estados
       - Compile-time safety

    CUÁNDO USAR STATE PATTERN:

    ✅ USAR cuando:
    - Workflows con muchos estados
    - Comportamiento varía significativamente por estado
    - Lógica de transiciones compleja
    - Necesitas auditoría de cambios de estado

    ❌ NO USAR cuando:
    - Pocos estados simples (2-3)
    - Transiciones triviales
    - Estados sin comportamiento diferenciado
    - Performance crítica con overhead de objetos

    PRÓXIMO EJERCICIO:
    04-Polimorfismo - Para comportamientos por tipo de objeto
    */
}