// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public class EnumType : NamedType, IEnumType
    {
        [NotNull] [ItemNotNull] private readonly Lazy<EnumTypeDefinitionSyntax> _syntax;


        public EnumType(string name, string description, Type clrType,
            IEnumerable<IEnumValueDefinition> valueDefinitions,
            IReadOnlyList<IDirectiveAnnotation> directives) : base(
            Check.NotNull(name, nameof(name)), description, clrType, Check.NotNull(directives, nameof(directives)))
        {
            Check.NotNull(valueDefinitions, nameof(valueDefinitions));
            Values = valueDefinitions.Select(v => EnumValue.From(v, this)).ToReadOnlyList();
            ValuesByName = Values.ToReadOnlyDictionary(v =>
            {
                Debug.Assert(v != null, nameof(v) + " != null");
                return v.Name;
            }, v => v);
            ValuesByValue = Values
                .Where(v =>
                {
                    Debug.Assert(v != null, nameof(v) + " != null");
                    return v.Value != null;
                })
                .ToReadOnlyDictionary(v =>
                {
                    Debug.Assert(v != null, nameof(v) + " != null");
                    return v.Value;
                }, v => v);
            _syntax = new Lazy<EnumTypeDefinitionSyntax>(() =>
            {
                var syntax = new EnumTypeDefinitionSyntax(SyntaxFactory.Name(Name),
                    SyntaxHelpers.Description(Description), null,
                    GetValues().ToSyntaxNodes<EnumValueDefinitionSyntax>());
                return syntax;
            });
        }


        public Maybe<object> Serialize(object value)
        {
            if (ValuesByValue.TryGetValue(value ?? DBNull.Value, out var enumValue))
            {
                return Maybe.Some<object>(enumValue.Name);
            }

            return Maybe.None<object>(
                $"{Name} Enum: unable to find enum value that matches resolved value \"{value}\"");
        }

        public bool IsValidValue(string value) => ParseValue(value) is Some<object>;

        public bool IsValidLiteral(ValueSyntax value) =>
            value is EnumValueSyntax enumNode && ValuesByName.ContainsKey(enumNode.Value);


        public Maybe<object> ParseValue(object value)
        {
            if (value is string str)
            {
                var enumValue = this.FindValue(str);
                if (enumValue != null)
                {
                    return Maybe.Some(enumValue.Value);
                }
            }

            return Maybe.None<object>();
        }

        public Maybe<object> ParseLiteral(ValueSyntax value)
        {
            if (value is EnumValueSyntax enumNode)
            {
                var enumValue = this.FindValue(enumNode.Value);
                if (enumValue != null)
                {
                    return Maybe.Some(enumValue.Value);
                }
            }

            return Maybe.None<object>();
        }


        public override TypeKind Kind { get; } = TypeKind.Enum;
        IEnumerable<IEnumValueDefinition> IEnumTypeDefinition.GetValues() => Values;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Enum;

        public IReadOnlyList<EnumValue> Values { get; }
        public IReadOnlyDictionary<string, EnumValue> ValuesByName { get; }
        public IReadOnlyDictionary<object, EnumValue> ValuesByValue { get; }

        public IEnumerable<EnumValue> GetValues() => Values;

        [NotNull]
        [GraphQLIgnore]
        public static EnumType From(IEnumTypeDefinition definition)
        {
            Check.NotNull(definition, nameof(definition));
            return new EnumType(definition.Name, definition.Description, definition.ClrType, definition.GetValues(),
                definition.DirectiveAnnotations);
        }
    }
}