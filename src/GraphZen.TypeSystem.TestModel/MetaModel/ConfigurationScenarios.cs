// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.MetaModel
{
    public class ConfigurationScenarios
    {
        public IReadOnlyList<ConfigurationSource> Define { get; set; } = ImmutableArray.Create<ConfigurationSource>();
        public IReadOnlyList<ConfigurationSource> Ignore { get; set; } = ImmutableArray.Create<ConfigurationSource>();
        public IReadOnlyList<ConfigurationSource> Remove { get; set; } = ImmutableArray.Create<ConfigurationSource>();
    }
}