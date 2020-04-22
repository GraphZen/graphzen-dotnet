// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit
{
    public static class SpecReportGenerator
    {
        public static void Generate()
        {
            var suites = new List<SpecSuite>
            {
                TypeSystemSuite.Create()
            };

            var reportDirectory = Directory.CreateDirectory(@"C:\_data\GraphZen");
            var existing = reportDirectory.GetFiles();

            foreach (var suite in suites)
            {

                using var p = SpecSuiteExcelPackage.Create(suite);
                var date = DateTime.Now.ToString("u").Replace(':', '.');
                var filename = $@"{date} {suite.Name}.xlsx";
                var filePath = Path.Join(reportDirectory.FullName, filename);
                p.SaveAs(new FileInfo(filePath));
            }

            foreach (var file in existing)
            {
                File.Delete(file.FullName);
            }
        }
    }
}