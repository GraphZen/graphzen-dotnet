#nullable disable
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Enums.EnumValues.Description
{
    public class EnumValue_ViaClrEnumValue_Description : EnumValue_Description, ILeafConventionConfigurationFixture
    {
        [GraphQLName(Grandparent)]
        public enum ExampleEnum
        {
            [Description(DataAnnotationDescriptionValue)]
            ExampleEnumValue
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(ExampleEnum.ExampleEnumValue),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Enum<ExampleEnum>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Enum<ExampleEnum>();
        }
    }
}