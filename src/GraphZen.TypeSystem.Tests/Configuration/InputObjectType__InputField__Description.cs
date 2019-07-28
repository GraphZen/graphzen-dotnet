// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective

using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

namespace GraphZen.Configuration
{
    public class InputObjectType__InputField__Description : InputObjectType__InputField__Description_Cases
    {
        public override void ConfigureParent(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            parentName = ExplicitParentName;
            sb.InputObject(GrandparentName).Field(ExplicitParentName, "String");
        }
    }
}