// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Types.Builders;
using GraphZen.Types.Internal;
using GraphZen.Utilities;
using JetBrains.Annotations;
using static GraphZen.Language.SyntaxFactory;


namespace GraphZen.Types
{
    public class ScalarType : NamedType, IScalarType
    {
        [CanBeNull] private readonly LeafLiteralParser<object, ValueSyntax> _literalParser;
        [CanBeNull] private readonly LeafSerializer<object> _serializer;

        [NotNull] [ItemNotNull] private readonly Lazy<SyntaxNode> _syntax;

        [CanBeNull] private readonly LeafValueParser<object> _valueParser;

        public ScalarType(
            string name,
            string description,
            Type clrType,
            LeafValueParser<object> valueParser,
            LeafLiteralParser<object, ValueSyntax> literalParser,
            LeafSerializer<object> serializer,
            IReadOnlyList<IDirectiveAnnotation> directives
        ) : base(Check.NotNull(name, nameof(name)), description, clrType, Check.NotNull(directives, nameof(directives)))

        {
            _valueParser = valueParser;
            _serializer = serializer;
            _literalParser = literalParser;
            _syntax = new Lazy<SyntaxNode>(() =>
                new ScalarTypeDefinitionSyntax(Name(Name),
                    Description != null ? StringValue(Description, true) : null));
        }

        [NotNull]
        public LeafLiteralParser<object, ValueSyntax> LiteralParser =>
            _literalParser ??
            throw new Exception($"Scalar {Name} does not have a {nameof(LiteralParser)} not defined.");

        [NotNull]
        public LeafSerializer<object> Serializer => _serializer ??
                                                    throw new Exception(
                                                        $"Scalar {Name} does not have a {nameof(Serializer)} not defined.");

        [NotNull]
        public LeafValueParser<object> ValueParser => _valueParser ??
                                                      throw new Exception(
                                                          $"Scalar {Name} does not have a {nameof(ValueParser)} not defined.");

        public Maybe<object> Serialize(object value) => Serializer(value) ?? throw new InvalidOperationException();
        public bool IsValidValue(string value) => ParseValue(value).HasValue;
        public bool IsValidLiteral(ValueSyntax value) => ParseLiteral(value).HasValue;
        public Maybe<object> ParseValue(object value) => ValueParser(value) ?? throw new InvalidOperationException();

        public Maybe<object> ParseLiteral(ValueSyntax value)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return LiteralParser(value) ?? throw new InvalidOperationException();
        }

        public override TypeKind Kind { get; } = TypeKind.Scalar;
        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Scalar;


        [NotNull]
        public static ScalarType From(IScalarTypeDefinition definition)
        {
            Check.NotNull(definition, nameof(definition));
            if (definition is ScalarTypeDefinition scalarDef && scalarDef.Source != null)
            {
                return scalarDef.Source;
            }

            return new ScalarType(
                definition.Name,
                definition.Description,
                definition.ClrType,
                definition.ValueParser,
                definition.LiteralParser,
                definition.Serializer,
                definition.DirectiveAnnotations
            );
        }


        [NotNull]
        public static ScalarType Create(string name,
            Action<ScalarTypeBuilder<object, ValueSyntax>> scalarTypeConfigurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(scalarTypeConfigurator, nameof(scalarTypeConfigurator));
            var schemaDef = new SchemaDefinition(Array.Empty<ScalarType>());
            var definition = schemaDef.GetOrAddScalar(name, ConfigurationSource.Explicit);
            var builder = new ScalarTypeBuilder<object, ValueSyntax>(definition?.Builder);
            scalarTypeConfigurator(builder);
            return From(definition);
        }

        [NotNull]
        public static ScalarType Create<TScalar>(Action<ScalarTypeBuilder<TScalar, ValueSyntax>> scalarTypeConfigurator)
        {
            Check.NotNull(scalarTypeConfigurator, nameof(scalarTypeConfigurator));
            var schemaDef = new SchemaDefinition(Array.Empty<ScalarType>());
            var definition = schemaDef.GetOrAddScalar(typeof(TScalar), ConfigurationSource.Explicit);
            var builder = new ScalarTypeBuilder<TScalar, ValueSyntax>(definition?.Builder);
            scalarTypeConfigurator(builder);
            return From(definition);
        }
    }
}