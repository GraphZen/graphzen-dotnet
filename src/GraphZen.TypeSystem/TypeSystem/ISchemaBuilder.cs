// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface ISchemaBuilder<TContext> :
        IInfrastructure<InternalSchemaBuilder>,
        IAnnotableBuilder<ISchemaBuilder<TContext>>, IInfrastructure<SchemaDefinition>
        where TContext : GraphQLContext
    {
        [NotNull]
        IDirectiveBuilder Directive(string name);

        [NotNull]
        IScalarTypeBuilder<object, ValueSyntax> Scalar(string name);

        [NotNull]
        IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>();

        [NotNull]
        IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType);


        [NotNull]
        IScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax;


        [NotNull]
        IObjectTypeBuilder<object, TContext> Object(Type clrType);


        [NotNull]
        IObjectTypeBuilder<object, TContext> Object(string name);

        [NotNull]
        IObjectTypeBuilder<TObject, TContext> Object<TObject>();

        [NotNull]
        ISchemaBuilder<TContext> IgnoreType<TObject>();


        [NotNull]
        ISchemaBuilder<TContext> IgnoreType(Type clrType);

        [NotNull]
        ISchemaBuilder<TContext> IgnoreType(string name);


        [NotNull]
        IInterfaceTypeBuilder<object, TContext> Interface(string name);


        [NotNull]
        IInterfaceTypeBuilder<object, TContext> Interface(Type clrType);


        [NotNull]
        IInterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>();

        [NotNull]
        IUnionTypeBuilder<object, TContext> Union(string name);

        [NotNull]
        IUnionTypeBuilder<TUnion, TContext> Union<TUnion>();

        [NotNull]
        IUnionTypeBuilder<object, TContext> Union(Type clrType);


        [NotNull]
        IEnumTypeBuilder<string> Enum(string name);

        [NotNull]
        IEnumTypeBuilder<TEnum> Enum<TEnum>();

        [NotNull]
        IEnumTypeBuilder<string> Enum(Type clrType);


        [NotNull]
        IInputObjectTypeBuilder<object> InputObject(string name);

        [NotNull]
        IInputObjectTypeBuilder<TInput> InputObject<TInput>();

        [NotNull]
        IInputObjectTypeBuilder<object> InputObject(Type clrType);


        [NotNull]
        ISchemaBuilder<TContext> QueryType(string type);

        [NotNull]
        ISchemaBuilder<TContext> QueryType(Type clrType);


        [NotNull]
        ISchemaBuilder<TContext> MutationType(string type);

        [NotNull]
        ISchemaBuilder<TContext> MutationType(Type clrType);


        [NotNull]
        ISchemaBuilder<TContext> SubscriptionType(string type);
    }
}