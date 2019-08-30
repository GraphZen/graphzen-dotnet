using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IQueryType : IQueryTypeDefinition
    {
        new ObjectType QueryType { get; }
    }
}