using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintexa.CommissionSystem
{
    // Enums para definir tipos de transacciones y comercios
    public enum TransactionType
    {
        CreditCard,
        DebitCard,
        BankTransfer,
        QRPayment
    }

    public enum MerchantTier  
    {
        Standard,
        Premium,
        HighVolume
    }

    // Modelos de datos
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public Guid MerchantId { get; set; }
        public DateTime ProcessedAt { get; set; }
    }

    public class Merchant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MerchantTier Tier { get; set; }
        public decimal MonthlyVolume { get; set; }
    }

    public class CommissionResult
    {
        public decimal BaseCommission { get; set; }
        public decimal DiscountApplied { get; set; }
        public decimal FinalCommission { get; set; }
        public string CalculationDetails { get; set; }
    }

    // TODO: Implementar la interfaz que define el contrato para las estrategias de comisión
    public interface ICommissionStrategy
    {
        // TODO: Agregar método para calcular comisión base
        // Debe recibir: decimal amount
        // Debe retornar: decimal

        // TODO: Agregar método para obtener nombre de la estrategia
        // Debe retornar: string

        // TODO: Agregar método para obtener fórmula de cálculo
        // Debe retornar: string con formato legible
    }

    // TODO: Implementar estrategia para tarjetas de crédito: 2.8% + $0.30
    public class CreditCardCommissionStrategy : ICommissionStrategy
    {
        // TODO: Definir constantes para porcentaje (0.028M) y fee fijo (0.30M)

        // TODO: Implementar CalculateBaseCommission
        // - Aplicar fórmula: amount * PERCENTAGE + FIXED_FEE

        // TODO: Implementar GetStrategyName
        // - Retornar "Credit Card"

        // TODO: Implementar GetFormula
        // - Retornar string formateado con porcentaje y fee
    }

    // TODO: Implementar estrategia para tarjetas de débito: 1.9% + $0.25
    public class DebitCardCommissionStrategy : ICommissionStrategy
    {
        // TODO: Definir constantes para porcentaje (0.019M) y fee fijo (0.25M)

        // TODO: Implementar todos los métodos de la interfaz
        // - CalculateBaseCommission con fórmula específica
        // - GetStrategyName: "Debit Card"
        // - GetFormula con formato apropiado
    }

    // TODO: Implementar estrategia para transferencias bancarias: 1.2% + $0.15
    public class BankTransferCommissionStrategy : ICommissionStrategy
    {
        // TODO: Definir constantes para porcentaje (0.012M) y fee fijo (0.15M)

        // TODO: Implementar todos los métodos de la interfaz
        // - GetStrategyName: "Bank Transfer"
    }

    // TODO: Implementar estrategia para pagos QR: 0.8% + $0.10
    public class QRPaymentCommissionStrategy : ICommissionStrategy
    {
        // TODO: Definir constantes para porcentaje (0.008M) y fee fijo (0.10M)

        // TODO: Implementar todos los métodos de la interfaz
        // - GetStrategyName: "QR Payment"
    }

    // TODO: Implementar contexto que utiliza las estrategias para calcular comisiones
    public class CommissionCalculator
    {
        // TODO: Definir Dictionary<TransactionType, ICommissionStrategy> para mapear tipos con estrategias

        // TODO: Constructor que inicialice el Dictionary
        // - CreditCard → CreditCardCommissionStrategy
        // - DebitCard → DebitCardCommissionStrategy
        // - BankTransfer → BankTransferCommissionStrategy
        // - QRPayment → QRPaymentCommissionStrategy

        // TODO: Implementar CalculateCommission(Transaction transaction, Merchant merchant)
        // - Validar parámetros no nulos
        // - Validar que amount sea positivo
        // - Seleccionar estrategia apropiada del Dictionary
        // - Calcular comisión base usando la estrategia
        // - Aplicar descuentos según tier del comercio
        // - Generar detalles del cálculo
        // - Retornar CommissionResult completo

        // TODO: Implementar ApplyDiscounts(decimal baseCommission, Merchant merchant)
        // - Premium: 20% descuento (multiply by 0.8)
        // - HighVolume: 20% descuento base
        // - Volumen >$10,000/mes: 15% descuento adicional (multiply by 0.85)
        // - Redondear a 2 decimales

        // TODO: Implementar GenerateCalculationDetails
        // - Incluir nombre de estrategia y fórmula
        // - Mostrar descuentos aplicados
        // - Mostrar comisión final
        // - Formato legible para el usuario

        // TODO: Implementar RegisterStrategy para extensibilidad
        // - Permitir agregar nuevas estrategias dinámicamente
        // - Validar que strategy no sea nulo

        // TODO: Implementar GetAvailableStrategies
        // - Retornar Dictionary readonly de estrategias disponibles
    }

    // TODO: Implementar Factory para crear calculadores pre-configurados
    public static class CommissionCalculatorFactory
    {
        // TODO: Implementar CreateStandardCalculator
        // - Retornar nueva instancia con configuración estándar

        // TODO: Implementar CreateCustomCalculator
        // - Recibir Dictionary de estrategias personalizadas
        // - Crear calculador y registrar estrategias personalizadas
    }

    // TODO: Clase de prueba para validar la implementación
    public class CommissionCalculatorTests
    {
        public static void RunTests()
        {
            // TODO: Crear instancia de CommissionCalculator

            // TODO: Definir casos de prueba con datos reales:
            // Caso 1: Credit card, comercio estándar
            // - Amount: $100, Expected: $3.10 (100 * 0.028 + 0.30)
            // Caso 2: Debit card, comercio premium  
            // - Amount: $200, Expected: $3.36 ((200 * 0.019 + 0.25) * 0.8)
            // Caso 3: QR Payment, comercio alto volumen + descuento volumen
            // - Amount: $500, Expected: $2.89 ((500 * 0.008 + 0.10) * 0.8 * 0.85)
            // Caso 4: Bank transfer, comercio estándar + descuento volumen
            // - Amount: $1000, Expected: $10.43 ((1000 * 0.012 + 0.15) * 0.85)

            // TODO: Para cada caso de prueba:
            // - Crear Transaction y Merchant apropiados
            // - Ejecutar CalculateCommission
            // - Verificar resultado esperado (tolerancia 0.01)
            // - Mostrar detalles completos en consola
            // - Contar pruebas exitosas vs fallidas

            // TODO: Mostrar resumen de resultados
            // - Pruebas ejecutadas, exitosas, fallidas
            // - Porcentaje de éxito
            // - Impacto esperado del sistema

            // TODO: Mostrar estrategias registradas
        }

        // TODO: Implementar TestExtensibility
        // - Demostrar cómo agregar nueva estrategia (Crypto)
        // - Crear CryptoCommissionStrategy (3.5% + $0.50)
        // - Registrar en el calculador
        // - Probar funcionalidad
    }

    // TODO: Programa principal
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("🏦 Fintexa - Sistema de Comisiones Automáticas");
            Console.WriteLine("Implementación con Strategy Pattern");
            Console.WriteLine("===============================================\n");
            
            // TODO: Ejecutar pruebas principales
            
            // TODO: Demostrar extensibilidad
            
            // TODO: Mostrar resumen final del sistema implementado
            
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}

/*
EJERCICIO: SISTEMA DE COMISIONES AUTOMÁTICAS

OBJETIVO:
Implementar un sistema de cálculo de comisiones automático usando Strategy Pattern
que maneje diferentes tipos de transacciones con sus fórmulas específicas de comisión.

PATRÓN A IMPLEMENTAR:
✅ Strategy Pattern: Algoritmos intercambiables para diferentes tipos de comisión

ESTRATEGIAS REQUERIDAS:
1. CreditCard: 2.8% + $0.30
2. DebitCard: 1.9% + $0.25  
3. BankTransfer: 1.2% + $0.15
4. QRPayment: 0.8% + $0.10

DESCUENTOS POR TIER:
- Premium: 20% descuento
- HighVolume: 20% descuento base
- Volumen >$10,000/mes: 15% descuento adicional

FUNCIONALIDADES REQUERIDAS:
1. Cálculo automático según tipo de transacción
2. Aplicación de descuentos por tier de comercio
3. Generación de detalles de cálculo
4. Extensibilidad para nuevos tipos
5. Factory para configuraciones predefinidas

CASOS DE PRUEBA REQUERIDOS:
- Credit card estándar: $100 → $3.10
- Debit card premium: $200 → $3.36  
- QR payment alto volumen: $500 → $2.89
- Bank transfer con descuento volumen: $1000 → $10.43

MÉTRICAS DE ÉXITO:
- 100% precisión en cálculos
- Tiempo de cálculo <1ms
- Extensibilidad verificada
- Cobertura completa de casos edge

TIPS PARA LA IMPLEMENTACIÓN:
- Usar decimal para precisión monetaria
- Implementar validaciones robustas
- Usar Dictionary para mapeo eficiente O(1)
- Aplicar descuentos en orden correcto
- Redondear resultados a 2 decimales
- Generar mensajes informativos para usuarios
- Considerar casos de comercios con múltiples descuentos

¡Éxito automatizando el sistema de comisiones de Fintexa!
*/