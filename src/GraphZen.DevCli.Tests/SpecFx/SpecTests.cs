// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
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
        public void can_create_type_system_subject()
        {
            var suite = TypeSystemSuite.Get();
            suite.Tests.Dump();
            var package = SpecSuiteExcelPackageBuilder.Create(suite);
        }
    }
}