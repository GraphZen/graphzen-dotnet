#nullable disable
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ISchema : ISchemaDefinition,

        IDirectivesContainer, IObjectTypesContainer, IInterfaceTypesContainer, IUnionTypesContainer, IScalarTypesContainer, IEnumTypesContainer, IInputObjectTypesContainer
    {

    }
}