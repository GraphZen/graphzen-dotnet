#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableDeprecation : IDeprecation
    {
        bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource);
        bool RemoveDeprecation(ConfigurationSource configurationSource);
    }
}