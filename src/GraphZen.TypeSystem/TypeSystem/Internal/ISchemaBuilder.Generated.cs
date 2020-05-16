#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem.Internal
{
    internal partial interface ISchemaBuilder<TContext>
    {
        #region SchemaBuilderInterfaceGenerator

        #region Directives

        DirectiveBuilder<object> Directive(string name);


        DirectiveBuilder<TDirective> Directive<TDirective>() where TDirective : notnull;
        DirectiveBuilder<TDirective> Directive<TDirective>(string name) where TDirective : notnull;


        DirectiveBuilder<object> Directive(Type clrType);
        DirectiveBuilder<object> Directive(Type clrType, string name);


        SchemaBuilder<TContext> UnignoreDirective<TDirective>() where TDirective : notnull;

        SchemaBuilder<TContext> UnignoreDirective(Type clrType);

        SchemaBuilder<TContext> UnignoreDirective(string name);


        SchemaBuilder<TContext> IgnoreDirective<TDirective>() where TDirective : notnull;

        SchemaBuilder<TContext> IgnoreDirective(Type clrType);

        SchemaBuilder<TContext> IgnoreDirective(string name);

        SchemaBuilder<TContext> RemoveDirective<TDirective>() where TDirective : notnull;

        SchemaBuilder<TContext> RemoveDirective(Type clrType);

        SchemaBuilder<TContext> RemoveDirective(string name);

        #endregion

        #region Types

        SchemaBuilder<TContext> UnignoreType<TClrType>() where TClrType : notnull;

        SchemaBuilder<TContext> UnignoreType(Type clrType);

        SchemaBuilder<TContext> UnignoreType(string name);


        SchemaBuilder<TContext> IgnoreType<TClrType>() where TClrType : notnull;

        SchemaBuilder<TContext> IgnoreType(Type clrType);

        SchemaBuilder<TContext> IgnoreType(string name);

        SchemaBuilder<TContext> RemoveType<TClrType>() where TClrType : notnull;

        SchemaBuilder<TContext> RemoveType(Type clrType);

        SchemaBuilder<TContext> RemoveType(string name);

        #endregion

        #region Objects

        ObjectTypeBuilder<object, TContext> Object(string name);


        ObjectTypeBuilder<TObject, TContext> Object<TObject>() where TObject : notnull;
        ObjectTypeBuilder<TObject, TContext> Object<TObject>(string name) where TObject : notnull;


        ObjectTypeBuilder<object, TContext> Object(Type clrType);
        ObjectTypeBuilder<object, TContext> Object(Type clrType, string name);


        SchemaBuilder<TContext> UnignoreObject<TObject>() where TObject : notnull;

        SchemaBuilder<TContext> UnignoreObject(Type clrType);

        SchemaBuilder<TContext> UnignoreObject(string name);


        SchemaBuilder<TContext> IgnoreObject<TObject>() where TObject : notnull;

        SchemaBuilder<TContext> IgnoreObject(Type clrType);

        SchemaBuilder<TContext> IgnoreObject(string name);

        SchemaBuilder<TContext> RemoveObject<TObject>() where TObject : notnull;

        SchemaBuilder<TContext> RemoveObject(Type clrType);

        SchemaBuilder<TContext> RemoveObject(string name);

        #endregion

        #region Unions

        UnionTypeBuilder<object, TContext> Union(string name);


        UnionTypeBuilder<TUnion, TContext> Union<TUnion>() where TUnion : notnull;
        UnionTypeBuilder<TUnion, TContext> Union<TUnion>(string name) where TUnion : notnull;


        UnionTypeBuilder<object, TContext> Union(Type clrType);
        UnionTypeBuilder<object, TContext> Union(Type clrType, string name);


        SchemaBuilder<TContext> UnignoreUnion<TUnion>() where TUnion : notnull;

        SchemaBuilder<TContext> UnignoreUnion(Type clrType);

        SchemaBuilder<TContext> UnignoreUnion(string name);


        SchemaBuilder<TContext> IgnoreUnion<TUnion>() where TUnion : notnull;

        SchemaBuilder<TContext> IgnoreUnion(Type clrType);

        SchemaBuilder<TContext> IgnoreUnion(string name);

        SchemaBuilder<TContext> RemoveUnion<TUnion>() where TUnion : notnull;

        SchemaBuilder<TContext> RemoveUnion(Type clrType);

        SchemaBuilder<TContext> RemoveUnion(string name);

        #endregion

        #region Scalars

        SchemaBuilder<TContext> UnignoreScalar<TScalar>() where TScalar : notnull;

        SchemaBuilder<TContext> UnignoreScalar(Type clrType);

        SchemaBuilder<TContext> UnignoreScalar(string name);


        SchemaBuilder<TContext> IgnoreScalar<TScalar>() where TScalar : notnull;

        SchemaBuilder<TContext> IgnoreScalar(Type clrType);

        SchemaBuilder<TContext> IgnoreScalar(string name);

        SchemaBuilder<TContext> RemoveScalar<TScalar>() where TScalar : notnull;

        SchemaBuilder<TContext> RemoveScalar(Type clrType);

        SchemaBuilder<TContext> RemoveScalar(string name);

        #endregion

        #region Enums

        EnumTypeBuilder<string> Enum(string name);


        EnumTypeBuilder<TEnum> Enum<TEnum>() where TEnum : notnull;
        EnumTypeBuilder<TEnum> Enum<TEnum>(string name) where TEnum : notnull;


        EnumTypeBuilder<string> Enum(Type clrType);
        EnumTypeBuilder<string> Enum(Type clrType, string name);


        SchemaBuilder<TContext> UnignoreEnum<TEnum>() where TEnum : notnull;

        SchemaBuilder<TContext> UnignoreEnum(Type clrType);

        SchemaBuilder<TContext> UnignoreEnum(string name);


        SchemaBuilder<TContext> IgnoreEnum<TEnum>() where TEnum : notnull;

        SchemaBuilder<TContext> IgnoreEnum(Type clrType);

        SchemaBuilder<TContext> IgnoreEnum(string name);

        SchemaBuilder<TContext> RemoveEnum<TEnum>() where TEnum : notnull;

        SchemaBuilder<TContext> RemoveEnum(Type clrType);

        SchemaBuilder<TContext> RemoveEnum(string name);

        #endregion

        #region Interfaces

        InterfaceTypeBuilder<object, TContext> Interface(string name);


        InterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>() where TInterface : notnull;
        InterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>(string name) where TInterface : notnull;


        InterfaceTypeBuilder<object, TContext> Interface(Type clrType);
        InterfaceTypeBuilder<object, TContext> Interface(Type clrType, string name);


        SchemaBuilder<TContext> UnignoreInterface<TInterface>() where TInterface : notnull;

        SchemaBuilder<TContext> UnignoreInterface(Type clrType);

        SchemaBuilder<TContext> UnignoreInterface(string name);


        SchemaBuilder<TContext> IgnoreInterface<TInterface>() where TInterface : notnull;

        SchemaBuilder<TContext> IgnoreInterface(Type clrType);

        SchemaBuilder<TContext> IgnoreInterface(string name);

        SchemaBuilder<TContext> RemoveInterface<TInterface>() where TInterface : notnull;

        SchemaBuilder<TContext> RemoveInterface(Type clrType);

        SchemaBuilder<TContext> RemoveInterface(string name);

        #endregion

        #region InputObjects

        InputObjectTypeBuilder<object> InputObject(string name);


        InputObjectTypeBuilder<TInputObject> InputObject<TInputObject>() where TInputObject : notnull;
        InputObjectTypeBuilder<TInputObject> InputObject<TInputObject>(string name) where TInputObject : notnull;


        InputObjectTypeBuilder<object> InputObject(Type clrType);
        InputObjectTypeBuilder<object> InputObject(Type clrType, string name);


        SchemaBuilder<TContext> UnignoreInputObject<TInputObject>() where TInputObject : notnull;

        SchemaBuilder<TContext> UnignoreInputObject(Type clrType);

        SchemaBuilder<TContext> UnignoreInputObject(string name);


        SchemaBuilder<TContext> IgnoreInputObject<TInputObject>() where TInputObject : notnull;

        SchemaBuilder<TContext> IgnoreInputObject(Type clrType);

        SchemaBuilder<TContext> IgnoreInputObject(string name);

        SchemaBuilder<TContext> RemoveInputObject<TInputObject>() where TInputObject : notnull;

        SchemaBuilder<TContext> RemoveInputObject(Type clrType);

        SchemaBuilder<TContext> RemoveInputObject(string name);

        #endregion

        #endregion
    }
}
// Source Hash Code: 15729562857696596206