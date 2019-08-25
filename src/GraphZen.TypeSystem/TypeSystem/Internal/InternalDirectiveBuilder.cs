// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalDirectiveBuilder : MemberDefinitionBuilder<DirectiveDefinition>
    {
        public InternalDirectiveBuilder(DirectiveDefinition definition,
            InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }

        public InternalDirectiveBuilder Locations(DirectiveLocation[] locations)
        {
            Definition.SetLocations(locations);
            return this;
        }


        public InternalInputValueBuilder Argument(string name, ConfigurationSource configurationSource)
        {
            return Definition.GetOrAddArgument(name, configurationSource).Builder;
        }
    }
}