using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GraphZen.MetaModel
{
    public abstract class Element { }

    public class Item : Element, IEnumerable<Element>
    {
        [NotNull]
        private readonly List<Element> _Elements = new List<Element>();
        public IEnumerator<Element> GetEnumerator() => _Elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public class LeafItem : Element { }
    public class ItemContainer : Element { }

    public class Schema : Item
    {
    }

    public class Name :LeafItem { }

    public class Program
    {
        void Main()
        {
        }
    }
}
