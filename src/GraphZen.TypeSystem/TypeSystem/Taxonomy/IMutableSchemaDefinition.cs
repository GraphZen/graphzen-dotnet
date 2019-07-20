using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableSchemaDefinition : ISchemaDefinition, IMutableDescription { }
}