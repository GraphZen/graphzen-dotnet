// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IArgumentsBuilder
    {
        IArgumentsBuilder RemoveArgument(string name);
        IArgumentsBuilder ClearArguments();
        IArgumentsBuilder Argument(string name, Action<IArgumentBuilder> argumentAction);
        IArgumentBuilder Argument(string name);
        IArgumentsBuilder Argument(string name, string type);
        IArgumentsBuilder Argument(string name, string type, Action<IArgumentBuilder> argumentAction);
        IArgumentsBuilder Argument(string name, Type clrType);
        IArgumentsBuilder Argument(string name, Type clrType, Action<IArgumentBuilder> argumentAction);
        IArgumentsBuilder Argument<TArgument>(string name);
        IArgumentsBuilder Argument<TArgument>(string name, Action<IArgumentBuilder> argumentAction);
        IArgumentsBuilder IgnoreArgument(string name);
        IArgumentsBuilder UnignoreArgument(string name);
    }

    public interface IArgumentsBuilder<out TBuilder> : IArgumentsBuilder
    {
        new TBuilder RemoveArgument(string name);
        new TBuilder ClearArguments();
        new TBuilder Argument(string name, Action<IArgumentBuilder> argumentAction);
        new TBuilder Argument(string name, string type);
        new TBuilder Argument(string name, string type, Action<IArgumentBuilder> argumentAction);

        new TBuilder Argument<TArgument>(string name);
        new TBuilder Argument<TArgument>(string name, Action<IArgumentBuilder> argumentAction);
        new TBuilder Argument(string name, Type clrType);
        new TBuilder Argument(string name, Type clrType, Action<IArgumentBuilder> argumentAction);
        new TBuilder IgnoreArgument(string name);
        new TBuilder UnignoreArgument(string name);
    }
}