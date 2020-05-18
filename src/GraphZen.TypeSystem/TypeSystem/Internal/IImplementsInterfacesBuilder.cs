// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IImplementsInterfacesBuilder<out TBuilder>
    {
        TBuilder ImplementsInterface(string name);

        TBuilder ImplementsInterfaces(string name, params string[] names);
        TBuilder IgnoreInterface<T>();
        TBuilder IgnoreInterface(Type clrType);
        TBuilder IgnoreInterface(string name);
        TBuilder UnignoreInterface(string name);
    }
}