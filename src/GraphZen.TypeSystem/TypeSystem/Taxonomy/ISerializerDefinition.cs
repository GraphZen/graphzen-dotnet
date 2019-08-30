using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface ISerializerDefinition
    {
        LeafSerializer<object>? Serializer { get; }
    }
}