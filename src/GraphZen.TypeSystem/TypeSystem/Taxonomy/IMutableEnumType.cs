// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMutableEnumType :
        IBuildableEnumType,
        IMutableNamedTypeDefinition
    {
        new IEnumTypeBuilder Builder { get; }
        ConfigurationSource? FindIgnoredValueConfigurationSource(string name);
        bool RemoveValue(IEnumValue value);
        bool IgnoreValue(string name, ConfigurationSource configurationSource);
        bool UnignoreValue(string name, ConfigurationSource configurationSource);
        IEnumValue AddValue(string name, ConfigurationSource configurationSource,
            ConfigurationSource nameConfigurationSource);
    }
}