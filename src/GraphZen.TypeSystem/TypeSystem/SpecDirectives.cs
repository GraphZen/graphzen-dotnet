// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

#nullable disable

namespace GraphZen.TypeSystem
{
    public static class SpecDirectives
    {
        private const string DefaultDeprecationReason = "No longer supported";


        public static Directive Deprecated { get; } = new Directive("deprecated",
            "Marks an element of a GraphQL schema as no longer supported.",
            new[]
            {
                DirectiveLocation.EnumValue,
                DirectiveLocation.FieldDefinition
            },
            new[]
            {
                new Argument("reason", "Explains why this element was deprecated, usually also including a " +
                                       "suggestion for how to access supported similar data. Formatted " +
                                       "in [Markdown](https://daringfireball.net/projects/markdown/).",
                    SpecScalars.String,
                    // ReSharper disable once AssignNullToNotNullAttribute
                    DefaultDeprecationReason, true, DirectiveAnnotation.EmptyList, null, null, null)
            }, null
        );


        public static Directive Include { get; } = new Directive("include",
            "Directs the executor to include this field or fragment only when the `if` argument is true.",
            new[] { DirectiveLocation.Field, DirectiveLocation.FragmentSpread, DirectiveLocation.InlineFragment },
            new[]
            {
                new Argument("if", "Included when true.", NonNullType.Of(SpecScalars.Boolean),
                    // ReSharper disable once AssignNullToNotNullAttribute
                    null, false, DirectiveAnnotation.EmptyList, null, null, null)
            }, null
        );


        public static Directive Skip { get; } = new Directive("skip",
            "'Directs the executor to include this field or fragment only when the `if` argument is true.",
            new[] { DirectiveLocation.Field, DirectiveLocation.FragmentSpread, DirectiveLocation.InlineFragment },
            new[]
            {
                new Argument("if", "Skipped when true.", NonNullType.Of(SpecScalars.Boolean),
                    // ReSharper disable once AssignNullToNotNullAttribute
                    null, false, DirectiveAnnotation.EmptyList, null, null, null)
            }, null);


        public static IReadOnlyList<Directive> All { get; } = new List<Directive>
        {
            Deprecated,
            Include,
            Skip
        }.AsReadOnly();
    }
}