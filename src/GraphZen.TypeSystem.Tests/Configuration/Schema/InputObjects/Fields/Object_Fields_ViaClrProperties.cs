﻿using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.InputObjects.Fields
{
    public class InputObject_Fields_ViaClrProperties : InputObject_Fields, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(ExampleInputObject),
                ItemNamedByConvention = nameof(ExampleInputObject.HelloWorld).FirstCharToLower(),
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = nameof(ExampleInputObject.IgnoredByConvention),
                ItemIgnoredByDataAnnotation = nameof(ExampleInputObject.IgnoredByDataAnnotation).FirstCharToLower()
            };


        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.InputObject<ExampleInputObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(parentName).ClrType<ExampleInputObject>();
        }


        [GraphQLIgnore]
        public class IgnoredType
        {
        }

        public class ExampleInputObject
        {
            public string HelloWorld { get; set; }

            [GraphQLName(DataAnnotationName)]
            public string NamedByDataAnnotation { get; set; }

            [GraphQLIgnore]
            public string IgnoredByDataAnnotation { get; set; }

            public IgnoredType IgnoredByConvention { get; set; }
        }
    }
}