// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
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
        public string Path => Parent != null ? $"{Parent.Path}.{Name.Replace(' ', '_')}" : Name.Replace(' ', '_');




        public IEnumerable<Subject> GetSelfAndAncestors()
        {
            if (Parent != null)
            {
                foreach (var grandParent in Parent.GetSelfAndAncestors())
                {
                    yield return grandParent;
                }
            }

            yield return this;
        }



        public ImmutableDictionary<string, SubjectSpec> Specs { get; }
        public ImmutableList<Subject> Children { get; }

        public Subject? Parent { get; }

        public IEnumerable<(string path, string specId, SpecPriority priority, SpecCoverageStatus status)>
            GetCoverage(SpecSuite suite) =>
            Specs.Values.Select(_ =>
            {
                var result = (Path, _.SpecId, _.Priority, GetCoverageStatus(_.SpecId, suite));

                return result;
            });


        public SpecCoverageStatus GetCoverageStatus(string specId, SpecSuite suite)
        {
            var tests = suite.Tests.Where(_ => _.SpecId == specId && _.SubjectPath == Path)
                .ToImmutableList();
            var nonSkippedTests = tests.Where(_ => _.SkipReason == null).ToImmutableList();
            var skippedTests = tests.Where(_ => _.SkipReason == null).ToImmutableList();
            if (nonSkippedTests.Any())
            {
                return SpecCoverageStatus.Implemented;
            }

            return skippedTests.Any() ? SpecCoverageStatus.Skipped : SpecCoverageStatus.Missing;
        }

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

        public Subject WithSpecPriority(SpecPriority priority, bool deep = false)
        {
            var sb = Specs.ToBuilder();
            foreach (var (specId, _) in Specs)
            {
                sb[specId] = new SubjectSpec(specId, priority);
            }

            var children = deep ? Children.Select(_ => _.WithSpecPriority(priority, true)).ToImmutableList() : Children;

            return new Subject(Name, Parent, sb.ToImmutable(), children);
        }

        private Subject WithSpecs(IEnumerable<string> specs, SpecPriority? priority)
        {
            var sb = Specs.ToBuilder();
            foreach (var specId in specs)
            {
                if (!Specs.ContainsKey(specId) || priority != null)
                {
                    sb[specId] = new SubjectSpec(specId, priority);
                }
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