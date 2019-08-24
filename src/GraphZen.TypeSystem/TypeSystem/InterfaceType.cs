// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public class InterfaceType : NamedType, IInterfaceType
    {
          private readonly Lazy<IReadOnlyDictionary<string, Field>> _fields;
          private readonly Lazy<InterfaceTypeDefinitionSyntax> _syntax;

        public InterfaceType(string name, string description, Type clrType,
              IEnumerable<IFieldDefinition> fields,
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
        public IEnumerable<Field> GetFields() => Fields.Values;


        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;


        
        public static InterfaceType From(IInterfaceTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            return new InterfaceType(definition.Name, definition.Description, definition.ClrType,
                definition.GetFields(), definition.ResolveType, definition.DirectiveAnnotations, schema);
        }
    }
}