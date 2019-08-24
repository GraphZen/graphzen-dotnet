// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;


using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public static class SchemaExtensions
    {
        
        public static UnionType GetUnion( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionType>(name);

        
        public static UnionType GetUnion( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        
        public static UnionType GetUnion<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<UnionType>(typeof(TClrType));

        public static UnionType? FindUnion( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionType>(name);

        public static UnionType? FindUnion<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionType>(typeof(TClrType));


        public static UnionType FindUnion( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetUnion( this Schema schema,  Type clrType, out UnionType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetUnion<TClrType>( this Schema schema, out UnionType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetUnion( this Schema schema,  string name, out UnionType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasUnion( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasUnion<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionType>(typeof(TClrType));

        public static bool HasUnion( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<UnionType>(Check.NotNull(name, nameof(name)));


        
        public static ScalarType GetScalar( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarType>(name);

        
        public static ScalarType GetScalar( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        
        public static ScalarType GetScalar<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ScalarType>(typeof(TClrType));

        public static ScalarType FindScalar( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarType>(name);

        public static ScalarType FindScalar<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarType>(typeof(TClrType));


        public static ScalarType FindScalar( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetScalar( this Schema schema,  Type clrType, out ScalarType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetScalar<TClrType>( this Schema schema, out ScalarType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetScalar( this Schema schema,  string name, out ScalarType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasScalar( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasScalar<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarType>(typeof(TClrType));

        public static bool HasScalar( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<ScalarType>(Check.NotNull(name, nameof(name)));


        
        public static InterfaceType GetInterface( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceType>(name);

        
        public static InterfaceType GetInterface( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        
        public static InterfaceType GetInterface<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InterfaceType>(typeof(TClrType));

        public static InterfaceType FindInterface( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceType>(name);

        public static InterfaceType FindInterface<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceType>(typeof(TClrType));


        public static InterfaceType FindInterface( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInterface( this Schema schema,  Type clrType,
            out InterfaceType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInterface<TClrType>( this Schema schema, out InterfaceType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInterface( this Schema schema,  string name,
            out InterfaceType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInterface( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInterface<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceType>(typeof(TClrType));

        public static bool HasInterface( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<InterfaceType>(Check.NotNull(name, nameof(name)));


        
        public static EnumType GetEnum( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumType>(name);

        
        public static EnumType GetEnum( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        
        public static EnumType GetEnum<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<EnumType>(typeof(TClrType));

        public static EnumType FindEnum( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumType>(name);

        public static EnumType FindEnum<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumType>(typeof(TClrType));


        public static EnumType FindEnum( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetEnum( this Schema schema,  Type clrType, out EnumType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetEnum<TClrType>( this Schema schema, out EnumType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetEnum( this Schema schema,  string name, out EnumType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasEnum( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasEnum<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumType>(typeof(TClrType));

        public static bool HasEnum( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<EnumType>(Check.NotNull(name, nameof(name)));


        
        public static InputObjectType GetInputObject( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectType>(name);

        
        public static InputObjectType GetInputObject( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        
        public static InputObjectType GetInputObject<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<InputObjectType>(typeof(TClrType));

        public static InputObjectType FindInputObject( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectType>(name);

        public static InputObjectType FindInputObject<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectType>(typeof(TClrType));


        public static InputObjectType FindInputObject( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetInputObject( this Schema schema,  Type clrType,
            out InputObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetInputObject<TClrType>( this Schema schema, out InputObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetInputObject( this Schema schema,  string name,
            out InputObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasInputObject( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasInputObject<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectType>(typeof(TClrType));

        public static bool HasInputObject( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<InputObjectType>(Check.NotNull(name, nameof(name)));


        
        public static ObjectType GetObject( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectType>(name);

        
        public static ObjectType GetObject( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        
        public static ObjectType GetObject<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<ObjectType>(typeof(TClrType));

        public static ObjectType FindObject( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectType>(name);

        public static ObjectType FindObject<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectType>(typeof(TClrType));


        public static ObjectType FindObject( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).FindType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGetObject( this Schema schema,  Type clrType, out ObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGetObject<TClrType>( this Schema schema, out ObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGetObject( this Schema schema,  string name, out ObjectType type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool HasObject( this Schema schema,  Type clrType) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectType>(Check.NotNull(clrType, nameof(clrType)));

        public static bool HasObject<TClrType>( this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectType>(typeof(TClrType));

        public static bool HasObject( this Schema schema,  string name) =>
            Check.NotNull(schema, nameof(schema)).HasType<ObjectType>(Check.NotNull(name, nameof(name)));
    }
}