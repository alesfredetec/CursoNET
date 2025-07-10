using System;
using System.Collections.Generic;

namespace TecnicasNoIf.Ejercicios
{
    /// <summary>
    /// PROBLEMA: Lógica de negocio compleja con múltiples condicionales
    /// 
    /// ISSUES IDENTIFICADOS:
    /// ❌ PROBLEMA 1: Switch/if gigante para algoritmos de pricing
    /// ❌ PROBLEMA 2: Lógica de negocio mezclada en un solo método
    /// ❌ PROBLEMA 3: Difícil testing - muchos casos en un método
    /// ❌ PROBLEMA 4: Violación Single Responsibility Principle
    /// ❌ PROBLEMA 5: No es extensible para nuevas estrategias
    /// ❌ PROBLEMA 6: Hard-coded business rules
    /// </summary>
    public class PricingEngineProblematic
    {
        /// <summary>
        /// Calcula precio con switch gigante - ANTI-PATTERN
        /// </summary>
        public decimal CalculatePrice(
            string customerType, 
            string productCategory, 
            decimal basePrice, 
            int quantity, 
            bool isWeekend,
            bool hasLoyaltyCard,
            int customerAge,
            string region)
        {
            // ❌ PROBLEMA 1: Switch gigante con lógica compleja anidada
            switch (customerType?.ToUpper())
            {
                case "PREMIUM":
                    // Lógica compleja para clientes premium
                    if (productCategory == "ELECTRONICS")
                    {
                        var discount = hasLoyaltyCard ? 0.20m : 0.15m;
                        if (quantity >= 5)
                        {
                            discount += 0.05m; // Bulk discount
                        }
                        if (isWeekend)
                        {
                            discount += 0.03m; // Weekend special
                        }
                        if (region == "VIP_ZONE")
                        {
                            discount += 0.02m; // Regional bonus
                        }
                        return basePrice * quantity * (1 - discount);
                    }
                    else if (productCategory == "CLOTHING")
                    {
                        var discount = 0.10m;
                        if (hasLoyaltyCard && customerAge > 60)
                        {
                            discount = 0.25m; // Senior + loyalty
                        }
                        if (quantity >= 3)
                        {
                            discount += 0.07m;
                        }
                        return basePrice * quantity * (1 - discount);
                    }
                    else if (productCategory == "BOOKS")
                    {
                        // Premium customers get flat 15% on books
                        return basePrice * quantity * 0.85m;
                    }
                    else
                    {
                        // Default premium discount
                        return basePrice * quantity * 0.90m;
                    }

                case "STANDARD":
                    // ❌ PROBLEMA 2: Lógica repetitiva pero ligeramente diferente
                    if (productCategory == "ELECTRONICS")
                    {
                        var discount = hasLoyaltyCard ? 0.10m : 0.05m;
                        if (quantity >= 10) // Different bulk threshold
                        {
                            discount += 0.03m;
                        }
                        if (isWeekend && hasLoyaltyCard)
                        {
                            discount += 0.02m; // Smaller weekend discount
                        }
                        return basePrice * quantity * (1 - discount);
                    }
                    else if (productCategory == "CLOTHING")
                    {
                        var discount = hasLoyaltyCard ? 0.08m : 0.03m;
                        if (customerAge > 60)
                        {
                            discount += 0.05m;
                        }
                        return basePrice * quantity * (1 - discount);
                    }
                    else if (productCategory == "BOOKS")
                    {
                        // Standard customers get loyalty discount only
                        return hasLoyaltyCard 
                            ? basePrice * quantity * 0.95m 
                            : basePrice * quantity;
                    }
                    else
                    {
                        return hasLoyaltyCard 
                            ? basePrice * quantity * 0.97m 
                            : basePrice * quantity;
                    }

                case "STUDENT":
                    // ❌ PROBLEMA 3: Lógica específica duplicada
                    if (productCategory == "BOOKS" || productCategory == "ELECTRONICS")
                    {
                        var discount = 0.15m; // Student discount
                        if (hasLoyaltyCard)
                        {
                            discount += 0.05m;
                        }
                        if (isWeekend)
                        {
                            discount += 0.03m; // Weekend study special
                        }
                        // Student bulk discount threshold is lower
                        if (quantity >= 2 && productCategory == "BOOKS")
                        {
                            discount += 0.10m;
                        }
                        return basePrice * quantity * (1 - Math.Min(discount, 0.40m)); // Cap at 40%
                    }
                    else if (productCategory == "CLOTHING")
                    {
                        return hasLoyaltyCard 
                            ? basePrice * quantity * 0.90m 
                            : basePrice * quantity * 0.95m;
                    }
                    else
                    {
                        return basePrice * quantity * 0.95m; // Flat 5% for other categories
                    }

                case "CORPORATE":
                    // ❌ PROBLEMA 4: Lógica empresarial diferente mezclada
                    if (quantity >= 50)
                    {
                        // Volume enterprise discount
                        var volumeDiscount = Math.Min(quantity / 100m * 0.05m, 0.30m);
                        var baseDiscount = productCategory switch
                        {
                            "ELECTRONICS" => 0.12m,
                            "CLOTHING" => 0.08m,
                            "BOOKS" => 0.20m,
                            _ => 0.05m
                        };
                        return basePrice * quantity * (1 - (baseDiscount + volumeDiscount));
                    }
                    else if (quantity >= 20)
                    {
                        // Medium volume discount
                        var baseDiscount = productCategory switch
                        {
                            "ELECTRONICS" => 0.08m,
                            "CLOTHING" => 0.05m,
                            "BOOKS" => 0.15m,
                            _ => 0.03m
                        };
                        return basePrice * quantity * (1 - baseDiscount);
                    }
                    else
                    {
                        // Small corporate order
                        return basePrice * quantity * 0.97m;
                    }

                case "VIP":
                    // ❌ PROBLEMA 5: Lógica VIP ultra-específica
                    var vipDiscount = productCategory switch
                    {
                        "ELECTRONICS" => 0.25m,
                        "CLOTHING" => 0.20m,
                        "BOOKS" => 0.30m,
                        _ => 0.15m
                    };

                    // VIP special conditions
                    if (hasLoyaltyCard)
                    {
                        vipDiscount += 0.05m;
                    }
                    if (isWeekend)
                    {
                        vipDiscount += 0.05m; // VIP weekend bonus
                    }
                    if (region == "VIP_ZONE")
                    {
                        vipDiscount += 0.10m; // Massive VIP zone bonus
                    }
                    if (customerAge > 65)
                    {
                        vipDiscount += 0.05m; // VIP senior discount
                    }

                    return basePrice * quantity * (1 - Math.Min(vipDiscount, 0.50m)); // Cap at 50%

                default:
                    // ❌ PROBLEMA 6: Default case sin estrategia clara
                    throw new ArgumentException($"Customer type '{customerType}' not supported");
            }
        }

        /// <summary>
        /// Obtiene descripción de descuentos - MÁS SWITCHES
        /// </summary>
        public string GetDiscountDescription(string customerType, string productCategory)
        {
            // ❌ PROBLEMA: Otro switch gigante para mantener
            switch (customerType?.ToUpper())
            {
                case "PREMIUM":
                    return productCategory switch
                    {
                        "ELECTRONICS" => "Premium electronics: 15-20% + bulk + weekend + loyalty bonuses",
                        "CLOTHING" => "Premium clothing: 10-25% based on age and loyalty",
                        "BOOKS" => "Premium books: Flat 15% discount",
                        _ => "Premium default: 10% discount"
                    };

                case "STANDARD":
                    return productCategory switch
                    {
                        "ELECTRONICS" => "Standard electronics: 5-10% + conditional bonuses",
                        "CLOTHING" => "Standard clothing: 3-8% + age-based bonuses",
                        "BOOKS" => "Standard books: 5% with loyalty card",
                        _ => "Standard default: 3% with loyalty"
                    };

                case "STUDENT":
                    return productCategory switch
                    {
                        "BOOKS" => "Student books: Up to 40% with loyalty and bulk",
                        "ELECTRONICS" => "Student electronics: 15-23% weekend special",
                        "CLOTHING" => "Student clothing: 5-10% discount",
                        _ => "Student default: 5% discount"
                    };

                case "CORPORATE":
                    return "Corporate: Volume-based 3-30% depending on quantity";

                case "VIP":
                    return $"VIP {productCategory}: 15-50% with all possible bonuses";

                default:
                    return "No discount information available";
            }
        }

        /// <summary>
        /// Valida elegibilidad - MÁS CONDICIONALES
        /// </summary>
        public bool IsEligibleForPromotion(string customerType, string productCategory, int customerAge)
        {
            // ❌ PROBLEMA: Otra lógica condicional compleja
            if (customerType == "STUDENT")
            {
                if (productCategory == "BOOKS" || productCategory == "ELECTRONICS")
                {
                    return customerAge <= 25; // Student age limit
                }
                return customerAge <= 30; // Extended for other categories
            }

            if (customerType == "PREMIUM" || customerType == "VIP")
            {
                return true; // Always eligible
            }

            if (customerType == "CORPORATE")
            {
                return productCategory != "CLOTHING"; // No corporate clothing promos
            }

            // Standard customers
            return customerAge >= 18; // Basic eligibility
        }
    }

    /*
    PROBLEMAS PRINCIPALES:

    1. COMPLEJIDAD EXTREMA:
       - CalculatePrice tiene complejidad ciclomática > 25
       - Lógica anidada de 4-5 niveles
       - Imposible de entender de un vistazo

    2. MANTENIMIENTO IMPOSIBLE:
       - Cambiar reglas de negocio = modificar switch gigante
       - Riesgo alto de breaking changes
       - Testing requiere 100+ casos

    3. VIOLACIÓN PRINCIPIOS SOLID:
       - Single Responsibility: método hace todo
       - Open/Closed: no extensible sin modificar
       - Dependency Inversion: lógica hard-coded

    4. DUPLICACIÓN DE LÓGICA:
       - Cálculos similares repetidos por tipo
       - Validaciones duplicadas
       - Rules engine distribuido

    5. EXTENSIBILIDAD NULA:
       - Agregar nuevo customer type = modificar switch
       - Nuevas reglas = tocar código existente
       - No se puede configurar dinámicamente

    6. TESTING NIGHTMARE:
       - Un método con 50+ scenarios
       - Mock difícil por dependencias internas
       - Regresiones fáciles

    OBJETIVO DEL EJERCICIO:
    Refactorizar usando Strategy Pattern para:
    ✅ Separar algoritmos de pricing por strategy
    ✅ Hacer extensible para nuevos customer types
    ✅ Simplificar testing con strategies aisladas
    ✅ Aplicar dependency injection
    ✅ Configurar rules externamente

    TIEMPO ESTIMADO: 90 minutos
    DIFICULTAD: Avanzado
    */
}