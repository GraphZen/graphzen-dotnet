// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Primitives;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class ObjectAssertionExtensions
    {
        [UsedImplicitly]
        public static AndConstraint<ReferenceTypeAssertions<TSubject, TAssertions>>
            BeEquivalentToJson<TSubject, TAssertions>(this ReferenceTypeAssertions<TSubject, TAssertions> assertions,
                object expected, ResultComparisonOptions? comparisonOptionsAction = null)
            where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions>
        {
            var diff = JsonDiffer.GetDiff(expected, assertions.Subject.As<object>(), comparisonOptionsAction);


            return new AndConstraint<ReferenceTypeAssertions<TSubject, TAssertions>>(assertions);
        }

        [UsedImplicitly]
        public static
            AndConstraint<ReferenceTypeAssertions<TSubject, TAssertions>>
            BeEquivalentToJson<TSubject, TAssertions>(this ReferenceTypeAssertions<TSubject, TAssertions> assertions,
                object expected, Action<ResultComparisonOptions>? comparisonOptionsAction)
            where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions>
        {
            var options = ResultComparisonOptions.FromOptionsAction(comparisonOptionsAction);
            return BeEquivalentToJson(assertions, expected, options);
        }

        [UsedImplicitly]
        public static AndConstraint<ObjectAssertions> BeEquivalentToJson(this ObjectAssertions objectAssertions,
            string expectedJson) => throw new NotImplementedException();
    }
}