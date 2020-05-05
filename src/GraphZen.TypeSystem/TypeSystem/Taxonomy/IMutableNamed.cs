// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableNamed : INamed, IMutableDefinition
    {
        bool SetName(string name, ConfigurationSource configurationSource);
        ConfigurationSource GetNameConfigurationSource();
    }

    public interface IMutableRemovableNamed : IMutableNamed
    {
        bool RemoveName(ConfigurationSource configurationSource);
    }
}