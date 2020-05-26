// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    internal static class SpecDirectives
    {
        public static SchemaBuilder<TContext> AddSpecDirectives<TContext>(this SchemaBuilder<TContext> schemaBuilder)
            where TContext : GraphQLContext
        {
            schemaBuilder.Directive("deprecated")
                .Description("Marks an element of a GraphQL schema as no longer supported.")
                .Locations(DirectiveLocation.EnumValue, DirectiveLocation.FieldDefinition)
                .Argument("reason", "String", reason =>
                {
                    reason.Description("Explains why this element was deprecated, usually also including a " +
                                       "suggestion for how to access supported similar data. Formatted " +
                                       "in [Markdown](https://daringfireball.net/projects/markdown/).")
                        .DefaultValue("No longer supported");
                });

            schemaBuilder.Directive("skip")
                .Description(
                    "'Directs the executor to skip this field or fragment only when the `if` argument is true.")
                .Locations(DirectiveLocation.Field, DirectiveLocation.FragmentSpread, DirectiveLocation.InlineFragment)
                .Argument<bool>("if", _ => _.Description("Skipped when true."));

            schemaBuilder.Directive("include")
                .Description(
                    "Directs the executor to include this field or fragment only when the `if` argument is true.")
                .Locations(DirectiveLocation.Field, DirectiveLocation.FragmentSpread, DirectiveLocation.InlineFragment)
                .Argument<bool>("if", _ => _.Description("Included when true."));

            return schemaBuilder;
        }
    }
}