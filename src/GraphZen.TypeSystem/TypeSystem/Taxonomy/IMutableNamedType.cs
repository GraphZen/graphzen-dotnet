// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMutableNamedTypeDefinition :
        IBuildableNamedTypeDefinition,
        IMutableDirectives,
        IMutableClrType,
        IMutableName,
        IMutableDescription
    {
        new INamedTypeDefinitionBuilder Builder { get; }
    }
}