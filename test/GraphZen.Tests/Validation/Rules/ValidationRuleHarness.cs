// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Validation.Rules
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public abstract class ValidationRuleHarness
    {
        public abstract ValidationRule RuleUnderTest { get; }

        public static Schema TestSchema { get; } = Schema.Create(sb =>
        {
            sb.Interface("Being")
                .Field("name", "String",
                    _ => _.Argument("surname", "Boolean"));

            sb.Interface("Pet")
                .Field("name", "String",
                    _ => _.Argument("surname", "Boolean"));

            sb.Interface("Canine")
                .Field("name", "String",
                    _ => _.Argument("surname", "Boolean"));

            sb.Enum("DogCommand")
                .Value("SIT", _ => _.CustomValue(0))
                .Value("HEEL", _ => _.CustomValue(1))
                .Value("DOWN", _ => _.CustomValue(2));

            sb.Object("Dog")
                .Field("name", "String", _ => _.Argument("surname", "Boolean"))
                .Field("nickname", "String")
                .Field("barkVolume", "Int")
                .Field("barks", "Boolean")
                .Field("doesKnowCommand", "Boolean", _ => _
                    .Argument("dogCommand", "DogCommand"))
                .Field("isHouseTrained", "Boolean", _ => _
                    .Argument("atOtherHomes", "Boolean", arg => arg
                        .DefaultValue(true)))
                .Field("isAtLocation", "Boolean", _ => _
                    .Argument("x", "Int")
                    .Argument("y", "Int"))
                .ImplementsInterfaces("Being", "Pet", "Canine");

            sb.Object("Cat")
                .Field("name", "String", _ => _.Argument("surname", "Boolean"))
                .Field("nickname", "String")
                .Field("meowsVolume", "Int")
                .Field("meows", "Boolean")
                .Field("furColor", "FurColor")
                .ImplementsInterfaces("Being", "Pet");

            sb.Union("CatOrDog").OfTypes("Cat", "Dog");

            sb.Interface("Intelligent")
                .Field("iq", "Int");

            sb.Object("Human")
                .ImplementsInterfaces("Being", "Intelligent")
                .Field("name", "String", _ => _.Argument("surname", "Boolean"))
                .Field("pets", "[Pet]")
                .Field("relatives", "[Human]")
                .Field("iq", "Int");

            sb.Object("Alien")
                .ImplementsInterfaces("Being", "Intelligent")
                .Field("iq", "Int")
                .Field("name", "String", _ => _.Argument("surname", "Boolean"))
                .Field("numEyes", "Int");

            sb.Union("DogOrHuman").OfTypes("Dog", "Human");

            sb.Union("HumanOrAlien").OfTypes("Human", "Alien");

            sb.Enum("FurColor")
                .Value("BROWN", _ => _.CustomValue(0))
                .Value("BLACK", _ => _.CustomValue(1))
                .Value("TAN", _ => _.CustomValue(2))
                .Value("SPOTTED", _ => _.CustomValue(3))
                .Value("NO_FUR", _ =>
                {
                    // _.CustomValue(null);
                })
                .Value("UNKNOWN");

            sb.InputObject("ComplexInput")
                .Field("requiredField", "Boolean!")
                .Field("nonNullField", "Boolean!", _ => _.DefaultValue(false))
                .Field("intField", "Int")
                .Field("stringField", "String")
                .Field("booleanField", "Boolean")
                .Field("stringListField", "[String]");

            sb.Object("ComplicatedArgs")
                .Field("intArgField", "String", _ => _.Argument("intArg", "Int"))
                .Field("nonNullIntArgField", "String", _ => _.Argument("nonNullIntArg", "Int!"))
                .Field("stringArgField", "String", _ => _.Argument("stringArg", "String"))
                .Field("booleanArgField", "String", _ => _.Argument("booleanArg", "Boolean"))
                .Field("enumArgField", "String", _ => _.Argument("enumArg", "FurColor"))
                .Field("floatArgField", "String", _ => _.Argument("floatArg", "Float"))
                .Field("idArgField", "String", _ => _.Argument("idArg", "ID"))
                .Field("stringListArgField", "String", _ => _.Argument("stringListArg", "[String]"))
                .Field("stringListNonNullArgField", "String", _ => _.Argument("stringListNonNullArg", "[String!]"))
                .Field("complexArgField", "String", _ => _.Argument("complexArg", "ComplexInput"))
                .Field("multipleReqs", "String", _ => _
                    .Argument("req1", "Int!")
                    .Argument("req2", "Int!"))
                .Field("nonNullFieldWithDefault", "String", _ => _.Argument("arg", "Int!", arg => arg.DefaultValue(0)))
                .Field("multipleOpts", "String", _ => _
                    .Argument("opt1", "Int", arg => arg.DefaultValue(0))
                    .Argument("opt2", "Int", arg => arg.DefaultValue(0)))
                .Field("multipleOptAndReq", "String", _ => _
                    .Argument("req1", "Int!")
                    .Argument("req2", "Int!")
                    .Argument("opt1", "Int", arg => arg.DefaultValue(0))
                    .Argument("opt2", "Int", arg => arg.DefaultValue(0)));

            sb.Scalar("Invalid")
                .Serializer(Maybe.Some)
                .LiteralParser(node => throw new Exception("Invalid scalar is always invalid: " + node.GetValue()))
                .ValueParser(node => throw new Exception("Invalid scalar is always invalid: " + node));

            sb.Scalar("Any")
                .Serializer(Maybe.Some)
                .LiteralParser(Maybe.Some<object>)
                .ValueParser(Maybe.Some);

            sb.Object("QueryRoot")
                .Field("human", "Human", _ => _.Argument("id", "ID"))
                .Field("alien", "Alien")
                .Field("cat", "Cat")
                .Field("pet", "Pet")
                .Field("catOrDog", "CatOrDog")
                .Field("dogOrHuman", "DogOrHuman")
                .Field("humanOrAlien", "HumanOrAlien")
                .Field("complicatedArgs", "ComplicatedArgs")
                .Field("invalidArg", "String", _ => _.Argument("arg", "Invalid"))
                .Field("anyArg", "String", _ => _.Argument("arg", "Any"));
            sb.QueryType("QueryRoot");

            sb.Directive("onQuery").Locations(DirectiveLocation.Query);
            sb.Directive("onMutation").Locations(DirectiveLocation.Mutation);
            sb.Directive("onSubscription").Locations(DirectiveLocation.Subscription);
            sb.Directive("onField").Locations(DirectiveLocation.Field);
            sb.Directive("onFragmentDefinition").Locations(DirectiveLocation.FragmentDefinition);
            sb.Directive("onFragmentSpread").Locations(DirectiveLocation.FragmentSpread);
            sb.Directive("onInlineFragment").Locations(DirectiveLocation.InlineFragment);
        });

        protected static ExpectedError Error(string message, params (int line, int column)[] lineColumnPairs) =>
            new ExpectedError(message,
                lineColumnPairs.Select(pair => new SourceLocation(pair.line, pair.column)).ToArray(), null);

        private void ExpectValidSDL(ValidationRule rule, string sdl)
        {
            var sdlSyntax = Parser.ParseDocument(sdl);
            var result = new DocumentValidator(new[] {rule}).Validate(sdlSyntax);
            result.Should().BeEmpty("it should validate");
        }

        private void ExpectValidQuery(Schema schema, ValidationRule rule, string query)
        {
            var document = Parser.ParseDocument(query);
            var result = new QueryValidator(new[] {rule}).Validate(schema, document);
            result.Should().BeEmpty("it should validate");
        }

        private void ExpectInvalidQuery(Schema schema, ValidationRule rule, string query,
            IReadOnlyList<ExpectedError> expectedErrors)
        {
            var document = Parser.ParseDocument(query);
            var result = new QueryValidator(new[] {rule}).Validate(schema, document)
                // Convert for comparison
                .Select(e => new ExpectedError(e))
                .ToArray();
            result.Should().HaveCountGreaterThan(0, "it should not validate");
            expectedErrors.Should().BeEquivalentToJsonFromObject(result);
        }

        private void ExpectInvalidSDL(ValidationRule rule, string sdl,
            IReadOnlyList<ExpectedError> expectedErrors)
        {
            var sdlSyntax = Parser.ParseDocument(sdl);
            var result = new DocumentValidator(new[] {rule}).Validate(sdlSyntax)
                // Convert for comparison
                .Select(e => new ExpectedError(e))
                .ToArray();
            result.Should().HaveCountGreaterThan(0, "it should not validate");
            expectedErrors.Should().BeEquivalentToJsonFromObject(result);
        }

        protected void QueryShouldPass(string query) => ExpectValidQuery(TestSchema, RuleUnderTest, query);

        protected void QueryShouldFail(string query, ExpectedError error, params ExpectedError[] errors) =>
            ExpectInvalidQuery(TestSchema, RuleUnderTest, query, new[] {error}.Concat(errors).ToArray());

        protected void SDLShouldPass(string sdl) => ExpectValidSDL(RuleUnderTest, sdl);

        protected void SDLShouldFail(string sdl, ExpectedError error, params ExpectedError[] errors) =>
            ExpectInvalidSDL(RuleUnderTest, sdl.Dedent(), new[] {error}.Concat(errors).ToArray());
    }
}