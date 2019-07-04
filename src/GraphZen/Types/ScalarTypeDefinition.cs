// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ScalarTypeDefinition : NamedTypeDefinition, IMutableScalarTypeDefinition
    {
        public ScalarTypeDefinition(ScalarType source, TypeIdentity identity, SchemaDefinition schema,
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

            identity.Definition = Source != null ? (IGraphQLTypeDefinition) Source : this;
        }

        private string DebuggerDisplay => $"scalar {Name}";


        [NotNull]
        public InternalScalarTypeBuilder Builder { get; }

        [CanBeNull]
        public ScalarType Source { get; }


        public LeafSerializer<object> Serializer { get; set; }

        public LeafLiteralParser<object, ValueSyntax> LiteralParser { get; set; }

        public LeafValueParser<object> ValueParser { get; set; }
        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Scalar;

        public override TypeKind Kind { get; } = TypeKind.Scalar;
    }
}