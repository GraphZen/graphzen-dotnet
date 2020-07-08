using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IMutableNamedTypesDefinition : INamedTypesDefinition,
        IMutableObjectTypesDefinition,
        IMutableInterfaceTypesDefinition,
        IMutableUnionTypesDefinition,
        IMutableScalarTypesDefinition,
        IMutableEnumTypesDefinition,
        IMutableInputObjectTypesDefinition


    {

        [GraphQLIgnore]
        new IEnumerable<NamedTypeDefinition> GetTypes(bool includeSpecTypes = false);
    }
}