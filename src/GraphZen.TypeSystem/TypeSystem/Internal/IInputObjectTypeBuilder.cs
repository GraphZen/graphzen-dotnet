// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity


    internal interface IInputObjectTypeBuilder<TInputObject> :
        IAnnotableBuilder<InputObjectTypeBuilder<TInputObject>>,
        IClrTypeBuilder<InputObjectTypeBuilder<object>>,
        INamedBuilder<InputObjectTypeBuilder<TInputObject>>,
        IDescriptionBuilder<InputObjectTypeBuilder<TInputObject>>,
        IInfrastructure<InternalInputObjectTypeBuilder>,
        IInfrastructure<InputObjectTypeDefinition>
    {
        InputObjectTypeBuilder<T> ClrType<T>();
        InputObjectTypeBuilder<T> ClrType<T>(string name);
        InputObjectTypeBuilder<TInputObject> Field(string name, string type);
        InputObjectTypeBuilder<TInputObject> RemoveField(string name);

        InputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputValueBuilder<object?>> inputFieldConfigurator);

        InputValueBuilder<object?> Field(string name);

        InputObjectTypeBuilder<TInputObject> Field(string name,
            Action<InputValueBuilder<object?>> inputFieldConfigurator);

        InputObjectTypeBuilder<TInputObject> Field<TField>(string name);

        InputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputValueBuilder<TField>> inputFieldConfigurator);

        InputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        InputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputValueBuilder<TField>> fieldBuilder);

        InputObjectTypeBuilder<TInputObject> IgnoreField<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        InputObjectTypeBuilder<TInputObject> IgnoreField(string name);

        InputObjectTypeBuilder<TInputObject> UnignoreField(string name);
    }
}