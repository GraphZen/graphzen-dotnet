#nullable disable
using System;
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Objects.Fields.Arguments.Description
{

    public class Object_Field_Argument_ViaClrMethod_Description : Object_Field_Argument_Description, ILeafConventionConfigurationFixture
    {
        [GraphQLName(GreatGrandparent)]
        public class ExampleObject
        {
            [GraphQLName(Grandparent)]
            public string ExampleField([Description(DataAnnotationDescriptionValue)]
                string argName) => throw new NotImplementedException();
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() => new LeafConventionContext
        {
            ParentName = "argName",
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