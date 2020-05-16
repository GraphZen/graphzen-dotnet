// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
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

        public Field(string name, string description, IFields? declaringType, IGraphQLType fieldType,
            IEnumerable<IArgumentDefinition>? arguments, Resolver<object, object?> resolver, MemberInfo? clrMember,
            bool isDeprecated = false, string? deprecatedReason = null,
            IReadOnlyList<IDirectiveAnnotation>? directives = null
        ) : this(name, description, declaringType, fieldType, arguments, resolver, isDeprecated, deprecatedReason,
            directives ?? DirectiveAnnotation.EmptyList, typeRef => (IGraphQLType)typeRef, clrMember)
        {
        }

        public Field(string name, string? description, IFields? declaringType, IGraphQLTypeReference fieldType,
            IEnumerable<IArgumentDefinition>? arguments, Resolver<object, object?>? resolver,
            bool isDeprecated, string? deprecatedReason,
            IReadOnlyList<IDirectiveAnnotation>? directives,
            TypeResolver typeResolver, MemberInfo? clrInfo) : base(directives)
        {
            Check.NotNull(typeResolver, nameof(typeResolver));
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            _fieldType = new Lazy<IGraphQLType>(() =>
            {
                try
                {
                    return typeResolver(fieldType);
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to resolve type for field {this} with type reference: {fieldType}. ",
                        e);
                }
            });
            Arguments = new ReadOnlyDictionary<string, Argument>(
                (arguments ?? Enumerable.Empty<IArgumentDefinition>())
                .Select(_ => Argument.From(_, this, typeResolver)).ToDictionary(_ => _.Name));
            Resolver = resolver;
            IsDeprecated = isDeprecated;
            DeprecationReason = deprecatedReason;
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

        public bool IsDeprecated { get; }

        [GraphQLCanBeNull] public string? DeprecationReason { get; }

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


        [GraphQLIgnore]
        public static Field From(IFieldDefinition definition, IFields declaringType,
            TypeResolver typeResolver)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(definition.FieldType, nameof(definition.FieldType));
            return new Field(definition.Name, definition.Description, declaringType,
                definition.FieldType, definition.GetArguments(), definition.Resolver, definition.IsDeprecated,
                definition.DeprecationReason, definition.GetDirectiveAnnotations().ToList(), typeResolver,
                definition.ClrInfo);
        }

        public override string ToString() => $"{DeclaringType}.{Name}";

        [GraphQLCanBeNull] public string? Description { get; }
    }
}