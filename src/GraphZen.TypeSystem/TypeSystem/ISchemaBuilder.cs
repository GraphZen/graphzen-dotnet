// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface ISchemaBuilder<TContext> :
        IInfrastructure<InternalSchemaBuilder>,
        IAnnotableBuilder<ISchemaBuilder<TContext>>, IInfrastructure<SchemaDefinition>
        where TContext : GraphQLContext
    {
        IDirectiveBuilder<object> Directive(string name);


        IScalarTypeBuilder<object, ValueSyntax> Scalar(string name);


        IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>();


        IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType);


        IScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax;


        IObjectTypeBuilder<object, TContext> Object(Type clrType);


        IObjectTypeBuilder<object, TContext> Object(string name);


        IObjectTypeBuilder<TObject, TContext> Object<TObject>();


        ISchemaBuilder<TContext> IgnoreType<TObject>();


        ISchemaBuilder<TContext> IgnoreType(Type clrType);


        ISchemaBuilder<TContext> IgnoreType(string name);


        ISchemaBuilder<TContext> UnignoreType<TObject>();


        ISchemaBuilder<TContext> UnignoreType(Type clrType);


        ISchemaBuilder<TContext> UnignoreType(string name);

        ISchemaBuilder<TContext> IgnoreDirective<TDirective>();


        ISchemaBuilder<TContext> IgnoreDirective(Type clrType);


        ISchemaBuilder<TContext> IgnoreDirective(string name);


        ISchemaBuilder<TContext> UnignoreDirective<TObject>();


        ISchemaBuilder<TContext> UnignoreDirective(Type clrType);


        ISchemaBuilder<TContext> UnignoreDirective(string name);



        IInterfaceTypeBuilder<object, TContext> Interface(string name);


        IInterfaceTypeBuilder<object, TContext> Interface(Type clrType);


        IInterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>();


        IUnionTypeBuilder<object, TContext> Union(string name);


        IUnionTypeBuilder<TUnion, TContext> Union<TUnion>();


        IUnionTypeBuilder<object, TContext> Union(Type clrType);


        IEnumTypeBuilder<string> Enum(string name);


        IEnumTypeBuilder<TEnum> Enum<TEnum>();


        IEnumTypeBuilder<string> Enum(Type clrType);


        IInputObjectTypeBuilder<object> InputObject(string name);


        IInputObjectTypeBuilder<TInput> InputObject<TInput>();


        IInputObjectTypeBuilder<object> InputObject(Type clrType);


        ISchemaBuilder<TContext> QueryType(string type);


        ISchemaBuilder<TContext> QueryType(Type clrType);


        ISchemaBuilder<TContext> MutationType(string type);


        ISchemaBuilder<TContext> MutationType(Type clrType);


        ISchemaBuilder<TContext> SubscriptionType(string type);
    }
}