// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [GraphQLIgnore]
    public interface IInputFieldDefinition : IInputValueDefinition
    {
        new PropertyInfo ClrInfo { get; }

        new IInputObjectTypeDefinition DeclaringMember { get; }
    }
}