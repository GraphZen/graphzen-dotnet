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
    public class EnumType__EnumValue__Description : EnumType__EnumValue__Description_Cases
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, out string parentName,
            ConfigurationSource scenario)
        {
            parentName = ExplicitParentName;
            sb.Enum(Grandparent).Value(ExplicitParentName);
        }
    }
}
