// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class TypeKindExtensions
    {
        public static bool IsInputType(this TypeKind kind)
        {
            switch (kind)
            {
                case TypeKind.Enum:
                case TypeKind.InputObject:
                case TypeKind.Scalar:
                    return true;
                case TypeKind.Object:
                case TypeKind.Interface:
                case TypeKind.Union:
                    return false;
                default:
                    throw new InvalidOperationException($"Cannot infer if input type from type kind {kind}.");
            }
        }

        public static bool IsOutputType(this TypeKind kind)
        {
            switch (kind)
            {
                case TypeKind.Scalar:
                case TypeKind.Object:
                case TypeKind.Interface:
                case TypeKind.Union:
                case TypeKind.Enum:
                    return true;
                case TypeKind.InputObject:
                    return false;
                default:
                    throw new InvalidOperationException($"Cannot infer if output type from type kind {kind}.");
            }
        }
    }
}