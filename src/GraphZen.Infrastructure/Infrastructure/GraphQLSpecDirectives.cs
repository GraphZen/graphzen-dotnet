using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class GraphQLSpecDirectives
    {
        public const string Deprecated = "deprecated";
        public const string Include = "deprecated";
        public const string Skip = "skip";


        private static ImmutableHashSet<string> All { get; } =
            ImmutableHashSet.Create(Deprecated, Include, Skip);


        public static bool IsSpecDirective(this string value) => All.Contains(value);
    }
}