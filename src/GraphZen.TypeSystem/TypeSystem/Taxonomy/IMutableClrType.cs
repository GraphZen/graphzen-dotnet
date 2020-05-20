// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableClrType : IClrType
    {
        bool SetClrType(Type clrType, bool inferName, ConfigurationSource configurationSource);
        bool RemoveClrType(ConfigurationSource configurationSource);
        ConfigurationSource? GetClrTypeConfigurationSource();
    }
}