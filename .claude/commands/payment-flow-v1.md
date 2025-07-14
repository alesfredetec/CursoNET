# /payment-flow - Payment Orchestration & Flow Design Command v1.0

**Purpose**: Design, implement, and optimize payment flows, transaction orchestration, and PSP integrations for financial systems.

@include shared/universal-constants-fintexa-v2.yml#Fintexa_Legend_Extensions

## Core Philosophy
@include shared/fintexa-core-v1.yml#Fintexa_Philosophy_v1

## Command Overview
```yaml
/payment-flow [primary-flags] [universal-flags]
Purpose: "Payment orchestration | Transaction design | Flow optimization | PSP integration"
Scope: "Card payments | Wallet operations | QR payments | Bank transfers | Settlement"
Integration: "Payment specialist persona | Transaction patterns | Orchestration workflows"
```

## Primary Flags

### Flow Design & Architecture
```yaml
--design-flow: "Payment flow design | Transaction lifecycle | State management | Error handling"
--orchestration: "Payment orchestration | Multi-PSP routing | Fallback strategies | Load balancing"
--transaction-lifecycle: "Authorization | Capture | Settlement | Reconciliation | Reporting"
--state-management: "Transaction states | State transitions | Event sourcing | Audit trails"
```

### Integration & Connectivity
```yaml
--psp-integration: "PSP connectivity | API integration | Protocol implementation | Certification"
--gateway-setup: "Payment gateway configuration | Routing rules | Failover mechanisms"
--protocol-implementation: "Card network protocols | Bank APIs | QR standards | Webhook handling"
--certification-support: "Visa/Mastercard certification | Compliance testing | Documentation"
```

### Transaction Processing
```yaml
--authorization-flow: "Payment authorization | Risk assessment | Fraud detection | Response handling"
--capture-settlement: "Payment capture | Settlement processing | Batch operations | Reconciliation"
--refund-chargeback: "Refund processing | Chargeback handling | Dispute management | Recovery"
--real-time-processing: "Instant payments | Real-time settlement | Low-latency processing"
```

### Quality & Performance
```yaml
--test-scenarios: "Payment testing | Edge cases | Error scenarios | Performance testing"
--monitoring-setup: "Transaction monitoring | Performance metrics | Alert configuration"
--optimization: "Performance tuning | Latency reduction | Throughput improvement | Cost optimization"
--reconciliation: "Payment reconciliation | Match algorithms | Exception handling | Reporting"
```

## Universal Flags Integration
@include commands/shared/flag-inheritance.yml#Universal_Always

### Thinking Modes
```yaml
--think: "Standard payment analysis | Flow design | Integration planning"
--think-hard: "Complex payment orchestration | Multi-PSP scenarios | Error handling strategies"
--ultrathink: "Critical payment issues | High-volume processing | Real-time optimization"
```

### MCP Integration
```yaml
--seq: "Multi-step payment analysis | Complex flow orchestration | Transaction lifecycle management"
--c7: "Payment standards research | PSP documentation | Best practices | Protocol specifications"
--pup: "Payment flow testing | Transaction validation | Performance testing | UI automation"
```

### Persona Integration
```yaml
--persona-payment-specialist: "Default persona for payment operations"
--persona-fintech-architect: "Payment architecture design | System integration | Scalability planning"
--persona-backend: "API implementation | Performance optimization | Reliability engineering"
--persona-security: "Payment security | Fraud prevention | Compliance validation"
```

## Command Patterns

### Payment Flow Design
```bash
# Complete payment flow design
/payment-flow --design-flow --orchestration --seq --think-hard --persona-payment-specialist
/payment-flow --state-management --transaction-lifecycle --c7
/payment-flow --test-scenarios --pup --monitoring-setup
```

### PSP Integration
```bash
# PSP integration and certification
/payment-flow --psp-integration --protocol-implementation --persona-payment-specialist
/payment-flow --certification-support --c7 --documentation
/payment-flow --test-scenarios --pup --validation
```

### Performance Optimization
```bash
# Payment performance optimization
/payment-flow --optimization --real-time-processing --seq --persona-backend
/payment-flow --monitoring-setup --performance-metrics --think-hard
/payment-flow --load-testing --pup --capacity-planning
```

### Transaction Monitoring
```bash
# Comprehensive transaction monitoring
/payment-flow --monitoring-setup --reconciliation --persona-payment-specialist
/payment-flow --alert-configuration --exception-handling --seq
/payment-flow --reporting --analytics --persona-data-analyst
```

## Integration Patterns

### Payment Ecosystem Integration
@include commands/shared/fintexa-integration-patterns-v1.yml#Payment_Networks
@include shared/universal-constants-fintexa-v2.yml#Fintexa_API_Patterns

### Transaction Patterns
@include commands/shared/fintech-persona-patterns-v1.yml#Payment_Specialist_Patterns_v1

### Quality Standards
```yaml
Payment_Performance:
  Authorization_Time: "<100ms p95 | <50ms p90 | <200ms p99"
  Success_Rate: ">99.99% | Zero fund loss | Complete transaction integrity"
  Availability: "99.99% uptime | <5min recovery | 24/7 operation"
  
Transaction_Quality:
  Data_Integrity: "100% accurate | Complete audit trail | Immutable records"
  Reconciliation: "100% match rate | <1h exception resolution | Automated processing"
  Compliance: "PCI DSS compliant | Regulatory adherence | Audit ready"
```

## Specialized Workflows

### Payment Flow Categories
```yaml
Card_Payments:
  Card_Present: "POS integration | EMV processing | Contactless payments | PIN verification"
  Card_Not_Present: "Online payments | E-commerce | 3DS authentication | Fraud prevention"
  Mobile_Payments: "In-app payments | Mobile wallets | QR codes | NFC processing"
  
Digital_Payments:
  Wallet_Transfers: "Account-to-account | Balance management | Multi-currency | Real-time"
  QR_Payments: "QR generation | Code scanning | Dynamic codes | Merchant integration"
  Bank_Transfers: "ACH processing | Wire transfers | Instant payments | Cross-border"
  
Alternative_Payments:
  Cryptocurrency: "Crypto processing | Wallet integration | Exchange connectivity | Compliance"
  Buy_Now_Pay_Later: "Installment processing | Credit assessment | Payment scheduling"
  Marketplace_Payments: "Split payments | Escrow services | Payout management | Commission handling"
```

### Transaction Lifecycle Management
```yaml
Authorization_Phase:
  Pre_Authorization: "Risk assessment | Fraud screening | Limit validation | Account verification"
  Authorization_Request: "Network routing | Protocol formatting | Timeout handling | Response processing"
  Authorization_Response: "Response parsing | Status handling | Error processing | Notification"
  
Capture_Phase:
  Capture_Request: "Amount validation | Currency handling | Batch processing | Settlement preparation"
  Capture_Processing: "Network submission | Status tracking | Exception handling | Retry logic"
  Capture_Confirmation: "Settlement confirmation | Account updates | Notification | Reconciliation"
  
Settlement_Phase:
  Settlement_Processing: "Batch settlement | Net settlement | Gross settlement | Multi-currency"
  Reconciliation: "Transaction matching | Exception handling | Dispute resolution | Reporting"
  Reporting: "Settlement reports | Financial reporting | Regulatory reporting | Analytics"
```

### Error Handling & Recovery
```yaml
Error_Categories:
  Network_Errors: "Timeout | Connection failure | Protocol error | Network unavailable"
  Business_Errors: "Insufficient funds | Invalid account | Fraud detected | Limit exceeded"
  System_Errors: "Database error | Service unavailable | Configuration error | Processing failure"
  
Recovery_Strategies:
  Immediate_Retry: "Transient errors | Network timeouts | Temporary unavailability"
  Delayed_Retry: "System overload | Rate limiting | Scheduled maintenance"
  Manual_Intervention: "Complex errors | Fraud investigation | Dispute resolution"
  
Compensation_Patterns:
  Automatic_Reversal: "Failed transactions | System errors | Timeout scenarios"
  Manual_Reversal: "Fraud detected | Customer request | Merchant dispute"
  Partial_Compensation: "Split transactions | Partial failures | Pro-rated adjustments"
```

## PSP Integration Framework

### Integration Types
```yaml
Direct_Integration:
  Visa_Direct: "Real-time payments | Push payments | Cross-border | Merchant services"
  Mastercard_Send: "Person-to-person | Business payments | Cross-border | Account funding"
  Local_Networks: "Debin | Transferencias 3.0 | Pago Mis Cuentas | Link"
  
Gateway_Integration:
  Acquiring_Banks: "Transaction processing | Settlement | Merchant services | Risk management"
  Payment_Processors: "Multi-PSP | Routing optimization | Failover | Cost optimization"
  Fintech_Partners: "Specialized services | Innovation | Niche markets | Value-added services"
  
API_Integration:
  RESTful_APIs: "Modern integration | JSON messaging | HTTP protocols | OpenAPI documentation"
  SOAP_Services: "Legacy integration | XML messaging | WS-Security | WSDL specifications"
  Messaging_Queues: "Asynchronous processing | Reliable delivery | Message ordering | Error handling"
```

### Integration Patterns
```yaml
Synchronous_Patterns:
  Request_Response: "Real-time processing | Immediate response | Low latency | Simple flow"
  Circuit_Breaker: "Fault tolerance | Fallback mechanisms | Recovery strategies | Monitoring"
  Timeout_Handling: "Response timeouts | Retry logic | Escalation procedures | Error reporting"
  
Asynchronous_Patterns:
  Event_Driven: "Event publishing | Message routing | Event sourcing | Saga orchestration"
  Webhook_Notifications: "Status updates | Event notifications | Retry mechanisms | Security"
  Batch_Processing: "Bulk operations | Scheduled processing | File exchange | Reconciliation"
```

## Output Organization

### Flow Documentation
```yaml
Flow_Specifications:
  Visual_Diagrams: "Sequence diagrams | Flow charts | State machines | Architecture diagrams"
  Technical_Docs: "API specifications | Message formats | Error codes | Integration guides"
  Business_Rules: "Payment rules | Validation logic | Business constraints | Approval workflows"
  
Integration_Guides:
  PSP_Integration: "Connection setup | Authentication | Message formats | Testing procedures"
  Certification_Docs: "Compliance requirements | Test scenarios | Validation procedures | Approval process"
  Operational_Procedures: "Monitoring procedures | Incident response | Escalation procedures | Recovery plans"
```

### Monitoring & Analytics
```yaml
Real_Time_Monitoring:
  Transaction_Metrics: "TPS | Success rate | Response time | Error rate"
  System_Health: "Service availability | Resource utilization | Performance trends | Capacity"
  Business_Metrics: "Revenue | Volume | Conversion rate | Customer satisfaction"
  
Historical_Analytics:
  Trend_Analysis: "Performance trends | Volume patterns | Seasonal variations | Growth projections"
  Error_Analysis: "Error patterns | Root cause analysis | Resolution trends | Prevention strategies"
  Business_Intelligence: "Revenue analysis | Customer behavior | Market trends | Competitive analysis"
```

## Success Metrics

### Operational Excellence
```yaml
Performance_Metrics:
  Latency: "Authorization <100ms | Settlement <1h | Reconciliation <24h"
  Throughput: "10,000+ TPS | Horizontal scaling | Load balancing | Peak handling"
  Reliability: "99.99% availability | <1min recovery | Zero data loss | Complete integrity"

Quality_Metrics:
  Accuracy: "100% transaction accuracy | Zero calculation errors | Complete audit trail"
  Completeness: "100% transaction coverage | Complete reporting | Full reconciliation"
  Compliance: "PCI DSS compliant | Regulatory adherence | Audit ready | Security validated"
```

### Business Impact
```yaml
Financial_Metrics:
  Revenue_Optimization: "Cost reduction | Revenue increase | Efficiency improvement | ROI measurement"
  Risk_Management: "Fraud reduction | Loss prevention | Compliance costs | Operational risk"
  Customer_Experience: "Payment success | User satisfaction | Conversion rates | Support reduction"

Strategic_Metrics:
  Market_Expansion: "New PSP integrations | Geographic expansion | Product innovation | Partnership growth"
  Competitive_Advantage: "Time-to-market | Feature differentiation | Cost leadership | Quality excellence"
  Scalability: "Volume growth | Geographic scaling | Product scaling | Technology evolution"
```

---
*Payment Flow Command v1.0 | Transaction orchestration | PSP integration | Payment excellence*