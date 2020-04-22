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
            Priority = priority ?? SpecPriority.Medium;
        }

        public string SpecId { get; }
        public SpecPriority Priority { get; }

        public SubjectSpec WithPriority(SpecPriority priority) => new SubjectSpec(SpecId, priority);
    }
}