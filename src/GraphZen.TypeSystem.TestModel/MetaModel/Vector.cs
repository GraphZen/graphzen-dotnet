// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.MetaModel
{

    public class Vector<TMarker, TMutableMarker, TMember, TMemberDefinition> : Vector
        where TMutableMarker : TMarker
        where TMemberDefinition : MemberDefinition, TMutableMarker
        where TMember : Member, TMarker
    {
        public Vector([NotNull] string name) : base(name, typeof(TMarker), typeof(TMutableMarker), typeof(TMember), typeof(TMemberDefinition))
        {
        }

    }

    public abstract class Vector : Element, IEnumerable<Element>
    {
        [NotNull] private readonly List<Element> _elements = new List<Element>();

        public Vector([NotNull] string name,
        Type markerInterfaceType, Type mutableMarkerInterfaceType, Type memberType, Type memberDefinitionType) : base(name, markerInterfaceType, mutableMarkerInterfaceType)
        {
            MemberType = memberType;
            MemberDefinitionType = memberDefinitionType;
        }

        public Type MemberType { get; }
        public Type MemberDefinitionType { get; }

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