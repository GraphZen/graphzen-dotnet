// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.Language;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public abstract class SyntaxConverter : ISyntaxConverter
    {
        public virtual bool CanRead { get; } = false;
        public virtual bool CanWrite { get; } = false;
        public virtual object FromSyntax(SyntaxNode node) => throw new NotImplementedException();
        public virtual SyntaxNode ToSyntax(object value) => throw new NotImplementedException();
    }
}