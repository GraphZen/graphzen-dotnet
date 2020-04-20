// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace GraphZen.SpecAudit.SpecFx
{
    public class SpecSuite
    {
        public SpecSuite(string name, SpecSubject rootSubject, IEnumerable<Spec> specs, Assembly testAssembly)
        {
            Name = name;
            Specs = specs.ToReadOnlyList();
            RootSubject = rootSubject;
            TestAssembly = testAssembly;
            Subjects = rootSubject.GetSelfAndDescendants().ToImmutableList();
            SubjectsByPath = Subjects.ToImmutableDictionary(_ => _.Path);
            Tests = SpecTest.DiscoverFrom(TestAssembly).ToImmutableList();
        }

        public string Name { get; }
        public IReadOnlyList<Spec> Specs { get; }
        public IReadOnlyList<SpecTest> Tests { get; }
        public IReadOnlyList<SpecSubject> Subjects { get; }
        public IReadOnlyDictionary<string, SpecSubject> SubjectsByPath { get; }
        public SpecSubject RootSubject { get; }
        public Assembly TestAssembly { get; }

        public ExcelPackage CreateReport()
        {
            var p = new ExcelPackage();
            CreateTestMatrix(p);
            ReportUnknownTests(p);
            return p;
        }

        private void ReportUnknownTests(ExcelPackage p)
        {
            var modelWs = p.Workbook.Worksheets.Add("Model");
            var pathHeader = modelWs.Cells[1, 1];
            pathHeader.Value = "Path";
            for (int i = 0; i < Subjects.Count; i++)
            {
                var subj = Subjects[i];
                var rowNumber = i + 2;
                var pathCell = modelWs.Cells[rowNumber, 1];
                pathCell.Value = subj.Path;
            }

            modelWs.Cells.AutoFitColumns();

            var testWs = p.Workbook.Worksheets.Add("Tests");
            var classCol = 1;
            var classHeader = testWs.Cells[1, classCol];
            classHeader.Value = "Class";
            var methodCol = 2;
            var methodHeader = testWs.Cells[1, methodCol];
            methodHeader.Value = "Method";
            var testPathCol = 3;
            var testPathHeader = testWs.Cells[1, testPathCol];
            testPathHeader.Value = "Path";
            var specCol = 4;
            var specHeader = testWs.Cells[1, specCol];
            specHeader.Value = "Spec";
            var subjectInModelCol = 5;
            var subjectInModelHeader = testWs.Cells[1, subjectInModelCol];
            subjectInModelHeader.Value = "Model Subject";
            var specInModelCol = 6;
            var specModelHeader = testWs.Cells[1, specInModelCol];
            specModelHeader.Value = "Model Spec";

            for (int i = 0; i < Tests.Count; i++)
            {
                var testInfo = Tests[i];
                var row = i + 2;
                var classCell = testWs.Cells[row, classCol];
                classCell.Value = testInfo.TestMethod.DeclaringType?.Name;
                var methodCell = testWs.Cells[row, methodCol];
                methodCell.Value = testInfo.TestMethod.Name;
                var testPathCell = testWs.Cells[row, testPathCol];
                testPathCell.Value = testInfo.SubjectPath;
                var specCell = testWs.Cells[row, specCol];
                specCell.Value = testInfo.SpecId;
                var modelSubject = SubjectsByPath.TryGetValue(testInfo.SubjectPath, out var subj) ? subj : null;
                var subjectInModelCell = testWs.Cells[row, subjectInModelCol];
                var subjectInModel = modelSubject != null;
                subjectInModelCell.Value = subjectInModel ? "✔" : "❌";
                if (!subjectInModel) subjectInModelCell.Style.Font.Color.SetColor(Color.Crimson);
                var specInModelCell = testWs.Cells[row, specInModelCol];
                var specInModel = modelSubject != null && modelSubject.Specs.Contains(testInfo.SpecId);
                specInModelCell.Value = specInModel ? "✔" : "❌";
                if (!specInModel) specInModelCell.Style.Font.Color.SetColor(Color.Crimson);

                if (!specInModel || !subjectInModel)
                    testWs.Row(row).Style.Border.BorderAround(ExcelBorderStyle.MediumDashDot);
            }

            testWs.Cells.AutoFitColumns();
        }

        private void CreateTestMatrix(ExcelPackage p)
        {
            var worksheet = p.Workbook.Worksheets.Add("Coverage");
            var specRows = new Dictionary<string, int>();
            var currentRow = 3;
            var specRowStart = currentRow;
            foreach (var spec in Specs)
            {
                worksheet.Cells[specRowStart, 1].Value = spec.Name;
                for (int i = 0; i < spec.Children.Count; i++)
                {
                    var child = spec.Children[i];
                    currentRow = specRowStart + i;
                    var childRow = worksheet.Cells[currentRow, 2];
                    childRow.Value = child.Name;
                    specRows[child.Id] = currentRow;
                }

                var specHeader = worksheet.Cells[specRowStart, 1, currentRow, 1];
                specHeader.Merge = true;
                specRowStart = currentRow + 1;
            }

            var currentColumn = 3;
            var columnStart = currentColumn;
            foreach (var subj in GetPrimarySubjects())
            {
                subj.Name.Dump();
                worksheet.Cells[1, currentColumn].Value = subj.Name;
                for (int i = 0; i < subj.Children.Count; i++)
                {
                    var child = subj.Children[i];
                    currentColumn = columnStart + i;
                    var childHeader = worksheet.Cells[2, currentColumn];
                    childHeader.Value = child.Name;
                    childHeader.Style.TextRotation = 45;
                    worksheet.Column(currentColumn).Width = 6;
                    foreach (var (specId, row) in specRows)
                    {
                        var statusCell = worksheet.Cells[row, currentColumn];
                        if (child.Specs.Contains(specId))
                        {
                            var tests = Tests.Where(_ => _.SpecId == specId && _.SubjectPath == child.Path)
                                .ToImmutableList();
                            var nonSkippedTests = tests.Where(_ => _.SkipReason == null).ToImmutableList();
                            var skippedTests = tests.Where(_ => _.SkipReason == null).ToImmutableList();
                            if (nonSkippedTests.Any())
                            {
                                statusCell.Value = "✅";
                            }
                            else if (skippedTests.Any())
                            {

                                statusCell.Value = "❓";
                            }
                            else
                            {
                                statusCell.Value = "❎";
                            }
                        }
                        else
                        {
                            statusCell.Value = "-";
                        }

                        statusCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                }

                var subjectHeader = worksheet.Cells[1, columnStart, 1, currentColumn];
                subjectHeader.Value = subj.Name;
                subjectHeader.Merge = true;
                subjectHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                columnStart = currentColumn + 1;
            }
        }

        private IReadOnlyList<SpecSubject> GetPrimarySubjects()
        {
            void Add(SpecSubject subj, List<SpecSubject> list)
            {
                list.Add(subj);
                foreach (var grandChild in subj.Children.SelectMany(c => c.Children))
                {
                    Add(grandChild, list);
                }
            }

            var subjects = new List<SpecSubject>();
            Add(RootSubject, subjects);
            return subjects.AsReadOnly();
        }
    }
}