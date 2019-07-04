// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;


namespace GraphZen.Utilities
{
    internal class Some<T> : Maybe<T>
    {
        protected Some(IReadOnlyList<object> values, IReadOnlyList<GraphQLError> errors) : base(values,
            errors)
        {
        }

        [CanBeNull]
        public T Value => ValueOrFailure();
    }
}