using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace GraphZen.Infrastructure
{
    public static class GraphQLNameTestHelpers
    {
        public static IReadOnlyList<(string name, string reason)> InvalidGraphQLNames { get; } =
            new List<(string name, string reason)>()
            {
                ("", "it is an empty string"),
                ("1", "it is a number"),
                ("abc 123", "it contains spaces")
            }.ToImmutableList();
    }
}
