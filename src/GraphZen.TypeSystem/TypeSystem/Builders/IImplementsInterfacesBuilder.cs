// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IImplementsInterfacesBuilder
    {
        IImplementsInterfacesBuilder ImplementsInterface(string name);

        IImplementsInterfacesBuilder ImplementsInterfaces(string name, params string[] names);
        IImplementsInterfacesBuilder IgnoreInterface<T>();
        IImplementsInterfacesBuilder IgnoreInterface(Type clrType);
        IImplementsInterfacesBuilder IgnoreInterface(string name);
        IImplementsInterfacesBuilder UnignoreInterface(string name);
    }

    public interface IImplementsInterfacesBuilder<out TBuilder> : IImplementsInterfacesBuilder
    {
        new TBuilder ImplementsInterface(string name);

        new TBuilder ImplementsInterfaces(string name, params string[] names);
        new TBuilder IgnoreInterface<T>();
        new TBuilder IgnoreInterface(Type clrType);
        new TBuilder IgnoreInterface(string name);
        new TBuilder UnignoreInterface(string name);
    }
}