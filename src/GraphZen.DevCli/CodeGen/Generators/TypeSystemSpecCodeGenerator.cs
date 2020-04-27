using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.SpecAudit;

namespace GraphZen.CodeGen.Generators
{
    public class TypeSystemSpecCodeGenerator
    {
        public static IEnumerable<GeneratedCode> ScaffoldSystemSpec()
        {
            var suite = TypeSystemSuite.Get();
            foreach (var subject in suite.RootSubject.GetSelfAndDescendants())
            {

                var ns = "GraphZen.TypeSystem.FunctionalTests";
                var pathBase = $@".\test\{ns}";
                var path = subject.GetSelfAndDescendants().Select(_ => _.Name);



                yield return new GeneratedCode(pathBase, "// TBD");
            }


            yield break;
        }
    }
}
