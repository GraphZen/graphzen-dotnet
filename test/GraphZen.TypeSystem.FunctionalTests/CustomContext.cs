using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests
{
    public class CustomContext : GraphQLContext
    {
        protected internal override void OnSchemaCreating(TypeSystem.SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Object("Foo");
            base.OnSchemaCreating(schemaBuilder);
        }
    }

    public class CustomContextTests
    {
        [Fact]
        public void Test()
        {
            var context = new CustomContext();
            context.Schema.GetObject("Foo").Should().NotBeNull();
            var schema = Schema.Create<CustomContext>(_ => { });
            schema.GetType("Foo");
        }
    }
}
