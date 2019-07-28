// Last generated: Saturday, July 27, 2019 3:49:03 PM
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
    public class InputObjectType__Description : InputObjectType__Description_Cases
    {
        public override void ConfigureParent(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            parentName = ExplicitParentName;
            sb.InputObject(parentName);
        }
    }
}