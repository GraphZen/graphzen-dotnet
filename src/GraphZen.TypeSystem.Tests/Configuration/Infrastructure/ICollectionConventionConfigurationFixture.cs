// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen
{
    public interface ICollectionConventionConfigurationFixture : ICollectionConfigurationFixture
    {
        CollectionConventionContext GetContext();
        void ConfigureContextConventionally(SchemaBuilder sb);

        void ConfigureClrContext(SchemaBuilder sb, string parentName);

        //void AddItemNamedByDataAnnotationViaClrType(SchemaBuilder sb);
    }
}