using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using Xunit.Sdk;

namespace GraphZen.TypeSystem.Tests
{
    public class TypeIdentityTests
    {
        [Fact(Skip = "wip")]
        public void renaming_clr_typed_item_should_retain_type_identity()
        {
            var schema = new SchemaDefinition();
            var _ = new SchemaBuilder(schema);
            // schema.GetTypeIdentities(true).XDump().Count().Should().Be(3);
            _.AddSpecScalars();
            _.AddSpecDirectives();
            _.AddIntrospectionTypes();
            schema.GetTypeIdentities(true).Dump("identities", true).Count().Should().Be(1);
            //_.InputObject("Foo").Field<long>("field");
            //var scalar = _.Scalar<long>();
            //schema.GetTypeIdentities().XDump().Count().Should().Be(3);
            //scalar.Name("Bar");
            //schema.GetTypeIdentities().Dump().Count().Should().Be(4);
        }


        [Fact]
        // ReSharper disable once InconsistentNaming
        public void test()
        {
            var schema3 = Schema.Create(_ =>
            {
                void DumpIds(string label) => _.GetDefinition().GetTypeIdentities().Dump(label, true);
                // type id 105: input object Foo
                _.InputObject("Foo")
                    // type id 106: Bar
                    .Field("field", "Bar")
                    // type id 107: FooBar
                    .Field("field2", "FooBar");

                // type id 109: input object Baz
                var baz = _.InputObject("Baz");
                DumpIds("Baz");

                //DumpIds("Baz");
                var bar = baz.Name("Bar");
                DumpIds("Baz => Bar");
                //DumpIds("Bar");
                var fooBar = bar.Name("FooBar");
                //DumpIds("FooBar");
            });
            schema3.GetInputObject("Foo").GetField("field").InputType.MaybeGetNamedType()?.Name.Should().Be("FooBar");

            var schema = Schema.Create(_ =>
                        {
                            _.InputObject("Foo").Field("field", "Bar");
                            _.InputObject("Bar").Name("Baz");

                        });
            schema.GetInputObject("Foo").GetField("field").InputType.MaybeGetNamedType()?.Name.Should().Be("Baz");

            var schema2 = Schema.Create(_ =>
                        {
                            _.InputObject("Foo").Field("field", "Bar");
                            _.InputObject("Baz").Name("Bar").Name("FooBar");

                        });
            schema2.GetInputObject("Foo").GetField("field").InputType.MaybeGetNamedType()?.Name.Should().Be("FooBar");

        }

        [Fact]
        public void test_type()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar("Foo");
                _.InputObject("Bar").Field("field", "[Foo!]!");
            });
            var foo = schema.GetScalar("Foo");
            var fieldType = (NonNullType) schema.GetInputObject("Bar").GetField("field").FieldType;
            fieldType.InnerType.Should().Be(foo);
        }
    }
}