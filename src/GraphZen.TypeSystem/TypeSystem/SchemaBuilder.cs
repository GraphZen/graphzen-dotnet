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
    public class SchemaBuilder : SchemaBuilder<GraphQLContext>
    {
        public SchemaBuilder(SchemaDefinition schemaDefinition) : base(schemaDefinition)
        {
        }
    }

    public partial class SchemaBuilder<TContext> : ISchemaBuilder<TContext> where TContext : GraphQLContext
    {
        public SchemaBuilder(SchemaDefinition schemaDefinition)
        {
            Check.NotNull(schemaDefinition, nameof(schemaDefinition));
            Builder = schemaDefinition.Builder;
        }

        private InternalSchemaBuilder Builder { get; }

        public IScalarTypeBuilder<object, ValueSyntax> Scalar(string name) =>
            new ScalarTypeBuilder<object, ValueSyntax>(Builder.Scalar(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);


        public IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>() where TScalar : notnull
        {
            var internalBuilder = Builder.Scalar(typeof(TScalar),
                ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<TScalar, ValueSyntax>(internalBuilder);
        }

        public IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>(string name) where TScalar : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Scalar(typeof(TScalar), name, ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<TScalar, ValueSyntax>(internalBuilder);
        }

        public IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Scalar(clrType, ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<object, ValueSyntax>(internalBuilder);
        }

        public IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Scalar(clrType, name, ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<object, ValueSyntax>(internalBuilder);
        }

        public IScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax where TScalar : notnull
        {
            var internalBuilder = Builder.Scalar(typeof(TScalar), ConfigurationSource.Explicit)!;
            return new ScalarTypeBuilder<TScalar, TValueNode>(internalBuilder);
        }

        public ISchemaBuilder<TContext> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> QueryType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.QueryType(type, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> QueryType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.QueryType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> MutationType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.MutationType(type, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> MutationType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.MutationType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> SubscriptionType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.SubscriptionType(type, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> AddDirectiveAnnotation(string name, object? value = null) =>
            throw new NotImplementedException();

        public ISchemaBuilder<TContext> UpdateOrAddDirectiveAnnotation(string name, object? value)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public ISchemaBuilder<TContext> RemoveDirectiveAnnotations() => throw new NotImplementedException();

        InternalSchemaBuilder IInfrastructure<InternalSchemaBuilder>.Instance => Builder;

        SchemaDefinition IInfrastructure<SchemaDefinition>.Instance => Builder.Definition;


        public ISchemaBuilder<TContext> DirectiveAnnotation(string name) => UpdateOrAddDirectiveAnnotation(name, null);

        public ISchemaBuilder<TContext> DirectiveAnnotation(object directive) =>
            throw new NotImplementedException();

        public ISchemaBuilder<TContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }
}