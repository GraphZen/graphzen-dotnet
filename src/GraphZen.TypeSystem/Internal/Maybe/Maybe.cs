// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Internal
{
    internal static class Maybe
    {
        internal static Maybe<T> Some<T>(T value)
        {
            return new SomeImpl<T>(new object?[] { value }, null);
        }


        internal static Maybe<T> None<T>(params GraphQLError[] errors)
        {
            errors = Check.NotNull(errors, nameof(errors));
            return new NoneImpl<T>(errors);
        }


        internal static Maybe<T> None<T>(IEnumerable<GraphQLError> errors)
        {
            errors = Check.NotNull(errors, nameof(errors));
            return new NoneImpl<T>(errors.ToArray());
        }


        internal static Maybe<T> None<T>(string errorMessage)
        {
            return None<T>(new GraphQLError(errorMessage));
        }


        private class NoneImpl<T> : None<T>
        {
            internal NoneImpl(IReadOnlyList<GraphQLError> errors) : base(null, errors)
            {
            }
        }

        private class SomeImpl<T> : Some<T>
        {
            internal SomeImpl(IReadOnlyList<object?> values, IReadOnlyList<GraphQLError>? errors) : base(
                values, errors)
            {
            }
        }
    }


    public class Maybe<T>
    {
        private readonly IReadOnlyList<object?> _values;

        protected Maybe(IReadOnlyList<object?>? values, IReadOnlyList<GraphQLError>? errors)
        {
            _values = values ?? Array.AsReadOnly(new object?[] { });
            Errs = errors ?? Array.AsReadOnly(new GraphQLError[] { });
        }


        protected IReadOnlyList<GraphQLError> Errs { get; }

        public bool HasValue => _values.Any();

        private bool Equals(Maybe<T> other)
        {
            if (this is Some<T> thisSome && other is Some<T> otherSome) return Equals(thisSome.Value, otherSome.Value);

            if (this is None<T> && other is None<T>) return true;

            return false;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((Maybe<T>)obj);
        }

        public override int GetHashCode()
        {
            return _values.GetHashCode();
        }


        public Maybe<TC> Cast<TC>()
        {
            return HasValue ? Maybe.Some((TC)_values.Single()!) : Maybe.None<TC>();
        }


        internal Maybe<TP> Select<TP>(Func<T, TP> selector)
        {
            Check.NotNull(selector, nameof(selector));
            if (HasValue)
            {
                var wrapped = selector(ValueOrFailure());
                return Maybe.Some(wrapped);
            }

            return Maybe.None<TP>();
        }


        protected T ValueOrFailure()
        {
            if (HasValue) return (T)_values.Single()!;

            throw new InvalidOperationException("Maybe does not have a value");
        }
    }
}