// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IEnumValueDefinition : IAnnotatableDefinition, INamed, IDeprecation, IInputDefinition,
        IOutputDefinition
    {
        object Value { get; }


        [GraphQLIgnore] IEnumTypeDefinition DeclaringType { get; }
    }
}