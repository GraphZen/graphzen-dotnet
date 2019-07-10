using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class UnionTypeDescriptionTestsBase: LeafElementConfigurationTests<IDescription, IMutableDescription,UnionTypeDefinition,UnionType> {}
}