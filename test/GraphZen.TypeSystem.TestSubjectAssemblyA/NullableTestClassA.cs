// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
// ReSharper disable UnusedParameter.Local

namespace GraphZen.TypeSystem.TestSubjectAssemblyA
{
    public class NullableTestClassA : INullableTestClass
    {
        public const string ConstructorWithNullableAndNonNullableParameters = nameof(ConstructorWithNullableAndNonNullableParameters);
        public const string ConstructorWithOnlyNullableParameters = nameof(ConstructorWithOnlyNullableParameters);
        public const string ConstructorWithOnlyNonNullableParameters = nameof(ConstructorWithOnlyNonNullableParameters);

        [Description(ConstructorWithNullableAndNonNullableParameters)]
        public NullableTestClassA(string? nullableRef, string nonNullable, int? nonNullableInt, int nonNullableValueTypeInt)
        {

        }

        [Description(ConstructorWithOnlyNonNullableParameters)]
        public NullableTestClassA(string nonNullable, string nonNullableB, string nonNullableC)
        {

        }


        [Description(ConstructorWithOnlyNullableParameters)]
        public NullableTestClassA(string? nullableRef, int valueType)
        {
        }

        public string? NullableReferenceTypeProperty { get; } = null!;
        public int? NullableValueTypeProperty { get; } = null!;
        public string NonNullableReferenceTypeProperty { get; } = null!;
        public int NonNullableValueTypeProperty { get; } = 0;
        public string? NullableReferenceTypeMethod() => throw new System.NotImplementedException();
        public int? NullableValueTypeMethod() => throw new System.NotImplementedException();

        public int NonNullableValueTypeMethod() => throw new System.NotImplementedException();

        public string? NullableReferenceTypeMethodWithNonNullableParameter(string nonNullable) => throw new System.NotImplementedException();
        public string NonNullableReferenceTypeMethodWithNonNullableParameter(string nonNullable) => throw new System.NotImplementedException();

        public string? NullableReferenceTypeMethodWithNullableParameter(string? nullable) => throw new System.NotImplementedException();
        public string? NullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable, string nonNullable) => throw new System.NotImplementedException();

        public string NonNullableReferenceTypeMethodWithNullableParameter(string? nullable) => throw new System.NotImplementedException();
        public string NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable, string nonNullable) => throw new System.NotImplementedException();

        public string NonNullableReferenceTypeMethod() => throw new System.NotImplementedException();
    }
}