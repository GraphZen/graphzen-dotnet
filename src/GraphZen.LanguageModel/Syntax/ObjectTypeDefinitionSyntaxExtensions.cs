// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    [UsedImplicitly]
    public static class ObjectTypeDefinitionSyntaxExtensions
    {
        public static ObjectTypeDefinitionSyntax WithSortedChildren(this ObjectTypeDefinitionSyntax objectNode)
        {
            Check.NotNull(objectNode, nameof(objectNode));
            return new ObjectTypeDefinitionSyntax(objectNode.Name, objectNode.Description,
                NamedSyntaxExtensions.OrderByName<NamedTypeSyntax>(objectNode.Interfaces).ToReadOnlyList(),
                NamedSyntaxExtensions.OrderByName<DirectiveSyntax>(objectNode.Directives).ToReadOnlyList(),
                NamedSyntaxExtensions.OrderByName<FieldDefinitionSyntax>(objectNode.Fields)
                    .ToReadOnlyList(), objectNode.Location);
        }
    }
}