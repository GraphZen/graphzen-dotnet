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
public  partial class SchemaDefinition {
#region Enum type accessors

        public EnumTypeDefinition GetEnum(string name) => GetType<EnumTypeDefinition>(name);

        public EnumTypeDefinition GetEnum(Type clrType) =>
                GetType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public EnumTypeDefinition GetEnum<TClrType>() => GetType<EnumTypeDefinition>(typeof(TClrType));

        public EnumTypeDefinition? FindEnum(string name) => FindType<EnumTypeDefinition>(name);

        public EnumTypeDefinition? FindEnum<TClrType>() => FindType<EnumTypeDefinition>(typeof(TClrType));

        public EnumTypeDefinition? FindEnum(Type clrType) => 
            FindType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetEnum(Type clrType, out EnumTypeDefinition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetEnum<TClrType>(out EnumTypeDefinition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetEnum(string name, out EnumTypeDefinition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasEnum(Type clrType) => HasType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasEnum<TClrType>() => HasType<EnumTypeDefinition>(typeof(TClrType));

        public bool HasEnum(string name) => HasType<EnumTypeDefinition>(Check.NotNull(name, nameof(name)));

#endregion
#region InputObject type accessors

        public InputObjectTypeDefinition GetInputObject(string name) => GetType<InputObjectTypeDefinition>(name);

        public InputObjectTypeDefinition GetInputObject(Type clrType) =>
                GetType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public InputObjectTypeDefinition GetInputObject<TClrType>() => GetType<InputObjectTypeDefinition>(typeof(TClrType));

        public InputObjectTypeDefinition? FindInputObject(string name) => FindType<InputObjectTypeDefinition>(name);

        public InputObjectTypeDefinition? FindInputObject<TClrType>() => FindType<InputObjectTypeDefinition>(typeof(TClrType));

        public InputObjectTypeDefinition? FindInputObject(Type clrType) => 
            FindType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetInputObject(Type clrType, out InputObjectTypeDefinition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetInputObject<TClrType>(out InputObjectTypeDefinition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetInputObject(string name, out InputObjectTypeDefinition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasInputObject(Type clrType) => HasType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasInputObject<TClrType>() => HasType<InputObjectTypeDefinition>(typeof(TClrType));

        public bool HasInputObject(string name) => HasType<InputObjectTypeDefinition>(Check.NotNull(name, nameof(name)));

#endregion
#region Interface type accessors

        public InterfaceTypeDefinition GetInterface(string name) => GetType<InterfaceTypeDefinition>(name);

        public InterfaceTypeDefinition GetInterface(Type clrType) =>
                GetType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public InterfaceTypeDefinition GetInterface<TClrType>() => GetType<InterfaceTypeDefinition>(typeof(TClrType));

        public InterfaceTypeDefinition? FindInterface(string name) => FindType<InterfaceTypeDefinition>(name);

        public InterfaceTypeDefinition? FindInterface<TClrType>() => FindType<InterfaceTypeDefinition>(typeof(TClrType));

        public InterfaceTypeDefinition? FindInterface(Type clrType) => 
            FindType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetInterface(Type clrType, out InterfaceTypeDefinition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetInterface<TClrType>(out InterfaceTypeDefinition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetInterface(string name, out InterfaceTypeDefinition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasInterface(Type clrType) => HasType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasInterface<TClrType>() => HasType<InterfaceTypeDefinition>(typeof(TClrType));

        public bool HasInterface(string name) => HasType<InterfaceTypeDefinition>(Check.NotNull(name, nameof(name)));

#endregion
#region Object type accessors

        public ObjectTypeDefinition GetObject(string name) => GetType<ObjectTypeDefinition>(name);

        public ObjectTypeDefinition GetObject(Type clrType) =>
                GetType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public ObjectTypeDefinition GetObject<TClrType>() => GetType<ObjectTypeDefinition>(typeof(TClrType));

        public ObjectTypeDefinition? FindObject(string name) => FindType<ObjectTypeDefinition>(name);

        public ObjectTypeDefinition? FindObject<TClrType>() => FindType<ObjectTypeDefinition>(typeof(TClrType));

        public ObjectTypeDefinition? FindObject(Type clrType) => 
            FindType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetObject(Type clrType, out ObjectTypeDefinition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetObject<TClrType>(out ObjectTypeDefinition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetObject(string name, out ObjectTypeDefinition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasObject(Type clrType) => HasType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasObject<TClrType>() => HasType<ObjectTypeDefinition>(typeof(TClrType));

        public bool HasObject(string name) => HasType<ObjectTypeDefinition>(Check.NotNull(name, nameof(name)));

#endregion
#region Scalar type accessors

        public ScalarTypeDefinition GetScalar(string name) => GetType<ScalarTypeDefinition>(name);

        public ScalarTypeDefinition GetScalar(Type clrType) =>
                GetType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public ScalarTypeDefinition GetScalar<TClrType>() => GetType<ScalarTypeDefinition>(typeof(TClrType));

        public ScalarTypeDefinition? FindScalar(string name) => FindType<ScalarTypeDefinition>(name);

        public ScalarTypeDefinition? FindScalar<TClrType>() => FindType<ScalarTypeDefinition>(typeof(TClrType));

        public ScalarTypeDefinition? FindScalar(Type clrType) => 
            FindType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetScalar(Type clrType, out ScalarTypeDefinition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetScalar<TClrType>(out ScalarTypeDefinition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetScalar(string name, out ScalarTypeDefinition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasScalar(Type clrType) => HasType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasScalar<TClrType>() => HasType<ScalarTypeDefinition>(typeof(TClrType));

        public bool HasScalar(string name) => HasType<ScalarTypeDefinition>(Check.NotNull(name, nameof(name)));

#endregion
#region Union type accessors

        public UnionTypeDefinition GetUnion(string name) => GetType<UnionTypeDefinition>(name);

        public UnionTypeDefinition GetUnion(Type clrType) =>
                GetType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public UnionTypeDefinition GetUnion<TClrType>() => GetType<UnionTypeDefinition>(typeof(TClrType));

        public UnionTypeDefinition? FindUnion(string name) => FindType<UnionTypeDefinition>(name);

        public UnionTypeDefinition? FindUnion<TClrType>() => FindType<UnionTypeDefinition>(typeof(TClrType));

        public UnionTypeDefinition? FindUnion(Type clrType) => 
            FindType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGetUnion(Type clrType, out UnionTypeDefinition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGetUnion<TClrType>(out UnionTypeDefinition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGetUnion(string name, out UnionTypeDefinition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool HasUnion(Type clrType) => HasType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public bool HasUnion<TClrType>() => HasType<UnionTypeDefinition>(typeof(TClrType));

        public bool HasUnion(string name) => HasType<UnionTypeDefinition>(Check.NotNull(name, nameof(name)));

#endregion
}
}
// Source Hash Code: -1940229550