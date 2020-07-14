// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public partial class MutableSchema
    {
        #region SchemaDefinitionTypeAccessorGenerator

        #region Enum type accessors

        public MutableEnumType GetEnum(string name) => GetType<MutableEnumType>(name);

        public MutableEnumType GetEnum(Type clrType) =>
            GetType<MutableEnumType>(Check.NotNull(clrType, nameof(clrType)));

        public MutableEnumType GetEnum<TClrType>() => GetType<MutableEnumType>(typeof(TClrType));

        public MutableEnumType? FindEnum(string name) => FindType<MutableEnumType>(name);

        public MutableEnumType? FindEnum<TClrType>() => FindType<MutableEnumType>(typeof(TClrType));

        public MutableEnumType? FindEnum(Type clrType) =>
            FindType<MutableEnumType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetEnum(Type clrType, [NotNullWhen(true)] out MutableEnumType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetEnum<TClrType>([NotNullWhen(true)] out MutableEnumType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetEnum(string name, [NotNullWhen(true)] out MutableEnumType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasEnum(Type clrType) => HasType<MutableEnumType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasEnum<TClrType>() => HasType<MutableEnumType>(typeof(TClrType));

        public bool HasEnum(string name) => HasType<MutableEnumType>(Check.NotNull(name, nameof(name)));

        public IEnumerable<MutableEnumType> GetEnums(bool includeSpecEnums = false) => includeSpecEnums
            ? Types.OfType<MutableEnumType>()
            : Types.OfType<MutableEnumType>().Where(_ => !_.IsSpec);

        IEnumerable<IEnumType> IEnumTypesDefinition.GetEnums(bool includeSpecEnums) =>
            GetEnums(includeSpecEnums);

        #endregion

        #region InputObject type accessors

        public MutableInputObjectType GetInputObject(string name) => GetType<MutableInputObjectType>(name);

        public MutableInputObjectType GetInputObject(Type clrType) =>
            GetType<MutableInputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public MutableInputObjectType GetInputObject<TClrType>() =>
            GetType<MutableInputObjectType>(typeof(TClrType));

        public MutableInputObjectType? FindInputObject(string name) => FindType<MutableInputObjectType>(name);

        public MutableInputObjectType? FindInputObject<TClrType>() =>
            FindType<MutableInputObjectType>(typeof(TClrType));

        public MutableInputObjectType? FindInputObject(Type clrType) =>
            FindType<MutableInputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetInputObject(Type clrType, [NotNullWhen(true)] out MutableInputObjectType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetInputObject<TClrType>([NotNullWhen(true)] out MutableInputObjectType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetInputObject(string name, [NotNullWhen(true)] out MutableInputObjectType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasInputObject(Type clrType) =>
            HasType<MutableInputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasInputObject<TClrType>() => HasType<MutableInputObjectType>(typeof(TClrType));

        public bool HasInputObject(string name) =>
            HasType<MutableInputObjectType>(Check.NotNull(name, nameof(name)));

        public IEnumerable<MutableInputObjectType> GetInputObjects(bool includeSpecInputObjects = false) =>
            includeSpecInputObjects
                ? Types.OfType<MutableInputObjectType>()
                : Types.OfType<MutableInputObjectType>().Where(_ => !_.IsSpec);

        IEnumerable<IInputObjectType> IInputObjectTypesDefinition.
            GetInputObjects(bool includeSpecInputObjects) => GetInputObjects(includeSpecInputObjects);

        #endregion

        #region Interface type accessors

        public MutableInterfaceType GetInterface(string name) => GetType<MutableInterfaceType>(name);

        public MutableInterfaceType GetInterface(Type clrType) =>
            GetType<MutableInterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public MutableInterfaceType GetInterface<TClrType>() => GetType<MutableInterfaceType>(typeof(TClrType));

        public MutableInterfaceType? FindInterface(string name) => FindType<MutableInterfaceType>(name);

        public MutableInterfaceType? FindInterface<TClrType>() =>
            FindType<MutableInterfaceType>(typeof(TClrType));

        public MutableInterfaceType? FindInterface(Type clrType) =>
            FindType<MutableInterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetInterface(Type clrType, [NotNullWhen(true)] out MutableInterfaceType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetInterface<TClrType>([NotNullWhen(true)] out MutableInterfaceType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetInterface(string name, [NotNullWhen(true)] out MutableInterfaceType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasInterface(Type clrType) =>
            HasType<MutableInterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasInterface<TClrType>() => HasType<MutableInterfaceType>(typeof(TClrType));

        public bool HasInterface(string name) => HasType<MutableInterfaceType>(Check.NotNull(name, nameof(name)));

        public IEnumerable<MutableInterfaceType> GetInterfaces(bool includeSpecInterfaces = false) =>
            includeSpecInterfaces
                ? Types.OfType<MutableInterfaceType>()
                : Types.OfType<MutableInterfaceType>().Where(_ => !_.IsSpec);

        IEnumerable<IInterfaceType> IInterfaceTypesDefinition.GetInterfaces(bool includeSpecInterfaces) =>
            GetInterfaces(includeSpecInterfaces);

        #endregion

        #region Object type accessors

        public MutableObjectType GetObject(string name) => GetType<MutableObjectType>(name);

        public MutableObjectType GetObject(Type clrType) =>
            GetType<MutableObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public MutableObjectType GetObject<TClrType>() => GetType<MutableObjectType>(typeof(TClrType));

        public MutableObjectType? FindObject(string name) => FindType<MutableObjectType>(name);

        public MutableObjectType? FindObject<TClrType>() => FindType<MutableObjectType>(typeof(TClrType));

        public MutableObjectType? FindObject(Type clrType) =>
            FindType<MutableObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetObject(Type clrType, [NotNullWhen(true)] out MutableObjectType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetObject<TClrType>([NotNullWhen(true)] out MutableObjectType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetObject(string name, [NotNullWhen(true)] out MutableObjectType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasObject(Type clrType) => HasType<MutableObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasObject<TClrType>() => HasType<MutableObjectType>(typeof(TClrType));

        public bool HasObject(string name) => HasType<MutableObjectType>(Check.NotNull(name, nameof(name)));

        public IEnumerable<MutableObjectType> GetObjects(bool includeSpecObjects = false) => includeSpecObjects
            ? Types.OfType<MutableObjectType>()
            : Types.OfType<MutableObjectType>().Where(_ => !_.IsSpec);

        IEnumerable<IObjectType> IObjectTypesDefinition.GetObjects(bool includeSpecObjects) =>
            GetObjects(includeSpecObjects);

        #endregion

        #region Scalar type accessors

        public MutableScalarType GetScalar(string name) => GetType<MutableScalarType>(name);

        public MutableScalarType GetScalar(Type clrType) =>
            GetType<MutableScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public MutableScalarType GetScalar<TClrType>() => GetType<MutableScalarType>(typeof(TClrType));

        public MutableScalarType? FindScalar(string name) => FindType<MutableScalarType>(name);

        public MutableScalarType? FindScalar<TClrType>() => FindType<MutableScalarType>(typeof(TClrType));

        public MutableScalarType? FindScalar(Type clrType) =>
            FindType<MutableScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetScalar(Type clrType, [NotNullWhen(true)] out MutableScalarType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetScalar<TClrType>([NotNullWhen(true)] out MutableScalarType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetScalar(string name, [NotNullWhen(true)] out MutableScalarType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasScalar(Type clrType) => HasType<MutableScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasScalar<TClrType>() => HasType<MutableScalarType>(typeof(TClrType));

        public bool HasScalar(string name) => HasType<MutableScalarType>(Check.NotNull(name, nameof(name)));

        public IEnumerable<MutableScalarType> GetScalars(bool includeSpecScalars = false) => includeSpecScalars
            ? Types.OfType<MutableScalarType>()
            : Types.OfType<MutableScalarType>().Where(_ => !_.IsSpec);

        IEnumerable<IScalarType> IScalarTypesDefinition.GetScalars(bool includeSpecScalars) =>
            GetScalars(includeSpecScalars);

        #endregion

        #region Union type accessors

        public MutableUnionType GetUnion(string name) => GetType<MutableUnionType>(name);

        public MutableUnionType GetUnion(Type clrType) =>
            GetType<MutableUnionType>(Check.NotNull(clrType, nameof(clrType)));

        public MutableUnionType GetUnion<TClrType>() => GetType<MutableUnionType>(typeof(TClrType));

        public MutableUnionType? FindUnion(string name) => FindType<MutableUnionType>(name);

        public MutableUnionType? FindUnion<TClrType>() => FindType<MutableUnionType>(typeof(TClrType));

        public MutableUnionType? FindUnion(Type clrType) =>
            FindType<MutableUnionType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetUnion(Type clrType, [NotNullWhen(true)] out MutableUnionType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetUnion<TClrType>([NotNullWhen(true)] out MutableUnionType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetUnion(string name, [NotNullWhen(true)] out MutableUnionType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasUnion(Type clrType) => HasType<MutableUnionType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasUnion<TClrType>() => HasType<MutableUnionType>(typeof(TClrType));

        public bool HasUnion(string name) => HasType<MutableUnionType>(Check.NotNull(name, nameof(name)));

        public IEnumerable<MutableUnionType> GetUnions(bool includeSpecUnions = false) => includeSpecUnions
            ? Types.OfType<MutableUnionType>()
            : Types.OfType<MutableUnionType>().Where(_ => !_.IsSpec);

        IEnumerable<IUnionType> IUnionTypesDefinition.GetUnions(bool includeSpecUnions) =>
            GetUnions(includeSpecUnions);

        #endregion

        #endregion

        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public MutableDirectiveDefinition? FindDirectiveDefinition(string name)
            => _directives.TryGetValue(Check.NotNull(name, nameof(name)), out var directive) ? directive : null;

        [GraphQLIgnore]
        public bool HasDirective(string name)
            => _directives.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public MutableDirectiveDefinition GetDirective(string name)
            => FindDirectiveDefinition(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException(
                   $"{this} does not contain a {nameof(MutableDirectiveDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetDirectiveDefinition(string name, [NotNullWhen(true)] out MutableDirectiveDefinition? directiveDefinition)
            => _directives.TryGetValue(Check.NotNull(name, nameof(name)), out directiveDefinition);

        #endregion

        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public TypeIdentity? FindTypeIdentity(string name)
            => _typeIdentities.TryGetValue(Check.NotNull(name, nameof(name)), out var typeIdentity)
                ? typeIdentity
                : null;

        [GraphQLIgnore]
        public bool HasTypeIdentity(string name)
            => _typeIdentities.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public TypeIdentity GetTypeIdentity(string name)
            => FindTypeIdentity(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException($"{this} does not contain a {nameof(TypeIdentity)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetTypeIdentity(string name, [NotNullWhen(true)] out TypeIdentity? typeIdentity)
            => _typeIdentities.TryGetValue(Check.NotNull(name, nameof(name)), out typeIdentity);

        #endregion
    }
}
// Source Hash Code: 17037223998941350511