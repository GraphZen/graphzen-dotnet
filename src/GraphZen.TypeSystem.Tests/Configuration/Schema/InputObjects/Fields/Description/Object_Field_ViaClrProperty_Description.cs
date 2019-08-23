using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.InputObjects.InputFields.Description;
using GraphZen.TypeSystem;

namespace GraphZen.InputObjects.Fields.Description
{
    public class InputObject_Field_ViaClrProperty_Description : InputObject_Field_Description, ILeafConventionConfigurationFixture
    {
        [GraphQLName(Grandparent)]
        public class ExampleInputObject
        {
            [Description(DataAnnotationDescriptionValue)]
            public string ExampleField { get; set; }
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(ExampleInputObject.ExampleField).FirstCharToLower(),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.InputObject<ExampleInputObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.InputObject<ExampleInputObject>();
        }
    }
}