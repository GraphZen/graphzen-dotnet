// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class Collection<TMarker, TMutableMarker> : Collection where TMutableMarker : TMarker
    {
        public Collection([NotNull] string name, Vector collectionItem) : base(name, Check.NotNull(collectionItem, nameof(collectionItem)), typeof(TMarker), typeof(TMutableMarker))
        {
        }
    }
    public abstract class Collection : Element
    {
        public Collection([NotNull] string name,[NotNull] Vector collectionItem,
         Type markerInterfaceType, Type mutableMarkerInterfaceType
        ) : base(name, markerInterfaceType, mutableMarkerInterfaceType)
        {
            CollectionItem = collectionItem;
        }

        [NotNull]
        public Vector CollectionItem { get; }
    }
}