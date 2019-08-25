// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Internal
{
    internal class None<T> : Maybe<T>
    {
        protected None(IReadOnlyList<object>? values, IReadOnlyList<GraphQLError> errors) : base(values,
            errors)
        {
        }


        public IReadOnlyList<GraphQLError> Errors => Errs;

        public override string ToString()
        {
            var errors = Errors.Any() ? Errors.Inspect() : "(no errors)";
            return $"None {errors}";
        }

        public void ThrowFirstErrorOrDefault(string defaultError = "Unkown error")
        {
            Errors.SingleOrDefault()?.Throw();
            throw new GraphQLException(defaultError);
        }
    }
}