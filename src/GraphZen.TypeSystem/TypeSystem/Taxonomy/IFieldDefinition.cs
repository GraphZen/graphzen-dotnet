// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System.Reflection;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IFieldDefinition : IAnnotatableDefinition, IArgumentsContainerDefinition, INamed, IDeprecation,
        IClrInfo,
        IOutputDefinition
    {
        
        IGraphQLTypeReference? FieldType { get; }

        
        Resolver<object, object> Resolver { get; }

        IFieldsContainerDefinition DeclaringType { get; }

        new MemberInfo ClrInfo { get; }
    }
}