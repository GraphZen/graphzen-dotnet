// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.TypeSystem.Internal
{
    public static class SyntaxHelpers
    {
        public static StringValueSyntax? Description(string? description) =>
            description != null ? StringValue(description, true) : null;


        public static TypeSyntax ToTypeSyntax(this IGraphQLType type)
        {
            Check.NotNull(type, nameof(type));
            switch (type)
            {
                case INamedTypeDefinition namedType:
                    return NamedType(Name(namedType.Name));
                case NonNullType nonNull:
                    return NonNullType((NullableTypeSyntax)nonNull.OfType.ToTypeSyntax());
                case ListType list:
                    return ListType(list.OfType.ToTypeSyntax());
            }

            throw new Exception($"Cannot create type syntax for type {type}");
        }


        public static IReadOnlyList<DirectiveSyntax> ToDirectiveNodes(
            this IEnumerable<IDirective> directives)
        {
            Check.NotNull(directives, nameof(directives));

            var annotationConverter = new DefaultIDirectiveAnnotationSyntaxConverter();
            return directives.Select(_ => (DirectiveSyntax)annotationConverter.ToSyntax(_)).ToImmutableList();
        }
    }
}