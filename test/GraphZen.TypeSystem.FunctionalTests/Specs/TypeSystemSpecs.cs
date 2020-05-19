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
        public class SdlSpec
        {
            public const string item_can_be_defined_by_sdl = null;
        }

        public class SdlExtensionSpec
        {
            public const string item_can_be_defined_by_sdl_extension = null;
        }


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


        public class DescriptionSpecs
        {
            public const string description_can_be_updated = null;
            public const string description_cannot_be_null = null;
            public const string description_can_be_removed = null;
        }


        public class InputXorOutputTypeSpecs
        {
            public const string named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io = null;
            public const string named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io = null;

            public const string
                clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io = null;

            public const string
                clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
                    = null;

            public const string clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io =
                null;

            public const string
                clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
                    = null;

            public const string
                clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io =
                    null;

            public const string
                clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io = null;
        }

        public class InputAndOutputTypeSpecs
        {
            public const string named_item_can_be_added_if_name_matches_input_type_identity = null;
            public const string named_item_can_be_added_if_name_matches_output_type_identity = null;
            public const string named_item_can_be_renamed_to_name_with_input_type_identity = null;
            public const string named_item_can_be_renamed_to_name_with_output_type_identity = null;
            public const string clr_typed_item_can_be_renamed_if_name_matches_input_type_identity = null;
            public const string clr_typed_item_can_be_renamed_if_name_matches_output_type_identity = null;

            public const string
                clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity =
                    null;

            public const string
                clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity =
                    null;
        }

        public class NamedCollectionSpecs
        {
            public const string named_item_can_be_added = null;
            public const string named_item_cannot_be_added_with_null_value = null;
            public const string named_item_cannot_be_added_with_invalid_name = null;
            public const string named_item_can_be_renamed = null;
            public const string named_item_can_be_removed = null;

            public const string named_item_cannot_be_removed_with_null_value = null;
            // public const string named_item_cannot_be_removed_with_invalid_name = null; TODO: can or cannot?
        }

        public class NameSpecs
        {
            public const string can_be_renamed = null;
            public const string name_must_be_valid_name = null;
            public const string name_cannot_be_null = null;
            public const string name_cannot_be_duplicate = null;
        }

        public class OutputFieldsDefinitionSpecs
        {
            // TODO put specs for IFieldsDefinitionBuilder not covered by NamedCollectionSpecs
        }


        public class ClrTypedCollectionSpecs
        {
            // Adding to collection with CLR Type
            public const string clr_typed_item_can_be_added = null;
            public const string adding_clr_typed_item_updates_matching_named_items_clr_type = null;
            public const string adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type = null;
            public const string clr_typed_item_can_be_added_via_type_param = null;
            public const string clr_typed_item_uses_clr_type_name = null;
            public const string clr_typed_item_with_name_annotation_uses_clr_type_name_annotation = null;
            public const string clr_typed_item_cannot_be_added_with_null_value = null;
            public const string clr_typed_item_cannot_be_added_with_invalid_name_attribute = null;
            public const string clr_typed_item_with_conflicting_name_can_be_added_with_custom_name = null;

            public const string clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name =
                null;

            public const string clr_typed_item_with_conflicting_name_annotation_can_be_added_with_custom_name = null;

            public const string
                clr_typed_item_with_conflicting_name_annotation_can_be_added_via_type_param_with_custom_name = null;

            public const string clr_typed_item_with_invalid_name_annotation_can_be_added_with_custom_name = null;

            public const string clr_typed_item_with_invalid_name_annotation_can_be_added_via_type_param_with_custom_name
                = null;

            public const string clr_typed_item_cannot_be_added_with_null_custom_name = null;
            public const string clr_typed_item_cannot_be_added_with_invalid_custom_name = null;
            public const string clr_typed_item_subsequently_added_with_custom_name_sets_name = null;
            public const string named_item_subsequently_added_with_type_and_custom_name_sets_clr_type = null;

            public const string clr_typed_item_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist =
                null;

            public const string
                clr_typed_item_with_name_annotation_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist
                    = null;


            // Removing from collection with CLR Type
            public const string clr_typed_item_can_be_removed = null;
            public const string clr_typed_item_can_be_removed_via_type_param = null;
            public const string clr_typed_item_cannot_be_removed_with_null_value = null;

            // Renaming CLR Typed item
            public const string clr_typed_item_can_be_renamed = null;
            public const string clr_typed_item_with_name_attribute_can_be_renamed = null;
            public const string clr_typed_item_cannot_be_renamed_with_an_invalid_name = null;
            public const string clr_typed_item_cannot_be_renamed_if_name_already_exists = null;
        }

        public class FrozenSpecs
        {
        }

        public class ClrTypeSpecs
        {
            public const string clr_type_can_be_added = null;
            public const string clr_type_can_be_added_via_type_param = null;


            public const string clr_type_can_be_changed = null;
            public const string clr_type_can_be_changed_via_type_param = null;


            public const string clr_type_can_be_added_with_custom_name = null;
            public const string clr_type_can_be_added_via_type_param_with_custom_name = null;

            public const string clr_type_can_be_changed_with_custom_name = null;
            public const string clr_type_can_be_changed_via_type_param_with_custom_name = null;

            public const string clr_type_cannot_be_null = null;
            public const string clr_type_should_be_unique = null;
            public const string clr_type_name_should_be_unique = null;
            public const string clr_type_name_annotation_should_be_unique = null;
            public const string clr_type_name_annotation_should_be_valid = null;
            public const string custom_name_should_be_unique = null;
            public const string custom_name_should_be_valid = null;
            public const string custom_name_cannot_be_null = null;

            // Changing CLR type
            public const string changing_clr_type_changes_name = null;
            public const string changing_clr_type_with_name_annotation_changes_name = null;
            public const string clr_type_with_conflicting_name_can_be_added_using_custom_name = null;
            public const string clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name = null;

            // Removing CLR Type
            public const string clr_type_can_be_removed = null;
            public const string clr_typed_item_when_type_removed_should_retain_name = null;

            public const string clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name =
                null;

            public const string custom_named_clr_typed_item_when_type_removed_should_retain_custom_name = null;
        }

        public static class QueryTypeSpecs
        {
            public const string it_is_set_by_convention_when_object_named_query_added = null;
            public const string it_is_set_by_convention_when_object_renamed_to_query = null;
        }
    }
}