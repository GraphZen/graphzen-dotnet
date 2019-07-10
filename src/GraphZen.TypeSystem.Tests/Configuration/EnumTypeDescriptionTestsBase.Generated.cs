
// ReSharper disable RedundantUsingDirective
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

namespace GraphZen.Configuration
{
    public abstract class EnumTypeDescriptionTestsBase: LeafElementConfigurationTests<IDescription, IMutableDescription,EnumTypeDefinition,EnumType> {

[Fact] public override void optional_not_defined_by_convention()  => base.optional_not_defined_by_convention(); 
[Fact] public override void define_by_data_annotation()  => base.define_by_data_annotation(); 

}
}