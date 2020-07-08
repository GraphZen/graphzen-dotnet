// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class DirectiveBuilder : IDirectiveBuilder
    {
        public DirectiveBuilder(InternalDirectiveBuilder builder)
        {
            Builder = builder;
        }

        protected InternalDirectiveBuilder Builder { get; }

        DirectiveDefinition IInfrastructure<DirectiveDefinition>.Instance => Builder.Definition;

        InternalDirectiveBuilder IInfrastructure<InternalDirectiveBuilder>.Instance => Builder;

        public IClrTypeBuilder ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public INamedBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder AddLocation(DirectiveLocation location)
        {
            Builder.AddLocation(location, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder RemoveLocation(DirectiveLocation location)
        {
            Builder.RemoveLocation(location, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder Locations(DirectiveLocation location, params DirectiveLocation[] additionalLocations)
        {
            var locations = additionalLocations.ToList().Prepend(location);
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder Locations(IEnumerable<DirectiveLocation> locations)
        {
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder ClearLocations()
        {
            Builder.RemoveLocations(ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveBuilder Repeatable(bool isRepeatable)
        {
            Builder.Repeatable(isRepeatable, ConfigurationSource.Explicit);
            return this;

        }
    }

    public class DirectiveBuilder<TDirective> : DirectiveBuilder, IDirectiveBuilder<TDirective>
        where TDirective : notnull
    {
        public DirectiveBuilder(InternalDirectiveBuilder builder) : base(builder)
        {
        }

        public new DirectiveBuilder<object> ClrType(Type clrType, bool inferName = false)
        {
            base.ClrType(clrType, inferName);
            return new DirectiveBuilder<object>(Builder);
        }

        public new DirectiveBuilder<object> ClrType(Type clrType, string name)
        {
            base.ClrType(clrType, name);
            return new DirectiveBuilder<object>(Builder);
        }

        public new DirectiveBuilder<object> RemoveClrType()
        {
            base.RemoveClrType();
            return new DirectiveBuilder<object>(Builder);
        }

        public DirectiveBuilder<T> ClrType<T>(bool inferName = false) where T : notnull
        {
            var ib = Builder.ClrType(typeof(T), inferName, ConfigurationSource.Explicit);
            return new DirectiveBuilder<T>(ib);
        }

        public DirectiveBuilder<T> ClrType<T>(string name) where T : notnull
        {
            Check.NotNull(name, nameof(name));
            var ib = Builder.ClrType(typeof(T), name, ConfigurationSource.Explicit);
            return new DirectiveBuilder<T>(ib);
        }

        public new DirectiveBuilder<TDirective> AddLocation(DirectiveLocation location)
        {
            return (DirectiveBuilder<TDirective>)base.AddLocation(location);
        }

        public new DirectiveBuilder<TDirective> RemoveLocation(DirectiveLocation location)
        {
            return (DirectiveBuilder<TDirective>)base.RemoveLocation(location);
        }

        public new DirectiveBuilder<TDirective> Locations(DirectiveLocation location,
            params DirectiveLocation[] additionalLocations)
        {
            return (DirectiveBuilder<TDirective>)base.Locations(location, additionalLocations);
        }

        public new DirectiveBuilder<TDirective> Locations(IEnumerable<DirectiveLocation> locations)
        {
            return (DirectiveBuilder<TDirective>)base.Locations(locations);
        }

        public new DirectiveBuilder<TDirective> ClearLocations()
        {
            return (DirectiveBuilder<TDirective>)base.ClearLocations();
        }

        public new DirectiveBuilder<TDirective> Description(string description) =>
            (DirectiveBuilder<TDirective>)base.Description(description);


        public new DirectiveBuilder<TDirective> RemoveDescription() =>
            (DirectiveBuilder<TDirective>)base.RemoveDescription();

        public new DirectiveBuilder<TDirective> Name(string name) => (DirectiveBuilder<TDirective>)base.Name(name);


        public new DirectiveBuilder<TDirective> Repeatable(bool repeatable)

        {
            return (DirectiveBuilder<TDirective>)base.Repeatable(repeatable);
        }

        public DirectiveBuilder<TDirective> RemoveArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveArgument(name, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Argument(string name, Action<ArgumentBuilder<object?>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name);
            var b = new ArgumentBuilder<object?>(ib);
            configurator(b);
            return this;
        }

        public ArgumentBuilder<object?> Argument(string name) =>
            new ArgumentBuilder<object?>(Builder.Argument(Check.NotNull(name, nameof(name))));

        public DirectiveBuilder<TDirective> Argument(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Argument(name, type, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Argument(string name, string type,
            Action<ArgumentBuilder<object?>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, type, ConfigurationSource.Explicit)!;
            var builder = new ArgumentBuilder<object?>(ib);
            configurator(builder);
            return this;
        }

        public DirectiveBuilder<TDirective> Argument<TArgument>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Argument(name, typeof(TArgument), ConfigurationSource.Explicit);
            return this;
        }


        public DirectiveBuilder<TDirective> Argument<TArgument>(string name,
            Action<ArgumentBuilder<TArgument>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(configurator, nameof(configurator));
            var ib = Builder.Argument(name, typeof(TArgument), ConfigurationSource.Explicit)!;
            var b = new ArgumentBuilder<TArgument>(ib);
            configurator(b);
            return this;
        }


        public DirectiveBuilder<TDirective> IgnoreArgument(string name) => throw new NotImplementedException();

        public DirectiveBuilder<TDirective> UnignoreArgument(string name) => throw new NotImplementedException();

        INamedBuilder INamedBuilder.Name(string name) => Name(name);
    }
}