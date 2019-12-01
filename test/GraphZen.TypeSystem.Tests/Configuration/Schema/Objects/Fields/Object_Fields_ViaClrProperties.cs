// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Fields
{
    public class Object_Fields_ViaClrProperties : Object_Fields, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(ExampleObject),
                ItemNamedByConvention = nameof(ExampleObject.HelloWorld).FirstCharToLower(),
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = nameof(ExampleObject.IgnoredByConvention),
                ItemIgnoredByDataAnnotation = nameof(ExampleObject.IgnoredByDataAnnotation).FirstCharToLower()
            };


        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<ExampleObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object(parentName).ClrType<ExampleObject>();
        }


        [GraphQLIgnore]
        public class IgnoredType
        {
        }

        public class ExampleObject
        {
            public string? HelloWorld { get; set; }

            [GraphQLName(DataAnnotationName)] public string? NamedByDataAnnotation { get; set; }

            [GraphQLIgnore] public string? IgnoredByDataAnnotation { get; set; }

            public IgnoredType? IgnoredByConvention { get; set; }
        }
    }
}