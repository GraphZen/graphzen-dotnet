// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using System.ComponentModel;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration
{
    public class EnumType__EnumValue_ViaClrEnumValue__Description : EnumType__EnumValue_ViaClrEnumValue__Description_Cases
    {
        public override string GrandparentName => nameof(ExampleClrEnum);

        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);
        public override string DataAnnotationValue => DataAnnotationDescription;

        public enum ExampleClrEnum
        {
            [Description(DataAnnotationDescription)]
            Foo,
            Bar
        }

        public override void ConfigureParent(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            sb.Enum<ExampleClrEnum>();
            switch (scenario)
            {
                case ConfigurationSource.DataAnnotation:
                    parentName = nameof(ExampleClrEnum.Foo);
                    break;
                case ConfigurationSource.Convention:
                    parentName = nameof(ExampleClrEnum.Bar);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scenario), scenario, null);
            }
        }
    }
}
