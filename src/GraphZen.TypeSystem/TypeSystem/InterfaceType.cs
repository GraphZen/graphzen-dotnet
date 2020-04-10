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
    public partial class InterfaceType : NamedType, IInterfaceType
    {
        private readonly Lazy<IReadOnlyDictionary<string, Field>> _fields;
        private readonly Lazy<InterfaceTypeDefinitionSyntax> _syntax;

        public InterfaceType(string name, string? description, Type? clrType,
            IEnumerable<IFieldDefinition> fields,
            TypeResolver<object, GraphQLContext>? resolveType,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema) : base(
            Check.NotNull(name, nameof(name)), description, clrType, Check.NotNull(directives, nameof(directives)))
        {
            Check.NotNull(schema, nameof(schema));
            Check.NotNull(fields, nameof(fields));
            _fields = new Lazy<IReadOnlyDictionary<string, Field>>(() =>
                // ReSharper disable once PossibleNullReferenceException
                fields.ToReadOnlyDictionary(_ => _.Name, _ => Field.From(_, this, schema.ResolveType)));
            ResolveType = resolveType;
            _syntax = new Lazy<InterfaceTypeDefinitionSyntax>(() => new InterfaceTypeDefinitionSyntax(
                SyntaxFactory.Name(Name),
                Description != null ? SyntaxFactory.StringValue(Description, true) : null,
                DirectiveAnnotations.ToDirectiveNodes(),
                Fields.Values.ToSyntaxNodes<FieldDefinitionSyntax>()));
        }


        public TypeResolver<object, GraphQLContext>? ResolveType { get; }

        public override TypeKind Kind { get; } = TypeKind.Interface;

        IEnumerable<IFieldDefinition> IFieldsDefinition.GetFields() => Fields.Values;


        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        [GenDictionaryAccessors(nameof(Field.Name), nameof(Field))]
        public IReadOnlyDictionary<string, Field> Fields => _fields.Value;

        public IEnumerable<Field> GetFields() => Fields.Values;


        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;


        public static InterfaceType From(IInterfaceTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            return new InterfaceType(definition.Name, definition.Description, definition.ClrType,
                definition.GetFields(), definition.ResolveType, definition.GetDirectiveAnnotations().ToList(), schema);
        }
    }
}