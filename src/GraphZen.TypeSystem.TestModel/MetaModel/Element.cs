// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public abstract class Element : IEnumerable<Element>
    {
        public Element([NotNull] string name, string memberName)
        {
            Name = name;
            MemberName = memberName;
        }

        public string MemberName { get; }
        public string Name { get; }
        public bool Optional { get; set; }
        public abstract IEnumerator<Element> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}