﻿using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface ICollectionConventionConfigurationFixture : ICollectionConfigurationFixture
    {
        CollectionConventionContext GetContext();
        void ConfigureParentConventionally([NotNull] SchemaBuilder sb);
    }
}