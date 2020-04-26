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
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NullableReferenceTypeProperty))]
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NullableReferenceTypeMethod))]
        [InlineData(typeof(INullableTestClass),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(INullableTestClass),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(INullableTestClass),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullableTestClass.NullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        public void members_should_have_nullable_reference_type(Type type, string memberName)
        {
            var member = type.GetMember(memberName).Single();
            member.HasNullableReferenceType().Should()
                .BeTrue($"{type.Name}.{member.Name} has a nullable reference type");
        }

        [Theory]
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NonNullableReferenceTypeProperty))]
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NonNullableReferenceTypeMethod))]
        [InlineData(typeof(INullableTestClass),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(INullableTestClass),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(INullableTestClass),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NonNullableValueTypeProperty))]
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NullableValueTypeProperty))]
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NullableValueTypeMethod))]
        [InlineData(typeof(INullableTestClass), nameof(INullableTestClass.NonNullableValueTypeMethod))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NonNullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NonNullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassA),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NonNullableValueTypeProperty))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NullableValueTypeProperty))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NullableValueTypeMethod))]
        [InlineData(typeof(NullableTestClassA), nameof(INullableTestClass.NonNullableValueTypeMethod))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NonNullableReferenceTypeProperty))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NonNullableReferenceTypeMethod))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNonNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNullableParameter))]
        [InlineData(typeof(NullableTestClassB),
            nameof(INullableTestClass.NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NonNullableValueTypeProperty))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NullableValueTypeProperty))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NullableValueTypeMethod))]
        [InlineData(typeof(NullableTestClassB), nameof(INullableTestClass.NonNullableValueTypeMethod))]
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
                if (parameterInfo.Name!.Contains("nullableRef"))
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeTrue($"{type}.ctor({parameterInfo.Name}) has a nullable reference type");
                }
                else
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeFalse($"{type}.ctor({parameterInfo.Name}) does not have a nullable reference type");
                }
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
                if (parameterInfo.Name!.Contains("nullableRef"))
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeTrue($"{type}.ctor({parameterInfo.Name}) has a nullable reference type");
                }
                else
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeFalse($"{type}.ctor({parameterInfo.Name}) does not have a nullable reference type");
                }
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
                if (parameterInfo.Name!.Contains("nullableRef"))
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeTrue($"{type}.ctor({parameterInfo.Name}) has a nullable reference type");
                }
                else
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeFalse($"{type}.ctor({parameterInfo.Name}) does not have a nullable reference type");
                }
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
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeFalse(
                            $"{type.Name}.{method.Name}({parameterInfo.Name}) does not have a nullable reference type");
                }
                else
                {
                    parameterInfo.HasNullableReferenceType().Should()
                        .BeTrue($"{type.Name}.{method.Name}({parameterInfo.Name}) has a nullable reference type");
                }
            }
        }
    }
}