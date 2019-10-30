// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IDirectiveBuilder<TDirective>
    {
        IDirectiveBuilder<TDirective> Description(string description);
        IDirectiveBuilder<TDirective> Name(string name);


        IDirectiveBuilder<TDirective> Locations(params DirectiveLocation[] locations);


        IDirectiveBuilder<TDirective> Argument(string name, string type,
            Action<InputValueBuilder>? configurator = null);


        IDirectiveBuilder<TDirective> Argument<TArg>(string name,
            Action<InputValueBuilder>? configurator = null);
    }
}