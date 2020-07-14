// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IField :
        IDirectives,
        IArguments,
        IName,
        IDescription,
        IMaybeDeprecated,
        IClrInfo,
        IOutputMember,
        IChildMember, 
        ISyntaxMember

    {
        IGraphQLType FieldType { get; }
        Resolver<object, object?>? Resolver { get; }
        IFields? DeclaringType { get; }
        new MemberInfo? ClrInfo { get; }
    }
}