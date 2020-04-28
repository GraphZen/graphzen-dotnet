// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
    public class SpecSuite
    {
        public SpecSuite(string name, Subject rootSubject, IEnumerable<Spec> specs, Assembly testAssembly)
        {
            Name = name;
            Specs = specs.ToImmutableList();
            RootSubject = rootSubject;
            TestAssembly = testAssembly;
            Subjects = rootSubject.GetSelfAndDescendants().ToImmutableList();
            SubjectsByPath = Subjects.ToImmutableDictionary(_ => _.Path);
            Tests = SpecTest.DiscoverFrom(TestAssembly).ToImmutableList();
        }

        public string Name { get; }
        public IReadOnlyList<Spec> Specs { get; }
        public IReadOnlyList<SpecTest> Tests { get; }
        public IReadOnlyList<Subject> Subjects { get; }
        public IReadOnlyDictionary<string, Subject> SubjectsByPath { get; }
        public Subject RootSubject { get; }
        public Assembly TestAssembly { get; }
    }
}