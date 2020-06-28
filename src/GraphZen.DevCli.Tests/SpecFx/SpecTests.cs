// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.CodeGen.Generators;
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

        [Fact]
        public void tests_should_be_in_the_right_classes()
        {
            var suite = TypeSystemSpecModel.Get();
            foreach (var _ in suite.Tests)
            {
                var spec = suite.Specs[_.SpecId];
                if (!suite.SubjectsByPath.ContainsKey(_.SubjectPath))
                {
                    throw new Exception($"Unable to find {_.SubjectPath}");
                }

                var subject = suite.SubjectsByPath[_.SubjectPath];
                var parent = spec.Parent;
                if (parent != null)
                {
                    var m = _.TestMethod;
                    var mClass = m.DeclaringType!;
                    var expectedClassName = TypeSystemSpecTestsCodeGenerator.GetClassName(subject, parent);
                    if (mClass.Name != expectedClassName)
                    {
                        var message = @$"

Class '{mClass.Name}' incorrectly contains test '{_.SpecId}'

{mClass.FullName!.TrimStart("GraphZen.TypeSystem.FunctionalTests.")} 

(expected class name: {expectedClassName})





".Dump();
                        throw new Exception(message);
                    }
                }
            }
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
                    var expectedMethodName = _.SpecId + "schemaBuilder";
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