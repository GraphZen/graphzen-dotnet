// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IInputFieldDefinition : IInputValueDefinition
    {
        new PropertyInfo? ClrInfo { get; }

        IGraphQLTypeReference FieldType { get; }
        new IInputObjectTypeDefinition DeclaringMember { get; }
    }
}