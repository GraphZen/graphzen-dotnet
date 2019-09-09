// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
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

        public InternalDirectiveBuilder Locations(DirectiveLocation[] locations,
            ConfigurationSource configurationSource)
        {
            foreach (var directiveLocation in locations)
            {
                Definition.AddLocation(directiveLocation, configurationSource);
            }

            return this;
        }


        public InternalInputValueBuilder Argument(string name, ConfigurationSource configurationSource) =>
            Definition.GetOrAddArgument(name, configurationSource).Builder;

        public InternalDirectiveBuilder Name(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }

        public void ClrType(Type idClrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }
    }
}