#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine;
using GraphZen.TypeSystem;

namespace GraphZen.StarWars
{
    [NoReorder]
    public abstract class StarWarsSchemaAndData : ExecutorHarness
    {
        [Description("A character in the Star Wars Trilogy")]
        [GraphQLName("Character")]
        public interface ICharacter
        {
            [Description("The id of the character.")]
            string Id { get; }

            [GraphQLCanBeNull]
            [Description("The name of the character.")]
            string Name { get; }

            [GraphQLIgnore]
            string[] FriendIds { get; }

            [GraphQLCanBeNull]
            [GraphQLListItemCanBeNull]
            [Description("Which movies they appear in.")]
            Episode[] AppearsIn { get; }

            [GraphQLCanBeNull]
            [Description("All secrets about their past.")]
            string SecretBackstory { get; }

            [GraphQLCanBeNull]
            [GraphQLListItemCanBeNull]
            [Description("The friends of the character, or an empty list if they have none.")]
            Task<ICharacter[]> FriendsAsync();
        }

        [Description("A humanoid creature in the Star Wars universe.")]
        public class Human : ICharacter
        {
            [Description("The home planet of the human, or null if unknown.")]
            [GraphQLCanBeNull]
            public string HomePlanet { get; set; }

            [Description("The id of the human.")]
            public string Id { get; set; }

            [GraphQLCanBeNull]
            [Description("The name of the human.")]
            public string Name { get; set; }

            [GraphQLIgnore]
            public string[] FriendIds { get; set; }

            [GraphQLCanBeNull]
            [GraphQLListItemCanBeNull]
            [Description("The friends of the human, or an empty list if they have none.")]
            public async Task<ICharacter[]> FriendsAsync()
            {
                var friends = FriendIds.Select(GetCharacterAsync).ToArray();
                await Task.WhenAll(friends);
                return friends.Select(_ => _.Result).ToArray();
            }

            [Description("Which movies they appear in.")]
            [GraphQLCanBeNull]
            [GraphQLListItemCanBeNull]
            public Episode[] AppearsIn { get; set; }

            [GraphQLCanBeNull]
            [Description("Where are they from and how they came to be who they are.")]
            public string SecretBackstory => throw new Exception("secretBackstory is secret");
        }

        [Description("A mechanical creature in the Star Wars universe.")]
        public class Droid : ICharacter
        {
            [GraphQLCanBeNull]
            [Description("The primary function of the droid")]
            public string PrimaryFunction { get; set; }

            [Description("The id of the droid.")]
            public string Id { get; set; }

            [GraphQLCanBeNull]
            [Description("The name of the droid.")]
            public string Name { get; set; }

            [GraphQLIgnore]
            public string[] FriendIds { get; set; }

            [GraphQLCanBeNull]
            [GraphQLListItemCanBeNull]
            public async Task<ICharacter[]> FriendsAsync()
            {
                var friends = FriendIds.Select(GetCharacterAsync).ToArray();
                await Task.WhenAll(friends);
                return friends.Select(_ => _.Result).ToArray();
            }

            [GraphQLCanBeNull]
            [GraphQLListItemCanBeNull]
            [Description("Which movies they appear in.")]
            public Episode[] AppearsIn { get; set; }

            [GraphQLCanBeNull]
            [Description("Construction date and the name of the designer.")]
            public string SecretBackstory { get; set; }
        }

        private static Human Luke { get; } = new Human
        {
            Id = "1000",
            Name = "Luke Skywalker",
            FriendIds = new[] { "1002", "1003", "2000", "2001" },
            AppearsIn = new[] { Episode.NewHope, Episode.Empire, Episode.Jedi },
            HomePlanet = "Tatooine"
        };

        private static Human Vader { get; } = new Human
        {
            Id = "1001",
            Name = "Darth Vader",
            FriendIds = new[] { "1004" },
            AppearsIn = new[] { Episode.NewHope, Episode.Empire, Episode.Jedi },
            HomePlanet = "Tatooine"
        };

        private static Human Han { get; } = new Human
        {
            Id = "1002",
            Name = "Han Solo",
            FriendIds = new[] { "1000", "1003", "2001" },
            AppearsIn = new[] { Episode.NewHope, Episode.Empire, Episode.Jedi }
        };

        private static Human Leia { get; } = new Human
        {
            Id = "1003",
            Name = "Leia Organa",
            FriendIds = new[] { "1000", "1002", "2000", "2001" },
            AppearsIn = new[] { Episode.NewHope, Episode.Empire, Episode.Jedi },
            HomePlanet = "Alderaan"
        };

        private static Human Tarkin { get; } = new Human
        {
            Id = "1004",
            Name = "Wilhuff Tarkin",
            FriendIds = new[] { "1001" },
            AppearsIn = new[] { Episode.Empire }
        };

        private static readonly IReadOnlyDictionary<string, Human> HumanData = new Dictionary<string, Human>
        {
            {"1000", Luke},
            {"1001", Vader},
            {"1002", Han},
            {"1003", Leia},
            {"1004", Tarkin}
        };

        private static Droid ThreePio { get; } = new Droid
        {
            Id = "2000",
            Name = "C-3P0",
            FriendIds = new[] { "1000", "1002", "1003", "2001" },
            AppearsIn = new[] { Episode.NewHope, Episode.Empire, Episode.Jedi },
            PrimaryFunction = "Protocol"
        };

        private static Droid Artoo { get; } = new Droid
        {
            Id = "2001",
            Name = "R2-D2",
            FriendIds = new[] { "1000", "1002", "1003" },
            AppearsIn = new[] { Episode.NewHope, Episode.Empire, Episode.Jedi },
            PrimaryFunction = "Astromech"
        };

        private static readonly IReadOnlyDictionary<string, Droid> DroidData = new Dictionary<string, Droid>
        {
            {"2000", ThreePio},
            {"2001", Artoo}
        };

        protected static Task<ICharacter> GetCharacterAsync(string id) =>
            Task.FromResult(HumanData.TryGetValue(id, out var human) ? (ICharacter)human :
                DroidData.TryGetValue(id, out var droid) ? droid : null);

        protected static IEnumerable<Task<ICharacter>> GetFriendsAsync(ICharacter character) =>
            character.FriendIds.Select(GetCharacterAsync);

        protected static ICharacter GetHero(Episode? episode) => episode == Episode.Empire ? (ICharacter)Luke : Artoo;

        protected static Human GetHuman(string id) => HumanData.TryGetValue(id, out var human) ? human : null;
        protected static Droid GetDroid(string id) => DroidData.TryGetValue(id, out var droid) ? droid : null;


        protected static Schema SchemaBuilderSchema = Schema.Create(sb =>
        {
            sb.Enum("Episode")
                .Description("One of the films in the Star Wars Trilogy")
                .Value("NEW_HOPE", _ => _
                    .Description("Released in 1977")
                    .CustomValue(Episode.NewHope))
                .Value("EMPIRE", _ => _
                    .Description("Released in 1980")
                    .CustomValue(Episode.Empire))
                .Value("JEDI", _ => _
                    .Description("Released in 1983")
                    .CustomValue(Episode.Jedi));

            sb.Interface("Character").Description("A character in the Star Wars Trilogy")
                .Field("id", "String!", _ => _
                    .Description("The id of the character."))
                .Field("name", "String", _ => _
                    .Description("The name of the character."))
                .Field("friends", "[Character]", _ => _
                    .Description("The friends of the character, or an empty list if they have none."))
                .Field("appearsIn", "[Episode]", _ => _
                    .Description("Which movies they appear in."))
                .Field("secretBackstory", "String", _ => _
                    .Description("All secrets about their past."));

            sb.Object("Human").Description("A humanoid creature in the Star Wars universe.")
                .ImplementsInterface("Character")
                .Field("id", "String!", _ => _.Description("The id of the human."))
                .Field("name", "String", _ => _.Description("The name of the human."))
                .Field("friends", "[Character]", _ => _
                    .Description("The friends of the human, or an empty list if they have none.")
                    .Resolve(human => GetFriendsAsync((Human)human)))
                .Field("appearsIn", "[Episode]", _ => _
                    .Description("Which movies they appear in."))
                .Field("homePlanet", "String", _ => _
                    .Description("The home planet of the human, or null if unknown."))
                .Field("secretBackstory", "String", _ => _
                    .Description("Where are they from and how they came to be who they are.")
                    .Resolve(() => throw new Exception("secretBackstory is secret")));

            sb.Object("Droid")
                .Description("A mechanical creature in the Star Wars universe.")
                .ImplementsInterface("Character")
                .Field("id", "String!", _ => _
                    .Description("The id of the droid."))
                .Field("name", "String", _ => _
                    .Description("The name of the droid."))
                .Field("friends", "[Character]", _ => _
                    .Resolve(human => GetFriendsAsync((Droid)human)))
                .Field("appearsIn", "[Episode]", _ => _
                    .Description("Which movies they appear in."))
                .Field("secretBackstory", "String", _ => _
                    .Description("Construction date and the name of the designer.")
                    .Resolve(() => throw new Exception("secretBackstory is secret")))
                .Field("primaryFunction", "String", _ => _
                    .Description("The primary function of the droid"));

            sb.Object("Query")
                .Field("hero", "Character", _ => _
                    .Argument("episode", "Episode", arg => arg
                        .Description(
                            "If omitted, returns the hero of the whole saga. If provided, returns the hero of that particular episode."))
                    .Resolve((root, args) => GetHero(args.episode)))
                .Field("human", "Human", _ => _
                    .Argument("id", "String!", arg => arg.Description("id of the human"))
                    .Resolve((root, args) => GetHuman(args.id)))
                .Field("droid", "Droid", _ => _
                    .Argument("id", "String!", arg => arg.Description("id of the droid"))
                    .Resolve((root, args) => GetHuman(args.id)));
        });

        protected static Schema StarWarsSchema => SchemaBuilderSchema;
        protected static Schema CodeFirstSchema => Schema.Create(_ => { _.Object<Query>(); });

        [Description("One of the films in the Star Wars Trilogy")]
        public enum Episode
        {
            [Description("Released in 1977")]
            [GraphQLName("NEW_HOPE")]
            NewHope = 4,

            [Description("Released in 1980")]
            [GraphQLName("EMPIRE")]
            Empire = 5,

            [Description("Released in 1983")]
            [GraphQLName("JEDI")]
            Jedi = 6
        }

        public class Query
        {
            // ReSharper disable once PossibleInvalidOperationException
            [GraphQLCanBeNull]
            public ICharacter Hero(
                [Description(
                    "If omitted, returns the hero of the whole saga. If provided, returns the hero of that particular episode.")]
                Episode? episode) => GetHero(episode);

            [UsedImplicitly]
            [GraphQLCanBeNull]
            [GraphQLName("human")]
            public Task<Human> GetHumanAsync([Description("id of the human")] string id) =>
                Task.FromResult(GetHuman(id));

            [GraphQLName("droid")]
            [GraphQLCanBeNull]
            [UsedImplicitly]
            public Droid GetDroidData([Description("id of the droid")] string id) => GetDroid(id);
        }
    }
}