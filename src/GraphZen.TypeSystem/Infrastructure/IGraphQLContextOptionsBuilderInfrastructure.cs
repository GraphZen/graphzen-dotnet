// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.Infrastructure;

public interface IGraphQLContextOptionsBuilderInfrastructure
{
    void AddOrUpdateExtension<TExtension>([NotNull] TExtension extension)
        where TExtension : class, IGraphQLContextOptionsExtension;
}