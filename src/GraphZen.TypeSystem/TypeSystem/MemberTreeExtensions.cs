// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class MemberTreeExtensions
    {
        public static IEnumerable<IMemberDefinition> Descendants(this IMemberParentDefinition parent)
        {
            foreach (var child in parent.Children())
            {
                yield return child;
                if (child is IMemberParentDefinition p)
                {
                    foreach (var desc in p.Descendants())
                    {
                        yield return desc;
                    }
                }
            }
        }

        public static IEnumerable<IMemberDefinition> DescendantsAndSelf(this IMemberParentDefinition parent)
        {
            yield return parent;
            foreach (var desc in parent.Descendants())
            {
                yield return desc;
            }
        }

        public static IEnumerable<IMember> Descendants(this IMemberParent parent)
        {
            foreach (var child in parent.Children())
            {
                yield return child;
                if (child is IMemberParent p)
                {
                    foreach (var desc in p.Descendants())
                    {
                        yield return desc;
                    }
                }
            }
        }

        public static IEnumerable<IMember> DescendantsAndSelf(this IMemberParent parent)
        {
            yield return parent;
            foreach (var desc in parent.Descendants())
            {
                yield return desc;
            }
        }
    }
}