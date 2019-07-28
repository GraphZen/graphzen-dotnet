// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration
{
    public class InterfaceType__Field__Argument__Description : InterfaceType__Field__Argument__Description_Cases
    {
        public override void ConfigureParent(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            parentName = ExplicitParentName;
            sb.Interface(GreatGrandparentName)
                .Field(GrandparentName, "String", fb => fb.Argument(ExplicitParentName, "String"));
        }
    }
}
