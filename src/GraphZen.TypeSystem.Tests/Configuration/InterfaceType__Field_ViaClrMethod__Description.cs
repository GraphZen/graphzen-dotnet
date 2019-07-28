// Last generated: Saturday, July 27, 2019 3:49:03 PM
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using System.ComponentModel;
using System.Configuration.Internal;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration
{
    public class InterfaceType__Field_ViaClrMethod__Description : InterfaceType__Field_ViaClrMethod__Description_Cases
    {
        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);
        public override string DataAnnotationValue => DataAnnotationDescription;

        public interface IInterfaceWithClrMethod
        {
            string HelloWorld();
            [Description(DataAnnotationDescription)]
            string HelloWorldWithDataAnnotation();
        }

        public override void ConfigureParentExplicitly(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            sb.Interface<IInterfaceWithClrMethod>();

            parentName = scenario == ConfigurationSource.Convention ? nameof(IInterfaceWithClrMethod.HelloWorld).FirstCharToLower() : scenario == ConfigurationSource.DataAnnotation ? nameof(IInterfaceWithClrMethod.HelloWorldWithDataAnnotation).FirstCharToLower() : throw new Exception();
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface<IInterfaceWithClrMethod>().Field(parentName, f => f.Description(null));
        }

        public override string InterfaceTypeName { get; } = nameof(IInterfaceWithClrMethod);
    }
}
