# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Spanish-language .NET educational course repository called "CursoNET" designed for **junior developers seeking promotion to semi-senior level**. The courses are **taught by architects** and focus on practical software engineering skills, architectural patterns, and best practices for enterprise-level development.

**Target Audience**: Junior .NET developers in professional environments
**Objective**: Promotion to semi-senior roles through practical knowledge and examples
**Teaching Style**: Architectural focus with real-world scenarios (financial systems, payment processing, microservices)

## Repository Structure

- `doc/` - Course materials in Spanish
  - `curso-gc.md` - Garbage Collection fundamentals course (markdown format)
  - `clase noif.md` - "No If" refactoring techniques course (markdown format)
  - `refactoring-presentacion.html` - Interactive HTML presentation on Refactoring (Procedural to Functional/OO)
  - `requisitos-presentacion (1).html` - Interactive HTML presentation on Requirements Engineering
  - `codestatico-presentacion.html` - Static Code Analysis presentation
  - `cursosql2-presentacion.html` - SQL Indexing and EF Core performance
  - `sqldisenio-presentacion.html` - Metadata-driven database design
- `samples/` - Organized practical exercises by class
  - `clase1-analisis-codigo-estatico/` - SonarLint and code quality examples
  - `clase2-analisis-requisitos/` - Requirements engineering exercises
  - `clase3-tecnicas-noif/` - "No If" pattern implementations
  - `clase4-refactoring-avanzado/` - Advanced refactoring techniques
  - `clase5-garbage-collection/` - Memory management and GC optimization
  - `clase6-indexacion-sql/` - SQL query optimization examples
  - `clase7-diseno-bd-metadatos/` - Metadata-driven database patterns
- `index.html` - Main portal with course navigation and interactive features

## Course Architecture and Content

### 1. Static Code Analysis (`codestatico-presentacion.html`)
**Format**: Interactive HTML slideshow (8 slides)
**Content**: Master SonarLint, cyclomatic complexity, and systematic refactoring
- SonarLint configuration and real-time analysis
- Cyclomatic complexity calculation and reduction (15+ → 2-3)
- Code smells identification and automated fixing
- Extract Method, Extract Class, Replace Conditional patterns
- CI/CD integration with quality gates and metrics

### 2. Requirements Analysis (`requisitos-presentacion.html`)
**Format**: Interactive HTML slideshow (14 slides)
**Content**: Modern requirements methodology for enterprise systems
- Functional vs Non-Functional Requirements
- User Stories with Connextra format
- Acceptance Criteria with Gherkin/BDD
- NFRs for microservices and Azure AKS
- Case studies from payment/financial systems

### 3. "No If" Techniques (`clase noif.md` + `noif-presentacion.html`)
**Format**: Markdown documentation + HTML presentation
**Structure**: 4 refactoring patterns with practical implementations
- **Dictionary Pattern** - Replacing switch statements 
- **Strategy Pattern** - Algorithm encapsulation
- **State Pattern** - Behavior based on internal state
- **Polymorphism** - Type-based conditional elimination

### 4. Advanced Refactoring (`refactoring-presentacion.html`)
**Format**: Interactive HTML slideshow (20 slides)
**Content**: Transform procedural code to functional/OO patterns
- Extract Method techniques
- Introduce Class for SRP
- Func<T> and Action<T> for flexibility
- Real case study: Order processing system
- Advanced patterns: Strategy, Factory, Pipeline

### 5. Garbage Collection (`curso-gc.md` + `gc-presentacion.html`)
**Format**: Markdown documentation + HTML presentation
**Structure**: 3 progressive classes
- **Class 1**: GC Fundamentals - Heap, generations (Gen 0/1/2), reachability, mark-and-compact
- **Class 2**: IDisposable Pattern - Unmanaged resources, proper disposal, using statements
- **Class 3**: OutOfMemoryException - Memory exhaustion scenarios, leak simulation

### 6. SQL Indexing and Performance (`cursosql2-presentacion.html`)
**Format**: Interactive HTML slideshow
**Content**: EF Core performance optimization and SQL indexing strategies
- Query performance analysis and optimization
- Index design patterns for high-traffic systems
- EF Core best practices for enterprise applications

### 7. Database Design with Metadata (`sqldisenio-presentacion.html`)
**Format**: Interactive HTML slideshow
**Content**: Metadata-driven database architecture patterns
- EAV (Entity-Attribute-Value) pattern implementation
- Dynamic schema design for enterprise systems
- Metadata-driven CRUD operations and validation

## Presentation Format and Style

### HTML Presentation Architecture
The HTML presentations follow a consistent, professional format:

**Visual Design**:
- Dark theme (`#0a0a0a` background) with gradient effects
- Purple/blue accent colors (`#667eea`, `#764ba2`)
- Glass-morphism cards with blur effects
- Responsive grid layouts
- Professional typography (system fonts)

**Interactive Features**:
- Keyboard navigation (arrow keys, spacebar, Home key)
- Touch/swipe support for mobile
- Progress bar and slide counter
- Previous/Next navigation buttons
- "Portal" home button for easy navigation
- Smooth animations and transitions
- Instructor information (Alejandro Sfrede, Área de Arquitectura)

**Code Presentation**:
- Syntax highlighting for C#
- "Before/After" comparisons with visual indicators
- Color-coded examples (bad=red, good=green)
- Monospace font for code blocks
- Collapsible sections for answers

**Content Structure**:
- Title slide with course overview
- Progressive concept building
- Practical examples from real systems
- Interactive exercises and questions
- Glossary of technical terms
- Conclusion with actionable takeaways

## Technical Context and Examples

The courses emphasize **enterprise-grade development** with examples from:
- **Payment Systems**: QR codes, credit card processing, PCI DSS compliance
- **Financial Services**: High-concurrency transaction processing
- **Cloud Architecture**: Azure AKS, microservices, observability
- **Security**: OWASP, mTLS, zero-trust principles
- **Performance**: SLI/SLO, circuit breakers, caching strategies

## Course Development Standards

When creating new courses following this format:

1. **Content Approach**: Practical, architect-level perspective with real-world scenarios
2. **Progression**: Start with fundamentals, build to advanced patterns
3. **Examples**: Use financial/payment systems for relevance
4. **Format**: Combine markdown docs with interactive HTML presentations
5. **Exercises**: Include hands-on coding exercises and architectural decisions
6. **Assessment**: Questions that test both understanding and practical application

## Tools and Technologies Referenced

The courses assume familiarity with:
- .NET Core/5+, C#, ASP.NET Core
- Azure cloud services (AKS, Key Vault, etc.)
- Microservices patterns (CQRS, Event Sourcing)
- Testing frameworks (SpecFlow, BDD)
- Observability tools (Serilog, Prometheus)
- DevOps practices (CI/CD, Infrastructure as Code)

## Exercise Portal Architecture

The `samples/index.html` serves as an interactive portal featuring:
- **File Explorer Interface**: Navigate through 7 classes of exercises
- **Code Viewer Modal**: Inline display of C#, SQL, and markdown files
- **Responsive Design**: Optimized for desktop and mobile viewing
- **Search Functionality**: Filter exercises by class or topic
- **Progress Tracking**: Visual indicators for completed exercises

### Exercise Structure Pattern
Each class follows a consistent "Before/After" pattern:
- `*-Before.cs/sql/md` - Problematic code with architectural issues
- `*-After.cs/sql/md` - Refactored solution with explanations
- `README.md` - Learning objectives and setup instructions
- `README.html` - Formatted version for web viewing

## Working with Presentations

### Navigation Standards
All HTML presentations use standardized navigation:
- **Arrow Keys**: Previous/Next slide
- **Spacebar**: Next slide
- **Home Key**: Return to main portal
- **Navigation Buttons**: Click-based controls
- **Slide Counter**: Current position indicator

### Content Modification Guidelines
When updating presentations:
1. Maintain consistent visual theme (dark background, purple/blue gradients)
2. Use semantic HTML structure with proper slide classes
3. Include instructor information in navigation area
4. Ensure responsive breakpoints for mobile viewing
5. Test keyboard navigation functionality