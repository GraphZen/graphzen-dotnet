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
            ConventionallyDefinedWithDataAnnotation,
            ConventionallyDefined
        }

        public override void DefineParentConventionally(SchemaBuilder sb, out string parentName)
        {
            sb.Enum<ExampleClrEnum>();
            parentName = nameof(ExampleClrEnum.ConventionallyDefined);

        }

        public override void DefineParentConventionallyWithDataAnnotation(SchemaBuilder sb, out string parentName)
        {
            sb.Enum<ExampleClrEnum>();
            parentName = nameof(ExampleClrEnum.ConventionallyDefinedWithDataAnnotation);
        }
    }
}
