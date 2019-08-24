#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Input value
    ///     http://facebook.github.io/graphql/June2018/#Value
    /// </summary>
    public abstract class ValueSyntax : SyntaxNode
    {
        protected ValueSyntax(SyntaxLocation location) : base(location)
        {
        }

        [NotNull]
        internal static IReadOnlyList<ValueSyntax> EmptyValuesCollection { get; } =
            Array.AsReadOnly(new ValueSyntax[] { });


        public abstract object GetValue();
    }
}