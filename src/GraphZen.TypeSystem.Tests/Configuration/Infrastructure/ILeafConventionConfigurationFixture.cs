// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface ILeafConventionConfigurationFixture : ILeafConfigurationFixture
    {

        
        LeafConventionContext GetContext();


        void ConfigureContextConventionally( SchemaBuilder sb);

        void ConfigureClrContext( SchemaBuilder sb, string parentName);
    }
}