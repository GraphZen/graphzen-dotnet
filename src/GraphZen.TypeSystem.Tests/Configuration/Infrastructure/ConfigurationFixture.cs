// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public abstract class ConfigurationFixture<
        TMarker,
        TDefMarker,
        TMutableDefMarker,
        TParentMemberDefinition,
        TParentMember> : IConfigurationFixture
        where TMarker : TDefMarker
        where TMutableDefMarker : TDefMarker
        where TParentMemberDefinition : MemberDefinition, TMutableDefMarker
        where TParentMember : Member, TMarker

    {
        protected const string Grandparent = nameof(Grandparent);
        protected const string GreatGrandparent = nameof(GreatGrandparent);

        

        public Type ParentMemberType { get; } = typeof(TParentMember);
        public Type ParentMemberDefinitionType { get; } = typeof(TParentMemberDefinition);
        public abstract void ConfigureParentExplicitly(SchemaBuilder sb, string parentName);

        Member IConfigurationFixture.GetParent(Schema schema, string parentName) =>
            GetParent(schema, parentName);

        MemberDefinition IConfigurationFixture.GetParent(SchemaBuilder sb,
            string parentName) =>
            GetParent(sb, parentName);

        //public virtual void DefineParentConventionally( SchemaBuilder sb,  out string parentName) =>
        //    throw new NotImplementedException(NotImplementedMessage(nameof(DefineParentConventionally), false));

        //public virtual void DefineParentConventionallyWithDataAnnotation( SchemaBuilder sb,
        //     out string parentName) =>
        //    throw new NotImplementedException(
        //        NotImplementedMessage(nameof(DefineParentConventionallyWithDataAnnotation), false));

        protected string NotImplementedMessage(string memberName, bool baseClass = true) =>
            $"implement '{memberName}' in type '{GetType().Name}{(baseClass ? "__Base" : "")}'";

        
        public abstract TParentMemberDefinition GetParent( SchemaBuilder sb,
             string parentName);

        
        public abstract TParentMember GetParent( Schema schema,  string parentName);
        
    }
}