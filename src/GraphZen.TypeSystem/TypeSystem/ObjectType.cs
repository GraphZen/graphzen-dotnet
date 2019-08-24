// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [GraphQLType(typeof(IGraphQLType))]
    public class ObjectType : NamedType, IObjectType
    {
          private readonly Lazy<IReadOnlyDictionary<string, Field>> _fields;
          private readonly Lazy<IReadOnlyList<InterfaceType>> _interfaces;
          private readonly Lazy<IReadOnlyDictionary<string, InterfaceType>> _interfaceMap;
          private readonly Lazy<ObjectTypeDefinitionSyntax> _syntax;

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

            _interfaceMap = new Lazy<IReadOnlyDictionary<string, InterfaceType>>(() =>
                {
                    return interfaces.ToReadOnlyDictionary(_ => _.Name, _ =>
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        return schema.GetType<InterfaceType>(_.Name);
                    });
                }
            );
            _interfaces =
                new Lazy<IReadOnlyList<InterfaceType>>(() => InterfacesMap.Values.ToImmutableList());
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


        public override TypeKind Kind { get; } = TypeKind.Object;
        IEnumerable<IFieldDefinition> IFieldsContainerDefinition.GetFields() => GetFields();

        public IEnumerable<Field> GetFields() =>
            // ReSharper disable once PossibleNullReferenceException
            Fields.Values;


        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public IReadOnlyDictionary<string, Field> Fields => _fields.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Object;


        
        public static ObjectType From(IObjectTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(Schema));
            return new ObjectType(definition.Name, definition.Description, definition.ClrType, definition.IsTypeOf,
                definition.GetFields(), definition.GetInterfaces(),
                definition.DirectiveAnnotations,
                schema
            );
        }

        public IEnumerable<InterfaceType> GetInterfaces() => Interfaces;
        public IReadOnlyList<InterfaceType> Interfaces => _interfaces.Value;
        public IReadOnlyDictionary<string, InterfaceType> InterfacesMap => _interfaceMap.Value;

        IEnumerable<IInterfaceTypeDefinition> IInterfacesContainerDefinition.GetInterfaces() => GetInterfaces();
    }
}