// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public static class SchemaDefinitionExtensions
    {
        [NotNull]
        public static UnionTypeDefinition GetUnion([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionTypeDefinition>(name);

        [NotNull]
        public static UnionTypeDefinition GetUnion([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static UnionTypeDefinition GetUnion<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionTypeDefinition>(typeof(TClrType));

        public static UnionTypeDefinition FindUnion([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionTypeDefinition>(name);

        public static UnionTypeDefinition FindUnion<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionTypeDefinition>(typeof(TClrType));


        public static UnionTypeDefinition FindUnion([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetUnion([NotNull] this SchemaDefinition schema, [NotNull] Type clrType,
            out UnionTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetUnion<TClrType>([NotNull] this SchemaDefinition schema,
            out UnionTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetUnion([NotNull] this SchemaDefinition schema, [NotNull] string name,
            out UnionTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasUnion([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasUnion<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionTypeDefinition>(typeof(TClrType));

        public static bool HasUnion([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionTypeDefinition>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static ScalarTypeDefinition GetScalar([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarTypeDefinition>(name);

        [NotNull]
        public static ScalarTypeDefinition GetScalar([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static ScalarTypeDefinition GetScalar<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarTypeDefinition>(typeof(TClrType));

        public static ScalarTypeDefinition FindScalar([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarTypeDefinition>(name);

        public static ScalarTypeDefinition FindScalar<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarTypeDefinition>(typeof(TClrType));


        public static ScalarTypeDefinition FindScalar([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetScalar([NotNull] this SchemaDefinition schema, [NotNull] Type clrType,
            out ScalarTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetScalar<TClrType>([NotNull] this SchemaDefinition schema,
            out ScalarTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetScalar([NotNull] this SchemaDefinition schema, [NotNull] string name,
            out ScalarTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasScalar([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<ScalarTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasScalar<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarTypeDefinition>(typeof(TClrType));

        public static bool HasScalar([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarTypeDefinition>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static InterfaceTypeDefinition GetInterface([NotNull] this SchemaDefinition schema,
            [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceTypeDefinition>(name);

        [NotNull]
        public static InterfaceTypeDefinition GetInterface([NotNull] this SchemaDefinition schema,
            [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static InterfaceTypeDefinition GetInterface<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceTypeDefinition>(typeof(TClrType));

        public static InterfaceTypeDefinition FindInterface([NotNull] this SchemaDefinition schema,
            [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceTypeDefinition>(name);

        public static InterfaceTypeDefinition FindInterface<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceTypeDefinition>(typeof(TClrType));


        public static InterfaceTypeDefinition FindInterface([NotNull] this SchemaDefinition schema,
            [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInterface([NotNull] this SchemaDefinition schema, [NotNull] Type clrType,
            out InterfaceTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInterface<TClrType>([NotNull] this SchemaDefinition schema,
            out InterfaceTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInterface([NotNull] this SchemaDefinition schema, [NotNull] string name,
            out InterfaceTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInterface([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<InterfaceTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInterface<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceTypeDefinition>(typeof(TClrType));

        public static bool HasInterface([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceTypeDefinition>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static EnumTypeDefinition GetEnum([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumTypeDefinition>(name);

        [NotNull]
        public static EnumTypeDefinition GetEnum([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static EnumTypeDefinition GetEnum<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumTypeDefinition>(typeof(TClrType));

        public static EnumTypeDefinition FindEnum([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumTypeDefinition>(name);

        public static EnumTypeDefinition FindEnum<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumTypeDefinition>(typeof(TClrType));


        public static EnumTypeDefinition FindEnum([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetEnum([NotNull] this SchemaDefinition schema, [NotNull] Type clrType,
            out EnumTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetEnum<TClrType>([NotNull] this SchemaDefinition schema, out EnumTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetEnum([NotNull] this SchemaDefinition schema, [NotNull] string name,
            out EnumTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasEnum([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasEnum<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumTypeDefinition>(typeof(TClrType));

        public static bool HasEnum([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumTypeDefinition>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static InputObjectTypeDefinition GetInputObject([NotNull] this SchemaDefinition schema,
            [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectTypeDefinition>(name);

        [NotNull]
        public static InputObjectTypeDefinition GetInputObject([NotNull] this SchemaDefinition schema,
            [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static InputObjectTypeDefinition GetInputObject<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectTypeDefinition>(typeof(TClrType));

        public static InputObjectTypeDefinition FindInputObject([NotNull] this SchemaDefinition schema,
            [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectTypeDefinition>(name);

        public static InputObjectTypeDefinition FindInputObject<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectTypeDefinition>(typeof(TClrType));


        public static InputObjectTypeDefinition FindInputObject([NotNull] this SchemaDefinition schema,
            [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInputObject([NotNull] this SchemaDefinition schema, [NotNull] Type clrType,
            out InputObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInputObject<TClrType>([NotNull] this SchemaDefinition schema,
            out InputObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInputObject([NotNull] this SchemaDefinition schema, [NotNull] string name,
            out InputObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInputObject([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<InputObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInputObject<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectTypeDefinition>(typeof(TClrType));

        public static bool HasInputObject([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectTypeDefinition>(Check.NotNull(name, nameof(name)));


        [NotNull]
        public static ObjectTypeDefinition GetObject([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectTypeDefinition>(name);

        [NotNull]
        public static ObjectTypeDefinition GetObject([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        [NotNull]
        public static ObjectTypeDefinition GetObject<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectTypeDefinition>(typeof(TClrType));

        public static ObjectTypeDefinition FindObject([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectTypeDefinition>(name);

        public static ObjectTypeDefinition FindObject<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectTypeDefinition>(typeof(TClrType));


        public static ObjectTypeDefinition FindObject([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetObject([NotNull] this SchemaDefinition schema, [NotNull] Type clrType,
            out ObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetObject<TClrType>([NotNull] this SchemaDefinition schema,
            out ObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetObject([NotNull] this SchemaDefinition schema, [NotNull] string name,
            out ObjectTypeDefinition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasObject([NotNull] this SchemaDefinition schema, [NotNull] Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<ObjectTypeDefinition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasObject<TClrType>([NotNull] this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectTypeDefinition>(typeof(TClrType));

        public static bool HasObject([NotNull] this SchemaDefinition schema, [NotNull] string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectTypeDefinition>(Check.NotNull(name, nameof(name)));
    }
}