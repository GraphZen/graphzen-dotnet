using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public abstract class DirectiveDescriptionTestsBase: LeafElementConfigurationTests<IDescription, IMutableDescription,DirectiveDefinition,Directive> {}
}