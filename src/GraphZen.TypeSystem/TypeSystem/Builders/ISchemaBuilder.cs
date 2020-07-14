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
    public partial interface ISchemaBuilder :
        IInfrastructure<InternalSchemaBuilder>,
        IDirectivesBuilder<ISchemaBuilder>,
        IDescriptionBuilder<ISchemaBuilder>,
        IInfrastructure<MutableSchema>

    {

        IScalarTypeBuilder Scalar(string name);
        IScalarTypeBuilder Scalar(Type clrType);
        IScalarTypeBuilder Scalar(Type clrType, string name);

        ISchemaBuilder QueryType(string type);
        ISchemaBuilder QueryType(Type clrType);
        ISchemaBuilder MutationType(string type);
        ISchemaBuilder MutationType(Type clrType);
        ISchemaBuilder SubscriptionType(string type);
    }

    public partial interface ISchemaBuilder<TContext> : ISchemaBuilder,
        IDirectivesBuilder<ISchemaBuilder<TContext>>,
        IDescriptionBuilder<ISchemaBuilder<TContext>>
    {

    }
}