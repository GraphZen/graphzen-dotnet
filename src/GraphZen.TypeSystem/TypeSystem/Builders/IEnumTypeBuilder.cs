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
    public interface IEnumTypeBuilder : INamedTypeDefinitionBuilder<IEnumTypeBuilder, IEnumTypeBuilder>,
        IInfrastructure<MutableEnumType>,
        IInfrastructure<InternalEnumTypeBuilder>
    {
        IEnumTypeBuilder Value(string name);
        IEnumTypeBuilder Value(string name, Action<IEnumValueBuilder> valueAction);
        IEnumTypeBuilder Value(string name, object? value);
        IEnumTypeBuilder Value(string name, object? value, Action<IEnumValueBuilder> valueAction);
        IEnumTypeBuilder RemoveValue(string name);
        IEnumTypeBuilder ClearValues();
        IEnumTypeBuilder IgnoreValue(string name);
        IEnumTypeBuilder UnignoreValue(string name);
    }
}