// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public class InputObjectType : NamedType, IInputObjectType
    {
        [NotNull] [ItemNotNull] private readonly Lazy<InputObjectTypeDefinitionSyntax> _syntax;

        public InputObjectType(string name, string description, Type clrType, IEnumerable<IInputFieldDefinition> fields,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema) : base(Check.NotNull(name, nameof(name)),
            description, clrType, Check.NotNull(directives, nameof(directives)))
        {
            Check.NotNull(fields, nameof(fields));
            Check.NotNull(schema, nameof(schema));
            Fields = fields.ToReadOnlyDictionary(_ => _?.Name, _ => InputField.From(_, schema.ResolveType, this));
            _syntax = new Lazy<InputObjectTypeDefinitionSyntax>(() =>
                new InputObjectTypeDefinitionSyntax(SyntaxFactory.Name(Name), SyntaxHelpers.Description(Description),
                    null,
                    Fields.Values.ToSyntaxNodes<InputValueDefinitionSyntax>()));
        }

        public IReadOnlyDictionary<string, InputField> Fields { get; }


        public override TypeKind Kind { get; } = TypeKind.InputObject;
        IEnumerable<IInputFieldDefinition> IInputObjectTypeDefinition.GetFields() => GetFields();

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputObject;

        public IEnumerable<InputField> GetFields() => Fields.Values;

        public InputField FindField(string name) =>
            Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        public bool HasField(string name) => Fields.ContainsKey(name);

        public InputField GetField(string name) =>
            FindField(Check.NotNull(name, nameof(name))) ??
            throw new Exception($"{this} does not have a field named '{name}'.");

        public bool TryGetField(string name, out InputField field) =>
            Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);

        [NotNull]
        public static InputObjectType From(IInputObjectTypeDefinition definition,
            Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            return new InputObjectType(definition.Name, definition.Description, definition.ClrType,
                definition.GetFields(), definition.DirectiveAnnotations, schema);
        }
    }
}