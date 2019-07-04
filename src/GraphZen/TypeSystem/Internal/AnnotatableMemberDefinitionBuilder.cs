// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class AnnotatableMemberDefinitionBuilder<TDefinition> : MemberDefinitionBuilder<TDefinition>
        where TDefinition : AnnotatableMemberDefinition
    {
        protected AnnotatableMemberDefinitionBuilder([NotNull] TDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        public void AddOrUpdateDirectiveAnnotation([NotNull] string name, object value)
        {
            var existing = Definition.FindDirectiveAnnotation(name);
            if (existing == null)
            {
                Definition.AddDirectiveAnnotation(name, value);
            }
            else
            {
                Definition.UpdateDirectiveAnnotation(name, value);
            }
        }

        public void RemoveDirectiveAnnotation([NotNull] string name)
        {
            Definition.RemoveDirectiveAnnotation(name);
        }
    }
}