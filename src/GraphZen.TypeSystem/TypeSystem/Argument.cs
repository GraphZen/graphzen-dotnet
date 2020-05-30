// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
            IGraphQLTypeReference type,
            object? defaultValue,
            bool hasDefaultValue,
            IEnumerable<IDirectiveAnnotation>? directives,
            IArguments declaringMember,
            ParameterInfo? clrInfo) :
            base(name, description, type, defaultValue, hasDefaultValue, directives, clrInfo, declaringMember)
        {
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.ArgumentDefinition;
        IGraphQLTypeReference IArgumentDefinition.ArgumentType => InputType;
        public IGraphQLType ArgumentType => InputType;
        public new IArguments DeclaringMember => (IArguments) base.DeclaringMember;
        public new ParameterInfo? ClrInfo => base.ClrInfo as ParameterInfo;
        IArgumentsDefinition IArgumentDefinition.DeclaringMember => DeclaringMember;


        internal static Func<IArguments, IEnumerable<Argument>> CreateArguments(
            IEnumerable<IArgumentDefinition> arguments) =>
            declaringMember => arguments.Select(_ => new Argument(_.Name, _.Description, _.ArgumentType, _.DefaultValue,
                _.HasDefaultValue, _.GetDirectiveAnnotations(), declaringMember, _.ClrInfo));

        [GraphQLIgnore]
        public static Argument From(IArgumentDefinition definition, IArguments declaringMember)
        {
            Check.NotNull(definition, nameof(definition));
            return new Argument(definition.Name, definition.Description, definition.ArgumentType,
                definition.DefaultValue, definition.HasDefaultValue, definition.GetDirectiveAnnotations(),
                declaringMember, definition.ClrInfo);
        }
    }
}