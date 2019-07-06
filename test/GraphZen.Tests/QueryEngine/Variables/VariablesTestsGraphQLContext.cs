// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.TypeSystem;
using Newtonsoft.Json;

namespace GraphZen.QueryEngine.Variables
{
    public class VariablesTestsGraphQLContext : GraphQLContext
    {
        protected internal override void OnSchemaCreating(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Scalar("TestComplexScalar")
                .Description("Complex scalar for test purposes")
                .LiteralParser(node =>
                    Maybe.Some<object>((string) node.GetValue() == "SerializedValue" ? "DeserializedValue" : null))
                .Serializer(value =>
                    Maybe.Some<object>(value is string str && str == "DeserializedValue"
                        ? "SerializedValue"
                        : null))
                .ValueParser(value =>
                    Maybe.Some((string) value == "SerializedValue" ? "DeserializedValue" : null).Cast<object>());

            schemaBuilder.InputObject("TestInputObject")
                .Field("a", "String")
                .Field("b", "[String]")
                .Field("c", "String!")
                .Field("d", "TestComplexScalar");


            schemaBuilder.InputObject("TestNestedInputObject")
                .Field("na", "TestInputObject!")
                .Field("nb", "String!");

            schemaBuilder.Enum("TestEnum")
                .Value("NULL", _ => _.CustomValue(null))
                .Value("NEGATIVE", _ => _.CustomValue(-1))
                .Value("BOOLEAN", _ => _.CustomValue(false))
                .Value("CUSTOM", _ => _.CustomValue("custom value"))
                .Value("DEFAULT_VALUE", _ => _.CustomValue(new { }));


            new List<(string fieldName, string inputArgType, string defaultValue)>
            {
                ("fieldWithEnumInput", "TestEnum", null),
                ("fieldWithNonNullableEnumInput", "TestEnum", null),
                ("fieldWithObjectInput", "TestInputObject", null),
                ("fieldWithNullableStringInput", "String", null),
                ("fieldWithNonNullableStringInput", "String!", null),
                ("fieldWithNonNullableStringInputAndDefaultArgumentValue", "String!",
                    "Hello World"),
                ("fieldWithDefaultArgumentValue", "String", "Hello World"),
                ("fieldWithNestedObjectInput", "TestNestedInputObject", "Hello World"),
                ("list", "[String]", null),
                ("nnList", "[String]!", null),
                ("listNN", "[String!]", null),
                ("nnListNN", "[String!]!", null)
            }.ForEach(field =>
            {
                schemaBuilder.Object("TestType")
                    .Field<string>(field.fieldName,
                        f =>
                        {
                            f.Argument("input", field.inputArgType, arg =>
                                {
                                    if (field.defaultValue != null)
                                    {
                                        arg.DefaultValue(field.defaultValue);
                                    }
                                })
                                .Resolve((source, args) =>
                                    args.ContainsKey("input")
                                        ? (string) JsonConvert.SerializeObject(args.input)
                                        : null);
                        });
            });

            schemaBuilder.QueryType("TestType");
        }
    }
}