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
                var className = string.Join("", classNameSegments);
                var fileName = string.Join("", $"{className}.Generated.cs").Dump("fileName");
                var filePath = Path.Combine(pathBase, Path.Combine(path), fileName);
                var ns = string.Join(".", path.Prepend(rootNamespace));


                var csharp = CSharpStringBuilder.Create();
                csharp.Namespace(ns, _ => { _.PartialClass(className, cls => { }); });


                var contents = $"/* {csharp} */";

                yield return new GeneratedCode(filePath, contents);
            }
        }
    }
}