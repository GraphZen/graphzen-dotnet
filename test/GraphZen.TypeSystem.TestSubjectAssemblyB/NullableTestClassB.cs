// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.TestSubjectAssemblyB
{
    public class NullableTestClassB : INullabilityTest
    {
        public NullableTestClassB(string? nullableReferenceTypeProperty, string nonNullableReferenceTypeProperty)
        {
            NullableReferenceTypeProperty = nullableReferenceTypeProperty;
            NonNullableReferenceTypeProperty = nonNullableReferenceTypeProperty;
        }

        public string? NullableReferenceTypeProperty { get; }
        public string NonNullableReferenceTypeProperty { get; }
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