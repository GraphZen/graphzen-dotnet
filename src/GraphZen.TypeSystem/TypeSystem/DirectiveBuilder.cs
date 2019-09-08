// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
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

        public IDirectiveBuilder<TDirective> Argument(string name, string type, Action<InputValueBuilder> configurator = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            var argBuilder = Builder.Argument(name, ConfigurationSource.Explicit).Type(type);
            configurator?.Invoke(new InputValueBuilder(argBuilder));
            return this;
        }

        public IDirectiveBuilder<TDirective> Argument<TArg>(string name, Action<InputValueBuilder> configurator = null) =>
            throw new NotImplementedException();
    }
}