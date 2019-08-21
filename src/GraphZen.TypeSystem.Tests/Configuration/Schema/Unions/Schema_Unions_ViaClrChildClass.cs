﻿using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Unions
{
    public class Schema_Unions_ViaClrChildClass : Schema_Unions, ICollectionConventionConfigurationFixture
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
            sb.Object<Query>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<Query>();
        }

        public class Query
        {
            public Foo Foo { get; set; }

            public Bar Bar { get; set; }

            public Baz Baz { get; set; }

            public FooBar FooBar { get; set; }
        }


        public class Foo : NamedByConvention { }
        public abstract class NamedByConvention
        {
        }


        [GraphQLName(DataAnnotationName)]
        public abstract class NamedByDataAnnotation
        {
        }

        public class Bar : NamedByDataAnnotation { }

        public abstract class IgnoredByConvention
        {
        }

        [GraphQLIgnore]
        public class Baz : IgnoredByConvention { }


        public class FooBar : IgnoredByDataAnnotation { }
        [GraphQLIgnore]
        public abstract class IgnoredByDataAnnotation
        {
        }
    }
}