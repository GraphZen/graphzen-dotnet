using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Interfaces.Fields.Description
{
    public class Interface_Field_ViaClrProperty_Description : Interface_Field_Description, ILeafConventionConfigurationFixture
    {
        [GraphQLName(Grandparent)]
        public interface IExampleInterface
        {
            [Description(DataAnnotationDescriptionValue)]
            string ExampleField { get; set; }
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(IExampleInterface.ExampleField).FirstCharToLower(),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Interface<IExampleInterface>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Interface<IExampleInterface>();
        }
    }
}