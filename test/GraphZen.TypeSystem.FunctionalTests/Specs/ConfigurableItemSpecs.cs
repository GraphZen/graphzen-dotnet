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
        public class Updateable
        {
            public const string it_can_be_updated = null;
        }

        public class Optional
        {
            public const string it_can_be_removed = null;
            public const string parent_can_be_created_without = null;
        }

        public class Required
        {
            public const string it_cannot_be_removed = null;
        }

        public class NamedCollection
        {
            public const string item_can_be_added = null;
            public const string item_can_be_renamed = null;
            public const string item_cannot_be_renamed_if_name_already_exists = null;
            public const string item_can_be_removed = null;
            public const string item_name_must_be_valid_name = null;
        }

        public class NamedTypeSet
        {
            public const string item_can_be_added = null;
            public const string item_can_be_removed = null;
            public const string item_must_be_valid_name = null;
        }
    }
}