// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity


    public interface IInputObjectTypeBuilder<TInputObject> : IAnnotableBuilder<IInputObjectTypeBuilder<TInputObject>>,
        IInfrastructure<InternalInputObjectTypeBuilder>, IInfrastructure<IInputObjectTypeDefinition>
    {
        IInputObjectTypeBuilder<TInputObject> Description(string description);
        IInputObjectTypeBuilder<object> ClrType(Type clrType);
        IInputObjectTypeBuilder<object> ClrType(Type clrType, string name);
        IInputObjectTypeBuilder<object> RemoveClrType();
        IInputObjectTypeBuilder<T> ClrType<T>();
        IInputObjectTypeBuilder<T> ClrType<T>(string name);
        IInputObjectTypeBuilder<TInputObject> Field(string name, string type);
        IInputObjectTypeBuilder<TInputObject> RemoveField(string name);

        IInputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputValueBuilder> inputFieldConfigurator);

        InputValueBuilder Field(string name);
        IInputObjectTypeBuilder<TInputObject> Field(string name, Action<InputValueBuilder> inputFieldConfigurator);

        IInputObjectTypeBuilder<TInputObject> Field<TField>(string name);

        IInputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputValueBuilder> inputFieldConfigurator);

        IInputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        IInputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputValueBuilder> fieldBuilder);

        IInputObjectTypeBuilder<TInputObject> IgnoreField<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        IInputObjectTypeBuilder<TInputObject> IgnoreField(string name);

        IInputObjectTypeBuilder<TInputObject> UnignoreField(string name);

        IInputObjectTypeBuilder<TInputObject> Name(string name);
    }
}