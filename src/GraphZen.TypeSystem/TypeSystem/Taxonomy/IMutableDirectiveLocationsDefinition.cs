// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableDirectiveLocationsDefinition : IDirectiveLocationsDefinition
    {
        bool AddLocation(DirectiveLocation location, ConfigurationSource configurationSource);
        bool RemoveLocation(DirectiveLocation location, ConfigurationSource configurationSource);
        bool IgnoreLocation(DirectiveLocation location, ConfigurationSource configurationSource);
        bool UnignoreLocation(DirectiveLocation location, ConfigurationSource configurationSource);
        ConfigurationSource? FindDirectiveLocationConfigurationSource(DirectiveLocation location);
        ConfigurationSource? FindIgnoredDirectiveLocationConfigurationSource(DirectiveLocation location);
    }
}