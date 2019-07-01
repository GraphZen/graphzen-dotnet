// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Types.Builders;
using GraphZen.Types.Internal;
using JetBrains.Annotations;
using Xunit;

// ReSharper disable UnusedMember.Local

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassNeverInstantiated.Local

namespace GraphZen.Types
{
    [NoReorder]
    public class UnionTypeBaseClassConfigurationTests
    {
        private class non_abstract_class
        {
        }

        private class UnionChildA : non_abstract_class
        {
        }

        private class UnionChildB : non_abstract_class
        {
        }

        private interface interface_without_union_data_annotation
        {
        }

        [Fact]
        public void union_type_from_class_via_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<non_abstract_class>();
                var unionDef = _.GetDefinition().GetUnion<non_abstract_class>();
                unionDef.GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                unionDef.MemberTypes.Count.Should().Be(2);
                _.GetDefinition().GetObject<UnionChildA>().GetConfigurationSource().Should()
                    .Be(ConfigurationSource.Convention);
                _.GetDefinition().GetObject<UnionChildB>().GetConfigurationSource().Should()
                    .Be(ConfigurationSource.Convention);
            });
            var union = schema.GetUnion<non_abstract_class>();
            var a = schema.GetObject<UnionChildA>();
            var b = schema.GetObject<UnionChildB>();
            union.MemberTypes[a.Name].Should().Be(a);
            union.MemberTypes[b.Name].Should().Be(b);
        }

        abstract class union_abstract_class
        {
        }

        private class union_gen_1_a : union_abstract_class
        {
        }

        private class union_gen_1_b : union_abstract_class
        {
        }

        abstract class union_gen_1_c : union_abstract_class
        {
        }

        private class union_gen_2_a : union_gen_1_c
        {
        }

        private class union_gen_2_b : union_gen_1_c
        {
        }

        private class Query
        {
            public union_abstract_class union_field { get; set; }
        }

        [Fact]
        public void union_type_from_abstract_class_convention()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<Query>();
                var unionADef = _.GetDefinition().GetUnion<union_abstract_class>();
                var unionBDef = _.GetDefinition().GetUnion<union_gen_1_c>();
                var childADef = _.GetDefinition().GetObject<union_gen_1_a>();
                childADef.GetConfigurationSource().Should().Be(ConfigurationSource.Convention);
                var childBDef = _.GetDefinition().GetObject<union_gen_1_b>();
                childBDef.GetConfigurationSource().Should().Be(ConfigurationSource.Convention);
                var childCDef = _.GetDefinition().GetObject<union_gen_2_a>();
                childCDef.GetConfigurationSource().Should().Be(ConfigurationSource.Convention);
                var childDDef = _.GetDefinition().GetObject<union_gen_2_b>();
                childDDef.GetConfigurationSource().Should().Be(ConfigurationSource.Convention);

                unionADef.MemberTypes.Count.Should().Be(4);
                unionBDef.MemberTypes.Count.Should().Be(2);
            });

            var unionA = schema.GetUnion<union_abstract_class>();
            var unionB = schema.GetUnion<union_gen_1_c>();
            var childA = schema.GetObject<union_gen_1_a>();
            var childB = schema.GetObject<union_gen_1_b>();
            var childC = schema.GetObject<union_gen_2_a>();
            var childD = schema.GetObject<union_gen_2_b>();

            unionA.MemberTypes[childA.Name].Should().Be(childA);
            unionA.MemberTypes[childB.Name].Should().Be(childB);
            unionA.MemberTypes[childC.Name].Should().Be(childC);
            unionA.MemberTypes[childD.Name].Should().Be(childD);

            unionB.MemberTypes[childC.Name].Should().Be(childC);
            unionB.MemberTypes[childD.Name].Should().Be(childD);
        }
    }
}