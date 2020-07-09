using System.Diagnostics.CodeAnalysis;
using GraphZen.CodeGen;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
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
