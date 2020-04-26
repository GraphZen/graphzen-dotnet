// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class SubjectAttribute : Attribute
    {
        public SubjectAttribute(string subject, params string[] subjects)
        {
            Subjects = ImmutableList.Create(subject).AddRange(subjects).Reverse();
        }

        public IReadOnlyList<string> Subjects { get; }
    }
}