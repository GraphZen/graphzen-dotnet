// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Configuration.Objects.Fields
{
    public class Object_Fields_ViaClrMethods : Object_Fields, ICollectionConventionConfigurationFixture
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
            public string HelloWorld() => throw new NotImplementedException();

            [GraphQLName(DataAnnotationName)]
            public string NamedByDataAnnotation() => throw new NotImplementedException();

            [GraphQLIgnore]
            public string IgnoredByDataAnnotation() => throw new NotImplementedException();

            public IgnoredType IgnoredByConvention() => throw new NotImplementedException();
        }

        public override string ToString() => nameof(Object_Fields_ViaClrMethods);
    }
}