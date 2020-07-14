// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class ScalarType : NamedTypeDefinition, IScalarType
    {
        private readonly Lazy<SyntaxNode> _syntax;

        public ScalarType(
            string name,
            string? description,
            Type? clrType,
            LeafValueParser<object?>? valueParser,
            LeafLiteralParser<object?, ValueSyntax>? literalParser,
            LeafSerializer<object?>? serializer,
            IReadOnlyList<IDirective> directives, Schema schema
        ) : base(name, description, clrType, directives, schema)

        {
            ValueParser = valueParser;
            Serializer = serializer;
            LiteralParser = literalParser;
            _syntax = new Lazy<SyntaxNode>(() =>
                new ScalarTypeDefinitionSyntax(SyntaxFactory.Name(Name),
                    Description != null ? SyntaxFactory.StringValue(Description, true) : null));
        }


        public LeafLiteralParser<object?, ValueSyntax>? LiteralParser { get; }


        public LeafSerializer<object?>? Serializer { get; }
        public LeafValueParser<object?>? ValueParser { get; }

        public Maybe<object?> Serialize(object value)
        {
            if (Serializer == null)
            {
                throw new Exception($"Error serializing value '{value}': {this} does not have a serializer defined.");
            }

            return Serializer(value);
        }

        public bool IsValidValue(string value) => ParseValue(value).HasValue;

        public bool IsValidLiteral(ValueSyntax value) => ParseLiteral(value).HasValue;

        public Maybe<object?> ParseValue(object value)
        {
            if (ValueParser == null)
            {
                throw new Exception($"Error parsing value '{value}': {this} does not have a value parser defined.");
            }

            return ValueParser(value);
        }

        public Maybe<object?> ParseLiteral(ValueSyntax value)
        {
            if (LiteralParser == null)
            {
                throw new Exception(
                    $"Error parsing literal value '{value}': {this} does not have a literal parser defined.");
            }

            return LiteralParser(value);
        }

        public override TypeKind Kind { get; } = TypeKind.Scalar;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Scalar;

        public static ScalarType From(IScalarType definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));

            return new ScalarType(
                definition.Name,
                definition.Description,
                definition.ClrType,
                definition.ValueParser,
                definition.LiteralParser,
                definition.Serializer,
                definition.DirectiveAnnotations, schema
            );
        }
    }
}