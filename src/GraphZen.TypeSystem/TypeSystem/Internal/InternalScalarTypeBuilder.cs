// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalScalarTypeBuilder : AnnotatableMemberDefinitionBuilder<ScalarTypeDefinition>
    {
        public InternalScalarTypeBuilder( ScalarTypeDefinition definition,
             InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }


        
        public InternalScalarTypeBuilder Serializer(LeafSerializer<object> serializer)
        {
            Definition.Serializer = serializer;
            return this;
        }

        
        public InternalScalarTypeBuilder ValueParser(LeafValueParser<object> valueParser)
        {
            Definition.ValueParser = valueParser;
            return this;
        }

        
        public InternalScalarTypeBuilder LiteralParser( LeafLiteralParser<object, ValueSyntax> literalParser)
        {
            Definition.LiteralParser = literalParser;
            return this;
        }

        public InternalScalarTypeBuilder ClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (Definition.SetClrType(clrType, configurationSource))
            {
                ConfigureFromClrType();
            }

            return this;
        }

        public bool ConfigureFromClrType()
        {
            var clrType = Definition.ClrType;
            if (clrType == null)
            {
                return false;
            }
            if (clrType.TryGetDescriptionFromDataAnnotation(out var description))
            {
                this.Description(description, ConfigurationSource.DataAnnotation);
            }
            return true;
        }
    }
}