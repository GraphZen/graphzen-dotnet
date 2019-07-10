// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public abstract class Element : IEnumerable<Element>
    {
        public Element([NotNull] string name)
        {
            Name = name;
        }

        public string Name { get; }
        public bool Optional { get; set; }
        public abstract IEnumerator<Element> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}