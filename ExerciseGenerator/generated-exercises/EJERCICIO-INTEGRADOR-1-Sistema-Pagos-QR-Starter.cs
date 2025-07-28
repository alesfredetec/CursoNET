using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fintexa.QRPayments
{
    // ===== CONTEXTO DEL EJERCICIO =====
    // Fintexa necesita expandir su sistema de pagos QR para soportar:
    // 1. QR Estático (monto fijo, reutilizable)
    // 2. QR Dinámico (monto variable)
    // 3. QR Recurrente (suscripciones)
    //
    // OBJETIVO: Implementar un procesador extensible que maneje diferentes tipos de QR
    // sin usar múltiples condicionales anidadas (aplicar técnicas NoIf)

    // ===== PUNTO DE PARTIDA - CÓDIGO ACTUAL (PROBLEMÁTICO) =====
    public class LegacyQRProcessor
    {
        // PROBLEMA: Este código actual solo maneja QR estáticos
        // y tendría que modificarse constantemente para nuevos tipos
        public string ProcessPayment(string qrCode, decimal amount)
        {
            // Código problemático que necesita refactorización
            if (qrCode.StartsWith("STATIC"))
            {
                if (amount <= 0)
                    return "Error: Monto inválido";
                
                if (qrCode.Length < 20)
                    return "Error: QR estático inválido";
                
                return "Pago estático procesado";
            }
            
            return "Tipo de QR no soportado";
        }
    }

    // ===== ÁREA DE TRABAJO - IMPLEMENTE SU SOLUCIÓN AQUÍ =====
    
    // TODO: FASE 1 - ANÁLISIS (15 min)
    // Analice los requisitos y complete la matriz de requisitos funcionales/no funcionales
    
    /* MATRIZ DE REQUISITOS - COMPLETAR
    
    FUNCIONALES:
    - [ ] RF001: Procesar QR estáticos con monto fijo
    - [ ] RF002: _______________________________ (complete)
    - [ ] RF003: _______________________________ (complete)
    
    NO FUNCIONALES:
    - [ ] RNF001: Tiempo respuesta < 3 segundos
    - [ ] RNF002: ______________________________ (complete)
    - [ ] RNF003: ______________________________ (complete)
    */

    // TODO: FASE 2 - DISEÑO (20 min)
    // Diseñe la arquitectura evitando condicionales complejas
    // Aplique principios de extensibilidad y mantenibilidad

    public enum QRType
    {
        // TODO: Defina los tipos de QR requeridos
    }

    public class QRPaymentRequest
    {
        // TODO: Propiedades necesarias para procesar un pago QR
        // Considere: código QR, monto, tipo, metadatos, etc.
    }

    public class QRPaymentResponse
    {
        // TODO: Estructura de respuesta del procesamiento
        // Considere: resultado, mensaje, referencia transacción, etc.
    }

    // TODO: Implemente el patrón que evite múltiples if/switch
    // Sugerencia: Piense en cómo manejar diferentes comportamientos sin condicionales
    
    public class QRPaymentProcessor
    {
        // TODO: FASE 3 - IMPLEMENTACIÓN (20 min)
        // Implemente el procesador principal
        
        public QRPaymentResponse ProcessPayment(QRPaymentRequest request)
        {
            // TODO: Implementar lógica principal sin condicionales anidadas
            throw new NotImplementedException("Implementar procesamiento QR");
        }

        // TODO: Métodos de validación específicos
        private bool ValidateQRFormat(string qrCode, QRType type)
        {
            throw new NotImplementedException("Implementar validación formato");
        }

        private bool ValidateFunds(decimal amount)
        {
            throw new NotImplementedException("Implementar validación fondos");
        }

        private bool ValidateMerchantLimits(string merchantId, decimal amount)
        {
            throw new NotImplementedException("Implementar validación límites");
        }
    }

    // TODO: Clases para manejo específico de cada tipo de QR
    // Evite duplicación de código, reutilice comportamientos comunes

    public class StaticQRHandler
    {
        // QR Estático: monto fijo, reutilizable
        // TODO: Implementar lógica específica
    }

    public class DynamicQRHandler
    {
        // QR Dinámico: monto variable definido por comercio
        // TODO: Implementar lógica específica
    }

    public class RecurringQRHandler
    {
        // QR Recurrente: para suscripciones
        // TODO: Implementar lógica específica
    }

    // TODO: INTEGRACIÓN CON SISTEMAS EXTERNOS
    public class PaymentGatewayIntegration
    {
        // Simulación de integración con Visa/Coelsa
        public bool ProcessWithVisa(QRPaymentRequest request)
        {
            // TODO: Simular integración
            return true;
        }

        public bool ProcessWithCoelsa(QRPaymentRequest request)
        {
            // TODO: Simular integración
            return true;
        }
    }

    public class AuditService
    {
        // Registro para compliance PCI DSS
        public void LogTransaction(QRPaymentRequest request, QRPaymentResponse response)
        {
            // TODO: Implementar logging de auditoría
        }
    }

    // ===== ÁREA DE TESTING - CASOS DE PRUEBA =====
    public class QRPaymentTests
    {
        // TODO: FASE 4 - VALIDACIÓN (5 min)
        // Implemente casos de prueba básicos
        
        public void TestStaticQRPayment()
        {
            // TODO: Probar pago QR estático
        }

        public void TestDynamicQRPayment()
        {
            // TODO: Probar pago QR dinámico
        }

        public void TestRecurringQRPayment()
        {
            // TODO: Probar pago QR recurrente
        }

        public void TestInvalidQRFormat()
        {
            // TODO: Probar manejo de errores
        }

        public void TestPerformanceRequirement()
        {
            // TODO: Verificar tiempo de respuesta < 3 segundos
        }
    }

    // ===== CHECKLIST DE ENTREGA =====
    /*
    COMPLETAR ANTES DE ENTREGAR:
    
    □ ANÁLISIS COMPLETADO
      □ Matriz de requisitos funcionales identificados
      □ Requisitos no funcionales documentados
      □ Riesgos y dependencias analizados
    
    □ DISEÑO COMPLETADO  
      □ Arquitectura sin condicionales complejas
      □ Interfaces y contratos definidos
      □ Modelo de datos estructurado
      □ Estrategia de extensibilidad clara
    
    □ IMPLEMENTACIÓN COMPLETADA
      □ Procesador QR funcional
      □ Validaciones por tipo implementadas
      □ Integración con servicios externos
      □ Manejo de errores apropiado
    
    □ VALIDACIÓN COMPLETADA
      □ Casos de prueba ejecutados
      □ Cumplimiento de requisitos verificado
      □ Métricas de calidad evaluadas
    
    □ CRITERIOS DE CALIDAD
      □ Código limpio y mantenible
      □ Sin duplicación innecesaria
      □ Principios SOLID aplicados
      □ Comentarios donde sea necesario
    */
}

// ===== RECURSOS ADICIONALES =====
/*
DOCUMENTACIÓN DE REFERENCIA:
- Estándares ISO/IEC 18004 para códigos QR
- PCI DSS Compliance Guidelines
- Fintexa API Documentation (simulated)

MÉTRICAS DE ÉXITO:
- Tiempo respuesta < 3 segundos
- Soporte para 1,000 transacciones concurrentes
- Código con complejidad ciclomática < 10
- Cobertura de pruebas > 80%
*/