// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class Collection<TMarker, TMutableMarker> : Collection where TMutableMarker : TMarker
    {
        public Collection([NotNull] string name, [NotNull] Vector collectionItem, bool inspectCollectionItem = true) : base(name, Check.NotNull(collectionItem, nameof(collectionItem)), typeof(TMarker), typeof(TMutableMarker), inspectCollectionItem)
        {
        }
    }
    public abstract class Collection : Element
    {

        public bool InspectCollectionItem { get; } 
        public Collection([NotNull] string name,[NotNull] Vector collectionItem,
         Type markerInterfaceType, Type mutableMarkerInterfaceType, bool inspectCollectionItem) : base(name, markerInterfaceType, mutableMarkerInterfaceType)
        {
            CollectionItem = collectionItem;
            InspectCollectionItem = inspectCollectionItem;
        }

        [NotNull]
        public Vector CollectionItem { get; }
    }
}