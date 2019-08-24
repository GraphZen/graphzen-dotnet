#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [GraphQLName("__InputValue")]
    [GraphQLObject]
    [Description("Arguments provided to Fields or Directives and the input fields of an " +
                 "InputObject are represented as Input Values which describe their type " +
                 "and optionally a default value.")]
    public abstract class InputValue : AnnotatableMember, IInputValue
    {
        [NotNull] [ItemNotNull] private readonly Lazy<InputValueDefinitionSyntax> _syntax;
        [NotNull] [ItemNotNull] private readonly Lazy<IGraphQLType> _type;

        /// <inheritdoc />
        protected InputValue(
            [NotNull] string name,
            [CanBeNull] string description,
            [NotNull] IGraphQLTypeReference type,
            object defaultValue,
            bool hasDefaultValue,
            [NotNull] IReadOnlyList<IDirectiveAnnotation> directives,
            TypeResolver typeResolver, object clrInfo, [NotNull] IMemberDefinition declaringMember) : base(directives)
        {
            IGraphQLType DefaultTypeResolver(IGraphQLTypeReference typeReference) =>
                type as IGraphQLType ?? throw new InvalidOperationException(
                    $"{typeReference} is not a valid GraphQL type. Provide a type resolver to correctly resolve.");

            typeResolver = typeResolver ?? DefaultTypeResolver;
            Name = name;
            Description = description;
            _type = new Lazy<IGraphQLType>(() => typeResolver(type));
            DefaultValue = defaultValue;
            HasDefaultValue = hasDefaultValue;
            ClrInfo = clrInfo;
            _syntax = new Lazy<InputValueDefinitionSyntax>(() =>
                new InputValueDefinitionSyntax(SyntaxFactory.Name(name), InputType.ToTypeSyntax(),
                    SyntaxHelpers.Description(Description),
                    AstFromValue.Get(hasDefaultValue ? Maybe.Some(defaultValue) : Maybe.None<object>(),
                        InputType)));
            DeclaringMember = declaringMember;
        }

        [GraphQLName("type")]
        public IGraphQLType InputType =>
            _type.Value;

        [GraphQLIgnore]
        public IMemberDefinition DeclaringMember { get; }

        IGraphQLTypeReference IInputValueDefinition.InputType => InputType;

        [GraphQLIgnore]
        public object DefaultValue { get; }

        [GraphQLIgnore]
        public bool HasDefaultValue { get; }


        public override string Description { get; }

        public string Name { get; }

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        [GraphQLIgnore]
        public object ClrInfo { get; }
    }
}