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
    public class DirectiveBuilder<TDirective> : IDirectiveBuilder<TDirective> where TDirective : notnull
    {
        public DirectiveBuilder(InternalDirectiveBuilder builder)
        {
            Builder = builder;
        }


        private InternalDirectiveBuilder Builder { get; }

        public DirectiveBuilder<object> ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            var ib = Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return new DirectiveBuilder<object>(ib);
        }

        IClrTypeBuilder IClrTypeBuilder.ClrType(Type clrType, string name) => ClrType(clrType, name);

        IClrTypeBuilder IClrTypeBuilder.RemoveClrType() => RemoveClrType();

        IClrTypeBuilder IClrTypeBuilder.ClrType(Type clrType, bool inferName) => ClrType(clrType, inferName);

        public DirectiveBuilder<object> ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var ib = Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return new DirectiveBuilder<object>(ib);
        }

        public DirectiveBuilder<object> RemoveClrType()
        {
            var ib = Builder.RemoveClrType(ConfigurationSource.Explicit);
            return new DirectiveBuilder<object>(ib);
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

        public DirectiveBuilder<TDirective> AddLocation(DirectiveLocation location)
        {
            Builder.AddLocation(location, ConfigurationSource.Explicit);

            return this;
        }

        public DirectiveBuilder<TDirective> RemoveLocation(DirectiveLocation location)
        {
            Builder.RemoveLocation(location, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Locations(DirectiveLocation location,
            params DirectiveLocation[] additionalLocations)
        {
            var locations = additionalLocations.ToList().Prepend(location);
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Locations(IEnumerable<DirectiveLocation> locations)
        {
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> RemoveLocations()
        {
            Builder.RemoveLocations(ConfigurationSource.Explicit);
            return this;
        }

        public DirectiveBuilder<TDirective> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        IDescriptionBuilder IDescriptionBuilder.RemoveDescription() => RemoveDescription();

        IDescriptionBuilder IDescriptionBuilder.Description(string description) => Description(description);

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


        public DirectiveBuilder<TDirective> Repeatable(bool repeatable)
        {
            Builder.Repeatable(repeatable, ConfigurationSource.Explicit);
            return this;
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

        DirectiveDefinition IInfrastructure<DirectiveDefinition>.Instance => Builder.Definition;

        InternalDirectiveBuilder IInfrastructure<InternalDirectiveBuilder>.Instance => Builder;
        INamedBuilder INamedBuilder.Name(string name) => Name(name);
    }
}