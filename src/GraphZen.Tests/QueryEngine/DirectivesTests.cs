// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.QueryEngine
{
    [NoReorder]
    public class DirectivesTests : ExecutorHarness
    {
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class Data
        {
            public string A()
            {
                return "a";
            }

            public string B()
            {
                return "b";
            }
        }

        public static Schema Schema = Schema.Create(_ =>
        {
            _.Object("TestType").Field("a", "String").Field("b", "String");
            _.QueryType("TestType");
        });

        private Task<ExecutionResult> ExecuteAsync(string query)
        {
            return ExecuteAsync(Schema, query, new Data());
        }


        [Fact]
        public Task WorksWithoutDirectives()
        {
            return ExecuteAsync("{ a, b }").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task IfTrueIncludesScalar()
        {
            return ExecuteAsync("{ a, b @include(if: true) }").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task IfFalseOmitsScalar()
        {
            return ExecuteAsync("{ a, b @include(if: false) }").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task UnlessFalseIncludesScalar()
        {
            return ExecuteAsync("{ a, b @skip(if: false) }").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task UnlessTrueOmitsScalar()
        {
            return ExecuteAsync("{ a, b @skip(if: true) }").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task IfFalseOmitsFragmentSpread()
        {
            return ExecuteAsync(@"
        query {
          a
          ...Frag @include(if: false)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task IfTrueIncludesFragmentSpread()
        {
            return ExecuteAsync(@"
        query {
          a
          ...Frag @include(if: true)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task UnlessFalseIncludesFragmentSpread()
        {
            return ExecuteAsync(@"
        query {
          a
          ...Frag @skip(if: false)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task UnlessTrueOmitsFragmentSpread()
        {
            return ExecuteAsync(@"
        query {
          a
          ...Frag @skip(if: true)
        }
        fragment Frag on TestType {
          b
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task IfFalseOmitsInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ... on TestType @include(if: false) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task IfTrueIncludesInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ... on TestType @include(if: true) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }


        [Fact]
        public Task UnlessFalseIncludesInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ... on TestType @skip(if: false) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task UnlessTrueIncludesInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ... on TestType @skip(if: true) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }

        //

        [Fact]
        public Task IfFalseOmitsAnonymousInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ... @include(if: false) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task IfTrueIncludesAnonymousInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ... @include(if: true) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }


        [Fact]
        public Task UnlessFalseIncludesAnonymousInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ... @skip(if: false) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task UnlessTrueIncludesAnonymousInlineFragment()
        {
            return ExecuteAsync(@"
        query {
          a
          ...  @skip(if: true) {
            b
          }
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task IncludeAndNoSkip()
        {
            return ExecuteAsync(@"
        {
          a
          b @include(if: true) @skip(if: false)
        }
        ").ShouldEqual(new { data = new { a = "a", b = "b" } });
        }

        [Fact]
        public Task IncludeAndSkip()
        {
            return ExecuteAsync(@"
        {
          a
          b @include(if: true) @skip(if: true)
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }

        [Fact]
        public Task NoIncludeOrSkip()
        {
            return ExecuteAsync(@"
        {
          a
          b @include(if: false) @skip(if: false)
        }
        ").ShouldEqual(new { data = new { a = "a" } });
        }
    }
}