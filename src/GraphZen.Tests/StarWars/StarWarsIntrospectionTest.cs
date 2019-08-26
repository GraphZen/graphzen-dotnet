// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.StarWars
{
    [NoReorder]
    public class StarWarsIntrospectionTest : StarWarsSchemaAndData
    {
        [Fact]
        public Task AllowsQueryingTheSchemaForTypes()
        {
            return ExecuteAsync(StarWarsSchema, @"

                query IntrospectionTypeQuery {
                  __schema {
                    types {
                      name
                    }
                  }
                }

            ").ShouldEqual(new
            {
                data = new
                {
                    __schema = new
                    {
                        types = new object[]
                        {
                            new {name = "Query"},
                            new {name = "Episode"},
                            new {name = "Character"},
                            new {name = "String"},
                            new {name = "Human"},
                            new {name = "Droid"},
                            new {name = "__Schema"},
                            new {name = "__Type"},
                            new {name = "__TypeKind"},
                            new {name = "Boolean"},
                            new {name = "Float"},
                            new {name = "ID"},
                            new {name = "Int"},
                            new {name = "__Field"},
                            new {name = "__InputValue"},
                            new {name = "__EnumValue"},
                            new {name = "__Directive"},
                            new {name = "__DirectiveLocation"}
                        }
                    }
                }
            }, new ResultComparisonOptions
            {
                SortBeforeCompare = true
            });
        }

        [Fact]
        public Task AllowsQueryingTheSchemaForTheQueryType()
        {
            return ExecuteAsync(StarWarsSchema, @"
            
            query IntrospectionQueryTypeQuery {
              __schema {
                queryType {
                  name
                }
              }
            }
        
        ").ShouldEqual(new
            {
                data = new
                {
                    __schema = new
                    {
                        queryType = new
                        {
                            name = "Query"
                        }
                    }
                }
            });
        }

        [Fact]
        public Task AllowsQueryingTheSchemaForASpecificType()
        {
            return ExecuteAsync(StarWarsSchema, @"
            
        query IntrospectionDroidTypeQuery {
          __type(name: ""Droid"") {
            name
          }
        }
        
        ").ShouldEqual(new
            {
                data = new
                {
                    __type = new
                    {
                        name = "Droid"
                    }
                }
            });
        }

        [Fact]
        public Task AllowsQueryingTheSchemaForAnObjectKind()
        {
            return ExecuteAsync(StarWarsSchema, @"
            
        query IntrospectionDroidKindQuery {
          __type(name: ""Droid"") {
            name
            kind
          }
        }
        
        ").ShouldEqual(new
            {
                data = new
                {
                    __type = new
                    {
                        name = "Droid",
                        kind = "OBJECT"
                    }
                }
            });
        }

        [Fact]
        public Task AllowsQueryingTheSchemaForAnInterfaceKind()
        {
            return ExecuteAsync(StarWarsSchema, @"
            
        query IntrospectionCharacterKindQuery {
          __type(name: ""Character"") {
            name
            kind
          }
        }
        
        ").ShouldEqual(new
            {
                data = new
                {
                    __type = new
                    {
                        name = "Character",
                        kind = "INTERFACE"
                    }
                }
            });
        }

        [Fact]
        public Task AllowsQueryingTheSchemaForObjectFields()
        {
            return ExecuteAsync(StarWarsSchema, @"
            
        query IntrospectionDroidFieldsQuery {
          __type(name: ""Droid"") {
            name
            fields {
              name
              type {
                name
                kind
              }
            }
          }
        }
        
        ").ShouldEqual(new
            {
                data = new
                {
                    __type = new
                    {
                        name = "Droid",
                        fields = new object[]
                        {
                            new
                            {
                                name = "id",
                                type = new
                                {
                                    name = (string) null,
                                    kind = "NON_NULL"
                                }
                            },
                            new
                            {
                                name = "name",
                                type = new
                                {
                                    name = "String",
                                    kind = "SCALAR"
                                }
                            },
                            new
                            {
                                name = "friends",
                                type = new
                                {
                                    name = (string) null,
                                    kind = "LIST"
                                }
                            },
                            new
                            {
                                name = "appearsIn",
                                type = new
                                {
                                    name = (string) null,
                                    kind = "LIST"
                                }
                            },

                            new
                            {
                                name = "secretBackstory",
                                type = new
                                {
                                    name = "String",
                                    kind = "SCALAR"
                                }
                            },
                            new
                            {
                                name = "primaryFunction",
                                type = new
                                {
                                    name = "String",
                                    kind = "SCALAR"
                                }
                            }
                        }
                    }
                }
            });
        }

        [Fact]
        public Task AllowsQueryingTheSchemaForNestedObjectFields()
        {
            return ExecuteAsync(StarWarsSchema, @"
            
        query IntrospectionDroidNestedFieldsQuery {
          __type(name: ""Droid"") {
            name
            fields {
              name
              type {
                name
                kind
                ofType {
                  name
                  kind
                }
              }
            }
          }
        } 
        
        ").ShouldEqual(new
            {
                data = new
                {
                    __type = new
                    {
                        name = "Droid",
                        fields = new object[]
                        {
                            new
                            {
                                name = "id",
                                type = new
                                {
                                    name = (string) null,
                                    kind = "NON_NULL",
                                    ofType = new
                                    {
                                        name = "String",
                                        kind = "SCALAR"
                                    }
                                }
                            },
                            new
                            {
                                name = "name",
                                type = new
                                {
                                    name = "String",
                                    kind = "SCALAR",
                                    ofType = (object) null
                                }
                            },
                            new
                            {
                                name = "friends",
                                type = new
                                {
                                    name = (string) null,
                                    kind = "LIST",
                                    ofType = new
                                    {
                                        name = "Character",
                                        kind = "INTERFACE"
                                    }
                                }
                            },
                            new
                            {
                                name = "appearsIn",
                                type = new
                                {
                                    name = (string) null,
                                    kind = "LIST",
                                    ofType = new
                                    {
                                        name = "Episode",
                                        kind = "ENUM"
                                    }
                                }
                            },

                            new
                            {
                                name = "secretBackstory",
                                type = new
                                {
                                    name = "String",
                                    kind = "SCALAR",
                                    ofType = (object) null
                                }
                            },
                            new
                            {
                                name = "primaryFunction",
                                type = new
                                {
                                    name = "String",
                                    kind = "SCALAR",
                                    ofType = (object) null
                                }
                            }
                        }
                    }
                }
            });
        }

        [Fact]
        public Task AllowsQueryingSchemaForFieldArgs()
        {
            return ExecuteAsync(StarWarsSchema, @"

        query IntrospectionQueryTypeQuery {
          __schema {
            queryType {
              fields {
                name
                args {
                  name
                  description
                  type {
                    name
                    kind
                    ofType {
                      name
                      kind
                    }
                  }
                  defaultValue
                }
              }
            }
          }
        }", throwOnError: true).ShouldEqual(new
            {
                data = new
                {
                    __schema = new
                    {
                        queryType = new
                        {
                            fields = new object[]
                            {
                                new
                                {
                                    name = "hero",
                                    args = new object[]
                                    {
                                        new
                                        {
                                            description =
                                                "If omitted, returns the hero of the whole saga. If provided, returns the hero of that particular episode.",
                                            name = "episode",
                                            type = new
                                            {
                                                kind = "ENUM",
                                                name = "Episode",
                                                ofType = (object) null
                                            },
                                            defaultValue = (object) null
                                        }
                                    }
                                },
                                new
                                {
                                    name = "human",
                                    args = new object[]
                                    {
                                        new
                                        {
                                            name = "id",
                                            description = "id of the human",
                                            type = new
                                            {
                                                kind = "NON_NULL",
                                                name = (string) null,
                                                ofType = new
                                                {
                                                    kind = "SCALAR",
                                                    name = "String"
                                                }
                                            },
                                            defaultValue = (object) null
                                        }
                                    }
                                },
                                new
                                {
                                    name = "droid",
                                    args = new object[]
                                    {
                                        new
                                        {
                                            name = "id",
                                            description = "id of the droid",
                                            type = new
                                            {
                                                kind = "NON_NULL",
                                                name = (string) null,
                                                ofType = new
                                                {
                                                    kind = "SCALAR",
                                                    name = "String"
                                                }
                                            },
                                            defaultValue = (object) null
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }, new ResultComparisonOptions
            {
                SortBeforeCompare = true
            });
        }

        [Fact]
        public Task AllowsQueryingTheSchemaForDocumentation()
        {
            return ExecuteAsync(StarWarsSchema, @"

        query IntrospectionDroidDescriptionQuery {
          __type(name: ""Droid"") {
            name
            description
          }
        }

").ShouldEqual(new
            {
                data = new
                {
                    __type = new
                    {
                        name = "Droid",
                        description = "A mechanical creature in the Star Wars universe."
                    }
                }
            });
        }
    }
}