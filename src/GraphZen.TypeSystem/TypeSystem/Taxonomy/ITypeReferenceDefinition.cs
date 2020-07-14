// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface INamedTypeReference : IChildMember, INullableType
    {
        INamedTypeDefinition Type { get; }
    }

    public interface INamedTypeReference<out T> : INamedTypeReference where T : INamedTypeDefinition
    {
        new T Type { get; }
    }

    public interface IInterfaceTypeReference : INamedTypeReference<IInterfaceType> { }

    public interface IObjectTypeReference : INamedTypeReference<IObjectType> { }
}