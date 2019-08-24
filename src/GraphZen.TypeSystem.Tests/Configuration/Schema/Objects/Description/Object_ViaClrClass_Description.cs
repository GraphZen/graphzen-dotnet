#nullable disable
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Objects
{

    public class Object_ViaClrClass_Description : Object_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(ExampleObject),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

        [Description(DataAnnotationDescriptionValue)]
        public class ExampleObject
        {

        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<ExampleObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<ExampleObject>();
        }
    }
}