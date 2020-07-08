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
    public interface IMutableFieldDefinition : IFieldDefinition, IMutableAnnotatableDefinition,
        IMutableTypeReferenceDefinition,
        IMutableNamed,
        IMutableDescription,
        IMutableArgumentsDefinition,
        IMutableDeprecation
    {
        new TypeReference FieldType { get; }

        new Resolver<object, object?>? Resolver { get; }

        new IMutableFieldsDefinition DeclaringType { get; }

        bool SetFieldType(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource);
        bool SetFieldType(string type, ConfigurationSource configurationSource);
    }
}