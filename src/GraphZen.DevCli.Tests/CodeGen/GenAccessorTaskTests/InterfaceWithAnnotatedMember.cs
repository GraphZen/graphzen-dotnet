using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.GenAccessorTaskTests
{
    public interface InterfaceWithAnnotatedMember
    {
        [GenDictionaryAccessors("name", "Number")] Dictionary<string, int> Numbers { get; }
    }
}