// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IFieldDefinition :
        IAnnotatableDefinition,
        IArgumentsDefinition,
        ITypeReferenceDefinition,
        INamed,
        IDescription,
        IDeprecation,
        IClrInfo,
        IOutputDefinition,
        IMemberParentDefinition
    {
        IGraphQLTypeReference FieldType { get; }


        Resolver<object, object?>? Resolver { get; }

        IFieldsDefinition? DeclaringType { get; }

        new MemberInfo? ClrInfo { get; }
    }
}