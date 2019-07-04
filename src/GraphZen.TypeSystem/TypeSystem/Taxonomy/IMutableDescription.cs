// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableDescription : IDescription
    {
        bool SetDescription(string description, ConfigurationSource configurationSource);
        ConfigurationSource? GetDescriptionConfigurationSource();
    }
}