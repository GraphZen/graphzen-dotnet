// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public partial interface IArguments : IArgumentsDefinition, IMember
    {
        new IReadOnlyCollection<Argument> Arguments { get; }

        [GenDictionaryAccessors(nameof(Argument.Name), nameof(Argument))]
        IReadOnlyDictionary<string, Argument> ArgumentMap { get; }
    }
}