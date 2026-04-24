// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class |
                AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Enum |
                AttributeTargets.Field)]
public class GraphQLIgnoreAttribute : Attribute
{
}
