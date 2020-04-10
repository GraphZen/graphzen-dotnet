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
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.TypeSystem
{
    public class EnumType : NamedType, IEnumType
    {
        private readonly Lazy<EnumTypeDefinitionSyntax> _syntax;

        public EnumType(string name,
            string? description,
            Type? clrType,
            IEnumerable<IEnumValueDefinition> valueDefinitions,
            IReadOnlyList<IDirectiveAnnotation> directives)
            : base(name, description, clrType, directives)
        {
            var values = valueDefinitions.Select(v => EnumValue.From(v, this)).ToImmutableList();
            Values = values.ToReadOnlyDictionary(v => v.Name);
            ValuesByValue = values.Where(_ => _.Value != null).ToReadOnlyDictionary(v => v.Value);
            _syntax = new Lazy<EnumTypeDefinitionSyntax>(() =>
            {
                var syntax = new EnumTypeDefinitionSyntax(Name(Name),
                    SyntaxHelpers.Description(Description), null,
                    GetValues().ToSyntaxNodes<EnumValueDefinitionSyntax>());
                return syntax;
            });
        }


        public Maybe<object> Serialize(object value)
        {
            if (ValuesByValue.TryGetValue(value ?? DBNull.Value, out var enumValue))
                return Maybe.Some<object>(enumValue.Name);

            return Maybe.None<object>(
                $"{Name} Enum: unable to find enum value that matches resolved value \"{value}\"");
        }

        public bool IsValidValue(string value) => ParseValue(value) is Some<object>;

        public bool IsValidLiteral(ValueSyntax value) =>
            value is EnumValueSyntax enumNode && Values.ContainsKey(enumNode.Value);


        public Maybe<object> ParseValue(object value)
        {
            if (value is string str)
            {
                var enumValue = this.FindValue(str);
                if (enumValue != null) return Maybe.Some(enumValue.Value);
            }

            return Maybe.None<object>();
        }

        public Maybe<object> ParseLiteral(ValueSyntax value)
        {
            if (value is EnumValueSyntax enumNode)
            {
                var enumValue = this.FindValue(enumNode.Value);
                if (enumValue != null) return Maybe.Some(enumValue.Value);
            }

            return Maybe.None<object>();
        }


        public override TypeKind Kind { get; } = TypeKind.Enum;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Enum;

        [GenDictionaryAccessors("Value")] public IReadOnlyDictionary<string, EnumValue> Values { get; }
        public IReadOnlyDictionary<object, EnumValue> ValuesByValue { get; }

        public IEnumerable<EnumValue> GetValues() => Values.Values;

        IEnumerable<IEnumValueDefinition> IEnumValuesDefinition.GetValues() => GetValues();


        [GraphQLIgnore]
        public static EnumType From(IEnumTypeDefinition definition)
        {
            Check.NotNull(definition, nameof(definition));
            return new EnumType(definition.Name,
                definition.Description,
                definition.ClrType,
                definition.GetValues(),
                definition.GetDirectiveAnnotations().ToList());
        }
    }
}