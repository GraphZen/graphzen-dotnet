// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    public partial class PunctuatorSyntax : SyntaxNode
    {
        public PunctuatorSyntax(SyntaxLocation location) : base(location)
        {
        }

        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();
    }
}