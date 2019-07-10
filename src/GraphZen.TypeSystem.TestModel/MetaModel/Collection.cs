// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class Collection : Element
    {
        public Collection([NotNull] string name, [NotNull] Vector collectionItem) : base(name)
        {
            CollectionItem = collectionItem;
        }

        public Vector CollectionItem { get; }
        public override IEnumerator<Element> GetEnumerator() => Enumerable.Empty<Element>().GetEnumerator();
    }
}