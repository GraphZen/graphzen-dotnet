// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable

namespace GraphZen.TypeSystem.Internal
{
    public struct ParseResult<T>
    {
        private readonly T _value;
        public bool HasValue { get; }

        public T Value => HasValue ? _value : throw new InvalidOperationException("Result has no value");

        internal ParseResult(object value, bool hasValue)
        {
            _value = (T)value;
            HasValue = hasValue;
        }


        public ParseResult<TNew> Cast<TNew>() => new ParseResult<TNew>(_value, HasValue);
    }

    public static class ParseResult
    {
        public static ParseResult<TInner> FromValue<TInner>(TInner value) => new ParseResult<TInner>(value, true);

        public static ParseResult<TInner> Empty<TInner>() => new ParseResult<TInner>(default(TInner), false);
    }
}