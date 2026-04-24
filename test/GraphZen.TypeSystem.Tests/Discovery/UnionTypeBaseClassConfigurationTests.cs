// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
using Xunit;


// ReSharper disable UnusedMember.Local

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassNeverInstantiated.Local

namespace GraphZen.TypeSystem.Tests
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

        [Fact]
        public void union_type_from_class_via_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<non_abstract_class>();
                var unionDef = _.GetDefinition().GetUnion<non_abstract_class>();
                Assert.Equal(ConfigurationSource.Explicit, unionDef.GetConfigurationSource());
                Assert.Equal(2, unionDef.GetMemberTypes().Count());
                Assert.Equal(ConfigurationSource.Convention,
                    _.GetDefinition().GetObject<UnionChildA>().GetConfigurationSource());
                Assert.Equal(ConfigurationSource.Convention,
                    _.GetDefinition().GetObject<UnionChildB>().GetConfigurationSource());
            });
            var union = schema.GetUnion<non_abstract_class>();
            var a = schema.GetObject<UnionChildA>();
            var b = schema.GetObject<UnionChildB>();
            Assert.Equal(a, union.MemberTypesMap[a.Name]);
            Assert.Equal(b, union.MemberTypesMap[b.Name]);
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
            public union_abstract_class union_field { get; set; } = null!;
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
                Assert.Equal(ConfigurationSource.Convention, childADef.GetConfigurationSource());
                var childBDef = _.GetDefinition().GetObject<union_gen_1_b>();
                Assert.Equal(ConfigurationSource.Convention, childBDef.GetConfigurationSource());
                var childCDef = _.GetDefinition().GetObject<union_gen_2_a>();
                Assert.Equal(ConfigurationSource.Convention, childCDef.GetConfigurationSource());
                var childDDef = _.GetDefinition().GetObject<union_gen_2_b>();
                Assert.Equal(ConfigurationSource.Convention, childDDef.GetConfigurationSource());

                Assert.Equal(4, unionADef.GetMemberTypes().Count());
                Assert.Equal(2, unionBDef.GetMemberTypes().Count());
            });

            var unionA = schema.GetUnion<union_abstract_class>();
            var unionB = schema.GetUnion<union_gen_1_c>();
            var childA = schema.GetObject<union_gen_1_a>();
            var childB = schema.GetObject<union_gen_1_b>();
            var childC = schema.GetObject<union_gen_2_a>();
            var childD = schema.GetObject<union_gen_2_b>();

            Assert.Equal(childA, unionA.MemberTypesMap[childA.Name]);
            Assert.Equal(childB, unionA.MemberTypesMap[childB.Name]);
            Assert.Equal(childC, unionA.MemberTypesMap[childC.Name]);
            Assert.Equal(childD, unionA.MemberTypesMap[childD.Name]);

            Assert.Equal(childC, unionB.MemberTypesMap[childC.Name]);
            Assert.Equal(childD, unionB.MemberTypesMap[childD.Name]);
        }
    }
}
