// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{

    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IEnumTypeBuilder : INamedTypeBuilder,
        IInfrastructure<EnumTypeDefinition>,
        IInfrastructure<InternalEnumTypeBuilder>
    {

    }

    public interface IEnumTypeBuilder<TEnumValue> : IEnumTypeBuilder,
        INamedTypeBuilder<EnumTypeBuilder<TEnumValue>, EnumTypeBuilder<object>>
         where TEnumValue : notnull
    {
        EnumTypeBuilder<TEnumValue> Value(TEnumValue value);
        EnumTypeBuilder<TEnumValue> RemoveValue(TEnumValue value);
        EnumTypeBuilder<TEnumValue> Value(TEnumValue value, Action<EnumValueBuilder> configurator);
        EnumTypeBuilder<TEnumValue> IgnoreValue(TEnumValue value);
        EnumTypeBuilder<TEnumValue> UnignoreValue(TEnumValue value);
        EnumTypeBuilder<T> ClrType<T>(bool inferName = false) where T : notnull;
        EnumTypeBuilder<T> ClrType<T>(string name) where T : notnull;
    }
}