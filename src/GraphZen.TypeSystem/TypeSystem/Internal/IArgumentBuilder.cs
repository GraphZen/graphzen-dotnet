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
    internal interface IArgumentBuilder<T> : IInfrastructure<InternalArgumentBuilder>,
        IAnnotableBuilder<ArgumentBuilder<T>>
    {
        ArgumentBuilder<TNew> ArgumentType<TNew>();
        ArgumentBuilder<object?> ArgumentType(Type clrType);
        ArgumentBuilder<object?> ArgumentType(string type);
    }
}