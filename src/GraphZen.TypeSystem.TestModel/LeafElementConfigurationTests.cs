// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    public abstract class ObjectType_Fields_CollectionConfigurationTests__Base : CollectionElementConfigurationTests<
          IFieldsContainerDefinition,
          IMutableFieldsContainerDefinition,
          ObjectTypeDefinition, ObjectType, FieldDefinition, Field
      >
    {
        public override void DefineParentExplicitly(SchemaBuilder sb, out string parentName)
        {
            parentName = "ExplicitObject";
            sb.Object(parentName);
        }

        public override ObjectTypeDefinition GetParentDefinitionByName([NotNull]SchemaDefinition schemaDefinition,
            [NotNull]string parentName) => schemaDefinition.GetObject(parentName);

        public override ObjectType GetParentByName([NotNull]Schema schema, [NotNull] string parentName) => schema.GetObject(parentName);

        public override IReadOnlyDictionary<string, FieldDefinition> GetDefinitionCollection(
            ObjectTypeDefinition parent) => parent.Fields;

        public override IReadOnlyDictionary<string, Field> GetCollection(ObjectType parent) => parent.Fields;

        public override ConfigurationSource? FindItemIgnoredConfigurationSource(ObjectTypeDefinition parent,
            [NotNull]string name) => parent.FindIgnoredFieldConfigurationSource(name);

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
            => sb.Object(parentName).Field(name);

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
            => sb.Object(parentName).IgnoreField(name);

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
            => sb.Object(parentName).UnignoreField(name);

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            throw new NotImplementedException();
        }
    }

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


    public abstract class CollectionElementConfigurationTests<
        TMarker,
        TMutableMarker,
        TParentMemberDefinition,
        TParentMember, 
        TCollectionItemDefinition, 
        TCollectionItem> : 
        ElementConfigurationTests<TMarker, TMutableMarker,
        TParentMemberDefinition, TParentMember>
        where TMutableMarker : TMarker
        where TParentMemberDefinition : MemberDefinition, TMutableMarker
        where TParentMember : Member, TMarker
        where TCollectionItemDefinition : MemberDefinition, IMutableNamed
        where TCollectionItem : Member, INamed
    {
        public abstract IReadOnlyDictionary<string, TCollectionItemDefinition> GetDefinitionCollection(
            TParentMemberDefinition parent);

        public IReadOnlyDictionary<string, TCollectionItemDefinition> GetDefinitionCollection(SchemaBuilder sb,
            string parentName)
        {
            var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
            return GetDefinitionCollection(parentDef);
        }

        public abstract IReadOnlyDictionary<string, TCollectionItem> GetCollection(TParentMember parent);
        public IReadOnlyDictionary<string, TCollectionItem> GetCollection(Schema schema, string parentName)
        {
            var parent = GetParentByName(schema, parentName);
            return GetCollection(parent);
        }

        public abstract ConfigurationSource? FindItemIgnoredConfigurationSource(TParentMemberDefinition parent, string name);

        public ConfigurationSource? FindItemIgnoredConfigurationSource(SchemaBuilder sb, string parentName, string name)
        {
            var parent = GetParentDefinitionByName(sb.GetDefinition(), parentName);
            return FindItemIgnoredConfigurationSource(parent, name);
        }


        public abstract void AddItem(SchemaBuilder sb, string parentName, string name);
        public abstract void IgnoreItem(SchemaBuilder sb, string parentName, string name);
        public abstract void UnignoreItem(SchemaBuilder sb, string parentName, string name);
        public abstract void RenameItem(SchemaBuilder sb, string parentName, string name, string newName);


        [Fact]
        public void when_parent_defined_explicitly_collection_is_empty()
        {
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection.Should().BeEmpty();
            });
            var collection = GetCollection(schema, parentName);
            collection.Should().BeEmpty();
        }


        [Fact]
        public void when_item_added_explicitly_item_configurationSource_should_be_explicit()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection[itemName].Should().NotBeNull();
                defCollection[itemName].Should().BeOfType<TCollectionItemDefinition>();
                defCollection[itemName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                defCollection.Count.Should().Be(1);
            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection.Count.Should().Be(1);
            collection[itemName].Should().NotBeNull();
            collection[itemName].Should().BeOfType<TCollectionItem>();
        }

        [Fact]
        public void when_item_added_explicitly_item_name_configurationSource_should_be_explicit()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection[itemName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                defCollection[itemName].Name.Should().Be(itemName);
            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection[itemName].Name.Should().Be(itemName);
        }

        [Fact]
        public void when_item_added_explicitly_then_ignored_explicitly_item_ignored_configuration_source_should_be_explicit()
        {
            var itemName = "addedExplicitly";
            Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out var parentName);
                AddItem(sb, parentName, itemName);
                IgnoreItem(sb, parentName, itemName);
                FindItemIgnoredConfigurationSource(sb, parentName, itemName).Should().Be(ConfigurationSource.Explicit);
            });
        }
        [Fact]
        public void when_item_added_explicitly_then_ignored_explicitly_item_should_be_removed()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection[itemName].Should().NotBeNull();
                defCollection[itemName].Should().BeOfType<TCollectionItemDefinition>();
                defCollection[itemName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                defCollection.Count.Should().Be(1);
                IgnoreItem(sb, parentName, itemName);
                defCollection.ContainsKey(itemName).Should().BeFalse();

            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection.Count.Should().Be(0);
        }
        [Fact]
        public void when_item_added_explicitly_then_ignored_then_unignored_explicitly_ignored_configuration_source_should_be_null()
        {
            var itemName = "addedExplicitly";
            Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out var parentName);
                AddItem(sb, parentName, itemName);
                IgnoreItem(sb, parentName, itemName);
                UnignoreItem(sb, parentName, itemName);
                FindItemIgnoredConfigurationSource(sb, parentName, itemName).Should().BeNull();
            });
        }

        [Fact]
        public void when_item_added_explicitly_then_ignored_then_unignored_and_re_added_should_exist()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                IgnoreItem(sb, parentName, itemName);
                UnignoreItem(sb, parentName, itemName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection.Count.Should().Be(1);
                defCollection[itemName].Should().NotBeNull();
                defCollection[itemName].Should().BeOfType<TCollectionItemDefinition>();
                defCollection[itemName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection[itemName].Name.Should().Be(itemName);
        }
    }

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