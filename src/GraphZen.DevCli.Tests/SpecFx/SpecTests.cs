// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
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
            suite.Tests.Dump();
            var package = SpecSuiteExcelPackageBuilder.Create(suite);
             throw new Exception();
        }
    }
}