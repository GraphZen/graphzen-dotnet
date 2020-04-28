// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public class TypeSystemSpecCodeGenerator
    {
        public static IEnumerable<GeneratedCode> ScaffoldSystemSpec()
        {
            var suite = TypeSystemSuite.Get();
            foreach (var subject in suite.RootSubject.GetSelfAndDescendants())
            {
                var rootNamespace = "GraphZen.TypeSystem.FunctionalTests";
                var pathBase = $@".\test\{rootNamespace}";

                var path = subject.GetSelfAndAncestors().Select(_ => _.Name).ToArray();
                var classNameSegments = path.Length == 1 ? path : path[^2..];
                var className = string.Join("", classNameSegments) + "Tests";
                var fileName = string.Join("", $"{className}Scaffold.Generated.cs");
                var fileDir = Path.Combine(pathBase, Path.Combine(path));
                var filePath = Path.Combine(fileDir, fileName);
                var ns = string.Join(".", path.Prepend(rootNamespace));
                var generate = false;
                var csharp = CSharpStringBuilder.Create();

                csharp.AppendLine("using Xunit;");
                csharp.AppendLine("using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;");
                // csharp.AppendLine("// ReSharper disable PartialTypeWithSinglePart");
                csharp.Namespace(ns, _ =>
                {
                    var testFileExists = File.Exists(Path.Combine(fileDir, $"{className}.cs"));

                    _.AppendLine("[NoReorder]");
                    _.AbstractClass(testFileExists ? className + "Scaffold" : className, cls =>
                    {
                        foreach (var (specId, subjectSpec) in subject.Specs.OrderBy(_ => _.Key))
                        {
                            if (suite.Specs.TryGetValue(specId, out var spec))
                            {
                                var isTestImplemented = testFileExists && suite.Tests.Any(_ =>
                                                                _.SubjectPath == subject.Path && _.SpecId == specId &&
                                                                !_.TestMethod.DeclaringType!.Name.Contains("Scaffold"));
                                if (!isTestImplemented)
                                {
                                    generate = true;
                                    var specRef = spec.FieldInfo != null
                                        ? $"nameof({spec.FieldInfo.DeclaringType!.Name}.{spec.FieldInfo.Name})"
                                        : $"\"{spec.Id}\"";
                                    cls.AppendLine($@"
[Spec({specRef})]
[Fact]
public void {spec.Id}() {{
    // Priority: {subjectSpec.Priority}
    var schema = Schema.Create(_ => {{

    }});
    throw new NotImplementedException();
}}

");
                                }
                            }
                        }
                    });

                    if (!testFileExists)
                    {
                        _.AppendLine($"// Move {className} into a separate file to start writing tests");
                        _.AppendLine("[NoReorder] ");
                        _.Class(className + "Scaffold",
                            cls => { });
                    }
                });

                //var contents = $"/* {csharp} */";
                //yield return new GeneratedCode(filePath, contents);
                if (generate)
                {
                    yield return new GeneratedCode(filePath, csharp.ToString());
                }
            }
        }
    }
}