// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
    public class SpecSubject
    {
        public SpecSubject(string name, ImmutableHashSet<string>? specs = null,
            ImmutableList<SpecSubject>? children = null)
        {
            Name = name;
            Specs = specs ?? ImmutableHashSet<string>.Empty;
            Children = children ?? ImmutableList<SpecSubject>.Empty;
        }

        public string Name { get; }
        public ImmutableHashSet<string> Specs { get; }
        public ImmutableList<SpecSubject> Children { get; }
        public SpecSubject WithName(string name) => new SpecSubject(name, Specs, Children);
        public SpecSubject WithSpecs(params string[] specs) => WithSpecs(specs.AsEnumerable());

        public SpecSubject WithSpecs<T>()
        {
            var type = typeof(T);
            var specs = SpecReflectionHelpers.GetConstFields(type).Select(_ => _.Name);
            return WithSpecs(specs);
        }

        public SpecSubject WithSpecs(IEnumerable<string> specs) => new SpecSubject(Name, Specs.Union(specs), Children);

        public SpecSubject WithChildren(params SpecSubject[] children) =>
            new SpecSubject(Name, Specs, Children.AddRange(children));

        public SpecSubject WithChild(SpecSubject child) => WithChildren(child);
    }
}