// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Types.Builders;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public class BlogMutationContext : BlogContext
    {
        protected internal override void OnSchemaCreating(SchemaBuilder schema)
        {
            base.OnSchemaCreating(schema);
            schema.MutationType("Mutation");
        }
    }
}