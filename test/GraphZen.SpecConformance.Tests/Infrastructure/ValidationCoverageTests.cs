// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Reflection;

namespace GraphZen.SpecConformance.Tests.Infrastructure;

public class ValidationCoverageTests
{
    [Fact]
    public void validation_manifest_sections_have_a_conformance_class()
    {
        var discoveredSections = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .SelectMany(type => type.GetCustomAttributes<SpecSectionAttribute>())
            .Select(attribute => attribute.Section)
            .ToHashSet(StringComparer.Ordinal);

        var missingSections = SpecCoverageManifest.ValidationSections
            .Where(section => !discoveredSections.Contains(section))
            .ToArray();

        Assert.True(missingSections.Length == 0,
            "Missing conformance classes for sections: " + string.Join(", ", missingSections));
    }
}
