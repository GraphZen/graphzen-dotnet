// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLType(typeof(InputValue))]
    public class Argument : InputValue, IArgument
    {
        public Argument(
            string name,
            string? description,
            IGraphQLType type,
            object? defaultValue,
            bool hasDefaultValue,
            IEnumerable<IDirective>? directives,
            IArguments declaringMember,
            ParameterInfo? clrInfo, Schema schema) :
            base(name, description, type, defaultValue, hasDefaultValue, directives, clrInfo, declaringMember, schema)
        {
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.ArgumentDefinition;
        IGraphQLType IArgument.ArgumentType => InputType;
        public IGraphQLType ArgumentType => InputType;
        public new IArguments DeclaringMember => (IArguments)base.DeclaringMember;
        public new ParameterInfo? ClrInfo => base.ClrInfo as ParameterInfo;
        IArguments IArgument.DeclaringMember => DeclaringMember;


        internal static Func<IArguments, IEnumerable<Argument>> CreateArguments(
            IEnumerable<IArgument> arguments, Schema schema) =>
            declaringMember => arguments.Select(_ => new Argument(_.Name, _.Description, _.ArgumentType, _.DefaultValue,
                _.HasDefaultValue, _.DirectiveAnnotations, declaringMember, _.ClrInfo, schema));

        [GraphQLIgnore]
        public static Argument From(IArgument definition, IArguments declaringMember, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            return new Argument(definition.Name, definition.Description, definition.ArgumentType,
                definition.DefaultValue, definition.HasDefaultValue, definition.DirectiveAnnotations,
                declaringMember, definition.ClrInfo, schema);
        }
    }
}