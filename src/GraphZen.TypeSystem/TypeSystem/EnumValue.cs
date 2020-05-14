// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLName("__EnumValue")]
    [Description("One possible value for a given Enum. Enum values are unique values, not " +
                 "a placeholder for a string or numeric value. However an Enum value is " +
                 "returned in a JSON response as a string.")]
    public class EnumValue : AnnotatableMember, IEnumValue
    {
        private readonly Lazy<EnumValueDefinitionSyntax> _syntax;

        public EnumValue(string name, string? description, object value, bool isDeprecated, string? deprecatedReason,
            IReadOnlyList<IDirectiveAnnotation> directives, EnumType declaringType) : base(directives )
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            DeclaringType = Check.NotNull(declaringType, nameof(declaringType));
            Value = value;
            IsDeprecated = isDeprecated;
            DeprecationReason = deprecatedReason;
            _syntax = new Lazy<EnumValueDefinitionSyntax>(() =>
                new EnumValueDefinitionSyntax(SyntaxFactory.EnumValue(SyntaxFactory.Name(Name)),
                    SyntaxHelpers.Description(Description))
            );
        }

        [GraphQLIgnore] public object Value { get; }

        IEnumTypeDefinition IEnumValueDefinition.DeclaringType => DeclaringType;

        [GraphQLIgnore] public EnumType DeclaringType { get; }

        public bool IsDeprecated { get; }

        [GraphQLCanBeNull] public string? DeprecationReason { get; }


        public string Name { get; }
        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.EnumValue;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;


        [GraphQLIgnore]
        public static EnumValue From(IEnumValueDefinition definition, EnumType declaringTye)
        {
            Check.NotNull(definition, nameof(definition));
            return new EnumValue(definition.Name, definition.Description, definition.Value, definition.IsDeprecated,
                definition.DeprecationReason, definition.GetDirectiveAnnotations().ToList(), declaringTye);
        }

        public override string ToString() => $"{Name} ({Value.Inspect()})";
        public string? Description { get; }
    }
}