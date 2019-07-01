// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.Language;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [GraphQLIgnore]
    public interface IDirectiveDefinition : INamed, IArgumentsContainerDefinition
    {
        [NotNull]
        IReadOnlyList<DirectiveLocation> Locations { get; }
    }
}