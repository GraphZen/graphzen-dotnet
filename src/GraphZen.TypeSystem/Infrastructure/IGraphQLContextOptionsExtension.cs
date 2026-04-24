// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace GraphZen.Infrastructure;

public interface IGraphQLContextOptionsExtension
{
    void ApplyServices(IServiceCollection services);
    void Validate(IGraphQLContextOptions options);
}