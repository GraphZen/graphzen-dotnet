// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.MetaModel
{
    public class LeafElement<TMarker, TMutableMarker, TElement> : LeafElement where TMutableMarker : TMarker
        where TMarker : IConfigurationElement<TElement>
    {
        public LeafElement([NotNull] string name) : base(name, typeof(TMarker), typeof(TMutableMarker),
            typeof(TElement))
        {
        }
    }

    public abstract class LeafElement : Element
    {
        public LeafElement([NotNull] string name, Type markerInterfaceType, Type mutableMarkerInterfaceType,
            Type elementType) : base(name)
        {
            MarkerInterfaceType = markerInterfaceType;
            MutableMarkerInterfaceType = mutableMarkerInterfaceType;
            ElementType = elementType;
        }

        public Type MarkerInterfaceType { get; }
        public Type MutableMarkerInterfaceType { get; }
        public Type ElementType { get; }
        public bool Optional { get; set; }
        public bool ConfiguredByConvention { get; set; } = true;
        public bool ConfiguredByDataAnnotation { get; set; } = true;
    }
}