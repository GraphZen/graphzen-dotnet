using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ISchemaDefinition : IDescription,
        IDirectivesContainerDefinition, IObjectTypesContainerDefinition, IInterfaceTypesContainerDefinition,
        IUnionTypesContainerDefinition, IScalarTypesContainerDefinition, IEnumTypesContainerDefinition,
        IInputObjectTypesContainerDefinition
    {
    }
}