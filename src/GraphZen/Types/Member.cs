// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Language;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public abstract class Member : ISyntaxConvertable
    {
        [GraphQLCanBeNull]
        public abstract string Description { get; }

        [GraphQLIgnore]
        public abstract SyntaxNode ToSyntaxNode();
    }
}