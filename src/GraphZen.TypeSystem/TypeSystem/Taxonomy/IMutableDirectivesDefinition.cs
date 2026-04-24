// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem.Taxonomy;

[GraphQLIgnore]
public interface IMutableDirectivesDefinition : IDirectivesDefinition
{
    [GraphQLIgnore]
    new IEnumerable<DirectiveDefinition> GetDirectives();

    bool RenameDirective(DirectiveDefinition directive, string name, ConfigurationSource configurationSource);


    ConfigurationSource? FindIgnoredDirectiveConfigurationSource(string name);
}
