﻿using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Unions
{
    public class Schema_Unions_ViaClrBaseClass : Schema_Unions, ICollectionConventionConfigurationFixture
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
            public NamedByConvention ConventionallyNamed { get; set; }

            [GraphQLIgnore]
            public IgnoredByConvention IgnoredByConvention { get; set; }

            public IgnoredByDataAnnotation IgnoredByDataAnnotation { get; set; }

            public NamedByDataAnnotation NamedByDataAnnoation { get; set; }
        }

        public abstract class NamedByConvention
        {
        }


        [GraphQLName(DataAnnotationName)]
        public abstract class NamedByDataAnnotation
        {
        }

        public abstract class IgnoredByConvention
        {
        }

        [GraphQLIgnore]
        public abstract class IgnoredByDataAnnotation
        {
        }
    }
}