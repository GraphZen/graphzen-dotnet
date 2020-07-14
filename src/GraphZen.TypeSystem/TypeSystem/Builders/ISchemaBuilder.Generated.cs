// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public partial interface ISchemaBuilder
    {
        #region SchemaBuilderInterfaceGenerator

        #region Directives

        IEnumerable<IBuildableDirectiveDefinition> GetDirectives(bool includeSpecDirectives = false);


        IDirectiveDefinitionBuilder Directive(string name);


        IDirectiveDefinitionBuilder Directive<TDirective>() where TDirective : notnull;
        IDirectiveDefinitionBuilder Directive<TDirective>(string name) where TDirective : notnull;


        IDirectiveDefinitionBuilder Directive(Type clrType);
        IDirectiveDefinitionBuilder Directive(Type clrType, string name);


        ISchemaBuilder UnignoreDirective<TDirective>() where TDirective : notnull;

        ISchemaBuilder UnignoreDirective(Type clrType);

        ISchemaBuilder UnignoreDirective(string name);


        ISchemaBuilder IgnoreDirective<TDirective>() where TDirective : notnull;

        ISchemaBuilder IgnoreDirective(Type clrType);

        ISchemaBuilder IgnoreDirective(string name);

        ISchemaBuilder RemoveDirective<TDirective>() where TDirective : notnull;

        ISchemaBuilder RemoveDirective(Type clrType);

        ISchemaBuilder RemoveDirective(string name);

        #endregion

        #region Types

        IEnumerable<IBuildableNamedTypeDefinition> GetTypes(bool includeSpecTypes = false);


        ISchemaBuilder UnignoreType<TClrType>() where TClrType : notnull;

        ISchemaBuilder UnignoreType(Type clrType);

        ISchemaBuilder UnignoreType(string name);


        ISchemaBuilder IgnoreType<TClrType>() where TClrType : notnull;

        ISchemaBuilder IgnoreType(Type clrType);

        ISchemaBuilder IgnoreType(string name);

        ISchemaBuilder RemoveType<TClrType>() where TClrType : notnull;

        ISchemaBuilder RemoveType(Type clrType);

        ISchemaBuilder RemoveType(string name);

        #endregion

        #region Objects

        IEnumerable<IBuildableObjectType> GetObjects(bool includeSpecObjects = false);


        IObjectTypeBuilder Object(string name);


        IObjectTypeBuilder<TObject, GraphQLContext> Object<TObject>() where TObject : notnull;
        IObjectTypeBuilder<TObject, GraphQLContext> Object<TObject>(string name) where TObject : notnull;


        IObjectTypeBuilder Object(Type clrType);
        IObjectTypeBuilder Object(Type clrType, string name);


        ISchemaBuilder UnignoreObject<TObject>() where TObject : notnull;

        ISchemaBuilder UnignoreObject(Type clrType);

        ISchemaBuilder UnignoreObject(string name);


        ISchemaBuilder IgnoreObject<TObject>() where TObject : notnull;

        ISchemaBuilder IgnoreObject(Type clrType);

        ISchemaBuilder IgnoreObject(string name);

        ISchemaBuilder RemoveObject<TObject>() where TObject : notnull;

        ISchemaBuilder RemoveObject(Type clrType);

        ISchemaBuilder RemoveObject(string name);

        #endregion

        #region Unions

        IEnumerable<IBuildableUnionType> GetUnions(bool includeSpecUnions = false);


        IUnionTypeBuilder Union(string name);


        IUnionTypeBuilder Union<TUnion>() where TUnion : notnull;
        IUnionTypeBuilder Union<TUnion>(string name) where TUnion : notnull;


        IUnionTypeBuilder Union(Type clrType);
        IUnionTypeBuilder Union(Type clrType, string name);


        ISchemaBuilder UnignoreUnion<TUnion>() where TUnion : notnull;

        ISchemaBuilder UnignoreUnion(Type clrType);

        ISchemaBuilder UnignoreUnion(string name);


        ISchemaBuilder IgnoreUnion<TUnion>() where TUnion : notnull;

        ISchemaBuilder IgnoreUnion(Type clrType);

        ISchemaBuilder IgnoreUnion(string name);

        ISchemaBuilder RemoveUnion<TUnion>() where TUnion : notnull;

        ISchemaBuilder RemoveUnion(Type clrType);

        ISchemaBuilder RemoveUnion(string name);

        #endregion

        #region Scalars
        IEnumerable<IScalarType> GetScalars(bool includeSpecScalars = false);
        ISchemaBuilder UnignoreScalar<TScalar>() where TScalar : notnull;
        ISchemaBuilder UnignoreScalar(Type clrType);
        ISchemaBuilder UnignoreScalar(string name);
        ISchemaBuilder IgnoreScalar<TScalar>() where TScalar : notnull;
        ISchemaBuilder IgnoreScalar(Type clrType);
        ISchemaBuilder IgnoreScalar(string name);

        ISchemaBuilder RemoveScalar<TScalar>() where TScalar : notnull;

        ISchemaBuilder RemoveScalar(Type clrType);

        ISchemaBuilder RemoveScalar(string name);

        #endregion

        #region Enums

        IEnumerable<IBuildableEnumType> GetEnums(bool includeSpecEnums = false);


        IEnumTypeBuilder Enum(string name);


        IEnumTypeBuilder Enum<TEnum>() where TEnum : notnull;
        IEnumTypeBuilder Enum<TEnum>(string name) where TEnum : notnull;


        IEnumTypeBuilder Enum(Type clrType);
        IEnumTypeBuilder Enum(Type clrType, string name);


        ISchemaBuilder UnignoreEnum<TEnum>() where TEnum : notnull;

        ISchemaBuilder UnignoreEnum(Type clrType);

        ISchemaBuilder UnignoreEnum(string name);


        ISchemaBuilder IgnoreEnum<TEnum>() where TEnum : notnull;

        ISchemaBuilder IgnoreEnum(Type clrType);

        ISchemaBuilder IgnoreEnum(string name);

        ISchemaBuilder RemoveEnum<TEnum>() where TEnum : notnull;

        ISchemaBuilder RemoveEnum(Type clrType);

        ISchemaBuilder RemoveEnum(string name);

        #endregion

        #region Interfaces

        IEnumerable<IBuildableInterfaceType> GetInterfaces(bool includeSpecInterfaces = false);


        IInterfaceTypeBuilder Interface(string name);


        IInterfaceTypeBuilder<TInterface, GraphQLContext> Interface<TInterface>() where TInterface : notnull;
        IInterfaceTypeBuilder<TInterface, GraphQLContext> Interface<TInterface>(string name) where TInterface : notnull;


        IInterfaceTypeBuilder Interface(Type clrType);
        IInterfaceTypeBuilder Interface(Type clrType, string name);


        ISchemaBuilder UnignoreInterface<TInterface>() where TInterface : notnull;

        ISchemaBuilder UnignoreInterface(Type clrType);

        ISchemaBuilder UnignoreInterface(string name);


        ISchemaBuilder IgnoreInterface<TInterface>() where TInterface : notnull;

        ISchemaBuilder IgnoreInterface(Type clrType);

        ISchemaBuilder IgnoreInterface(string name);

        ISchemaBuilder RemoveInterface<TInterface>() where TInterface : notnull;

        ISchemaBuilder RemoveInterface(Type clrType);

        ISchemaBuilder RemoveInterface(string name);

        #endregion

        #region InputObjects

        IEnumerable<IBuildableInputObjectType> GetInputObjects(bool includeSpecInputObjects = false);


        IInputObjectTypeBuilder InputObject(string name);


        IInputObjectTypeBuilder InputObject<TInputObject>() where TInputObject : notnull;
        IInputObjectTypeBuilder InputObject<TInputObject>(string name) where TInputObject : notnull;

        IInputObjectTypeBuilder InputObject(Type clrType);
        IInputObjectTypeBuilder InputObject(Type clrType, string name);

        ISchemaBuilder UnignoreInputObject<TInputObject>() where TInputObject : notnull;

        ISchemaBuilder UnignoreInputObject(Type clrType);

        ISchemaBuilder UnignoreInputObject(string name);


        ISchemaBuilder IgnoreInputObject<TInputObject>() where TInputObject : notnull;

        ISchemaBuilder IgnoreInputObject(Type clrType);

        ISchemaBuilder IgnoreInputObject(string name);

        ISchemaBuilder RemoveInputObject<TInputObject>() where TInputObject : notnull;

        ISchemaBuilder RemoveInputObject(Type clrType);

        ISchemaBuilder RemoveInputObject(string name);

        #endregion

        #endregion
    }
}
// Source Hash Code: 5871842628999824593