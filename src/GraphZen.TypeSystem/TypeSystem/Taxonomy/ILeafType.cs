// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface ILeafType : INamedTypeDefinition
    {
        Maybe<object?> Serialize(object value);
        bool IsValidValue(string value);
        bool IsValidLiteral(ValueSyntax value);
        Maybe<object?> ParseValue(object value);
        Maybe<object?> ParseLiteral(ValueSyntax value);
    }
}