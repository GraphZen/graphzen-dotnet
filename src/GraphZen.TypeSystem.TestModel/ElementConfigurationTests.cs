#nullable disable
using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public abstract class ElementConfigurationTests<TMarker,
        TMutableMarker,
        TParentMemberDefinition,
        TParentMember>
        where TMutableMarker : TMarker
        where TParentMemberDefinition : MemberDefinition, TMutableMarker
        where TParentMember : Member, TMarker

    {
        public abstract void DefineParentExplicitly(SchemaBuilder sb, out string parentName);

        public virtual void DefineParentConventionally(SchemaBuilder sb, out string parentName) =>
            throw new NotImplementedException(NotImplementedMessage(nameof(DefineParentConventionally), false));

        public virtual void DefineParentConventionallyWithDataAnnotation(SchemaBuilder sb, out string parentName) =>
            throw new NotImplementedException(
                NotImplementedMessage(nameof(DefineParentConventionallyWithDataAnnotation), false));

        protected string NotImplementedMessage(string memberName, bool baseClass = true) =>
            $"implement '{memberName}' in type '{GetType().Name}{(baseClass ? "__Base" : "")}'";

        public abstract TParentMemberDefinition GetParentDefinitionByName(SchemaDefinition schemaDefinition,
            string parentName);

        public abstract TParentMember GetParentByName(Schema schema, string parentName);
    }
}