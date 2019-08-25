// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class SyntaxConverter : ISyntaxConverter
    {
        public virtual bool CanRead { get; } = false;
        public virtual bool CanWrite { get; } = false;

        public virtual object FromSyntax(SyntaxNode node)
        {
            throw new NotImplementedException();
        }

        public virtual SyntaxNode ToSyntax(object value)
        {
            throw new NotImplementedException();
        }
    }
}