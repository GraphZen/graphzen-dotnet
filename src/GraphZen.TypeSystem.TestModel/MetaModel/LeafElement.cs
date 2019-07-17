// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class LeafElement<TMarker, TMutableMarker> : LeafElement where TMutableMarker : TMarker
    {
        public LeafElement([NotNull] string name) : base(name, typeof(TMutableMarker), typeof(TMutableMarker))
        {
        }
    }

    public abstract class LeafElement : Element
    {
        public LeafElement([NotNull] string name, Type markerInterface, Type mutableMarkerInterface) : base(name)
        {
            MarkerInterface = markerInterface;
            MutableMarkerInterface = mutableMarkerInterface;
        }

        public Type MarkerInterface { get; }
        public Type MutableMarkerInterface { get; }
    }
}