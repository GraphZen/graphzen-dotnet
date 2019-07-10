using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class EnumTypeNameTestsBase: LeafElementConfigurationTests<INamed, IMutableNamed,EnumTypeDefinition,EnumType> {}
}