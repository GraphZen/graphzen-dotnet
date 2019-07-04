// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;


namespace GraphZen.Validation.Rules
{
    public class FieldArgsMustBeProperlyNamed : SchemaValidationRuleVisitor
    {
        public FieldArgsMustBeProperlyNamed(SchemaValidationContext context) : base(context)
        {
        }
    }
}