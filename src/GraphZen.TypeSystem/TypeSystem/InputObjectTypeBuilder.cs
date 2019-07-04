// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.TypeSystem.Internal;


namespace GraphZen.TypeSystem
{
    public class InputObjectTypeBuilder<TInputObject> : IInputObjectTypeBuilder<TInputObject>,
        IInfrastructure<InternalInputObjectTypeBuilder>
    {
        public InputObjectTypeBuilder(InternalInputObjectTypeBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            Builder = builder;
        }

        [NotNull]
        private InternalInputObjectTypeBuilder Builder { get; }

        InternalInputObjectTypeBuilder IInfrastructure<InternalInputObjectTypeBuilder>.Instance => Builder;

        public IInputObjectTypeBuilder<TInputObject> Description(string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputValueBuilder> inputFieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            var fb = Builder.Field(name, ConfigurationSource.Explicit).Type(type);
            inputFieldConfigurator?.Invoke(new InputValueBuilder(fb));
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputValueBuilder> inputFieldConfigurator = null)
        {
            Check.NotNull(name, nameof(name));
            var fb = Builder.Field(name, ConfigurationSource.Explicit)
                .Type(typeof(TField));
            inputFieldConfigurator?.Invoke(new InputValueBuilder(fb));
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputValueBuilder> fieldBuilder = null)
        {
            Check.NotNull(fieldSelector, nameof(fieldSelector));
            var property = fieldSelector.GetPropertyInfoFromExpression();
            var fb = Builder.Field(property, ConfigurationSource.Explicit);
            fieldBuilder?.Invoke(new InputValueBuilder(fb));
            return this;
        }


        public IInputObjectTypeBuilder<TInputObject> Name(string name)
        {
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> DirectiveAnnotation(string name) =>
            DirectiveAnnotation(name, null);

        public IInputObjectTypeBuilder<TInputObject> DirectiveAnnotation(string name, object value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public IInputObjectTypeBuilder<TInputObject> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }
}