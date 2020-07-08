// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    internal interface IArgumentsDefinitionBuilder<out TBuilder>
    {
        TBuilder RemoveArgument(string name);
        TBuilder Argument(string name, Action<ArgumentBuilder<object?>> configurator);
        ArgumentBuilder<object?> Argument(string name);
        TBuilder Argument(string name, string type);
        TBuilder Argument(string name, string type, Action<ArgumentBuilder<object?>> configurator);

        TBuilder Argument<TArgument>(string name);
        TBuilder Argument<TArgument>(string name, Action<ArgumentBuilder<TArgument>> configurator);

        TBuilder IgnoreArgument(string name);
        TBuilder UnignoreArgument(string name);
    }
}