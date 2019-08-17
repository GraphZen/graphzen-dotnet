using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public abstract class ElementConfigurationFixture<
        TMarker,
        TDefMarker,
        TMutableDefMarker,
        TParentMemberDefinition,
        TParentMember> : IElementConfigurationFixture
        where TMarker : TDefMarker
        where TMutableDefMarker : TDefMarker
        where TParentMemberDefinition : MemberDefinition, TMutableDefMarker
        where TParentMember : Member, TMarker

    {
        public abstract void DefineParent([NotNull] SchemaBuilder sb, [NotNull] string parentName);

        Member IElementConfigurationFixture.GetParent([NotNull] Schema schema, [NotNull] string parentName) =>
            GetParent(schema, parentName);

        MemberDefinition IElementConfigurationFixture.GetParent([NotNull] SchemaBuilder schemaBuilder,
            [NotNull] string parentName) =>
            GetParent(schemaBuilder, parentName);

        public virtual void DefineParentConventionally([NotNull] SchemaBuilder sb, [NotNull] out string parentName) =>
            throw new NotImplementedException(NotImplementedMessage(nameof(DefineParentConventionally), false));

        public virtual void DefineParentConventionallyWithDataAnnotation([NotNull] SchemaBuilder sb,
            [NotNull] out string parentName) =>
            throw new NotImplementedException(
                NotImplementedMessage(nameof(DefineParentConventionallyWithDataAnnotation), false));

        protected string NotImplementedMessage(string memberName, bool baseClass = true) =>
            $"implement '{memberName}' in type '{GetType().Name}{(baseClass ? "__Base" : "")}'";

        [NotNull]
        public abstract TParentMemberDefinition GetParent([NotNull] SchemaBuilder schemaBuilder,
            [NotNull] string parentName);

        [NotNull]
        public abstract TParentMember GetParent([NotNull] Schema schema, [NotNull] string parentName);
    }
}