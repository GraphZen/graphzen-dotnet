// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.TestSubjectAssemblyA
{
    public class NullableTestClassA : INullabilityTest
    {
        public const string ConstructorWithNullableAndNonNullableParameters = nameof(ConstructorWithNullableAndNonNullableParameters);
        public const string ConstructorWithOnlyNullableParameters = nameof(ConstructorWithOnlyNullableParameters);
        public const string ConstructorWithOnlyNonNullableParameters = nameof(ConstructorWithOnlyNonNullableParameters);

        [Description(ConstructorWithNullableAndNonNullableParameters)]
        public NullableTestClassA(string? nullable, string nonNullable)
        {

        }

        [Description(ConstructorWithOnlyNonNullableParameters)]
        public NullableTestClassA(string nonNullable, string nonNullableB, string nonNullableC)
        {

        }


        [Description(ConstructorWithOnlyNullableParameters)]
        public NullableTestClassA(string? nullable)
        {
        }

        public string? NullableReferenceTypeProperty { get; } = null!;
        public string NonNullableReferenceTypeProperty { get; } = null!;
        public string? NullableReferenceTypeMethod() => throw new System.NotImplementedException();
        public string? NullableReferenceTypeMethodWithNonNullableParameter(string nonNullable) => throw new System.NotImplementedException();
        public string NonNullableReferenceTypeMethodWithNonNullableParameter(string nonNullable) => throw new System.NotImplementedException();

        public string? NullableReferenceTypeMethodWithNullableParameter(string? nullable) => throw new System.NotImplementedException();
        public string? NullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable, string nonNullable) => throw new System.NotImplementedException();

        public string NonNullableReferenceTypeMethodWithNullableParameter(string? nullable) => throw new System.NotImplementedException();
        public string NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable, string nonNullable) => throw new System.NotImplementedException();

        public string NonNullableReferenceTypeMethod() => throw new System.NotImplementedException();
    }
}