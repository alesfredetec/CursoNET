# /fraud-analysis - Fraud Detection & Risk Analysis Command v1.0

**Purpose**: Comprehensive fraud detection, risk analysis, and security monitoring for financial transactions and payment systems.

@include shared/universal-constants-fintexa-v2.yml#Fintexa_Legend_Extensions

## Core Philosophy
@include shared/fintexa-core-v1.yml#Fintexa_Philosophy_v1

## Command Overview
```yaml
/fraud-analysis [primary-flags] [universal-flags]
Purpose: "Fraud detection | Risk assessment | Security monitoring | Pattern analysis"
Scope: "Transaction fraud | Account security | Payment risk | Behavioral analysis"
Integration: "Security personas | Risk patterns | ML models | Real-time monitoring"
```

## Primary Flags

### Fraud Detection & Prevention
```yaml
--pattern-detection: "Fraud pattern analysis | Anomaly detection | Behavioral analysis | Trend identification"
--real-time-scoring: "Transaction scoring | Risk assessment | Decision engine | Real-time blocking"
--rule-engine: "Business rules | Risk rules | Fraud rules | Decision trees | Rule optimization"
--ml-models: "Machine learning | Predictive modeling | Feature engineering | Model training"
```

### Risk Assessment & Management
```yaml
--risk-assessment: "Risk evaluation | Threat analysis | Vulnerability assessment | Impact analysis"
--risk-scoring: "Customer risk scoring | Transaction risk | Merchant risk | Geographic risk"
--risk-monitoring: "Continuous monitoring | Risk dashboard | Alert management | Escalation"
--risk-mitigation: "Risk controls | Mitigation strategies | Action plans | Prevention measures"
```

### Investigation & Analysis
```yaml
--fraud-investigation: "Case investigation | Evidence collection | Pattern analysis | Root cause"
--case-management: "Case tracking | Investigation workflow | Documentation | Resolution"
--forensic-analysis: "Digital forensics | Transaction analysis | Account analysis | Evidence preservation"
--incident-response: "Fraud incidents | Response procedures | Containment | Recovery"
```

### Monitoring & Alerting
```yaml
--real-time-monitoring: "Transaction monitoring | Account monitoring | Behavioral monitoring"
--alert-management: "Alert generation | Alert tuning | False positive reduction | Prioritization"
--dashboard-setup: "Risk dashboards | KPI monitoring | Executive reporting | Operational views"
--reporting: "Fraud reporting | Risk reporting | Regulatory reporting | Management reporting"
```

## Universal Flags Integration
@include commands/shared/flag-inheritance.yml#Universal_Always

### Thinking Modes
```yaml
--think: "Standard fraud analysis | Risk assessment | Pattern recognition"
--think-hard: "Complex fraud schemes | Advanced analytics | Multi-vector analysis"
--ultrathink: "Sophisticated fraud investigation | AI-driven analysis | Predictive modeling"
```

### MCP Integration
```yaml
--seq: "Multi-step fraud analysis | Complex investigation workflows | Risk assessment chains"
--c7: "Fraud prevention research | Security standards | Best practices | Industry intelligence"
--pup: "Fraud testing | Security validation | User behavior simulation | Attack simulation"
```

### Persona Integration
```yaml
--persona-security: "Default persona for fraud analysis operations"
--persona-data-analyst: "Data analysis | Pattern recognition | Statistical modeling | Reporting"
--persona-analyzer: "Investigation | Root cause analysis | Evidence-based reasoning"
--persona-compliance-security: "Regulatory compliance | Audit requirements | Control validation"
```

## Command Patterns

### Fraud Detection Setup
```bash
# Comprehensive fraud detection implementation
/fraud-analysis --pattern-detection --real-time-scoring --seq --think-hard --persona-security
/fraud-analysis --ml-models --rule-engine --c7 --persona-data-analyst
/fraud-analysis --real-time-monitoring --alert-management --dashboard-setup
```

### Risk Assessment
```bash
# Complete risk assessment workflow
/fraud-analysis --risk-assessment --risk-scoring --seq --persona-security
/fraud-analysis --risk-monitoring --risk-mitigation --think-hard
/fraud-analysis --reporting --dashboard-setup --persona-data-analyst
```

### Fraud Investigation
```bash
# Fraud investigation and analysis
/fraud-analysis --fraud-investigation --forensic-analysis --seq --persona-analyzer
/fraud-analysis --case-management --evidence-collection --think-hard
/fraud-analysis --incident-response --documentation --persona-compliance-security
```

### Model Optimization
```bash
# ML model optimization and tuning
/fraud-analysis --ml-models --pattern-detection --seq --persona-data-analyst
/fraud-analysis --alert-tuning --false-positive-reduction --think-hard
/fraud-analysis --performance-optimization --validation --pup
```

## Integration Patterns

### Security Framework Integration
@include commands/shared/fintech-persona-patterns-v1.yml#Compliance_Security_Patterns_v1
@include shared/universal-constants-fintexa-v2.yml#Security_Implementation

### Risk Management Patterns
```yaml
Risk_Categories:
  Transaction_Risk: "Amount anomalies | Frequency patterns | Geographic inconsistencies | Merchant risk"
  Account_Risk: "Account takeover | Identity theft | Credential compromise | Profile changes"
  Payment_Risk: "Card fraud | Digital wallet fraud | Bank transfer fraud | Cryptocurrency fraud"
  Behavioral_Risk: "Usage patterns | Device fingerprinting | Biometric analysis | Session analysis"
  
Risk_Factors:
  Customer_Factors: "Credit history | Account age | Transaction history | Geographic location"
  Transaction_Factors: "Amount | Frequency | Time | Location | Merchant | Payment method"
  Device_Factors: "Device fingerprint | IP address | Geolocation | Browser characteristics"
  Network_Factors: "Network reputation | Proxy usage | VPN detection | Bot detection"
```

### Quality Standards
```yaml
Fraud_Detection_Performance:
  Detection_Rate: ">99.5% fraud detection | <1% false positive rate | Real-time processing"
  Response_Time: "<100ms scoring | <1s investigation | <5min containment"
  Accuracy: ">98% model accuracy | Continuous improvement | Regular validation"
  
Risk_Management_Effectiveness:
  Risk_Reduction: ">95% risk mitigation | Proactive prevention | Continuous monitoring"
  Cost_Efficiency: "Cost-effective controls | ROI optimization | Resource efficiency"
  Compliance: "Regulatory adherence | Audit compliance | Control effectiveness"
```

## Specialized Workflows

### Fraud Detection Models
```yaml
Machine_Learning_Models:
  Supervised_Learning:
    Classification: "Fraud vs legitimate | Decision trees | Random forest | Neural networks"
    Regression: "Risk scoring | Probability estimation | Continuous scoring | Trend analysis"
    Ensemble_Methods: "Model combination | Voting classifiers | Stacking | Boosting"
  
  Unsupervised_Learning:
    Clustering: "Customer segmentation | Behavior clustering | Anomaly clusters | Risk groups"
    Anomaly_Detection: "Outlier detection | Isolation forest | One-class SVM | Autoencoders"
    Association_Rules: "Pattern mining | Rule discovery | Correlation analysis | Market basket"
  
  Deep_Learning:
    Neural_Networks: "Deep learning | Feature learning | Pattern recognition | Complex relationships"
    Recurrent_Networks: "Sequential patterns | Time series | Behavior sequences | Temporal analysis"
    Autoencoders: "Anomaly detection | Feature extraction | Dimensionality reduction | Reconstruction"
```

### Real-Time Processing
```yaml
Stream_Processing:
  Event_Streaming: "Real-time events | Kafka streaming | Event sourcing | Stream analytics"
  Complex_Event_Processing: "Pattern matching | Event correlation | Temporal patterns | Rule processing"
  Real_Time_Analytics: "Live dashboards | Streaming analytics | Real-time KPIs | Immediate insights"
  
Scoring_Engine:
  Real_Time_Scoring: "Transaction scoring | Account scoring | Behavioral scoring | Risk assessment"
  Decision_Engine: "Automated decisions | Rule execution | ML inference | Action triggering"
  Response_Engine: "Real-time responses | Blocking | Alerting | Escalation | Notification"
```

### Investigation Workflows
```yaml
Investigation_Process:
  Case_Creation: "Alert triage | Case creation | Priority assignment | Resource allocation"
  Evidence_Collection: "Transaction logs | Account data | Device information | Communication records"
  Analysis_Phase: "Pattern analysis | Timeline reconstruction | Root cause analysis | Impact assessment"
  Resolution_Phase: "Decision making | Action execution | Documentation | Case closure"
  
Investigation_Tools:
  Data_Analysis: "SQL queries | Statistical analysis | Data visualization | Pattern recognition"
  Network_Analysis: "Connection analysis | Graph analysis | Relationship mapping | Social networks"
  Timeline_Analysis: "Event sequencing | Temporal patterns | Chronological analysis | Correlation"
  Visualization: "Network graphs | Timeline charts | Geographic maps | Pattern visualizations"
```

## Fraud Categories & Patterns

### Transaction Fraud
```yaml
Card_Fraud:
  Card_Not_Present: "Online fraud | E-commerce fraud | Subscription fraud | Account takeover"
  Card_Present: "Counterfeit cards | Lost/stolen cards | Skimming | Shimming"
  Mobile_Payments: "Mobile app fraud | Digital wallet fraud | QR code fraud | NFC fraud"
  
Payment_Fraud:
  Bank_Transfer: "Authorized push payment | Business email compromise | Invoice fraud"
  Digital_Wallets: "Account takeover | P2P fraud | Balance manipulation | Token fraud"
  Cryptocurrency: "Exchange fraud | Wallet fraud | Mining fraud | ICO fraud"
```

### Account Fraud
```yaml
Identity_Fraud:
  Account_Opening: "Synthetic identity | Stolen identity | Document fraud | Bust-out schemes"
  Account_Takeover: "Credential stuffing | Phishing | Social engineering | SIM swapping"
  Profile_Manipulation: "Address changes | Contact changes | Authentication bypass"
  
Behavioral_Fraud:
  Usage_Anomalies: "Unusual transactions | Geographic inconsistencies | Time anomalies"
  Device_Anomalies: "New devices | Suspicious locations | VPN usage | Bot activity"
  Pattern_Changes: "Spending patterns | Login patterns | Communication patterns"
```

### Merchant Fraud
```yaml
Merchant_Risk:
  High_Risk_Merchants: "Adult content | Gambling | Cryptocurrency | High chargeback rates"
  Merchant_Fraud: "Transaction laundering | Friendly fraud | Chargeback abuse | Factoring"
  Processing_Fraud: "Card testing | Velocity attacks | Refund abuse | Loyalty fraud"
```

## Risk Scoring Framework

### Scoring Models
```yaml
Customer_Risk_Score:
  Demographics: "Age | Geographic location | Income level | Employment status"
  History: "Account age | Transaction history | Payment history | Dispute history"
  Behavior: "Usage patterns | Device patterns | Communication patterns | Social signals"
  
Transaction_Risk_Score:
  Amount_Analysis: "Transaction amount | Amount patterns | Velocity analysis | Threshold analysis"
  Merchant_Analysis: "Merchant risk | Industry risk | Geographic risk | Reputation analysis"
  Context_Analysis: "Time patterns | Location patterns | Device analysis | Channel analysis"
  
Real_Time_Factors:
  Velocity_Checks: "Transaction frequency | Amount velocity | Geographic velocity | Merchant velocity"
  Anomaly_Detection: "Statistical anomalies | Behavioral anomalies | Pattern deviations"
  Network_Analysis: "Device networks | IP networks | Identity networks | Relationship analysis"
```

### Decision Framework
```yaml
Risk_Thresholds:
  Low_Risk: "Score 0-30 | Automatic approval | Standard monitoring | Low friction"
  Medium_Risk: "Score 31-70 | Additional verification | Enhanced monitoring | Risk controls"
  High_Risk: "Score 71-100 | Manual review | Investigation required | Transaction blocking"
  
Action_Matrix:
  Approve: "Low risk | Known customer | Standard transaction | Verified identity"
  Challenge: "Medium risk | New pattern | Additional verification | Step-up authentication"
  Block: "High risk | Suspected fraud | Policy violation | Security threat"
  Review: "Complex case | Manual investigation | Expert analysis | Evidence review"
```

## Output Organization

### Dashboards & Reporting
```yaml
Real_Time_Dashboards:
  Operations_Dashboard: "Live alerts | Transaction monitoring | System health | Performance metrics"
  Risk_Dashboard: "Risk scores | Fraud rates | Model performance | Alert statistics"
  Investigation_Dashboard: "Case status | Investigation queue | Resource utilization | Resolution metrics"
  
Management_Reporting:
  Executive_Reports: "Fraud losses | Risk trends | Model performance | Strategic initiatives"
  Operational_Reports: "Daily operations | Alert statistics | Investigation results | System performance"
  Regulatory_Reports: "Compliance metrics | Audit reports | Control effectiveness | Risk assessments"
```

### Investigation Documentation
```yaml
Case_Documentation:
  Case_Summary: "Case overview | Key findings | Evidence summary | Recommendations"
  Investigation_Timeline: "Event sequence | Analysis steps | Decision points | Actions taken"
  Evidence_Catalog: "Digital evidence | Transaction records | Communication logs | External data"
  
Resolution_Documentation:
  Decision_Rationale: "Analysis results | Risk assessment | Decision factors | Approval workflow"
  Action_Records: "Actions taken | System changes | Account adjustments | Communication sent"
  Lessons_Learned: "Investigation insights | Process improvements | Model updates | Training needs"
```

## Success Metrics

### Fraud Prevention Effectiveness
```yaml
Detection_Metrics:
  True_Positive_Rate: ">99.5% fraud detection | Minimal false negatives | Complete coverage"
  False_Positive_Rate: "<1% false positives | Customer experience | Operational efficiency"
  Precision_Recall: "High precision | High recall | Balanced performance | Continuous optimization"

Financial_Impact:
  Fraud_Losses: "<0.05% transaction volume | Trending downward | Below industry average"
  Prevention_Savings: "ROI >10:1 | Cost avoidance | Revenue protection | Brand protection"
  Operational_Costs: "Cost-effective operations | Resource optimization | Automation benefits"
```

### Operational Excellence
```yaml
Performance_Metrics:
  Response_Time: "<100ms real-time scoring | <1s investigation | <5min containment"
  System_Availability: "99.99% uptime | <1min recovery | 24/7 operation | Peak handling"
  Scalability: "Linear scaling | Volume handling | Performance maintenance | Cost efficiency"

Quality_Metrics:
  Investigation_Quality: ">95% investigation accuracy | Complete documentation | Audit compliance"
  Model_Performance: ">98% model accuracy | Regular validation | Continuous improvement"
  Customer_Impact: "Minimal friction | Positive experience | Reduced complaints | Trust maintenance"
```

---
*Fraud Analysis Command v1.0 | Real-time detection | ML-powered | Investigation excellence*