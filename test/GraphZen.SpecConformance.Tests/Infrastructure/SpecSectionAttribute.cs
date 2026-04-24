// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using Xunit.Sdk;

namespace GraphZen.SpecConformance.Tests.Infrastructure;

[TraitDiscoverer(
    "GraphZen.SpecConformance.Tests.Infrastructure.SpecSectionDiscoverer",
    "GraphZen.SpecConformance.Tests")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class SpecSectionAttribute(string section, string? rule = null) : Attribute, ITraitAttribute
{
    public string Section { get; } = section;

    public string? Rule { get; } = rule;
}
