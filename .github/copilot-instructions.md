# AI Coding Agent Instructions for CursoNET

## Project Context
- **CursoNET** is a Spanish-language .NET educational repository for junior developers aiming to reach semi-senior level, with a strong focus on enterprise architecture, clean code, and real-world scenarios (FinTech, microservices, payment systems).
- All course content, code, and documentation are in Spanish. Use clear, concise Spanish in all code comments and documentation.

## Architecture & Structure
- **Monorepo**: Organized by `doc/` (course theory, markdown, HTML) and `samples/` (practical C# code, before/after refactoring, patterns).
- **Key Patterns**: Emphasize Dictionary, Strategy, State, and Polymorphism patterns to eliminate complex conditionals (see `samples/clase3-tecnicas-noif/`).
- **Refactoring**: Each code sample has `*-Before.cs` and `*-After.cs` files. Always show both versions and explain the refactor in Spanish.
- **Requirements Analysis**: User stories (Connextra), acceptance criteria (Gherkin), and NFRs are central (see `samples/clase2-analisis-requisitos/`).

## Coding & Documentation Conventions
- **C# Modern**: Use .NET Core, ASP.NET Core, Entity Framework Core. Follow Microsoft C# conventions.
- **Patterns**: Prefer SOLID, CQRS, DDD, and microservices approaches. Avoid complex `if`/`switch`—use patterns above.
- **Comments**: Explain the "why" behind decisions, not just the "what". Use Spanish for all comments.
- **Refactor Format**: For any refactor, always output:
  - `// --- ANTES ---` (before)
  - `// --- DESPUÉS ---` (after)
  - Brief explanation of the improvement (in Spanish)
- **Testing**: Use xUnit or NUnit for unit tests. For ASP.NET Core, use TestServer/WebApplicationFactory for integration tests.
- **CI/CD**: Azure Pipelines is preferred for automation.

## Developer Workflows
- **Build**: Use `dotnet build` for all C# projects.
- **Test**: Use `dotnet test` for running tests. Place tests alongside code samples or in dedicated test projects.
- **Static Analysis**: Use SonarLint (see `samples/clase1-analisis-codigo-estatico/`).
- **Interactive Content**: HTML presentations in `doc/` are part of the learning flow—do not modify unless updating course content.

## Integration & External Dependencies
- **Database**: SQL Server (local or Azure), optionally NoSQL.
- **Messaging**: RabbitMQ, Azure Service Bus (optional, for advanced samples).
- **Containers**: Docker/Kubernetes (optional, for advanced samples).

## Examples
- See `samples/clase3-tecnicas-noif/01-DictionaryPattern-After.cs` for Dictionary pattern.
- See `samples/clase2-analisis-requisitos/01-Requirements-Simple-After.md` for requirements analysis format.

## Special Rules
- Never output generic advice—always tailor to CursoNET's patterns and Spanish context.
- When in doubt, prefer clarity, simplicity, and educational value over cleverness.

---
¿Faltan convenciones, flujos o patrones clave? Por favor, revisa y sugiere mejoras para que la guía sea aún más útil para agentes de IA y desarrolladores nuevos en el proyecto.
