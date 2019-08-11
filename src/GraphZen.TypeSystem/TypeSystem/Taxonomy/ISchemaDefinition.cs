using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ISchemaDefinition : IDescription
    {

    }

    [GraphQLIgnore]
    public interface IObjectTypesContainerDefinition
    {
        IEnumerable<IObjectTypeDefinition> GetObjects();
    }
    [GraphQLIgnore]
    public interface IObjectTypesContainer : IObjectTypesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableObjectTypesContainerDefinition : IObjectTypesContainerDefinition { }


    
  
  
    [GraphQLIgnore]
    public interface IInterfaceTypesContainerDefinition
    {
        IEnumerable < IInterfaceTypeDefinition > GetInterfaces();
    }
    [GraphQLIgnore]
    public interface IInterfaceTypesContainer : IInterfaceTypesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableInterfaceTypesContainerDefinition : IInterfaceTypesContainerDefinition { }
  
  
  
  
  
  
    [GraphQLIgnore]
    public interface IUnionTypesContainerDefinition
    {
        IEnumerable < IUnionTypeDefinition > GetUnions();
    }
    [GraphQLIgnore]
    public interface IUnionTypesContainer : IUnionTypesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableUnionTypesContainerDefinition : IUnionTypesContainerDefinition { }
  
  
  
  
  
  
    [GraphQLIgnore]
    public interface IScalarTypesContainerDefinition
    {
        IEnumerable < IScalarTypeDefinition > GetScalars();
    }
    [GraphQLIgnore]
    public interface IScalarTypesContainer : IScalarTypesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableScalarTypesContainerDefinition : IScalarTypesContainerDefinition { }
  
  
  
  
  
  
    [GraphQLIgnore]
    public interface IEnumTypesContainerDefinition
    {
        IEnumerable < IEnumTypeDefinition > GetEnums();
    }
    [GraphQLIgnore]
    public interface IEnumTypesContainer : IEnumTypesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableEnumTypesContainerDefinition : IEnumTypesContainerDefinition { }
  
  
  
  
  
  
    [GraphQLIgnore]
    public interface IInputObjectTypesContainerDefinition
    {
        IEnumerable < IInputObjectTypeDefinition > GetInputObjects();
    }
    [GraphQLIgnore]
    public interface IInputObjectTypesContainer : IInputObjectTypesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableInputObjectTypesContainerDefinition : IInputObjectTypesContainerDefinition { }



  
    [GraphQLIgnore]
    public interface IDirectivesContainerDefinition
    {
        IEnumerable < IDirectiveDefinition > GetDirectives();
    }
    [GraphQLIgnore]
    public interface IDirectivesContainer : IDirectivesContainerDefinition { }
    [GraphQLIgnore]
    public interface IMutableDirectivesContainerDefinition : IDirectivesContainerDefinition { }



}