// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
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
        protected TypeSyntax(SyntaxLocation? location) : base(location)
        {
        }


        public NamedTypeSyntax GetNamedType()
        {
            TypeSyntax GetNamedType(TypeSyntax node)
            {
                switch (node)
                {
                    case NamedTypeSyntax named:
                        return named;
                    case ListTypeSyntax lt:
                        return GetNamedType(lt.OfType);
                    case NonNullTypeSyntax nn:
                        return GetNamedType(nn.OfType);
                }

                throw new Exception($"Unable to identity named type for node {this}");
            }


            return (NamedTypeSyntax)GetNamedType(this);
        }
    }
}