using System;
using System.Collections.Generic;
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
                // var path = @"\test\GraphZen.TypeSystem.FunctionalTests";
            }


            yield break;
        }
    }
}
