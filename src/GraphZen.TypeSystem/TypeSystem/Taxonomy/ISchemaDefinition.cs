using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ISchemaDefinition : IDescription
    {

    }

    [GraphQLIgnore]
    public interface IObjectTypesContainerDefinition
    {
        IEnumerable<IObjectTypeDefinition> GetObjects();
    }
    [GraphQLIgnore]
    public interface IObjectTypesContainer : IObjectTypesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableObjectTypesContainerDefinition : IObjectTypesContainerDefinition { }
}