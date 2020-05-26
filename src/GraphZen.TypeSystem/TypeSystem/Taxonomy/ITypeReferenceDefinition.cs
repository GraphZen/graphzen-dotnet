using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ITypeReferenceDefinition
    {
        IGraphQLTypeReference TypeReference { get; }
    }
}