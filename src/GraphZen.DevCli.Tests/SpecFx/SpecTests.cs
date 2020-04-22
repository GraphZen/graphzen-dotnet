using System;
using System.Collections.Generic;
using System.Text;
using GraphZen.SpecAudit;
using GraphZen.SpecAudit.SpecFx;
using Xunit;

namespace GraphZen
{
    public class SpecTests
    {
        [Fact]
        public void can_create_type_system_subject()
        {
            var suite = TypeSystemSuite.Create();
            var package = SpecSuiteExcelPackageBuilder.Create(suite);
        }
    }
}
