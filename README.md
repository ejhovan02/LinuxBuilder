# LinuxBuilder

A full-stack **.NET** application with an **ASP.NET Core** backend API, **Entity Framework Core (SQLite)** + **ASP.NET Core Identity** + **JWT Bearer** authentication, and a **Blazor WebAssembly** client for managing custom Linux OS templates and packages.

## Solution Architecture

- **Backend API (`LinuxBuilder.Server` / root API)**: Built on ASP.NET Core with `LinuxBuilderContext` (`IdentityDbContext<ApplicationUser>`), SQLite persistence, ASP.NET Core Identity with automatic seeding of `"Admin"` and `"User"` roles on startup, JWT Bearer authentication, CORS configured for `http://localhost:5210` and `https://linuxbuilder-client.azurestaticapps.net`, and `IMemoryCache` for read endpoints.
- **Blazor WebAssembly Client (`Client` / `LinuxBuilder.Client`)**: Provides pages for home navigation (`/`), registration (`/register`), login (`/login`), and template creation (`/create-template`), backed by `AuthService`, `TemplateService`, and `UserSessionService`.
- **Shared Models (`LinuxBuilder.Shared.Models`)**: Contains `OsTemplate`, `Package`, `LoginModel`, and `RegisterModel` (alongside client-side `LoginResponse`).

## Project Structure

```text
LinuxBuilder/
├── Controllers/
│   ├── AuthController.cs          # User registration, login, and 7-day JWT generation
│   └── TemplateController.cs      # Protected endpoints for packages and OS templates (with IMemoryCache)
├── Data/ (or root)
│   ├── ApplicationUser.cs         # IdentityUser subclass with Templates collection
│   └── LinuxBuilderContext.cs     # EF Core IdentityDbContext with Packages and OsTemplates
├── Client/
│   ├── Pages/
│   │   ├── Home.razor             # / — navigation to /login and /create-template
│   │   ├── Login.razor            # /login — authenticates and starts UserSessionService session
│   │   ├── Register.razor         # /register — registers a new user and redirects to /login
│   │   └── CreateTemplate.razor   # /create-template — session-guarded form to create an OS template
│   ├── Services/
│   │   ├── AuthService.cs         # Calls /api/auth/register and /api/auth/login
│   │   ├── TemplateService.cs     # Calls /api/template/packages, /api/template/os-templates, and /api/template
│   │   └── UserSessionService.cs  # Holds active UserSession (userId, Token, Email) in memory
│   ├── Models/
│   │   └── LoginResponse.cs       # Client DTO for login responses
│   └── Program.cs                 # Blazor WASM host, ApiUrl HttpClient, and scoped services
├── Shared/Models/
│   ├── OsTemplate.cs              # Linux distro, desktop, packages, locale, partition, and version metadata
│   ├── Package.cs                 # Package definition (Id, Name, PackageName)
│   ├── LoginModel.cs              # Login request validation model
│   └── RegisterModel.cs           # Registration request with password confirmation validation
└── Program.cs                     # SQLite, Identity, JWT Bearer, CORS, role seeding, and SPA fallback
