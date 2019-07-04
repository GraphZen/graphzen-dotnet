// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IInputValueDefinition : IAnnotatableDefinition, INamed, IInputDefinition, IClrInfo
    {
        [NotNull]
        IGraphQLTypeReference InputType { get; }

        [NotNull]
        IMemberDefinition DeclaringMember { get; }

        [CanBeNull]
        object DefaultValue { get; }

        bool HasDefaultValue { get; }
    }
}