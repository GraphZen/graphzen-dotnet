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
    public interface IInputFieldBuilder :
        IInfrastructure<InternalInputFieldBuilder>,
        IInfrastructure<MutableInputField>,
        IDirectivesBuilder<IInputFieldBuilder>
    {
        IInputFieldBuilder FieldType<T>();
        IInputFieldBuilder FieldType(Type clrType);
        IInputFieldBuilder FieldType(string type);
        IInputFieldBuilder DefaultValue(object? value);
        IInputFieldBuilder RemoveDefaultValue();
    }
}