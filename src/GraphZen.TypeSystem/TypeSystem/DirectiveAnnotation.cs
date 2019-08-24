// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public class DirectiveAnnotation : IDirectiveAnnotation
    {
        public DirectiveAnnotation(string name, object value)
        {
            Name = Check.NotNull(name, nameof(name));
            Value = value;
        }

        public static IReadOnlyList<IDirectiveAnnotation> EmptyList { get; } =
            new List<IDirectiveAnnotation>(0).AsReadOnly();

        public string Name { get; }
        public object Value { get; }
    }
}