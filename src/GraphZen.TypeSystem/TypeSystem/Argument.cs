// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [GraphQLType(typeof(InputValue))]
    public class Argument : InputValue, IArgument
    {
        public Argument(
            [NotNull] string name,
            string description,
            [NotNull] IGraphQLType type,
            [NotNull] IArgumentsContainer declaringMember,
            object defaultValue, bool hasDefaultValue,
            IReadOnlyList<IDirectiveAnnotation> directives = null,
            ParameterInfo clrInfo = null
        ) : this(name, description, type, defaultValue, hasDefaultValue,
            directives ?? DirectiveAnnotation.EmptyList, typeRef => (IGraphQLType) typeRef, declaringMember, clrInfo)
        {
        }

        public Argument(
            [NotNull] string name,
            string description,
            [NotNull] IGraphQLTypeReference type,
            object defaultValue,
            bool hasDefaultValue,
            IReadOnlyList<IDirectiveAnnotation> directives,
            TypeResolver typeResolver,
            [NotNull] IArgumentsContainer declaringMember,
            ParameterInfo clrInfo) :
            base(name, description, type,
                defaultValue, hasDefaultValue,
                Check.NotNull(directives, nameof(directives)),
                typeResolver, clrInfo, declaringMember)
        {
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.ArgumentDefinition;

        public new IArgumentsContainer DeclaringMember => (IArgumentsContainer) base.DeclaringMember;
        public new ParameterInfo ClrInfo => base.ClrInfo as ParameterInfo;
        IArgumentsContainerDefinition IArgumentDefinition.DeclaringMember => DeclaringMember;

        [NotNull]
        [GraphQLIgnore]
        public static Argument From(IArgumentDefinition definition, [NotNull] IArgumentsContainer declaringMember,
            TypeResolver typeResolver)
        {
            Check.NotNull(definition, nameof(definition));
            return new Argument(definition.Name, definition.Description, definition.InputType,
                definition.DefaultValue, definition.HasDefaultValue,
                definition.DirectiveAnnotations,
                typeResolver, declaringMember, definition.ClrInfo);
        }
    }
}