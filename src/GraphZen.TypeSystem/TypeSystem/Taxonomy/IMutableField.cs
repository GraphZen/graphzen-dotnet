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
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMutableField : IBuildableField, IMutableDirectives,
        IMutableName,
        IMutableDescription,
        IMutableArguments,
        IMutableMaybeDeprecated
    {
        new IFieldBuilder Builder { get; }
        bool SetFieldType(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource);
        bool SetFieldType(string type, ConfigurationSource configurationSource);
    }
}