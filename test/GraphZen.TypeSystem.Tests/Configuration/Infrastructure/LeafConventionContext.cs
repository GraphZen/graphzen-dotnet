// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.Infrastructure;

public class LeafConventionContext
{
    public string? ParentName { get; set; }

    public object? DataAnnotationValue { get; set; }
}