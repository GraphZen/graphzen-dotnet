using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class FieldNameTestsBase: LeafElementConfigurationTests<INamed, IMutableNamed,FieldDefinition,Field> {}
}