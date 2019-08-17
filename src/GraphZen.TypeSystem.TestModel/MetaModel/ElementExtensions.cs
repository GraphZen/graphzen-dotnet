using System;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public static class ElementExtensions
    {
        [NotNull]
        public static T Set<T>([NotNull]this T element, [NotNull] Action<T> elementAction) where T : Element
        {
            elementAction(element);
            return element;
        }

        [NotNull]
        public static T SetConventions<T>([NotNull]this T element, params string[] conventions) where T : Element
        {
            element.Conventions = conventions;
            return element;
        }
    }
}