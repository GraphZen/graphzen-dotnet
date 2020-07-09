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
    public partial class InterfaceType : NamedType, IInterfaceType
    {
        private readonly Lazy<IReadOnlyDictionary<string, Field>> _fieldMap;
        private readonly Lazy<IReadOnlyCollection<Field>> _fields;
        private readonly Lazy<InterfaceTypeDefinitionSyntax> _syntax;

        public InterfaceType(string name, string? description, Type? clrType,
            IEnumerable<IFieldDefinition> fields,
            TypeResolver<object, GraphQLContext>? resolveType,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema) : base(
            name, description, clrType, directives, schema)
        {
            Check.NotNull(schema, nameof(schema));
            Check.NotNull(fields, nameof(fields));
            _fieldMap = new Lazy<IReadOnlyDictionary<string, Field>>(() =>
                // ReSharper disable once PossibleNullReferenceException
                fields.ToReadOnlyDictionary(_ => _.Name, _ => Field.From(_, this, Schema)));
            _fields = new Lazy<IReadOnlyCollection<Field>>(() => _fieldMap.Value.Values.ToList().AsReadOnly());
            ResolveType = resolveType;
            _syntax = new Lazy<InterfaceTypeDefinitionSyntax>(() => new InterfaceTypeDefinitionSyntax(
                SyntaxFactory.Name(Name),
                Description != null ? SyntaxFactory.StringValue(Description, true) : null,
                DirectiveAnnotations.ToDirectiveNodes(),
                Fields.ToSyntaxNodes<FieldDefinitionSyntax>()));
        }


        public TypeResolver<object, GraphQLContext>? ResolveType { get; }


        public override TypeKind Kind { get; } = TypeKind.Interface;
        public override IEnumerable<IMember> Children() => throw new NotImplementedException();



        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        [GenDictionaryAccessors(nameof(Field.Name), nameof(Field))]
        public IReadOnlyDictionary<string, Field> FieldMap => _fieldMap.Value;

        public IReadOnlyCollection<Field> Fields => _fields.Value;


        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;


        public static InterfaceType From(IInterfaceTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            return new InterfaceType(definition.Name, definition.Description, definition.ClrType,
                definition.Fields, definition.ResolveType, definition.DirectiveAnnotations, schema);
        }

        IReadOnlyCollection<IFieldDefinition> IFieldsDefinition.Fields => Fields;
    }
}