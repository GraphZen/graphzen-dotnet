#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem {
public static class SchemaDefinitionExtensions {
#region Enum type accessors 

     public static EnumTypeDefinition GetEnum(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumTypeDefinition>(name);


        public static EnumTypeDefinition GetEnum(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));


        public static EnumTypeDefinition GetEnum<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumTypeDefinition>(typeof(TClrType));

        public static EnumTypeDefinition? FindEnum(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumTypeDefinition>(name);

        public static EnumTypeDefinition? FindEnum<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumTypeDefinition>(typeof(TClrType));


        public static EnumTypeDefinition? FindEnum(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetEnum(this SchemaDefinition schema, Type clrType,
            out EnumTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetEnum<TClrType>(this SchemaDefinition schema,
            out EnumTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetEnum(this SchemaDefinition schema, string name,
            out EnumTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasEnum(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasEnum<TClrType>(this SchemaDefinition schema) => Check.NotNull(schema, nameof(schema))
            .HasType<EnumTypeDefinition>(typeof(TClrType));

        public static bool HasEnum(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<EnumTypeDefinition>(Check.NotNull(name, nameof(name)));



#endregion
#region InputObject type accessors 

     public static InputObjectTypeDefinition GetInputObject(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectTypeDefinition>(name);


        public static InputObjectTypeDefinition GetInputObject(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));


        public static InputObjectTypeDefinition GetInputObject<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectTypeDefinition>(typeof(TClrType));

        public static InputObjectTypeDefinition? FindInputObject(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectTypeDefinition>(name);

        public static InputObjectTypeDefinition? FindInputObject<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectTypeDefinition>(typeof(TClrType));


        public static InputObjectTypeDefinition? FindInputObject(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInputObject(this SchemaDefinition schema, Type clrType,
            out InputObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInputObject<TClrType>(this SchemaDefinition schema,
            out InputObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInputObject(this SchemaDefinition schema, string name,
            out InputObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInputObject(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInputObject<TClrType>(this SchemaDefinition schema) => Check.NotNull(schema, nameof(schema))
            .HasType<InputObjectTypeDefinition>(typeof(TClrType));

        public static bool HasInputObject(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<InputObjectTypeDefinition>(Check.NotNull(name, nameof(name)));



#endregion
#region Interface type accessors 

     public static InterfaceTypeDefinition GetInterface(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceTypeDefinition>(name);


        public static InterfaceTypeDefinition GetInterface(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));


        public static InterfaceTypeDefinition GetInterface<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceTypeDefinition>(typeof(TClrType));

        public static InterfaceTypeDefinition? FindInterface(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceTypeDefinition>(name);

        public static InterfaceTypeDefinition? FindInterface<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceTypeDefinition>(typeof(TClrType));


        public static InterfaceTypeDefinition? FindInterface(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInterface(this SchemaDefinition schema, Type clrType,
            out InterfaceTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInterface<TClrType>(this SchemaDefinition schema,
            out InterfaceTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInterface(this SchemaDefinition schema, string name,
            out InterfaceTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInterface(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInterface<TClrType>(this SchemaDefinition schema) => Check.NotNull(schema, nameof(schema))
            .HasType<InterfaceTypeDefinition>(typeof(TClrType));

        public static bool HasInterface(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<InterfaceTypeDefinition>(Check.NotNull(name, nameof(name)));



#endregion
#region Object type accessors 

     public static ObjectTypeDefinition GetObject(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectTypeDefinition>(name);


        public static ObjectTypeDefinition GetObject(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));


        public static ObjectTypeDefinition GetObject<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectTypeDefinition>(typeof(TClrType));

        public static ObjectTypeDefinition? FindObject(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectTypeDefinition>(name);

        public static ObjectTypeDefinition? FindObject<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectTypeDefinition>(typeof(TClrType));


        public static ObjectTypeDefinition? FindObject(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetObject(this SchemaDefinition schema, Type clrType,
            out ObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetObject<TClrType>(this SchemaDefinition schema,
            out ObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetObject(this SchemaDefinition schema, string name,
            out ObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasObject(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasObject<TClrType>(this SchemaDefinition schema) => Check.NotNull(schema, nameof(schema))
            .HasType<ObjectTypeDefinition>(typeof(TClrType));

        public static bool HasObject(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<ObjectTypeDefinition>(Check.NotNull(name, nameof(name)));



#endregion
#region Scalar type accessors 

     public static ScalarTypeDefinition GetScalar(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarTypeDefinition>(name);


        public static ScalarTypeDefinition GetScalar(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));


        public static ScalarTypeDefinition GetScalar<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarTypeDefinition>(typeof(TClrType));

        public static ScalarTypeDefinition? FindScalar(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarTypeDefinition>(name);

        public static ScalarTypeDefinition? FindScalar<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarTypeDefinition>(typeof(TClrType));


        public static ScalarTypeDefinition? FindScalar(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetScalar(this SchemaDefinition schema, Type clrType,
            out ScalarTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetScalar<TClrType>(this SchemaDefinition schema,
            out ScalarTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetScalar(this SchemaDefinition schema, string name,
            out ScalarTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasScalar(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasScalar<TClrType>(this SchemaDefinition schema) => Check.NotNull(schema, nameof(schema))
            .HasType<ScalarTypeDefinition>(typeof(TClrType));

        public static bool HasScalar(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<ScalarTypeDefinition>(Check.NotNull(name, nameof(name)));



#endregion
#region Union type accessors 

     public static UnionTypeDefinition GetUnion(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionTypeDefinition>(name);


        public static UnionTypeDefinition GetUnion(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));


        public static UnionTypeDefinition GetUnion<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionTypeDefinition>(typeof(TClrType));

        public static UnionTypeDefinition? FindUnion(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionTypeDefinition>(name);

        public static UnionTypeDefinition? FindUnion<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionTypeDefinition>(typeof(TClrType));


        public static UnionTypeDefinition? FindUnion(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetUnion(this SchemaDefinition schema, Type clrType,
            out UnionTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetUnion<TClrType>(this SchemaDefinition schema,
            out UnionTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetUnion(this SchemaDefinition schema, string name,
            out UnionTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasUnion(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasUnion<TClrType>(this SchemaDefinition schema) => Check.NotNull(schema, nameof(schema))
            .HasType<UnionTypeDefinition>(typeof(TClrType));

        public static bool HasUnion(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<UnionTypeDefinition>(Check.NotNull(name, nameof(name)));



#endregion
}
}
