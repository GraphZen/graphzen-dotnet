// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IEnumTypeBuilder<TEnumValue> :
        IInfrastructure<InternalEnumTypeBuilder>,
        IAnnotableBuilder<EnumTypeBuilder<TEnumValue>>,
        IDescriptionBuilder<EnumTypeBuilder<TEnumValue>>,
        INamedBuilder<EnumTypeBuilder<TEnumValue>>,
        IClrTypeBuilder<EnumTypeBuilder<object>> where TEnumValue : notnull
    {
        EnumTypeBuilder<TEnumValue> Value(TEnumValue value);
        EnumTypeBuilder<TEnumValue> Value(TEnumValue value, Action<EnumValueBuilder> configurator);
        EnumTypeBuilder<TEnumValue> IgnoreValue(TEnumValue value);
        EnumTypeBuilder<TEnumValue> UnignoreValue(TEnumValue value);
        EnumTypeBuilder<T> ClrType<T>() where T : notnull;
        EnumTypeBuilder<T> ClrType<T>(string name) where T : notnull;
    }
}