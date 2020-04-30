﻿// Copyright (c) GraphZen LLC. All rights reserved.
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
            public const string parent_cannot_be_created_without_required_item = null;
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

        public class NamedTypeSetSpecs
        {
            public const string set_item_can_be_added = null;
            public const string set_item_can_be_removed = null;
            public const string set_item_must_be_valid_name = null;
        }

        public class DirectivesSpecs
        {
        }
    }
}