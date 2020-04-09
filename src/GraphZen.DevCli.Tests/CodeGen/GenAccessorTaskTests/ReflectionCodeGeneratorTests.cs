// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.CodeGen.ReflectionCodeGenerator;

namespace GraphZen.CodeGen.GenAccessorTaskTests
{
    [NoReorder]
    public class ReflectionCodeGeneratorTests
    {
        [Fact]
        public void GetCodeGenSourceTypes_should_return_types_with_members_annotated_for_codegen()
        {
            var result = GenAccessorsTask.FromTypes(GetSourceTypes<ReflectionCodeGeneratorTests>());
            result.Should().HaveCount(2);
            result.Should().Contain(_ => _.TargetType == typeof(InterfaceWithAnnotatedMember));
            result.Should().Contain(_ => _.TargetType == typeof(ClassWithAnnotatedMember));
        }
    }
}