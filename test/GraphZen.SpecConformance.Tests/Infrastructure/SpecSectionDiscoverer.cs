// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using Xunit.Abstractions;
using Xunit.Sdk;

namespace GraphZen.SpecConformance.Tests.Infrastructure;

public sealed class SpecSectionDiscoverer : ITraitDiscoverer
{
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        var arguments = traitAttribute.GetConstructorArguments().ToArray();
        var section = (string)arguments[0];
        var rule = arguments.Length > 1 ? arguments[1] as string : null;

        foreach (var parentSection in ExpandSectionHierarchy(section))
        {
            yield return new KeyValuePair<string, string>("SpecSection", parentSection);
        }

        yield return new KeyValuePair<string, string>("SpecVersion", SpecMetadata.Version);

        if (!string.IsNullOrWhiteSpace(rule))
        {
            yield return new KeyValuePair<string, string>("SpecRule", rule!);
        }
    }

    private static IEnumerable<string> ExpandSectionHierarchy(string section)
    {
        var parts = section.Split('.');
        for (var i = 1; i <= parts.Length; i++)
        {
            yield return string.Join(".", parts.Take(i));
        }
    }
}
