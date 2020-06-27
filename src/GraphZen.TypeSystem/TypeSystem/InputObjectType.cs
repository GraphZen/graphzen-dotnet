// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public partial class InputObjectType : NamedType, IInputObjectType
    {
        private readonly Lazy<InputObjectTypeDefinitionSyntax> _syntax;

        public InputObjectType(string name, string? description, Type? clrType,
            IEnumerable<IInputFieldDefinition> fields,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema) : base(name,
            description, clrType, directives, schema)
        {
            Check.NotNull(fields, nameof(fields));
            Check.NotNull(schema, nameof(schema));
            Fields = fields.ToReadOnlyDictionary(_ => _.Name, _ => InputField.From(_, this));
            _syntax = new Lazy<InputObjectTypeDefinitionSyntax>(() =>
                new InputObjectTypeDefinitionSyntax(SyntaxFactory.Name(Name), SyntaxHelpers.Description(Description),
                    null,
                    Fields.Values.ToSyntaxNodes<InputValueDefinitionSyntax>()));
        }


        [GenDictionaryAccessors(nameof(InputField.Name), "Field")]
        public IReadOnlyDictionary<string, InputField> Fields { get; }


        public override TypeKind Kind { get; } = TypeKind.InputObject;
        public override IEnumerable<IMember> Children() => throw new NotImplementedException();

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputObject;

        public IEnumerable<InputField> GetFields() => Fields.Values;


        public static InputObjectType From(IInputObjectTypeDefinition definition,
            Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            return new InputObjectType(definition.Name, definition.Description, definition.ClrType,
                definition.GetFields(), definition.GetDirectiveAnnotations().ToList(), schema);
        }

        IEnumerable<IInputFieldDefinition> IInputFieldsDefinition.GetFields() => GetFields();
    }
}