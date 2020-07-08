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
    internal partial interface ISchemaBuilder<TContext> :
        IInfrastructure<InternalSchemaBuilder>,
        IAnnotableBuilder<SchemaBuilder<TContext>>,
        IDescriptionBuilder<SchemaBuilder<TContext>>,
        IInfrastructure<SchemaDefinition> where TContext : GraphQLContext
    {
        ScalarTypeBuilder<object, ValueSyntax> Scalar(string name);

        ScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>() where TScalar : notnull;
        ScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>(string name) where TScalar : notnull;

        ScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType);
        ScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType, string name);

        ScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax
            where TScalar : notnull;

        SchemaBuilder<TContext> QueryType(string type);

        SchemaBuilder<TContext> QueryType(Type clrType);

        SchemaBuilder<TContext> MutationType(string type);

        SchemaBuilder<TContext> MutationType(Type clrType);

        SchemaBuilder<TContext> SubscriptionType(string type);
    }
}