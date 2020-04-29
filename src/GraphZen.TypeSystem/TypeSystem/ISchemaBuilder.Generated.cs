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
        #region Directive

        IDirectiveBuilder<object> Directive(string name);


        IDirectiveBuilder<T> Directive<T>() where T : notnull;


        IDirectiveBuilder<object> Directive(Type clrType);


        ISchemaBuilder<TContext> UnignoreDirective<T>() where T : notnull;

        ISchemaBuilder<TContext> UnignoreDirective(Type clrType);

        ISchemaBuilder<TContext> UnignoreDirective(string name);


        ISchemaBuilder<TContext> IgnoreDirective<T>() where T : notnull;

        ISchemaBuilder<TContext> IgnoreDirective(Type clrType);

        ISchemaBuilder<TContext> IgnoreDirective(string name);

        #endregion

        #region Type

        ISchemaBuilder<TContext> UnignoreType<TClrType>() where TClrType : notnull;

        ISchemaBuilder<TContext> UnignoreType(Type clrType);

        ISchemaBuilder<TContext> UnignoreType(string name);


        ISchemaBuilder<TContext> IgnoreType<TClrType>() where TClrType : notnull;

        ISchemaBuilder<TContext> IgnoreType(Type clrType);

        ISchemaBuilder<TContext> IgnoreType(string name);

        #endregion

        #region Object

        ISchemaBuilder<TContext> UnignoreObject<T>() where T : notnull;

        ISchemaBuilder<TContext> UnignoreObject(Type clrType);

        ISchemaBuilder<TContext> UnignoreObject(string name);


        ISchemaBuilder<TContext> IgnoreObject<T>() where T : notnull;

        ISchemaBuilder<TContext> IgnoreObject(Type clrType);

        ISchemaBuilder<TContext> IgnoreObject(string name);

        #endregion

        #region Union

        ISchemaBuilder<TContext> UnignoreUnion<T>() where T : notnull;

        ISchemaBuilder<TContext> UnignoreUnion(Type clrType);

        ISchemaBuilder<TContext> UnignoreUnion(string name);


        ISchemaBuilder<TContext> IgnoreUnion<T>() where T : notnull;

        ISchemaBuilder<TContext> IgnoreUnion(Type clrType);

        ISchemaBuilder<TContext> IgnoreUnion(string name);

        #endregion

        #region Scalar

        ISchemaBuilder<TContext> UnignoreScalar<T>() where T : notnull;

        ISchemaBuilder<TContext> UnignoreScalar(Type clrType);

        ISchemaBuilder<TContext> UnignoreScalar(string name);


        ISchemaBuilder<TContext> IgnoreScalar<T>() where T : notnull;

        ISchemaBuilder<TContext> IgnoreScalar(Type clrType);

        ISchemaBuilder<TContext> IgnoreScalar(string name);

        #endregion

        #region Enum

        IEnumTypeBuilder<string> Enum(string name);


        IEnumTypeBuilder<T> Enum<T>() where T : notnull;


        IEnumTypeBuilder<string> Enum(Type clrType);


        ISchemaBuilder<TContext> UnignoreEnum<T>() where T : notnull;

        ISchemaBuilder<TContext> UnignoreEnum(Type clrType);

        ISchemaBuilder<TContext> UnignoreEnum(string name);


        ISchemaBuilder<TContext> IgnoreEnum<T>() where T : notnull;

        ISchemaBuilder<TContext> IgnoreEnum(Type clrType);

        ISchemaBuilder<TContext> IgnoreEnum(string name);

        #endregion

        #region Interface

        ISchemaBuilder<TContext> UnignoreInterface<T>() where T : notnull;

        ISchemaBuilder<TContext> UnignoreInterface(Type clrType);

        ISchemaBuilder<TContext> UnignoreInterface(string name);


        ISchemaBuilder<TContext> IgnoreInterface<T>() where T : notnull;

        ISchemaBuilder<TContext> IgnoreInterface(Type clrType);

        ISchemaBuilder<TContext> IgnoreInterface(string name);

        #endregion

        #region InputObject

        ISchemaBuilder<TContext> UnignoreInputObject<T>() where T : notnull;

        ISchemaBuilder<TContext> UnignoreInputObject(Type clrType);

        ISchemaBuilder<TContext> UnignoreInputObject(string name);


        ISchemaBuilder<TContext> IgnoreInputObject<T>() where T : notnull;

        ISchemaBuilder<TContext> IgnoreInputObject(Type clrType);

        ISchemaBuilder<TContext> IgnoreInputObject(string name);

        #endregion

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