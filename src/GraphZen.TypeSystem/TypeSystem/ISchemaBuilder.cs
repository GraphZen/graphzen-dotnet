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
        IScalarTypeBuilder<object, ValueSyntax> Scalar(string name);

        IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>() where TScalar : notnull;
        IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>(string name) where TScalar : notnull;

        IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType);
        IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType, string name);

        IScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax
            where TScalar : notnull;

        ISchemaBuilder<TContext> Description(string description);
        ISchemaBuilder<TContext> RemoveDescription();
        ISchemaBuilder<TContext> QueryType(string type);

        ISchemaBuilder<TContext> QueryType(Type clrType);

        ISchemaBuilder<TContext> MutationType(string type);

        ISchemaBuilder<TContext> MutationType(Type clrType);

        ISchemaBuilder<TContext> SubscriptionType(string type);
    }
}