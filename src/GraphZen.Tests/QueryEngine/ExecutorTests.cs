#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using Newtonsoft.Json;
using Xunit;

namespace GraphZen.QueryEngine
{
    [NoReorder]
    public class ExecutorTests : ExecutorHarness
    {
        [Fact]
        public Task AcceptsAnObjectWithNamedPropertiesAsArguments()
        {
            var doc = "query Example { a }";
            var data = "rootValue";
            var schema = Schema.Create(sb =>
            {
                sb.Object("Type").Field("a", "String", _ => _
                    .Resolve(x => x));
                sb.QueryType("Type");
            });

            return ExecuteAsync(schema, doc, data).ShouldEqual(new
            {
                data = new
                {
                    a = "rootValue"
                }
            });
        }


        private class DeepData
        {
            private readonly Data _data;

            public DeepData(Data data)
            {
                _data = data;
            }

            [UsedImplicitly]
            public string A() => "Already Been Done";

            [UsedImplicitly]
            public string B() => "Boring";

            [UsedImplicitly]
            public object[] C() => new object[] { "Contrived", null, "Confusing" };

            [UsedImplicitly]
            public object[] Deeper() => new object[] { _data, null, _data };
        }

        private class Data
        {
            [UsedImplicitly]
            public string F => "Fish";

            [UsedImplicitly]
            public string A() => "Apple";

            [UsedImplicitly]
            public string B() => "Banana";

            [UsedImplicitly]
            public string C() => "Cookie";

            [UsedImplicitly]
            public string D() => "Donut";

            [UsedImplicitly]
            public string E() => "Egg";

            [UsedImplicitly]
            public string Pic(int? size) => $"Pic of size: {size ?? 50}";

            [UsedImplicitly]
            public DeepData Deep() => new DeepData(this);

            [UsedImplicitly]
            public Task<Data> DataAsync() => Task.FromResult(this);
        }

        [Fact]
        public async Task ExecutesArbitraryCode()
        {
            var doc = @"
              query Example($size: Int) {
                a,
                b,
                x: c
                ...c
                f
                ...on DataType {
                  pic(size: $size)
                  task: dataAsync {
                    a
                  }
                }
                deep {
                  a
                  b
                  c
                  deeper {
                    a
                    b
                  }
                }
              }

              fragment c on DataType {
                d
                e
              }";
            var expected = new
            {
                data = new
                {
                    a = "Apple",
                    b = "Banana",
                    x = "Cookie",
                    d = "Donut",
                    e = "Egg",
                    f = "Fish",
                    pic = "Pic of size: 100",
                    task = new { a = "Apple" },
                    deep = new
                    {
                        a = "Already Been Done",
                        b = "Boring",
                        c = new object[] { "Contrived", null, "Confusing" },
                        deeper = new object[]
                        {
                            new {a = "Apple", b = "Banana"},
                            null,
                            new {a = "Apple", b = "Banana"}
                        }
                    }
                }
            };

            var schema = Schema.Create(sb =>
            {
                sb.Object("DataType")
                    .Field("a", "String")
                    .Field("b", "String")
                    .Field("c", "String")
                    .Field("d", "String")
                    .Field("e", "String")
                    .Field("f", "String")
                    .Field("pic", "String", pic => { pic.Argument("size", "Int"); })
                    .Field("deep", "DeepDataType")
                    .Field("dataAsync", "DataType");

                sb.Object("DeepDataType")
                    .Field("a", "String")
                    .Field("b", "String")
                    .Field("c", "[String]")
                    .Field("d", "[String]")
                    .Field("deeper", "[DataType]");

                sb.QueryType("DataType");
            });
            await ExecuteAsync(schema, doc, new Data(), new { size = 100 }).ShouldEqual(expected);
        }

        [Fact]
        public async Task MergesParallelFragments()
        {
            var doc = @"
                { a, ...FragOne, ...FragTwo }

                fragment FragOne on Type {
                  b
                  deep { b, deeper: deep { b } }
                }

                fragment FragTwo on Type {
                  c
                  deep { c, deeper: deep { c } }
                } ";

            var schema = Schema.Create(sb =>
            {
                sb.Object("Type")
                    .Field("a", "String", _ => _.Resolve(() => "Apple"))
                    .Field("b", "String", _ => _.Resolve(() => "Banana"))
                    .Field("c", "String", _ => _.Resolve(() => "Cherry"))
                    .Field("deep", "Type", _ => _.Resolve(() => new { }));
                sb.QueryType("Type");
            });

            await ExecuteAsync(schema, doc).ShouldEqual(new
            {
                data = new
                {
                    a = "Apple",
                    b = "Banana",
                    c = "Cherry",
                    deep = new
                    {
                        b = "Banana",
                        c = "Cherry",
                        deeper = new
                        {
                            b = "Banana",
                            c = "Cherry"
                        }
                    }
                }
            });
        }

        [Fact]
        public async Task ProvidesInfoAboutCurrentExecutionState()
        {
            var ast = Parser.ParseDocument("query ($var: String) { result: test }");
            ResolveInfo info = default;
            var schemaSut = Schema.Create(sb =>
            {
                sb.Object("Test").Field("test", "String", _ => _
                    .Resolve((source, args, context, resolveInfo) =>
                    {
                        info = resolveInfo;
                        return "hello";
                    }));
                sb.QueryType("Test");
            });

            var rootValue = new { root = "val" };

            await ExecuteAsync(schemaSut, ast, rootValue, new { var = "abc" });
            info.FieldName.Should().Be("test");
            info.FieldNodes.Count.Should().Be(1);
            info.FieldNodes[0].Should()
                .Be(ast.Definitions[0].As<OperationDefinitionSyntax>().SelectionSet.Selections[0]);
            info.ReturnType.Should().Be(SpecScalars.String);
            info.ParentType.Should().Be(schemaSut.QueryType);
            info.Path.Previous.Should().Be(null);
            info.Path.Key.Should().Be("result");
            info.Schema.Should().Be(schemaSut);
            info.RootValue.Should().Be(rootValue);
            info.Operation.Should().Be(ast.Definitions[0]);
            TestHelpers.AssertEqualsDynamic(new { var = "abc" }, info.VariableValues);
        }

        [Fact]
        public async Task ThreadsRootValueContextCorrectly()
        {
            var doc = "query Example { a }";
            var data = new DynamicDictionary();
            data["contextThing"] = "thing";
            dynamic resolvedRootValue = default;
            var schema = Schema.Create(sb =>
            {
                sb.Object("Type").Field("a", "String", _ => _.Resolve(source =>
                {
                    resolvedRootValue = source;
                    return null;
                }));
                sb.QueryType("Type");
            });
            await ExecuteAsync(schema, Parser.ParseDocument(doc), data);
            Assert.Equal("thing", resolvedRootValue.contextThing);
        }

        public class OrderingData
        {
            public string A() => "a";
            public Task<string> B() => Task.FromResult("b");
            public string C() => "c";
            public Task<string> D() => Task.FromResult("d");
            public string E() => "e";
        }


        [Fact]
        public async Task CorrectFieldOrderingDespiteExecutionOrder()
        {
            var doc = @"
            {
              a,
              b,
              c,
              d,
              e
            }";
            var schema = Schema.Create(sb =>
            {
                sb.Object("Type")
                    .Field("a", "String")
                    .Field("b", "String")
                    .Field("c", "String")
                    .Field("d", "String")
                    .Field("e", "String");
                sb.QueryType("Type");
            });

            var result = await ExecuteAsync(schema, doc, new OrderingData()).ShouldEqual(new
            {
                data = new
                {
                    a = "a",
                    b = "b",
                    c = "c",
                    d = "d",
                    e = "e"
                }
            });

            result.Data.Keys.Should()
                .BeEquivalentTo(new[] { "a", "b", "c", "d", "e" }, opts => opts.WithStrictOrdering());
        }

        [Fact]
        public async Task AvoidsRecursion()
        {
            var doc = @"
              query Q {
                a
                ...Frag
                ...Frag
              }

              fragment Frag on Type {
                a,
                ...Frag
              }
            ";
            var data = new { a = "b" };
            var schema = Schema.Create(_ =>
            {
                _.Object("Type").Field("a", "String");
                _.QueryType("Type");
            });

            await ExecuteAsync(schema, doc, data).ShouldEqual(new
            {
                data = new
                {
                    a = "b"
                }
            });
        }

        [Fact]
        public async Task DoesNotIncludeIllegalFieldsInOutput()
        {
            var doc = @"
            mutation M {
              thisIsIllegalDontIncludeMe
            } ";

            var schema = Schema.Create(_ =>
            {
                _.Object("Q").Field("a", "String");
                _.Object("M").Field("c", "String");


                _.QueryType("Q");
                _.MutationType("M");
            });

            await ExecuteAsync(schema, doc).ShouldEqual(new { data = new { } });
        }

        [Fact]
        public Task DoesNotIncludeArgumentsThatWereNotSet()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Type")
                    .Field("field", "String", field => field.Resolve((data, args) => JsonConvert.SerializeObject(args))
                        .Argument("a", "Boolean")
                        .Argument("b", "Boolean")
                        .Argument("c", "Boolean")
                        .Argument("d", "Int")
                        .Argument("e", "Int")
                    );
                _.QueryType("Type");
            });

            return ExecuteAsync(schema, "{ field(a: true, c: false, e: 0) }").ShouldEqual(new
            {
                data = new
                {
                    field = "{\"a\":true,\"c\":false,\"e\":0}"
                }
            });
        }


        private class Special
        {
            public string Value { [UsedImplicitly] get; set; }
        }

        private class NotSpecial
        {
            public string Value { [UsedImplicitly] get; set; }
        }

        private class SpecialsRoot
        {
            public object[] Specials { get; set; }
        }

        [Fact]
        public Task FailsWhenIsTypeOfCheckIsNotMet()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("SpecialType").IsTypeOf(
                    o => o is Special).Field("value", "String");
                _.Object("Query").Field("specials", "[SpecialType]",
                    f => f.Resolve(root => ((SpecialsRoot)root).Specials));
            });

            var rootValue = new SpecialsRoot
            {
                Specials = new object[] { new Special { Value = "foo" }, new NotSpecial { Value = "bar" } }
            };

            return ExecuteAsync(schema, "{specials { value } }", rootValue).ShouldEqual(new
            {
                data = new
                {
                    specials = new object[] { new { value = "foo" }, null }
                },
                errors = new object[]
                {
                    new
                    {
                        message = "Expected value of type \"SpecialType\" but got: {value: \"bar\"}.",
                        locations = new object[]
                        {
                            new {line = 1, column = 2}
                        },
                        path = new object[]
                        {
                            "specials", 1
                        }
                    }
                }
            });
        }

        [Fact]
        public Task ExecutesIgnoringInvalidNonExecutableDefinitions()
        {
            var query = @"
              { foo }

              type Query { bar: String }
            ";
            var schema = Schema.Create(_ => { _.Object("Query").Field("foo", "String"); });

            return ExecuteAsync(schema, query, throwOnError: true).ShouldEqual(new
            {
                data = new
                {
                    foo = (object)null
                }
            });
        }

        [Fact(Skip = "TODO")]
        public Task UsesACustomFieldResolver() => throw new NotImplementedException();
    }
}