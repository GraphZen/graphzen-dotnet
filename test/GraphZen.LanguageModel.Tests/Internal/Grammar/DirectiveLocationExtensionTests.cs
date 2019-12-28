// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.LanguageModel.Tests.Internal.Grammar
{
    public class DirectiveLocationExtensionTests
    {
        [Fact]
        public void get_display_value_should_not_be_null_for_all_values()
        {
            foreach (var value in Enum.GetValues(typeof(DirectiveLocation)).Cast<DirectiveLocation>())
            {
                value.GetDisplayValue().Should().NotBeNull();
            }
        }
    }
}