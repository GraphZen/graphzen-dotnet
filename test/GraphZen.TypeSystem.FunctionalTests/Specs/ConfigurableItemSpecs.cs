// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.FunctionalTests.Specs
{
    [Description("Hello")]
    [DisplayName("Test 2")]
    public class ConfigurableItemSpecs
    {
        [Description("Hello2")] public const string Hello = nameof(Hello);
    }
}