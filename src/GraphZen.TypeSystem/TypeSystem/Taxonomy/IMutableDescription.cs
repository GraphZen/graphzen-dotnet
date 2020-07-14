// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IMutableDescription : IBuildableDescription
    {
        bool SetDescription(string description, ConfigurationSource configurationSource);
        bool RemoveDescription(ConfigurationSource configurationSource);
        ConfigurationSource GetDescriptionConfigurationSource();
    }
}