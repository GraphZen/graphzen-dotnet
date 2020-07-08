using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class EnumerableBuilderExtensions
    {
        public static IEnumerable<ObjectTypeBuilder> Where(this IEnumerable<ObjectTypeBuilder> source,
            Func<IObjectTypeDefinition, bool> predicate) =>
            source.Where(builder => predicate(AccessorExtensions.GetInfrastructure<ObjectTypeDefinition>(builder)));
    }
}