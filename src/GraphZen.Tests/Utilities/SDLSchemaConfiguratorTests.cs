// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.QueryEngine;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Utilities
{
    [NoReorder]
    public class SDLSchemaConfiguratorTests : ExecutorHarness
    {
        [Fact]
        public Task ItCanUseBuiltSchemaForLimitedExecution()
        {
            var schema = Schema.Create(@"
                type Query {
                  str: String
                }
            ");

            return ExecuteAsync(schema, " { str } ", new {str = 123}).ShouldEqual(new
            {
                data = new
                {
                    str = "123"
                }
            });
        }


        [Fact]
        public Task ItCanBuildSchemaDirectlyFromTheSource()
        {
            var schema = Schema.Create(@"
                type Query {
                  add(x: Int, y: Int): Int
                }
            ");

            var root = new
            {
                add = (Func<dynamic, int>) (args => args.x + args.y)
            };

            return ExecuteAsync(schema, "{ add(x: 34, y: 55) }", root).ShouldEqual(new
            {
                data = new
                {
                    add = 89
                }
            });
        }

        private static void ShouldRoundTrip(string sdl, ResultComparisonOptions? options = null)
        {
            var body = sdl.Dedent();
            var schema = Schema.Create(body);
            var printed = schema.Print();
            if (printed != body)
            {
                var diff = TestHelpers.GetDiff(body, printed, options ?? new ResultComparisonOptions());
                throw new Exception(diff);
            }
        }

        [Fact]
        public void SimpleType()
        {
            ShouldRoundTrip(@"
          type Query {
            str: String
            int: Int
            float: Float
            id: ID
            bool: Boolean
          }
        ");
        }

        [Fact]
        public void WithDirectives()
        {
            ShouldRoundTrip(@"
          directive @foo(arg: Int) on FIELD

          type Query {
            str: String
          }
        ");
        }

        [Fact]
        public void SupportsDescriptions()
        {
            ShouldRoundTrip(@"
              directive @foo(
                """"""
                It has an argument
                """"""
                arg: Int
              ) on FIELD

              """"""
              With an enum
              """"""
              enum Color {
                RED
                GREEN
                BLUE
              }

              """"""
              What a great type
              """"""
              type Query {
                """"""
                And a field to boot
                """"""
                str: String
              }
            ");
        }

        [Fact]
        public void MaintainsSpecDirectives()
        {
            var body = @"
              type Query {
                str: String
              }
            ".Dedent();
            var schema = Schema.Create(body);
            schema.Directives.Count.Should().Be(3);
            schema.FindDirective("skip").Should().Be(SpecDirectives.Skip);
            schema.FindDirective("include").Should().Be(SpecDirectives.Include);
            schema.FindDirective("deprecated").Should().Be(SpecDirectives.Deprecated);
        }

        [Fact]
        public void ShouldCreateSchemaWithDeprecatedDirective()
        {
            var body = @"
              type Query {
                str: String @deprecated(reason: ""test"") 
              }
            ".Dedent();
            var schema = Schema.Create(body);
            var expectedDirective = new GraphQLDeprecatedAttribute("test");
            var field = schema.QueryType.GetField("str");
            var actualDirective = field.FindDirectiveAnnotation("deprecated");
            actualDirective.Value.Should().Be(expectedDirective);
            field.DeprecationReason.Should().Be("test");
        }

        [Fact]
        public void ShouldCreateSchemaWithSyntaxDirective()
        {
            var body = @"
              type Query @unknown {
                str: String 
              }
            ".Dedent();
            var schema = Schema.Create(body);
            var expectedDirective = SyntaxFactory.Directive(SyntaxFactory.Name("unknown"));
            var actualDirective = schema.QueryType.FindDirectiveAnnotation("unknown");
            actualDirective.Value.Should().Be(expectedDirective);
        }

        [Fact]
        public void OverridingDirectivesExcludesSpecified()
        {
            var body = @"
              directive @skip on FIELD
              directive @include on FIELD
              directive @deprecated on FIELD_DEFINITION

              type Query {
                str: String
              }
            ".Dedent();
            var schema = Schema.Create(body);
            schema.Directives.Count.Should().Be(3);
            schema.FindDirective("skip").Should().NotBe(SpecDirectives.Skip);
            schema.FindDirective("include").Should().NotBe(SpecDirectives.Include);
            schema.FindDirective("deprecated").Should().NotBe(SpecDirectives.Deprecated);
        }

        [Fact]
        public void AddingDirectivesMaintainsSpecDirectives()
        {
            var body = @"
              directive @foo(arg: Int) on FIELD

              type Query {
                str: String
              }
            ".Dedent();
            var schema = Schema.Create(body);
            schema.Directives.Count.Should().Be(4);
            schema.FindDirective("skip").Should().Be(SpecDirectives.Skip);
            schema.FindDirective("include").Should().Be(SpecDirectives.Include);
            schema.FindDirective("deprecated").Should().Be(SpecDirectives.Deprecated);
        }

        [Fact]
        public void TypeModifiers()
        {
            ShouldRoundTrip(@"
          type Query {
            nonNullStr: String!
            listOfStrs: [String]
            listOfNonNullStrs: [String!]
            nonNullListOfStrs: [String]!
            nonNullListOfNonNullStrs: [String!]!
          }
        ");
        }

        [Fact]
        public void RecursiveType()
        {
            ShouldRoundTrip(@"
          type Query {
            str: String
            recurse: Query
          }
        ");
        }

        [Fact]
        public void TwoTypesCircular()
        {
            ShouldRoundTrip(@"
          schema {
            query: TypeOne
          }

          type TypeOne {
            str: String
            typeTwo: TypeTwo
          }

          type TypeTwo {
            str: String
            typeOne: TypeOne
          }
        ");
        }

        [Fact]
        public void SingleArgumentField()
        {
            ShouldRoundTrip(@"
          type Query {
            str(int: Int): String
            floatToStr(float: Float): String
            idToStr(id: ID): String
            booleanToStr(bool: Boolean): String
            strToStr(bool: String): String
          }
        ");
        }

        [Fact]
        public void SimpleTypeWithMultipleArguments()
        {
            ShouldRoundTrip(@"
          type Query {
            str(int: Int, bool: Boolean): String
          }
        ");
        }

        [Fact]
        public void SimpleTypeWithInterface()
        {
            ShouldRoundTrip(@"
          type Query implements WorldInterface {
            str: String
          }

          interface WorldInterface {
            str: String
          }
        ");
        }

        [Fact]
        public void SimpleOutputEnum()
        {
            ShouldRoundTrip(@"
          enum Hello {
            WORLD
          }

          type Query {
            hello: Hello
          }
        ");
        }

        [Fact]
        public void SimpleInputEnum()
        {
            ShouldRoundTrip(@"
          enum Hello {
            WORLD
          }

          type Query {
            str(hello: Hello): String
          }
        ");
        }

        [Fact]
        public void MultipleValueEnum()
        {
            ShouldRoundTrip(@"
          enum Hello {
            WO
            RLD
          }

          type Query {
            hello: Hello
          }
        ");
        }

        [Fact]
        public void SimpleUnion()
        {
            ShouldRoundTrip(@"
          union Hello = World

          type Query {
            hello: Hello
          }

          type World {
            str: String
          }
      ");
        }

        [Fact]
        public void MultipleUnion()
        {
            ShouldRoundTrip(@"
          union Hello = WorldOne | WorldTwo

          type Query {
            hello: Hello
          }

          type WorldOne {
            str: String
          }

          type WorldTwo {
            str: String
          }
        ");
        }

        [Fact(Skip = "needs schema validator")]
        public void CanBuildRecursiveUnion()
        {
        }

        [Fact]
        public async Task SpecifyingUnionTypeUsing__typename()
        {
            var schema = Schema.Create(@"
              type Query {
                fruits: [Fruit]
              }

              union Fruit = Apple | Banana

              type Apple {
                color: String
              }

              type Banana {
                length: Int
              }
            ");

            var query = @"
              {
                fruits {
                  ... on Apple {
                    color
                  }
                  ... on Banana {
                    length
                  }
                }
              }";
            var root = new
            {
                fruits = new object[]
                {
                    new
                    {
                        color = "green",
                        __typename = "Apple"
                    },
                    new
                    {
                        length = 5,
                        __typename = "Banana"
                    }
                }
            };
            await ExecuteAsync(schema, query, root).ShouldEqual(new
            {
                data = new
                {
                    fruits = new object[]
                    {
                        new
                        {
                            color = "green"
                        },
                        new
                        {
                            length = 5
                        }
                    }
                }
            });
        }

        [Fact]
        public async Task SpecifyingInterfaceUsing__typename()
        {
            var schema = Schema.Create(@"
              type Query {
                characters: [Character]
              }

              interface Character {
                name: String!
              }

              type Human implements Character {
                name: String!
                totalCredits: Int
              }

              type Droid implements Character {
                name: String!
                primaryFunction: String
              }
            ");

            var query = @"
              {
                characters {
                  name
                  ... on Human {
                    totalCredits
                  }
                  ... on Droid {
                    primaryFunction
                  }
                }
              }
            ";

            var root = new
            {
                characters = new object[]
                {
                    new
                    {
                        name = "Han Solo",
                        totalCredits = 10,
                        __typename = "Human"
                    },
                    new
                    {
                        name = "R2-D2",
                        primaryFunction = "Astromech",
                        __typename = "Droid"
                    }
                }
            };

            await ExecuteAsync(schema, query, root).ShouldEqual(new
            {
                data = new
                {
                    characters = new object[]
                    {
                        new
                        {
                            name = "Han Solo",
                            totalCredits = 10
                        },
                        new
                        {
                            name = "R2-D2",
                            primaryFunction = "Astromech"
                        }
                    }
                }
            });
        }

        [Fact]
        public void CustomScalar()
        {
            ShouldRoundTrip(@"
          scalar CustomScalar

          type Query {
            customScalar: CustomScalar
          }
        ");
        }

        [Fact]
        public void InputObject()
        {
            ShouldRoundTrip(@"
          input Input {
            int: Int
          }

          type Query {
            field(in: Input): String
          }
        ");
        }

        [Fact(Skip = "TODO")]
        public void SimpleArgumentFieldWithDefault()
        {
            ShouldRoundTrip(@"
          type Query {
            str(int: Int = 2): String
          }
        ");
        }

        [Fact(Skip = "TODO")]
        public void CustomScalarArgumentWithDefault()
        {
            ShouldRoundTrip(@"
          scalar CustomScalar

          type Query {
            str(int: CustomScalar = 2): String
          }
        ");
        }

        [Fact]
        public void SimpleTypeWithMutation()
        {
            ShouldRoundTrip(@"
          schema {
            query: HelloScalars
            mutation: Mutation
          }

          type HelloScalars {
            str: String
            int: Int
            bool: Boolean
          }

          type Mutation {
            addHelloScalars(str: String, int: Int, bool: Boolean): HelloScalars
          }
        ");
        }

        [Fact]
        public void SimpleTypeWithSubscription()
        {
            ShouldRoundTrip(@"
          schema {
            query: HelloScalars
            subscription: Subscription
          }

          type HelloScalars {
            str: String
            int: Int
            bool: Boolean
          }

          type Subscription {
            subscribeHelloScalars(str: String, int: Int, bool: Boolean): HelloScalars
          }
        ");
        }

        [Fact]
        public void UnreferencedTypeImplementingReferencedInterface()
        {
            ShouldRoundTrip(@"
          type Concrete implements Iface {
            key: String
          }

          interface Iface {
            key: String
          }

          type Query {
            iface: Iface
          }
        ");
        }

        [Fact]
        public void UnreferencedTypeImplementingReferencedUnion()
        {
            ShouldRoundTrip(@"
          type Concrete {
            key: String
          }

          type Query {
            union: Union
          }

          union Union = Concrete
        ");
        }

        [Fact(Skip = "TODO")]
        public void SupportsDeprecated()
        {
            var body = @"
              enum MyEnum {
                VALUE
                OLD_VALUE @deprecated
                OTHER_VALUE @deprecated(reason: ""Terrible reasons"")
              }

              type Query {
                field1: String @deprecated
                field2: Int @deprecated(reason: ""Because I said so"")
                enum: MyEnum
              }
            ";
            ShouldRoundTrip(body);
            // var ast = Parser.ParseDocument(body);
            // var schema = Schema.Create(ast);
            /*
               var myEnum = schema.GetType('MyEnum');
               
               var value = myEnum.getValue('VALUE');
               expect(value.isDeprecated).to.equal(false);
               
               var oldValue = myEnum.getValue('OLD_VALUE');
               expect(oldValue.isDeprecated).to.equal(true);
               expect(oldValue.deprecationReason).to.equal('No longer supported');
               
               var otherValue = myEnum.getValue('OTHER_VALUE');
               expect(otherValue.isDeprecated).to.equal(true);
               expect(otherValue.deprecationReason).to.equal('Terrible reasons');
               
               var rootFields = schema.GetType('Query').getFields();
               expect(rootFields.field1.isDeprecated).to.equal(true);
               expect(rootFields.field1.deprecationReason).to.equal('No longer supported');
               
               expect(rootFields.field2.isDeprecated).to.equal(true);
               expect(rootFields.field2.deprecationReason).to.equal('Because I said so');
             */
        }

        [Fact]
        public void CorrectlyAssignASTNodes()
        {
            var schemaAST = Parser.ParseDocument(@"
              schema {
                query: Query
              }

              type Query {
                testField(testArg: TestInput): TestUnion
              }

              input TestInput {
                testInputField: TestEnum
              }

              enum TestEnum {
                TEST_VALUE
              }

              union TestUnion = TestType

              interface TestInterface {
                interfaceField: String
              }

              type TestType implements TestInterface {
                interfaceField: String
              }

              scalar TestScalar

              directive @test(arg: TestScalar) on FIELD
            ");
            var schema = Schema.Create(schemaAST);

            var query = schema.GetObject("Query");
            var testInput = schema.GetInputObject("TestInput");
            var testEnum = schema.GetEnum("TestEnum");
            var testUnion = schema.GetUnion("TestUnion");
            var testInterface = schema.GetInterface("TestInterface");
            var testType = schema.GetObject("TestType");
            var testScalar = schema.GetScalar("TestScalar");
            var testDirective = schema.FindDirective("test");


            var restoredSchemaAST = SyntaxFactory.Document(
                new ISyntaxConvertable[]
                {
                    schema,
                    query,
                    testInput,
                    testEnum,
                    testUnion,
                    testInterface,
                    testType,
                    testScalar,
                    testDirective
                }.ToSyntaxNodes<DefinitionSyntax>().ToArray()
            );
            restoredSchemaAST.ToSyntaxString().Should().Be(schemaAST.ToSyntaxString());
            var testField = query.Fields["testField"];
            testField.Print().Should().Be("testField(testArg: TestInput): TestUnion");
            testField.GetArguments().First().Print().Should().Be("testArg: TestInput");
            testInput.Fields["testInputField"].Print().Should().Be("testInputField: TestEnum");
            testEnum.GetValue("TEST_VALUE").Print().Should().Be("TEST_VALUE");
            testInterface.Fields["interfaceField"].Print().Should().Be("interfaceField: String");
            testType.Fields["interfaceField"].Print().Should().Be("interfaceField: String");
            // ReSharper disable once PossibleNullReferenceException
            testDirective.GetArguments().First().Print().Should().Be("arg: TestScalar");
        }

        [Fact]
        public void RootOperationTypesWithCustomNames()
        {
            var schema = Schema.Create(@"
              schema {
                query: SomeQuery
                mutation: SomeMutation
                subscription: SomeSubscription
              }
              type SomeQuery { str: String }
              type SomeMutation { str: String }
              type SomeSubscription { str: String }
            ");
            schema.QueryType.Name.Should().Be("SomeQuery");
            schema.MutationType?.Name.Should().Be("SomeMutation");
            schema.SubscriptionType?.Name.Should().Be("SomeSubscription");
        }

        [Fact]
        public void DefaultRootOperationTypeNames()
        {
            var schema = Schema.Create(@"
              type Query { str: String }
              type Mutation { str: String }
              type Subscription { str: String }
            ");

            schema.QueryType.Name.Should().Be("Query");
            schema.MutationType?.Name.Should().Be("Mutation");
            schema.SubscriptionType?.Name.Should().Be("Subscription");
        }

        [Fact(Skip = "TODO - requires schema validation")]
        public void CanBuildInvalidSchema()
        {
        }

        [Fact(Skip = "TODO - may not implement")]
        public void AcceptsLegacyNames()
        {
        }

        [Fact(Skip = "TODO")]
        public void RejectsInvalidSDL()
        {
        }
    }
}