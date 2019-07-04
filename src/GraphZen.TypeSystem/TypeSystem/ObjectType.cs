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
    [GraphQLType(typeof(IGraphQLType))]
    public class ObjectType : NamedType, IObjectType
    {
        [NotNull] [ItemNotNull] private readonly Lazy<IReadOnlyDictionary<string, Field>> _fields;
        [NotNull] [ItemNotNull] private readonly Lazy<IReadOnlyList<InterfaceType>> _lazyInterfaces;
        [NotNull] [ItemNotNull] private readonly Lazy<ObjectTypeDefinitionSyntax> _syntax;

        private ObjectType(string name, string description, Type clrType, IsTypeOf<object, GraphQLContext> isTypeOf,
            IEnumerable<IFieldDefinition> fields,
            IEnumerable<INamedTypeReference> interfaces,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema) : base(Check.NotNull(name, nameof(name)),
            description, clrType,
            Check.NotNull(directives, nameof(directives))
        )
        {
            IsTypeOf = isTypeOf;
            Check.NotNull(fields, nameof(fields));
            Check.NotNull(interfaces, nameof(interfaces));
            Check.NotNull(schema, nameof(schema));
            _fields = new Lazy<IReadOnlyDictionary<string, Field>>(() =>
                // ReSharper disable once AssignNullToNotNullAttribute
                fields.ToReadOnlyDictionary(_ => _?.Name, _ => Field.From(_, this, schema.ResolveType)));
            // ReSharper disable once PossibleNullReferenceException
            _lazyInterfaces = new Lazy<IReadOnlyList<InterfaceType>>(() =>
            {
                return interfaces.Select(_ => schema.GetType<InterfaceType>(_.Name)).ToList().AsReadOnly();
            });
            _syntax = new Lazy<ObjectTypeDefinitionSyntax>(() =>
            {
                var fieldNodes = Fields.Values.ToSyntaxNodes<FieldDefinitionSyntax>();
                var syntax = new ObjectTypeDefinitionSyntax(
                    SyntaxFactory.Name(Name),
                    SyntaxHelpers.Description(Description),
                    Interfaces.Select(_ => SyntaxFactory.NamedType(SyntaxFactory.Name(_.Name))).ToArray(),
                    null,
                    // ReSharper disable once PossibleNullReferenceException
                    fieldNodes
                );
                return syntax;
            });
        }


        public IsTypeOf<object, GraphQLContext> IsTypeOf { get; }
        IEnumerable<INamedTypeReference> IObjectTypeDefinition.Interfaces => Interfaces;

        public IReadOnlyList<InterfaceType> Interfaces => _lazyInterfaces.Value;

        public override TypeKind Kind { get; } = TypeKind.Object;
        IEnumerable<IFieldDefinition> IFieldsContainerDefinition.GetFields() => GetFields();

        public IEnumerable<Field> GetFields(bool includeDeprecated = false) =>
            // ReSharper disable once PossibleNullReferenceException
            Fields.Values.Where(_ => includeDeprecated || !_.IsDeprecated);


        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public IReadOnlyDictionary<string, Field> Fields => _fields.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Object;

        public bool Implements(string interfaceType)
        {
            Check.NotNull(interfaceType, nameof(interfaceType));
            return Interfaces.Any(_ => _.Name == interfaceType);
        }


        [NotNull]
        public static ObjectType From(IObjectTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(Schema));
            return new ObjectType(definition.Name, definition.Description, definition.ClrType, definition.IsTypeOf,
                definition.GetFields(), definition.Interfaces,
                definition.DirectiveAnnotations,
                schema
            );
        }
    }
}