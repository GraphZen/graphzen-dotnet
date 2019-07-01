// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public static class SchemaExtensions
    {
        [NotNull]
        public static UnionType GetUnion([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionType>(name);

        [NotNull]
        public static UnionType GetUnion([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static UnionType GetUnion<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionType>(typeof(TClrType));

        public static UnionType FindUnion([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionType>(name);

        public static UnionType FindUnion<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionType>(typeof(TClrType));


        public static UnionType FindUnion([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetUnion([NotNull] this Schema schema, [NotNull] Type clrType, out UnionType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetUnion<TClrType>([NotNull] this Schema schema, out UnionType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetUnion([NotNull] this Schema schema, [NotNull] string name, out UnionType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasUnion([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasUnion<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionType>(typeof(TClrType));

        public static bool HasUnion([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionType>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static ScalarType GetScalar([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarType>(name);

        [NotNull]
        public static ScalarType GetScalar([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static ScalarType GetScalar<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarType>(typeof(TClrType));

        public static ScalarType FindScalar([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarType>(name);

        public static ScalarType FindScalar<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarType>(typeof(TClrType));


        public static ScalarType FindScalar([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetScalar([NotNull] this Schema schema, [NotNull] Type clrType, out ScalarType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetScalar<TClrType>([NotNull] this Schema schema, out ScalarType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetScalar([NotNull] this Schema schema, [NotNull] string name, out ScalarType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasScalar([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasScalar<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarType>(typeof(TClrType));

        public static bool HasScalar([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarType>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static InterfaceType GetInterface([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceType>(name);

        [NotNull]
        public static InterfaceType GetInterface([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static InterfaceType GetInterface<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceType>(typeof(TClrType));

        public static InterfaceType FindInterface([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceType>(name);

        public static InterfaceType FindInterface<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceType>(typeof(TClrType));


        public static InterfaceType FindInterface([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInterface([NotNull] this Schema schema, [NotNull] Type clrType,
            out InterfaceType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInterface<TClrType>([NotNull] this Schema schema, out InterfaceType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInterface([NotNull] this Schema schema, [NotNull] string name,
            out InterfaceType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInterface([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInterface<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceType>(typeof(TClrType));

        public static bool HasInterface([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceType>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static EnumType GetEnum([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumType>(name);

        [NotNull]
        public static EnumType GetEnum([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static EnumType GetEnum<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumType>(typeof(TClrType));

        public static EnumType FindEnum([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumType>(name);

        public static EnumType FindEnum<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumType>(typeof(TClrType));


        public static EnumType FindEnum([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetEnum([NotNull] this Schema schema, [NotNull] Type clrType, out EnumType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetEnum<TClrType>([NotNull] this Schema schema, out EnumType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetEnum([NotNull] this Schema schema, [NotNull] string name, out EnumType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasEnum([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasEnum<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumType>(typeof(TClrType));

        public static bool HasEnum([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumType>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static InputObjectType GetInputObject([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectType>(name);

        [NotNull]
        public static InputObjectType GetInputObject([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static InputObjectType GetInputObject<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectType>(typeof(TClrType));

        public static InputObjectType FindInputObject([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectType>(name);

        public static InputObjectType FindInputObject<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectType>(typeof(TClrType));


        public static InputObjectType FindInputObject([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInputObject([NotNull] this Schema schema, [NotNull] Type clrType,
            out InputObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInputObject<TClrType>([NotNull] this Schema schema, out InputObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInputObject([NotNull] this Schema schema, [NotNull] string name,
            out InputObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInputObject([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInputObject<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectType>(typeof(TClrType));

        public static bool HasInputObject([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectType>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static ObjectType GetObject([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectType>(name);

        [NotNull]
        public static ObjectType GetObject([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static ObjectType GetObject<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectType>(typeof(TClrType));

        public static ObjectType FindObject([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectType>(name);

        public static ObjectType FindObject<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectType>(typeof(TClrType));


        public static ObjectType FindObject([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetObject([NotNull] this Schema schema, [NotNull] Type clrType, out ObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetObject<TClrType>([NotNull] this Schema schema, out ObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetObject([NotNull] this Schema schema, [NotNull] string name, out ObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasObject([NotNull] this Schema schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasObject<TClrType>([NotNull] this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectType>(typeof(TClrType));

        public static bool HasObject([NotNull] this Schema schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectType>(Check.NotNull(name, nameof(name)));
    }
}