#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableEnumValueDefinition : IEnumValueDefinition, IMutableAnnotatableDefinition,
        IMutableNamed,
        IMutableDescription,
        IMutableDeprecation
    {
        [CanBeNull]
        new object Value { get; set; }

        [NotNull]
        new EnumTypeDefinition DeclaringType { get; }
    }
}