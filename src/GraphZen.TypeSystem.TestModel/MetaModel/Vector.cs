// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{

    public class Vector<TMarker, TMutableMarker> : Vector where TMutableMarker : TMarker
    {
        public Vector([NotNull] string name) : base(name, typeof(TMarker), typeof(TMutableMarker))
        {
        }
    }

    public abstract class Vector : Element, IEnumerable<Element>
    {
        [NotNull] private readonly List<Element> _elements = new List<Element>();

        public Vector([NotNull] string name,
        Type markerInterfaceType, Type mutableMarkerInterfaceType

        ) : base(name, markerInterfaceType, mutableMarkerInterfaceType)
        {
        }

        public IEnumerator<Element> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public Vector Add([NotNull] Element leafElement)
        {
            if (leafElement == null)
            {
                throw new Exception($"cannot add null element to vector '{Name}'");
            }

            _elements.Add(leafElement);
            return this;
        }
    }
}