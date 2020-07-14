// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class InternalMemberDefinitionBuilder<TDefinition> : InternalMemberDefinitionBuilder
        where TDefinition : MutableMember
    {
        protected InternalMemberDefinitionBuilder(TDefinition definition) : base(
            definition)
        {
        }


        public new TDefinition Definition => (TDefinition)base.Definition;
    }


    public abstract class InternalMemberDefinitionBuilder
    {
        protected InternalMemberDefinitionBuilder(MutableMember definition)
        {
            Definition = definition;
        }

        public MutableMember Definition { get; }

        public bool RemoveDescription(ConfigurationSource configurationSource) =>
            Definition.RemoveDescription(configurationSource);

        public MutableSchema Schema => Definition.Schema;
    }
}