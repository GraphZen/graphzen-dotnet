// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class ScalarTypeDefinition : NamedTypeDefinition, IMutableScalarTypeDefinition
    {
        public ScalarTypeDefinition(
            ScalarType source,
            TypeIdentity identity,
            SchemaDefinition schema,
            ConfigurationSource configurationSource) : this(identity, schema, configurationSource)
        {
            Check.NotNull(source, nameof(source));
            Source = source;
        }

        public ScalarTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            Check.NotNull(identity, nameof(identity)), Check.NotNull(schema, nameof(schema)), configurationSource
        )
        {
            Builder = new InternalScalarTypeBuilder(this, schema.Builder);

            identity.Definition = Source != null ? (INamedTypeDefinition) Source : this;
        }

        private string DebuggerDisplay => $"scalar {Name}";


        public InternalScalarTypeBuilder Builder { get; }
        public ScalarType? Source { get; }
        public LeafSerializer<object>? Serializer { get; set; }

        public bool SetSerializer(LeafSerializer<object>? serializer, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public ConfigurationSource? GetSerializerConfigurationSource() => throw new NotImplementedException();

        public LeafLiteralParser<object, ValueSyntax>? LiteralParser { get; set; }
        public ConfigurationSource? GetLiteralParserConfigurationSource() => throw new NotImplementedException();

        public bool SetLiteralParser(LeafLiteralParser<object, ValueSyntax>? literalParser,
            ConfigurationSource configurationSource) => throw new NotImplementedException();

        public LeafValueParser<object>? ValueParser { get; set; }
        public ConfigurationSource? GetValueParserConfigurationSource() => throw new NotImplementedException();

        public bool SetValueParser(LeafValueParser<object>? valueParser, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Scalar;
        public override TypeKind Kind { get; } = TypeKind.Scalar;
    }
}