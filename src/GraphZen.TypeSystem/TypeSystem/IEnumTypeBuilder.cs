// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem
{
    public interface IEnumTypeBuilder<in TEnumValue> : IAnnotableBuilder<IEnumTypeBuilder<TEnumValue>>
    {
        [NotNull]
        IEnumTypeBuilder<TEnumValue> Description([CanBeNull] string description);

        [NotNull]
        IEnumTypeBuilder<TEnumValue> Value(TEnumValue value, Action<IEnumValueBuilder> valueConfigurator = null);

        [NotNull]
        IEnumTypeBuilder<TEnumValue> Name(string name);
    }
}