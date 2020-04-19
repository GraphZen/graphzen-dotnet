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
        public SpecSubject(string name) : this(name, null, ImmutableHashSet<string>.Empty,
            ImmutableList<SpecSubject>.Empty)
        {
        }

        private SpecSubject(string name, SpecSubject? parent, ImmutableHashSet<string> specs,
            ImmutableList<SpecSubject> children)
        {
            Name = name;
            Parent = parent;
            Specs = specs ?? ImmutableHashSet<string>.Empty;
            Children = children.Select(_ => _.WithParent(this)).ToImmutableList() ?? ImmutableList<SpecSubject>.Empty;
        }

        public string Name { get; }
        public string Path => Parent != null ? $"{Parent.Path}_{Name}" : Name;
        public ImmutableHashSet<string> Specs { get; }
        public ImmutableList<SpecSubject> Children { get; }

        public IEnumerable<SpecSubject> GetSelfAndDescendants()
        {
            yield return this;
            foreach (var s in Children.SelectMany(_ => _.GetSelfAndDescendants()))
            {
                yield return s;
            }
        }

        public SpecSubject? Parent { get; }
        private SpecSubject WithParent(SpecSubject parent) => new SpecSubject(Name, parent, Specs, Children);
        public SpecSubject WithName(string name) => new SpecSubject(name, Parent, Specs, Children);
        public SpecSubject WithSpecs(params string[] specs) => WithSpecs(specs.AsEnumerable());

        public SpecSubject WithSpecs<T>()
        {
            var type = typeof(T);
            var specs = SpecReflectionHelpers.GetConstFields(type).Select(_ => _.Name);
            return WithSpecs(specs);
        }

        public SpecSubject WithSpecs(IEnumerable<string> specs) =>
            new SpecSubject(Name, Parent, Specs.Union(specs), Children);

        public SpecSubject WithChildren(params SpecSubject[] children) =>
            new SpecSubject(Name, Parent, Specs, Children.AddRange(children));

        public SpecSubject WithChild(SpecSubject child) => WithChildren(child);
    }
}