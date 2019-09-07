// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IArgumentsDefinitionBuilder<out TBuilder>
    {
        TBuilder Argument(string name, Action<InputValueBuilder> configurator);
        InputValueBuilder Argument(string name);

        TBuilder Argument(string name, string type, Action<InputValueBuilder>? configurator = null);


        TBuilder Argument<TArgument>(string name, Action<InputValueBuilder>? configurator = null);


        TBuilder IgnoreArgument(string name);


        TBuilder UnignoreArgument(string name);
    }
}