// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLName("__Field")]
    [Description("Object and Interface types are described by a list of Fields, each of " +
                 "which has a name, potentially a list of arguments, and a return type.")]
    public partial class Field : AnnotatableMember, IField
    {
        private readonly Lazy<IGraphQLType> _fieldType;
        private readonly Lazy<FieldDefinitionSyntax> _syntax;


        public Field(string name, string? description, IFields? declaringType, IGraphQLTypeReference fieldType,
            Func<Field, IEnumerable<Argument>>? arguments,
            Resolver<object, object?>? resolver,
            IEnumerable<IDirectiveAnnotation>? directives,
            MemberInfo? clrInfo, Schema schema) : base(directives, schema)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            _fieldType = new Lazy<IGraphQLType>(() =>
            {
                try
                {
                    return schema.ResolveType(fieldType);
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to resolve type for field {this} with type reference: {fieldType}. ",
                        e);
                }
            });
            Arguments = arguments == null
                ? ImmutableDictionary<string, Argument>.Empty
                : arguments(this).ToReadOnlyDictionary(_ => _.Name);

            Resolver = resolver;
            ClrInfo = clrInfo;
            DeclaringType = declaringType!;
            _syntax = new Lazy<FieldDefinitionSyntax>(() =>
            {
                var fieldTypeNode = FieldType.ToTypeSyntax();
                return new FieldDefinitionSyntax(SyntaxFactory.Name(Name), fieldTypeNode,
                    SyntaxHelpers.Description(Description),
                    Arguments.Values.ToSyntaxNodes<InputValueDefinitionSyntax>());
            });
        }


        [GraphQLIgnore] public IFields DeclaringType { get; }


        [GraphQLName("type")] public IGraphQLType FieldType => _fieldType.Value;


        IGraphQLTypeReference IFieldDefinition.FieldType => FieldType;

        [GraphQLIgnore] public Resolver<object, object?>? Resolver { get; }

        IFieldsDefinition IFieldDefinition.DeclaringType => DeclaringType;

        public bool IsDeprecated { get; } = false;

        [GraphQLCanBeNull] public string? DeprecationReason { get; } = null;

        [GraphQLIgnore]
        [GenDictionaryAccessors(nameof(Argument.Name), nameof(Argument))]
        public IReadOnlyDictionary<string, Argument> Arguments { get; }


        public string Name { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.FieldDefinition;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        [GraphQLIgnore]
        IEnumerable<IArgumentDefinition> IArgumentsDefinition.GetArguments() => GetArguments();

        [GraphQLName("args")]
        public IEnumerable<Argument> GetArguments() => Arguments.Values;

        [GraphQLIgnore] public MemberInfo? ClrInfo { get; }

        object? IClrInfo.ClrInfo => ClrInfo;

        [GraphQLCanBeNull] public string? Description { get; }

        [GraphQLIgnore] public IGraphQLType TypeReference => FieldType;
        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => TypeReference;

        [GraphQLIgnore]
        public static Field From(IFieldDefinition definition, IFields? declaringType, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(definition.FieldType, nameof(definition.FieldType));
            return new Field(definition.Name, definition.Description, declaringType,
                definition.FieldType, Argument.CreateArguments(definition.GetArguments()), definition.Resolver,
                definition.GetDirectiveAnnotations(),
                definition.ClrInfo,
                schema);
        }


        public override string ToString() => $"{DeclaringType}.{Name}";
        IEnumerable<IMemberDefinition> IMemberParentDefinition.Children() => Children();

        public IEnumerable<IMember> Children() => GetArguments();
    }
}