// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen
{
    public class BlogSubscriptionContext : BlogContext
    {
        protected internal override void OnSchemaCreating(SchemaBuilder schema)
        {
            base.OnSchemaCreating(schema);
            schema.SubscriptionType("Subscription");
        }
    }
}