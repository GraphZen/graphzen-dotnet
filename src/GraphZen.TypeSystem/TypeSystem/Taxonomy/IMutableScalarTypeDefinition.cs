// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableScalarTypeDefinition : IScalarTypeDefinition, IMutableGraphQLTypeDefinition
    {
        
        new LeafSerializer<object> Serializer { get; set; }

        
        new LeafLiteralParser<object, ValueSyntax> LiteralParser { get; set; }

        
        new LeafValueParser<object> ValueParser { get; set; }
    }
}