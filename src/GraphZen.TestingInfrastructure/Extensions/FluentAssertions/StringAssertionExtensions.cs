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
            ResultComparisonOptions? options)
        {
            var diff = StringDiffer.GetDiff(expected, assertions.Subject, options);

            Execute.Assertion
                .ForCondition(diff == null)
                .FailWith(() => new FailReason(diff!.EscapeCurlyBraces()));

            return new AndConstraint<StringAssertions>(assertions);
        }


        public static AndConstraint<StringAssertions> Be(this StringAssertions assertions, string expected,
            Action<ResultComparisonOptions>? comparisonOptionsAction)
        {
            var options = ResultComparisonOptions.FromOptionsAction(comparisonOptionsAction);
            return assertions.Be(expected, options);
        }

        public static AndConstraint<StringAssertions> Be(this StringAssertions assertions, string expected,
            bool showDiff) => showDiff
            ? assertions.Be(expected, (Action<ResultComparisonOptions>?) null)
            : assertions.Be(expected);
    }
}