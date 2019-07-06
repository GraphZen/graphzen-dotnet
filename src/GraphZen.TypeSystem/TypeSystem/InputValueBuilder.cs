// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public class InputValueBuilder : IInfrastructure<InternalInputValueBuilder>, IAnnotableBuilder<InputValueBuilder>
    {
        public InputValueBuilder(InternalInputValueBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }


        [NotNull]
        protected InternalInputValueBuilder Builder { get; }

        public InputValueBuilder DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);

        public InputValueBuilder DirectiveAnnotation(string name, object value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public InputValueBuilder RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }

        InternalInputValueBuilder IInfrastructure<InternalInputValueBuilder>.Instance => Builder;

        public InputValueBuilder Name(string name)
        {
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        [NotNull]
        public InputValueBuilder Type(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.Type(type);
            return this;
        }

        [NotNull]
        public InputValueBuilder Type<TInputValue>(bool canBeNull = false)
        {
            Builder.Type(typeof(TInputValue));
            return this;
        }

        [NotNull]
        public InputValueBuilder DefaultValue([CanBeNull] object value)
        {
            Builder.DefaultValue(value, ConfigurationSource.Explicit);
            return this;
        }

        [NotNull]
        public InputValueBuilder RemoveDefaultValue()
        {
            Builder.RemoveDefaultValue(ConfigurationSource.Explicit);
            return this;
        }


        [NotNull]
        public InputValueBuilder Description([CanBeNull] string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }
    }
}