// Last generated: Saturday, July 27, 2019 3:49:03 PM
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration
{
    public class InterfaceType__Field_ViaClrProperty__Description : InterfaceType__Field_ViaClrProperty__Description_Cases
    {
        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);
        public override string DataAnnotationValue => DataAnnotationDescription;

        public interface IInterfaceWithClrProperty
        {
            string HelloWorld();

            [Description(DataAnnotationDescription)]
            string WithDataAnnotation();
        }

        public override void ConfigureParentExplicitly(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            sb.Interface<IInterfaceWithClrProperty>();
            if (scenario == ConfigurationSource.DataAnnotation)
            {
                parentName = nameof(IInterfaceWithClrProperty.WithDataAnnotation).FirstCharToLower();
            }
            else if (scenario == ConfigurationSource.Convention)
            {
                parentName = nameof(IInterfaceWithClrProperty.HelloWorld).FirstCharToLower();
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(scenario), scenario, null);
            }
        }

        public override string InterfaceTypeName => nameof(IInterfaceWithClrProperty);
    }
}
