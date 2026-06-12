# CLAUDE.md — MichaelAaronsonCom

## What this project is

Personal website and full-stack skills showcase for MichaelAaronson.com. Also serves as a
working contact-management and productivity tool. Personal project: prefer low-overhead,
pragmatic solutions over enterprise ceremony.

## Stack

- ASP.NET Core **Razor Pages**, **.NET 10**
- Entity Framework Core, **Code-First** migrations
- Two databases: **SQL Server** for content, **SQLite** for ASP.NET Identity
- Bootstrap 5 for base styling; CSS Grid + flexbox cards for new list views
- Markdown content via **Markdig** (render) + **EasyMDE** (edit, loaded from CDN)
- **MetadataExtractor** for image EXIF data
- IDE: Visual Studio 2026 Insiders (ReSharper installed)
- Hosting: Winhost shared hosting; PWA / Edge Sidebar support (minimum width ~376px)

## Architecture

- Pages organized under `Pages/Public/`, `Pages/Private/`, `Pages/Admin/`
- Experimental/sandbox pages live under `Pages/Devel/` (e.g., `Pages/Devel/GridAndCards/`)
- Key functional areas: 
	- Contacts: 
		- Persons (with image management), 
		- Groups (many-to-many with Person), 
	- Planning, DailyTasks:
		- Goal: High level. Optional. Rarely used.
		- Domain: Computer, Finance, Health, etc. Required (one per step)
		- Project: A collection of relevant steps. Optional. Rarely used.
		- Step: The core unit of work. Listed on DailyTasks and Planning pages.

## Working agreement — IMPORTANT

- **Michael reviews and understands every change.** Propose a plan before editing.
  Make small, focused changes. Explain anything non-obvious.
- **Michael handles Git commits and publishing himself.** Do not commit, push, or publish.
- **Incremental, test-before-commit**: verify locally first; production deploys come later
  and are Michael's call.
- **Sandbox-first for new UI patterns**: prove the pattern in a `Pages/Devel/` sandbox page
  before touching production pages.
- Dev database is for schema testing only; **production holds the real data**.

## Conventions

### Razor Pages & binding

- Do NOT use `[BindProperty] public Step Step { get; set; }` (overposting risk).
  Bind individual properties instead, as in the existing Private pages.
- DRY over convenience: link to existing Create/Edit pages rather than duplicating
  inline forms, so model changes only touch one form.
- Delete must carry friction: Delete lives on the Edit page only (never on listing pages),
  behind a JS confirm dialog, styled `btn-outline-danger`. Delete is for mistakes and
  duplicates, not routine workflow.

### CSS

- New list views use **CSS Grid + card layouts**, not HTML tables. Existing table pages
  are left as-is. (Bootstrap `table-responsive` is just horizontal scrolling — tables are
  not responsive layout primitives.)
- Scoped CSS: files must be named exactly `<Page>.cshtml.css` — a plain `.css` extension
  is silently ignored. `Website.styles.css` is a **generated bundle — never edit it**.
  The bundle `<link>` must exist in `_Layout.cshtml`.
- Target narrow widths (~376px) for pages used in the Edge Sidebar PWA (Planning,
  DailyTasks). Use container queries for reflow, not viewport media queries alone.

### Scaffolding

- The new `dotnet scaffold` engine writes to `Pages/<Model>Pages` regardless of the folder
  selected — move generated files to the intended folder afterward.
- The scaffolder builds the project to discover types: **build errors block all scaffold
  attempts**. Watch for CS0101/CS0111 duplicate-definition errors from prior scaffold runs.

## Build & deploy

- Build/run: standard `dotnet build` / `dotnet run` from the Website project, or VS 2026.
- Migrations: EF Core Code-First (`Add-Migration` / `Update-Database` in VS Package
  Manager Console, or `dotnet ef`).
- Publish: **Visual Studio Publish only** (it sets correct file permissions on Winhost —
  raw FTP does not). Publish profiles are prefixed `PROD_` / `DEV_`; never mix them up.
  Databases: `DB_129343_prod` and `DB_129343_devel`.

## Current focus (update as it changes)

- Port the proven GridAndCards pattern (CSS Grid rows + flexbox cards + container-query
  reflow) from the sandbox to the real Planning page — DONE for Planning.
- Next: apply the same card layout to **DailyTasks**.
- Planned DailyTasks enhancements: snooze/reschedule (push StartDate forward), a status
  field (In Progress vs. eligible), due date separate from StartDate.
- Resolve `dotnet scaffold` duplicate-output issues (notes in Obsidian:
  `Notes/Devel/MichaelAaronsonCom/MichaelAaronsonCom - CRUD scaffolding.md`).