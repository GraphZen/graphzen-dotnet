#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem {
public partial class Schema {
#region Enum type accessors

        public EnumType GetEnum(string name) => GetType<EnumType>(name);

        public EnumType GetEnum(Type clrType) => GetType<EnumType>(Check.NotNull(clrType, nameof(clrType)));
        
        public EnumType GetEnum<TClrType>() => GetType<EnumType>(typeof(TClrType));

        public EnumType? FindEnum(string name) => FindType<EnumType>(name);

        public EnumType? FindEnum<TClrType>() => FindType<EnumType>(typeof(TClrType));

        public EnumType? FindEnum(Type clrType) => FindType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetEnum(Type clrType, [NotNullWhen(true)] out EnumType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetEnum<TClrType>([NotNullWhen(true)] out EnumType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetEnum(string name, [NotNullWhen(true)] out EnumType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasEnum(Type clrType) => HasType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasEnum<TClrType>() => HasType<EnumType>(typeof(TClrType));

        public bool HasEnum(string name) => HasType<EnumType>(Check.NotNull(name, nameof(name)));


#endregion
#region InputObject type accessors

        public InputObjectType GetInputObject(string name) => GetType<InputObjectType>(name);

        public InputObjectType GetInputObject(Type clrType) => GetType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));
        
        public InputObjectType GetInputObject<TClrType>() => GetType<InputObjectType>(typeof(TClrType));

        public InputObjectType? FindInputObject(string name) => FindType<InputObjectType>(name);

        public InputObjectType? FindInputObject<TClrType>() => FindType<InputObjectType>(typeof(TClrType));

        public InputObjectType? FindInputObject(Type clrType) => FindType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetInputObject(Type clrType, [NotNullWhen(true)] out InputObjectType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetInputObject<TClrType>([NotNullWhen(true)] out InputObjectType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetInputObject(string name, [NotNullWhen(true)] out InputObjectType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasInputObject(Type clrType) => HasType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasInputObject<TClrType>() => HasType<InputObjectType>(typeof(TClrType));

        public bool HasInputObject(string name) => HasType<InputObjectType>(Check.NotNull(name, nameof(name)));


#endregion
#region Interface type accessors

        public InterfaceType GetInterface(string name) => GetType<InterfaceType>(name);

        public InterfaceType GetInterface(Type clrType) => GetType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));
        
        public InterfaceType GetInterface<TClrType>() => GetType<InterfaceType>(typeof(TClrType));

        public InterfaceType? FindInterface(string name) => FindType<InterfaceType>(name);

        public InterfaceType? FindInterface<TClrType>() => FindType<InterfaceType>(typeof(TClrType));

        public InterfaceType? FindInterface(Type clrType) => FindType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetInterface(Type clrType, [NotNullWhen(true)] out InterfaceType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetInterface<TClrType>([NotNullWhen(true)] out InterfaceType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetInterface(string name, [NotNullWhen(true)] out InterfaceType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasInterface(Type clrType) => HasType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasInterface<TClrType>() => HasType<InterfaceType>(typeof(TClrType));

        public bool HasInterface(string name) => HasType<InterfaceType>(Check.NotNull(name, nameof(name)));


#endregion
#region Object type accessors

        public ObjectType GetObject(string name) => GetType<ObjectType>(name);

        public ObjectType GetObject(Type clrType) => GetType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));
        
        public ObjectType GetObject<TClrType>() => GetType<ObjectType>(typeof(TClrType));

        public ObjectType? FindObject(string name) => FindType<ObjectType>(name);

        public ObjectType? FindObject<TClrType>() => FindType<ObjectType>(typeof(TClrType));

        public ObjectType? FindObject(Type clrType) => FindType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetObject(Type clrType, [NotNullWhen(true)] out ObjectType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetObject<TClrType>([NotNullWhen(true)] out ObjectType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetObject(string name, [NotNullWhen(true)] out ObjectType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasObject(Type clrType) => HasType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasObject<TClrType>() => HasType<ObjectType>(typeof(TClrType));

        public bool HasObject(string name) => HasType<ObjectType>(Check.NotNull(name, nameof(name)));


#endregion
#region Scalar type accessors

        public ScalarType GetScalar(string name) => GetType<ScalarType>(name);

        public ScalarType GetScalar(Type clrType) => GetType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));
        
        public ScalarType GetScalar<TClrType>() => GetType<ScalarType>(typeof(TClrType));

        public ScalarType? FindScalar(string name) => FindType<ScalarType>(name);

        public ScalarType? FindScalar<TClrType>() => FindType<ScalarType>(typeof(TClrType));

        public ScalarType? FindScalar(Type clrType) => FindType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetScalar(Type clrType, [NotNullWhen(true)] out ScalarType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetScalar<TClrType>([NotNullWhen(true)] out ScalarType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetScalar(string name, [NotNullWhen(true)] out ScalarType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasScalar(Type clrType) => HasType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasScalar<TClrType>() => HasType<ScalarType>(typeof(TClrType));

        public bool HasScalar(string name) => HasType<ScalarType>(Check.NotNull(name, nameof(name)));


#endregion
#region Union type accessors

        public UnionType GetUnion(string name) => GetType<UnionType>(name);

        public UnionType GetUnion(Type clrType) => GetType<UnionType>(Check.NotNull(clrType, nameof(clrType)));
        
        public UnionType GetUnion<TClrType>() => GetType<UnionType>(typeof(TClrType));

        public UnionType? FindUnion(string name) => FindType<UnionType>(name);

        public UnionType? FindUnion<TClrType>() => FindType<UnionType>(typeof(TClrType));

        public UnionType? FindUnion(Type clrType) => FindType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetUnion(Type clrType, [NotNullWhen(true)] out UnionType? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetUnion<TClrType>([NotNullWhen(true)] out UnionType? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetUnion(string name, [NotNullWhen(true)] out UnionType? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasUnion(Type clrType) => HasType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasUnion<TClrType>() => HasType<UnionType>(typeof(TClrType));

        public bool HasUnion(string name) => HasType<UnionType>(Check.NotNull(name, nameof(name)));


#endregion
}
}
