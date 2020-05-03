// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
    public class SpecSuite
    {
        public SpecSuite(string name, Subject rootSubject, Type specsType)
        {
            var specs = Spec.GetSpecs(specsType);
            Name = name;
            RootSpecs = specs.ToImmutableList();
            RootSubject = rootSubject;
            TestAssembly = specsType.Assembly;
            Subjects = rootSubject.GetSelfAndDescendants().ToImmutableList();
            SubjectsByPath = Subjects.ToImmutableDictionary(_ => _.Path);
            Tests = SpecTest.DiscoverFrom(TestAssembly).ToImmutableList();

            var allSpecs = RootSpecs.SelectMany(_ => _.GetSelfAndDescendants()).ToImmutableList();


            var duplicateSpecs = allSpecs.GroupBy(_ => _.Name).FirstOrDefault(_ => _.Count() > 1)?.ToImmutableList() ??
                                 ImmutableList<Spec>.Empty;
            if (duplicateSpecs.Any())
            {
                var dups = string.Join(",\n", duplicateSpecs.Select(_ => "- " + _));
                throw new ArgumentException($"Specs must have unique names. Duplicate specs:\n\n{dups}\n");
            }


            Specs = allSpecs.ToDictionary(_ => _.Name);
        }


        public string Name { get; }
        public IReadOnlyList<Spec> RootSpecs { get; }
        public IReadOnlyDictionary<string, Spec> Specs { get; }
        public IReadOnlyList<SpecTest> Tests { get; }
        public IReadOnlyList<Subject> Subjects { get; }
        public IReadOnlyDictionary<string, Subject> SubjectsByPath { get; }
        public Subject RootSubject { get; }
        public Assembly TestAssembly { get; }
    }
}