using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public class ClassWithAnnotatedMember : InterfaceWithAnnotatedMember
    {
        public Dictionary<string, int> Numbers { get; } = new Dictionary<string, int>();
    }
}