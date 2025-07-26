using System;
using System.Collections.Generic;

namespace PaymentSystem
{
    // Código problemático que necesita refactorización usando Strategy + Factory
    public class PaymentProcessor
    {
        public void ProcessPayment(string paymentMethod, decimal amount, Dictionary<string, string> paymentData)
        {
            // Código monolítico con múltiples responsabilidades y violación de principios SOLID
            if (paymentMethod == "CreditCard")
            {
                // Validación hardcodeada para tarjeta de crédito
                if (!paymentData.ContainsKey("CardNumber") || 
                    !paymentData.ContainsKey("ExpiryDate") || 
                    !paymentData.ContainsKey("CVV"))
                {
                    throw new Exception("Missing credit card information");
                }

                var cardNumber = paymentData["CardNumber"];
                if (cardNumber.Length != 16)
                {
                    throw new Exception("Invalid card number");
                }

                // Lógica de procesamiento de tarjeta
                Console.WriteLine($"Processing ${amount} via Credit Card ending in {cardNumber.Substring(12)}");
                
                // Simulación de llamada a API externa
                if (amount > 10000)
                {
                    throw new Exception("Amount exceeds credit card limit");
                }
                
                Console.WriteLine("Credit card payment successful");
            }
            else if (paymentMethod == "PayPal")
            {
                // Validación hardcodeada para PayPal
                if (!paymentData.ContainsKey("Email") || !paymentData.ContainsKey("Password"))
                {
                    throw new Exception("Missing PayPal credentials");
                }

                var email = paymentData["Email"];
                if (!email.Contains("@"))
                {
                    throw new Exception("Invalid PayPal email");
                }

                // Lógica de procesamiento PayPal
                Console.WriteLine($"Processing ${amount} via PayPal for {email}");
                
                if (amount < 1)
                {
                    throw new Exception("PayPal minimum amount is $1");
                }
                
                Console.WriteLine("PayPal payment successful");
            }
            else if (paymentMethod == "BankTransfer")
            {
                // Validación hardcodeada para transferencia bancaria
                if (!paymentData.ContainsKey("AccountNumber") || 
                    !paymentData.ContainsKey("RoutingNumber"))
                {
                    throw new Exception("Missing bank transfer information");
                }

                var accountNumber = paymentData["AccountNumber"];
                var routingNumber = paymentData["RoutingNumber"];

                // Lógica de procesamiento bancario
                Console.WriteLine($"Processing ${amount} via Bank Transfer to account {accountNumber}");
                
                if (amount > 50000)
                {
                    throw new Exception("Bank transfer limit exceeded");
                }
                
                Console.WriteLine("Bank transfer initiated successfully");
            }
            else
            {
                throw new Exception($"Unsupported payment method: {paymentMethod}");
            }
        }

        // Método adicional que también viola principios
        public string GetPaymentStatus(string paymentMethod, string transactionId)
        {
            if (paymentMethod == "CreditCard")
            {
                // Lógica específica para verificar estado de tarjeta
                return "Completed";
            }
            else if (paymentMethod == "PayPal")
            {
                // Lógica específica para verificar estado de PayPal
                return "Pending";
            }
            else if (paymentMethod == "BankTransfer")
            {
                // Lógica específica para verificar estado bancario
                return "Processing";
            }
            
            return "Unknown";
        }
    }

    // Clase principal que demuestra el uso problemático
    public class Program
    {
        public static void Main(string[] args)
        {
            var processor = new PaymentProcessor();
            
            try
            {
                // Ejemplo de uso con tarjeta de crédito
                var creditCardData = new Dictionary<string, string>
                {
                    {"CardNumber", "1234567890123456"},
                    {"ExpiryDate", "12/25"},
                    {"CVV", "123"}
                };
                processor.ProcessPayment("CreditCard", 150.00m, creditCardData);

                // Ejemplo de uso con PayPal
                var paypalData = new Dictionary<string, string>
                {
                    {"Email", "user@example.com"},
                    {"Password", "secretpassword"}
                };
                processor.ProcessPayment("PayPal", 75.50m, paypalData);
                
                // Ejemplo de uso con transferencia bancaria
                var bankData = new Dictionary<string, string>
                {
                    {"AccountNumber", "12345678"},
                    {"RoutingNumber", "987654321"}
                };
                processor.ProcessPayment("BankTransfer", 1000.00m, bankData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Payment failed: {ex.Message}");
            }
        }
    }
}

/*
PROBLEMAS IDENTIFICADOS EN ESTE CÓDIGO:
1. Violación del principio Single Responsibility - Una clase hace todo
2. Violación del principio Open/Closed - Agregar métodos requiere modificar la clase
3. Código duplicado en validaciones y procesamiento
4. Hardcoding de lógica de negocio
5. Falta de abstracciones apropiadas
6. Manejo de errores inconsistente
7. Dificultad para testing unitario
8. Acoplamiento alto entre componentes
9. Falta de logging y configuración
10. No es extensible para nuevos métodos de pago
*/