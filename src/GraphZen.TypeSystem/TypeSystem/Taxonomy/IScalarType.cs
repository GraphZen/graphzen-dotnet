// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem.Taxonomy;

[GraphQLIgnore]
public interface IScalarType :
    ILeafType,
    IScalarTypeDefinition,
    ISerializer,
    IValueParser,
    ILiteralParser
{
}