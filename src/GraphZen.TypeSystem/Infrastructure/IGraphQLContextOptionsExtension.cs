// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace GraphZen.Infrastructure
{
    public interface IGraphQLContextOptionsExtension
    {
        void ApplyServices(IServiceCollection services);
        void Validate(IGraphQLContextOptions options);
    }
}