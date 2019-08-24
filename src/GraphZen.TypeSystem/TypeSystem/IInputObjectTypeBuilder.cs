// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Linq.Expressions;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface IInputObjectTypeBuilder<TInputObject> : IAnnotableBuilder<IInputObjectTypeBuilder<TInputObject>>
    {
        
        IInputObjectTypeBuilder<TInputObject> Description(string description);

        
        IInputObjectTypeBuilder<object> ClrType(Type clrType);

        
        IInputObjectTypeBuilder<T> ClrType<T>();


        
        IInputObjectTypeBuilder<TInputObject> Field(string name, string type,
            Action<InputValueBuilder> inputFieldConfigurator = null);

        
        IInputObjectTypeBuilder<TInputObject> Field(string name,
            Action<InputValueBuilder> inputFieldConfigurator = null);

        
        IInputObjectTypeBuilder<TInputObject> Field<TField>(string name,
            Action<InputValueBuilder> inputFieldConfigurator = null);

        
        IInputObjectTypeBuilder<TInputObject> Field<TField>(Expression<Func<TInputObject, TField>> fieldSelector,
            Action<InputValueBuilder> fieldBuilder = null);

        
        IInputObjectTypeBuilder<TInputObject> IgnoreField<TField>(Expression<Func<TInputObject, TField>> fieldSelector);

        
        IInputObjectTypeBuilder<TInputObject> IgnoreField(string name);

        
        IInputObjectTypeBuilder<TInputObject> UnignoreField(string name);


        
        IInputObjectTypeBuilder<TInputObject> Name(string name);
    }
}