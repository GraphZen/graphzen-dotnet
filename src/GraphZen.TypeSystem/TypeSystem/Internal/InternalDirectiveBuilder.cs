// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalDirectiveBuilder : MemberDefinitionBuilder<DirectiveDefinition>
    {
        public InternalDirectiveBuilder( DirectiveDefinition definition,
             InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }

        public InternalDirectiveBuilder Locations(DirectiveLocation[] locations)
        {
            Definition.SetLocations(locations);
            return this;
        }


        
        public InternalInputValueBuilder Argument( string name, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, configurationSource).Builder;
    }
}