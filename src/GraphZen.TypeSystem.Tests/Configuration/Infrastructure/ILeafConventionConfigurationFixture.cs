﻿using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface ILeafConventionConfigurationFixture : ILeafConfigurationFixture
    {

        [NotNull]
        LeafConventionContext GetContext();


        void ConfigureContextConventionally([NotNull] SchemaBuilder sb);

        void ConfigureClrContext([NotNull] SchemaBuilder sb, string parentName);
    }
}