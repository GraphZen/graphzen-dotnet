using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Objects.Fields
{
    public class ObjectFields_ViaClrProperties : ObjectFields, IConventionalCollectionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        public class ExampleObject
        {
            public string HelloWorld { get; set; }
            [GraphQLName(DataAnnotationName)]
            public string NamedByDataAnnotation { get; set; }
            [GraphQLIgnore]
            public string Ignored { get; set; }
        }


        public CollectionConventionContext ConfigureViaConvention(SchemaBuilder sb)
        {
            sb.Object<ExampleObject>();
            return new CollectionConventionContext()
            {
                ParentName = nameof(ExampleObject),
                ItemNamedByConvention = nameof(ExampleObject.HelloWorld).FirstCharToLower()

            };
        }
    }
}