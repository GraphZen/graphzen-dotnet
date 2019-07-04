// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using static GraphZen.Language.SyntaxFactory;


namespace GraphZen.Language
{
    public static class SyntaxHelpers
    {
        [CanBeNull]
        public static StringValueSyntax Description(string description)
        {
            if (description != null)
            {
                var node = StringValue(description, true);
                return node;
            }

            return null;
        }

        [NotNull]
        public static TypeSyntax ToTypeSyntax(this IGraphQLType type)
        {
            Check.NotNull(type, nameof(type));
            switch (type)
            {
                case INamedType namedType:
                    return NamedType(Name(namedType.Name));
                case NonNullType nonNull:
                    return NonNull((NullableTypeSyntax) nonNull.OfType.ToTypeSyntax());
                case ListType list:
                    return ListType(list.OfType.ToTypeSyntax());
            }

            throw new Exception($"Cannot create type syntax for type {type}");
        }

        [NotNull]
        public static IReadOnlyList<DirectiveSyntax> ToDirectiveNodes(
            this IReadOnlyList<IDirectiveAnnotation> directives)
        {
            Check.NotNull(directives, nameof(directives));

            var annotationConverter = new DefaultIDirectiveAnnotationSyntaxConverter();
            return directives.Select(_ => (DirectiveSyntax) annotationConverter.ToSyntax(_)).ToList().AsReadOnly();
        }
    }
}