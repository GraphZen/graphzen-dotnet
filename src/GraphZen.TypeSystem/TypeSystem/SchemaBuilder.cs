// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public class SchemaBuilder : ISchemaBuilder<GraphQLContext>
    {
        public SchemaBuilder(SchemaDefinition schemaDefinition)
        {
            Check.NotNull(schemaDefinition, nameof(schemaDefinition));
            Builder = schemaDefinition.Builder;
        }

        [NotNull]
        protected InternalSchemaBuilder Builder { get; }


        public IDirectiveBuilder Directive(string name) =>
            new DirectiveBuilder(Builder.Directive(Check.NotNull(name, nameof(name)), ConfigurationSource.Explicit));

        public IScalarTypeBuilder<object, ValueSyntax> Scalar(string name) =>
            new ScalarTypeBuilder<object, ValueSyntax>(Builder.Scalar(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));


        public IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>() =>
            new ScalarTypeBuilder<TScalar, ValueSyntax>(Builder.Scalar(typeof(TScalar), ConfigurationSource.Explicit));

        public IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType) =>
            new ScalarTypeBuilder<object, ValueSyntax>(Builder.Scalar(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit));

        public IScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax =>
            new ScalarTypeBuilder<TScalar, TValueNode>(Builder.Scalar(typeof(TScalar), ConfigurationSource.Explicit));

        public IObjectTypeBuilder<object, GraphQLContext> Object(Type clrType) =>
            new ObjectTypeBuilder<object, GraphQLContext>(Builder.Object(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit
            ));

        public IObjectTypeBuilder<object, GraphQLContext> Object(string name) =>
            new ObjectTypeBuilder<object, GraphQLContext>(Builder.Object(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        public IObjectTypeBuilder<TObject, GraphQLContext> Object<TObject>() =>
            new ObjectTypeBuilder<TObject, GraphQLContext>(
                Builder.Object(typeof(TObject), ConfigurationSource.Explicit));

        public ISchemaBuilder<GraphQLContext> IgnoreType<TClrType>() => IgnoreType(typeof(TClrType));

        public ISchemaBuilder<GraphQLContext> IgnoreType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreType(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreType(name, ConfigurationSource.Explicit);
            return this;
        }


        public IInterfaceTypeBuilder<object, GraphQLContext> Interface(string name) =>
            new InterfaceTypeBuilder<object, GraphQLContext>(Builder.Interface(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        public IInterfaceTypeBuilder<object, GraphQLContext> Interface(Type clrType) =>
            new InterfaceTypeBuilder<object, GraphQLContext>(Builder.Interface(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit
            ));

        public IInterfaceTypeBuilder<TInterface, GraphQLContext> Interface<TInterface>() =>
            new InterfaceTypeBuilder<TInterface, GraphQLContext>(Builder.Interface(typeof(TInterface),
                ConfigurationSource.Explicit));

        public IUnionTypeBuilder<object, GraphQLContext> Union(string name) =>
            new UnionTypeBuilder<object, GraphQLContext>(Builder.Union(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        public IUnionTypeBuilder<TUnion, GraphQLContext> Union<TUnion>() =>
            new UnionTypeBuilder<TUnion, GraphQLContext>(Builder.Union(typeof(TUnion), ConfigurationSource.Explicit));

        public IUnionTypeBuilder<object, GraphQLContext> Union(Type clrType) =>
            new UnionTypeBuilder<object, GraphQLContext>(Builder.Union(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit));

        public IEnumTypeBuilder<string> Enum(string name) =>
            new EnumTypeBuilder<string>(Builder.Enum(Check.NotNull(name, nameof(name)), ConfigurationSource.Explicit));

        public IEnumTypeBuilder<TEnum> Enum<TEnum>() =>
            new EnumTypeBuilder<TEnum>(Builder.Enum(typeof(TEnum), ConfigurationSource.Explicit));

        public IEnumTypeBuilder<string> Enum(Type clrType) =>
            new EnumTypeBuilder<string>(Builder.Enum(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit));

        public IInputObjectTypeBuilder<object> InputObject(string name) =>
            new InputObjectTypeBuilder<object>(Builder.InputObject(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        public IInputObjectTypeBuilder<TInput> InputObject<TInput>() =>
            new InputObjectTypeBuilder<TInput>(Builder.InputObject(typeof(TInput), ConfigurationSource.Explicit));

        public IInputObjectTypeBuilder<object> InputObject(Type clrType) =>
            new InputObjectTypeBuilder<object>(Builder.InputObject(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit));

        public ISchemaBuilder<GraphQLContext> QueryType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.QueryType(type, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> QueryType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.QueryType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> MutationType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.MutationType(type, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> MutationType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.MutationType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> SubscriptionType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.SubscriptionType(type, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);

        public ISchemaBuilder<GraphQLContext> DirectiveAnnotation(string name, object value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }

        InternalSchemaBuilder IInfrastructure<InternalSchemaBuilder>.Instance => Builder;

        SchemaDefinition IInfrastructure<SchemaDefinition>.Instance => Builder.Definition;
    }


    public class SchemaBuilder<TContext> : SchemaBuilder, ISchemaBuilder<TContext> where TContext : GraphQLContext

    {
        public SchemaBuilder(SchemaDefinition schemaDefinition) : base(schemaDefinition)
        {
        }


        public new IObjectTypeBuilder<object, TContext> Object(Type clrType) =>
            new ObjectTypeBuilder<object, TContext>(Builder.Object(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit));

        public new IObjectTypeBuilder<object, TContext> Object(string name) =>
            new ObjectTypeBuilder<object, TContext>(Builder.Object(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        public new IObjectTypeBuilder<TObject, TContext> Object<TObject>() =>
            new ObjectTypeBuilder<TObject, TContext>(Builder.Object(typeof(TObject), ConfigurationSource.Explicit));

        public new ISchemaBuilder<TContext> IgnoreType<TObject>() =>
            (ISchemaBuilder<TContext>)base.IgnoreType<TObject>();

        public new ISchemaBuilder<TContext> IgnoreType(Type clrType) =>
            (ISchemaBuilder<TContext>)base.IgnoreType(clrType);

        public new ISchemaBuilder<TContext> IgnoreType(string name) => (ISchemaBuilder<TContext>)base.IgnoreType(name);

        public new IInterfaceTypeBuilder<object, TContext> Interface(string name) =>
            new InterfaceTypeBuilder<object, TContext>(Builder.Interface(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        public new IInterfaceTypeBuilder<object, TContext> Interface(Type clrType) =>
            new InterfaceTypeBuilder<object, TContext>(Builder.Interface(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit
            ));

        public new IInterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>() =>
            new InterfaceTypeBuilder<TInterface, TContext>(Builder.Interface(typeof(TInterface),
                ConfigurationSource.Explicit));

        public new IUnionTypeBuilder<object, TContext> Union(string name) =>
            new UnionTypeBuilder<object, TContext>(Builder.Union(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        public new IUnionTypeBuilder<TUnion, TContext> Union<TUnion>() =>
            new UnionTypeBuilder<TUnion, TContext>(Builder.Union(typeof(TUnion), ConfigurationSource.Explicit));

        public new IUnionTypeBuilder<object, TContext> Union(Type clrType) =>
            new UnionTypeBuilder<object, TContext>(Builder.Union(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit));

        public new ISchemaBuilder<TContext> QueryType(string type)
        {
            base.QueryType(type);
            return this;
        }

        public new ISchemaBuilder<TContext> QueryType(Type clrType) =>
            (ISchemaBuilder<TContext>)base.QueryType(clrType);

        public new ISchemaBuilder<TContext> MutationType(string type)
        {
            base.MutationType(type);
            return this;
        }

        public new ISchemaBuilder<TContext> MutationType(Type clrType) =>
            (ISchemaBuilder<TContext>)base.MutationType(clrType);

        public new ISchemaBuilder<TContext> SubscriptionType(string type)
        {
            base.SubscriptionType(type);
            return this;
        }

        public new ISchemaBuilder<TContext> DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);

        public new ISchemaBuilder<TContext> DirectiveAnnotation(string name, object value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public new ISchemaBuilder<TContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }
}