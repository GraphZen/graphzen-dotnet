// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IScalarTypeDefinition :
        ILeafTypeDefinition, IInputDefinition, IOutputDefinition
    {
        [CanBeNull]
        LeafSerializer<object> Serializer { get; }

        [CanBeNull]
        LeafLiteralParser<object, ValueSyntax> LiteralParser { get; }

        [CanBeNull]
        LeafValueParser<object> ValueParser { get; }
    }
}