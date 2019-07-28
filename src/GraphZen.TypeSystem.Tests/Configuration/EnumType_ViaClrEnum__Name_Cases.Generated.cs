// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration {
public abstract  class EnumType_ViaClrEnum__Name_Cases : EnumType__Name_Base {
[Fact]
public override void configured_explicitly_reconfigured_explicitly() => base.configured_explicitly_reconfigured_explicitly(); 
[Fact]
public override void configured_by_data_annotation() => base.configured_by_data_annotation(); 
[Fact]
public override void configured_by_data_annotation_then_reconfigured_explicitly() => base.configured_by_data_annotation_then_reconfigured_explicitly(); 
}
}
