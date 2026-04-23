# GraphZen

A code-first GraphQL framework for .NET.

GraphZen lets you define GraphQL schemas using plain C# classes and conventions, with full support for queries, mutations, subscriptions, and an integrated playground UI. It handles schema generation, query parsing, validation, and execution out of the box.

> **Project status:** This project is being actively revived after a period of dormancy. Expect breaking changes as the codebase is modernized.

## Quick Start

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later

### Installation

```bash
dotnet add package GraphZen.AspNetCore.Server
```

### Define your schema

GraphZen discovers your schema from plain C# classes by convention. Define `Query`, `Mutation`, and model types as regular classes:

```csharp
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public class Query
{
    [Description("Get the latest blog posts")]
    public List<Post> Posts(int? postId)
    {
        if (postId != null)
            return FakeBlogData.Posts.Where(_ => _.Id == postId.Value).ToList();

        return FakeBlogData.Posts;
    }
}

public class Mutation
{
    [Description("Add a new post")]
    public bool AddPost(string author, string title, string post)
    {
        FakeBlogData.Posts.Add(new Post
        {
            Id = FakeBlogData.Posts.Max(_ => _.Id) + 1,
            Author = author,
            Title = title,
            Content = post
        });
        return true;
    }
}
```

### Wire it up in ASP.NET Core

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGraphQLContext();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
            endpoints.MapGraphQLPlayground();
        });
    }
}
```

That's it. Run the app and open the root URL to access the GraphQL Playground.

### Schema builder API

For more control, you can define your schema explicitly using the `GraphQLContext` builder API:

```csharp
public class BlogContext : GraphQLContext
{
    protected internal override void OnSchemaCreating(SchemaBuilder schema)
    {
        schema.Object("Post")
            .Field("id", "Int!")
            .Field("title", "String")
            .Field("author", "String")
            .Field("content", "String");

        schema.Object("Query")
            .Field("posts", "[Post]")
            .Field("post", "Post");
    }
}
```

## Features

- **Code-first schema definition** -- define your GraphQL schema using C# classes with convention-based discovery, or use the explicit schema builder API
- **Full type system** -- objects, interfaces, unions, enums, input types, and custom scalars
- **Query execution engine** -- parsing, validation, and execution of GraphQL queries
- **ASP.NET Core integration** -- middleware, endpoint routing, and dependency injection
- **Built-in GraphQL Playground** -- interactive GraphQL IDE served directly from your app
- **GraphQL client** -- `GraphQLClient` for consuming GraphQL APIs over HTTP

## Packages

| Package | NuGet |
| --- | :---: |
| **Server** | |
| [GraphZen.AspNetCore.Server](https://www.nuget.org/packages/GraphZen.AspNetCore.Server) | [![NuGet](https://img.shields.io/nuget/v/GraphZen.AspNetCore.Server.svg)](https://www.nuget.org/packages/GraphZen.AspNetCore.Server) |
| **SDK** | |
| [GraphZen.Abstractions](https://www.nuget.org/packages/GraphZen.Abstractions) | [![NuGet](https://img.shields.io/nuget/v/GraphZen.Abstractions.svg)](https://www.nuget.org/packages/GraphZen.Abstractions) |
| [GraphZen.LanguageModel](https://www.nuget.org/packages/GraphZen.LanguageModel) | [![NuGet](https://img.shields.io/nuget/v/GraphZen.LanguageModel.svg)](https://www.nuget.org/packages/GraphZen.LanguageModel) |
| [GraphZen.TypeSystem](https://www.nuget.org/packages/GraphZen.TypeSystem) | [![NuGet](https://img.shields.io/nuget/v/GraphZen.TypeSystem.svg)](https://www.nuget.org/packages/GraphZen.TypeSystem) |
| [GraphZen.QueryEngine](https://www.nuget.org/packages/GraphZen.QueryEngine) | [![NuGet](https://img.shields.io/nuget/v/GraphZen.QueryEngine.svg)](https://www.nuget.org/packages/GraphZen.QueryEngine) |

## Building from source

```bash
git clone https://github.com/GraphZen/graphzen-dotnet.git
cd graphzen-dotnet
dotnet build
dotnet test
```

## License

GraphZen is licensed under the [GraphZen Community License](https://github.com/GraphZen/graphzen-dotnet/blob/master/LICENSE) (AGPLv3 with Commons Clause). Commercial licenses are available at [graphzen.com](https://graphzen.com).

Copyright 2017-2026 GraphZen LLC. "GraphZen" is a registered trademark of GraphZen LLC.
