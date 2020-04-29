#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem
{
    public partial interface ISchemaBuilder<TContext>
    {
        ISchemaBuilder<TContext> UnignoreEnum<TEnum>() where TEnum : notnull;

        ISchemaBuilder<TContext> UnignoreEnum(Type clrType);

        ISchemaBuilder<TContext> UnignoreEnum(string name);


        ISchemaBuilder<TContext> IgnoreEnum<TEnum>() where TEnum : notnull;

        ISchemaBuilder<TContext> IgnoreEnum(Type clrType);

        ISchemaBuilder<TContext> IgnoreEnum(string name);


        ISchemaBuilder<TContext> UnignoreInputObject<TInputObject>() where TInputObject : notnull;

        ISchemaBuilder<TContext> UnignoreInputObject(Type clrType);

        ISchemaBuilder<TContext> UnignoreInputObject(string name);


        ISchemaBuilder<TContext> IgnoreInputObject<TInputObject>() where TInputObject : notnull;

        ISchemaBuilder<TContext> IgnoreInputObject(Type clrType);

        ISchemaBuilder<TContext> IgnoreInputObject(string name);


        ISchemaBuilder<TContext> UnignoreInterface<TInterface>() where TInterface : notnull;

        ISchemaBuilder<TContext> UnignoreInterface(Type clrType);

        ISchemaBuilder<TContext> UnignoreInterface(string name);


        ISchemaBuilder<TContext> IgnoreInterface<TInterface>() where TInterface : notnull;

        ISchemaBuilder<TContext> IgnoreInterface(Type clrType);

        ISchemaBuilder<TContext> IgnoreInterface(string name);


        ISchemaBuilder<TContext> UnignoreObject<TObject>() where TObject : notnull;

        ISchemaBuilder<TContext> UnignoreObject(Type clrType);

        ISchemaBuilder<TContext> UnignoreObject(string name);


        ISchemaBuilder<TContext> IgnoreObject<TObject>() where TObject : notnull;

        ISchemaBuilder<TContext> IgnoreObject(Type clrType);

        ISchemaBuilder<TContext> IgnoreObject(string name);


        ISchemaBuilder<TContext> UnignoreScalar<TScalar>() where TScalar : notnull;

        ISchemaBuilder<TContext> UnignoreScalar(Type clrType);

        ISchemaBuilder<TContext> UnignoreScalar(string name);


        ISchemaBuilder<TContext> IgnoreScalar<TScalar>() where TScalar : notnull;

        ISchemaBuilder<TContext> IgnoreScalar(Type clrType);

        ISchemaBuilder<TContext> IgnoreScalar(string name);


        ISchemaBuilder<TContext> UnignoreUnion<TUnion>() where TUnion : notnull;

        ISchemaBuilder<TContext> UnignoreUnion(Type clrType);

        ISchemaBuilder<TContext> UnignoreUnion(string name);


        ISchemaBuilder<TContext> IgnoreUnion<TUnion>() where TUnion : notnull;

        ISchemaBuilder<TContext> IgnoreUnion(Type clrType);

        ISchemaBuilder<TContext> IgnoreUnion(string name);


        ISchemaBuilder<TContext> UnignoreDirective<TDirective>() where TDirective : notnull;

        ISchemaBuilder<TContext> UnignoreDirective(Type clrType);

        ISchemaBuilder<TContext> UnignoreDirective(string name);


        ISchemaBuilder<TContext> IgnoreDirective<TDirective>() where TDirective : notnull;

        ISchemaBuilder<TContext> IgnoreDirective(Type clrType);

        ISchemaBuilder<TContext> IgnoreDirective(string name);


        ISchemaBuilder<TContext> UnignoreType<TType>() where TType : notnull;

        ISchemaBuilder<TContext> UnignoreType(Type clrType);

        ISchemaBuilder<TContext> UnignoreType(string name);


        ISchemaBuilder<TContext> IgnoreType<TType>() where TType : notnull;

        ISchemaBuilder<TContext> IgnoreType(Type clrType);

        ISchemaBuilder<TContext> IgnoreType(string name);


        #region InputObject type accessors

        #endregion

        #region Interface type accessors

        IInterfaceTypeBuilder<object, TContext> Interface(Type clrType);


        IInterfaceTypeBuilder<object, TContext> Interface(string name);


        IInterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>() where TInterface : notnull;

        #endregion

        #region Object type accessors

        IObjectTypeBuilder<object, TContext> Object(Type clrType);


        IObjectTypeBuilder<object, TContext> Object(string name);


        IObjectTypeBuilder<TObject, TContext> Object<TObject>() where TObject : notnull;

        #endregion

        #region Union type accessors

        IUnionTypeBuilder<object, TContext> Union(Type clrType);


        IUnionTypeBuilder<object, TContext> Union(string name);


        IUnionTypeBuilder<TUnion, TContext> Union<TUnion>() where TUnion : notnull;

        #endregion
    }
}