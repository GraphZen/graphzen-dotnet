// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public static class InterfaceTypeDefinitionSyntaxExensions
    {
        public static InterfaceTypeDefinitionSyntax WithSortedChildren(this InterfaceTypeDefinitionSyntax interfaceNode)
        {
            Check.NotNull(interfaceNode, nameof(interfaceNode));
            return new InterfaceTypeDefinitionSyntax(interfaceNode.Name, interfaceNode.Description,
                interfaceNode.Directives.OrderByName().ToReadOnlyList(),
                interfaceNode.Fields.OrderByName().ToReadOnlyList(),
                interfaceNode.Location);
        }
    }
}