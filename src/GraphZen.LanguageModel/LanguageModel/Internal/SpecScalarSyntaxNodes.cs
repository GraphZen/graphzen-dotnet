// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using GraphZen.Infrastructure;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.LanguageModel.Internal
{
    internal static class SpecScalarSyntaxNodes
    {
        public static ScalarTypeDefinitionSyntax String { get; } = ScalarTypeDefinition(
            Name("String"),
            StringValue("string value"));

        public static ScalarTypeDefinitionSyntax ID { get; } =
            ScalarTypeDefinition(Name("ID"),
                StringValue("The `ID` scalar type represents a unique identifier, often used to " +
                            "refetch an object or as key for a cache. The ID type appears in a JSON " +
                            "response as a String; however, it is not intended to be human-readable. " +
                            "When expected as an input type, any string (such as `\"4\"`) or integer " +
                            "(such as `4`) input value will be accepted as an ID."));


        public static ScalarTypeDefinitionSyntax Int { get; } =
            ScalarTypeDefinition(Name("Int"), StringValue("integer value"));

        public static ScalarTypeDefinitionSyntax Float { get; } = ScalarTypeDefinition(Name("Float"),
            StringValue("The `Float` scalar type represents signed double-precision fractional " +
                        "values as specified by " +
                        "[IEEE 754](http://en.wikipedia.org/wiki/IEEE_floating_point)."));

        public static ScalarTypeDefinitionSyntax Boolean { get; } =
            ScalarTypeDefinition(Name("Boolean"), StringValue("boolean value"));

        [ItemNotNull]
        public static ImmutableArray<ScalarTypeDefinitionSyntax> All { get; } =
            ImmutableArray.Create(String, ID, Int, Float, Boolean);
    }
}