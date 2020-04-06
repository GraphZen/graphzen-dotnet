// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable enable


namespace GraphZen.Infrastructure
{
    internal static class InternalNumerics
    {
        public static bool IsNumber(object value) =>
            value is sbyte
            || value is byte
            || value is short
            || value is ushort
            || value is int
            || value is uint
            || value is long
            || value is ulong
            || value is float
            || value is double
            || value is decimal;


        public static bool IsWholeNumber(double value)
        {
            var noDecimal = Math.Abs(value % 1) <= double.Epsilon * 100;
            return noDecimal;
        }

        public static bool TryGetWholeDouble(object val, out double result)
        {
            if (IsNumber(val))
            {
                var doubleValue = Convert.ToDouble(val);
                if (IsWholeNumber(doubleValue))
                {
                    result = doubleValue;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public static bool TryConvertToInt32(double value, out int result)
        {
            result = default;

            if (IsWholeNumber(value) && value <= int.MaxValue && value >= int.MinValue)
            {
                result = (int) value;
                return true;
            }

            return false;
        }
    }
}