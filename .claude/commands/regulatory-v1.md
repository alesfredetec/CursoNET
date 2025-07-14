# /regulatory - Regulatory Compliance & Audit Command v1.0

**Purpose**: Comprehensive regulatory compliance management, audit preparation, and compliance validation for financial systems.

@include shared/universal-constants-fintexa-v2.yml#Fintexa_Legend_Extensions

## Core Philosophy
@include shared/fintexa-core-v1.yml#Fintexa_Philosophy_v1

## Command Overview
```yaml
/regulatory [primary-flags] [universal-flags]
Purpose: "Regulatory compliance | Audit management | Risk assessment | Control validation"
Scope: "PCI DSS | BCRA | UIF | AFIP | SOX | GDPR compliance"
Integration: "Fintech personas | Compliance patterns | Audit workflows"
```

## Primary Flags

### Compliance Assessment
```yaml
--audit-prep: "Audit preparation | Evidence collection | Control documentation | Gap analysis"
--pci-scan: "PCI DSS compliance scan | Control validation | Remediation planning"
--bcra-compliance: "BCRA regulatory compliance | Central bank requirements | Financial reporting"
--data-protection: "GDPR | Data privacy | Information governance | Consent management"
```

### Risk Management
```yaml
--risk-assessment: "Regulatory risk evaluation | Impact analysis | Mitigation strategies"
--control-validation: "Internal controls testing | Effectiveness assessment | Documentation"
--gap-analysis: "Compliance gap identification | Remediation roadmap | Priority assessment"
--monitoring-setup: "Continuous compliance monitoring | Alert configuration | Reporting automation"
```

### Reporting & Documentation
```yaml
--regulatory-reporting: "BCRA | UIF | AFIP reporting | Submission workflows | Validation"
--audit-trail: "Immutable audit logs | Transaction traceability | Evidence preservation"
--documentation: "Compliance documentation | Policy creation | Procedure development"
--evidence-collection: "Audit evidence gathering | Control documentation | Test results"
```

### Specialized Operations
```yaml
--incident-reporting: "Regulatory incident notification | Impact assessment | Remediation tracking"
--certification: "Compliance certification management | Renewal tracking | Maintenance"
--training: "Compliance training programs | Awareness campaigns | Knowledge assessments"
--vendor-assessment: "Third-party risk assessment | Due diligence | Contract compliance"
```

## Universal Flags Integration
@include commands/shared/flag-inheritance.yml#Universal_Always

### Thinking Modes
```yaml
--think: "Standard regulatory analysis | Compliance review | Risk assessment"
--think-hard: "Deep compliance analysis | Complex regulatory interpretation | Multi-jurisdiction review"
--ultrathink: "Comprehensive audit preparation | Critical compliance issues | Regulatory crisis response"
```

### MCP Integration
```yaml
--seq: "Multi-step compliance analysis | Regulatory interpretation | Audit workflow orchestration"
--c7: "Regulatory standards research | Best practices | Official documentation"
--pup: "Compliance testing | Control validation | Audit evidence collection"
```

### Persona Integration
```yaml
--persona-compliance-security: "Default persona for regulatory operations"
--persona-fintech-architect: "Architectural compliance review | System design validation"
--persona-data-analyst: "Regulatory reporting | Data compliance | Analytics validation"
--persona-payment-specialist: "Payment compliance | Transaction monitoring | Settlement auditing"
```

## Command Patterns

### Audit Preparation Workflow
```bash
# Comprehensive audit preparation
/regulatory --audit-prep --seq --think-hard --persona-compliance-security
/regulatory --evidence-collection --documentation --persona-data-analyst
/regulatory --control-validation --pup --persona-fintech-architect
```

### PCI DSS Compliance
```bash
# Complete PCI DSS assessment
/regulatory --pci-scan --seq --persona-compliance-security
/regulatory --gap-analysis --remediation-plan --think-hard
/regulatory --control-validation --pup --evidence-collection
```

### Regulatory Reporting
```bash
# BCRA regulatory reporting
/regulatory --bcra-compliance --regulatory-reporting --persona-data-analyst
/regulatory --data-validation --audit-trail --seq
/regulatory --submission-workflow --monitoring-setup
```

### Incident Response
```bash
# Regulatory incident management
/regulatory --incident-reporting --think-hard --persona-compliance-security
/regulatory --impact-assessment --remediation-tracking --seq
/regulatory --documentation --evidence-collection
```

## Integration Patterns

### Fintech Ecosystem Integration
@include commands/shared/fintexa-integration-patterns-v1.yml#Regulatory_Integration

### Compliance Framework
@include commands/shared/fintech-persona-patterns-v1.yml#Compliance_Security_Patterns_v1

### Quality Standards
```yaml
Compliance_Metrics:
  Audit_Pass_Rate: "100% target | Zero critical findings | Complete remediation"
  Control_Effectiveness: ">95% effective controls | Continuous monitoring | Regular testing"
  Regulatory_Adherence: "100% compliance | Zero violations | Proactive monitoring"
  
Evidence_Quality:
  Documentation_Completeness: "100% documented controls | Updated procedures | Clear evidence"
  Traceability: "Complete audit trails | Immutable logs | Full transaction history"
  Validation: "Independent verification | Third-party validation | Regulatory approval"
```

## Specialized Workflows

### Multi-Jurisdiction Compliance
```yaml
Argentina_Regulations:
  BCRA: "Central bank regulations | Financial reporting | Real-time monitoring"
  UIF: "Money laundering prevention | Suspicious activity reporting | Customer screening"
  AFIP: "Tax compliance | Invoice validation | Taxpayer verification"
  CNV: "Securities regulation | Market supervision | Investor protection"

International_Standards:
  PCI_DSS: "Payment card industry security | Tokenization | Secure processing"
  ISO_27001: "Information security management | Risk assessment | Control framework"
  SOC_2: "Service organization controls | Trust principles | Audit requirements"
  GDPR: "Data protection | Privacy rights | Consent management"
```

### Risk Assessment Framework
```yaml
Risk_Categories:
  Regulatory_Risk: "Compliance violations | Regulatory penalties | License revocation"
  Operational_Risk: "Process failures | System outages | Human error"
  Financial_Risk: "Transaction losses | Settlement failures | Fraud incidents"
  Reputational_Risk: "Customer trust | Market confidence | Brand damage"

Mitigation_Strategies:
  Preventive_Controls: "Policy enforcement | Access controls | Monitoring systems"
  Detective_Controls: "Audit procedures | Exception reporting | Continuous monitoring"
  Corrective_Controls: "Incident response | Remediation procedures | Process improvement"
```

## Output Organization

### Compliance Reports
```yaml
Report_Structure:
  Executive_Summary: "Compliance status | Key findings | Recommendations | Action plans"
  Detailed_Findings: "Control assessment | Gap analysis | Risk evaluation | Evidence review"
  Remediation_Plan: "Priority actions | Timeline | Resources | Success metrics"
  Appendices: "Supporting evidence | Control documentation | Test results | Certifications"

Report_Distribution:
  Internal_Stakeholders: "Management | Audit committee | Risk committee | Legal team"
  External_Parties: "Regulators | Auditors | Certification bodies | Business partners"
  Documentation: "Compliance repository | Version control | Access controls | Retention policies"
```

### Audit Evidence
```yaml
Evidence_Types:
  System_Evidence: "Configuration screenshots | Log extracts | System reports | Access lists"
  Process_Evidence: "Procedure documentation | Training records | Meeting minutes | Approvals"
  Control_Evidence: "Control testing results | Exception reports | Monitoring outputs | Reviews"
  
Evidence_Management:
  Collection: "Automated extraction | Manual documentation | Third-party validation"
  Storage: "Secure repository | Version control | Access controls | Retention management"
  Presentation: "Organized folders | Clear indexing | Summary documents | Cross-references"
```

## Success Metrics

### Compliance Effectiveness
```yaml
Quantitative_Metrics:
  Audit_Success: "100% clean audits | Zero critical findings | Complete remediation within SLA"
  Regulatory_Adherence: "Zero violations | 100% reporting compliance | Proactive monitoring"
  Control_Performance: ">95% control effectiveness | Continuous improvement | Risk reduction"

Qualitative_Metrics:
  Stakeholder_Confidence: "Regulator satisfaction | Management confidence | Audit committee approval"
  Process_Maturity: "Automated controls | Proactive monitoring | Continuous improvement"
  Cultural_Integration: "Compliance awareness | Risk culture | Ethical behavior"
```

### Operational Excellence
```yaml
Efficiency_Metrics:
  Automation_Rate: ">80% automated controls | Reduced manual effort | Streamlined processes"
  Response_Time: "<24h issue resolution | Real-time monitoring | Immediate alerts"
  Cost_Optimization: "Reduced compliance costs | Efficient resource utilization | ROI improvement"

Quality_Metrics:
  Accuracy: "100% accurate reporting | Error-free submissions | Validated data"
  Completeness: "100% control coverage | Complete documentation | Full traceability"
  Timeliness: "On-time submissions | Proactive notifications | Early issue detection"
```

---
*Regulatory Command v1.0 | Financial compliance | Evidence-based auditing | Proactive risk management*