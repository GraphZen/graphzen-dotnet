// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Reflection;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IInputFieldDefinition : IInputValueDefinition
    {
        new PropertyInfo ClrInfo { get; }

        new IInputObjectTypeDefinition DeclaringMember { get; }
    }
}