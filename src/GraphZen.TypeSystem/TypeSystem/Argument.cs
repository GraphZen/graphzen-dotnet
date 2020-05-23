// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLType(typeof(InputValue))]
    public class Argument : InputValue, IArgument
    {
        public Argument(
            string name,
            string? description,
            IGraphQLType? type,
            IArguments? declaringMember,
            object? defaultValue, bool hasDefaultValue,
            IReadOnlyList<IDirectiveAnnotation>? directives = null,
            ParameterInfo? clrInfo = null
        ) : this(name, description, type!, defaultValue, hasDefaultValue,
            directives ?? DirectiveAnnotation.EmptyList, typeRef => (IGraphQLType)typeRef, declaringMember!, clrInfo)
        {
        }

        public Argument(
            string name,
            string? description,
            IGraphQLTypeReference type,
            object? defaultValue,
            bool hasDefaultValue,
            IReadOnlyList<IDirectiveAnnotation> directives,
            TypeResolver? typeResolver,
            IArguments declaringMember,
            ParameterInfo? clrInfo) :
            base(name, description, type,
                defaultValue, hasDefaultValue,
                Check.NotNull(directives, nameof(directives)),
                typeResolver, clrInfo, declaringMember)
        {
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.ArgumentDefinition;
        IGraphQLTypeReference IArgumentDefinition.ArgumentType => InputType;
        public IGraphQLType ArgumentType => InputType;
        public new IArguments DeclaringMember => (IArguments)base.DeclaringMember;
        public new ParameterInfo? ClrInfo => base.ClrInfo as ParameterInfo;
        IArgumentsDefinition IArgumentDefinition.DeclaringMember => DeclaringMember;


        [GraphQLIgnore]
        public static Argument From(IArgumentDefinition definition, IArguments declaringMember,
            TypeResolver typeResolver)
        {
            Check.NotNull(definition, nameof(definition));
            return new Argument(definition.Name, definition.Description, definition.ArgumentType,
                definition.DefaultValue, definition.HasDefaultValue,
                definition.GetDirectiveAnnotations().ToImmutableList(),
                typeResolver, declaringMember, definition.ClrInfo);
        }
    }
}