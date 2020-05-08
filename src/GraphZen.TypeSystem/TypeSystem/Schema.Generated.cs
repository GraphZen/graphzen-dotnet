#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem {
public  partial class Schema {
#region Enum type accessors

[GraphQLIgnore]        
public EnumType GetEnum(string name) => GetType<EnumType>(name);

[GraphQLIgnore]        
        public EnumType GetEnum(Type clrType) => GetType<EnumType>(Check.NotNull(clrType, nameof(clrType)));
        
[GraphQLIgnore]        
        public EnumType GetEnum<TClrType>() => GetType<EnumType>(typeof(TClrType));

[GraphQLIgnore]        
        public EnumType? FindEnum(string name) => FindType<EnumType>(name);

[GraphQLIgnore]        
        public EnumType? FindEnum<TClrType>() => FindType<EnumType>(typeof(TClrType));

[GraphQLIgnore]        
        public EnumType? FindEnum(Type clrType) => FindType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool TryGetEnum(Type clrType, [NotNullWhen(true)] out EnumType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

[GraphQLIgnore]        
        public bool TryGetEnum<TClrType>([NotNullWhen(true)] out EnumType? type) =>
            TryGetType(typeof(TClrType), out type);

[GraphQLIgnore]        
        public bool TryGetEnum(string name, [NotNullWhen(true)] out EnumType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

[GraphQLIgnore]        
        public bool HasEnum(Type clrType) => HasType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool HasEnum<TClrType>() => HasType<EnumType>(typeof(TClrType));

[GraphQLIgnore]        
        public bool HasEnum(string name) => HasType<EnumType>(Check.NotNull(name, nameof(name)));


#endregion
#region InputObject type accessors

[GraphQLIgnore]        
public InputObjectType GetInputObject(string name) => GetType<InputObjectType>(name);

[GraphQLIgnore]        
        public InputObjectType GetInputObject(Type clrType) => GetType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));
        
[GraphQLIgnore]        
        public InputObjectType GetInputObject<TClrType>() => GetType<InputObjectType>(typeof(TClrType));

[GraphQLIgnore]        
        public InputObjectType? FindInputObject(string name) => FindType<InputObjectType>(name);

[GraphQLIgnore]        
        public InputObjectType? FindInputObject<TClrType>() => FindType<InputObjectType>(typeof(TClrType));

[GraphQLIgnore]        
        public InputObjectType? FindInputObject(Type clrType) => FindType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool TryGetInputObject(Type clrType, [NotNullWhen(true)] out InputObjectType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

[GraphQLIgnore]        
        public bool TryGetInputObject<TClrType>([NotNullWhen(true)] out InputObjectType? type) =>
            TryGetType(typeof(TClrType), out type);

[GraphQLIgnore]        
        public bool TryGetInputObject(string name, [NotNullWhen(true)] out InputObjectType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

[GraphQLIgnore]        
        public bool HasInputObject(Type clrType) => HasType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool HasInputObject<TClrType>() => HasType<InputObjectType>(typeof(TClrType));

[GraphQLIgnore]        
        public bool HasInputObject(string name) => HasType<InputObjectType>(Check.NotNull(name, nameof(name)));


#endregion
#region Interface type accessors

[GraphQLIgnore]        
public InterfaceType GetInterface(string name) => GetType<InterfaceType>(name);

[GraphQLIgnore]        
        public InterfaceType GetInterface(Type clrType) => GetType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));
        
[GraphQLIgnore]        
        public InterfaceType GetInterface<TClrType>() => GetType<InterfaceType>(typeof(TClrType));

[GraphQLIgnore]        
        public InterfaceType? FindInterface(string name) => FindType<InterfaceType>(name);

[GraphQLIgnore]        
        public InterfaceType? FindInterface<TClrType>() => FindType<InterfaceType>(typeof(TClrType));

[GraphQLIgnore]        
        public InterfaceType? FindInterface(Type clrType) => FindType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool TryGetInterface(Type clrType, [NotNullWhen(true)] out InterfaceType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

[GraphQLIgnore]        
        public bool TryGetInterface<TClrType>([NotNullWhen(true)] out InterfaceType? type) =>
            TryGetType(typeof(TClrType), out type);

[GraphQLIgnore]        
        public bool TryGetInterface(string name, [NotNullWhen(true)] out InterfaceType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

[GraphQLIgnore]        
        public bool HasInterface(Type clrType) => HasType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool HasInterface<TClrType>() => HasType<InterfaceType>(typeof(TClrType));

[GraphQLIgnore]        
        public bool HasInterface(string name) => HasType<InterfaceType>(Check.NotNull(name, nameof(name)));


#endregion
#region Object type accessors

[GraphQLIgnore]        
public ObjectType GetObject(string name) => GetType<ObjectType>(name);

[GraphQLIgnore]        
        public ObjectType GetObject(Type clrType) => GetType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));
        
[GraphQLIgnore]        
        public ObjectType GetObject<TClrType>() => GetType<ObjectType>(typeof(TClrType));

[GraphQLIgnore]        
        public ObjectType? FindObject(string name) => FindType<ObjectType>(name);

[GraphQLIgnore]        
        public ObjectType? FindObject<TClrType>() => FindType<ObjectType>(typeof(TClrType));

[GraphQLIgnore]        
        public ObjectType? FindObject(Type clrType) => FindType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool TryGetObject(Type clrType, [NotNullWhen(true)] out ObjectType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

[GraphQLIgnore]        
        public bool TryGetObject<TClrType>([NotNullWhen(true)] out ObjectType? type) =>
            TryGetType(typeof(TClrType), out type);

[GraphQLIgnore]        
        public bool TryGetObject(string name, [NotNullWhen(true)] out ObjectType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

[GraphQLIgnore]        
        public bool HasObject(Type clrType) => HasType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool HasObject<TClrType>() => HasType<ObjectType>(typeof(TClrType));

[GraphQLIgnore]        
        public bool HasObject(string name) => HasType<ObjectType>(Check.NotNull(name, nameof(name)));


#endregion
#region Scalar type accessors

[GraphQLIgnore]        
public ScalarType GetScalar(string name) => GetType<ScalarType>(name);

[GraphQLIgnore]        
        public ScalarType GetScalar(Type clrType) => GetType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));
        
[GraphQLIgnore]        
        public ScalarType GetScalar<TClrType>() => GetType<ScalarType>(typeof(TClrType));

[GraphQLIgnore]        
        public ScalarType? FindScalar(string name) => FindType<ScalarType>(name);

[GraphQLIgnore]        
        public ScalarType? FindScalar<TClrType>() => FindType<ScalarType>(typeof(TClrType));

[GraphQLIgnore]        
        public ScalarType? FindScalar(Type clrType) => FindType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool TryGetScalar(Type clrType, [NotNullWhen(true)] out ScalarType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

[GraphQLIgnore]        
        public bool TryGetScalar<TClrType>([NotNullWhen(true)] out ScalarType? type) =>
            TryGetType(typeof(TClrType), out type);

[GraphQLIgnore]        
        public bool TryGetScalar(string name, [NotNullWhen(true)] out ScalarType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

[GraphQLIgnore]        
        public bool HasScalar(Type clrType) => HasType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool HasScalar<TClrType>() => HasType<ScalarType>(typeof(TClrType));

[GraphQLIgnore]        
        public bool HasScalar(string name) => HasType<ScalarType>(Check.NotNull(name, nameof(name)));


#endregion
#region Union type accessors

[GraphQLIgnore]        
public UnionType GetUnion(string name) => GetType<UnionType>(name);

[GraphQLIgnore]        
        public UnionType GetUnion(Type clrType) => GetType<UnionType>(Check.NotNull(clrType, nameof(clrType)));
        
[GraphQLIgnore]        
        public UnionType GetUnion<TClrType>() => GetType<UnionType>(typeof(TClrType));

[GraphQLIgnore]        
        public UnionType? FindUnion(string name) => FindType<UnionType>(name);

[GraphQLIgnore]        
        public UnionType? FindUnion<TClrType>() => FindType<UnionType>(typeof(TClrType));

[GraphQLIgnore]        
        public UnionType? FindUnion(Type clrType) => FindType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool TryGetUnion(Type clrType, [NotNullWhen(true)] out UnionType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

[GraphQLIgnore]        
        public bool TryGetUnion<TClrType>([NotNullWhen(true)] out UnionType? type) =>
            TryGetType(typeof(TClrType), out type);

[GraphQLIgnore]        
        public bool TryGetUnion(string name, [NotNullWhen(true)] out UnionType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

[GraphQLIgnore]        
        public bool HasUnion(Type clrType) => HasType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool HasUnion<TClrType>() => HasType<UnionType>(typeof(TClrType));

[GraphQLIgnore]        
        public bool HasUnion(string name) => HasType<UnionType>(Check.NotNull(name, nameof(name)));


#endregion
}
}
// Source Hash Code: 1863251513