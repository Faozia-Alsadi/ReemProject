# ReemProject — Project Rules for Claude

## Technology Stack
- **Backend**: .NET 10 / C# — Clean Architecture
- **Frontend**: Angular 21 — standalone components
- **Styling**: Bootstrap v5 (no other CSS frameworks)
- **Database**: Microsoft SQL Server 2025

## Architecture
- Follow Clean Architecture strictly: Domain → Application → Infrastructure → API
- Never reference outer layers from inner layers (Domain has no dependencies)
- Use CQRS (MediatR) for Application layer commands and queries
- Use Repository pattern in Infrastructure layer
- Keep controllers thin — delegate everything to MediatR handlers

## Code Conventions
- Follow existing naming conventions in the project
- No hardcoded strings — use i18n resource files (AR + EN)
- Use `async/await` throughout (no `.Result` or `.Wait()`)
- Use `Soft Delete` — never hard delete; all entities have `IsDeleted` + `DeletedAt` fields
- All entities extend `BaseEntity` (has `Id`, `CreatedAt`, `UpdatedAt`, `IsDeleted`)

## Security (Security by Design)
- Follow OWASP Top 10
- HTTP security headers must be set in middleware
- CORS must be explicitly configured
- Rate limiting enabled on all public endpoints
- No secrets in code — use environment variables or Azure Key Vault
- PDPPL compliance required for all personal data handling

## Localization
- Primary language: Arabic (RTL)
- Secondary language: English (LTR)
- All UI strings go in translation files under `frontend/src/assets/i18n/`
- Backend validation messages also use resource files

## Git
- One branch per feature: `feature/<name>`
- Commit messages: clear and descriptive in English
- Never commit `appsettings.Development.json`, `.env`, secrets, or connection strings

## Prompt Template
When asking Claude to implement a task, use this structure:

```
[Goal] What needs to be done (1–2 sentences).
[Reference files] @path/to/file — follow this existing pattern.
[Constraints]
- Stack: .NET 10 + Angular 21. No new libraries without approval.
- Architecture: Clean Architecture, keep layers separated.
- Localization: AR + EN with RTL. No hardcoded strings.
- Security: OWASP Top 10.
- Naming: match existing conventions.
[Steps] Understand → plan → implement after approval.
[Verification] Run tests + build, show result, fix failures.
[Out of scope] List what must NOT change.
```

## Important Reminders
- Never paste real ministry data, citizen data, passwords, or API keys into Claude
- Use fake/mock data in examples
- Review all Claude suggestions before applying — Claude assists, it does not replace the developer
