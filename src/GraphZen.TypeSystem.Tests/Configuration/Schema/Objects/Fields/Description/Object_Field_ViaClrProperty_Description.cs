using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Objects.Fields.Description
{
    public class Object_Field_ViaClrProperty_Description : Object_Field_Description, ILeafConventionConfigurationFixture
    {
        [GraphQLName(Grandparent)]
        public class ExampleObject
        {
            [Description(DataAnnotationDescriptionValue)]
            public string ExampleField { get; set; }
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = nameof(ExampleObject.ExampleField).FirstCharToLower(),
            DataAnnotationValue = DataAnnotationDescriptionValue
        };

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