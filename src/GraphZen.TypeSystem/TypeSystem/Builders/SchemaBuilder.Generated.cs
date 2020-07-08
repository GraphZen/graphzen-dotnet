// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public partial class SchemaBuilder<TContext>
    {
        #region SchemaBuilderGenerator

        #region Directives

        public IEnumerable<DirectiveBuilder> GetDirectives(bool includeSpecDirectives = false) =>
            Builder.Definition.GetDirectives(includeSpecDirectives).Select(_ => new DirectiveBuilder(_.InternalBuilder));


        public DirectiveBuilder<object> Directive(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Directive(name, ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<object>(internalBuilder);
            return builder;
        }


        public DirectiveBuilder<TDirective> Directive<TDirective>() where TDirective : notnull
        {
            var internalBuilder = Builder.Directive(typeof(TDirective), ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<TDirective>(internalBuilder);
            return builder;
        }

        public DirectiveBuilder<TDirective> Directive<TDirective>(string name) where TDirective : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Directive(typeof(TDirective), name, ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<TDirective>(internalBuilder);
            return builder;
        }


        public DirectiveBuilder<object> Directive(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Directive(clrType, ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<object>(internalBuilder);
            return builder;
        }

        public DirectiveBuilder<object> Directive(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Directive(clrType, name, ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<object>(internalBuilder);
            return builder;
        }


        public SchemaBuilder<TContext> UnignoreDirective<TDirective>() where TDirective : notnull
        {
            Builder.UnignoreDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreDirective(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreDirective<TDirective>() where TDirective : notnull
        {
            Builder.IgnoreDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreDirective(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveDirective<TDirective>() where TDirective : notnull
        {
            Builder.RemoveDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirective(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Types

        public SchemaBuilder<TContext> UnignoreType<TClrType>() where TClrType : notnull
        {
            Builder.UnignoreType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreType(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreType(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreType<TClrType>() where TClrType : notnull
        {
            Builder.IgnoreType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreType(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreType(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveType<TClrType>() where TClrType : notnull
        {
            Builder.RemoveType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveType(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveType(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Objects

        public IEnumerable<ObjectTypeBuilder> GetObjects(bool includeSpecObjects = false) =>
            Builder.Definition.GetObjects(includeSpecObjects).Select(_ => new ObjectTypeBuilder(_.InternalBuilder));


        public ObjectTypeBuilder<object, TContext> Object(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Object(name, ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public ObjectTypeBuilder<TObject, TContext> Object<TObject>() where TObject : notnull
        {
            var internalBuilder = Builder.Object(typeof(TObject), ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<TObject, TContext>(internalBuilder);
            return builder;
        }

        public ObjectTypeBuilder<TObject, TContext> Object<TObject>(string name) where TObject : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Object(typeof(TObject), name, ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<TObject, TContext>(internalBuilder);
            return builder;
        }

        public ObjectTypeBuilder<object, TContext> Object(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Object(clrType, ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public ObjectTypeBuilder<object, TContext> Object(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Object(clrType, name, ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }


        public SchemaBuilder<TContext> UnignoreObject<TObject>() where TObject : notnull
        {
            Builder.UnignoreObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreObject(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreObject<TObject>() where TObject : notnull
        {
            Builder.IgnoreObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreObject(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveObject<TObject>() where TObject : notnull
        {
            Builder.RemoveObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveObject(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Unions

        public IEnumerable<UnionTypeBuilder> GetUnions(bool includeSpecUnions = false) =>
            Builder.Definition.GetUnions(includeSpecUnions).Select(_ => new UnionTypeBuilder(_.InternalBuilder));


        public UnionTypeBuilder<object, TContext> Union(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Union(name, ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public UnionTypeBuilder<TUnion, TContext> Union<TUnion>() where TUnion : notnull
        {
            var internalBuilder = Builder.Union(typeof(TUnion), ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<TUnion, TContext>(internalBuilder);
            return builder;
        }

        public UnionTypeBuilder<TUnion, TContext> Union<TUnion>(string name) where TUnion : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Union(typeof(TUnion), name, ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<TUnion, TContext>(internalBuilder);
            return builder;
        }

        public UnionTypeBuilder<object, TContext> Union(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Union(clrType, ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public UnionTypeBuilder<object, TContext> Union(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Union(clrType, name, ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }


        public SchemaBuilder<TContext> UnignoreUnion<TUnion>() where TUnion : notnull
        {
            Builder.UnignoreUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreUnion(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreUnion<TUnion>() where TUnion : notnull
        {
            Builder.IgnoreUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreUnion(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveUnion<TUnion>() where TUnion : notnull
        {
            Builder.RemoveUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveUnion(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Scalars

        public IEnumerable<ScalarTypeBuilder> GetScalars(bool includeSpecScalars = false) =>
            Builder.Definition.GetScalars(includeSpecScalars).Select(_ => new ScalarTypeBuilder(_.InternalBuilder));


        public SchemaBuilder<TContext> UnignoreScalar<TScalar>() where TScalar : notnull
        {
            Builder.UnignoreScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreScalar(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreScalar<TScalar>() where TScalar : notnull
        {
            Builder.IgnoreScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreScalar(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveScalar<TScalar>() where TScalar : notnull
        {
            Builder.RemoveScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveScalar(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Enums

        public IEnumerable<EnumTypeBuilder> GetEnums(bool includeSpecEnums = false) =>
            Builder.Definition.GetEnums(includeSpecEnums).Select(_ => new EnumTypeBuilder(_.InternalBuilder));


        public EnumTypeBuilder<string> Enum(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Enum(name, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<string>(internalBuilder);
            return builder;
        }


        public EnumTypeBuilder<TEnum> Enum<TEnum>() where TEnum : notnull
        {
            var internalBuilder = Builder.Enum(typeof(TEnum), ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<TEnum>(internalBuilder);
            return builder;
        }

        public EnumTypeBuilder<TEnum> Enum<TEnum>(string name) where TEnum : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Enum(typeof(TEnum), name, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<TEnum>(internalBuilder);
            return builder;
        }


        public EnumTypeBuilder<string> Enum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Enum(clrType, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<string>(internalBuilder);
            return builder;
        }

        public EnumTypeBuilder<string> Enum(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Enum(clrType, name, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<string>(internalBuilder);
            return builder;
        }


        public SchemaBuilder<TContext> UnignoreEnum<TEnum>() where TEnum : notnull
        {
            Builder.UnignoreEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreEnum(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreEnum<TEnum>() where TEnum : notnull
        {
            Builder.IgnoreEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreEnum(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveEnum<TEnum>() where TEnum : notnull
        {
            Builder.RemoveEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveEnum(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Interfaces

        public IEnumerable<InterfaceTypeBuilder> GetInterfaces(bool includeSpecInterfaces = false) =>
            Builder.Definition.GetInterfaces(includeSpecInterfaces).Select(_ => _.Builder);


        public InterfaceTypeBuilder<object, TContext> Interface(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Interface(name, ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public InterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>() where TInterface : notnull
        {
            var internalBuilder = Builder.Interface(typeof(TInterface), ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<TInterface, TContext>(internalBuilder);
            return builder;
        }

        public InterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>(string name) where TInterface : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Interface(typeof(TInterface), name, ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<TInterface, TContext>(internalBuilder);
            return builder;
        }

        public InterfaceTypeBuilder<object, TContext> Interface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Interface(clrType, ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public InterfaceTypeBuilder<object, TContext> Interface(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Interface(clrType, name, ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }


        public SchemaBuilder<TContext> UnignoreInterface<TInterface>() where TInterface : notnull
        {
            Builder.UnignoreInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreInterface<TInterface>() where TInterface : notnull
        {
            Builder.IgnoreInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveInterface<TInterface>() where TInterface : notnull
        {
            Builder.RemoveInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region InputObjects

        public IEnumerable<InputObjectTypeBuilder> GetInputObjects(bool includeSpecInputObjects = false) =>
            Builder.Definition.GetInputObjects(includeSpecInputObjects)
                .Select(_ => new InputObjectTypeBuilder(_.InternalBuilder));


        public InputObjectTypeBuilder<object> InputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.InputObject(name, ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<object>(internalBuilder);
            return builder;
        }


        public InputObjectTypeBuilder<TInputObject> InputObject<TInputObject>() where TInputObject : notnull
        {
            var internalBuilder = Builder.InputObject(typeof(TInputObject), ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<TInputObject>(internalBuilder);
            return builder;
        }

        public InputObjectTypeBuilder<TInputObject> InputObject<TInputObject>(string name) where TInputObject : notnull
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.InputObject(typeof(TInputObject), name, ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<TInputObject>(internalBuilder);
            return builder;
        }


        public InputObjectTypeBuilder<object> InputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.InputObject(clrType, ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<object>(internalBuilder);
            return builder;
        }

        public InputObjectTypeBuilder<object> InputObject(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.InputObject(clrType, name, ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<object>(internalBuilder);
            return builder;
        }


        public SchemaBuilder<TContext> UnignoreInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.UnignoreInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> UnignoreInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreInputObject(name, ConfigurationSource.Explicit);
            return this;
        }


        public SchemaBuilder<TContext> IgnoreInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.IgnoreInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> IgnoreInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreInputObject(name, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.RemoveInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public SchemaBuilder<TContext> RemoveInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveInputObject(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #endregion
    }
}
// Source Hash Code: 7923490702767696174