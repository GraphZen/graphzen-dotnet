// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ILeafType : INamedType
    {
        
        Maybe<object> Serialize( object value);

        bool IsValidValue( string value);
        bool IsValidLiteral( ValueSyntax value);

        
        Maybe<object> ParseValue( object value);

        
        Maybe<object> ParseLiteral(ValueSyntax value);
    }
}