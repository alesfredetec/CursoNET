using System;

namespace TecnicasNoIf.Ejercicios
{ 
    /// <summary>
    /// PROBLEMA: Múltiples switch/if statements para mapeos simples
    /// 
    /// ISSUES IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Switch duplicado en múltiples métodos
    /// ❌ PROBLEMA 2: Magic strings repetidos 
    /// ❌ PROBLEMA 3: Difícil mantenimiento al agregar nuevos tipos
    /// ❌ PROBLEMA 4: Violación DRY principle
    /// ❌ PROBLEMA 5: Alta complejidad ciclomática
    /// ❌ PROBLEMA 6: No es extensible sin modificar código
    /// </summary>
    public class PaymentProcessorProblematic
    {
        /// <summary>
        /// Calcula fee usando switch - difícil de mantener
        /// </summary>
        public decimal CalculateProcessingFee(string paymentType, decimal amount)
        {
            // ❌ PROBLEMA 1: Switch duplicado que se repite en otros métodos
            switch (paymentType?.ToUpper())
            {
                case "CREDIT_CARD":
                    return amount * 0.029m;
                case "DEBIT_CARD":
                    return amount * 0.015m;
                case "PAYPAL":
                    return amount * 0.035m;
                case "BANK_TRANSFER":
                    return amount * 0.008m;
                case "CRYPTO":
                    return amount * 0.025m;
                case "APPLE_PAY":
                    return amount * 0.029m;
                case "GOOGLE_PAY":
                    return amount * 0.029m;
                default:
                    throw new ArgumentException($"Payment type '{paymentType}' not supported");
            }
        }

        /// <summary>
        /// Obtiene tiempo de procesamiento - MISMO SWITCH repetido
        /// </summary>
        public int GetProcessingTimeMinutes(string paymentType)
        {
            // ❌ PROBLEMA 2: Magic strings repetidos sin constantes
            switch (paymentType?.ToUpper())
            {
                case "CREDIT_CARD":
                    return 2;
                case "DEBIT_CARD":
                    return 1;
                case "PAYPAL":
                    return 5;
                case "BANK_TRANSFER":
                    return 1440; // 24 horas
                case "CRYPTO":
                    return 60;
                case "APPLE_PAY":
                    return 1;
                case "GOOGLE_PAY":
                    return 1;
                default:
                    throw new ArgumentException($"Payment type '{paymentType}' not supported");
            }
        }

        /// <summary>
        /// Valida disponibilidad - MISMO SWITCH otra vez
        /// </summary>
        public bool IsPaymentMethodAvailable(string paymentType, string region)
        {
            // ❌ PROBLEMA 3: Para agregar nuevo método = modificar TODOS los switches
            switch (paymentType?.ToUpper())
            {
                case "CREDIT_CARD":
                    return true; // Disponible globalmente
                case "DEBIT_CARD":
                    return true;
                case "PAYPAL":
                    return region != "CHINA"; // Bloqueado en China
                case "BANK_TRANSFER":
                    return region == "EUROPE" || region == "USA";
                case "CRYPTO":
                    return region != "CHINA" && region != "INDIA";
                case "APPLE_PAY":
                    return region == "USA" || region == "EUROPE";
                case "GOOGLE_PAY":
                    return region != "CHINA";
                default:
                    return false;
            }
        }

        /// <summary>
        /// Obtiene límites máximos - CUARTO SWITCH igual
        /// </summary>
        public decimal GetMaxTransactionLimit(string paymentType)
        {
            // ❌ PROBLEMA 4: Violación DRY - misma lógica repetida
            switch (paymentType?.ToUpper())
            {
                case "CREDIT_CARD":
                    return 50000m;
                case "DEBIT_CARD":
                    return 5000m;
                case "PAYPAL":
                    return 10000m;
                case "BANK_TRANSFER":
                    return 100000m;
                case "CRYPTO":
                    return 25000m;
                case "APPLE_PAY":
                    return 3000m;
                case "GOOGLE_PAY":
                    return 3000m;
                default:
                    throw new ArgumentException($"Payment type '{paymentType}' not supported");
            }
        }

        /// <summary>
        /// Procesa pago con if/else anidados complejos
        /// </summary>
        public string ProcessPayment(string paymentType, decimal amount, string region)
        {
            // ❌ PROBLEMA 5: Alta complejidad ciclomática
            if (paymentType == null)
            {
                return "ERROR: Payment type is required";
            }

            if (amount <= 0)
            {
                return "ERROR: Amount must be positive";
            }

            // ❌ PROBLEMA 6: No es extensible - para nuevo método hay que tocar aquí
            if (paymentType.ToUpper() == "CREDIT_CARD")
            {
                if (amount > 50000)
                {
                    return "ERROR: Amount exceeds credit card limit";
                }
                if (!IsPaymentMethodAvailable(paymentType, region))
                {
                    return "ERROR: Credit cards not available in this region";
                }
                return $"Processing ${amount} via Credit Card (Fee: ${CalculateProcessingFee(paymentType, amount):F2})";
            }
            else if (paymentType.ToUpper() == "PAYPAL")
            {
                if (amount > 10000)
                {
                    return "ERROR: Amount exceeds PayPal limit";
                }
                if (region == "CHINA")
                {
                    return "ERROR: PayPal not available in China";
                }
                return $"Processing ${amount} via PayPal (Fee: ${CalculateProcessingFee(paymentType, amount):F2})";
            }
            else if (paymentType.ToUpper() == "BANK_TRANSFER")
            {
                if (region != "EUROPE" && region != "USA")
                {
                    return "ERROR: Bank transfers only available in Europe and USA";
                }
                if (amount > 100000)
                {
                    return "ERROR: Amount exceeds bank transfer limit";
                }
                return $"Processing ${amount} via Bank Transfer (Fee: ${CalculateProcessingFee(paymentType, amount):F2})";
            }
            // ... más condiciones similares para otros métodos

            return "ERROR: Unsupported payment method";
        }

        /// <summary>
        /// Obtiene descripción del método - MÁS SWITCHES
        /// </summary>
        public string GetPaymentMethodDescription(string paymentType)
        {
            // ❌ PROBLEMA: Otro switch más que mantener sincronizado
            switch (paymentType?.ToUpper())
            {
                case "CREDIT_CARD":
                    return "Secure credit card processing with instant authorization";
                case "DEBIT_CARD":
                    return "Direct debit from bank account with real-time verification";
                case "PAYPAL":
                    return "PayPal secure digital wallet service";
                case "BANK_TRANSFER":
                    return "Direct bank-to-bank wire transfer";
                case "CRYPTO":
                    return "Cryptocurrency payment with blockchain verification";
                case "APPLE_PAY":
                    return "Apple's contactless payment system";
                case "GOOGLE_PAY":
                    return "Google's mobile payment platform";
                default:
                    return "Unknown payment method";
            }
        }
    }

    /*
    PROBLEMAS PRINCIPALES:

    1. CÓDIGO DUPLICADO:
       - 6 métodos con switches similares
       - Magic strings repetidos sin constantes
       - Misma lógica de validación copiada

    2. MANTENIMIENTO COSTOSO:
       - Agregar nuevo método de pago = modificar 6+ lugares
       - Cambiar fee de un método = buscar en múltiples switches
       - Riesgo alto de inconsistencias

    3. COMPLEJIDAD ALTA:
       - ProcessPayment tiene complejidad ciclomática > 10
       - Lógica anidada difícil de seguir
       - Testing requiere muchos casos

    4. EXTENSIBILIDAD LIMITADA:
       - No se puede agregar métodos sin modificar código base
       - Violación Open/Closed Principle
       - No permite configuración dinámica

    5. TESTING COMPLEJO:
       - Cada switch necesita todos los casos
       - Difícil mockear comportamientos
       - Cobertura requiere muchos unit tests

    OBJETIVO DEL EJERCICIO:
    Refactorizar usando Dictionary Pattern para:
    ✅ Eliminar switches duplicados
    ✅ Centralizar configuración
    ✅ Facilitar extensión
    ✅ Reducir complejidad
    ✅ Mejorar testability

    TIEMPO ESTIMADO: 60 minutos
    DIFICULTAD: Intermedio-Avanzado
    */
}