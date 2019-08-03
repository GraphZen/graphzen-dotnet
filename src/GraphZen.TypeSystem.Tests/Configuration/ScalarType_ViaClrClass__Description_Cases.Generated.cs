// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration {
public abstract  class ScalarType_ViaClrClass__Description_Cases : ScalarType__Description_Base {
[Fact]
public override void configured_explicitly_reconfigured_explicitly() => base.configured_explicitly_reconfigured_explicitly(); 
[Fact]
public override void optional_not_defined_by_convention_when_parent_configured_explicitly() => base.optional_not_defined_by_convention_when_parent_configured_explicitly(); 
[Fact]
public override void optional_not_defined_by_convention_then_configured_explicitly() => base.optional_not_defined_by_convention_then_configured_explicitly(); 
[Fact]
public override void optional_not_defined_by_convention_then_configured_explicitly_then_removed() => base.optional_not_defined_by_convention_then_configured_explicitly_then_removed(); 
[Fact]
public override void configured_by_data_annotation() => base.configured_by_data_annotation(); 
[Fact]
public override void configured_by_data_annotation_then_reconfigured_explicitly() => base.configured_by_data_annotation_then_reconfigured_explicitly(); 
[Fact]
public override void optional_configured_by_data_annotation_then_removed_explicitly() => base.optional_configured_by_data_annotation_then_removed_explicitly(); 
}
}
