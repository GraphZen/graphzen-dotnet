// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration {
public class EnumType__Description : EnumType__Description_Cases
{
    public override string ExplicitParentName { get; } = nameof(ExplicitParentName);

    public override void ConfigureParent(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
    {
        parentName = ExplicitParentName;
        sb.Enum(parentName);
    }

    public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
    {
        sb.Enum(parentName).Description(null);
    }
}
}
