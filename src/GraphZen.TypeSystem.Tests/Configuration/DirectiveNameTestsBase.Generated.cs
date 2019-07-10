
// ReSharper disable RedundantUsingDirective
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

namespace GraphZen.Configuration
{
    public abstract class DirectiveNameTestsBase: LeafElementConfigurationTests<INamed, IMutableNamed,DirectiveDefinition,Directive> {

[Fact] public override void defined_by_convention()  => base.defined_by_convention(); 
[Fact] public override void define_by_data_annotation()  => base.define_by_data_annotation(); 

}
}