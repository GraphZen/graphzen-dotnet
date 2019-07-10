using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class ScalarTypeDescriptionTestsBase: LeafElementConfigurationTests<IDescription, IMutableDescription,ScalarTypeDefinition,ScalarType> {}
}