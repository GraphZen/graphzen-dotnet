#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem
{
    public interface ISyntaxConverter
    {
        bool CanRead { get; }
        bool CanWrite { get; }
        object FromSyntax(SyntaxNode node);
        SyntaxNode ToSyntax(object value);
    }
}