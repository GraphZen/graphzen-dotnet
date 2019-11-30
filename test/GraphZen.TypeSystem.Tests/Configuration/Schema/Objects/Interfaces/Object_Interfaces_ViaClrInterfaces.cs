// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Interfaces
{
    public class Object_Interfaces_ViaClrInterfaces : Object_Interfaces, ICollectionConventionConfigurationFixture
    {
        public class Query
        {
            public ObjectWithInterfaces? ObjectWithInterfaces { get; set; }
        }

        public class ObjectWithInterfaces : INamedByConvention, INamedByDataAnnotation, IIgnoredByDataAnnotation
        {
        }

        public interface INamedByConvention
        {
        }


        [GraphQLName(DataAnnotationName)]
        public interface INamedByDataAnnotation
        {
        }

        public interface IIgnoredByConvention
        {
        }

        [GraphQLIgnore]
        public interface IIgnoredByDataAnnotation
        {
        }


        public const string DataAnnotationName = nameof(DataAnnotationName);


        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(ObjectWithInterfaces),
                ItemNamedByConvention = nameof(INamedByConvention),
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
            sb.Object<ObjectWithInterfaces>();
        }
    }
}