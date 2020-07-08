// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface ISchema :
        ISchemaDefinition,
        IQueryType,
        IMutationType,
        ISubscriptionType,
        IDirectives,
        INamedTypes,
            IMemberParent
    {
    }

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