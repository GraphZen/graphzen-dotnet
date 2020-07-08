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
    internal interface IInputFieldBuilder<T> : IInfrastructure<InternalInputFieldBuilder>,
        IInfrastructure<InputFieldDefinition>,
        IAnnotableBuilder<InputFieldBuilder<T>>
    {
        InputFieldBuilder<TNew> FieldType<TNew>();
        InputFieldBuilder<object?> FieldType(Type clrType);
        InputFieldBuilder<object?> FieldType(string type);
    }
}