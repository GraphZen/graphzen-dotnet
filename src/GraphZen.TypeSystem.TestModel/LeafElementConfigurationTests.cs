// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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
        public virtual object ConventionalValue =>
            throw new NotImplementedException($"implement '{nameof(ConventionalValue)}' in type '{GetType().Name}'");

        public virtual object DataAnnotationValue =>
            throw new NotImplementedException($"implement '{nameof(DataAnnotationValue)}' in type '{GetType().Name}'");

        public virtual object ExplicitValue =>
            throw new NotImplementedException($"implement '{nameof(ExplicitValue)}' in type '{GetType().Name}'");

        public virtual string NotDefinedByConventionParentName =>
            throw new NotImplementedException(
                $"implement '{nameof(NotDefinedByConventionParentName)}' in type '{GetType().Name}'");

        public virtual string ConventionalParentName =>
            throw new NotImplementedException(
                $"implement '{nameof(ConventionalParentName)}' in type '{GetType().Name}'");

        public virtual string DataAnnotationParentName =>
            throw new NotImplementedException(
                $"implement '{nameof(DataAnnotationParentName)}' in type '{GetType().Name}'");

        public virtual void DefineByConvention(SchemaBuilder sb) =>
            throw new NotImplementedException($"implement '{nameof(DefineByConvention)}' in type '{GetType().Name}'");

        public virtual void DefineEmptyByConvention(SchemaBuilder sb) =>
            throw new NotImplementedException(
                $"implement '{nameof(DefineEmptyByConvention)}' in type '{GetType().Name}'");

        public virtual void DefineByDataAnnotation(SchemaBuilder sb) =>
            throw new NotImplementedException(
                $"implement '{nameof(DefineByDataAnnotation)}' in type '{GetType().Name}'");

        public virtual void ConfigureExplicitly(SchemaBuilder sb, string parentName) =>
            throw new NotImplementedException($"implement '{nameof(ConfigureExplicitly)}' in type '{GetType().Name}'");

        public abstract ConfigurationSource GetElementConfigurationSource(TMutableMarker definition);

        public abstract TParentMemberDefinition GetParentDefinition(SchemaDefinition schemaDefinition, string parentName);

        public abstract TParentMember GetParent(Schema schema, string parentName);

        public abstract bool TryGetValue(TMarker parent, out object value);



        public virtual void optional_not_defined_by_default()
        {
            var schema = Schema.Create(sb =>
                        {
                            DefineEmptyByConvention(sb);
                            var parentDef = GetParentDefinition(sb.GetDefinition(), NotDefinedByConventionParentName);
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                            TryGetValue(parentDef, out _).Should().BeFalse();
                        });
            var parent = GetParent(schema, NotDefinedByConventionParentName);
            TryGetValue(parent, out _).Should().BeFalse();

        }


        public virtual void optional_not_defined_by_convention()
        {
            var schema = Schema.Create(sb =>
            {
                DefineEmptyByConvention(sb);
                var parentDef = GetParentDefinition(sb.GetDefinition(), NotDefinedByConventionParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                TryGetValue(parentDef, out _).Should().BeFalse();
            });
            var parent = GetParent(schema, NotDefinedByConventionParentName);
            TryGetValue(parent, out _).Should().BeFalse();
        }

        public virtual void configure_by_convention()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByConvention(sb);
                var parentDef = GetParentDefinition(sb.GetDefinition(), ConventionalParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                TryGetValue(parentDef, out var defVal).Should().BeTrue();
                defVal.Should().Be(ConventionalValue);
            });
            var parent = GetParent(schema, ConventionalParentName);
            TryGetValue(parent, out var val).Should().BeTrue();
            val.Should().Be(ConventionalValue);
        }

        public virtual void define_by_data_annotation()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByDataAnnotation(sb);
                var definition = sb.GetDefinition();
                var parentDef = GetParentDefinition(definition, DataAnnotationParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                TryGetValue(parentDef, out var defVal).Should().BeTrue();
                defVal.Should().Be(DataAnnotationValue);
            });
            var parent = GetParent(schema, DataAnnotationParentName);
            TryGetValue(parent, out var val).Should().BeTrue();
            val.Should().Be(DataAnnotationValue);
        }

        public virtual void define_by_data_annotation_overridden_by_explicit_configuration()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByDataAnnotation(sb);
                var definition = sb.GetDefinition();
                var parentDef = GetParentDefinition(definition, DataAnnotationParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                TryGetValue(parentDef, out var defVal).Should().BeTrue();
                defVal.Should().Be(DataAnnotationValue);
                ConfigureExplicitly(sb, DataAnnotationParentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                TryGetValue(parentDef, out var newVal).Should().BeTrue();
                newVal.Should().Be(ExplicitValue);
            });
            var parent = GetParent(schema, DataAnnotationParentName);
            TryGetValue(parent, out var val).Should().BeTrue();
            val.Should().Be(ExplicitValue);
        }
    }
}