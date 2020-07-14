// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{


    [GraphQLIgnore]
    public interface ISchema : IDescription,
        IDirectives
    {
        IObjectTypeReference? QueryType { get; }

        IObjectTypeReference? MutationType { get; }

        IObjectTypeReference? SubscriptionType { get; }

        [GraphQLIgnore]
        IEnumerable<IDirectiveDefinition> GetDirectiveDefinitions(bool includeSpecDirectives = false);

        [GraphQLIgnore]
        IEnumerable<INamedTypeDefinition> GetTypes(bool includeSpecTypes = false);

        [GraphQLIgnore]
        IEnumerable<IObjectType> GetObjects(bool includeSpecObjects = false);

        [GraphQLIgnore]
        IEnumerable<IInterfaceType> GetInterfaces(bool includeSpecInterfaces = false);

        [GraphQLIgnore]
        IEnumerable<IUnionType> GetUnions(bool includeSpecUnions = false);

        [GraphQLIgnore]
        IEnumerable<IScalarType> GetScalars(bool includeSpecScalars = false);

        [GraphQLIgnore]
        IEnumerable<IEnumType> GetEnums(bool includeSpecEnums = false);

        [GraphQLIgnore]
        IEnumerable<IInputObjectType> GetInputObjects(bool includeSpecInputObjects = false);
    }
}