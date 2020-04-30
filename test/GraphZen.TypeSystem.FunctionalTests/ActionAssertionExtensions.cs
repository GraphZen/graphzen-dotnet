// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions.Specialized;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.FunctionalTests
{
    public static class ActionAssertionExtensions
    {
        public static ExceptionAssertions<InvalidNameException> ThrowInvalidNameArgument(
            this ActionAssertions actionAssertions,
            string name, string reason) =>
            actionAssertions.Throw<ArgumentException>()
                .WithMessage(GraphQLName.GetInvalidNameErrorMessage(name) + " (Parameter 'name')", reason)
                .WithInnerException<InvalidNameException>()
                .WithMessage(GraphQLName.GetInvalidNameErrorMessage(name), reason);


        public static ExceptionAssertions<ArgumentNullException>
            ThrowArgumentNullException(this ActionAssertions actionAssertions, string paramName) => actionAssertions
            .Throw<ArgumentNullException>().WithMessage($"*Parameter '{paramName}'*");
    }
}