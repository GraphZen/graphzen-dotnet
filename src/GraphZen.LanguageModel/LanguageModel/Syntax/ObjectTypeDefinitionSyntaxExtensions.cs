#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    [UsedImplicitly]
    public static class ObjectTypeDefinitionSyntaxExtensions
    {
        public static ObjectTypeDefinitionSyntax WithSortedChildren(this ObjectTypeDefinitionSyntax objectNode)
        {
            Check.NotNull(objectNode, nameof(objectNode));
            return new ObjectTypeDefinitionSyntax(objectNode.Name, objectNode.Description,
                objectNode.Interfaces.OrderByName().ToReadOnlyList(),
                objectNode.Directives.OrderByName().ToReadOnlyList(),
                objectNode.Fields.OrderByName()
                    .ToReadOnlyList(), objectNode.Location);
        }
    }
}