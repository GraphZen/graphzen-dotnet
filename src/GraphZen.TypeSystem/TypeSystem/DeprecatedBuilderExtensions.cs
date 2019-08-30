// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable

namespace GraphZen.TypeSystem
{
    public static class DeprecatedBuilderExtensions
    {
        public static TBuilder Deprecated<TBuilder>(
            this TBuilder builder,
            string reason = null) where TBuilder : IAnnotableBuilder<TBuilder> =>
            Check.NotNull(builder, nameof(builder))
                .DirectiveAnnotation("deprecated", new GraphQLDeprecatedAttribute(reason));
    }
}