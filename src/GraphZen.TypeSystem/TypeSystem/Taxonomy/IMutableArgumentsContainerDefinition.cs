#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableArgumentsContainerDefinition : IArgumentsContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, ArgumentDefinition> Arguments { get; }

        bool RenameArgument([NotNull] ArgumentDefinition argument, [NotNull] string name,
            ConfigurationSource configurationSource);

        [NotNull]
        [ItemNotNull]
        new IEnumerable<ArgumentDefinition> GetArguments();
    }
}