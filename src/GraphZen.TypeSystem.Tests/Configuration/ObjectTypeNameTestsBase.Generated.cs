using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class ObjectTypeNameTestsBase: LeafElementConfigurationTests<INamed, IMutableNamed,ObjectTypeDefinition,ObjectType> {}
}