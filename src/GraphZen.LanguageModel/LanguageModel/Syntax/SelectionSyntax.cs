// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Selection
    ///     http://facebook.github.io/graphql/June2018/#Selection
    /// </summary>
    public abstract class SelectionSyntax : SyntaxNode, IDirectivesSyntax
    {
        protected SelectionSyntax(SyntaxLocation location) : base(location)
        {
        }

        public abstract IReadOnlyList<DirectiveSyntax> Directives { get; }
    }
}