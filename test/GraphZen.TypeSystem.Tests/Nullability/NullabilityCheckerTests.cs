// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.TestSubjectAssemblyA;
using GraphZen.TypeSystem.TestSubjectAssemblyB;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests.Nullability
{
    public class NullabilityCheckerTests
    {
        [Theory]
        [InlineData(typeof(INullabilityTest), nameof(INullabilityTest.NullableReferenceTypeProperty))]
        [InlineData(typeof(INullabilityTest), nameof(INullabilityTest.NullableReferenceTypeMethod))]
        [InlineData(typeof(INullabilityTest),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(INullabilityTest),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(INullabilityTest),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassA), nameof(INullabilityTest.NullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassA), nameof(INullabilityTest.NullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassB), nameof(INullabilityTest.NullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassB), nameof(INullabilityTest.NullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullabilityTest.NullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        public void members_should_have_nullable_reference_type(Type type, string memberName)
        {
            var member = type.GetMember(memberName).Single();
            member.HasNullableReferenceType().Should()
                .BeTrue($"{type.Name}.{member.Name} has a nullable reference type");
        }

        [Theory]
        [InlineData(typeof(INullabilityTest), nameof(INullabilityTest.NonNullableReferenceTypeProperty))]
        [InlineData(typeof(INullabilityTest), nameof(INullabilityTest.NonNullableReferenceTypeMethod))]
        [InlineData(typeof(INullabilityTest),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(INullabilityTest),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(INullabilityTest),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassA), nameof(INullabilityTest.NonNullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassA), nameof(INullabilityTest.NonNullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassB), nameof(INullabilityTest.NonNullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassB), nameof(INullabilityTest.NonNullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullabilityTest.NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        public void members_should_not_have_nullable_reference_type(Type type, string memberName)
        {
            var member = type.GetMember(memberName).Single();
            member.HasNullableReferenceType().Should()
                .BeFalse($"{type.Name}.{member.Name} does not have a nullable reference type");
        }

        [Fact]
        public void ctor_with_only_non_nullable_parameters_should_not_have_nullable_ref_types()
        {
            var type = typeof(NullableTestClassA);
            var ctor = type.GetConstructors().Single(_ =>
                _.GetCustomAttribute<DescriptionAttribute>()?.Description ==
                NullableTestClassA.ConstructorWithOnlyNonNullableParameters);

            var ctorParams = ctor.GetParameters();
            ctorParams.Should().NotBeEmpty();
            foreach (var parameterInfo in ctorParams)
            {
                parameterInfo.HasNullableReferenceType().Should()
                    .BeFalse($"{type}.ctor({parameterInfo.Name}) does not have a nullable reference type");
            }
        }

        [Fact]
        public void ctor_with_only_nullable_parameters_should_have_nullable_ref_types()
        {
            var type = typeof(NullableTestClassA);
            var ctor = type.GetConstructors().Single(_ =>
                _.GetCustomAttribute<DescriptionAttribute>()?.Description ==
                NullableTestClassA.ConstructorWithOnlyNullableParameters);

            var ctorParams = ctor.GetParameters();
            ctorParams.Should().NotBeEmpty();
            foreach (var parameterInfo in ctorParams)
            {
                parameterInfo.HasNullableReferenceType().Should()
                    .BeTrue($"{type}.ctor({parameterInfo.Name}) has a nullable reference type");
            }
        }

        [Fact]
        public void ctor_with_nullable_and_non_nullable_parameters_should_have_expected_nullable_ref_types()
        {
            var type = typeof(NullableTestClassA);
            var ctor = type.GetConstructors().Single(_ =>
                _.GetCustomAttribute<DescriptionAttribute>()?.Description ==
                NullableTestClassA.ConstructorWithNullableAndNonNullableParameters);

            var ctorParams = ctor.GetParameters();
            ctorParams.Should().NotBeEmpty();
            foreach (var parameterInfo in ctorParams)
            {
                if (parameterInfo.Name!.Contains("nonNullable"))
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeFalse($"{type}.ctor({parameterInfo.Name}) does not have a nullable reference type");
                else
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeTrue($"{type}.ctor({parameterInfo.Name}) has a nullable reference type");
            }
        }


        [Theory]
        [InlineData(typeof(NullableTestClassA),
            nameof(NullableTestClassA.NonNullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(NullableTestClassA.NullableReferenceTypeMethodWithNonNullableParameter))]
        public void method_with_only_non_nullable_parameters_should_not_have_nullable_ref_types(Type type,
            string methodName)
        {
            var method = type.GetMethod(methodName)!;
            var ctorParams = method.GetParameters();
            ctorParams.Should().NotBeEmpty();
            foreach (var parameterInfo in ctorParams)
            {
                parameterInfo.HasNullableReferenceType().Should()
                    .BeFalse(
                        $"{type.Name}.{method.Name}({parameterInfo.Name}) does not have a nullable reference type");
            }
        }

        [Theory]
        [InlineData(typeof(NullableTestClassA),
            nameof(NullableTestClassA.NonNullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(NullableTestClassA.NullableReferenceTypeMethodWithNullableParameter))]
        public void method_with_only_nullable_parameters_should_have_nullable_ref_types(Type type, string methodName)
        {
            var method = type.GetMethod(methodName)!;

            var ctorParams = method.GetParameters();
            ctorParams.Should().NotBeEmpty();
            foreach (var parameterInfo in ctorParams)
            {
                parameterInfo.HasNullableReferenceType().Should()
                    .BeTrue($"{type.Name}.{method.Name}({parameterInfo.Name}) has a nullable reference type");
            }
        }

        [Theory]
        [InlineData(typeof(NullableTestClassA),
            nameof(NullableTestClassA.NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassA),
            nameof(NullableTestClassA.NullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        public void method_with_nullable_and_non_nullable_parameters_should_have_expected_nullable_ref_types(Type type,
            string methodName)
        {
            var method = type.GetMethod(methodName)!;
            var ctorParams = method.GetParameters();
            ctorParams.Should().NotBeEmpty();
            foreach (var parameterInfo in ctorParams)
            {
                if (parameterInfo.Name!.Contains("nonNullable"))
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeFalse($"{type.Name}.{method.Name}({parameterInfo.Name}) does not have a nullable reference type");
                else
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeTrue($"{type.Name}.{method.Name}({parameterInfo.Name}) has a nullable reference type");
            }
        }
    }
}