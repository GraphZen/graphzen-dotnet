// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


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


        internal static IReadOnlyList<ValueSyntax> EmptyValuesCollection { get; } =
            Array.AsReadOnly(new ValueSyntax[] { });


        public abstract object GetValue();
    }
}