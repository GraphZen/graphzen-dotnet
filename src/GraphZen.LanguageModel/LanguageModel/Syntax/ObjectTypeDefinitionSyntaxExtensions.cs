// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
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
                objectNode.Interfaces.OrderByName().ToImmutableList(),
                objectNode.Directives.OrderByName().ToImmutableList(),
                objectNode.Fields.OrderByName()
                    .ToImmutableList(), objectNode.Location);
        }
    }
}