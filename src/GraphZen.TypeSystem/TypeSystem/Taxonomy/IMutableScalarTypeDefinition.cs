// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableScalarTypeDefinition : IScalarTypeDefinition, IMutableNamedTypeDefinition
    {
        new LeafSerializer<object>? Serializer { get; set; }


        new LeafLiteralParser<object, ValueSyntax>? LiteralParser { get; set; }


        new LeafValueParser<object>? ValueParser { get; set; }
    }
}