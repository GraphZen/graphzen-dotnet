#nullable disable
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Interfaces.Fields.Arguments.Description
{
    public class Interface_Field_Argument_ViaClrMethod_Description : Interface_Field_Argument_Description,
        ILeafConventionConfigurationFixture
    {
        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = "argName",
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

        [GraphQLName(GreatGrandparent)]
        public interface IExampleInterface
        {
            [GraphQLName(Grandparent)]
            string ExampleField([Description(DataAnnotationDescriptionValue)]
                string argName);
        }
    }
}