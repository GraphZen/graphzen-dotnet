// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class Collection<TMarker, TMutableMarker> : Collection where TMutableMarker : TMarker
    {
        public Collection([NotNull] string name, [CanBeNull] Vector collectionItem) : base(name, collectionItem, typeof(TMarker), typeof(TMutableMarker))
        {
        }
    }
    public abstract class Collection : Element
    {
        public Collection([NotNull] string name, [CanBeNull] Vector collectionItem,
         Type markerInterfaceType, Type mutableMarkerInterfaceType
        ) : base(name, markerInterfaceType, mutableMarkerInterfaceType)
        {
            CollectionItem = collectionItem;
        }

        public Vector CollectionItem { get; }
    }
}