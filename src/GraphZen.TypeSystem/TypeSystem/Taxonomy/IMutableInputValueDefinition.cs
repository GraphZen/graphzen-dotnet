// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableInputValueDefinition : IInputValueDefinition, IMutableAnnotatableDefinition, IMutableNamed,
        IMutableDescription
    {
        new IGraphQLTypeReference InputType { get; set; }

        bool SetDefaultValue(object value, ConfigurationSource configurationSource);
        bool RemoveDefaultValue(ConfigurationSource configurationSource);
        ConfigurationSource? GetDefaultValueConfigurationSource();
    }
}