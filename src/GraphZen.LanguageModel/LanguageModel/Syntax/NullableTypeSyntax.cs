// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     All types except non-null type
    /// </summary>
    public abstract class NullableTypeSyntax : TypeSyntax
    {
        protected NullableTypeSyntax(SyntaxLocation location) : base(location)
        {
        }
    }
}