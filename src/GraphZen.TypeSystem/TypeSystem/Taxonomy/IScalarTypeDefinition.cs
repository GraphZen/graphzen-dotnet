// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IScalarTypeDefinition :
        ILeafTypeDefinition, IInputDefinition, IOutputDefinition
    {
        
        LeafSerializer<object> Serializer { get; }

        
        LeafLiteralParser<object, ValueSyntax> LiteralParser { get; }

        
        LeafValueParser<object> ValueParser { get; }
    }
}