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
    public partial interface IMutableArgumentsDefinition : IArgumentsDefinition, IMutableDefinition
    {
        [GenDictionaryAccessors(nameof(ArgumentDefinition.Name), "Argument")]
        IReadOnlyDictionary<string, ArgumentDefinition> Arguments { get; }

        ArgumentDefinition? GetOrAddArgument(string name, Type clrType, ConfigurationSource configurationSource);
        ArgumentDefinition? GetOrAddArgument(string name, string type, ConfigurationSource configurationSource);
        bool RemoveArgument(ArgumentDefinition argument);
        bool AddArgument(ArgumentDefinition argument);
        ConfigurationSource? FindIgnoredArgumentConfigurationSource(string name);
        new IEnumerable<ArgumentDefinition> GetArguments();
    }
}