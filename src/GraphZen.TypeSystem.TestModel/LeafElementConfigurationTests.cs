using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public abstract class LeafElementConfigurationTests<TMarker, TMutableMarker, TParentMemberDefinition, TParentMember>
        where TMutableMarker : TMarker
        where TParentMemberDefinition : MemberDefinition, TMutableMarker
        where TParentMember : Member, TMarker
    {
        public virtual object ConventionalValue => throw new NotImplementedException();

        public virtual object DataAnnotationValue => throw new NotImplementedException();

        public virtual object ExplicitValue => throw new NotImplementedException();

        public virtual string ConventionalParentName => throw new NotImplementedException();

        public virtual void DefineByConvention(SchemaBuilder sb) => throw new NotImplementedException();

        public abstract ConfigurationSource GetElementConfigurationSource(TMutableMarker definition);

        public abstract TMutableMarker GetParentDefinition(SchemaDefinition schemaDefinition, string parentName);

        public abstract TMarker GetParent(Schema schema, string parentName);

        public abstract object GetValue(TMarker parent);

        public virtual void defined_by_convention()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByConvention(sb);
                var parentDef = GetParentDefinition(sb.GetDefinition(), ConventionalParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                GetValue(parentDef).Should().Be(ConventionalValue);
            });
            var parent = GetParent(schema, ConventionalParentName);
            GetValue(parent).Should().Be(ConventionalValue);
        }
    }
}