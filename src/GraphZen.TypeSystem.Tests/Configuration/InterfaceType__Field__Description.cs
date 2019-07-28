// Last generated: Saturday, July 27, 2019 3:49:03 PM
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
    public class InterfaceType__Field__Description : InterfaceType__Field__Description_Cases
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            parentName = "InterfaceField";
            sb.Interface(InterfaceTypeName).Field(parentName, "String");
        }

        public override string InterfaceTypeName { get; } = nameof(InterfaceTypeName);
    }
}
