#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableScalarTypeDefinition : IScalarTypeDefinition, IMutableGraphQLTypeDefinition
    {
        [CanBeNull]
        new LeafSerializer<object> Serializer { get; set; }

        [CanBeNull]
        new LeafLiteralParser<object, ValueSyntax> LiteralParser { get; set; }

        [CanBeNull]
        new LeafValueParser<object> ValueParser { get; set; }
    }
}