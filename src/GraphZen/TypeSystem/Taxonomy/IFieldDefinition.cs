// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Reflection;
using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IFieldDefinition : IAnnotatableDefinition, IArgumentsContainerDefinition, INamed, IDeprecation,
        IClrInfo,
        IOutputDefinition
    {
        [CanBeNull]
        IGraphQLTypeReference FieldType { get; }

        [CanBeNull]
        Resolver<object, object> Resolver { get; }

        IFieldsContainerDefinition DeclaringType { get; }


        new MemberInfo ClrInfo { get; }
    }
}