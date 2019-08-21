using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.Unions
{
    public class Schema_Unions_ViaClrMarkerInterface : Schema_Unions, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        public CollectionConventionContext GetContext() => new CollectionConventionContext
        {
            ItemNamedByConvention = nameof(INamedByConvention),
            DefaultItemConfigurationSource = ConfigurationSource.DataAnnotation,
            ItemNamedByDataAnnotation = DataAnnotationName,
            ItemIgnoredByConvention = nameof(IIgnoredByConvention),
            ItemIgnoredByDataAnnotation = nameof(IIgnoredByDataAnnotation)
        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<Query>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<Query>();
        }

        public class Foo { }
        public class Bar { }
        public class Baz { }



        public class Query
        {
            public INamedByConvention ConventionallyNamed { get; set; }

            [GraphQLIgnore]
            public IIgnoredByConvention IgnoredByConvention { get; set; }

            public IIgnoredByDataAnnotation IgnoredByDataAnnotation { get; set; }

            public INamedByDataAnnotation NamedByDataAnnoation { get; set; }
        }

        [GraphQLUnion]
        public interface INamedByConvention
        {
        }


        [GraphQLUnion]
        [GraphQLName(DataAnnotationName)]
        public interface INamedByDataAnnotation
        {
        }

        [GraphQLUnion]
        public interface IIgnoredByConvention
        {
        }

        [GraphQLIgnore]
        [GraphQLUnion]
        public interface IIgnoredByDataAnnotation
        {
        }
    }
}