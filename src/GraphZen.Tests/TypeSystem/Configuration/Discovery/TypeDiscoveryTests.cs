// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using Xunit;

// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem
{
    [NoReorder]
    public class TypeDiscoveryTests
    {
        public class type_never_included
        {
        }

        public class object_included_via_property_type_on_class
        {
        }

        public class object_included_via_method_return_type_on_class
        {
        }

        [GraphQLIgnore]
        public class object_ignored_by_data_annotation
        {
        }


        public interface IFooInterface
        {
            [GraphQLIgnore]
            type_never_included property_from_interface_ignored_by_data_annotation { get; }
        }

        public class input_object_included_via_field_argument
        {
        }


        [GraphQLIgnore]
        public class input_object_ignored_by_data_annotation
        {
        }


        [GraphQLIgnore]
        public class object_included_by_explicit_configuration : IFooInterface
        {
            public object_ignored_by_data_annotation PropertyFieldWithTypeIgnoredByDataAnnotation { get; set; }
            public object_included_via_property_type_on_class PropertyField { get; set; }

            [GraphQLIgnore]
            public type_never_included PropertyIgnoredByDataAnnotation { get; set; }

            [GraphQLIgnore]
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public type_never_included property_from_interface_ignored_by_data_annotation { get; }

            public object_included_via_method_return_type_on_class MethodField(
                input_object_ignored_by_data_annotation arg1,
                input_object_included_via_field_argument arg2,
                [GraphQLIgnore] type_never_included arg3) => default;

            [GraphQLIgnore]
            public type_never_included MethodIgnoredByDataAnnotation() => default;
        }


        [Theory]
        [InlineData(typeof(object_ignored_by_data_annotation), TypeKind.Object, null,
            ConfigurationSource.DataAnnotation)]
        [InlineData(typeof(input_object_included_via_field_argument), TypeKind.InputObject,
            ConfigurationSource.Convention, null)]
        [InlineData(typeof(object_included_by_explicit_configuration), TypeKind.Object, ConfigurationSource.Explicit,
            ConfigurationSource.DataAnnotation)]
        [InlineData(typeof(object_included_via_method_return_type_on_class), TypeKind.Object,
            ConfigurationSource.Convention, null)]
        [InlineData(typeof(object_included_via_property_type_on_class), TypeKind.Object, ConfigurationSource.Convention,
            null)]
        [InlineData(typeof(input_object_ignored_by_data_annotation), TypeKind.InputObject, null,
            ConfigurationSource.DataAnnotation)]
        [InlineData(typeof(type_never_included), null, null, null)]
        public void included_by_convention(Type clrType, TypeKind? kind, ConfigurationSource? configurationSource,
            ConfigurationSource? ignoredConfigurationSource)
        {
            var ignored = !(configurationSource.HasValue && configurationSource.Overrides(ignoredConfigurationSource));
            var schema = Schema.Create(_ =>
            {
                _.Object<object_included_by_explicit_configuration>();
                var def = kind.HasValue
                    ? _.GetDefinition().FindType(clrType, kind.Value)
                    : _.GetDefinition().Types.SingleOrDefault(t => t.ClrType == clrType);
                if (ignored)
                {
                    def.Should().BeNull($"because it is ignored by {ignoredConfigurationSource} configuration.");
                    if (ignoredConfigurationSource.HasValue)
                    {
                        _.GetDefinition().FindIgnoredTypeConfigurationSource(clrType.GetGraphQLName()).Should()
                            .Be(ignoredConfigurationSource, $"because {clrType.Name} should be ignored.");
                    }
                }
                else
                {
                    def.Should()
                        .NotBeNull(
                            $"because {clrType.Name} is included as a {kind} by {configurationSource} configuration.");
                    // ReSharper disable once PossibleNullReferenceException
                    def.GetConfigurationSource().Should().Be(configurationSource);
                }
            });

            var type = schema.GetTypes().Where(t => t.Kind == kind).SingleOrDefault(_ => _.ClrType == clrType);
            if (ignored)
            {
                type.Should().BeNull($"because it should be included by {configurationSource} configuration.");
            }
            else
            {
                type.Should()
                    .NotBeNull($"it should be should be ignored by {ignoredConfigurationSource} configuration.");
            }
        }
    }
}