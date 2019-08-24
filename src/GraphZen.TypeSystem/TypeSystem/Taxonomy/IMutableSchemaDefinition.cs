using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableSchemaDefinition : 
        ISchemaDefinition, 
        IMutableDescription, 
        IMutableDirectivesContainerDefinition, 
        IMutableObjectTypesContainerDefinition, 
        IMutableInterfaceTypesContainerDefinition, 
        IMutableUnionTypesContainerDefinition, 
        IMutableScalarTypesContainerDefinition, 
        IMutableEnumTypesContainerDefinition,IMutableInputObjectTypesContainerDefinition
    { }
}