// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable


namespace GraphZen
{
    public abstract class LeafElementConfigurationFixture<TMarker, TDefMarker, TMutableDefMarker, TElement,
        TParentMemberDefinition, TParentMember
    > :
        ConfigurationFixture<TMarker, TDefMarker, TMutableDefMarker, TParentMemberDefinition, TParentMember>,
        ILeafConfigurationFixture
        where TMutableDefMarker : TDefMarker
        where TParentMemberDefinition : MemberDefinition, TMutableDefMarker
        where TParentMember : Member, TMarker
        where TMarker : IConfigurationElement<TElement>, TDefMarker
    {
        //public virtual TElement ConventionalValue =>
        //    throw new NotImplementedException($"implement '{nameof(ConventionalValue)}' in type '{GetType().Name}'");

        //public virtual TElement DataAnnotationValue =>
        //    throw new NotImplementedException(NotImplementedMessage(nameof(DataAnnotationValue), false));

        //public virtual bool DefinedByConvention =>
        //    throw new NotImplementedException("implemented automatically by generated code");

        //public virtual bool DefinedByDataAnnotation =>
        //    throw new NotImplementedException("implemented automatically by generated code");


        //public abstract TElement ValueA { get; }
        //public abstract TElement ValueB { get; }
        //public abstract TElement ValueC { get; }

        //public virtual string GrandparentName { get; } = "Grandparent";
        //public virtual string GreatGrandparentName { get; } = "GreatGrandparent";


        //public void DefineParents(SchemaBuilder sb, out List<string> parentNames)
        //{
        //    parentNames = new List<string>();
        //    DefineParentExplicitly(sb, out var explicitParentName);
        //    parentNames.Add(explicitParentName);

        //    if (DefinedByConvention)
        //    {
        //        DefineParentConventionally(sb, out var cp);
        //        parentNames.Add(cp);
        //    }

        //    if (DefinedByDataAnnotation)
        //    {
        //        DefineParentConventionallyWithDataAnnotation(sb, out var cpa);
        //        parentNames.Add(cpa);
        //    }
        //}

        //public abstract void ConfigureExplicitly(SchemaBuilder sb, string parentName, TElement value);

        //public virtual void RemoveExplicitly(SchemaBuilder sb, string parentName) =>
        //    throw new NotImplementedException(NotImplementedMessage(nameof(RemoveExplicitly)));


        //public abstract ConfigurationSource GetElementConfigurationSource(TMutableDefMarker definition);


        //public abstract bool TryGetValue(TMarker parent, out TElement value);
        public abstract ConfigurationSource GetElementConfigurationSource(TMutableDefMarker parent);

        public ConfigurationSource GetElementConfigurationSource(MemberDefinition parent)
        {
            return GetElementConfigurationSource((TMutableDefMarker)(parent as TParentMemberDefinition));
        }

        public bool TryGetValue(MemberDefinition parent, out object value)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var result = TryGetValue(parent as TParentMemberDefinition, out var inner);
            value = inner;
            return result;
        }

        public bool TryGetValue(Member parent, out object value)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var result = TryGetValue(parent as TParentMember, out var inner);
            value = inner;
            return result;
        }

        public void ConfigureExplicitly(SchemaBuilder sb, string parentName, object value)
        {
            ConfigureExplicitly(sb, parentName, (TElement)value);
        }

        public abstract void RemoveValue(SchemaBuilder sb, string parentName);


        object ILeafConfigurationFixture.ValueA => ValueA;

        object ILeafConfigurationFixture.ValueB => ValueB;

        public abstract void ConfigureExplicitly(SchemaBuilder sb, string parentName, TElement value);


        public abstract TElement ValueA { get; }
        public abstract TElement ValueB { get; }


        public abstract bool TryGetValue(TParentMember parent, out TElement value);

        public abstract bool TryGetValue(TParentMemberDefinition parent, out TElement value);
    }
}