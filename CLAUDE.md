# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

GraphZen is a GraphQL SDK for .NET that implements the GraphQL specification. It provides a code-first approach to building GraphQL APIs with ASP.NET Core integration.

## Build Commands

```bash
# Build entire solution
dotnet build

# Build specific project
dotnet build src/GraphZen.TypeSystem/GraphZen.TypeSystem.csproj

# Run all tests
dotnet test

# Run tests for a specific project
dotnet test test/GraphZen.TypeSystem.Tests/GraphZen.TypeSystem.Tests.csproj

# Run a single test by name
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"

# Run tests with verbosity
dotnet test -v n
```

## Architecture

### Core Packages (Layered Dependencies)

The SDK is organized in layers, each building on the previous:

1. **GraphZen.Abstractions** - Data annotations for code-first GraphQL development (no dependencies)
2. **GraphZen.Infrastructure** - Internal utilities shared across packages
3. **GraphZen.LanguageModel** - GraphQL parser, AST, and printer (uses Superpower for parsing)
4. **GraphZen.TypeSystem** - Schema definition and type system object model
5. **GraphZen.QueryEngine** - Query execution engine
6. **GraphZen.AspNetCore.Server** - ASP.NET Core integration for hosting GraphQL APIs

### Key Concepts

**Schema Building**: Schemas are built using `SchemaBuilder` with a fluent API. The `SchemaDefinition` is the mutable builder state, and `Schema` is the immutable runtime representation.

**Type System**: GraphQL types follow a taxonomy pattern with interface pairs:
- `I*Definition` interfaces for mutable builder state (e.g., `IObjectTypeDefinition`)
- `I*` interfaces for immutable runtime types (e.g., `IObjectType`)

**Language Model**: The `LanguageModel` namespace contains:
- `Syntax/` - AST node types (immutable, each with `*SyntaxExtensions` for fluent operations)
- `GraphQLSyntaxVisitor` and `GraphQLSyntaxWalker` - Visitor pattern for AST traversal
- Generated code via T4 templates (`*.Generated.cs` from `*.Generated.tt`)

**ASP.NET Core Integration**:
- `services.AddGraphQLContext()` - Register GraphQL services
- `endpoints.MapGraphQL()` - Map GraphQL endpoint
- `endpoints.MapGraphQLPlayground()` - Map GraphQL Playground UI

### Test Organization

Tests are organized by type:
- `*.Tests` - Unit tests
- `*.IntegrationTests` - Integration tests
- `*.FunctionalTests` - End-to-end functional tests
- `GraphZen.Infrastructure.Testing` - Shared test utilities (xUnit, with custom `JsonAssert` and `StringAssert` helpers)

### Code Generation

The `GraphZen.DevCli` project contains code generation utilities:
```bash
dotnet run --project src/GraphZen.DevCli/GraphZen.DevCli.csproj gen
```

## Code Style

- Nullable reference types enabled (`<Nullable>enable</Nullable>`)
- Warnings treated as errors (`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`)
- Uses JetBrains.Annotations for nullability hints
- Latest C# language version
