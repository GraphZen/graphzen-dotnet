// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class DirectiveBuilder<TDirective> : IDirectiveBuilder<TDirective>
    {
        public DirectiveBuilder(InternalDirectiveBuilder builder)
        {
            Builder = builder;
        }


        private InternalDirectiveBuilder Builder { get; }

        public DirectiveBuilder<object> ClrType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var ib = Builder.ClrType(clrType, ConfigurationSource.Explicit);
            return new DirectiveBuilder<object>(ib);
        }

        public DirectiveBuilder<object> ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            throw new NotImplementedException();
        }

        public DirectiveBuilder<object> RemoveClrType() => throw new NotImplementedException();

        public DirectiveBuilder<T> ClrType<T>()
        {
            var ib = Builder.ClrType(typeof(T), ConfigurationSource.Explicit);
            return new DirectiveBuilder<T>(ib);
        }

        public DirectiveBuilder<T> ClrType<T>(string name) => throw new NotImplementedException();

        public DirectiveBuilder<TDirective> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Locations(params DirectiveLocation[] locations)
        {
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> RemoveArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveArgument(name, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Argument(string name, Action<InputValueBuilder<object?>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, ConfigurationSource.Explicit);
            var b = new InputValueBuilder<object?>(ib);
            configurator(b);
            return this;
        }

        public InputValueBuilder<object?> Argument(string name)
        {
            Check.NotNull(name, nameof(name));
            var ab = Builder.Argument(name, ConfigurationSource.Explicit);
            return new InputValueBuilder<object?>(ab);
        }

        public DirectiveBuilder<TDirective> Argument(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Argument(name, ConfigurationSource.Explicit).Type(type, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Argument(string name, string type,
            Action<InputValueBuilder<object?>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, ConfigurationSource.Explicit).Type(type, ConfigurationSource.Explicit);
            var builder = new InputValueBuilder<object?>(ib);
            configurator(builder);
            return this;
        }

        public DirectiveBuilder<TDirective> Argument<TArgument>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Argument(name, ConfigurationSource.Explicit).Type(typeof(TArgument), ConfigurationSource.Explicit);
            return this;
        }


        public DirectiveBuilder<TDirective> Argument<TArgument>(string name,
            Action<InputValueBuilder<TArgument>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, ConfigurationSource.Explicit)
                .Type(typeof(TArgument), ConfigurationSource.Explicit);
            var b = new InputValueBuilder<TArgument>(ib);
            configurator(b);
            return this;
        }


        public DirectiveBuilder<TDirective> IgnoreArgument(string name) => throw new NotImplementedException();

        public DirectiveBuilder<TDirective> UnignoreArgument(string name) => throw new NotImplementedException();

        DirectiveDefinition IInfrastructure<DirectiveDefinition>.Instance => Builder.Definition;

        InternalDirectiveBuilder IInfrastructure<InternalDirectiveBuilder>.Instance => Builder;
    }
}