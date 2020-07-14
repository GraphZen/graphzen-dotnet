// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IArgumentBuilder :
        IInfrastructure<InternalArgumentBuilder>,
        IInfrastructure<MutableArgument>,
        IDirectivesBuilder<IArgumentBuilder>
    {
        IArgumentBuilder ArgumentType<T>();
        IArgumentBuilder ArgumentType(Type clrType);
        IArgumentBuilder ArgumentType(string type);
        IArgumentBuilder DefaultValue(object? value);
        IArgumentBuilder RemoveDefaultValue();
    }
}