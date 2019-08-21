using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ISchemaDefinition : IDescription,
        IDirectivesContainerDefinition, IObjectTypesContainerDefinition, IInterfaceTypesContainerDefinition,
        IUnionTypesContainerDefinition, IScalarTypesContainerDefinition, IEnumTypesContainerDefinition,
        IInputObjectTypesContainerDefinition
    {
    }

    [GraphQLIgnore]
    public interface IObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IEnumerable<IObjectTypeDefinition> GetObjects();
    }

    [GraphQLIgnore]
    public interface IObjectTypesContainer : IObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectType> GetObjects();

        [NotNull]
        [ItemNotNull]
        [GraphQLIgnore]
        IReadOnlyList<ObjectType> Objects { get; }
    }

    [GraphQLIgnore]
    public interface IMutableObjectTypesContainerDefinition : IObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ObjectTypeDefinition> GetObjects();
    }


    [GraphQLIgnore]
    public interface IInterfaceTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IEnumerable<IInterfaceTypeDefinition> GetInterfaces();
    }

    [GraphQLIgnore]
    public interface IInterfaceTypesContainer : IInterfaceTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<InterfaceType> GetInterfaces();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<InterfaceType> Interfaces { get; }
    }

    [GraphQLIgnore]
    public interface IMutableInterfaceTypesContainerDefinition : IInterfaceTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<InterfaceTypeDefinition> GetInterfaces();
    }


    [GraphQLIgnore]
    public interface IUnionTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IEnumerable<IUnionTypeDefinition> GetUnions();

    }

    [GraphQLIgnore]
    public interface IUnionTypesContainer : IUnionTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<UnionType> GetUnions();
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<UnionType> Unions { get; }

    }

    [GraphQLIgnore]
    public interface IMutableUnionTypesContainerDefinition : IUnionTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<UnionTypeDefinition> GetUnions();
    }


    [GraphQLIgnore]
    public interface IScalarTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IEnumerable<IScalarTypeDefinition> GetScalars();
    }

    [GraphQLIgnore]
    public interface IScalarTypesContainer : IScalarTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ScalarType> GetScalars();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<ScalarType> Scalars { get; }
    }

    [GraphQLIgnore]
    public interface IMutableScalarTypesContainerDefinition : IScalarTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<ScalarTypeDefinition> GetScalars();
    }


    [GraphQLIgnore]
    public interface IEnumTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IEnumerable<IEnumTypeDefinition> GetEnums();
    }

    [GraphQLIgnore]
    public interface IEnumTypesContainer : IEnumTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumType> GetEnums();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<EnumType> Enums { get; }
    }

    [GraphQLIgnore]
    public interface IMutableEnumTypesContainerDefinition : IEnumTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumTypeDefinition> GetEnums();
    }


    [GraphQLIgnore]
    public interface IInputObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IEnumerable<IInputObjectTypeDefinition> GetInputObjects();
    }

    [GraphQLIgnore]
    public interface IInputObjectTypesContainer : IInputObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<InputObjectType> GetInputObjects();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<InputObjectType> InputObjects { get; }
    }

    [GraphQLIgnore]
    public interface IMutableInputObjectTypesContainerDefinition : IInputObjectTypesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<InputObjectTypeDefinition> GetInputObjects();
    }


    [GraphQLIgnore]
    public interface IDirectivesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IEnumerable<IDirectiveDefinition> GetDirectives();
    }

    [GraphQLIgnore]
    public interface IDirectivesContainer : IDirectivesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<Directive> GetDirectives();

        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<Directive> Directives { get; }

    }

    [GraphQLIgnore]
    public interface IMutableDirectivesContainerDefinition : IDirectivesContainerDefinition
    {
        [GraphQLIgnore]
        [NotNull]
        [ItemNotNull]
        new IEnumerable<DirectiveDefinition> GetDirectives();
    }
}