// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Language;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalScalarTypeBuilder : AnnotatableMemberDefinitionBuilder<ScalarTypeDefinition>
    {
        public InternalScalarTypeBuilder([NotNull] ScalarTypeDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(definition, schemaBuilder)
        {
        }


        [NotNull]
        public InternalScalarTypeBuilder Serializer(LeafSerializer<object> serializer)
        {
            Definition.Serializer = serializer;
            return this;
        }

        [NotNull]
        public InternalScalarTypeBuilder ValueParser(LeafValueParser<object> valueParser)
        {
            Definition.ValueParser = valueParser;
            return this;
        }

        [NotNull]
        public InternalScalarTypeBuilder LiteralParser([NotNull] LeafLiteralParser<object, ValueSyntax> literalParser)
        {
            Definition.LiteralParser = literalParser;
            return this;
        }
    }
}