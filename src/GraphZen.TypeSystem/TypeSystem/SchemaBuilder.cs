// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public partial class SchemaBuilder : ISchemaBuilder<GraphQLContext>
    {
        public SchemaBuilder(SchemaDefinition schemaDefinition)
        {
            Check.NotNull(schemaDefinition, nameof(schemaDefinition));
            Builder = schemaDefinition.Builder;
        }


        protected InternalSchemaBuilder Builder { get; }


        

        public IScalarTypeBuilder<object, ValueSyntax> Scalar(string name) =>
            new ScalarTypeBuilder<object, ValueSyntax>(Builder.Scalar(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);


        public IScalarTypeBuilder<TScalar, ValueSyntax> Scalar<TScalar>() where TScalar : notnull =>
            new ScalarTypeBuilder<TScalar, ValueSyntax>(Builder.Scalar(typeof(TScalar),
                ConfigurationSource.Explicit)!);

        public IScalarTypeBuilder<object, ValueSyntax> Scalar(Type clrType) =>
            new ScalarTypeBuilder<object, ValueSyntax>(Builder.Scalar(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit)!);

        public IScalarTypeBuilder<TScalar, TValueNode> Scalar<TScalar, TValueNode>()
            where TValueNode : ValueSyntax where TScalar : notnull =>
            new ScalarTypeBuilder<TScalar, TValueNode>(Builder.Scalar(typeof(TScalar),
                ConfigurationSource.Explicit)!);

  
        public ISchemaBuilder<GraphQLContext> UnignoreScalar<TScalar>() where TScalar : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreScalar(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreScalar(string name) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreScalar<TScalar>() where TScalar : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreScalar(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreScalar(string name) => throw new NotImplementedException();

        public IObjectTypeBuilder<object, GraphQLContext> Object(Type clrType) =>
            new ObjectTypeBuilder<object, GraphQLContext>(Builder.Object(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit
            )!);

        public IObjectTypeBuilder<object, GraphQLContext> Object(string name) =>
            new ObjectTypeBuilder<object, GraphQLContext>(Builder.Object(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public IObjectTypeBuilder<TObject, GraphQLContext> Object<TObject>() where TObject : notnull =>
            new ObjectTypeBuilder<TObject, GraphQLContext>(
                Builder.Object(typeof(TObject), ConfigurationSource.Explicit)!);

        public ISchemaBuilder<GraphQLContext> UnignoreUnion<TUnion>() where TUnion : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreUnion(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreUnion(string name) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreUnion<TUnion>() where TUnion : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreUnion(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreUnion(string name) => throw new NotImplementedException();

               
      
        public IInterfaceTypeBuilder<object, GraphQLContext> Interface(string name) =>
            new InterfaceTypeBuilder<object, GraphQLContext>(Builder.Interface(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public ISchemaBuilder<GraphQLContext> IgnoreInterface(string name) => throw new NotImplementedException();

        public IInterfaceTypeBuilder<object, GraphQLContext> Interface(Type clrType) =>
            new InterfaceTypeBuilder<object, GraphQLContext>(Builder.Interface(
                Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit
            )!);

        public IInterfaceTypeBuilder<TInterface, GraphQLContext> Interface<TInterface>() where TInterface : notnull =>
            new InterfaceTypeBuilder<TInterface, GraphQLContext>(Builder.Interface(typeof(TInterface),
                ConfigurationSource.Explicit)!);

             public IUnionTypeBuilder<object, GraphQLContext> Union(string name) =>
            new UnionTypeBuilder<object, GraphQLContext>(Builder.Union(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public IUnionTypeBuilder<TUnion, GraphQLContext> Union<TUnion>() where TUnion : notnull =>
            new UnionTypeBuilder<TUnion, GraphQLContext>(Builder.Union(typeof(TUnion),
                ConfigurationSource.Explicit)!);

        public IUnionTypeBuilder<object, GraphQLContext> Union(Type clrType) =>
            new UnionTypeBuilder<object, GraphQLContext>(Builder.Union(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit)!);

        public IEnumTypeBuilder<string> Enum(string name) =>
            new EnumTypeBuilder<string>(Builder.Enum(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public IEnumTypeBuilder<TEnum> Enum<TEnum>() where TEnum : notnull =>
            new EnumTypeBuilder<TEnum>(Builder.Enum(typeof(TEnum), ConfigurationSource.Explicit)!);

        public IEnumTypeBuilder<string> Enum(Type clrType) =>
            new EnumTypeBuilder<string>(Builder.Enum(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit)!);

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

        public ISchemaBuilder<GraphQLContext> UnignoreEnum<TEnum>() where TEnum : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreEnum(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreEnum(string name) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreEnum<TEnum>() where TEnum : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreEnum(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreEnum(string name) => throw new NotImplementedException();


        public ISchemaBuilder<GraphQLContext> UnignoreInputObject<TInputObject>() where TInputObject : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreInputObject(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreInputObject(string name) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreInputObject<TInputObject>() where TInputObject : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreInputObject(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreInputObject(string name) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreInterface<TInterface>() where TInterface : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreInterface(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> UnignoreInterface(string name) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreInterface<TInterface>() where TInterface : notnull =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> IgnoreInterface(Type clrType) => throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> DirectiveAnnotation(string name, object? value)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreDirectiveAnnotation(string name) =>
            throw new NotImplementedException();

        InternalSchemaBuilder IInfrastructure<InternalSchemaBuilder>.Instance => Builder;

        SchemaDefinition IInfrastructure<SchemaDefinition>.Instance => Builder.Definition;

        public IInputObjectTypeBuilder<object> InputObject(string name) =>
            new InputObjectTypeBuilder<object>(Builder.InputObject(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public IInputObjectTypeBuilder<TInput> InputObject<TInput>() =>
            new InputObjectTypeBuilder<TInput>(
                Builder.InputObject(typeof(TInput), ConfigurationSource.Explicit)!);

        public IInputObjectTypeBuilder<object> InputObject(Type clrType) =>
            new InputObjectTypeBuilder<object>(Builder.InputObject(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit)!);

        public ISchemaBuilder<GraphQLContext> DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);

        public ISchemaBuilder<GraphQLContext> DirectiveAnnotation(object directive) =>
            throw new NotImplementedException();

        public ISchemaBuilder<GraphQLContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }


    [DebuggerStepThrough]
    public partial class SchemaBuilder<TContext> : SchemaBuilder, ISchemaBuilder<TContext> where TContext : GraphQLContext

    {
        public SchemaBuilder(SchemaDefinition schemaDefinition) : base(schemaDefinition)
        {
        }


        public new ISchemaBuilder<TContext> UnignoreObject(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreObject<TObject>() where TObject : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreObject(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreObject(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreScalar<TScalar>() where TScalar : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreScalar(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreScalar(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreScalar<TScalar>() where TScalar : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreScalar(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreScalar(string name) => throw new NotImplementedException();

        public new IObjectTypeBuilder<object, TContext> Object(Type clrType) =>
            new ObjectTypeBuilder<object, TContext>(Builder.Object(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit)!);

        public new IObjectTypeBuilder<object, TContext> Object(string name) =>
            new ObjectTypeBuilder<object, TContext>(Builder.Object(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public new IObjectTypeBuilder<TObject, TContext> Object<TObject>() where TObject : notnull =>
            new ObjectTypeBuilder<TObject, TContext>(Builder.Object(typeof(TObject),
                ConfigurationSource.Explicit)!);

        public new ISchemaBuilder<TContext> UnignoreUnion<TUnion>() where TUnion : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreUnion(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreUnion(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreUnion<TUnion>() where TUnion : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreUnion(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreUnion(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreType<TClrType>() where TClrType : notnull =>
            (ISchemaBuilder<TContext>)base.IgnoreType<TClrType>();

        public new ISchemaBuilder<TContext> IgnoreType(Type clrType) =>
            (ISchemaBuilder<TContext>)base.IgnoreType(clrType);

        public new ISchemaBuilder<TContext> IgnoreType(string name) => (ISchemaBuilder<TContext>)base.IgnoreType(name);

        public new ISchemaBuilder<TContext> UnignoreType<TClrType>() where TClrType : notnull =>
            (ISchemaBuilder<TContext>)base.UnignoreType(typeof(TClrType));

        public new ISchemaBuilder<TContext> UnignoreType(Type clrType) =>
            (ISchemaBuilder<TContext>)base.UnignoreType(clrType);

        public new ISchemaBuilder<TContext> UnignoreType(string name) =>
            (ISchemaBuilder<TContext>)base.UnignoreType(name);

        public new ISchemaBuilder<TContext> IgnoreDirective<TDirective>() where TDirective : notnull =>
            (ISchemaBuilder<TContext>)base.IgnoreDirective<TDirective>();

        public new ISchemaBuilder<TContext> IgnoreDirective(Type clrType) =>
            (ISchemaBuilder<TContext>)base.IgnoreDirective(clrType);

        public new ISchemaBuilder<TContext> IgnoreDirective(string name) =>
            (ISchemaBuilder<TContext>)base.IgnoreDirective(name);

        public new ISchemaBuilder<TContext> UnignoreDirective<TDirective>() where TDirective : notnull =>
            (ISchemaBuilder<TContext>)base.UnignoreDirective<TDirective>();

        public new ISchemaBuilder<TContext> UnignoreDirective(Type clrType) =>
            (ISchemaBuilder<TContext>)base.UnignoreDirective(clrType);

        public new ISchemaBuilder<TContext> UnignoreDirective(string name) =>
            (ISchemaBuilder<TContext>)base.UnignoreDirective(name);

        public new IInterfaceTypeBuilder<object, TContext> Interface(string name) =>
            new InterfaceTypeBuilder<object, TContext>(Builder.Interface(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public new ISchemaBuilder<TContext> IgnoreEnum(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreInputObject<TInputObject>() where TInputObject : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreInputObject(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreInputObject(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreInputObject<TInputObject>() where TInputObject : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreInputObject(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreInputObject(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreInterface<TInterface>() where TInterface : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreInterface(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreInterface(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreInterface<TInterface>() where TInterface : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreInterface(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreInterface(string name) => throw new NotImplementedException();

        public new IInterfaceTypeBuilder<object, TContext> Interface(Type clrType) =>
            new InterfaceTypeBuilder<object, TContext>(Builder.Interface(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit
            )!);

        public new IInterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>() where TInterface : notnull =>
            new InterfaceTypeBuilder<TInterface, TContext>(Builder.Interface(typeof(TInterface),
                ConfigurationSource.Explicit)!);

        public new ISchemaBuilder<TContext> UnignoreObject<TObject>() where TObject : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreObject(Type clrType) => throw new NotImplementedException();

        public new IUnionTypeBuilder<object, TContext> Union(string name) =>
            new UnionTypeBuilder<object, TContext>(Builder.Union(Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit)!);

        public new IUnionTypeBuilder<TUnion, TContext> Union<TUnion>() where TUnion : notnull =>
            new UnionTypeBuilder<TUnion, TContext>(Builder.Union(typeof(TUnion), ConfigurationSource.Explicit)!);

        public new IUnionTypeBuilder<object, TContext> Union(Type clrType) =>
            new UnionTypeBuilder<object, TContext>(Builder.Union(Check.NotNull(clrType, nameof(clrType)),
                ConfigurationSource.Explicit)!);

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

        public new ISchemaBuilder<TContext> UnignoreEnum<TEnum>() where TEnum : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreEnum(Type clrType) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> UnignoreEnum(string name) => throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreEnum<TEnum>() where TEnum : notnull =>
            throw new NotImplementedException();

        public new ISchemaBuilder<TContext> IgnoreEnum(Type clrType) => throw new NotImplementedException();


        public new ISchemaBuilder<TContext> DirectiveAnnotation(string name, object? value = null)
        {
            Builder.DirectiveAnnotation(Check.NotNull(name, nameof(name)), value, ConfigurationSource.Explicit);
            return this;
        }

        public new ISchemaBuilder<TContext> IgnoreDirectiveAnnotation(string name) =>
            (ISchemaBuilder<TContext>)base.IgnoreDirectiveAnnotation(name);
    }
}