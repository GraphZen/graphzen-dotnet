// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IArgumentsContainer : IArgumentsContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, Argument> Arguments { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<Argument> GetArguments();
    }
}