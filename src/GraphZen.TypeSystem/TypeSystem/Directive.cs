// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class Directive : IDirective
    {
        private readonly MutableDirectiveDefinition _directive;

        public Directive(MutableDirectiveDefinition directive, object? value)
        {
            _directive = directive;
            Value = value;
        }

        public string Name => _directive.Name;
        public object? Value { get; }
        public ISchema Schema => throw new NotImplementedException();
        public IParentMember Parent => throw new NotImplementedException();
    }
}