// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit;
using GraphZen.SpecAudit.SpecFx;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public class TypeSystemSpecTestsCodeGenerator
    {
        public static string GetClassName(Subject subject, Spec parentSpec) =>
            parentSpec.Id.TrimEnd("Spec").TrimEnd("Specs") + "Tests";

        public static IEnumerable<GeneratedCode> ScaffoldSystemSpec()
        {
            var suite = TypeSystemSpecModel.Get();
            foreach (var subject in suite.RootSubject.GetSelfAndDescendants())
            {
                var rootNamespace = "GraphZen.TypeSystem.FunctionalTests";
                var pathBase = $@".\test\{rootNamespace}";

                foreach (var rootSpec in suite.RootSpecs)
                {
                    var path = subject.GetSelfAndAncestors().Select(_ => _.Name).ToArray();
                    var className = GetClassName(subject, rootSpec);
                    var fileName = string.Join("", $"{className}Scaffold.Generated.cs");
                    var fileDir = Path.Combine(pathBase, Path.Combine(path));
                    var filePath = Path.Combine(fileDir, fileName);
                    var ns = string.Join(".", path.Prepend(rootNamespace));
                    var generate = false;
                    var csharp = CSharpStringBuilder.Create();

                    csharp.AppendLine("using Xunit;");
                    csharp.AppendLine("using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;");
                    // csharp.AppendLine("// ReSharper disable PartialTypeWithSinglePart");
                    csharp.AppendLine("// ReSharper disable All");
                    csharp.Namespace(ns, _ =>
                    {
                        var testFileExists = File.Exists(Path.Combine(fileDir, $"{className}.cs"));

                        _.AppendLine("[NoReorder]");
                        _.AbstractClass(testFileExists ? className + "Scaffold" : className, cls =>
                        {

                            // TODO: ordering is off here
                            foreach (var (specId, spec) in rootSpec.Children.Select(c => (c.Id, c)))
                            {
                                if (subject.Specs.TryGetValue(specId, out var _))
                                {


                                    var isTestImplemented = testFileExists && suite.Tests.Any(t =>
                                        t.SubjectPath == subject.Path && t.SpecId == specId &&
                                        !t.TestMethod.DeclaringType!.Name.Contains("Scaffold") && t.TestMethod.DeclaringType!.Namespace!.EndsWith(subject.Path));
                                    if (!isTestImplemented &&
                                        !specId.Contains("deprecated", StringComparison.OrdinalIgnoreCase))
                                    {
                                        generate = true;
                                        var specRef = spec.FieldInfo != null
                                            ? $"nameof({spec.FieldInfo.DeclaringType!.Name}.{spec.FieldInfo.Name})"
                                            : $"\"{spec.Id}\"";
                                        cls.AppendLine($@"
[Spec({specRef})]
[Fact(Skip=""TODO"")]
public void {spec.Id}_() {{
    // var schema = Schema.Create(_ => {{ }});
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

                    if (generate)
                    {
                        yield return new GeneratedCode(filePath, csharp.ToString());
                    }
                }
            }
        }
    }
}