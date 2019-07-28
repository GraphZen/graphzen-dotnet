// Last generated: Saturday, July 27, 2019 3:49:03 PM
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using System.Configuration.Internal;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration
{
    public class InterfaceType__Field_ViaClrMethod__Description : InterfaceType__Field_ViaClrMethod__Description_Cases
    {

        public interface IInterfaceWithClrMethod
        {
            string HelloWorld();
        }

        public override void ConfigureParentExplicitly(SchemaBuilder sb, out string parentName)
        {
            sb.Interface<IInterfaceWithClrMethod>();
            parentName = nameof(IInterfaceWithClrMethod.HelloWorld).FirstCharToLower();
        }

        public override string InterfaceTypeName { get; } = nameof(IInterfaceWithClrMethod);
    }
}
