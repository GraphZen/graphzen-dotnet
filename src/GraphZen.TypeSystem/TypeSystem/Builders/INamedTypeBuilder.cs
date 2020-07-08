// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface INamedTypeBuilder :
        INamedBuilder,
        IDescriptionBuilder,
        IClrTypeBuilder,
        IAnnotableBuilder
    {
    }

    public interface INamedTypeBuilder<TBuilder, TUntypedBuilder> :
        INamedBuilder<TBuilder>,
        IDescriptionBuilder<TBuilder>,
        IClrTypeBuilder<TUntypedBuilder>,
        IAnnotableBuilder<TBuilder>
    {
    }
}