// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace GraphZen.SpecAudit.SpecFx
{
    public class SpecSuite
    {
        public SpecSuite(string name, SpecSubject subject, IEnumerable<Spec> specs)
        {
            Name = name;
            Specs = specs.ToReadOnlyList();
            Subject = subject;
        }

        public string Name { get; }
        public IReadOnlyList<Spec> Specs { get; }
        public SpecSubject Subject { get; }

        public ExcelPackage CreateReport()
        {
            var p = new ExcelPackage();

            //A workbook must have at least on cell, so lets add one... 
            var worksheet = p.Workbook.Worksheets.Add("MySheet");
            //To set values in the spreadsheet use the Cells indexer.

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
                    foreach (var (id, row) in specRows)
                    {
                        var statusCell = worksheet.Cells[row, currentColumn];
                        statusCell.Value = "❌";
                    }
                }

                var subjectHeader = worksheet.Cells[1, columnStart, 1, currentColumn];
                subjectHeader.Value = subj.Name;
                subjectHeader.Merge = true;
                subjectHeader.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                columnStart = currentColumn + 1;
            }

            return p;
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
            Add(Subject, subjects);
            return subjects.AsReadOnly();
        }
    }
}