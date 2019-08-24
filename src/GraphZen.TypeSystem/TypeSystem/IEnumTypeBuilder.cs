// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface IEnumTypeBuilder<in TEnumValue> : IAnnotableBuilder<IEnumTypeBuilder<TEnumValue>>
    {
        
        IEnumTypeBuilder<TEnumValue> Description( string description);

        
        IEnumTypeBuilder<TEnumValue> Value(TEnumValue value, Action<IEnumValueBuilder> valueConfigurator = null);

        
        IEnumTypeBuilder<TEnumValue> Name(string name);

        
        IEnumTypeBuilder<object> ClrType(Type clrType);

        
        IEnumTypeBuilder<T> ClrType<T>();

    }
}