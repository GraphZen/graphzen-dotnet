// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Type system extension
    ///     http://facebook.github.io/graphql/June2018/#TypeSystemExtension
    /// </summary>
    public abstract class TypeSystemExtensionSyntax : DefinitionSyntax
    {
        protected TypeSystemExtensionSyntax(SyntaxLocation location) : base(location)
        {
        }
    }
}