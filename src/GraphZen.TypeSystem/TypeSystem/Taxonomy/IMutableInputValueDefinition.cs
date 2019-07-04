// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;


namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IMutableInputValueDefinition : IInputValueDefinition, IMutableAnnotatableDefinition, IMutableNamed
    {
        [CanBeNull]
        new IGraphQLTypeReference InputType { get; set; }

        bool SetDefaultValue(object value, ConfigurationSource configurationSource);
        bool RemoveDefaultValue(ConfigurationSource configurationSource);
        ConfigurationSource? GetDefaultValueConfigurationSource();
    }
}