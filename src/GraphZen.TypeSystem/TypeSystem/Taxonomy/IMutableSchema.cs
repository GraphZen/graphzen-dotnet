// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IMutableSchema : IBuildableSchema, IMutableDescription
    {
        new ISchemaBuilder Builder { get; }
        // ReSharper disable once PossibleInterfaceMemberAmbiguity
        bool RenameDirective(MutableDirectiveDefinition directive, string name, ConfigurationSource configurationSource);
        ConfigurationSource? FindIgnoredDirectiveConfigurationSource(string name);
        bool SetQueryType(IObjectType type, ConfigurationSource configurationSource);
        bool RemoveQueryType(ConfigurationSource configurationSource);
        ConfigurationSource? GetQueryTypeConfigurationSource();
        bool SetMutationType(IObjectType type, ConfigurationSource configurationSource);
        bool RemoveMutationType(ConfigurationSource configurationSource);
        ConfigurationSource? GetMutationTypeConfigurationSource();
        bool SetSubscriptionType(IObjectType type, ConfigurationSource configurationSource);
        bool RemoveSubscriptionType(ConfigurationSource configurationSource);
        ConfigurationSource? GetSubscriptionTypeConfigurationSource();
        bool RenameDirectiveDefinition(MutableDirectiveDefinition directive, string name, ConfigurationSource configurationSource);

        ConfigurationSource? FindIgnoredDirectiveDefinitionConfigurationSource(string name);
    }
}