// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IArgumentDefinition : IInputValueDefinition
    {
        new IArgumentsDefinition DeclaringMember { get; }

        new ParameterInfo? ClrInfo { get; }
    }
}