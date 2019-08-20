using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Objects.Fields
{
    public class ObjectFields_ViaClrProperties : ObjectFields, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        [GraphQLIgnore]
        public class IgnoredType
        {

        }
        public class ExampleObject
        {
            public string HelloWorld { get; set; }
            [GraphQLName(DataAnnotationName)]
            public string NamedByDataAnnotation { get; set; }
            [GraphQLIgnore]
            public string IgnoredByDataAnnotation { get; set; }
            public IgnoredType IgnoredByConvention { get; set; }
        }

        public CollectionConventionContext ConfigureViaConvention(SchemaBuilder sb)
        {
            sb.Object<ExampleObject>();
            return new CollectionConventionContext
            {
                ParentName = nameof(ExampleObject),
                ItemNamedByConvention = nameof(ExampleObject.HelloWorld).FirstCharToLower(),
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = nameof(ExampleObject.IgnoredByConvention),
                ItemIgnoredByDataAnnotation = nameof(ExampleObject.IgnoredByDataAnnotation).FirstCharToLower()
            };
        }
    }
}