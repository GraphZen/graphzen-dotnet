// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public partial interface IMutableArguments : IBuildableArguments, IMutableMember
    {
        IArgument? GetOrAddArgument(string name, Type clrType, ConfigurationSource configurationSource);
        IArgument? GetOrAddArgument(string name, string type, ConfigurationSource configurationSource);
        bool RemoveArgument(IArgument argument);
        bool AddArgument(IArgument argument);
        ConfigurationSource? FindIgnoredArgumentConfigurationSource(string name);
    }
}