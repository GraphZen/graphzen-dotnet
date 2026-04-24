// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem;

public interface IEnumTypeBuilder<in TEnumValue> : IAnnotableBuilder<IEnumTypeBuilder<TEnumValue>>
{
    IEnumTypeBuilder<TEnumValue> Description(string? description);
    IEnumTypeBuilder<TEnumValue> Value(TEnumValue value, Action<IEnumValueBuilder>? configurator = null);
    IEnumTypeBuilder<TEnumValue> IgnoreValue(TEnumValue value);
    IEnumTypeBuilder<TEnumValue> UnignoreValue(TEnumValue value);
    IEnumTypeBuilder<TEnumValue> Name(string name);
    IEnumTypeBuilder<object> ClrType(Type clrType);
    IEnumTypeBuilder<T> ClrType<T>();
}