// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.TypeSystem
{
    public partial class EnumType : NamedTypeDefinition, IEnumType
    {
        private readonly Lazy<EnumTypeDefinitionSyntax> _syntax;

        public EnumType(string name,
            string? description,
            Type? clrType,
            IEnumerable<IEnumValue> valueDefinitions,
            IReadOnlyList<IDirective> directives, Schema schema)
            : base(name, description, clrType, directives, schema)
        {
            Values = valueDefinitions.Select(v => EnumValue.From(v, this)).ToImmutableList();
            ValueMap = Values.ToReadOnlyDictionary(v => v.Name, v => (IEnumValue)v);
            ValuesByValue = Values.Where(_ => _.Value != null).ToReadOnlyDictionary(v => v.Value!);
            _syntax = new Lazy<EnumTypeDefinitionSyntax>(() =>
            {
                var syntax = new EnumTypeDefinitionSyntax(Name(Name),
                    SyntaxHelpers.Description(Description), null,
                    Values.ToSyntaxNodes<EnumValueDefinitionSyntax>());
                return syntax;
            });
        }


        public Maybe<object?> Serialize(object value)
        {
            if (ValuesByValue.TryGetValue(value ?? DBNull.Value, out var enumValue))
            {
                return Maybe.Some<object?>(enumValue.Name);
            }

            return Maybe.None<object?>(
                $"{Name} Enum: unable to find enum value that matches resolved value \"{value}\"");
        }

        public bool IsValidValue(string value) => ParseValue(value) is Some<object>;

        public bool IsValidLiteral(ValueSyntax value) =>
            value is EnumValueSyntax enumNode && ValueMap.ContainsKey(enumNode.Value);


        public Maybe<object?> ParseValue(object value) => value is string str && TryGetValue(str, out var enumValue)
            ? Maybe.Some(enumValue.Value)
            : Maybe.None<object?>();

        public Maybe<object?> ParseLiteral(ValueSyntax value) =>
            value is EnumValueSyntax enumNode && TryGetValue(enumNode.Value, out var enumValue)
                ? Maybe.Some(enumValue.Value)
                : Maybe.None<object?>();


        public override TypeKind Kind { get; } = TypeKind.Enum;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Enum;


        [GenDictionaryAccessors(nameof(EnumValue.Value), nameof(EnumValue.Value))]
        public IReadOnlyDictionary<object, IEnumValue> ValuesByValue { get; }




        [GraphQLIgnore]
        public static EnumType From(IEnumType definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            return new EnumType(definition.Name,
                definition.Description,
                definition.ClrType,
                definition.Values,
                definition.DirectiveAnnotations, schema);
        }

        public IReadOnlyDictionary<string, IEnumValue> ValueMap { get; }
        public IReadOnlyCollection<IEnumValue> Values { get; }
    }
}