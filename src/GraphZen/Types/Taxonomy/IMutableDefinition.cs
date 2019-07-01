// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [GraphQLIgnore]
    public interface IMutableDefinition : IMemberDefinition, IMutableDescription
    {
        ConfigurationSource GetConfigurationSource();
    }
}