using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface INamedTypesDefinition :
        IObjectTypesDefinition,
        IInterfaceTypesDefinition,
        IUnionTypesDefinition,
        IScalarTypesDefinition,
        IEnumTypesDefinition,
        IInputObjectTypesDefinition

    {
        [GraphQLIgnore]
        IEnumerable<INamedTypeDefinition> GetTypes(bool includeSpecTypes = false);


    }
}