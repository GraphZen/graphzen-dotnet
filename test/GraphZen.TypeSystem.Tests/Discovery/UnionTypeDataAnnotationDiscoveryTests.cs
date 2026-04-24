// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.TypeSystem.Internal;


// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace GraphZen.TypeSystem.Tests;

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
            Assert.NotNull(unionDef);
            Assert.Equal(ConfigurationSource.Explicit, unionDef.GetConfigurationSource());
            Assert.Equal(3, unionDef.GetMemberTypes().Count());
            Assert.NotNull(_.GetDefinition().FindObject<explicitly_created_union_type_member_a>());
            Assert.NotNull(_.GetDefinition().FindObject<explicitly_created_union_type_conventional_member_b>());
        });

        var union = schema.FindUnion<explicitly_created_union_type>();
        Assert.NotNull(union);
        var a = schema.GetObject<explicitly_created_union_type_member_a>();
        var b = schema.GetObject<explicitly_created_union_type_conventional_member_b>();
        Assert.Equal(a, union.MemberTypesMap[a.Name]);
        Assert.Equal(b, union.MemberTypesMap[b.Name]);
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
            Assert.Equal(ConfigurationSource.DataAnnotation, unionDef.GetConfigurationSource());
            Assert.Equal(2, unionDef.GetMemberTypes().Count());
            Assert.Equal(ConfigurationSource.Explicit,
                _.GetDefinition().GetObject<explicitly_created_object>().GetConfigurationSource());
            Assert.Equal(ConfigurationSource.Convention,
                _.GetDefinition().GetObject<sibling_union_member>().GetConfigurationSource());
        });
        var union = schema.GetUnion<union_discovered_by_data_annotation_via_interface>();
        var memberA = schema.GetObject<explicitly_created_object>();
        var memberB = schema.GetObject<sibling_union_member>();
        Assert.Equal(memberA, union.MemberTypesMap[memberA.Name]);
        Assert.Equal(memberB, union.MemberTypesMap[memberB.Name]);
    }

    public class explicitly_created_object_with_union_field
    {
        public union_discovered_via_field union_field { get; set; } = null!;
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
            Assert.Equal(ConfigurationSource.DataAnnotation, unionDef.GetConfigurationSource());
            Assert.Equal(2, unionDef.GetMemberTypes().Count());
            Assert.Equal(ConfigurationSource.Convention,
                _.GetDefinition().GetObject<union_member_a>().GetConfigurationSource());
            Assert.Equal(ConfigurationSource.Convention,
                _.GetDefinition().GetObject<union_member_b>().GetConfigurationSource());
        });
        var union = schema.GetUnion<union_discovered_via_field>();
        var memberA = schema.GetObject<union_member_a>();
        var memberB = schema.GetObject<union_member_b>();
        Assert.Equal(memberA, union.MemberTypesMap[memberA.Name]);
        Assert.Equal(memberB, union.MemberTypesMap[memberB.Name]);
    }
}
