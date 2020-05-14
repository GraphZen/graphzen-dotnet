// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable
namespace GraphZen.TypeSystem
{
    public class DirectiveBuilder<TDirective> : IDirectiveBuilder<TDirective>
    {
        public DirectiveBuilder(InternalDirectiveBuilder builder)
        {
            Builder = builder;
        }


        private InternalDirectiveBuilder Builder { get; }

        public IDirectiveBuilder<object> ClrType(Type clrType) => throw new NotImplementedException();

        public IDirectiveBuilder<object> ClrType(Type clrType, string name) => throw new NotImplementedException();

        public IDirectiveBuilder<object> RemoveClrType() => throw new NotImplementedException();

        public IDirectiveBuilder<T> ClrType<T>() => throw new NotImplementedException();

        public IDirectiveBuilder<T> ClrType<T>(string name) => throw new NotImplementedException();

        public IDirectiveBuilder<TDirective> Description(string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder<TDirective> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder<TDirective> Locations(params DirectiveLocation[] locations)
        {
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder<TDirective> RemoveArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveArgument(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder<TDirective> Argument(string name, Action<InputValueBuilder> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, ConfigurationSource.Explicit);
            var b = new InputValueBuilder(ib);
            configurator(b);
            return this;
        }

        public InputValueBuilder Argument(string name)
        {
            Check.NotNull(name, nameof(name));
            var ab = Builder.Argument(name, ConfigurationSource.Explicit);
            return new InputValueBuilder(ab);
        }

        public IDirectiveBuilder<TDirective> Argument(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Argument(name, ConfigurationSource.Explicit).Type(type, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder<TDirective> Argument(string name, string type, Action<InputValueBuilder> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, ConfigurationSource.Explicit).Type(type, ConfigurationSource.Explicit);
            var builder = new InputValueBuilder(ib);
            configurator(builder);
            return this;
        }

        public IDirectiveBuilder<TDirective> Argument<TArgument>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Argument(name, ConfigurationSource.Explicit).Type(typeof(TArgument), ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder<TDirective> Argument<TArgument>(string name, Action<InputValueBuilder> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, ConfigurationSource.Explicit)
                .Type(typeof(TArgument), ConfigurationSource.Explicit);
            var b = new InputValueBuilder(ib);
            configurator(b);
            return this;
        }


        public IDirectiveBuilder<TDirective> IgnoreArgument(string name) => throw new NotImplementedException();

        public IDirectiveBuilder<TDirective> UnignoreArgument(string name) => throw new NotImplementedException();

        IDirectiveDefinition IInfrastructure<IDirectiveDefinition>.Instance => Builder.Definition;

        InternalDirectiveBuilder IInfrastructure<InternalDirectiveBuilder>.Instance => Builder;
    }
}