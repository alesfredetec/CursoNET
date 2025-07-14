# /hello - Interactive Greeting Command v1.0

**Purpose**: Interactive user greeting with customizable formality levels and educational integration.

@include shared/universal-constants.yml#Universal_Legend

## Command Overview
```yaml
/hello [primary-flags] [universal-flags]
Purpose: "User interaction | Greeting protocols | Educational engagement | Rapport building"
Scope: "Basic greetings | Formal communications | Mentor interactions | User onboarding"
Integration: "Universal flags | Persona system | Context awareness | Session management"
```

## Primary Flags

### Formality Control
```yaml
--formal: "Professional greeting protocol | Business etiquette | Respectful communication | Structured interaction"
--casual: "Informal greeting style | Friendly approach | Relaxed communication | Personal touch"
--introduction: "Comprehensive introduction | Capability overview | Service explanation | Expectation setting"
```

### Interaction Modes
```yaml
--welcome: "New user onboarding | Initial setup guidance | Feature introduction | Getting started"
--status: "Current session status | Active configurations | Available capabilities | Context summary"
--help-overview: "Quick help summary | Available commands | Getting started guide | Resource links"
```

## Universal Flags Integration
@include commands/shared/flag-inheritance.yml#Universal_Always

### Persona Integration
```yaml
--persona-mentor: "Educational approach | Supportive communication | Learning-focused interaction"
--persona-architect: "Systematic introduction | Structured communication | Professional protocol"
--persona-frontend: "User-experience focused | Friendly interface | Accessible communication"
--persona-estudiante: "Learning-focused greeting | Educational context | Patient interaction | Question encouragement"
```

### Thinking Modes
```yaml
--think: "Standard greeting analysis | Context assessment | Appropriate response selection"
--think-hard: "Comprehensive user analysis | Optimal interaction strategy | Personalized approach"
```

## Command Patterns

### Basic Greeting Flow
```bash
# Simple greeting
/hello
# Output: Friendly, context-aware greeting

# Formal greeting
/hello --formal
# Output: Professional, respectful greeting with capabilities overview

# Mentor-guided greeting
/hello --persona-mentor
# Output: Educational greeting with learning focus and guidance offers

# Student-focused greeting  
/hello --persona-estudiante --welcome
# Output: Learning-oriented welcome with educational support offers
```

### Advanced Interaction Patterns
```bash
# Comprehensive introduction
/hello --formal --introduction --persona-mentor --think-hard
# Output: Detailed professional introduction with educational focus

# Status-aware greeting
/hello --status --welcome --persona-architect
# Output: Systematic greeting with current context and structured guidance

# Educational onboarding
/hello --welcome --introduction --persona-estudiante --seq
# Output: Step-by-step educational welcome with learning pathway guidance
```

## Implementation Logic

### Greeting Personalization Engine
```yaml
Context_Analysis:
  New_User: "First interaction detection | Onboarding flow | Capability introduction"
  Returning_User: "Session continuity | Previous context | Progress acknowledgment"
  Project_Context: "Current project awareness | Relevant capability highlighting | Task-specific greeting"
  Educational_Context: "Learning mode detection | Student needs assessment | Educational resource availability"

Formality_Assessment:
  Formal_Indicators: "Business context | Professional project | Enterprise environment"
  Casual_Indicators: "Personal project | Learning context | Experimental environment"
  Educational_Indicators: "Tutorial mode | Learning session | Student persona active"
  Auto_Detection: "Context clues | User history | Project type | Communication patterns"
```

### Response Generation Framework
```yaml
Greeting_Components:
  Opening: "Contextual salutation | Time-aware greeting | Personalized address"
  Capability_Summary: "Relevant features | Available commands | Context-specific options"
  Educational_Offer: "Learning support | Tutorial availability | Guided assistance"
  Engagement_Offer: "How can I help | Next steps suggestion | Guidance availability"
  Session_Context: "Current state | Active configurations | Available resources"
```

## Educational Integration

### Learning-Focused Greetings
@include commands/shared/learning-patterns-v1.yml#Socratic_Method#Question_Categories#Clarification_Questions

```yaml
Educational_Greeting_Flow:
  Welcome_Assessment: "What brings you here today? | What would you like to learn? | How can I help you understand?"
  Context_Building: "What's your experience with this topic? | Any specific goals? | Preferred learning style?"
  Support_Offering: "I can explain step-by-step | Ask questions anytime | We'll go at your pace"
  Expectation_Setting: "Learning is collaborative | Questions are encouraged | Mistakes help us learn"
```

### Persona-Specific Behaviors
```yaml
Estudiante_Persona_Greeting:
  Approach: "Patient and encouraging | Question-welcoming | Step-by-step oriented"
  Language: "Simple explanations | Clear structure | Supportive tone"
  Offers: "Tutorial mode | Guided learning | Practice exercises | Concept clarification"
  
Mentor_Persona_Greeting:
  Approach: "Supportive guidance | Knowledge sharing | Skill development focus"
  Language: "Educational terminology | Clear instruction | Encouraging feedback"
  Offers: "Learning pathways | Skill assessment | Resource recommendations | Progress tracking"
```

## Quality Standards

### Response Quality Metrics
```yaml
Appropriateness: "Context-suitable formality | Professional when needed | Friendly when appropriate"
Completeness: "All requested information | Relevant capabilities | Clear next steps"
Engagement: "Inviting interaction | Helpful tone | Encouraging communication"
Educational_Value: "Learning opportunities identified | Support offered | Growth mindset promoted"
Efficiency: "Concise yet complete | No unnecessary verbosity | Clear communication"
```

### Success Criteria
```yaml
User_Experience: "Positive first impression | Clear capability understanding | Confident next steps"
Educational_Impact: "Learning motivation increased | Support accessibility clear | Growth opportunities identified"
Technical_Accuracy: "Correct context detection | Appropriate persona activation | Proper flag handling"
Integration_Quality: "Seamless persona switching | Proper flag inheritance | Context preservation"
```

## Usage Examples

### Basic Usage
```bash
/hello
# Simple, context-aware greeting

/hello --formal
# Professional greeting with capability overview

/hello --casual --welcome
# Friendly welcome for new users
```

### Educational Usage
```bash
/hello --persona-estudiante
# Student-focused greeting with learning emphasis

/hello --persona-mentor --introduction
# Mentor-style comprehensive introduction

/hello --welcome --persona-estudiante --think-hard
# Deep educational onboarding analysis
```

### Advanced Combinations
```bash
/hello --formal --introduction --persona-mentor --seq --think-hard
# Comprehensive professional educational introduction with systematic approach

/hello --status --persona-estudiante --help-overview
# Student-focused status with learning-oriented help overview
```

---
*Hello Command v1.0 | Interactive greetings | Educational integration | Context-aware communication*