// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem.FunctionalTests.Specs
{
    public class TypeSystemSpecs
    {
        public class DirectiveAnnotationSpecs
        {
            public const string directive_annotation_can_be_added = null;
            public const string directive_annotation_cannot_be_added_unless_directive_is_defined = null;
            public const string directive_annotation_cannot_be_upserted_unless_directive_is_defined = null;
            public const string directive_annotation_cannot_be_added_unless_location_is_valid = null;
            public const string directive_annotation_cannot_be_upserted_unless_location_is_valid = null;
            public const string directive_annotation_cannot_be_added_with_null_name = null;
            public const string directive_annotation_cannot_be_upserted_with_null_name = null;
            public const string directive_annotation_cannot_be_added_with_invalid_name = null;
            public const string directive_annotation_cannot_be_upserted_with_invalid_name = null;
            public const string directive_annotations_can_be_removed = null;
            public const string directive_annotations_can_be_removed_by_name = null;
            public const string directive_annotations_cannot_be_removed_by_name_with_null_name = null;
            public const string directive_annotations_are_removed_when_directive_is_removed = null;
            public const string directive_annotations_are_removed_when_directive_is_ignored = null;
        }

        public class UpdateableSpecs
        {
            public const string updateable_item_can_be_updated = null;
        }

        public class OptionalSpecs
        {
            public const string optional_item_can_be_removed = null;
            public const string parent_can_be_created_without_optional_item = null;
        }

        public class RequiredSpecs
        {
            public const string required_item_cannot_be_removed = null;
        }

        public class NamedCollectionSpecs
        {
            public const string named_item_can_be_added = null;
            public const string named_item_cannot_be_added_with_null_value = null;
            public const string named_item_cannot_be_added_with_invalid_name = null;
            public const string named_item_can_be_renamed = null;
            public const string named_item_cannot_be_renamed_with_null_value = null;
            public const string named_item_cannot_be_renamed_with_an_invalid_name = null;
            public const string named_item_cannot_be_renamed_if_name_already_exists = null;
            public const string named_item_can_be_removed = null;
            public const string named_item_cannot_be_removed_with_null_value = null;
            public const string named_item_cannot_be_removed_with_invalid_name = null;
        }

        public class ClrTypeSpecs
        {
        }

        public class TypedCollectionSpecs
        {
            public const string typed_item_can_be_added = null;
            public const string typed_item_can_be_added_via_type_param = null;
            public const string typed_item_cannot_be_added_with_null_value = null;
            public const string typed_item_cannot_be_added_with_invalid_name_attribute = null;
            public const string typed_item_can_be_renamed = null;
            public const string typed_item_with_name_attribute_can_be_renamed = null;
            public const string typed_item_cannot_be_renamed_with_null_value = null;
            public const string typed_item_cannot_be_renamed_with_an_invalid_name = null;
            public const string typed_item_cannot_be_renamed_if_name_already_exists = null;
            public const string typed_item_can_be_removed = null;
            public const string typed_item_can_be_removed_via_type_param = null;
            public const string typed_item_cannot_be_removed_with_null_value = null;
        }

        public class NamedTypeSetSpecs
        {
            public const string set_item_can_be_added = null;
            public const string set_item_can_be_removed = null;
            public const string set_item_must_be_valid_name = null;
        }
    }
}