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
        where TInputObject : notnull
    {
        InputObjectTypeBuilder<T> ClrType<T>(bool inferName = false) where T : notnull;
        InputObjectTypeBuilder<T> ClrType<T>(string name) where T : notnull;
        InputObjectTypeBuilder<TInputObject> Field(string name, string type);
        InputObjectTypeBuilder<TInputObject> RemoveField(string name);

        InputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputFieldBuilder<object?>> inputFieldConfigurator);

        InputFieldBuilder<object?> Field(string name);

        InputObjectTypeBuilder<TInputObject> Field(string name,
            Action<InputFieldBuilder<object?>> inputFieldConfigurator);

        InputObjectTypeBuilder<TInputObject> Field<TField>(string name);

        InputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputFieldBuilder<TField>> inputFieldConfigurator);

        InputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        InputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputFieldBuilder<TField>> fieldBuilder);

        InputObjectTypeBuilder<TInputObject> IgnoreField<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        InputObjectTypeBuilder<TInputObject> IgnoreField(string name);

        InputObjectTypeBuilder<TInputObject> UnignoreField(string name);
    }
}