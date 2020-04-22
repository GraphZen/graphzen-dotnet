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
    public enum SpecPriority
    {
        Default,
        Low,
        Medium,
        High
    }


    public class SubjectSpec
    {
        public SubjectSpec(string specId, SpecPriority? priority)
        {
            SpecId = specId;
            Priority = priority ?? SpecPriority.Default;
        }

        public string SpecId { get; }
        public SpecPriority Priority { get; }

        public SubjectSpec WithPriority(SpecPriority priority) => new SubjectSpec(SpecId, priority);
    }

    public class Subject
    {
        public Subject(string name) : this(name, null, ImmutableDictionary<string, SubjectSpec>.Empty,
            ImmutableList<Subject>.Empty)
        {
        }

        private Subject(string name, Subject? parent, ImmutableDictionary<string, SubjectSpec> specs,
            ImmutableList<Subject> children) : this(name, parent, specs.Values, children)
        {
        }


        private Subject(string name, Subject? parent, IEnumerable<SubjectSpec> specs,
            ImmutableList<Subject> children)
        {
            Name = name;
            Parent = parent;
            Specs = specs.ToImmutableDictionary(_ => _.SpecId) ?? ImmutableDictionary<string, SubjectSpec>.Empty;
            Children = children.Select(_ => _.WithParent(this)).ToImmutableList() ?? ImmutableList<Subject>.Empty;
        }

        public string Name { get; }
        public string Path => Parent != null ? $"{Parent.Path}_{Name}" : Name;
        public ImmutableDictionary<string, SubjectSpec> Specs { get; }
        public ImmutableList<Subject> Children { get; }

        public Subject? Parent { get; }

        public IEnumerable<Subject> GetSelfAndDescendants()
        {
            yield return this;
            foreach (var s in Children.SelectMany(_ => _.GetSelfAndDescendants()))
            {
                yield return s;
            }
        }

        private Subject WithParent(Subject parent) => new Subject(Name, parent, Specs, Children);
        public Subject WithName(string name) => new Subject(name, Parent, Specs, Children);
        public Subject WithSpecs(params string[] specs) => WithSpecs(specs, null);
        public Subject WithSpecs(SpecPriority priority, params string[] specs) => WithSpecs(specs, priority);

        public Subject WithSpecPriority(SpecPriority priority)
        {
            var sb = Specs.ToBuilder();
            foreach (var (specId, _) in Specs)
            {
                sb[specId] = new SubjectSpec(specId, priority);
            }

            return new Subject(Name, Parent, sb.ToImmutable(), Children);
        }

        private Subject WithSpecs(IEnumerable<string> specs, SpecPriority? priority)
        {
            var sb = Specs.ToBuilder();
            foreach (var specId in specs)
            {
                if (!Specs.ContainsKey(specId) || priority != null) sb[specId] = new SubjectSpec(specId, priority);
            }

            return new Subject(Name, Parent, sb.ToImmutable(), Children);
        }

        public Subject WithSpecs<T>(SpecPriority? priority = null) =>
            WithSpecs(SpecReflectionHelpers.GetConstFields(typeof(T)).Select(_ => _.Name), priority);

        public Subject WithChildren(params Subject[] children) =>
            new Subject(Name, Parent, Specs, Children.AddRange(children));

        public Subject WithChild(Subject child) => WithChildren(child);
    }
}