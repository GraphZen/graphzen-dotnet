using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface INamedTypes : INamedTypesDefinition,
        IObjectTypes,
        IInterfaceTypes,
        IUnionTypes,
        IScalarTypes,
        IEnumTypes,
        IInputObjectTypes

    {

        new IEnumerable<NamedType> GetTypes(bool includeSpecTypes = false);
    }
}