// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit;
using GraphZen.SpecAudit.SpecFx;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
{
    public class SpecTests
    {
        [Fact]
        public void can_create_type_system_subject()
        {
            var suite = TypeSystemSuite.Get();


            suite.Subjects
                .Where(_ => _.Path == "Schema.DirectiveAnnotations.DirectiveAnnotation.Arguments.Argument.Name")
                .Select(_ =>
                {
                    var path = _.GetSelfAndAncestors().Select(_ => _.Name).ToArray();
                    var fileNameSegments = path.Length == 1 ? path : path[^2..];
                    var fileName = string.Join("", fileNameSegments.Append("Tests.cs")).Dump("fileName");
                    var filePath = Path.Combine(path).Dump("filePath");
                    var @namespace = string.Join(".", path).Dump("namespace");

                    path.Dump("test");
                    return _.Path;
                }).Dump();

            var package = SpecSuiteExcelPackageBuilder.Create(suite);
            // throw new Exception();
        }
    }
}