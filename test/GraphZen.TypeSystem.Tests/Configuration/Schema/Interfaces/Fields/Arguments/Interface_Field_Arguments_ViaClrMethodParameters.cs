// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields.Arguments
{
    // ReSharper disable once InconsistentNaming
    public class Interface_Field_Arguments_ViaClrMethodParameters : Interface_Field_Arguments,
        ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        [GraphQLName(Grandparent)]
        public interface ISomeInterface
        {
            string SomeField(
                string arg1,
                [GraphQLIgnore] string ignoreMe,
                [GraphQLName(DataAnnotationName)] string arg2);
        }

        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(ISomeInterface.SomeField).FirstCharToLower(),
                ItemIgnoredByDataAnnotation = "ignoreMe",
                ItemNamedByConvention = "arg1",
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = "na"
            };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Interface<ISomeInterface>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Interface<ISomeInterface>();
        }
    }
}