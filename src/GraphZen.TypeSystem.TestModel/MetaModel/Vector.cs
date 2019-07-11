// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class Vector : Element
    {
        [NotNull] private readonly List<Element> _elements = new List<Element>();

        public Vector([NotNull] string name, string memberName) : base(name, memberName)
        {
        }


        public Vector Add(Element leafElement)
        {
            _elements.Add(leafElement);
            return this;
        }

        public override IEnumerator<Element> GetEnumerator() => _elements.GetEnumerator();
    }
}