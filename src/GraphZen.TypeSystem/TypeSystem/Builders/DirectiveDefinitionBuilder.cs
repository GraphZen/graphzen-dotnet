// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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
    internal class DirectiveDefinitionBuilder : IDirectiveDefinitionBuilder
    {
        public DirectiveDefinitionBuilder(InternalDirectiveDefinitionBuilder builder)
        {
            Builder = builder;
        }

        private InternalDirectiveDefinitionBuilder Builder { get; }

        MutableDirectiveDefinition IInfrastructure<MutableDirectiveDefinition>.Instance => Builder.Definition;

        InternalDirectiveDefinitionBuilder IInfrastructure<InternalDirectiveDefinitionBuilder>.Instance => Builder;

        public IDirectiveDefinitionBuilder ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return this;
        }

        IClrTypeBuilder IClrTypeBuilder.ClrType(Type clrType, string name) => ClrType(clrType, name);

        IClrTypeBuilder IClrTypeBuilder.RemoveClrType() => RemoveClrType();

        IClrTypeBuilder IClrTypeBuilder.ClrType(Type clrType, bool inferName) => ClrType(clrType, inferName);

        public IDirectiveDefinitionBuilder ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        IDescriptionBuilder IDescriptionBuilder.RemoveDescription() => RemoveDescription();

        IDescriptionBuilder IDescriptionBuilder.Description(string description) => Description(description);

        public IDirectiveDefinitionBuilder RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder AddLocation(DirectiveLocation location)
        {
            Builder.AddLocation(location, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder RemoveLocation(DirectiveLocation location)
        {
            Builder.RemoveLocation(location, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder Locations(DirectiveLocation location, params DirectiveLocation[] additionalLocations)
        {
            var locations = additionalLocations.ToList().Prepend(location);
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder Locations(IEnumerable<DirectiveLocation> locations)
        {
            Builder.Locations(locations, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder ClearLocations()
        {
            Builder.RemoveLocations(ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder Repeatable(bool isRepeatable)
        {
            Builder.Repeatable(isRepeatable, ConfigurationSource.Explicit);
            return this;
        }
        public IDirectiveDefinitionBuilder ClrType<T>(bool inferName = false) where T : notnull
        {
            Builder.ClrType(typeof(T), inferName, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder ClrType<T>(string name) where T : notnull
        {
            Check.NotNull(name, nameof(name));
            Builder.ClrType(typeof(T), name, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder RemoveArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveArgument(name, ConfigurationSource.Explicit);
            return this;
        }

        IDirectiveDefinitionBuilder IArgumentsBuilder<IDirectiveDefinitionBuilder>.ClearArguments() => throw new NotImplementedException();

        IArgumentsBuilder IArgumentsBuilder.ClearArguments() => throw new NotImplementedException();

        IArgumentsBuilder IArgumentsBuilder.Argument(string name, Action<IArgumentBuilder> argumentAction) => Argument(name, argumentAction);

        IArgumentsBuilder IArgumentsBuilder.RemoveArgument(string name) => RemoveArgument(name);

        public IDirectiveDefinitionBuilder Argument(string name, Action<IArgumentBuilder> argumentAction)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(argumentAction, nameof(argumentAction));
            var ib = Builder.Argument(name);
            argumentAction(ib.Definition.Builder);
            return this;
        }

        public IArgumentBuilder Argument(string name)
        {
            Check.NotNull(name, nameof(name));
            return Builder.Argument(name).Definition.Builder;
        }

        IArgumentsBuilder IArgumentsBuilder.Argument(string name, string type) => Argument(name, type);

        IArgumentsBuilder IArgumentsBuilder.Argument(string name, string type, Action<IArgumentBuilder> argumentAction) => Argument(name, type, argumentAction);

        IDirectiveDefinitionBuilder IArgumentsBuilder<IDirectiveDefinitionBuilder>.Argument(string name, Type clrType) => throw new NotImplementedException();

        IDirectiveDefinitionBuilder IArgumentsBuilder<IDirectiveDefinitionBuilder>.Argument(string name, Type clrType, Action<IArgumentBuilder> argumentAction) => throw new NotImplementedException();

        IArgumentsBuilder IArgumentsBuilder.Argument(string name, Type clrType) => throw new NotImplementedException();

        IArgumentsBuilder IArgumentsBuilder.Argument(string name, Type clrType, Action<IArgumentBuilder> argumentAction) => throw new NotImplementedException();

        IArgumentsBuilder IArgumentsBuilder.Argument<TArgument>(string name) => Argument<TArgument>(name);

        IArgumentsBuilder IArgumentsBuilder.Argument<TArgument>(string name, Action<IArgumentBuilder> argumentAction) => Argument(name, argumentAction);

        IArgumentsBuilder IArgumentsBuilder.IgnoreArgument(string name) => IgnoreArgument(name);

        IArgumentsBuilder IArgumentsBuilder.UnignoreArgument(string name) => UnignoreArgument(name);

        public IDirectiveDefinitionBuilder Argument(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Argument(name, type, ConfigurationSource.Explicit);
            return this;
        }

        public IDirectiveDefinitionBuilder Argument(string name, string type,
            Action<IArgumentBuilder> argumentAction)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(argumentAction, nameof(argumentAction));
            var ib = Builder.Argument(name, type, ConfigurationSource.Explicit)!;
            argumentAction(ib.Definition.Builder);
            return this;
        }

        public IDirectiveDefinitionBuilder Argument<TArgument>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Argument(name, typeof(TArgument), ConfigurationSource.Explicit);
            return this;
        }


        public IDirectiveDefinitionBuilder Argument<TArgument>(string name, Action<IArgumentBuilder> argumentAction)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(argumentAction, nameof(argumentAction));
            var ib = Builder.Argument(name, typeof(TArgument), ConfigurationSource.Explicit)!;
            argumentAction(ib.Definition.Builder);
            return this;
        }


        public IDirectiveDefinitionBuilder IgnoreArgument(string name) => throw new NotImplementedException();

        public IDirectiveDefinitionBuilder UnignoreArgument(string name) => throw new NotImplementedException();
        INameBuilder INameBuilder.Name(string name) => Name(name);
    }

}