// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;

using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableClrType : IClrType
    {
        bool SetClrType(Type clrType, ConfigurationSource configurationSource);
        ConfigurationSource GetClrTypeConfigurationSource();
    }
}