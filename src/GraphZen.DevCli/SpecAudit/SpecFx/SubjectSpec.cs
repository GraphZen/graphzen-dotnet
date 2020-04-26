// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
    public class SubjectSpec
    {
        public SubjectSpec(string specId, SpecPriority? priority)
        {
            SpecId = specId;
            Priority = priority ?? SpecPriority.Low;
        }

        public string SpecId { get; }
        public SpecPriority Priority { get; }

        public SubjectSpec WithPriority(SpecPriority priority) => new SubjectSpec(SpecId, priority);
    }
}