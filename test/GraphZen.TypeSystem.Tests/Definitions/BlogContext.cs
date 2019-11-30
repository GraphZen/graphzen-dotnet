// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen
{
    public class BlogContext : GraphQLContext
    {
        protected internal override void OnSchemaCreating(SchemaBuilder schema)
        {
            schema.Object("Image")
                .Field("url", "String")
                .Field("width", "Int")
                .Field("height", "Int");

            schema.Object("Author")
                .Field("id", "String")
                .Field("name", "String")
                .Field("pic", "Image", field =>
                {
                    // field.Argument("width").Type("Int");
                    // field.Argument("height").Type("Int");
                })
                .Field("recentArticle", "Article");

            schema.Object("Article")
                .Field("id", "String")
                .Field("isPublished", "Boolean")
                .Field("author", "Author")
                .Field("title", "String")
                .Field("body", "String");

            schema.Object("Query")
                .Field("article", "Article", _ =>
                {
                    // .Argument("id").Type("String");
                })
                .Field("feed", "[Article]");

            schema.Object("Mutation")
                .Field("writeArticle", "Article");

            schema.Object("Subscription")
                .Field("articleSubscribe", "Article", _ =>
                {
                    // .Argument("id").Type("String");
                });
        }
    }
}