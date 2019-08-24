// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen
{
    public class CollectionConventionContext
    {
        public string ParentName { get; set; }
        public string ItemIgnoredByDataAnnotation { get; set; }
        public string ItemIgnoredByConvention { get; set; }
        public string ItemNamedByConvention { get; set; }
        public string ItemNamedByDataAnnotation { get; set; }

        public ConfigurationSource? DefaultItemConfigurationSource { get; set; }
    }
}