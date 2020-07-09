// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
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
            FieldMap = fields.ToReadOnlyDictionary(_ => _.Name, _ => InputField.From(_, this));
            Fields = FieldMap.Values.ToList().AsReadOnly();
            _syntax = new Lazy<InputObjectTypeDefinitionSyntax>(() =>
                new InputObjectTypeDefinitionSyntax(SyntaxFactory.Name(Name), SyntaxHelpers.Description(Description),
                    null,
                    Fields.ToSyntaxNodes<InputValueDefinitionSyntax>()));
        }


        [GenDictionaryAccessors(nameof(InputField.Name), "Field")]
        public IReadOnlyDictionary<string, InputField> FieldMap { get; }

        public IReadOnlyCollection<InputField> Fields { get; }


        public override TypeKind Kind { get; } = TypeKind.InputObject;
        public override IEnumerable<IMember> Children() => throw new NotImplementedException();

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputObject;


        public static InputObjectType From(IInputObjectTypeDefinition definition,
            Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            return new InputObjectType(definition.Name, definition.Description, definition.ClrType,
                definition.Fields, definition.DirectiveAnnotations, schema);
        }

        IReadOnlyCollection<IInputFieldDefinition> IInputFieldsDefinition.Fields => Fields;
    }
}