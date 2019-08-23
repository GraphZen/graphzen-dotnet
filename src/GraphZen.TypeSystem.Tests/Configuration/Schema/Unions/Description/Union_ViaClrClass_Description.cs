using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Unions.Description
{
    public class Union_ViaClrClass_Description : Union_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(ExampleUnion),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Union<ExampleUnion>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Union<ExampleUnion>();
        }

        [Description(DataAnnotationDescriptionValue)]
        public class ExampleUnion
        {
        }
    }
}