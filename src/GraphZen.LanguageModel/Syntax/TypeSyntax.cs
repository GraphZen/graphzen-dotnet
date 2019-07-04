// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Type reference
    ///     http://facebook.github.io/graphql/June2018/#Type
    /// </summary>
    public abstract class TypeSyntax : SyntaxNode
    {
        protected TypeSyntax(SyntaxLocation location) : base(location)
        {
        }

        [NotNull]
        public LanguageModel.NamedTypeSyntax GetNamedType()
        {
            TypeSyntax GetNamedType(TypeSyntax node)
            {
                switch (node)
                {
                    case LanguageModel.NamedTypeSyntax named:
                        return named;
                    case LanguageModel.ListTypeSyntax lt:
                        return GetNamedType(lt.OfType);
                    case LanguageModel.NonNullTypeSyntax nn:
                        return GetNamedType(nn.OfType);
                }

                throw new Exception($"Unable to identity named type for node {this}");
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            return (LanguageModel.NamedTypeSyntax) GetNamedType(this);
        }
    }
}