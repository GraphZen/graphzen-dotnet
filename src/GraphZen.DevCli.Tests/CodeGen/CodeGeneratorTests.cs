using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GraphZen.CodeGen
{
    public class CodeGeneratorTests
    {
        [Fact]
        public void TestCodeGen()
        {
            var test = CodeGenerator.GetGeneratedCode();
        }

    }
}
