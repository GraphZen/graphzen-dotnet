using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
    public enum SpecCoverageStatus
    {
        Implemented,
        Missing,
        NotNeeded,
        Skipped
    }
}