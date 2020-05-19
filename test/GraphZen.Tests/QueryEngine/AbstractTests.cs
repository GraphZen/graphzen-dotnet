// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.QueryEngine
{
    [NoReorder]
    public class AbstractTests : ExecutorHarness
    {
        private class Human
        {
            public string Name { [UsedImplicitly] get; set; }
        }

        private class Cat
        {
            public string Name { [UsedImplicitly] get; set; }
            public bool Meows { [UsedImplicitly] get; set; }
        }

        private class Dog
        {
            public string Name { [UsedImplicitly] get; set; }
            public bool? Woofs { [UsedImplicitly] get; set; }
        }

        [Fact]
        public Task IsTypeOfUsedToResolveRuntimeTypeForInterface()
        {
            var schema = Schema.Create(sb =>
            {
                // ReSharper disable once PossibleNullReferenceException
                sb.Interface("Pet")
                    .Field("name", "String");

                sb.Object("Dog")
                    .ImplementsInterface("Pet")
                    .IsTypeOf(_ => _ is Dog)
                    .Field("name", "String")
                    .Field("woofs", "Boolean");

                sb.Object("Cat").ImplementsInterface("Pet")
                    .IsTypeOf(_ => _ is Cat)
                    .Field("name", "String")
                    .Field("meows", "Boolean");

                sb.Object("Query")
                    .Field("pets", "[Pet]", _ => _.Resolve(() => new object[]
                    {
                        new Dog {Name = "Odie", Woofs = true},
                        new Cat {Name = "Garfield", Meows = false}
                    }));
            });

            var query = @"
            {
              pets {
                name
                ... on Dog {
                  woofs
                }
                ... on Cat {
                  meows
                }
              }
            }";
            return ExecuteAsync(schema, query).ShouldEqual(new
            {
                data = new
                {
                    pets = new object[]
                    {
                        new
                        {
                            name = "Odie",
                            woofs = true
                        },
                        new
                        {
                            name = "Garfield",
                            meows = false
                        }
                    }
                }
            });
        }

        [Fact]
        public Task ResolveTypeOnInterfaceYielsUsefulError()
        {
            var schema = Schema.Create(sb =>
            {
                Debug.Assert(sb != null, nameof(sb) + " != null");
                sb.Interface("Pet").ResolveType((obj, context, info) =>
                {
                    switch (obj)
                    {
                        case Dog _:
                            return "Dog";
                        case Cat _:
                            return "Cat";
                        case Human _:
                            return "Human";
                    }

                    return null!;
                });

                sb.Object("Human")
                    .Field("name", "String");

                sb.Object("Dog")
                    .ImplementsInterface("Pet")
                    .Field("name", "String")
                    .Field("woofs", "Boolean");

                sb.Object("Cat").ImplementsInterface("Pet")
                    .Field("name", "String")
                    .Field("meows", "Boolean");

                sb.Object("Query")
                    .Field("pets", "[Pet]", _ => _.Resolve(() => new object[]
                    {
                        new Dog {Name = "Odie", Woofs = true},
                        new Cat {Name = "Garfield", Meows = false},
                        new Human {Name = "Jon"}
                    }));
            });
            var query = @"
            {
              pets {
                name
                ... on Dog {
                  woofs
                }
                ... on Cat {
                  meows
                }
              }
            }";

            return ExecuteAsync(schema, query).ShouldEqual(new
            {
                data = new
                {
                    pets = new object[]
                    {
                        new
                        {
                            name = "Odie",
                            woofs = true
                        },
                        new
                        {
                            name = "Garfield",
                            meows = false
                        },
                        null!
                    }
                },
                errors = new object[]
                {
                    new
                    {
                        message = "Runtime Object type \"Human\" is not a possible type for \"Pet\".",
                        locations = new object[] {new {line = 3, column = 15}},
                        path = new object[] {"pets", 2}
                    }
                }
            });
        }

        [Fact]
        public Task ResolveTypeOnUnionYieldsUsefulError()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Object("Human")
                    .Field("name", "String");

                sb.Object("Dog")
                    .Field("name", "String")
                    .Field("woofs", "Boolean");

                sb.Object("Cat")
                    .Field("name", "String")
                    .Field("meows", "Boolean");

                sb.Union("Pet")
                    .OfTypes("Dog", "Cat")
                    .ResolveType((obj, context, info) =>
                    {
                        switch (obj)
                        {
                            case Dog _:
                                return "Dog";
                            case Cat _:
                                return "Cat";
                            case Human _:
                                return "Human";
                        }

                        return null!;
                    });


                sb.Object("Query")
                    .Field("pets", "[Pet]", _ => _.Resolve(() => new object[]
                    {
                        new Dog {Name = "Odie", Woofs = true},
                        new Cat {Name = "Garfield", Meows = false},
                        new Human {Name = "Jon"}
                    }));
            });
            var query = @"
            {
              pets {
                name
                ... on Dog {
                  woofs
                }
                ... on Cat {
                  meows
                }
              }
            }";

            return ExecuteAsync(schema, query).ShouldEqual(new
            {
                data = new
                {
                    pets = new object[]
                    {
                        new
                        {
                            name = "Odie",
                            woofs = true
                        },
                        new
                        {
                            name = "Garfield",
                            meows = false
                        },
                        null!
                    }
                },
                errors = new object[]
                {
                    new
                    {
                        message = "Runtime Object type \"Human\" is not a possible type for \"Pet\".",
                        locations = new object[] {new {line = 3, column = 15}},
                        path = new object[] {"pets", 2}
                    }
                }
            });
        }

        [Fact]
        public Task ReturningInvalidValueFromResolveTypeYieldsUsefulError()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Interface("FooInterface")
                    .ResolveType((value, context, info) => null!)
                    .Field("bar", "String");

                sb.Object("FooObject")
                    .ImplementsInterface("FooInterface")
                    .Field("bar", "String");

                sb.Object("Query").Field("foo", "FooInterface", _ => _
                    .Resolve(() => "dummy"));
            });

            return ExecuteAsync(schema, "{ foo { bar } }")
                .ShouldEqual(new
                {
                    data = new { foo = (string)null! },
                    errors = new object[]
                    {
                        new
                        {
                            message =
                                "Abstract type FooInterface must resolve to an Object type at runtime for field Query.foo with value \"dummy\", received \"null\". Either the FooInterface type should provide a \"resolveType\" function or each possible types should provide an \"IsTypeOf\" function.",
                            locations = new object[]
                            {
                                new
                                {
                                    line = 1,
                                    column = 3
                                }
                            },
                            path = new object[] {"foo"}
                        }
                    }
                });
        }

        [Fact]
        public Task ResolveTypeAllowsResolvingWithTypeName()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Interface("Pet").ResolveType((obj, context, info) =>
                {
                    switch (obj)
                    {
                        case Dog _:
                            return "Dog";
                        case Cat _:
                            return "Cat";
                        default:
                            return null!;
                    }
                });


                sb.Object("Dog")
                    .ImplementsInterface("Pet")
                    .Field("name", "String")
                    .Field("woofs", "Boolean");

                sb.Object("Cat").ImplementsInterface("Pet")
                    .Field("name", "String")
                    .Field("meows", "Boolean");

                sb.Object("Query")
                    .Field("pets", "[Pet]",
                        _ => _.Resolve(() => new object[]
                        {
                            new Dog {Name = "Odie", Woofs = true},
                            new Cat {Name = "Garfield", Meows = false}
                        }));
            });

            var query = @"
            {
              pets {
                name
                ... on Dog {
                  woofs
                }
                ... on Cat {
                  meows
                }
              }
            }";

            return ExecuteAsync(schema, query).ShouldEqual(new
            {
                data = new
                {
                    pets = new object[]
                    {
                        new
                        {
                            name = "Odie",
                            woofs = true
                        },
                        new
                        {
                            name = "Garfield",
                            meows = false
                        }
                    }
                }
            });
        }
    }
}