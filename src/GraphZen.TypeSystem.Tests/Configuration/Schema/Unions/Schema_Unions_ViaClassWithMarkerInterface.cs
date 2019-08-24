using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.Unions
{
    public class Schema_Unions_ViaClassWithMarkerInterface : Schema_Unions, ICollectionConventionConfigurationFixture
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

        public class Foo : INamedByConvention { }
        [GraphQLIgnore]
        public class Bar : IIgnoredByConvention { }
        public class Baz : IIgnoredByDataAnnotation { }


        public class FooBar : INamedByDataAnnotation { }

        public class Query
        {
            public Foo ConventionallyNamed { get; set; }

            public Bar IgnoredByConvention { get; set; }

            public Baz IgnoredByDataAnnotation { get; set; }

            public FooBar NamedByDataAnnoation { get; set; }
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