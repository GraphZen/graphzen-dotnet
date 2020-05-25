using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests
{
    public class TypeIdentityTests
    {
        [Fact]
        public void known_type_identity_created_for_type()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.GetDefinition().GetTypeReferences() .Where(r => r.Identity.ToString().Contains("unknown output type Boolean")).Dump();
                _.GetDefinition().GetTypeIdentities() .Where(r => r.ToString().Contains("unknown output type Boolean")).Dump();
                var ids = _.GetDefinition().GetTypeIdentities().Dump("ids", true).ToList();
                ids.Count.Should().Be(1);
                var fooId = ids.Single();
                fooId.Name.Should().Be("Foo");
                fooId.Definition.Should().BeOfType<InputObjectTypeDefinition>();
            });
        }

        [Fact]
        // ReSharper disable once InconsistentNaming
        public void test()
        {
            var schema3 = Schema.Create(_ =>
            {
                void DumpIds(string label) => _.GetDefinition().GetTypeIdentities().Dump(label + " (ids)", true);

                void DumpRefs(string label) => _.GetDefinition().GetTypeReferences().Dump(label + " (refs)", true);
                // type id 105: input object Foo
                _.InputObject("InputObject")
                    // type id 106: Bar
                    .Field("field1", "Foo!")
                    // type id 107: FooBar
                    .Field("field2", "[Bar]");


                // type id 109: input object Baz
                var baz = _.InputObject("Baz");
                // DumpIds("Initial"); DumpRefs("Initial");

                var bar = baz.Name("Bar");
                // DumpIds("Baz => Bar"); DumpRefs("Baz => Bar");

                var foo = bar.Name("Foo");
                // DumpIds("Bar => Foo"); DumpRefs("Bar => Foo");

                foo.Name("FooBar");
                // DumpIds("Foo => FooBar"); DumpRefs("Foo=> FooBar");

                //_.RemoveInputObject("FooBar");
                DumpIds("Remove FooBar");
                DumpRefs("Remove FooBar");
            });
            schema3.GetInputObject("InputObject").GetField("field1").InputType.GetNamedType().Name.Should()
                .Be("FooBar");

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
            var fieldType = (NonNullType)schema.GetInputObject("Bar").GetField("field").FieldType;
            fieldType.InnerType.Should().Be(foo);
        }

        [Fact]
        public void type_rename_update_type_references()
        {
            var schema = Schema.Create(_ =>
            {
                IEnumerable<TypeIdentity> Ids() => _.GetDefinition().GetTypeIdentities();

                IEnumerable<TypeReference> Refs() => _.GetDefinition().GetTypeReferences();
                // Initial state: two known input object identities, two unknown input identities (from input object fields)
                _.InputObject("InputObject")
                    .Field("field1", "Foo!")
                    .Field("field2", "[Bar]");

                var baz = _.InputObject("Baz");
                Ids().Count().Should().Be(4);
                Ids().Should().ContainSingle(id =>
                    id.Name == "InputObject" && id.Definition is InputObjectTypeDefinition);
                var fooId = Ids().Should().ContainSingle(id => id.Name == "Foo" && id.Definition == null).Subject;
                var barId = Ids().Should().ContainSingle(id => id.Name == "Bar" && id.Definition == null).Subject;
                Ids().Should().ContainSingle(id => id.Name == "Baz" && id.Definition is InputObjectTypeDefinition);

                Refs().Count().Should().Be(2);
                Refs().Should().ContainSingle(tr => ReferenceEquals(tr.Identity, fooId));
                Refs().Should().ContainSingle(tr => ReferenceEquals(tr.Identity, barId));


                // First action: rename known input type to name of unknown type identity
                var bar = baz.Name("Bar");
                Ids().Count().Should().Be(3);
                Ids().Should().ContainSingle(id =>
                    id.Name == "InputObject" && id.Definition is InputObjectTypeDefinition);
                Ids().Should().ContainSingle(id => id.Name == "Bar").Subject.Definition.Should()
                    .BeOfType<InputObjectTypeDefinition>();
                var barId2 = Ids().Should().ContainSingle(id => id.Name == "Bar").Subject.Definition.Should()
                    .BeOfType<InputObjectTypeDefinition>().Subject.Identity;

                ReferenceEquals(barId, barId2).Should().BeFalse();

                Refs().Count().Should().Be(2);
                Refs().Should().ContainSingle(tr => ReferenceEquals(tr.Identity, fooId));
                Refs().Should().ContainSingle(tr => ReferenceEquals(tr.Identity, barId2));

                // Second action: rename input type to name of second unknown type identity
                var foo = bar.Name("Foo");
                Ids().Count().Should().Be(2);
                Ids().Should().ContainSingle(id =>
                    id.Name == "InputObject" && id.Definition is InputObjectTypeDefinition);
                var fooDef = Ids().Should().ContainSingle(id => id.Name == "Foo").Subject.Definition.Should()
                    .BeOfType<InputObjectTypeDefinition>().Subject;


                Refs().Count().Should().Be(2);
                // All type references now point to the same type ref
                foreach (var typeReference in Refs())
                {
                    typeReference.Identity.Name.Should().Be("Foo");
                    ReferenceEquals(typeReference.Identity, fooDef.Identity).Should().BeTrue();
                    ReferenceEquals(typeReference.Identity.Definition, fooDef).Should().BeTrue();
                }


                // Third action: rename common type
                foo.Name("FooBar");
                foreach (var typeReference in Refs())
                {
                    typeReference.Identity.Name.Should().Be("FooBar");
                    ReferenceEquals(typeReference.Identity, fooDef.Identity).Should().BeTrue();
                    ReferenceEquals(typeReference.Identity.Definition, fooDef).Should().BeTrue();
                }

                _.RemoveInputObject("FooBar");
                // All type refs that referenced type are removed
                Refs().Should().BeEmpty();
            });

            schema.GetTypes().Dump().Count().Should().Be(1);
            schema.GetInputObject("InputObject").Fields.Should().BeEmpty();
        }

        [Fact]
        public void unknown_type_identity_created_for_type()
        {
            Schema.Create(_ =>
            {
                _.InputObject("InputObject").Field("field", "Foo");
                var ids = _.GetDefinition().GetTypeIdentities().ToList();
                ids.Count.Should().Be(2);
                var fooId = ids.Should().ContainSingle(id => id.Name == "Foo").Subject;
                fooId.Name.Should().Be("Foo");
                fooId.Definition.Should().Be(null);
            });
        }
    }
}