// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ISchemaDefinition :
        IDescription,
        IQueryTypeDefinition, IMutationTypeDefinition, ISubscriptionTypeDefinition,
        IDirectivesContainerDefinition,
        IObjectTypesContainerDefinition,
        IInterfaceTypesContainerDefinition,
        IUnionTypesContainerDefinition,
        IScalarTypesContainerDefinition,
        IEnumTypesContainerDefinition,
        IInputObjectTypesContainerDefinition
    {
    }

    [GraphQLIgnore]
    public interface IQueryTypeDefinition
    {
        IObjectTypeDefinition? QueryType { get; }
    }

    public interface IMutableQueryTypeDefinition : IQueryTypeDefinition
    {
        new ObjectTypeDefinition? QueryType { get; }
        bool SetQueryType(ObjectTypeDefinition? type, ConfigurationSource configurationSource);
        ConfigurationSource? GetQueryTypeConfigurationSource();
    }

    [GraphQLIgnore]
    public interface IQueryType : IQueryTypeDefinition
    {
        new ObjectType QueryType { get; }
    }

    [GraphQLIgnore]
    public interface IMutationTypeDefinition
    {
        IObjectTypeDefinition? MutationType { get; }
    }

    public interface IMutableMutationTypeDefinition : IMutationTypeDefinition
    {
        new ObjectTypeDefinition? MutationType { get; }
        bool SetMutationType(ObjectTypeDefinition? type, ConfigurationSource configurationSource);
        ConfigurationSource? GetMutationTypeConfigurationSource();
    }

    [GraphQLIgnore]
    public interface IMutationType : IMutationTypeDefinition
    {
        new ObjectType? MutationType { get; }
    }

    [GraphQLIgnore]
    public interface ISubscriptionTypeDefinition
    {
        IObjectTypeDefinition? SubscriptionType { get; }
    }

    public interface IMutableSubscriptionTypeDefinition : ISubscriptionTypeDefinition
    {
        new ObjectTypeDefinition? SubscriptionType { get; }
        bool SetSubscriptionType(ObjectTypeDefinition? type, ConfigurationSource configurationSource);
        ConfigurationSource? GetSubscriptionTypeConfigurationSource();
    }

    [GraphQLIgnore]
    public interface ISubscriptionType : ISubscriptionTypeDefinition
    {
        new ObjectType? SubscriptionType { get; }
    }
}