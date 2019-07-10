using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class UnionTypeNameTestsBase: LeafElementConfigurationTests<INamed, IMutableNamed,UnionTypeDefinition,UnionType> {}
}