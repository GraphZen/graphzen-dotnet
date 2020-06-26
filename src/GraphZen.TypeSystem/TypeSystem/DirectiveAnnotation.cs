// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class DirectiveAnnotation : IDirectiveAnnotation
    {
        private readonly DirectiveDefinition _directive;

        public DirectiveAnnotation(DirectiveDefinition directive, object? value)
        {
            _directive = directive;
            Value = value;
        }

        public string Name => _directive.Name;
        public object? Value { get; }
    }
}