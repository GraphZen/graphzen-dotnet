// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration {
public abstract  class InterfaceType__Field__Name_Cases : InterfaceType__Field__Name_Base {

public override bool DefinedByConvention { get; } = false;
public override bool DefinedByDataAnnotation { get; } = false;

[Fact]
public override void configured_explicitly_reconfigured_explicitly() => base.configured_explicitly_reconfigured_explicitly(); 
}
}
