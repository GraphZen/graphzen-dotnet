using System;
using System.Collections.Generic;
using System.Text;

namespace GraphZen.Internal
{
    internal static class StringExtensions
    {
        public static string TrimEnd(this string source, string value) => !source.EndsWith(value) ? source : source.Remove(source.LastIndexOf(value, StringComparison.Ordinal));
    }
}
