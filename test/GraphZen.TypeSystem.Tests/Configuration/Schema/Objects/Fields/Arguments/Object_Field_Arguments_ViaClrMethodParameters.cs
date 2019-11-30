using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Configuration.Objects.Fields.Arguments
{
    public class Object_Field_Arguments_ViaClrMethodParameters : Object_Field_Arguments,
        ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        [GraphQLName(Grandparent)]
        public class SomeObject
        {
            public string SomeField(
                string arg1,
                [GraphQLIgnore] string ignoreMe,
                [GraphQLName(DataAnnotationName)] string arg2) =>
                throw new NotImplementedException();
        }

        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(SomeObject.SomeField).FirstCharToLower(),
                ItemIgnoredByDataAnnotation = "ignoreMe",
                ItemNamedByConvention = "arg1",
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = "na"
            };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<SomeObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<SomeObject>();
        }
    }
}