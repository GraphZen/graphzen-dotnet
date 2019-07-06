// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Threading.Tasks;
using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.StarWars
{
    [NoReorder]
    public class StarWarsQueryTest : StarWarsSchemaAndData
    {
        [Fact]
        public Task CorrectlyIdentifiesR2D2AsTHeHeroOfTheStarWarsSaga() =>
            ExecuteAsync(StarWarsSchema, "query HeroNameQuery { hero { name } }").ShouldEqual(new
            {
                data = new
                {
                    hero = new
                    {
                        name = "R2-D2"
                    }
                }
            });

        [Fact(Skip = "Not applicable to .NET implementation")]
        public Task AcceptsAnObjectWithNamedPropertiesToGraphQL() => Task.CompletedTask;

        [Fact]
        public Task AllowsUsToQueryForTheIdAndFriendsOfR2D2() =>
            ExecuteAsync(StarWarsSchema, @" 
                query HeroNameAndFriendsQuery {
                  hero {
                    id
                    name
                    friends {
                      name
                    }
                  }
                }").ShouldEqual(new
            {
                data = new
                {
                    hero = new
                    {
                        id = "2001",
                        name = "R2-D2",
                        friends = new object[]
                        {
                            new {name = "Luke Skywalker"},
                            new {name = "Han Solo"},
                            new {name = "Leia Organa"}
                        }
                    }
                }
            });

        [Fact]
        public Task AllowsUsToQueryForFriendsOfFriendsOfR2D2() =>
            ExecuteAsync(StarWarsSchema, @" 
                query NestedQuery {
                  hero {
                    name
                    friends {
                      name
                      appearsIn
                      friends {
                        name
                      }
                    }
                  }
                }
                ").ShouldEqual(new
            {
                data = new
                {
                    hero = new
                    {
                        name = "R2-D2",
                        friends = new object[]
                        {
                            new
                            {
                                name = "Luke Skywalker",
                                appearsIn = new object[] {"NEW_HOPE", "EMPIRE", "JEDI"},
                                friends = new object[]
                                {
                                    new {name = "Han Solo"},
                                    new {name = "Leia Organa"},
                                    new {name = "C-3P0"},
                                    new {name = "R2-D2"}
                                }
                            },
                            new
                            {
                                name = "Han Solo",
                                appearsIn = new object[] {"NEW_HOPE", "EMPIRE", "JEDI"},
                                friends = new object[]
                                {
                                    new {name = "Luke Skywalker"},
                                    new {name = "Leia Organa"},
                                    new {name = "R2-D2"}
                                }
                            },
                            new
                            {
                                name = "Leia Organa",
                                appearsIn = new object[] {"NEW_HOPE", "EMPIRE", "JEDI"},
                                friends = new object[]
                                {
                                    new {name = "Luke Skywalker"},
                                    new {name = "Han Solo"},
                                    new {name = "C-3P0"},
                                    new {name = "R2-D2"}
                                }
                            }
                        }
                    }
                }
            });

        [Fact]
        public Task AllowsUsToQueryFOrLukeSkywalkerDirectlyUsingHisId() => ExecuteAsync(StarWarsSchema, @"
            query FetchLukeQuery {
              human(id: ""1000"") {
                name
              }
            }").ShouldEqual(new
        {
            data = new
            {
                human = new
                {
                    name = "Luke Skywalker"
                }
            }
        });

        [Theory]
        [InlineData("1000", "Luke Skywalker")]
        [InlineData("1002", "Han Solo")]
        [InlineData("not a valid id", null)]
        public Task GenericQuery(string id, string name)
        {
            var human = name != null ? new {name} : null;
            return ExecuteAsync(StarWarsSchema, @" 
                query FetchSomeIDQuery($someId: String!) {
                  human(id: $someId) {
                    name
                  }
                }", null, new {someId = id}).ShouldEqual(new
            {
                data = new
                {
                    human
                }
            });
        }

        [Fact]
        public Task QueryForLukeWithAlias() => ExecuteAsync(StarWarsSchema, @"
              query FetchLukeAliased {
                luke: human(id: ""1000"") {
                  name
                }
              }").ShouldEqual(new
        {
            data = new
            {
                luke = new {name = "Luke Skywalker"}
            }
        });

        [Fact]
        public Task QueryForLukeAndLeiaOnRootWithAliases() => ExecuteAsync(StarWarsSchema, @"
              query FetchLukeAndLeiaAliased {
                luke: human(id: ""1000"") {
                  name
                }
                leia: human(id: ""1003"") {
                  name
                }
              }").ShouldEqual(new
        {
            data = new
            {
                luke = new {name = "Luke Skywalker"},
                leia = new {name = "Leia Organa"}
            }
        });

        [Fact]
        public Task QueryUsingDuplicatedContent() => ExecuteAsync(StarWarsSchema, @"
          query DuplicateFields {
            luke: human(id: ""1000"") {
              name
              homePlanet
            }
            leia: human(id: ""1003"") {
              name
              homePlanet
            }
          }").ShouldEqual(new
        {
            data = new
            {
                luke = new {name = "Luke Skywalker", homePlanet = "Tatooine"},
                leia = new {name = "Leia Organa", homePlanet = "Alderaan"}
            }
        });

        [Fact]
        public Task QueryUsingFragmentToAvoidDuplicatedContent() => ExecuteAsync(StarWarsSchema, @"
            query UseFragment {
              luke: human(id: ""1000"") {
                ...HumanFragment
              }
              leia: human(id: ""1003"") {
                ...HumanFragment
              }
            }

            fragment HumanFragment on Human {
              name
              homePlanet
            }
          ").ShouldEqual(new
        {
            data = new
            {
                luke = new {name = "Luke Skywalker", homePlanet = "Tatooine"},
                leia = new {name = "Leia Organa", homePlanet = "Alderaan"}
            }
        });

        [Fact]
        public Task UsingTypenameToVerifyR2D2IsADroid() => ExecuteAsync(StarWarsSchema, @"
            query CheckTypeOfR2{
              hero {
                __typename
                name
              }
            }
            ").ShouldEqual(new
        {
            data = new
            {
                hero = new
                {
                    __typename = "Droid",
                    name = "R2-D2"
                }
            }
        });

        [Fact]
        public Task UsingTypenameToVerifyLukeIsAHuman() => ExecuteAsync(StarWarsSchema, @"
            query CheckTypeOfLuke {
              hero(episode: EMPIRE) {
                __typename
                name
              }
            }
            ").ShouldEqual(new
        {
            data = new
            {
                hero = new
                {
                    __typename = "Human",
                    name = "Luke Skywalker"
                }
            }
        });

        [Fact]
        public Task CorrectlyReportsErrorOnAccessingSecretBackstory()
            => ExecuteAsync(StarWarsSchema, @"
            query HeroNameQuery {
              hero {
                name
                secretBackstory
              }
            }
            ").ShouldEqual(new
            {
                data = new
                {
                    hero = new
                    {
                        name = "R2-D2",
                        secretBackstory = (string) null
                    }
                },
                errors = new object[]
                {
                    new
                    {
                        message = "secretBackstory is secret",
                        locations = new object[]
                        {
                            new {line = 5, column = 17}
                        },
                        path = new object[] {"hero", "secretBackstory"}
                    }
                }
            });

        [Fact]
        public Task CorrectlyReportsErrorOnAccessingSecretBackstoryInAList()
            => ExecuteAsync(StarWarsSchema, @"
            query HeroNameQuery {
              hero {
                name
                friends {
                  name
                  secretBackstory
                }
              }
            }
            ").ShouldEqual(new
            {
                data = new
                {
                    hero = new
                    {
                        name = "R2-D2",
                        friends = new object[]
                        {
                            new
                            {
                                name = "Luke Skywalker",
                                secretBackstory = (string) null
                            },
                            new
                            {
                                name = "Han Solo",
                                secretBackstory = (string) null
                            },
                            new
                            {
                                name = "Leia Organa",
                                secretBackstory = (string) null
                            }
                        }
                    }
                },
                errors = new object[]
                {
                    new
                    {
                        message = "secretBackstory is secret",
                        locations = new object[]
                        {
                            new {line = 7, column = 19}
                        },
                        path = new object[] {"hero", "friends", 0, "secretBackstory"}
                    },
                    new
                    {
                        message = "secretBackstory is secret",
                        locations = new object[]
                        {
                            new {line = 7, column = 19}
                        },
                        path = new object[] {"hero", "friends", 1, "secretBackstory"}
                    },
                    new
                    {
                        message = "secretBackstory is secret",
                        locations = new object[]
                        {
                            new {line = 7, column = 19}
                        },
                        path = new object[] {"hero", "friends", 2, "secretBackstory"}
                    }
                }
            });

        [Fact]
        public Task CorrectlyReportsErrorOnAccessingSecretBackstoryThroughAnAlias()
            => ExecuteAsync(StarWarsSchema, @"
            query HeroNameQuery {
              mainHero: hero {
                name
                story: secretBackstory
              }
            }
            ").ShouldEqual(new
            {
                data = new
                {
                    mainHero = new
                    {
                        name = "R2-D2",
                        story = (string) null
                    }
                },
                errors = new object[]
                {
                    new
                    {
                        message = "secretBackstory is secret",
                        locations = new object[]
                        {
                            new {line = 5, column = 17}
                        },
                        path = new object[] {"mainHero", "story"}
                    }
                }
            });
    }
}