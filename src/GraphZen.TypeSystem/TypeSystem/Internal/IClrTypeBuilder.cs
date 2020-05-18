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
    internal interface IClrTypeBuilder<out TUntypedBuilder>
    {
        TUntypedBuilder ClrType(Type clrType);
        TUntypedBuilder ClrType(Type clrType, string name);
        TUntypedBuilder RemoveClrType();
    }
}