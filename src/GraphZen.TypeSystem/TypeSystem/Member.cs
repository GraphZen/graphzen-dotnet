// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public abstract class Member : ISyntaxConvertable
    {
        [GraphQLCanBeNull]
        public abstract string Description { get; }

        [GraphQLIgnore]
        public abstract SyntaxNode ToSyntaxNode();
    }
}