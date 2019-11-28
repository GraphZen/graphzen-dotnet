// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace GraphZen.Infrastructure
{
    public static class ReferenceTypeAssertionsExtensions
    {
        [UsedImplicitly]
        public static AndConstraint<ReferenceTypeAssertions<TSubject, TAssertions>>
            BeEquivalentToJson<TSubject, TAssertions>(this ReferenceTypeAssertions<TSubject, TAssertions> assertions,
                object expected, JsonDiffOptions? options = null)
            where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions>
        {
            var diff = JsonDiffer.GetDiff(assertions.Subject.As<object>(), expected, options);

            Execute.Assertion.ForCondition(diff == null).FailWith(() => new FailReason(diff!.EscapeCurlyBraces()));


            return new AndConstraint<ReferenceTypeAssertions<TSubject, TAssertions>>(assertions);
        }

        [UsedImplicitly]
        public static
            AndConstraint<ReferenceTypeAssertions<TSubject, TAssertions>>
            BeEquivalentToJson<TSubject, TAssertions>(this ReferenceTypeAssertions<TSubject, TAssertions> assertions,
                object expected, Action<JsonDiffOptions>? optionsAction)
            where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions>
        {
            var options = JsonDiffOptions.FromOptionsAction(optionsAction);
            return BeEquivalentToJson(assertions, expected, options);
        }

        [UsedImplicitly]
        public static AndConstraint<ObjectAssertions> BeEquivalentToJson(this ObjectAssertions assertions,
            string expectedJson, JsonDiffOptions? options)
        {
            var expectedJObj = JObject.Parse(expectedJson);
            var diff = JsonDiffer.GetDiff(assertions.Subject, expectedJObj, options);


            throw new NotImplementedException();
        }
    }
}