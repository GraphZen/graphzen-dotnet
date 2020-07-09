// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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
            CodeGenerator.GetGeneratedCode();
        }
    }
}