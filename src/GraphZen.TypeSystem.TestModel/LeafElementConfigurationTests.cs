// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public abstract class LeafElementConfigurationTests<TMarker, TMutableMarker, TParentMemberDefinition, TParentMember, TElement>
        where TMutableMarker : TMarker
        where TParentMemberDefinition : MemberDefinition, TMutableMarker
        where TParentMember : Member, TMarker
        where TMarker : IConfigurationElement<TElement>
    {
        public virtual TElement ConventionalValue =>
            throw new NotImplementedException($"implement '{nameof(ConventionalValue)}' in type '{GetType().Name}'");

        public virtual TElement DataAnnotationValue =>
            throw new NotImplementedException($"implement '{nameof(DataAnnotationValue)}' in type '{GetType().Name}'");

        public abstract TElement ValueA { get; }
        public abstract TElement ValueB { get; }
        public abstract TElement ValueC { get; }

        public virtual string ExplicitParentName => nameof(ExplicitParentName);
        //throw new NotImplementedException(
        //    $"implement '{nameof(ExplicitParentName)}' in type '{GetType().Name}'");

        public virtual string ConventionalParentName =>
            throw new NotImplementedException(
                $"implement '{nameof(ConventionalParentName)}' in type '{GetType().Name}'");

        public virtual string DataAnnotationParentName =>
            throw new NotImplementedException(
                $"implement '{nameof(DataAnnotationParentName)}' in type '{GetType().Name}'");

        public virtual void DefineByConvention(SchemaBuilder sb) =>
            throw new NotImplementedException($"implement '{nameof(DefineByConvention)}' in type '{GetType().Name}'");

        //public abstract void ConfigureParentExplicitlyByName(SchemaBuilder sb, string name);
        public abstract void ConfigureParentExplicitly(SchemaBuilder sb, out string parentName);
        //throw new NotImplementedException(
        //    $"implement '{nameof(DefineParentExplicitly)}' in type '{GetType().Name}'");

        public virtual void DefineByDataAnnotation(SchemaBuilder sb) =>
            throw new NotImplementedException(
                $"implement '{nameof(DefineByDataAnnotation)}' in type '{GetType().Name}'");

        public abstract void ConfigureExplicitly(SchemaBuilder sb, string parentName, TElement value);
        // => throw new NotImplementedException($"implement '{nameof(ConfigureExplicitly)}' in type '{GetType().Name}'");

        public abstract ConfigurationSource GetElementConfigurationSource(TMutableMarker definition);

        public abstract TParentMemberDefinition GetParentDefinitionByName(SchemaDefinition schemaDefinition,
            string parentName);

        public abstract TParentMember GetParentByName(Schema schema, string parentName);

        public abstract bool TryGetValue(TMarker parent, out TElement value);



        public virtual void optional_not_defined_by_convention_when_parent_configured_explicitly()
        {
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                ConfigureParentExplicitly(sb, out parentName);
                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                TryGetValue(parentDef, out _).Should().BeFalse();
            });
            var parent = GetParentByName(schema, parentName);
            TryGetValue(parent, out _).Should().BeFalse();
        }

        public virtual void optional_not_defined_by_convention_then_configured_explicitly()
        {
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                ConfigureParentExplicitly(sb, out parentName);
                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                TryGetValue(parentDef, out _).Should().BeFalse();
                ConfigureExplicitly(sb, parentName, ValueA);
                TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                configuredA.Should().Be(ValueA);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
            });
            var parent = GetParentByName(schema, parentName);
            TryGetValue(parent, out var finalVal).Should().BeTrue();
            finalVal.Should().Be(ValueA);
        }

        public virtual void configured_explicitly_reconfigured_explicitly()
        {
            ValueA.Should().NotBe(ValueB);
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                ConfigureParentExplicitly(sb, out parentName);
                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                TryGetValue(parentDef, out _).Should().BeFalse();
                ConfigureExplicitly(sb, parentName, ValueA);
                TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                configuredA.Should().Be(ValueA);
                ConfigureExplicitly(sb, parentName, ValueB);
                TryGetValue(parentDef, out var configuredB).Should().BeTrue();
                configuredB.Should().Be(ValueB);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
            });
            var parent = GetParentByName(schema, parentName);
            TryGetValue(parent, out var finalVal).Should().BeTrue();
            finalVal.Should().Be(ValueB);
        }

        public virtual void configured_by_data_annotation()
        {

        }

        public virtual void configured_by_data_annotation_then_reconfigured_explicitly()
        {

        }

        public virtual void optional_configured_by_data_annotation_then_removed_explicitly()
        {

        }

        public virtual void configure_by_convention()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByConvention(sb);
                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), ConventionalParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                TryGetValue(parentDef, out var defVal).Should().BeTrue();
                defVal.Should().Be(ConventionalValue);
            });
            var parent = GetParentByName(schema, ConventionalParentName);
            TryGetValue(parent, out var val).Should().BeTrue();
            val.Should().Be(ConventionalValue);
        }

        public virtual void define_by_data_annotation()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByDataAnnotation(sb);
                var definition = sb.GetDefinition();
                var parentDef = GetParentDefinitionByName(definition, DataAnnotationParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                TryGetValue(parentDef, out var defVal).Should().BeTrue();
                defVal.Should().Be(DataAnnotationValue);
            });
            var parent = GetParentByName(schema, DataAnnotationParentName);
            TryGetValue(parent, out var val).Should().BeTrue();
            val.Should().Be(DataAnnotationValue);
        }

        public virtual void define_by_data_annotation_overridden_by_explicit_configuration()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByDataAnnotation(sb);
                var definition = sb.GetDefinition();
                var parentDef = GetParentDefinitionByName(definition, DataAnnotationParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                TryGetValue(parentDef, out var defVal).Should().BeTrue();
                defVal.Should().Be(DataAnnotationValue);
                // ConfigureExplicitly(sb, DataAnnotationParentName);
                throw new NotImplementedException();
                //GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                //TryGetValue(parentDef, out var newVal).Should().BeTrue();
                // newVal.Should().Be(ValueA);
            });
            var parent = GetParentByName(schema, DataAnnotationParentName);
            TryGetValue(parent, out var val).Should().BeTrue();
            val.Should().Be(ValueA);
        }
    }
}