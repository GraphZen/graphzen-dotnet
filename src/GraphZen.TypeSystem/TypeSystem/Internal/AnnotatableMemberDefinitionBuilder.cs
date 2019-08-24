// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class AnnotatableMemberDefinitionBuilder<TDefinition> : MemberDefinitionBuilder<TDefinition>
        where TDefinition : AnnotatableMemberDefinition
    {
        protected AnnotatableMemberDefinitionBuilder( TDefinition definition,
             InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        public void AddOrUpdateDirectiveAnnotation( string name, object value)
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

        public void RemoveDirectiveAnnotation( string name)
        {
            Definition.RemoveDirectiveAnnotation(name);
        }
    }
}