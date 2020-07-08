// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IMutableArgumentDefinition : IArgumentDefinition, IMutableInputValueDefinition
    {
        new TypeReference ArgumentType { get; }
        new IMutableArgumentsDefinition DeclaringMember { get; }
        bool SetArgumentType(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource);
        bool SetArgumentType(string type, ConfigurationSource configurationSource);
    }
}