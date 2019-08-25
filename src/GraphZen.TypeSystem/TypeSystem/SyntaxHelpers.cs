// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable
namespace GraphZen.TypeSystem
{
    public static class SyntaxHelpers
    {
        public static StringValueSyntax Description(string description)
        {
            if (description != null)
            {
                var node = SyntaxFactory.StringValue(description, true);
                return node;
            }

            return null;
        }


        public static TypeSyntax ToTypeSyntax(this IGraphQLType type)
        {
            Check.NotNull(type, nameof(type));
            switch (type)
            {
                case INamedType namedType:
                    return SyntaxFactory.NamedType(SyntaxFactory.Name(namedType.Name));
                case NonNullType nonNull:
                    return SyntaxFactory.NonNull((NullableTypeSyntax) nonNull.OfType.ToTypeSyntax());
                case ListType list:
                    return SyntaxFactory.ListType(list.OfType.ToTypeSyntax());
            }

            throw new Exception($"Cannot create type syntax for type {type}");
        }


        public static IReadOnlyList<DirectiveSyntax> ToDirectiveNodes(
            this IReadOnlyList<IDirectiveAnnotation> directives)
        {
            Check.NotNull(directives, nameof(directives));

            var annotationConverter = new DefaultIDirectiveAnnotationSyntaxConverter();
            return directives.Select(_ => (DirectiveSyntax) annotationConverter.ToSyntax(_)).ToList().AsReadOnly();
        }
    }
}