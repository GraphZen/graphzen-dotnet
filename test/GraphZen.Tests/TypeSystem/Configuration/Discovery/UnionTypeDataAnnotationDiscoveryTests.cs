// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace GraphZen.TypeSystem
{
    [NoReorder]
    public class UnionTypeDataAnnotationDiscoveryTests
    {
        public interface explicitly_created_union_type
        {
        }

        public class explicitly_created_union_type_member_a : explicitly_created_union_type
        {
        }

        public class explicitly_created_union_type_conventional_member_b : explicitly_created_union_type
        {
        }

        public class explicitly_created_union_type_explicit_member_c
        {
        }

        [GraphQLIgnore]
        public class explicitly_created_union_type_conventional_member_d : explicitly_created_union_type
        {
        }

        [Fact]
        public void union_type_created_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<explicitly_created_union_type>().OfTypes<explicitly_created_union_type_explicit_member_c>();
                var unionDef = _.GetDefinition().FindUnion<explicitly_created_union_type>();
                unionDef.Should().NotBeNull();
                unionDef.GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                unionDef.MemberTypes.Count.Should().Be(3);
                _.GetDefinition().FindObject<explicitly_created_union_type_member_a>().Should().NotBeNull();
                _.GetDefinition().FindObject<explicitly_created_union_type_conventional_member_b>().Should()
                    .NotBeNull();
            });

            var union = schema.FindUnion<explicitly_created_union_type>();
            union.Should().NotBeNull();
            var a = schema.GetObject<explicitly_created_union_type_member_a>();
            var b = schema.GetObject<explicitly_created_union_type_conventional_member_b>();
            union.MemberTypes[a.Name].Should().Be(a);
            union.MemberTypes[b.Name].Should().Be(b);
        }


        public class explicitly_created_object : union_discovered_by_data_annotation_via_interface
        {
        }

        public class sibling_union_member : union_discovered_by_data_annotation_via_interface
        {
        }

        [GraphQLUnion]
        public interface union_discovered_by_data_annotation_via_interface
        {
        }

        [Fact]
        public void union_discovered_by_data_annotation_on_object_interface()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<explicitly_created_object>();
                var unionDef = _.GetDefinition().GetUnion<union_discovered_by_data_annotation_via_interface>();
                unionDef.GetConfigurationSource().Should().Be(ConfigurationSource.DataAnnotation);
                unionDef.MemberTypes.Count.Should().Be(2);
                _.GetDefinition().GetObject<explicitly_created_object>().GetConfigurationSource().Should()
                    .Be(ConfigurationSource.Explicit);
                _.GetDefinition().GetObject<sibling_union_member>().GetConfigurationSource().Should()
                    .Be(ConfigurationSource.Convention);
            });
            var union = schema.GetUnion<union_discovered_by_data_annotation_via_interface>();
            var memberA = schema.GetObject<explicitly_created_object>();
            var memberB = schema.GetObject<sibling_union_member>();
            union.MemberTypes[memberA.Name].Should().Be(memberA);
            union.MemberTypes[memberB.Name].Should().Be(memberB);
        }

        public class explicitly_created_object_with_union_field
        {
            public union_discovered_via_field union_field { get; set; }
        }

        [GraphQLUnion]
        public interface union_discovered_via_field
        {
        }

        public class union_member_a : union_discovered_via_field
        {
        }

        public class union_member_b : union_discovered_via_field
        {
        }

        [Fact]
        public void union_discovered_by_data_annotation_on_object_field_type()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<explicitly_created_object_with_union_field>();
                var unionDef = _.GetDefinition().GetUnion<union_discovered_via_field>();
                unionDef.GetConfigurationSource().Should().Be(ConfigurationSource.DataAnnotation);
                unionDef.MemberTypes.Count.Should().Be(2);
                _.GetDefinition().GetObject<union_member_a>().GetConfigurationSource().Should()
                    .Be(ConfigurationSource.Convention);
                _.GetDefinition().GetObject<union_member_b>().GetConfigurationSource().Should()
                    .Be(ConfigurationSource.Convention);
            });
            var union = schema.GetUnion<union_discovered_via_field>();
            var memberA = schema.GetObject<union_member_a>();
            var memberB = schema.GetObject<union_member_b>();
            union.MemberTypes[memberA.Name].Should().Be(memberA);
            union.MemberTypes[memberB.Name].Should().Be(memberB);
        }
    }
}