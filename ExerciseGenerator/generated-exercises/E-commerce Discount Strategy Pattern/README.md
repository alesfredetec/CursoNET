# E-commerce Discount Strategy Pattern

**Skill Level:** Advanced
**Topic Area:** DesignPatterns
**Exercise Type:** Implementation
**Estimated Time:** 45 minutes

## Description
Context: Enterprise payment system

Implement the Strategy Pattern to handle different discount calculation strategies in an e-commerce system.

This exercise demonstrates how the Strategy Pattern eliminates complex conditional logic and makes the system easily extensible for new discount types.

Real-world application: E-commerce platforms need flexible discount systems that can handle seasonal sales, member discounts, bulk purchase discounts, and promotional codes without modifying existing code.

## Learning Objectives
- Implement the Strategy Pattern to eliminate conditional logic
- Create a flexible, extensible discount calculation system
- Understand how Strategy Pattern follows Open/Closed Principle
- Practice dependency injection and inversion of control
- Design maintainable object-oriented solutions

## Prerequisites
- Understanding of interfaces and abstract classes
- Knowledge of polymorphism and inheritance
- Familiarity with SOLID principles (especially Open/Closed)
- Basic understanding of dependency injection concepts

## Problem Statement
You're building an e-commerce system that needs to calculate discounts for different customer types and scenarios:

1. **Regular Customer**: No discount (0%)
2. **Premium Member**: 10% discount on all purchases
3. **VIP Member**: 15% discount + free shipping
4. **Seasonal Sales**: 20% discount during special periods
5. **Bulk Purchase**: 5% discount for orders over $100, 10% for orders over $500

Currently, the system uses a large switch statement that becomes harder to maintain as new discount types are added. Your task is to refactor this using the Strategy Pattern.

**Requirements:**
- Each discount strategy should be a separate class
- The system should be easily extensible for new discount types
- Discount calculation should be consistent and testable
- The client code should be clean and simple

## Technical Requirements
- Implement IDiscountStrategy interface
- Create concrete strategy classes for each discount type
- Implement DiscountContext class to manage strategies
- Use dependency injection to set strategies
- Include proper error handling and validation
- Follow SOLID principles, especially Open/Closed

## Success Criteria
- All discount strategies implement the same interface
- Context class properly delegates to strategies
- New discount types can be added without modifying existing code
- All calculations are accurate and tested
- Client code is clean and doesn't contain business logic
- Code demonstrates proper object-oriented design

## Extension Challenges
- Add combination discounts (e.g., member + seasonal)
- Implement discount validation rules and business constraints
- Add logging and audit trail for discount applications
- Create a discount factory to automatically select appropriate strategies
- Implement time-based discount activation/deactivation
- Add discount priority system for overlapping discounts

## Common Pitfalls & Tips
- Don't create strategies with state - keep them stateless for reusability
- Avoid tight coupling between context and specific strategy implementations
- Remember to validate input parameters in both context and strategies
- Consider edge cases like negative amounts or null values
- Don't forget to test boundary conditions (exactly $100, $500, etc.)
- Consider performance implications of your design choices
- Think about scalability and extensibility
- Consider architectural patterns and their trade-offs

## Files
- `E-commerce Discount Strategy Pattern-Starter.cs` - Your starting point
- `E-commerce Discount Strategy Pattern-Solution.cs` - Complete solution (review after attempting)
- `E-commerce Discount Strategy Pattern-Tests.cs` - Unit tests to validate your solution
