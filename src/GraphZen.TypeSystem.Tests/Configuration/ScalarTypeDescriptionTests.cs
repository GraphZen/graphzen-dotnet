// Last generated: Wednesday, July 10, 2019 10:06:54 AM
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute


using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Configuration
{
    public class ScalarTypeDescriptionTests : ScalarTypeDescriptionTestsBase
    {
        public const string DataAnnotationDescription = "description from data annotation";
        public override object DataAnnotationValue => DataAnnotationDescription;
        public override string DataAnnotationParentName => nameof(ScalarDescriptionViaDataAnnotation);
        public override string NotDefinedByConventionParentName => nameof(ScalarNoDescription);

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();

        public override IMutableDescription GetParentDefinition(SchemaDefinition schemaDefinition, string parentName) =>
            schemaDefinition.GetScalar(parentName);

        public override IDescription GetParent(Schema schema, string parentName) => schema.GetScalar(parentName);

        public override bool TryGetValue(IDescription parent, out object value)
        {
            value = parent.Description;
            return parent.Description != null;
        }

        public override void DefineByDataAnnotation(SchemaBuilder sb)
        {
            sb.Scalar<ScalarDescriptionViaDataAnnotation>();
        }

        public override void DefineEmptyByConvention(SchemaBuilder sb)
        {
            sb.Scalar<ScalarNoDescription>();
        }

        public class ScalarNoDescription
        {
        }

        [Description(DataAnnotationDescription)]
        public class ScalarDescriptionViaDataAnnotation
        {
        }
    }
}