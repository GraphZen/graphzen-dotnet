// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower.Model;

#nullable disable


namespace GraphZen.LanguageModel.Internal
{
    internal static class TextSpanExtensions
    {
        internal static SyntaxLocation ToLocation(this TextSpan span)
        {
            var start = span.Position.Absolute;
            var end = start + span.Length;
            return new SyntaxLocation(start, end, span.Position.Line, span.Position.Column, new Source(span.Source));
        }
    }
}