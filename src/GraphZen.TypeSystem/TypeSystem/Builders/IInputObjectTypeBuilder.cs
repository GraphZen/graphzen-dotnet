// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity

    public interface IInputObjectTypeBuilder : INamedTypeDefinitionBuilder<IInputObjectTypeBuilder, IInputObjectTypeBuilder>,
        IInfrastructure<InternalInputObjectTypeBuilder>,
        IInfrastructure<MutableInputObjectType>
    {
        IInputObjectTypeBuilder Field(string name, string type);
        IInputObjectTypeBuilder RemoveField(string name);

        IInputObjectTypeBuilder Field(string name, string type, Action<IInputFieldBuilder> fieldAction);

        IInputFieldBuilder Field(string name);

        IInputObjectTypeBuilder Field(string name, Action<IInputFieldBuilder> fieldAction);

        IInputObjectTypeBuilder Field<TField>(string name);

        IInputObjectTypeBuilder Field<TField>(string name, Action<IInputFieldBuilder> fieldAction);

        IInputObjectTypeBuilder IgnoreField(string name);

        IInputObjectTypeBuilder UnignoreField(string name);
    }


    internal interface IInputObjectTypeBuilder<out TBuilder> : IInputObjectTypeBuilder,
        INamedTypeDefinitionBuilder<IInputObjectTypeBuilder<TBuilder>, IInputObjectTypeBuilder<TBuilder>>
    {
        new TBuilder Field(string name, string type);
        new TBuilder RemoveField(string name);

        new TBuilder Field(string name, string type, Action<IInputFieldBuilder> fieldAction);


        new TBuilder Field(string name, Action<IInputFieldBuilder> fieldAction);

        new TBuilder Field<TField>(string name);

        new TBuilder Field<TField>(string name, Action<IInputFieldBuilder> fieldAction);

        new TBuilder IgnoreField(string name);

        new TBuilder UnignoreField(string name);
    }
}