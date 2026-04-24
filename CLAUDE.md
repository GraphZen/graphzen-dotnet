# CLAUDE.md

GraphZen is a code-first GraphQL SDK for .NET.

## Commands

- **Build:** `dotnet build`
- **Test:** `dotnet test`
- **Filter tests:** `dotnet test --filter "FullyQualifiedName~TestName"`
- **Code gen:** `dotnet run --project src/GraphZen.DevCli/GraphZen.DevCli.csproj gen`

## Architecture

Layered packages, each building on the previous:

1. **Abstractions** - Data annotations for code-first GraphQL development
2. **Infrastructure** - Internal shared utilities
3. **LanguageModel** - GraphQL parser, AST, and printer (uses Superpower)
4. **TypeSystem** - Schema definition and type system object model
5. **QueryEngine** - Query execution engine
6. **AspNetCore.Server** - ASP.NET Core integration for hosting GraphQL APIs

Key patterns:
- `SchemaBuilder` fluent API builds `SchemaDefinition` (mutable) -> `Schema` (immutable)
- Type system uses `I*Definition` / `I*` interface pairs (mutable builder vs immutable runtime)
- AST nodes in `Syntax/` with visitor/walker pattern; T4-generated code (`*.Generated.cs`)
- Tests follow `*.Tests`, `*.IntegrationTests`, `*.FunctionalTests` conventions

## Related Repositories

- **Superpower** (parsing library) - Local clone at `~/Code/datalust/superpower` (upstream: https://github.com/datalust/superpower). Used by `GraphZen.LanguageModel` for GraphQL parsing.

## ReSharper CLI (`dotnet jb`)

Inspection excludes (e.g. `TestResults/`) are configured in `GraphZen.sln.DotSettings`. Cleanup requires `--exclude` (not supported via `.DotSettings`).

- **Inspect:** `dotnet jb inspectcode GraphZen.sln -f Text --stdout`
- **Inspect (warnings+):** `dotnet jb inspectcode GraphZen.sln -e WARNING -f Text --stdout`
- **Inspect single project:** `dotnet jb inspectcode GraphZen.sln --project "GraphZen.TypeSystem" -f Text --stdout`
- **Cleanup:** `dotnet jb cleanupcode GraphZen.sln --exclude="**/TestResults/**"`
- **Cleanup scoped:** `dotnet jb cleanupcode GraphZen.sln --include "src/GraphZen.TypeSystem/**/*.cs"`
- **Reformat only:** `dotnet jb cleanupcode GraphZen.sln --exclude="**/TestResults/**" --profile "Built-in: Reformat Code"`

## Code Style

Nullable enabled, warnings as errors, latest C# language version.

## Related Repositories

- **Superpower** (parsing library): `~/Code/datalust/superpower` | [github.com/datalust/superpower](https://github.com/datalust/superpower)
- **graphql-spec** (GraphQL specification): `~/Code/graphql/graphql-spec` | [github.com/graphql/graphql-spec](https://github.com/graphql/graphql-spec)
- **graphql-js** (reference implementation): `~/Code/graphql/graphql-js` | [github.com/graphql/graphql-js](https://github.com/graphql/graphql-js)
- **graphiql** (in-browser GraphQL IDE): `~/Code/graphql/graphiql` | [github.com/graphql/graphiql](https://github.com/graphql/graphiql)
- **dataloader** (batching/caching utility): `~/Code/graphql/dataloader` | [github.com/graphql/dataloader](https://github.com/graphql/dataloader)
- **foundation** (GraphQL Foundation): `~/Code/graphql/foundation` | [github.com/graphql/foundation](https://github.com/graphql/foundation)
