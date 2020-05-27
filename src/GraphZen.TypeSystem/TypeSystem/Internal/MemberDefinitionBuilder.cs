// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class MemberDefinitionBuilder<TDefinition> : MemberDefinitionBuilder
        where TDefinition : MemberDefinition
    {
        protected MemberDefinitionBuilder(TDefinition definition ) : base(
            definition )
        {
        }


        public new TDefinition Definition => (TDefinition)base.Definition;
    }


    public abstract class MemberDefinitionBuilder
    {
        protected MemberDefinitionBuilder(MemberDefinition definition)
        {
            Definition = definition;
        }

        public MemberDefinition Definition { get; }
        public bool RemoveDescription(ConfigurationSource configurationSource) => Definition.RemoveDescription(configurationSource);

        public SchemaDefinition Schema => Definition.Schema;
    }
}