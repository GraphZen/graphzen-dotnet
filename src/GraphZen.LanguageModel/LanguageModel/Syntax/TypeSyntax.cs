// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.LanguageModel;

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
        TypeSyntax FindNamedType(TypeSyntax node)
        {
            switch (node)
            {
                case NamedTypeSyntax named:
                    return named;
                case ListTypeSyntax lt:
                    return FindNamedType(lt.OfType);
                case NonNullTypeSyntax nn:
                    return FindNamedType(nn.OfType);
            }

            throw new Exception($"Unable to identity named type for node {this}");
        }


        return (NamedTypeSyntax)FindNamedType(this);
    }
}