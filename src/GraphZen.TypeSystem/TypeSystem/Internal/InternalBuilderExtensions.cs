// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public static class InternalBuilderExtensions
    {
        public static void SetName<TDefinition>(this MemberDefinitionBuilder<TDefinition> builder,
            string name, ConfigurationSource configurationSource)
            where TDefinition : MemberDefinition, IMutableNamed
        {
            
            builder.Definition.SetName(name, configurationSource);
        }

        public static void Description<TDefinition>(this MemberDefinitionBuilder<TDefinition> builder,
            string? description, ConfigurationSource configurationSource)
            where TDefinition : MemberDefinition, IMutableDescription
        {
            builder.Definition.SetDescription(description, configurationSource);
        }
    }
}