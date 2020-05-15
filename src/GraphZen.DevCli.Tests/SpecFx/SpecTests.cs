// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit;
using GraphZen.SpecAudit.SpecFx;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.SpecFx
{
    public class SpecTests
    {
        [Fact]
        public void can_create_type_system_excel_package()
        {
            var suite = TypeSystemSpecModel.Get();
            var package = SpecSuiteExcelPackageBuilder.Create(suite);
            package.Should().NotBeNull();
        }

        [Fact(Skip = "ignoring")]
        public void tests_should_be_in_the_right_classes()
        {
            var suite = TypeSystemSpecModel.Get();
            suite.Tests.Select(_ =>
            {
                var spec = suite.Specs[_.SpecId];
                var parentId = spec.Parent?.Id;
                if (parentId != null)
                {
                    var m = _.TestMethod;
                    var mClass = m.DeclaringType!.Name;
                    var expectedClassName = $"{parentId}Tests";
                    if (mClass != parentId)
                    {
                        return $"{mClass}.{m.Name} should be in class '{expectedClassName}'";
                    }

                }
                return null;
            }).FirstOrDefault(_ => _ != null).Should().BeNull();
        }

        [Fact(Skip = "ignoring")]
        public void tests_should_have_the_right_name()
        {
            var suite = TypeSystemSpecModel.Get();
            suite.Tests.Select(_ =>
            {
                var spec = suite.Specs[_.SpecId];
                var parentId = spec.Parent?.Id;
                if (parentId != null)
                {
                    var m = _.TestMethod;
                    var mClass = m.DeclaringType!.Name;
                    var expectedMethodName = _.SpecId + "_";
                    if (m.Name != expectedMethodName)
                    {
                        return $"{mClass}.{m.Name} should be named \"{expectedMethodName}\"";
                    }
                }
                return null;
            }).Where(_ => _ != null).Take(1).Dump().Count().Should().Be(0);
        }
    }
}