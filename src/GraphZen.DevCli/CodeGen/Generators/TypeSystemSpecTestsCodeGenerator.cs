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
using GraphZen.Internal;
using GraphZen.SpecAudit;
using GraphZen.SpecAudit.SpecFx;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public class TypeSystemSpecTestsCodeGenerator
    {

        public static string GetClassName(Subject subject, Spec parentSpec)
        {
            var spec = parentSpec.Id.TrimEnd("Spec").TrimEnd("Specs") + "Tests";
            if (parentSpec.Id.StartsWith(subject.Name))
            {
                return spec;
            }
            return subject.Name + spec;
        }

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
                            foreach (var (specId, spec) in rootSpec.Children.ToImmutableDictionary(_ => _.Id))
                            {
                                if (subject.Specs.TryGetValue(specId, out var _))
                                {
                                    var isTestImplemented = testFileExists && suite.Tests.Any(t =>
                                        t.SubjectPath == subject.Path && t.SpecId == specId &&
                                        !t.TestMethod.DeclaringType!.Name.Contains("Scaffold"));
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