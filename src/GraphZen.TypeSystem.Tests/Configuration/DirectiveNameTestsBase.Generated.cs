using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class DirectiveNameTestsBase: LeafElementConfigurationTests<INamed, IMutableNamed,DirectiveDefinition,Directive> {}
}