using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IBuildableArguments : IArguments
    {
        IArgumentsBuilder Builder { get; }
    }
}