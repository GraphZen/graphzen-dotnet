using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.InputObjects
{
    public class Schema_InputObjects_ViaClrClasses : Schema_InputObjects, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        public CollectionConventionContext GetContext() => new CollectionConventionContext
        {
            ItemNamedByConvention = nameof(NamedByConvention),
            ItemNamedByDataAnnotation = DataAnnotationName,
            ItemIgnoredByConvention = nameof(IgnoredByConvention),
            ItemIgnoredByDataAnnotation = nameof(IgnoredByDataAnnotation)
        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.InputObject<ParentInputObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.InputObject<ParentInputObject>();
        }

        public void AddItemNamedByDataAnnotationViaClrType(SchemaBuilder sb)
        {
            throw new System.NotImplementedException();
        }

        public class ParentInputObject
        {
            public NamedByConvention ConventionallyNamed { get; set; }

            [GraphQLIgnore]
            public IgnoredByConvention IgnoredByConvention { get; set; }

            public IgnoredByDataAnnotation IgnoredByDataAnnotation { get; set; }

            public NamedByDataAnnotation NamedByDataAnnoation { get; set; }
        }

        public class NamedByConvention
        {
        }


        [GraphQLName(DataAnnotationName)]
        public class NamedByDataAnnotation
        {
        }

        public class IgnoredByConvention
        {
        }

        [GraphQLIgnore]
        public class IgnoredByDataAnnotation
        {
        }
    }
}