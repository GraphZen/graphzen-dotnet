// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.TypeSystem.Tests
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