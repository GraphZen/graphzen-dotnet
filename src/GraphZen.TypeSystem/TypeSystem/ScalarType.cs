// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class ScalarType : NamedType, IScalarType
    {
        private readonly LeafLiteralParser<object, ValueSyntax>? _literalParser;
        private readonly LeafSerializer<object>? _serializer;
        private readonly Lazy<SyntaxNode> _syntax;
        private readonly LeafValueParser<object>? _valueParser;

        public ScalarType(
            string name,
            string? description,
            Type? clrType,
            LeafValueParser<object>? valueParser,
            LeafLiteralParser<object, ValueSyntax>? literalParser,
            LeafSerializer<object>? serializer,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema
        ) : base(name, description, clrType, directives, schema)

        {
            _valueParser = valueParser;
            _serializer = serializer;
            _literalParser = literalParser;
            _syntax = new Lazy<SyntaxNode>(() =>
                new ScalarTypeDefinitionSyntax(SyntaxFactory.Name(Name),
                    Description != null ? SyntaxFactory.StringValue(Description, true) : null));
        }


        public LeafLiteralParser<object, ValueSyntax> LiteralParser =>
            _literalParser ??
            throw new Exception($"Scalar {Name} does not have a {nameof(LiteralParser)} not defined.");


        public LeafSerializer<object> Serializer => _serializer ??
                                                    throw new Exception(
                                                        $"Scalar {Name} does not have a {nameof(Serializer)} not defined.");

        public LeafValueParser<object> ValueParser => _valueParser ??
                                                      throw new Exception(
                                                          $"Scalar {Name} does not have a {nameof(ValueParser)} not defined.");

        public Maybe<object> Serialize(object value) => Serializer(value);

        public bool IsValidValue(string value) => ParseValue(value).HasValue;

        public bool IsValidLiteral(ValueSyntax value) => ParseLiteral(value).HasValue;

        public Maybe<object> ParseValue(object value) => ValueParser(value);

        public Maybe<object> ParseLiteral(ValueSyntax value) => LiteralParser(value);

        public override TypeKind Kind { get; } = TypeKind.Scalar;
        public override IEnumerable<IMember> Children() => throw new NotImplementedException();

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Scalar;

        public static ScalarType From(IScalarTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));

            return new ScalarType(
                definition.Name,
                definition.Description,
                definition.ClrType,
                definition.ValueParser,
                definition.LiteralParser,
                definition.Serializer,
                definition.GetDirectiveAnnotations().ToList(), schema
            );
        }
    }
}