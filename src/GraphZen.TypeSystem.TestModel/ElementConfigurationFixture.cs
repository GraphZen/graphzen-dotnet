// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
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
        public abstract void DefineParent(SchemaBuilder sb, string parentName);

        Member IElementConfigurationFixture.GetParent(Schema schema, string parentName) =>
            GetParent(schema, parentName);

        MemberDefinition IElementConfigurationFixture.GetParent(SchemaBuilder schemaBuilder, string parentName) =>
            GetParent(schemaBuilder, parentName);

        public virtual void DefineParentConventionally(SchemaBuilder sb, out string parentName) =>
            throw new NotImplementedException(NotImplementedMessage(nameof(DefineParentConventionally), false));

        public virtual void DefineParentConventionallyWithDataAnnotation(SchemaBuilder sb, out string parentName) =>
            throw new NotImplementedException(
                NotImplementedMessage(nameof(DefineParentConventionallyWithDataAnnotation), false));

        protected string NotImplementedMessage(string memberName, bool baseClass = true) =>
            $"implement '{memberName}' in type '{GetType().Name}{(baseClass ? "__Base" : "")}'";

        public abstract TParentMemberDefinition GetParent(SchemaBuilder schemaBuilder,
            string parentName);

        public abstract TParentMember GetParent(Schema schema, string parentName);
    }
}