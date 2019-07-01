// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types.Builders.Internal
{
    public class InternalDirectiveBuilder : MemberDefinitionBuilder<DirectiveDefinition>
    {
        public InternalDirectiveBuilder([NotNull] DirectiveDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }

        public InternalDirectiveBuilder Locations(DirectiveLocation[] locations)
        {
            Definition.SetLocations(locations);
            return this;
        }


        [NotNull]
        public InternalInputValueBuilder Argument([NotNull] string name, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, configurationSource).Builder;
    }
}