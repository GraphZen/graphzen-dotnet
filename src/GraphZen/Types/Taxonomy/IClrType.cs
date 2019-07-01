// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [GraphQLIgnore]
    public interface IClrType
    {
        [CanBeNull]
        Type ClrType { get; }
    }

    public interface IMutableClrType : IClrType
    {
        bool SetClrType(Type clrType, ConfigurationSource configurationSource);
    }

    [GraphQLIgnore]
    public interface IClrInfo
    {
        [CanBeNull]
        object ClrInfo { get; }
    }
}