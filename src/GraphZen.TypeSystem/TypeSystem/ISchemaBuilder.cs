// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

// ReSharper disable PossibleInterfaceMemberAmbiguity
namespace GraphZen.TypeSystem
{
    public partial interface ISchemaBuilder<TContext> :
        IInfrastructure<InternalSchemaBuilder>,
        IAnnotableBuilder<ISchemaBuilder<TContext>>,
        IInfrastructure<SchemaDefinition> where TContext : GraphQLContext
    {
        IDirectiveBuilder<object> Directive(string name);


        IScalarTypeBuilder<object, ValueSyntax> Scalar(string name);


        IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>();


        IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType);


        IScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax;


     

   

        IEnumTypeBuilder<string> Enum(string name);


        IEnumTypeBuilder<TEnum> Enum<TEnum>() where TEnum : notnull;


        IEnumTypeBuilder<string> Enum(Type clrType);


        ISchemaBuilder<TContext> QueryType(string type);


        ISchemaBuilder<TContext> QueryType(Type clrType);


        ISchemaBuilder<TContext> MutationType(string type);


        ISchemaBuilder<TContext> MutationType(Type clrType);


        ISchemaBuilder<TContext> SubscriptionType(string type);
    }
}