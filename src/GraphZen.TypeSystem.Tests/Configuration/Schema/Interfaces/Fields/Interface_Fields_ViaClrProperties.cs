﻿using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Interfaces.Fields
{
    public class Interface_Fields_ViaClrProperties : Interface_Fields, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(IExampleInterface),
                ItemNamedByConvention = nameof(IExampleInterface.HelloWorld).FirstCharToLower(),
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = nameof(IExampleInterface.IgnoredByConvention),
                ItemIgnoredByDataAnnotation = nameof(IExampleInterface.IgnoredByDataAnnotation).FirstCharToLower()
            };


        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Interface<IExampleInterface>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Interface(parentName).ClrType<IExampleInterface>();
        }


        [GraphQLIgnore]
        public class IgnoredType
        {
        }

        public interface IExampleInterface
        {
            string HelloWorld { get; set; }

            [GraphQLName(DataAnnotationName)]
            string NamedByDataAnnotation { get; set; }

            [GraphQLIgnore]
            string IgnoredByDataAnnotation { get; set; }

            IgnoredType IgnoredByConvention { get; set; }
        }
    }
}