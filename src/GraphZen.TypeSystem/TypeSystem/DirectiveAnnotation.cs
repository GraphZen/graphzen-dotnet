// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class DirectiveAnnotation : IDirectiveAnnotation
    {
        public DirectiveAnnotation(string name, object? value)
        {
            Name = Check.NotNull(name, nameof(name));
            Value = value;
        }

        public static IReadOnlyList<IDirectiveAnnotation> EmptyList { get; } =
            ImmutableList<IDirectiveAnnotation>.Empty;

        public string Name { get; }
        public object? Value { get; }
    }
}