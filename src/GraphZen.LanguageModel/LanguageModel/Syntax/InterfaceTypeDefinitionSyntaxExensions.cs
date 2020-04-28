// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public static class InterfaceTypeDefinitionSyntaxExensions
    {
        public static InterfaceTypeDefinitionSyntax WithSortedChildren(this InterfaceTypeDefinitionSyntax interfaceNode)
        {
            Check.NotNull(interfaceNode, nameof(interfaceNode));
            return new InterfaceTypeDefinitionSyntax(interfaceNode.Name, interfaceNode.Description,
                interfaceNode.Directives.OrderByName().ToImmutableList(),
                interfaceNode.Fields.OrderByName().ToImmutableList(),
                interfaceNode.Location);
        }
    }
}