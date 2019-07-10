// Last generated: Wednesday, July 10, 2019 9:41:22 AM

using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen.Configuration
{
    public abstract class ScalarTypeNameTests : ScalarTypeNameTestsBase
    {
        public override ConfigurationSource GetElementConfigurationSource(IMutableNamed definition) =>
            definition.GetNameConfigurationSource();

        public override IMutableNamed GetParentDefinition(SchemaDefinition schemaDefinition, string parentName) =>
            schemaDefinition.GetScalar(parentName);

        public override INamed GetParent(Schema schema, string parentName) => schema.GetScalar(parentName);


        public override string ConventionalParentName { get; } = nameof(ConventionalScalarName);
        public override object ConventionalValue => ConventionalParentName;
        public override object DataAnnotationValue => DataAnnotationName;

        public const string DataAnnotationName = nameof(DataAnnotationName);

        public override string DataAnnotationParentName => nameof(NamedByDataAnnotation);

        public class ConventionalScalarName { }

        [GraphQLName(DataAnnotationName)]
        public class NamedByDataAnnotation { }

        public override void DefineByConvention(SchemaBuilder sb) => sb.Scalar<ConventionalScalarName>();

        public override void DefineByDataAnnotation(SchemaBuilder sb) => sb.Scalar<NamedByDataAnnotation>();
    }
}