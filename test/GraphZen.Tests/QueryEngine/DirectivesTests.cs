// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.QueryEngine;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.QueryEngine
{
    [NoReorder]
    public class DirectivesTests : ExecutorHarness
    {
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class Data
        {
            public string A() => "a";

            public string B() => "b";
        }

        public static Schema Schema = Schema.Create(_ =>
        {
            _.Object("TestType").Field("a", "String").Field("b", "String");
            _.QueryType("TestType");
        });

        private Task<ExecutionResult> ExecuteAsync(string query) => ExecuteAsync(Schema, query, new Data());


        [Fact]
        public Task WorksWithoutDirectives() =>
            ExecuteAsync("{ a, b }").ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task IfTrueIncludesScalar() => ExecuteAsync("{ a, b @include(if: true) }")
            .ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task IfFalseOmitsScalar() =>
            ExecuteAsync("{ a, b @include(if: false) }").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task UnlessFalseIncludesScalar() => ExecuteAsync("{ a, b @skip(if: false) }")
            .ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task UnlessTrueOmitsScalar() =>
            ExecuteAsync("{ a, b @skip(if: true) }").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task IfFalseOmitsFragmentSpread() =>
            ExecuteAsync(@"
        query {
          a
          ...Frag @include(if: false)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task IfTrueIncludesFragmentSpread() =>
            ExecuteAsync(@"
        query {
          a
          ...Frag @include(if: true)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task UnlessFalseIncludesFragmentSpread() =>
            ExecuteAsync(@"
        query {
          a
          ...Frag @skip(if: false)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task UnlessTrueOmitsFragmentSpread() =>
            ExecuteAsync(@"
        query {
          a
          ...Frag @skip(if: true)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task IfFalseOmitsInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ... on TestType @include(if: false) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task IfTrueIncludesInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ... on TestType @include(if: true) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a", b = "b"}});


        [Fact]
        public Task UnlessFalseIncludesInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ... on TestType @skip(if: false) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task UnlessTrueIncludesInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ... on TestType @skip(if: true) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a"}});

        //

        [Fact]
        public Task IfFalseOmitsAnonymousInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ... @include(if: false) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task IfTrueIncludesAnonymousInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ... @include(if: true) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a", b = "b"}});


        [Fact]
        public Task UnlessFalseIncludesAnonymousInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ... @skip(if: false) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task UnlessTrueIncludesAnonymousInlineFragment() =>
            ExecuteAsync(@"
        query {
          a
          ...  @skip(if: true) {
            b
          }
        }
        ").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task IncludeAndNoSkip() =>
            ExecuteAsync(@"
        {
          a
          b @include(if: true) @skip(if: false)
        }
        ").ShouldEqual(new {data = new {a = "a", b = "b"}});

        [Fact]
        public Task IncludeAndSkip() =>
            ExecuteAsync(@"
        {
          a
          b @include(if: true) @skip(if: true)
        }
        ").ShouldEqual(new {data = new {a = "a"}});

        [Fact]
        public Task NoIncludeOrSkip() =>
            ExecuteAsync(@"
        {
          a
          b @include(if: false) @skip(if: false)
        }
        ").ShouldEqual(new {data = new {a = "a"}});
    }
}