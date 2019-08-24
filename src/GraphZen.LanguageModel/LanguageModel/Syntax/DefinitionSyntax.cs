#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Executable (operation/fragment) or type system definitions
    ///     http://facebook.github.io/graphql/June2018/#Definition
    /// </summary>
    public abstract class DefinitionSyntax : SyntaxNode
    {
        protected DefinitionSyntax(SyntaxLocation location) : base(location)
        {
        }
    }
}