// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IClrTypeBuilder
    {
        IClrTypeBuilder ClrType(Type clrType, bool inferName = false);
        IClrTypeBuilder ClrType(Type clrType, string name);
        IClrTypeBuilder RemoveClrType();
    }

    public interface IClrTypeBuilder<out TUntypedBuilder> : IClrTypeBuilder
    {
        new TUntypedBuilder ClrType(Type clrType, bool inferName = false);
        new TUntypedBuilder ClrType(Type clrType, string name);
        new TUntypedBuilder RemoveClrType();
    }
}