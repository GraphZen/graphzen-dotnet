using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public interface InterfaceWithAnnotatedMember
    {
        [GenAccessorExtensions("Number")] Dictionary<string, int> Numbers { get; }
    }
}