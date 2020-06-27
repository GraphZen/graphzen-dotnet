using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableMemberDefinition : IMemberDefinition
    {
        new SchemaDefinition Schema { get; }
        MemberDefinitionBuilder Builder { get; }
    }
}