// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public partial interface IMutableArgumentsDefinition : IArgumentsDefinition
    {
        [GenDictionaryAccessors(nameof(ArgumentDefinition.Name), "Argument")]
        IReadOnlyDictionary<string, ArgumentDefinition> Arguments { get; }

        bool RemoveArgument(ArgumentDefinition argument);
        bool AddArgument(ArgumentDefinition argument);
        new IEnumerable<ArgumentDefinition> GetArguments();
    }
}