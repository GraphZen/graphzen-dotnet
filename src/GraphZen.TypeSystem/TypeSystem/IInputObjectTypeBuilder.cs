// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq.Expressions;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface IInputObjectTypeBuilder<TInputObject> : IAnnotableBuilder<IInputObjectTypeBuilder<TInputObject>>
    {
        [NotNull]
        IInputObjectTypeBuilder<TInputObject> Description(string description);

        [NotNull]
        IInputObjectTypeBuilder<object> ClrType(Type clrType);

        [NotNull]
        IInputObjectTypeBuilder<T> ClrType<T>();


        [NotNull]
        IInputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputValueBuilder> inputFieldConfigurator = null);

        [NotNull]
        IInputObjectTypeBuilder<TInputObject> Field(string name,
            Action<InputValueBuilder> inputFieldConfigurator = null);

        [NotNull]
        IInputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputValueBuilder> inputFieldConfigurator = null);

        [NotNull]
        IInputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputValueBuilder> fieldBuilder = null);

        [NotNull]
        IInputObjectTypeBuilder<TInputObject> IgnoreField<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        [NotNull]
        IInputObjectTypeBuilder<TInputObject> IgnoreField(string name);

        [NotNull]
        IInputObjectTypeBuilder<TInputObject> UnignoreField(string name);


        [NotNull]
        IInputObjectTypeBuilder<TInputObject> Name(string name);
    }
}