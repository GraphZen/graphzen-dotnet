// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.Internal;

internal class Some<T> : Maybe<T>
{
    protected Some(IReadOnlyList<object?>? values, IReadOnlyList<GraphQLServerError>? errors) : base(values,
        errors)
    {
    }


    public T Value => ValueOrFailure();
}
