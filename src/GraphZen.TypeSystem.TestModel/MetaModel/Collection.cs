// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class Collection : Element
    {
        public Collection([NotNull] string name, [CanBeNull] Vector collectionItem) : base(name)
        {
            CollectionItem = collectionItem;
        }

        public Vector CollectionItem { get; }
    }
}