// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Types.Builders.Internal;
using JetBrains.Annotations;
using static GraphZen.Types.Internal.ConfigurationSource;

namespace GraphZen.Types.Builders
{
    public class DirectiveBuilder : IDirectiveBuilder
    {
        public DirectiveBuilder([NotNull] InternalDirectiveBuilder builder)
        {
            Builder = builder;
        }

        [NotNull]
        private InternalDirectiveBuilder Builder { get; }

        public IDirectiveBuilder Description(string description)
        {
            Builder.Description(description, Explicit);
            return this;
        }

        public IDirectiveBuilder Locations(params DirectiveLocation[] locations)
        {
            Builder.Locations(locations);
            return this;
        }

        public IDirectiveBuilder Argument(string name, string type, Action<InputValueBuilder> argumentBuilder = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            var argBuilder = Builder.Argument(name, Explicit).Type(type);
            argumentBuilder?.Invoke(new InputValueBuilder(argBuilder));
            return this;
        }

        public IDirectiveBuilder Argument<TArg>(string name, Action<InputValueBuilder> argumentBuilder = null) =>
            throw new NotImplementedException();
    }
}