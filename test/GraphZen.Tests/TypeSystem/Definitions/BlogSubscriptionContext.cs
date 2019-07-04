// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
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