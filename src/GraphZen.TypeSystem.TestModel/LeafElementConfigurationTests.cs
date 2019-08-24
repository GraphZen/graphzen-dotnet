#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    public abstract class LeafElementConfigurationTests<TMarker, TMutableMarker, TParentMemberDefinition, TParentMember,
        TElement> :
        ElementConfigurationTests<TMarker, TMutableMarker, TParentMemberDefinition, TParentMember>
        where TMutableMarker : TMarker
        where TParentMemberDefinition : MemberDefinition, TMutableMarker
        where TParentMember : Member, TMarker
        where TMarker : IConfigurationElement<TElement>
    {
        public virtual TElement ConventionalValue =>
            throw new NotImplementedException($"implement '{nameof(ConventionalValue)}' in type '{GetType().Name}'");

        public virtual TElement DataAnnotationValue =>
            throw new NotImplementedException(NotImplementedMessage(nameof(DataAnnotationValue), false));

        public virtual bool DefinedByConvention =>
            throw new NotImplementedException("implemented automatically by generated code");

        public virtual bool DefinedByDataAnnotation =>
            throw new NotImplementedException("implemented automatically by generated code");


        public abstract TElement ValueA { get; }
        public abstract TElement ValueB { get; }
        public abstract TElement ValueC { get; }

        public virtual string GrandparentName { get; } = "Grandparent";
        public virtual string GreatGrandparentName { get; } = "GreatGrandparent";


        public void DefineParents(SchemaBuilder sb, out List<string> parentNames)
        {
            parentNames = new List<string>();
            DefineParentExplicitly(sb, out var explicitParentName);
            parentNames.Add(explicitParentName);

            if (DefinedByConvention)
            {
                DefineParentConventionally(sb, out var cp);
                parentNames.Add(cp);
            }

            if (DefinedByDataAnnotation)
            {
                DefineParentConventionallyWithDataAnnotation(sb, out var cpa);
                parentNames.Add(cpa);
            }
        }

        public abstract void ConfigureExplicitly(SchemaBuilder sb, string parentName, TElement value);

        public virtual void RemoveExplicitly(SchemaBuilder sb, string parentName) =>
            throw new NotImplementedException(NotImplementedMessage(nameof(RemoveExplicitly)));


        public abstract ConfigurationSource GetElementConfigurationSource(TMutableMarker definition);


        public abstract bool TryGetValue(TMarker parent, out TElement value);

        public virtual void optional_not_defined_by_convention_when_parent_configured_explicitly()
        {
            List<string> parentNames = null;
            var schema = Schema.Create(sb =>
            {
                DefineParents(sb, out parentNames);
                foreach (var parentName in parentNames)
                {
                    var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                    TryGetValue(parentDef, out _).Should().BeFalse();
                }
            });
            foreach (var parentName in parentNames)
            {
                var parent = GetParentByName(schema, parentName);
                TryGetValue(parent, out _).Should().BeFalse();
            }
        }

        public virtual void optional_not_defined_by_convention_then_configured_explicitly()
        {
            List<string> parentNames = null;
            var schema = Schema.Create(sb =>
            {
                DefineParents(sb, out parentNames);
                foreach (var parentName in parentNames)
                {
                    var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                    TryGetValue(parentDef, out _).Should().BeFalse();
                    ConfigureExplicitly(sb, parentName, ValueA);
                    TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                    configuredA.Should().Be(ValueA);
                    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                }
            });
            foreach (var parentName in parentNames)
            {
                var parent = GetParentByName(schema, parentName);
                TryGetValue(parent, out var finalVal).Should().BeTrue();
                finalVal.Should().Be(ValueA);
            }
        }

        public virtual void optional_not_defined_by_convention_then_configured_explicitly_then_removed()
        {
            List<string> parentNames = null;
            var schema = Schema.Create(sb =>
            {
                DefineParents(sb, out parentNames);
                foreach (var parentName in parentNames)
                {
                    var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                    TryGetValue(parentDef, out _).Should().BeFalse();
                    ConfigureExplicitly(sb, parentName, ValueA);
                    TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                    configuredA.Should().Be(ValueA);
                    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                    RemoveExplicitly(sb, parentName);
                    TryGetValue(parentDef, out var configuredC).Should().BeFalse();
                    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                    configuredC.Should().BeNull();
                }
            });
            foreach (var parentName in parentNames)
            {
                var parent = GetParentByName(schema, parentName);
                TryGetValue(parent, out var finalVal).Should().BeFalse();
                finalVal.Should().BeNull();
            }
        }

        public virtual void configured_explicitly_reconfigured_explicitly()
        {
            ValueA.Should().NotBe(ValueB);
            List<string> parentNames = null;
            var schema = Schema.Create(sb =>
            {
                DefineParents(sb, out parentNames);
                foreach (var parentName in parentNames)
                {
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
                }
            });
            foreach (var parentName in parentNames)
            {
                var parent = GetParentByName(schema, parentName);
                TryGetValue(parent, out var finalVal).Should().BeTrue();
                finalVal.Should().Be(ValueB);
            }
        }

        public virtual void configured_by_data_annotation()
        {
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                DefineParentConventionallyWithDataAnnotation(sb, out parentName);
                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                TryGetValue(parentDef, out var dataAnnotationValue).Should().BeTrue();
                dataAnnotationValue.Should().Be(DataAnnotationValue);
            });
            var parent = GetParentByName(schema, parentName);
            TryGetValue(parent, out var finalVal).Should().BeTrue();
            finalVal.Should().Be(DataAnnotationValue);
        }

        public virtual void configured_by_data_annotation_then_reconfigured_explicitly()
        {
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                DefineParentConventionallyWithDataAnnotation(sb, out parentName);
                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                TryGetValue(parentDef, out _).Should().BeTrue();
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

        public virtual void optional_configured_by_data_annotation_then_removed_explicitly()
        {
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                DefineParentConventionallyWithDataAnnotation(sb, out parentName);
                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                TryGetValue(parentDef, out _).Should().BeTrue();
                RemoveExplicitly(sb, parentName);
                TryGetValue(parentDef, out _).Should().BeFalse();
                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
            });
            var parent = GetParentByName(schema, parentName);
            TryGetValue(parent, out _).Should().BeFalse();
        }

        public virtual void configure_by_convention()
        {
            //var schema = Schema.Create(sb =>
            //{
            //    DefineByConvention(sb);
            //    var parentDef = GetParentDefinitionByName(sb.GetDefinition(), ConventionalParentName);
            //    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
            //    TryGetValue(parentDef, out var defVal).Should().BeTrue();
            //    defVal.Should().Be(ConventionalValue);
            //});
            //var parent = GetParentByName(schema, ConventionalParentName);
            //TryGetValue(parent, out var val).Should().BeTrue();
            //val.Should().Be(ConventionalValue);
        }

        public virtual void define_by_data_annotation()
        {
            //var schema = Schema.Create(sb =>
            //{
            //    DefineByDataAnnotation(sb);
            //    var definition = sb.GetDefinition();
            //    var parentDef = GetParentDefinitionByName(definition, DataAnnotationParentName);
            //    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
            //    TryGetValue(parentDef, out var defVal).Should().BeTrue();
            //    defVal.Should().Be(DataAnnotationValue);
            //});
            //var parent = GetParentByName(schema, DataAnnotationParentName);
            //TryGetValue(parent, out var val).Should().BeTrue();
            //val.Should().Be(DataAnnotationValue);
        }

        public virtual void define_by_data_annotation_overridden_by_explicit_configuration()
        {
            //var schema = Schema.Create(sb =>
            //{
            //    DefineByDataAnnotation(sb);
            //    var definition = sb.GetDefinition();
            //    var parentDef = GetParentDefinitionByName(definition, DataAnnotationParentName);
            //    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
            //    TryGetValue(parentDef, out var defVal).Should().BeTrue();
            //    defVal.Should().Be(DataAnnotationValue);
            //    // ConfigureExplicitly(sb, DataAnnotationParentName);
            //    throw new NotImplementedException();
            //    //GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
            //    //TryGetValue(parentDef, out var newVal).Should().BeTrue();
            //    // newVal.Should().Be(ValueA);
            //});
            //var parent = GetParentByName(schema, DataAnnotationParentName);
            //TryGetValue(parent, out var val).Should().BeTrue();
            //val.Should().Be(ValueA);
        }
    }
}