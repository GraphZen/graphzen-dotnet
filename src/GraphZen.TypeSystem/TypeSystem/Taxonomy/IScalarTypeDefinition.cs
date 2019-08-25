// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IScalarTypeDefinition :
        ILeafTypeDefinition, IInputDefinition, IOutputDefinition
    {
        LeafSerializer<object>? Serializer { get; }


        LeafLiteralParser<object, ValueSyntax>? LiteralParser { get; }


        LeafValueParser<object>? ValueParser { get; }
    }
}