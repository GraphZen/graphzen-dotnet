// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.LanguageModel;


namespace GraphZen.TypeSystem
{
    public class InterfaceType : NamedType, IInterfaceType
    {
        [NotNull] [ItemNotNull] private readonly Lazy<IReadOnlyDictionary<string, Field>> _fields;
        [NotNull] [ItemNotNull] private readonly Lazy<InterfaceTypeDefinitionSyntax> _syntax;

        public InterfaceType(string name, string description, Type clrType,
            [NotNull] [ItemNotNull] IEnumerable<IFieldDefinition> fields,
            TypeResolver<object, GraphQLContext> resolveType,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema) : base(
            Check.NotNull(name, nameof(name)), description, clrType, Check.NotNull(directives, nameof(directives)))
        {
            Check.NotNull(schema, nameof(schema));
            Check.NotNull(fields, nameof(fields));
            _fields = new Lazy<IReadOnlyDictionary<string, Field>>(() =>
                // ReSharper disable once PossibleNullReferenceException
                // ReSharper disable once AssignNullToNotNullAttribute
                fields.ToReadOnlyDictionary(_ => _.Name, _ => Field.From(_, this, schema.ResolveType)));
            ResolveType = resolveType;
            _syntax = new Lazy<InterfaceTypeDefinitionSyntax>(() => new InterfaceTypeDefinitionSyntax(
                SyntaxFactory.Name(Name),
                Description != null ? SyntaxFactory.StringValue(Description, true) : null,
                DirectiveAnnotations.ToDirectiveNodes(),
                Fields.Values.ToSyntaxNodes<FieldDefinitionSyntax>()));
        }


        public TypeResolver<object, GraphQLContext> ResolveType { get; }

        public override TypeKind Kind { get; } = TypeKind.Interface;

        IEnumerable<IFieldDefinition> IFieldsContainerDefinition.GetFields() => Fields.Values;


        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public IReadOnlyDictionary<string, Field> Fields => _fields.Value;

        public IEnumerable<Field> GetFields(bool includeDeprecated = false) =>
            // ReSharper disable once PossibleNullReferenceException
            Fields.Values.Where(_ => includeDeprecated || !_.IsDeprecated);


        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;

        public bool TryGetField(string name, out Field field) =>
            Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);

        public Field FindField(string name) =>
            TryGetField(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        public bool HasField(string name) => Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        public Field GetField(string name) => FindField(Check.NotNull(name, nameof(name))) ??
                                              throw new Exception($"{this} does not have a field named '{name}'.");


        [NotNull]
        public static InterfaceType From(IInterfaceTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            return new InterfaceType(definition.Name, definition.Description, definition.ClrType,
                definition.GetFields(), definition.ResolveType, definition.DirectiveAnnotations, schema);
        }
    }
}