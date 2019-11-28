// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class StringAssertionExtensions
    {
        public static AndConstraint<StringAssertions> Be(this StringAssertions assertions, string expected,
            StringDiffOptions? options)
        {
            var diff = assertions.Subject.GetDiff(expected, options);

            Execute.Assertion
                .ForCondition(diff == null)
                .FailWith(() => new FailReason(diff!.EscapeCurlyBraces()));

            return new AndConstraint<StringAssertions>(assertions);
        }


        public static AndConstraint<StringAssertions> Be(this StringAssertions assertions, string expected,
            Action<StringDiffOptions>? comparisonOptionsAction)
        {
            var options = StringDiffOptions.FromOptionsAction(comparisonOptionsAction);
            return assertions.Be(expected, options);
        }

        public static AndConstraint<StringAssertions> Be(this StringAssertions assertions, string expected,
            bool showDiff) => showDiff
            ? assertions.Be(expected, (Action<StringDiffOptions>?)null)
            : assertions.Be(expected);
    }
}