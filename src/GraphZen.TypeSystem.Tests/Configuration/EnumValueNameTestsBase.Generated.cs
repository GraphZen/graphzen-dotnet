using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class EnumValueNameTestsBase: LeafElementConfigurationTests<INamed, IMutableNamed,EnumValueDefinition,EnumValue> {}
}