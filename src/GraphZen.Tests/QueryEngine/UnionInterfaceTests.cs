// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem;
using Xunit;

namespace GraphZen.QueryEngine
{
    [NoReorder]
    public class UnionInterfaceTests : ExecutorHarness
    {
        public class Person
        {
            public string Name { [UsedImplicitly] get; set; }
            public object[] Pets { get; set; }
            public object[] Friends { get; set; }
        }

        public class Cat
        {
            public string Name { [UsedImplicitly] get; set; }
            public bool Meows { [UsedImplicitly] get; set; }
        }

        public class Dog
        {
            public string Name { [UsedImplicitly] get; set; }
            public bool Barks { [UsedImplicitly] get; set; }
        }

        public static Schema Schema { get; } = Schema.Create(_ =>
        {
            _.Interface("Named")
                .Field("name", "String");

            _.Object("Dog")
                .ImplementsInterface("Named")
                .Field("name", "String")
                .Field("barks", "Boolean")
                .IsTypeOf(obj => obj is Dog);

            _.Object("Cat")
                .ImplementsInterface("Named")
                .Field("name", "String")
                .Field("meows", "Boolean")
                .IsTypeOf(obj => obj is Cat);

            _.Union("Pet")
                .OfTypes("Dog", "Cat")
                .ResolveType((value, context, info) =>
                {
                    switch (value)
                    {
                        case Dog _:
                            return "Dog";
                        case Cat _:
                            return "Cat";
                    }

                    return null;
                });

            _.Object("Person")
                .ImplementsInterface("Named")
                .Field("name", "String")
                .Field("pets", "[Pet]")
                .Field("friends", "[Named]")
                .IsTypeOf(obj => obj is Person);

            _.QueryType("Person");
        });

        public static Cat Garfield { get; } = new Cat { Name = "Garfield", Meows = false };
        public static Dog Odie { get; } = new Dog { Name = "Odie", Barks = true };
        public static Person Liz { get; } = new Person { Name = "Liz" };

        public static Person John { get; } = new Person
        {
            Name = "John",
            Pets = new object[] { Garfield, Odie },
            Friends = new object[] { Liz, Odie }
        };

        [Fact]
        public Task ItCanIntrospectOnUnionAndIntersectionTypes() => ExecuteAsync(Schema, @"
               {
                Named: __type(name: ""Named"") {
                  kind
                  name
                  fields { name }
                  interfaces { name }
                  possibleTypes { name }
                  enumValues { name }
                  inputFields { name }
                }
                Pet: __type(name: ""Pet"") {
                  kind
                  name
                  fields { name }
                  interfaces { name }
                  possibleTypes { name }
                  enumValues { name }
                  inputFields { name }
                }
              }
            ").ShouldEqual(new
        {
            data = new
            {
                Named = new
                {
                    kind = "INTERFACE",
                    name = "Named",
                    fields = new object[] { new { name = "name" } },
                    interfaces = (object)null,
                    possibleTypes = new object[]
                    {
                        new {name = "Cat"},
                        new {name = "Dog"},
                        new {name = "Person"}
                    },
                    enumValues = (object)null,
                    inputFields = (object)null
                },
                Pet = new
                {
                    kind = "UNION",
                    name = "Pet",
                    fields = (object)null,

                    interfaces = (object)null,
                    possibleTypes = new object[]
                    {
                        new {name = "Dog"},
                        new {name = "Cat"}
                    },
                    enumValues = (object)null,
                    inputFields = (object)null
                }
            }
        });

        /// <summary>
        ///     NOTE: This is an *invalid* query, but it should be an *executable* query.
        /// </summary>
        [Fact]
        public Task ExecutesUsingUnionTypes() => ExecuteAsync(Schema, @"
              {
                __typename
                name
                pets {
                  __typename
                  name
                  barks
                  meows
                }
              }
        ", John).ShouldEqual(new
        {
            data = new
            {
                __typename = "Person",
                name = "John",
                pets = new object[]
                {
                    new {__typename = "Cat", name = "Garfield", meows = false},
                    new {__typename = "Dog", name = "Odie", barks = true}
                }
            }
        });

        /// <summary>
        ///     This is the valid version of the query in the above test.
        /// </summary>
        [Fact]
        public Task ExecutesUsingUnionTypesWithInlineFragments() => ExecuteAsync(Schema, @"
          {
            __typename
            name
            pets {
              __typename
              ... on Dog {
                name
                barks
              }
              ... on Cat {
                name
                meows
              }
            }
          }
        ", John).ShouldEqual(new
        {
            data = new
            {
                __typename = "Person",
                name = "John",
                pets = new object[]
                {
                    new {__typename = "Cat", name = "Garfield", meows = false},
                    new {__typename = "Dog", name = "Odie", barks = true}
                }
            }
        });


        /// <summary>
        ///     NOTE: This is an *invalid* query, but it should be an *executable* query.
        /// </summary>
        [Fact]
        public Task ExecutesUsingInterfaceTypes() => ExecuteAsync(Schema, @"
              {
                __typename
                name
                friends {
                  __typename
                  name
                  barks
                  meows
                }
              }
        ", John).ShouldEqual(new
        {
            data = new
            {
                __typename = "Person",
                name = "John",
                friends = new object[]
                {
                    new {__typename = "Person", name = "Liz"},
                    new {__typename = "Dog", name = "Odie", barks = true}
                }
            }
        });

        /// <summary>
        ///     This is the valid version of the query in the above test.
        /// </summary>
        [Fact]
        public Task ExecutesInterfaceTypesWithInlineFragments() => ExecuteAsync(Schema, @"
          {
            __typename
            name
            friends {
              __typename
              name
              ... on Dog {
                barks
              }
              ... on Cat {
                meows
              }
            }
          }
        ", John).ShouldEqual(new
        {
            data = new
            {
                __typename = "Person",
                name = "John",
                friends = new object[]
                {
                    new {__typename = "Person", name = "Liz"},
                    new {__typename = "Dog", name = "Odie", barks = true}
                }
            }
        });

        [Fact]
        public Task AllowsFragmentConditionsToBeAbstractTypes() => ExecuteAsync(Schema, @"
          {
            __typename
            name
            pets { ...PetFields }
            friends { ...FriendFields }
          }

          fragment PetFields on Pet {
            __typename
            ... on Dog {
              name
              barks
            }
            ... on Cat {
              name
              meows
            }
          }

          fragment FriendFields on Named {
            __typename
            name
            ... on Dog {
              barks
            }
            ... on Cat {
              meows
            }
          }
        ", John).ShouldEqual(new
        {
            data = new
            {
                __typename = "Person",
                name = "John",
                pets = new object[]
                {
                    new {__typename = "Cat", name = "Garfield", meows = false},
                    new {__typename = "Dog", name = "Odie", barks = true}
                },
                friends = new object[]
                {
                    new {__typename = "Person", name = "Liz"},
                    new {__typename = "Dog", name = "Odie", barks = true}
                }
            }
        });

        public class CustomContext : GraphQLContext
        {
            public string AuthToken { get; } = "123abc";
        }

        [Fact]
        public async Task GetsExecutionInfoInResolver()
        {
            CustomContext encounteredContext = default;
            Schema encounteredSchema = default;
            object encounteredRootValue = default;
            var schema = Schema.Create<CustomContext>(_ =>
            {
                _.Interface("Named")
                    .Field("name", "String")
                    .ResolveType((value, context, info) =>
                    {
                        encounteredContext = context;
                        encounteredSchema = info.Schema;
                        encounteredRootValue = info.RootValue;
                        return "Person";
                    });

                _.Object("Person").ImplementsInterface("Named")
                    .Field("name", "String")
                    .Field("friends", "[Named]");

                _.QueryType("Person");
            });

            var john = new Person { Name = "John", Friends = new object[] { Liz }, Pets = new object[] { } };
            var cxt = new CustomContext();


            var ast = Parser.ParseDocument("{ name, friends { name } }");
            await Executor.ExecuteAsync(schema, ast, john, cxt).ShouldEqual(new
            {
                data = new
                {
                    name = "John",
                    friends = new object[]
                    {
                        new {name = "Liz"}
                    }
                }
            });

            encounteredContext.Should().Be(cxt);
            encounteredSchema.Should().Be(schema);
            encounteredRootValue.Should().Be(john);
        }
    }
}