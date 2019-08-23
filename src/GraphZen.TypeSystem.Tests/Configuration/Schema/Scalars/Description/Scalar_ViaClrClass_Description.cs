using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Scalars.Description
{

    public class Scalar_ViaClrClass_Description : Scalar_Description, ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(ExampleScalar),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

        [Description(DataAnnotationDescriptionValue)]
        public class ExampleScalar
        {

        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Scalar<ExampleScalar>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Scalar<ExampleScalar>();
        }
    }
}