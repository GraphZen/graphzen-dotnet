// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{
    public interface IInfrastructure<out T>
    {
        [NotNull]
        T Instance { get; }
    }
}