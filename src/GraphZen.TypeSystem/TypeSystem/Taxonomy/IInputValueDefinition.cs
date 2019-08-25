// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IInputValueDefinition : IAnnotatableDefinition, INamed, IInputDefinition, IClrInfo
    {
        IGraphQLTypeReference? InputType { get; }


        IMemberDefinition DeclaringMember { get; }


        object? DefaultValue { get; }

        bool HasDefaultValue { get; }
    }
}